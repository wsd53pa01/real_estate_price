using System.ComponentModel.DataAnnotations;

namespace RealEstatePrice.Core.DTOs.RealEstatePrice
{
    public class RealEstatePriceRequest
    {
        /// <summary>
        /// 交易日期起
        /// </summary>
        [Required]
        public string BeginDate { get; set; }

        /// <summary>
        /// 交易日期迄
        /// </summary>
        [Required]
        public string EndDate { get; set; }
    }
}