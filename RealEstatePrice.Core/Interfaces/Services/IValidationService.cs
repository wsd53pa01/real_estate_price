using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core.DTOs.RealEstatePrice;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Core.Interfaces.Services
{
    public interface IValidationService : IModule
    {
        /// <summary>
        /// 驗證 PriceId
        /// </summary>
        /// <param name="priceId">price's table id</param>
        bool PriceIdValidate(int priceId);
    }
}