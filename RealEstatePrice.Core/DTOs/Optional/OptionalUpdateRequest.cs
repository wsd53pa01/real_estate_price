using System.ComponentModel.DataAnnotations;

namespace RealEstatePrice.Core.DTOs.Optional
{
    public class OptionalUpdateRequest
    {
        [Required]
        public int OptionalID { get; set; }
        
        /// <summary>
        /// 資料標籤
        /// </summary>
        [Required]
        public string Tag { get; set; }

        /// <summary>
        /// 資料備註
        /// </summary>
        [Required]
        public string Remark { get; set; }
    }
}