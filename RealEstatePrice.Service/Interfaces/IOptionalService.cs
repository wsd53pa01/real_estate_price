using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core.DTOs.Optional;
using RealEstatePrice.Core.Wrappers;

namespace RealEstatePrice.Service.Interfaces
{
    public interface IOptionalService : IModule
    {
        /// <summary>
        /// 新增自選資料
        /// </summary>
        /// <param name="request"></param>
        Task<Response<OptionalResponse>> CreateOptional(OptionalCreateRequest request);

        /// <summary>
        /// 取得所有自選資料
        /// </summary>
        Task<Response<List<OptionalResponse>>> GetOptionals();

        /// <summary>
        /// 更新自選資料
        /// </summary>
        /// <param name="request"></param>
        Task<Response<string>> UpdateOptional(OptionalUpdateRequest request);

        /// <summary>
        /// 刪除自選資料
        /// </summary>
        /// <param name="optionalId">自選資料的 Table Id</param>
        Task<Response<string>> DeleteOptional(int optionalId);
    }
}