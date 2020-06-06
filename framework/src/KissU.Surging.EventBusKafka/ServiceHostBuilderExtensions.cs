﻿using Autofac;
using KissU.EventBus;
using KissU.Extensions;
using Microsoft.Extensions.Hosting;

namespace KissU.Surging.EventBusKafka
{
    /// <summary>
    /// ServiceHostBuilderExtensions.
    /// </summary>
    public static class ServiceHostBuilderExtensions
    {
        /// <summary>
        /// Subscribes at.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder SubscribeAt(this IHostBuilder hostBuilder)
        {
            return hostBuilder.Configure(mapper => { mapper.Resolve<ISubscriptionAdapt>().SubscribeAt(); });
        }
    }
}