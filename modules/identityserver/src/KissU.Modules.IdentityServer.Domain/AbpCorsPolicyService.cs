﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using KissU.Modules.IdentityServer.Domain.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace KissU.Modules.IdentityServer.Domain
{
    public class AbpCorsPolicyService : ICorsPolicyService
    {
        public ILogger<AbpCorsPolicyService> Logger { get; set; }
        protected IHybridServiceScopeFactory HybridServiceScopeFactory { get; }
        protected IDistributedCache<AllowedCorsOriginsCacheItem> Cache { get; }

        public AbpCorsPolicyService(
            IDistributedCache<AllowedCorsOriginsCacheItem> cache,
            IHybridServiceScopeFactory hybridServiceScopeFactory)
        {
            Cache = cache;
            HybridServiceScopeFactory = hybridServiceScopeFactory;
            Logger = NullLogger<AbpCorsPolicyService>.Instance;
        }

        public virtual async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var cacheItem = await Cache.GetOrAddAsync(AllowedCorsOriginsCacheItem.AllOrigins, CreateCacheItemAsync);

            var isAllowed = cacheItem.AllowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            if (!isAllowed)
            {
                Logger.LogWarning($"Origin is not allowed: {origin}");
            }

            return isAllowed;
        }

        protected virtual async Task<AllowedCorsOriginsCacheItem> CreateCacheItemAsync()
        {
            // doing this here and not in the ctor because: https://github.com/aspnet/AspNetCore/issues/2377
            using (var scope = HybridServiceScopeFactory.CreateScope())
            {
                var clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository>();

                return new AllowedCorsOriginsCacheItem
                {
                    AllowedOrigins = (await clientRepository.GetAllDistinctAllowedCorsOriginsAsync()).ToArray()
                };
            }
        }
    }
}
