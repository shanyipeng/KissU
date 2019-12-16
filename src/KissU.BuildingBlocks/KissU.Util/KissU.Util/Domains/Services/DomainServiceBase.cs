﻿using KissU.Util.Logs;
using KissU.Util.Logs.Core;
using KissU.Util.Sessions;

namespace KissU.Util.Domains.Services
{
    /// <summary>
    /// 领域服务
    /// </summary>
    public abstract class DomainServiceBase : IDomainService
    {
        /// <summary>
        /// 初始化领域服务
        /// </summary>
        protected DomainServiceBase()
        {
            Log = NullLog.Instance;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public ILog Log { get; set; }

        /// <summary>
        /// 用户会话
        /// </summary>
        public virtual ISession Session => Sessions.Session.Instance;
    }
}
