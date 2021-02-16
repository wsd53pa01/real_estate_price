using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using RealEstatePrice.Core;

namespace RealEstatePrice.Repository.DbType
{
    public class SqlLite : ISqlLite
    {
        private readonly ConnectionStrings _connectionString;

        public SqlLite(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value;
            
        }

        public IDbConnection GetDbConnection()
        {
            SQLiteConnection dbConnection = new SQLiteConnection(_connectionString.RealEstatePrice);
            return dbConnection;
        }
    }
}