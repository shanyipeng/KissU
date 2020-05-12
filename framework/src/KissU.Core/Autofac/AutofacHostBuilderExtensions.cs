﻿using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KissU.Core.Autofac
{
    public static class AutofacHostBuilderExtensions
    {
        public static IHostBuilder UseAutofac(this IHostBuilder hostBuilder)
        {
            var containerBuilder = new ContainerBuilder();

            return hostBuilder.ConfigureServices((_, services) =>
                {
                    services.AddObjectAccessor(containerBuilder);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder));
        }
    }
}