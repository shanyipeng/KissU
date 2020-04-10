﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KissU.Modules.GreatWall.Data.Pos;
using KissU.Modules.GreatWall.Domain.Models;
using KissU.Modules.GreatWall.Domain.Repositories;
using KissU.Modules.GreatWall.Domain.UnitOfWorks;
using KissU.Util.EntityFrameworkCore.Core;
using Microsoft.EntityFrameworkCore;

namespace KissU.Modules.GreatWall.Data.Repositories
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        /// <summary>
        /// 初始化权限仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public PermissionRepository(IGreatWallUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// 获取资源标识列表
        /// </summary>
        /// <param name="applicationId">应用程序标识</param>
        /// <param name="roleId">角色标识</param>
        /// <param name="isDeny">是否拒绝</param>
        /// <returns>Task&lt;List&lt;Guid&gt;&gt;.</returns>
        public async Task<List<Guid>> GetResourceIdsAsync(Guid applicationId, Guid roleId, bool isDeny)
        {
            var queryable = from permission in Find()
                join resource in UnitOfWork.Set<ResourcePo>() on permission.ResourceId equals resource.Id
                where resource.ApplicationId == applicationId && permission.RoleId == roleId &&
                      permission.IsDeny == isDeny
                select resource.Id;
            return await queryable.ToListAsync();
        }

        /// <summary>
        /// 获取权限标识列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        /// <returns>Task&lt;List&lt;Guid&gt;&gt;.</returns>
        public async Task<List<Guid>> GetPermissionIdsAsync(Guid roleId, List<Guid> resourceIds)
        {
            return await Find().Where(t => t.RoleId == roleId && resourceIds.Contains(t.ResourceId)).Select(t => t.Id)
                .ToListAsync();
        }

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <param name="resourceIds">资源标识列表</param>
        public async Task RemoveAsync(Guid roleId, List<Guid> resourceIds)
        {
            var permissionIds = await GetPermissionIdsAsync(roleId, resourceIds);
            await RemoveAsync(permissionIds);
        }
    }
}