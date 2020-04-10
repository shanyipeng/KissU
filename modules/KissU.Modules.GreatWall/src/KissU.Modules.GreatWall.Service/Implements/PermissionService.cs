﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Modules.GreatWall.Application.Contracts.Abstractions;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.Requests;
using KissU.Modules.GreatWall.Application.Contracts.Queries;
using KissU.Modules.GreatWall.Service.Contracts;
using KissU.Surging.ProxyGenerator;

namespace KissU.Modules.GreatWall.Service.Implements
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : ProxyServiceBase, IPermissionService
    {
        private readonly IPermissionAppService _appService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appService">应用服务</param>
        /// <exception cref="ArgumentNullException">appService</exception>
        public PermissionService(IPermissionAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="query">权限参数</param>
        /// <returns>Task&lt;List&lt;Guid&gt;&gt;.</returns>
        public async Task<List<Guid>> GetResourceIdsAsync(PermissionQuery query)
        {
            return await _appService.GetResourceIdsAsync(query);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="request">参数</param>
        public async Task SaveAsync(SavePermissionRequest request)
        {
            await _appService.SaveAsync(request);
        }
    }
}