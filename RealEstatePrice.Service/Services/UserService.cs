using System.Threading.Tasks;
using RealEstatePrice.Core.DTOs.User;
using RealEstatePrice.Core.Wrappers;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Service.Services
{
  public class UserService : IUserService
  {
    public async Task<Response<UserResponse>> GetUserAsync()
    {
        UserResponse response = new UserResponse();
        response.Id = 1;
        response.Name = "Wilson";
        return new Response<UserResponse>(response, "success");
    }
  }
}