using Dapper;

namespace RealEstatePrice.Repository.Models
{
    [Table("Users")]
    public class Users
    {
        /// <summary>
        /// Table Id
        /// </summary>
        [Key]
        [IgnoreInsert]
        [IgnoreUpdate]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Column("name")]
        public string Name { get; set;}
    }
}