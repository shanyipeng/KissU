﻿using System.Threading;
using System.Threading.Tasks;
using Autofac;
using KissU.Core.CPlatform;
using KissU.Core.CPlatform.Engines;
using KissU.Core.CPlatform.Module;
using KissU.Core.CPlatform.Runtime.Server;
using KissU.Core.ServiceHosting.Extensions.Runtime;
using KissU.Core.ServiceHosting.Extensions.Runtime.Implementation;
using Microsoft.Extensions.Logging;

namespace KissU.Core.ServiceHosting.Extensions
{
    /// <summary>
    /// 服务主机模块
    /// </summary>
    public class ServiceHostModule : EnginePartModule
    {
        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialize(AppModuleContext context)
        {
            var serviceProvider = context.ServiceProvoider;
            base.Initialize(context);
            serviceProvider.GetInstances<IServiceEngineLifetime>().ServiceEngineStarted.Register(() =>
            {
                var provider = serviceProvider.GetInstances<IBackgroundServiceEntryProvider>();
                var entries = provider.GetEntries();
                foreach (var entry in entries)
                {
                    var cts = new CancellationTokenSource();
                    Task.Run(() =>
                    {
                        try
                        {
                            entry.Behavior.StartAsync(cts.Token);
                        }
                        catch
                        {
                            entry.Behavior.StopAsync(cts.Token);
                        }
                    });
                }
            });
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder">构建器包装</param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            builder.Register(provider =>
            {
                return new DefaultBackgroundServiceEntryProvider(
                    provider.Resolve<IServiceEntryProvider>(),
                    provider.Resolve<ILogger<DefaultBackgroundServiceEntryProvider>>(),
                    provider.Resolve<CPlatformContainer>()
                );
            }).As(typeof(IBackgroundServiceEntryProvider)).SingleInstance();
        }
    }
}