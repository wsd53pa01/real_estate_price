using RealEstatePrice.Repository.DbType;

namespace RealEstatePrice.Repository.Repositories
{
    /// <summary>
    ///     Unit Of Work 管理
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly ISqlLite _realEstatePrice;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     注入 PostgreSQL 連線、UnitOfWork
        /// </summary>
        /// <param name="postgresql"></param>
        /// <param name="unitOfWork"></param>
        public UnitOfWorkManager(ISqlLite realEstatePrice,
            IUnitOfWork unitOfWork)
        {
            _realEstatePrice = realEstatePrice;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     開啟 UnitOfWork 的 DB連線
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork Begin()
        {
            _unitOfWork.DbConnection = _realEstatePrice.GetDbConnection();
            _unitOfWork.DbConnection.Open();
            return _unitOfWork;
        }
    }
}