﻿using System.Collections.Generic;
using KissU.Core.Maps;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.NgAlain;
using KissU.Modules.GreatWall.Application.Contracts.Dtos.Responses;
using KissU.Modules.GreatWall.Domain.Models;

namespace KissU.Modules.GreatWall.Application.Extensions
{
    /// <summary>
    /// 菜单参数扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转成NgAlain菜单
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>List&lt;MenuInfo&gt;.</returns>
        public static List<MenuInfo> ToNgAlainMenus(this IEnumerable<MenuResponse> data)
        {
            return new MenuResult(data).GetResult();
        }

        /// <summary>
        /// 转成菜单参数
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>MenuResponse.</returns>
        public static MenuResponse ToMenuResponse(this Module entity)
        {
            if (entity == null)
                return null;
            var result = entity.MapTo<MenuResponse>();
            result.External = entity.IsExternalUrl();
            return result;
        }
    }
}