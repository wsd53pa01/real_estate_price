using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using RealEstatePrice.Repository.Models;

namespace RealEstatePrice.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _db;
        public GenericRepository(IDbConnection db)
        {
            _db = db;
        }

        public int? Create(T entity, IDbTransaction transaction = null)
        {
            int? result = _db.Insert(entity, transaction);
            return result;
        }

        public bool Create(IEnumerable<T> entity, IDbTransaction transaction)
        {
            string sql = GenerateInsertSql();
            int result = _db.Execute(sql, entity.ToArray(), transaction);
            return result == entity.Count();
        }

        public IEnumerable<int> CreateReturnId(IEnumerable<T> entity, IDbTransaction transaction)
        {
            string sql = GenerateInsertSql() + "\r\n RETURNING id";
            IEnumerable<int> result = entity.Select(x => { return _db.Query<int>(sql, x, transaction).First(); });
            return result;
        }

        public bool Delete(int id, IDbTransaction transaction = null)
        {
            int result = _db.Delete<T>(id, transaction);
            return result > 0;
        }

        public bool Delete(IEnumerable<int> id, IDbTransaction transaction)
        {
            string sql = GenerateDeleteSql();
            var param = new {Id = id.ToArray()};
            int result = _db.Execute(sql, param, transaction);
            return result == id.Count();
        }

        public int Delete(object entity, IDbTransaction transaction)
        {
            string whereClause = GenerateWhereClause(entity);
            string deleteSql = GenerateDeleteSql(whereClause);
            int result = _db.Execute(deleteSql, entity, transaction);
            return result;
        }
        public T Get(int id)
        {
            T result = _db.Get<T>(id);
            return result;
        }

        public IEnumerable<T> Get(object param = null)
        {
            param = param == null ? new { } : param;
            string orm = GenerateWhereClause(param);
            string whereClause = param.GetType().GetProperties().Any() ? orm : "";
            IEnumerable<T> result = _db.GetList<T>(whereClause, param);
            return result;
        }

        public IEnumerable<T1> Query<T1>(string query, object parameters = null, IDbTransaction transaction = null)
        {
            IEnumerable<T1> result = _db.Query<T1>(query, parameters, transaction);
            return result;
        }

        public bool Update(T entity, IDbTransaction transaction = null)
        {
            int result = _db.Update(entity, transaction);
            return result > 0;
        }

        public bool Update(IEnumerable<T> entity, IDbTransaction transaction)
        {
            string sql = GenerateUpdateSql();
            int result = _db.Execute(sql, entity.ToArray(), transaction);
            return result == entity.Count();
        }


        private string GenerateDeleteSql()
        {
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<TableAttribute>().Name;
            string sql = $"DELETE FROM {tableName} WHERE id = ANY(@Id)";
            return sql;
        }

        private string GenerateDeleteSql(string whereClause)
        {
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<TableAttribute>().Name;
            string sql = $"DELETE FROM {tableName} {whereClause}";
            return sql;
        }

        private string GenerateInsertSql()
        {
            Type type = typeof(T);

            string tableName = type.GetCustomAttribute<TableAttribute>().Name;
            Type ignoreAttribute = typeof(IgnoreInsertAttribute);

            PropertyInfo[] properties = type.GetProperties();
            IEnumerable<PropertyInfo> fields =
                properties.Where(x => x.GetCustomAttributes().All(y => ignoreAttribute != y.GetType()));
            IEnumerable<OrmResult.Parameter> parameters = from item in fields
                select new OrmResult.Parameter
                {
                    FieldName = item.GetCustomAttribute<ColumnAttribute>().Name,
                    ParameterName = item.Name
                };

            OrmResult orm = new OrmResult
            {
                TableName = tableName,
                Parameters = parameters.ToList()
            };
            string sql = $"INSERT INTO {orm.TableName}({string.Join(", ", orm.Parameters.Select(x => x.FieldName))}) " +
                         $"VALUES(@{string.Join(", @", orm.Parameters.Select(x => x.ParameterName))})";
            return sql;
        }

        private string GenerateUpdateSql()
        {
            Type type = typeof(T);

            string tableName = type.GetCustomAttribute<TableAttribute>().Name;

            IEnumerable<string> fields = type.GetProperties()
                .Where(x => x.GetCustomAttributes().All(y => y.GetType() != typeof(IgnoreUpdateAttribute)))
                .Select(x => $"{x.GetCustomAttribute<ColumnAttribute>().Name} = @{x.Name}");
            string sql = $"UPDATE {tableName} SET {string.Join(", ", fields)} WHERE id = @Id";
            return sql;
        }

        private string GenerateWhereClause(object entity)
        {
            string tableName = typeof(T).Name;

            PropertyInfo[] entityProperties = entity.GetType()
                .GetProperties();
            var field = typeof(T).GetProperties()
                .Where(x => entityProperties.Any(y => y.Name == x.Name))
                .Select(x => new {CustomAttribute = x.GetCustomAttribute<ColumnAttribute>().Name, FieldName = x.Name});
            IEnumerable<OrmResult.Parameter> parameters = from item in entityProperties
                select new OrmResult.Parameter
                {
                    FieldName = field.First(x => x.FieldName == item.Name).CustomAttribute,
                    ParameterName = item.Name
                };

            OrmResult orm = new OrmResult
            {
                TableName = tableName,
                Parameters = parameters.ToList()
            };

            List<string> clause = new List<string>();

            for (int i = 0; i < orm.Parameters.Count; i++)
            {
                bool isArray = entityProperties.ElementAt(i).PropertyType.IsArray;

                if (isArray)
                    clause.Add($"{orm.Parameters[i].FieldName} = ANY(@{orm.Parameters[i].ParameterName} )");
                else
                    clause.Add($"{orm.Parameters[i].FieldName} = @{orm.Parameters[i].ParameterName} ");
            }

            string result = $"WHERE {string.Join(" AND ", clause)}";
            return result;
        }
    }
}