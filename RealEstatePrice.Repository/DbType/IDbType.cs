using System.Data;

namespace RealEstatePrice.Repository.DbType
{
    public interface IDbType
    {
        IDbConnection GetDbConnection();
    }
}