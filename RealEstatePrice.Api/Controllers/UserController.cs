using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUserAsync());
        }
        
    }
}