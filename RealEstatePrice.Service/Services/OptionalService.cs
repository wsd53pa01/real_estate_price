using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstatePrice.Core.DTOs.Optional;
using RealEstatePrice.Core.Wrappers;
using RealEstatePrice.Repository.Models;
using RealEstatePrice.Repository.Repositories;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Service.Services
{
    public class OptionalService : IOptionalService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public OptionalService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 取得所有自選資料
        /// </summary>
        public async Task<Response<List<OptionalResponse>>> GetOptionals()
        {
            using (IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                string sql = @"SELECT * 
                               FROM Optionals o
                                 INNER JOIN Prices p  
                                   ON o.PriceID = p.PriceID";
                List<OptionalResponse> response = uow.OptionalsRepository.Query<OptionalResponse>(sql).ToList();
                return new Response<List<OptionalResponse>>(response, "success");
            }
        }

        /// <summary>
        /// 新增自選資料
        /// </summary>
        public async Task<Response<OptionalResponse>> CreateOptional(OptionalCreateRequest request)
        {
            using (IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                Optionals optional = new Optionals();
                optional.PriceID = request.PriceId;
                optional.Remark = request.Remark;
                optional.Tag = request.Tag;
                uow.BeginTransaction();
                int? id = uow.OptionalsRepository.Create(optional, uow.DbTransaction);
                if(id != null)
                {
                    optional.OptionalID = (int)id;
                    uow.Commit();
                    Prices price = uow.PricesRepository.Get(optional.PriceID);
                    OptionalResponse response = new OptionalResponse();
                    response.PriceID = price.PriceID;
                    response.District = price.District;
                    response.TransactionSign = price.TransactionSign;
                    response.HouseNumberPlate = price.HouseNumberPlate;
                    response.AreaSquareMeter = price.AreaSquareMeter;
                    response.TransactionDate = price.TransactionDate;
                    response.TransactionNumber = price.TransactionNumber;
                    response.TotalFlorNumber = price.TotalFlorNumber;
                    response.BuildingState = price.BuildingState;
                    response.MainUse = price.MainUse;
                    response.MainBuildingMaterials = price.MainBuildingMaterials;
                    response.CompleteDate = price.CompleteDate;
                    response.ShiftingTotalArea = price.ShiftingTotalArea;
                    response.Room = price.Room;
                    response.Hall = price.Hall;
                    response.Health = price.Health;
                    response.Compartmented = price.Compartmented;
                    response.ManageOrganization = price.ManageOrganization;
                    response.TotalPrices = price.TotalPrices;
                    response.UnitPrices = price.UnitPrices;
                    response.BerthCategory = price.BerthCategory;
                    response.BerthAreaSquareMeter = price.BerthAreaSquareMeter;
                    response.BerthTotalPrices = price.BerthTotalPrices;
                    response.Note = price.Note;
                    response.MainBuildingArea = price.MainBuildingArea;
                    response.OutbuildingArea = price.OutbuildingArea;
                    response.BalconyArea = price.BalconyArea;
                    response.Elevator = price.Elevator;
                    response.Remark = request.Remark;
                    response.Tag = request.Tag;
                    return new Response<OptionalResponse>(response, "success");
                }
                uow.Rollback();
                return new Response<OptionalResponse>("fail");
            }
        }

        /// <summary>
        /// 更新自選資料
        /// </summary>
        /// <param name="request"></param>
        public async Task<Response<string>> UpdateOptional(OptionalUpdateRequest request)
        {
            using(IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                uow.BeginTransaction();
                Optionals optional = uow.OptionalsRepository.Get(request.OptionalID);
                optional.Tag = request.Tag;
                optional.Remark = request.Remark;
                bool isSuccess = uow.OptionalsRepository.Update(optional, uow.DbTransaction);
                if (isSuccess) 
                {
                    uow.Commit();
                    return new Response<string>("", "success");
                }
                uow.Rollback();
                return new Response<string>("fail");
            }
        }

        /// <summary>
        /// 刪除自選資料
        /// </summary>
        /// <param name="optionalId">自選資料的 Table Id</param>
        public async Task<Response<string>> DeleteOptional(int optionalId)
        {
            using (IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                uow.BeginTransaction();
                bool isSuccess = uow.OptionalsRepository.Delete(optionalId);
                if (isSuccess)
                {
                    uow.Commit();
                    return new Response<string>("", "success");
                }
                uow.Rollback();
                return new Response<string>("fail");
            }
        }
    }
}