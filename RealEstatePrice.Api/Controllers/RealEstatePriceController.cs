using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstatePrice.Core.DTOs.RealEstatePrice;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstatePriceController : ControllerBase
    {
        private readonly IRealEstatePriceService _realEstatePrice;
        public RealEstatePriceController(IRealEstatePriceService realEstatePrice)
        {
            _realEstatePrice = realEstatePrice;
        }

        /// <summary>
        /// 取得不動產實價
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRealEstatePrices([FromQuery] RealEstatePriceRequest request)
        {
            return Ok(await _realEstatePrice.GetRealEstatePrice(request));
        }

    }
}