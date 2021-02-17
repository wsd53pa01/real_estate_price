using System;
using System.Data;
using Dapper;
using RealEstatePrice.Repository.Models;

namespace RealEstatePrice.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Users> UsersRepository
            => new GenericRepository<Users>(DbConnection);

        public IGenericRepository<dynamic> Repository
            => new GenericRepository<dynamic>(DbConnection);
            
        public UnitOfWork()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
        }
        
        public IDbConnection DbConnection { get; set; }
        public IDbTransaction DbTransaction { get; set; }

        public void BeginTransaction()
        {
            DbTransaction = DbConnection.BeginTransaction();
        }

        public void Commit()
        {
            DbTransaction.Commit();
        }

        public void Rollback()
        {
            DbTransaction.Rollback();
        }

        #region IDisposable Support

        private bool disposedValue; // 偵測多餘的呼叫

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DbConnection != null)
            {
                // DbConnection.Close();
                if (!disposedValue)
                {
                    if (disposing) DbConnection.Dispose();

                    // TODO: 釋放非受控資源 (非受控物件) 並覆寫下方的完成項。
                    // TODO: 將大型欄位設為 null。
                    disposedValue = true;
                }
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放非受控資源的程式碼時，才覆寫完成項。
        // ~UnitOfWork() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        #endregion IDisposable Support
    }
}