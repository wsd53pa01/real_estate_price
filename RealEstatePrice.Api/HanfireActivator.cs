using System;
using Hangfire;

namespace RealEstatePrice.Api
{
    /// <summary>
    ///     Hangfire 設定
    /// </summary>
    public class HangfireActivator : JobActivator
    {
        /// <summary>
        ///     Defines a mechanism for retrieving a service object
        /// </summary>
        private readonly IServiceProvider _provider;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="provider">Defines a mechanism for retrieving a service object</param>
        public HangfireActivator(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        ///     Get service from provider by type
        /// </summary>
        /// <param name="jobType">Type of service.</param>
        /// <returns>The instance of type</returns>
        public override object ActivateJob(Type jobType)
        {
            return _provider.GetService(jobType);
        }
    }
}