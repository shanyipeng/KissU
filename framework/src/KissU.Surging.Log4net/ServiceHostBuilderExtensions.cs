﻿using System;
using Autofac;
using KissU.Extensions;
using KissU.Helpers;
using KissU.Surging.CPlatform;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KissU.Surging.Log4net
{
    /// <summary>
    /// ServiceHostBuilderExtensions.
    /// </summary>
    public static class ServiceHostBuilderExtensions
    {
        /// <summary>
        /// Uses the log4net.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="log4NetConfigFile">The log4 net configuration file.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder UseLog4net(this IHostBuilder hostBuilder,
            string log4NetConfigFile = "log4net.config")
        {
            return hostBuilder.Configure(mapper =>
            {
                var section = AppConfig.GetSection("Logging");
                log4NetConfigFile = EnvironmentHelper.GetEnvironmentVariable(log4NetConfigFile);
                mapper.Resolve<ILoggerFactory>().AddProvider(new Log4NetProvider(log4NetConfigFile));
            });
        }

        /// <summary>
        /// Uses the log4net.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="minLevel">The minimum level.</param>
        /// <param name="log4NetConfigFile">The log4 net configuration file.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder UseLog4net(this IHostBuilder hostBuilder, LogLevel minLevel,
            string log4NetConfigFile = "log4net.config")
        {
            hostBuilder.ConfigureLogging(logger => { logger.SetMinimumLevel(minLevel); });
            return hostBuilder.Configure(mapper =>
            {
                log4NetConfigFile = EnvironmentHelper.GetEnvironmentVariable(log4NetConfigFile);
                mapper.Resolve<ILoggerFactory>().AddProvider(new Log4NetProvider(log4NetConfigFile));
            });
        }

        /// <summary>
        /// Uses the log4net.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="log4NetConfigFile">The log4 net configuration file.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder UseLog4net(this IHostBuilder hostBuilder,
            Func<string, LogLevel, bool> filter, string log4NetConfigFile = "log4net.config")
        {
            hostBuilder.ConfigureLogging(logger => { logger.AddFilter(filter); });
            return hostBuilder.Configure(mapper =>
            {
                log4NetConfigFile = EnvironmentHelper.GetEnvironmentVariable(log4NetConfigFile);
                mapper.Resolve<ILoggerFactory>().AddProvider(new Log4NetProvider(log4NetConfigFile));
            });
        }
    }
}