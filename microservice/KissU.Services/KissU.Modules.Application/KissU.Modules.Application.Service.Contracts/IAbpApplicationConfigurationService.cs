﻿using System.Threading.Tasks;
using KissU.Dependency;
using KissU.Modules.Application.Configurations;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;

namespace KissU.Modules.Application.Service.Contracts
{
    [ServiceBundle("api/abp/application-configuration")]
    public interface IAbpApplicationConfigurationService : IServiceKey
    {
        [HttpGet(true)]
        Task<ApplicationConfigurationDto> GetAsync();
    }
}