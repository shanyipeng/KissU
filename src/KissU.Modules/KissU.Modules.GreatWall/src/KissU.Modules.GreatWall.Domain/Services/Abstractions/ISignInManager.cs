﻿// <copyright file="ISignInManager.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

namespace KissU.Modules.GreatWall.Domain.Services.Abstractions
{
    using KissU.Modules.GreatWall.Domain.Models;
    using KissU.Modules.GreatWall.Domain.Shared.Results;
    using Util.Domains.Services;

    /// <summary>
    ///     登录服务
    /// </summary>
    public interface ISignInManager : IDomainService
    {
        /// <summary>
        ///     登录
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="isPersistent">cookie是否持久保留,设置为false,当关闭浏览器则cookie失效</param>
        /// <param name="lockoutOnFailure">达到登录失败次数是否锁定</param>
        Task<SignInResult> SignInAsync(User user, string password, bool isPersistent = false,
            bool lockoutOnFailure = true);

        /// <summary>
        ///     退出登录
        /// </summary>
        Task SignOutAsync();
    }
}
