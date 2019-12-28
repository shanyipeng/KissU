﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using KissU.Core.CPlatform.Transport.Implementation;
using KissU.Core.ProxyGenerator;
using KissU.Modules.Admin.Service.Contracts.Abstractions;
using KissU.Modules.Admin.Service.Contracts.Dtos.NgAlain;
using KissU.Modules.Admin.Service.Extensions;
using KissU.Modules.GreatWall.Service.Contracts;

namespace KissU.Modules.Admin.Service.Implements
{
    /// <summary>
    /// 启动服务
    /// </summary>
    public class StartupService : ProxyServiceBase, IStartupService
    {
        /// <summary>
        /// 获取应用程序数据
        /// </summary>
        [HttpGet(true)]
        public async Task<AppData> GetAppDataAsync()
        {
            object payload = RpcContext.GetContext().GetAttachment("payload");
            AppData data = new AppData
            {
                App = { Name = "KissU", Description = ".Net Core权限系统" }
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
            List<GreatWall.Application.Dtos.Responses.MenuResponse> result = await GetService<IMenuService>().GetMenusAsync();
            return result.ToNgAlainMenus();
        }
    }
}