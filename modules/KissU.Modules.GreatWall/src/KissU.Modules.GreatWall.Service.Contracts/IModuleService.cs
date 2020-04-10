﻿using System;
using System.Threading.Tasks;
using KissU.Core.Ioc;
using KissU.Modules.GreatWall.Application.Contracts.Dtos;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.Requests;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;

namespace KissU.Modules.GreatWall.Service.Contracts
{
    /// <summary>
    /// 模块服务
    /// </summary>
    [ServiceBundle("api/{Service}")]
    public interface IModuleService : IServiceKey
    {
        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="request">创建模块参数</param>
        /// <returns>Task&lt;Guid&gt;.</returns>
        [HttpPost(true)]
        Task<Guid> CreateAsync(CreateModuleRequest request);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="request">模块参数</param>
        /// <returns>Task.</returns>
        [HttpPut(true)]
        Task UpdateAsync(ModuleDto request);
    }
}