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
using RealEstatePrice.Core.Wrappers;
using RealEstatePrice.Repository.Models;
using RealEstatePrice.Repository.Repositories;
using RealEstatePrice.Service.FileHelpers;
using RealEstatePrice.Service.Interfaces;

namespace RealEstatePrice.Service.Services
{
    public class RealEstatePriceService : IRealEstatePriceService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public RealEstatePriceService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 取得不動產實價
        /// </summary>
        public async Task<Response<string>> FetchRealEstatePrice()
        {
            using (var client = new WebClient())
            {
                createFolder();
                Uri uri = new Uri("https://plvr.land.moi.gov.tw//Download?type=zip&fileName=lvr_landcsv.zip", UriKind.Absolute);
                client.DownloadFile(uri, @".\temp\temp.zip");
                var finish = await Unzip();
                if (finish)
                    InsertCSVDataToDB();
                RemoveFolder();
                return new Response<string>("", "success");
            }
        }

        /// <summary>
        /// 新增資料夾，如果存在不新增
        /// </summary>
        private void createFolder() 
        {
            string path = @"./temp";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 刪除資料夾，如果存在刪除資料夾
        /// </summary>
        private void RemoveFolder() 
        {
            string path = @"./temp";
            if(Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }

        /// <summary>
        /// 解壓縮檔案。
        /// </summary>
        private async Task<Boolean> Unzip()
        {
            string zipPath = @".\temp\temp.zip";
            string extractPath = @".\temp";
            Regex rgx = new Regex(@"_a.csv");
            using (ZipArchive archive = ZipFile.OpenRead(zipPath)) 
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (rgx.IsMatch(entry.FullName))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
                        entry.ExtractToFile(destinationPath);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 將 CVS Data Insert 到 DB 的 Prices Table。
        /// </summary>
        private void InsertCSVDataToDB()
        {
            // FileHelper Parse csv data
            var engine = new FileHelperEngine<LandCsv>();
            var files = Directory.GetFiles("./temp");
            List<Prices> prices = new List<Prices>();
            foreach(var file in files)
            {
                if (file.Contains("temp.zip"))
                    continue;
                var parseData = engine.ReadFile(file);
                var temp = parseData.Skip(2).Select(data => new Prices
                {
                    District = data.TheVillagesAndTownsUrbanDistrict,
                    TransactionSign = data.TransactionSign,
                    HouseNumberPlate = data.HouseNumberPlate,
                    AreaSquareMeter = data.AreaSquareMeter,
                    TransactionDate = data.TransactionDate,
                    TransactionNumber = data.TransactionNumber,
                    TotalFlorNumber = data.TotalFlorNumber,
                    BuildingState = data.BuildingState,
                    MainUse = data.MainUse,
                    MainBuildingMaterials = data.MainBuildingMaterials,
                    CompleteDate = data.CompleteDate,
                    ShiftingTotalArea = data.BuildingShiftingTotalArea,
                    Room = data.Room,
                    Hall = data.Hall,
                    Health = data.Health,
                    Compartmented = data.Compartmented,
                    ManageOrganization = data.ManageOrganization,
                    TotalPrices = data.TotalPrices,
                    UnitPrices = data.UnitPrices,
                    BerthCategory = data.BerthCategory,
                    BerthAreaSquareMeter = data.BerthAreaSquareMeter,
                    BerthTotalPrices = data.BerthTotalPrices,
                    Note = data.Note,
                    MainBuildingArea = data.MainBuildingArea,
                    OutbuildingArea = data.OutbuildingArea,
                    BalconyArea = data.BalconyArea,
                    Elevator = data.Elevator
                });
                prices.AddRange(temp);
            }

            using (IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                uow.BeginTransaction();
                
                // 判斷是否新增過，如果新增過直接 return。
                string TransactionDate = prices.Take(1).FirstOrDefault().TransactionDate;
                Prices data = uow.PricesRepository.Get(new { TransactionDate }).FirstOrDefault(); 
                if ( data != null ) 
                {
                    uow.Rollback();
                    return;
                }
                
                // 新增資料
                uow.PricesRepository.Create(prices, uow.DbTransaction);
                uow.Commit();
            }
        }

        /// <summary>
        /// 取得不動產實價
        /// </summary>
        public async Task<Response<List<RealEstatePriceResponse>>> GetRealEstatePrice(RealEstatePriceRequest request) 
        {
            using(IUnitOfWork uow = _unitOfWorkManager.Begin())
            {
                string sql = "SELECT * FROM Prices WHERE TransactionDate BETWEEN @BeginDate AND @EndDate";
                object param = new { request.BeginDate, request.EndDate };
                List<RealEstatePriceResponse> response = uow.PricesRepository
                    .Query<Prices>(sql, param)
                    .Select(price => new RealEstatePriceResponse
                    {
                        PriceID = price.PriceID,
                        District = price.District,
                        TransactionSign = price.TransactionSign,
                        HouseNumberPlate = price.HouseNumberPlate,
                        AreaSquareMeter = price.AreaSquareMeter,
                        TransactionDate = price.TransactionDate,
                        TransactionNumber = price.TransactionNumber,
                        TotalFlorNumber = price.TotalFlorNumber,
                        BuildingState = price.BuildingState,
                        MainUse = price.MainUse,
                        MainBuildingMaterials = price.MainBuildingMaterials,
                        CompleteDate = price.CompleteDate,
                        ShiftingTotalArea = price.ShiftingTotalArea,
                        Room = price.Room,
                        Hall = price.Hall,
                        Health = price.Health,
                        Compartmented = price.Compartmented,
                        ManageOrganization = price.ManageOrganization,
                        TotalPrices = price.TotalPrices,
                        UnitPrices = price.UnitPrices,
                        BerthCategory = price.BerthCategory,
                        BerthAreaSquareMeter = price.BerthAreaSquareMeter,
                        BerthTotalPrices = price.BerthTotalPrices,
                        Note = price.Note,
                        MainBuildingArea = price.MainBuildingArea,
                        OutbuildingArea = price.OutbuildingArea,
                        BalconyArea = price.BalconyArea,
                        Elevator = price.Elevator
                    })
                    .ToList();
                return new Response<List<RealEstatePriceResponse>>(response, "success");
            }
        }
    }
}