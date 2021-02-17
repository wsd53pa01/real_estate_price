using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstatePrice.Core.DTOs.User;
using RealEstatePrice.Core.Wrappers;
using RealEstatePrice.Repository.Repositories;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UserService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 取得 User 
        /// </summary>
        public async Task<Response<List<UserResponse>>> GetUserAsync()
        {
            using(IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                List<UserResponse> response = uow.UsersRepository
                    .Get()
                    .Select( user => new UserResponse() 
                    {
                        Id = user.Id,
                        Name = user.Name
                    })
                    .ToList();
                return new Response<List<UserResponse>>(response, "success");
            }
        }
    }
}