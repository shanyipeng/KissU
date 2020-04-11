﻿using System;
using System.Threading.Tasks;
using KissU.Core.Aspects;
using KissU.Core.Validations.Aspects;
using KissU.Modules.GreatWall.Application.Contracts.Dtos;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.Requests;
using KissU.Util.Ddd.Application.Contracts;

namespace KissU.Modules.GreatWall.Application.Contracts.Abstractions
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public interface IModuleAppService : IService
    {
        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="request">创建模块参数</param>
        /// <returns>Task&lt;Guid&gt;.</returns>
        Task<Guid> CreateAsync([NotNull] [Valid] CreateModuleRequest request);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="request">模块参数</param>
        /// <returns>Task.</returns>
        Task UpdateAsync([NotNull] [Valid] ModuleDto request);
    }
}