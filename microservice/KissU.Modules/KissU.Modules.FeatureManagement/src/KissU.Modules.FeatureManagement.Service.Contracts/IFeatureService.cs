﻿using System.Threading.Tasks;
using KissU.Core.Dependency;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;

namespace KissU.Modules.FeatureManagement.Service.Contracts
{
    [ServiceBundle("api/{Service}")]
    public interface IFeatureService : IServiceKey
    {
        [HttpGet(true)]
        Task<FeatureListDto> GetAsync(string providerName, string providerKey);

        [HttpPost(true)]
        Task UpdateAsync(string providerName, string providerKey, UpdateFeaturesDto input);
    }
}