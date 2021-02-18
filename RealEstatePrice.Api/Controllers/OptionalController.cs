using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstatePrice.Core.DTOs.Optional;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionalController : ControllerBase
    {
        private readonly IOptionalService _optionalService;
        public OptionalController(IOptionalService optionalService)
        {
            _optionalService = optionalService;
        }

        /// <summary>
        /// 取得所有自選資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOptional()
        {
            return Ok(await _optionalService.GetOptionals());
        }

        /// <summary>
        /// 取得不動產實價
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOptional([FromBody] OptionalCreateRequest request)
        {
            return Ok(await _optionalService.CreateOptional(request));
        }

        /// <summary>
        /// 更新自選資料
        /// </summary>
        /// <param name="request"></param>
        [HttpPut]
        public async Task<IActionResult> UpdateOptional([FromBody] OptionalUpdateRequest request) 
        {
            return Ok(await _optionalService.UpdateOptional(request));
        }
        
        /// <summary>
        /// 刪除自選資料
        /// </summary>
        /// <param name="optionalId"></param>
        [HttpDelete]
        public async Task<IActionResult> DeleteOptional([FromBody] int optionalId) 
        {
            return Ok(await _optionalService.DeleteOptional(optionalId));
        }
    }
}