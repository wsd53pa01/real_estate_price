using System;
using System.Data;
using RealEstatePrice.Autofac;

namespace RealEstatePrice.Repository.Repositories
{
    public interface IUnitOfWork : IModule, IDisposable
    {
        /// <summary>
        ///     Database Connection
        /// </summary>
        IDbConnection DbConnection { get; set; }

        /// <summary>
        ///     Dapper 做 Transaction 時必須提供，
        ///     Transaction Property。
        /// </summary>
        IDbTransaction DbTransaction { get; set; }

        /// <summary>
        ///     其他相關的 Repository，
        ///     通常會是 Join Table 時使用，
        ///     因為 Join Table 會包含 2 個以上的Table，
        ///     語句上會較為模糊，
        ///     所以提供一個，單一個詞的 Repository Function Name。
        /// </summary>
        IGenericRepository<dynamic> Repository { get; }

        /// <summary>
        ///     DB Transaction Commit
        /// </summary>
        void Commit();

        /// <summary>
        ///     DB Transaction Rollback
        /// </summary>
        void Rollback();

        /// <summary>
        ///     啟動 Transaction
        /// </summary>
        void BeginTransaction();
    }
}