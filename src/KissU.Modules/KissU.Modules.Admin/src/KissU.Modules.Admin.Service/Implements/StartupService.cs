﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KissU.Modules.Admin.Service.Contracts.Abstractions;
using KissU.Modules.Admin.Service.Contracts.Dtos.NgAlain;
using KissU.Modules.Admin.Service.Extensions;
using KissU.Modules.GreatWall.Service.Contracts.Abstractions;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.CPlatform.Transport.Implementation;
using Surging.Core.ProxyGenerator;
using Util.Helpers;

namespace KissU.Modules.Admin.Service.Implements
{
    /// <summary>
    /// 启动服务
    /// </summary>
    public class StartupService : ProxyServiceBase, IStartupService
    {
        /// <summary>
        /// 初始化主控制器
        /// </summary>
        public StartupService()
        {
        }

        /// <summary>
        /// 获取应用程序数据
        /// </summary>
        [HttpGet(true)]
        public async Task<AppData> GetAppDataAsync()
        {
            var payload = RpcContext.GetContext().GetAttachment("payload");
            var data = new AppData
            {
                App = { Name = "KissU", Description = ".Net Core权限系统" },
                //User = { Name = Web.Identity.Name, Avatar = "/assets/tmp/img/avatar.jpg", Email = "gongap@qq.com" },
                //Menu = await GetMenus()
            };
            return data;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        private async Task<List<MenuInfo>> GetMenus()
        {
            var result = await this.GetService<IMenuService>().GetMenusAsync();
            return result.ToNgAlainMenus();
        }
    }
}