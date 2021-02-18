using System.ComponentModel.DataAnnotations;
using RealEstatePrice.Core.Validations;

namespace RealEstatePrice.Core.DTOs.Optional
{
    public class OptionalCreateRequest
    {
        [Required]
        [PriceIdValidation]
        public int PriceId { get; set; }
        
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