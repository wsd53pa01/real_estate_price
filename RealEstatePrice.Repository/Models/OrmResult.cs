using System.Collections.Generic;

namespace RealEstatePrice.Repository.Models
{
    /// <summary>
    /// 在GenericRepository中反射物件，並用以生成sql子句
    /// </summary>
    public class OrmResult
    {
        /// <summary>
        /// 資料表名稱
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// sql子句的欄位及參數
        /// </summary>
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// sql子句的欄位及參數
        /// </summary>
        public class Parameter
        {
            /// <summary>
            /// 欄位名稱
            /// </summary>
            public string FieldName { get; set; }

            /// <summary>
            /// 參數名稱
            /// </summary>
            public string ParameterName { get; set; }
        }
    }
}