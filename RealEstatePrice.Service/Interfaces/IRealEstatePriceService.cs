using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core.DTOs.RealEstatePrice;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Service.Interfaces
{
    public interface IRealEstatePriceService : IModule
    {
        /// <summary>
        /// 擷取當期不動產的 Open Data
        /// </summary>
        Task<Response<string>> FetchRealEstatePrice();

        /// <summary>
        /// 取得不動產實價
        /// </summary>
        Task<Response<List<RealEstatePriceResponse>>> GetRealEstatePrice(RealEstatePriceRequest request);
    }
}