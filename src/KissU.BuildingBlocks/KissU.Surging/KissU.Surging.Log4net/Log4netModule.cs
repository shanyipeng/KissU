﻿using KissU.Core.Module;
using KissU.Core.Utilities;
using KissU.Surging.CPlatform;
using KissU.Surging.CPlatform.Module;
using KissU.Surging.CPlatform.Utilities;
using Microsoft.Extensions.Logging;

namespace KissU.Surging.Log4net
{
    /// <summary>
    /// Log4netModule.
    /// Implements the <see cref="EnginePartModule" />
    /// </summary>
    /// <seealso cref="EnginePartModule" />
    public class Log4netModule : EnginePartModule
    {
        private string log4NetConfigFile = "${LogPath}|log4net.config";

        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialize(AppModuleContext context)
        {
            var serviceProvider = context.ServiceProvoider;
            base.Initialize(context);
            var section = AppConfig.GetSection("Logging");
            log4NetConfigFile = EnvironmentHelper.GetEnvironmentVariable(log4NetConfigFile);
            serviceProvider.GetInstances<ILoggerFactory>().AddProvider(new Log4NetProvider(log4NetConfigFile));
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder">构建器包装</param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
        }
    }
}