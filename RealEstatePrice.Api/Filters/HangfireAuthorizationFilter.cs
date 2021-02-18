using Hangfire.Dashboard;

namespace RealEstatePrice.Api.Filters
{ 
    /// <summary>
    ///     Hangfire Filter 設定
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            // 驗證
            // var httpContext = context.GetHttpContext();
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            // return httpContext.User.IsInRole("Administrator");
            return true;
        }
    }
}