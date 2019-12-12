﻿using System;
using System.Collections.Generic;
using Autofac;
using KissU.Core.CPlatform;
using KissU.Core.CPlatform.EventBus;
using KissU.Core.CPlatform.EventBus.Events;
using KissU.Core.CPlatform.EventBus.Implementation;
using KissU.Core.CPlatform.Module;
using KissU.Core.CPlatform.Routing;
using KissU.Core.EventBusRabbitMQ.Configurations;
using KissU.Core.EventBusRabbitMQ.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace KissU.Core.EventBusRabbitMQ
{
    public class EventBusRabbitMQModule : EnginePartModule
    {
        public override void Initialize(AppModuleContext context)
        {
            var serviceProvider = context.ServiceProvoider;
            base.Initialize(context);
            new ServiceRouteWatch(serviceProvider.GetInstances<CPlatformContainer>(), () =>
            {
                var subscriptionAdapt = serviceProvider.GetInstances<ISubscriptionAdapt>();
                serviceProvider.GetInstances<IEventBus>().OnShutdown += (sender, args) =>
                 {
                     subscriptionAdapt.Unsubscribe();
                 };
                serviceProvider.GetInstances<ISubscriptionAdapt>().SubscribeAt();
            });
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            UseRabbitMQTransport(builder)
            .AddRabbitMQAdapt(builder);
        }

        public EventBusRabbitMQModule UseRabbitMQTransport(ContainerBuilderWrapper builder)
        {
            builder.RegisterType(typeof(Implementation.EventBusRabbitMQ)).As(typeof(IEventBus)).SingleInstance();
            builder.RegisterType(typeof(DefaultConsumeConfigurator)).As(typeof(IConsumeConfigurator)).SingleInstance();
            builder.RegisterType(typeof(InMemoryEventBusSubscriptionsManager)).As(typeof(IEventBusSubscriptionsManager)).SingleInstance();
            builder.Register(provider =>
            {
                var logger = provider.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();
                EventBusOption option = new EventBusOption();
                var section = CPlatform.AppConfig.GetSection("EventBus");
                if (section.Exists())
                    option = section.Get<EventBusOption>();
                else if (AppConfig.Configuration != null)
                    option = AppConfig.Configuration.Get<EventBusOption>();
                var factory = new ConnectionFactory()
                {
                    HostName = option.EventBusConnection,
                    UserName = option.EventBusUserName,
                    Password = option.EventBusPassword,
                    VirtualHost = option.VirtualHost,
                    Port = int.Parse(option.Port),
                };
                factory.RequestedHeartbeat = 60;
                AppConfig.BrokerName = option.BrokerName;
                AppConfig.MessageTTL = option.MessageTTL;
                AppConfig.RetryCount = option.RetryCount;
                AppConfig.PrefetchCount = option.PrefetchCount;
                AppConfig.FailCount = option.FailCount;
                return new DefaultRabbitMQPersistentConnection(factory, logger);
            }).As<IRabbitMQPersistentConnection>();
            return this;
        }

        private ContainerBuilderWrapper UseRabbitMQEventAdapt(ContainerBuilderWrapper builder, Func<IServiceProvider, ISubscriptionAdapt> adapt)
        {
            builder.RegisterAdapter(adapt);
            return builder;
        }

        private EventBusRabbitMQModule AddRabbitMQAdapt(ContainerBuilderWrapper builder)
        {
            UseRabbitMQEventAdapt(builder, provider =>
               new RabbitMqSubscriptionAdapt(
                   provider.GetService<IConsumeConfigurator>(),
                   provider.GetService<IEnumerable<IIntegrationEventHandler>>()
                   )
             );
            return this;
        }
    }
}