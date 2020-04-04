﻿using Autofac;
using KissU.Core;
using KissU.Core.Module;
using KissU.Surging.CPlatform;
using KissU.Surging.CPlatform.Diagnostics;
using KissU.Surging.CPlatform.Ids;
using KissU.Surging.CPlatform.Module;
using KissU.Surging.CPlatform.Mqtt;
using KissU.Surging.CPlatform.Runtime.Server;
using KissU.Surging.CPlatform.Runtime.Server.Implementation;
using KissU.Surging.Protocol.Mqtt.Diagnostics;
using KissU.Surging.Protocol.Mqtt.Implementation;
using KissU.Surging.Protocol.Mqtt.Internal.Runtime;
using KissU.Surging.Protocol.Mqtt.Internal.Runtime.Implementation;
using KissU.Surging.Protocol.Mqtt.Internal.Services;
using KissU.Surging.Protocol.Mqtt.Internal.Services.Implementation;
using Microsoft.Extensions.Logging;

namespace KissU.Surging.Protocol.Mqtt
{
    /// <summary>
    /// MqttProtocolModule.
    /// Implements the <see cref="EnginePartModule" />
    /// </summary>
    /// <seealso cref="EnginePartModule" />
    public class MqttProtocolModule : EnginePartModule
    {
        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialize(AppModuleContext context)
        {
            base.Initialize(context);
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder">构建器包装</param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            builder.RegisterType<MqttTransportDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();
            builder.RegisterType(typeof(DefaultMqttServiceFactory)).As(typeof(IMqttServiceFactory)).SingleInstance();
            builder.RegisterType(typeof(DefaultMqttBrokerEntryManager)).As(typeof(IMqttBrokerEntryManger))
                .SingleInstance();
            builder.RegisterType(typeof(MqttRemoteInvokeService)).As(typeof(IMqttRemoteInvokeService)).SingleInstance();
            builder.Register(provider =>
            {
                return new WillService(
                    provider.Resolve<ILogger<WillService>>(),
                    provider.Resolve<CPlatformContainer>()
                );
            }).As<IWillService>().SingleInstance();
            builder.Register(provider => { return new MessagePushService(new SacnScheduled()); })
                .As<IMessagePushService>().SingleInstance();
            builder.RegisterType(typeof(ClientSessionService)).As(typeof(IClientSessionService)).SingleInstance();
            builder.Register(provider =>
            {
                return new MqttChannelService(
                    provider.Resolve<IMessagePushService>(),
                    provider.Resolve<IClientSessionService>(),
                    provider.Resolve<ILogger<MqttChannelService>>(),
                    provider.Resolve<IWillService>(),
                    provider.Resolve<IMqttBrokerEntryManger>(),
                    provider.Resolve<IMqttRemoteInvokeService>(),
                    provider.Resolve<IServiceIdGenerator>()
                );
            }).As(typeof(IChannelService)).SingleInstance();
            builder.RegisterType(typeof(DefaultMqttBehaviorProvider)).As(typeof(IMqttBehaviorProvider))
                .SingleInstance();

            if (AppConfig.ServerOptions.Protocol == CommunicationProtocol.Mqtt)
            {
                RegisterDefaultProtocol(builder);
            }
            else if (AppConfig.ServerOptions.Protocol == CommunicationProtocol.None)
            {
                RegisterMqttProtocol(builder);
            }
        }

        private static void RegisterDefaultProtocol(ContainerBuilderWrapper builder)
        {
            builder.Register(provider =>
            {
                return new DotNettyMqttServerMessageListener(
                    provider.Resolve<ILogger<DotNettyMqttServerMessageListener>>(),
                    provider.Resolve<IChannelService>(),
                    provider.Resolve<IMqttBehaviorProvider>()
                );
            }).SingleInstance();
            builder.Register(provider =>
            {
                var messageListener = provider.Resolve<DotNettyMqttServerMessageListener>();
                return new DefaultServiceHost(async endPoint =>
                {
                    await messageListener.StartAsync(endPoint);
                    return messageListener;
                }, null);
            }).As<IServiceHost>();
        }

        private static void RegisterMqttProtocol(ContainerBuilderWrapper builder)
        {
            builder.Register(provider =>
            {
                return new DotNettyMqttServerMessageListener(
                    provider.Resolve<ILogger<DotNettyMqttServerMessageListener>>(),
                    provider.Resolve<IChannelService>(),
                    provider.Resolve<IMqttBehaviorProvider>()
                );
            }).SingleInstance();
            builder.Register(provider =>
            {
                var messageListener = provider.Resolve<DotNettyMqttServerMessageListener>();
                return new MqttServiceHost(async endPoint =>
                {
                    await messageListener.StartAsync(endPoint);
                    return messageListener;
                });
            }).As<IServiceHost>();
        }
    }
}