﻿using System;
using System.Collections.Generic;
using Xunit;
using Util;
using Util.Helpers;
using Util.Dependency;
using KissU.Domain.Systems.Models;
using KissU.Domain.Systems.Repositories;
using KissU.Service.Abstractions.Systems;
using KissU.Service.Dtos.Systems;
using KissU.IntegrationTest.Tests.Dtos.Systems;
using KissU.Service.Dtos.Systems.Extensions;

namespace KissU.IntegrationTest.Tests.Services.Systems 
{
    /// <summary>
    /// Api资源服务测试
    /// </summary>
    [Collection( "GlobalConfig" )]
    public class ApiServiceTest : IDisposable 
	{
        /// <summary>
        /// 容器作用域
        /// </summary>
        private readonly IScope _scope;
        /// <summary>
        /// Api资源服务
        /// </summary>
        private readonly IApiService _apiService;
        /// <summary>
        /// Api资源仓储
        /// </summary>
        private readonly IApiRepository _apiRepository;
        /// <summary>
        /// Api资源Dto
        /// </summary>
        private readonly ApiDto _apiDto;

        /// <summary>
        /// 测试初始化
        /// </summary>
        public ApiServiceTest() 
		{
            _scope = Ioc.BeginScope();
            _apiRepository = _scope.Create<IApiRepository>();
            _apiService = _scope.Create<IApiService>();
            _apiDto = ApiDtoTest.Create();
        }
        
        /// <summary>
        /// 测试清理
        /// </summary>
        public void Dispose() 
		{
            _scope.Dispose();
        }
        
        /// <summary>
        /// 测试
        /// </summary>
        [Fact]
        public void Test() 
		{
		    var count = _apiRepository.Count();
			_apiRepository.Add( _apiDto.ToEntity());
            Assert.Equal( count + 1, _apiRepository.Count() );
        }
    }
}