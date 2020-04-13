﻿using System;
using KissU.Core.Helpers;
using KissU.Util.Biz.Payments.Alipay.Configs;
using KissU.Util.Biz.Payments.Alipay.Parameters;
using Xunit;
using Xunit.Abstractions;

namespace KissU.Util.Biz.Tests.Integration.Payments.Alipay.Parameters
{
    /// <summary>
    /// 支付宝参数生成器测试
    /// </summary>
    public class AlipayParameterBuilderTest : IDisposable
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        /// <param name="output">The output.</param>
        public AlipayParameterBuilderTest(ITestOutputHelper output)
        {
            _output = output;
            TimeHelper.SetTime(TestConst.Time);
            _builder = new AlipayParameterBuilder(new AlipayConfig());
        }

        /// <summary>
        /// 测试清理
        /// </summary>
        public void Dispose()
        {
            TimeHelper.Reset();
        }

        /// <summary>
        /// 输出
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// 支付宝参数生成器
        /// </summary>
        private readonly AlipayParameterBuilder _builder;

        /// <summary>
        /// 添加应用标识
        /// </summary>
        [Fact]
        public void TestAppId()
        {
            _builder.AppId("a");
            Assert.Equal("app_id=a", _builder.ToString());
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        [Fact]
        public void TestContent()
        {
            _builder.Content.OutTradeNo("a");
            Assert.Equal("biz_content={\"out_trade_no\":\"a\"}", _builder.ToString());
        }

        /// <summary>
        /// 添加请求方法
        /// </summary>
        [Fact]
        public void TestMethod()
        {
            _builder.Method("a");
            Assert.Equal("method=a", _builder.ToString());
        }
    }
}