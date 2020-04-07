﻿using System;
using System.Linq.Expressions;
using KissU.Util.Datas.Tests.Integration.Commons.Domains.Models;

namespace KissU.Util.Datas.Tests.Integration.Commons.Datas.Criterias
{
    /// <summary>
    /// 客户查询条件
    /// </summary>
    public class CustomerCriteria : ICriteria<Customer>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// 昵称
        /// </summary>
        private readonly string _nickname;

        /// <summary>
        /// 初始化客户查询条件
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="nickname">昵称</param>
        public CustomerCriteria(string name, string nickname)
        {
            _name = name;
            _nickname = nickname;
        }

        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <returns>Expression&lt;Func&lt;Customer, System.Boolean&gt;&gt;.</returns>
        public Expression<Func<Customer, bool>> GetPredicate()
        {
            return customer => customer.Name == _name && customer.Nickname == _nickname;
        }
    }
}