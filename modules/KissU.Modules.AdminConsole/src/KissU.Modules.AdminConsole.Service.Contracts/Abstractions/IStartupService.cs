﻿using System.Threading.Tasks;
using KissU.Core.Ioc;
using KissU.Modules.AdminConsole.Service.Contracts.Dtos.NgAlain;
using KissU.Surging.CPlatform.Filters.Implementation;
using KissU.Surging.CPlatform.Runtime.Client.Address.Resolvers.Implementation.Selectors.Implementation;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using KissU.Surging.CPlatform.Support;
using KissU.Surging.CPlatform.Support.Attributes;
using KissU.Util.Ddd.Application.Contracts;

namespace KissU.Modules.AdminConsole.Service.Contracts.Abstractions
{
    /// <summary>
    /// Interface IStartupService
    /// Implements the <see cref="IService" />
    /// </summary>
    /// <seealso cref="IService" />
    [ServiceBundle("api/{Service}")]
    public interface IStartupService : IServiceKey
    {
        /// <summary>
        /// 获取应用程序数据
        /// </summary>
        /// <returns>Task&lt;AppData&gt;.</returns>
        [Command(Strategy = StrategyType.Injection, ShuntStrategy = AddressSelectorMode.HashAlgorithm,
            ExecutionTimeoutInMilliseconds = 2500, BreakerRequestVolumeThreshold = 3, Injection = @"return 1;",
            RequestCacheEnabled = false)]
        [HttpGet(true)]
        [Authorization(AuthType = AuthorizationType.JWTBearer)]
        Task<AppData> GetAppDataAsync();
    }
}