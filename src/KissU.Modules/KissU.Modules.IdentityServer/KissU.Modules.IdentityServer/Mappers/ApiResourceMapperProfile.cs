﻿// <copyright file="ApiResourceMapperProfile.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using AutoMapper;
using KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate;
using Ids4 = IdentityServer4.Models;

namespace KissU.Modules.IdentityServer.Mappers
{
    /// <summary>
    /// Api资源AutoMapper映射配置
    /// </summary>
    public class ApiResourceMapperProfile : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ApiResourceMapperProfile()
        {
            CreateMap<ApiResourceSecret, Ids4.Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null));
        }
    }
}