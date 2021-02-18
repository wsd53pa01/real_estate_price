using RealEstatePrice.Core.DTOs.RealEstatePrice;

namespace RealEstatePrice.Core.DTOs.Optional
{
    public class OptionalResponse : RealEstatePriceResponse
    {
        /// <summary>
        /// Table Id 
        /// </summary>
        public int OptionalID { get; set; }

        /// <summary>
        /// Data Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Data 備註
        /// </summary>
        public string Remark  { get; set; }
    }
}