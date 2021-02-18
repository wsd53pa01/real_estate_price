using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileHelpers;
using RealEstatePrice.Core.DTOs.RealEstatePrice;
using RealEstatePrice.Core.Interfaces.Services;
using RealEstatePrice.Core.Wrappers;
using RealEstatePrice.Repository.Models;
using RealEstatePrice.Repository.Repositories;
using RealEstatePrice.Service.FileHelpers;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Service.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ValidationService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 驗證 PriceId
        /// </summary>
        /// <param name="priceId">price's table id</param>
        public bool PriceIdValidate(int priceId)
        {
            using (IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                Prices price = uow.PricesRepository.Get(priceId);
                return price != null;
            }
        }
    }
}