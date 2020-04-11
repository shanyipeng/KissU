﻿using System;
using KissU.Core.Sessions;
using KissU.Modules.GreatWall.Data.Repositories;
using KissU.Modules.GreatWall.Domain.Models;
using KissU.Modules.GreatWall.Domain.Services.Implements;
using KissU.Modules.GreatWall.Domain.Shared.Describers;
using KissU.Modules.GreatWall.Domain.Shared.Extensions;
using KissU.Modules.GreatWall.Domain.Shared.Options;
using KissU.Util.AspNetCore.Sessions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace KissU.Modules.GreatWall.Application.Extensions
{
    /// <summary>
    /// AspNetIdentity扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 添加AspNetIdentity服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="setupAction">配置操作</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AspNetIdentity(this IServiceCollection services,
            Action<PermissionOptions> setupAction = null)
        {
            var permissionOptions = new PermissionOptions();
            setupAction?.Invoke(permissionOptions);
            services.AddIdentity<User, Role>(options => options.Load(permissionOptions))
                .AddUserStore<UserRepository>().AddRoleStore<RoleRepository>()
                .AddUserManager<IdentityUserManager>().AddSignInManager<IdentitySignInManager>()
                .AddDefaultTokenProviders();
            services.AddScoped<IdentityErrorDescriber, IdentityErrorChineseDescriber>();
            services.AddSingleton<ISession, Session>();
            return services;
        }

        /// <summary>
        /// 添加权限服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="setupAction">配置操作</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAspNetIdentityCore(this IServiceCollection services,
            Action<PermissionOptions> setupAction = null)
        {
            var permissionOptions = new PermissionOptions();
            setupAction?.Invoke(permissionOptions);
            services.AddAuthentication();
            services.AddIdentityCore<User>(options => options.Load(permissionOptions))
                .AddRoles<Role>()
                .AddUserStore<UserRepository>().AddRoleStore<RoleRepository>()
                .AddUserManager<IdentityUserManager>().AddSignInManager<IdentitySignInManager>()
                .AddDefaultTokenProviders();
            services.AddScoped<IdentityErrorDescriber, IdentityErrorChineseDescriber>();
            services.AddSingleton<ISession, Session>();
            return services;
        }
    }
}