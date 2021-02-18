using System.ComponentModel.DataAnnotations;

namespace RealEstatePrice.Core.DTOs.Optional
{
    public class OptionalCreateRequest
    {
        [Required]
        public int PriceID { get; set; }
        
        /// <summary>
        /// 資料標籤
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 資料備註
        /// </summary>
        public string Remark { get; set; }
    }
}