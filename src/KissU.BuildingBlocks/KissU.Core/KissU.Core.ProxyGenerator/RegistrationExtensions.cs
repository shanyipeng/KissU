﻿using System;
using KissU.Core.CPlatform.Module;
using KissU.Core.ProxyGenerator.Interceptors;
using KissU.Core.ProxyGenerator.Interceptors.Implementation;

namespace KissU.Core.ProxyGenerator
{
    /// <summary>
    /// RegistrationExtensions.
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Adds the client intercepted.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="interceptorServiceType">Type of the interceptor service.</param>
        public static void AddClientIntercepted(this ContainerBuilderWrapper builder, Type interceptorServiceType)
        {
            builder.RegisterType(interceptorServiceType).As<IInterceptor>().SingleInstance();
            builder.RegisterType<InterceptorProvider>().As<IInterceptorProvider>().SingleInstance();
        }

        /// <summary>
        /// Adds the client intercepted.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="interceptorServiceTypes">The interceptor service types.</param>
        public static void AddClientIntercepted(this ContainerBuilderWrapper builder,
            params Type[] interceptorServiceTypes)
        {
            builder.RegisterTypes(interceptorServiceTypes).As<IInterceptor>().SingleInstance();
            builder.RegisterType<InterceptorProvider>().As<IInterceptorProvider>().SingleInstance();
        }
    }
}