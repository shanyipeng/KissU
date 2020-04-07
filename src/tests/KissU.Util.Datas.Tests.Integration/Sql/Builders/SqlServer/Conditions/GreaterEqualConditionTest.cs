﻿using Xunit;

namespace KissU.Util.Datas.Tests.Integration.Sql.Builders.SqlServer.Conditions
{
    /// <summary>
    /// Sql大于等于查询条件测试
    /// </summary>
    public class GreaterEqualConditionTest
    {
        /// <summary>
        /// 获取条件
        /// </summary>
        [Fact]
        public void Test()
        {
            var condition = new GreaterEqualCondition("Age", "@Age");
            Assert.Equal("Age>=@Age", condition.GetCondition());
        }
    }
}