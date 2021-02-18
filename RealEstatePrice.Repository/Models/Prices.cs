using System;
using Dapper;

namespace RealEstatePrice.Repository.Models
{

    [Table("Prices")]
    public class Prices
    {
        /// <summary>
        /// Table Id
        /// </summary>
        [Key]
        [IgnoreInsert]
        [IgnoreUpdate]
        [Column("PriceID")]
        public int PriceID { get; set; }

        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        [Column("District")]
        public string District { get; set;}

        /// <summary>
        /// 交易標的
        /// </summary>
        [Column("TransactionSign")]
        public string TransactionSign { get; set;}

        /// <summary>
        /// 土地區段位置建物區段門牌
        /// </summary>
        [Column("HouseNumberPlate")]
        public string HouseNumberPlate { get; set; }

        /// <summary>
        /// 土地移轉總面積平方公尺
        /// </summary>
        [Column("AreaSquareMeter")]
        public string AreaSquareMeter { get; set; }

        /// <summary>
        /// 交易年月日
        /// </summary>
        [Column("TransactionDate")]
        public string TransactionDate { get; set; }

        /// <summary>
        /// 交易筆棟數
        /// </summary>
        [Column("TransactionNumber")]
        public string TransactionNumber { get; set; }

        /// <summary>
        /// 總樓層數
        /// </summary>
        [Column("TotalFlorNumber")]
        public string TotalFlorNumber { get; set; }

        /// <summary>
        /// 建物型態
        /// </summary>
        [Column("BuildingState")]
        public string BuildingState { get; set; }

        /// <summary>
        /// 主要用途
        /// </summary>
        [Column("MainUse")]
        public string MainUse { get; set; }

        /// <summary>
        /// 主要建材
        /// </summary>
        [Column("MainBuildingMaterials")]
        public string MainBuildingMaterials { get; set; }

        /// <summary>
        /// 建築完成年月
        /// </summary>
        [Column("CompleteDate")]
        public string CompleteDate { get; set; }

        /// <summary>
        /// 建物移轉總面積平方公尺
        /// </summary>
        [Column("ShiftingTotalArea")]
        public string ShiftingTotalArea { get; set; }

        /// <summary>
        /// 建物現況格局-房
        /// </summary>
        [Column("Room")]
        public string Room { get; set; }

        /// <summary>
        /// 建物現況格局-廳
        /// </summary>
        [Column("Hall")]
        public string Hall { get; set; }

        /// <summary>
        /// 建物現況格局-衛
        /// </summary>
        [Column("Health")]
        public string Health { get; set; }

        /// <summary>
        /// 建物現況格局-隔間
        /// </summary>
        [Column("Compartmented")]
        public string Compartmented { get; set; }

        /// <summary>
        /// 有無管理組織
        /// </summary>
        [Column("ManageOrganization")]
        public string ManageOrganization { get; set; }

        /// <summary>
        /// 總價元
        /// </summary>
        [Column("TotalPrices")]
        public string TotalPrices { get; set; }

        /// <summary>
        /// 單價元平方公尺
        /// </summary>
        [Column("UnitPrices")]
        public string UnitPrices { get; set; }

        /// <summary>
        /// 車位類別
        /// </summary>
        [Column("BerthCategory")]
        public string BerthCategory { get; set; }

        /// <summary>
        /// 車位移轉總面積(平方公尺)
        /// </summary>
        [Column("BerthAreaSquareMeter")]
        public string BerthAreaSquareMeter { get; set; }

        /// <summary>
        /// 車位總價元
        /// </summary>
        [Column("BerthTotalPrices")]
        public string BerthTotalPrices { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Column("Note")]
        public string Note { get; set; }

        /// <summary>
        /// 主建物面積
        /// </summary>
        [Column("MainBuildingArea")]
        public string MainBuildingArea { get; set; }

        /// <summary>
        /// 附屬建物面積
        /// </summary>
        [Column("OutbuildingArea")]
        public string OutbuildingArea { get; set; }

        /// <summary>
        /// 陽台面積
        /// </summary>
        [Column("BalconyArea")]
        public string BalconyArea { get; set; }

        /// <summary>
        /// 電梯
        /// </summary>
        [Column("Elevator")]
        public string Elevator { get; set; }
    }
}