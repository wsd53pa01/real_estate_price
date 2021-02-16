using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RealEstatePrice.Repository.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        ///     INSERT/DELETE/UPDATE/SELECT 以外的操作
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">參數</param>
        /// <param name="transaction">transaction</param>
        /// <returns></returns>
        IEnumerable<T1> Query<T1>(string query, object parameters = null, IDbTransaction transaction = null);

        #region R

        /// <summary>
        ///     取得特定的資料
        /// </summary>
        /// <param name="id">model Id</param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        ///     依查詢條件取得IEnumerable&lt;T&gt;
        /// </summary>
        /// <param name="param">where clause. ※屬性請與model property相同名稱 Ex: new { Code = "ABC", Id = idGroup.ToArray() }。</param>
        /// <returns></returns>
        IEnumerable<T> Get(object param = null);

        #endregion R

        #region CUD

        /// <summary>
        ///     單筆新增並回傳Id
        /// </summary>
        /// <param name="entity">要新增的資料</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        int? Create(T entity, IDbTransaction transaction = null);

        /// <summary>
        ///     大量新增
        /// </summary>
        /// <param name="entity">要新增的資料</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        bool Create(IEnumerable<T> entity, IDbTransaction transaction);

        /// <summary>
        ///     大量新增並回傳Id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IEnumerable<int> CreateReturnId(IEnumerable<T> entity, IDbTransaction transaction);

        /// <summary>
        ///     依照Id更新
        /// </summary>
        /// <param name="entity">單一Table物件</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        bool Update(T entity, IDbTransaction transaction = null);

        /// <summary>
        ///     依照Id大量更新
        /// </summary>
        /// <param name="entity">多個Table物件</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        bool Update(IEnumerable<T> entity, IDbTransaction transaction);

        /// <summary>
        ///     依照Id刪除
        /// </summary>
        /// <param name="id">Table Id</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        bool Delete(int id, IDbTransaction transaction = null);

        /// <summary>
        ///     依照Id大量刪除
        /// </summary>
        /// <param name="id">Table Id</param>
        /// <param name="transaction">db transaction</param>
        /// <returns></returns>
        bool Delete(IEnumerable<int> id, IDbTransaction transaction);

        /// <summary>
        ///     依照where條件刪除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns>被刪除的數量</returns>
        int Delete(object entity, IDbTransaction transaction);

        #endregion CUD
    }
}