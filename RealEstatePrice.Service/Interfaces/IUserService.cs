using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core.DTOs.User;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Service.Interfaces
{
    public interface IUserService : IModule
    {
        /// <summary>
        /// 取得 User 
        /// </summary>
        Task<Response<List<UserResponse>>> GetUserAsync();
    }
}