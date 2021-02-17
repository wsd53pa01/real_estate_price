using RealEstatePrice.Autofac;

namespace RealEstatePrice.Repository.Repositories
{
    public interface IUnitOfWorkManager : IModule
    {
        IUnitOfWork Begin();
    }
}