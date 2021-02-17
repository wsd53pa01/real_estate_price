namespace RealEstatePrice.Core
{
    /// <summary>
    /// appsettings.json mapping
    /// </summary>
    public class Appsettings
    {
      /// <summary>
      /// 連線字串
      /// </summary>
      public ConnectionStrings ConnectionsStrings { get; set; }
    }

    /// <summary>
    /// 連線字串
    /// </summary>
    public class ConnectionStrings
    {
      /// <summary>
      /// 不動產實價 DB 連線字串
      /// </summary>
      public string RealEstatePrice { get; set; }
    }
}