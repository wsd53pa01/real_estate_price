namespace RealEstatePrice.Core.DTOs.RealEstatePrice
{
    public class RealEstatePriceResponse
    {
        /// <summary>
        /// Table Id
        /// </summary>
        public int PriceID { get; set; }

        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        public string District { get; set;}

        /// <summary>
        /// 交易標的
        /// </summary>
        public string TransactionSign { get; set;}

        /// <summary>
        /// 土地區段位置建物區段門牌
        /// </summary>
        public string HouseNumberPlate { get; set; }

        /// <summary>
        /// 土地移轉總面積平方公尺
        /// </summary>
        public string AreaSquareMeter { get; set; }

        /// <summary>
        /// 交易年月日
        /// </summary>
        public string TransactionDate { get; set; }

        /// <summary>
        /// 交易筆棟數
        /// </summary>
        public string TransactionNumber { get; set; }

        /// <summary>
        /// 總樓層數
        /// </summary>
        public string TotalFlorNumber { get; set; }

        /// <summary>
        /// 建物型態
        /// </summary>
        public string BuildingState { get; set; }

        /// <summary>
        /// 主要用途
        /// </summary>
        public string MainUse { get; set; }

        /// <summary>
        /// 主要建材
        /// </summary>
        public string MainBuildingMaterials { get; set; }

        /// <summary>
        /// 建築完成年月
        /// </summary>
        public string CompleteDate { get; set; }

        /// <summary>
        /// 建物移轉總面積平方公尺
        /// </summary>
        public string ShiftingTotalArea { get; set; }

        /// <summary>
        /// 建物現況格局-房
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// 建物現況格局-廳
        /// </summary>
        public string Hall { get; set; }

        /// <summary>
        /// 建物現況格局-衛
        /// </summary>
        public string Health { get; set; }

        /// <summary>
        /// 建物現況格局-隔間
        /// </summary>
        public string Compartmented { get; set; }

        /// <summary>
        /// 有無管理組織
        /// </summary>
        public string ManageOrganization { get; set; }

        /// <summary>
        /// 總價元
        /// </summary>
        public string TotalPrices { get; set; }

        /// <summary>
        /// 單價元平方公尺
        /// </summary>
        public string UnitPrices { get; set; }

        /// <summary>
        /// 車位類別
        /// </summary>
        public string BerthCategory { get; set; }

        /// <summary>
        /// 車位移轉總面積(平方公尺)
        /// </summary>
        public string BerthAreaSquareMeter { get; set; }

        /// <summary>
        /// 車位總價元
        /// </summary>
        public string BerthTotalPrices { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 主建物面積
        /// </summary>
        public string MainBuildingArea { get; set; }

        /// <summary>
        /// 附屬建物面積
        /// </summary>
        public string OutbuildingArea { get; set; }

        /// <summary>
        /// 陽台面積
        /// </summary>
        public string BalconyArea { get; set; }

        /// <summary>
        /// 電梯
        /// </summary>
        public string Elevator { get; set; }
    }
}