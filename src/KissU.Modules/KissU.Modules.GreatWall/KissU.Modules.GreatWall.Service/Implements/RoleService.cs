﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Modules.GreatWall.Application.Contracts.Abstractions;
using KissU.Modules.GreatWall.Application.Contracts.Dtos;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.Requests;
using KissU.Modules.GreatWall.Application.Contracts.Queries;
using KissU.Modules.GreatWall.Service.Contracts;
using KissU.Surging.ProxyGenerator;
using KissU.Util.Ddd.Domains.Repositories;

namespace KissU.Modules.GreatWall.Service.Implements
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService : ProxyServiceBase, IRoleService
    {
        private readonly IRoleAppService _appService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appService">应用服务</param>
        /// <exception cref="ArgumentNullException">appService</exception>
        public RoleService(IRoleAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        /// <summary>
        /// 通过标识获取
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>Task&lt;RoleDto&gt;.</returns>
        public async Task<RoleDto> GetByIdAsync(string id)
        {
            return await _appService.GetByIdAsync(id);
        }

        /// <summary>
        /// 通过标识列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        /// <returns>Task&lt;List&lt;RoleDto&gt;&gt;.</returns>
        public async Task<List<RoleDto>> GetByIdsAsync(string ids)
        {
            return await _appService.GetByIdsAsync(ids);
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns>Task&lt;List&lt;RoleDto&gt;&gt;.</returns>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            return await _appService.GetAllAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>Task&lt;List&lt;RoleDto&gt;&gt;.</returns>
        public async Task<List<RoleDto>> QueryAsync(RoleQuery parameter)
        {
            return await _appService.QueryAsync(parameter);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>Task&lt;PagerList&lt;RoleDto&gt;&gt;.</returns>
        public async Task<PagerList<RoleDto>> PagerQueryAsync(RoleQuery parameter)
        {
            return await _appService.PagerQueryAsync(parameter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        public async Task DeleteAsync(string ids)
        {
            await _appService.DeleteAsync(ids);
        }

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>Task&lt;List&lt;RoleDto&gt;&gt;.</returns>
        public async Task<List<RoleDto>> GetRolesAsync(Guid userId)
        {
            return await _appService.GetRolesAsync(userId);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="request">创建角色参数</param>
        /// <returns>Task&lt;Guid&gt;.</returns>
        public async Task<Guid> CreateAsync(CreateRoleRequest request)
        {
            return await _appService.CreateAsync(request);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request">修改角色参数</param>
        public async Task UpdateAsync(UpdateRoleRequest request)
        {
            await _appService.UpdateAsync(request);
        }

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="request">用户角色参数</param>
        public async Task AddUsersToRoleAsync(UserRoleRequest request)
        {
            await _appService.AddUsersToRoleAsync(request);
        }

        /// <summary>
        /// 从角色移除用户
        /// </summary>
        /// <param name="request">用户角色参数</param>
        public async Task RemoveUsersFromRoleAsync(UserRoleRequest request)
        {
            await _appService.RemoveUsersFromRoleAsync(request);
        }
    }
}