﻿using System;
using KissU.Util.Applications.Dtos;

namespace KissU.Modules.GreatWall.Application.Dtos.Requests
{
    /// <summary>
    /// 用户角色参数
    /// </summary>
    public class UserRoleRequest : RequestBase
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 用户标识列表
        /// </summary>
        public string UserIds { get; set; }
    }
}