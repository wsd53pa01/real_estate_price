using System;
using Dapper;

namespace RealEstatePrice.Repository.Models
{
    /// <summary>
    /// 自選
    /// </summary>
    [Table("Optionals")]
    public class Optionals
    {
        /// <summary>
        /// Table Id
        /// </summary>
        [Key]
        [IgnoreInsert]
        [IgnoreUpdate]
        [Column("OptionalID")]
        public int OptionalID { get; set; }

        /// <summary>
        /// Prices Table id
        /// </summary>
        [Column("PriceID")]
        public int PriceID { get; set; }

        /// <summary>
        /// 標籤
        /// </summary>
        [Column("Tag")]
        public string Tag { get; set; }

        /// <summary>
        /// 自選資料的備註欄為
        /// </summary>
        [Column("Remark")]
        public string Remark { get; set; }
    }
}