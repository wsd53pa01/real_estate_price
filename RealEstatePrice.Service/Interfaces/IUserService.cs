using System.Threading.Tasks;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core.DTOs.User;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Service.Interfaces
{
    public interface IUserService : IModule
    {
        Task<Response<UserResponse>> GetUserAsync();
    }
}