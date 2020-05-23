﻿using System.Diagnostics;
using System.Threading.Tasks;
using KissU.Surging.CPlatform.Routing;
using KissU.Surging.ProxyGenerator.Interceptors;
using KissU.Surging.ProxyGenerator.Interceptors.Implementation;

namespace KissU.Surging.System.Intercept
{
    public class LogProviderInterceptor : IInterceptor
    {
        private readonly IInterceptorProvider _interceptorProvider;

        private readonly IServiceRouteProvider _serviceRouteProvider;
        public LogProviderInterceptor(IInterceptorProvider interceptorProvider, IServiceRouteProvider serviceRouteProvider)
        {
            _interceptorProvider = interceptorProvider;
            _serviceRouteProvider = serviceRouteProvider;
        }

        public async Task Intercept(IInvocation invocation)
        {
            var route = await _serviceRouteProvider.Locate(invocation.ServiceId);
            var cacheMetadata = route.ServiceDescriptor.GetIntercept("Log");
            if (cacheMetadata != null)
            {
                var watch = Stopwatch.StartNew();
                await invocation.Proceed();
                var result = invocation.ReturnValue;
            }
        }
    }
}
