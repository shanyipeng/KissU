﻿using KissU.Core.Commons;
using KissU.Core.Logs;
using KissU.Core.Properties;
using KissU.Core.Sessions;
using KissU.Util.AspNetCore.Mvc.Commons;
using KissU.Util.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KissU.Util.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// WebApi控制器
    /// </summary>
    [Route("api/[controller]")]
    [ExceptionHandler]
    [ErrorLog]
    [TraceLog]
    public abstract partial class WebApiControllerBase : Controller
    {
        /// <summary>
        /// 日志
        /// </summary>
        private ILog _log;

        /// <summary>
        /// 日志
        /// </summary>
        public ILog Log => _log ??= GetLog();

        /// <summary>
        /// 会话
        /// </summary>
        public virtual ISession Session => Sessions.Session.Instance;

        /// <summary>
        /// 获取日志操作
        /// </summary>
        protected virtual ILog GetLog()
        {
            try
            {
                return Core.Logs.Log.GetLog(this);
            }
            catch
            {
                return Core.Logs.Log.Null;
            }
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        protected virtual IActionResult Success(dynamic data = null, string message = null)
        {
            if (message == null)
                message = R.Success;
            return new Result(StateCode.Ok, message, data);
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        protected virtual IActionResult Fail(string message)
        {
            return new Result(StateCode.Fail, message);
        }
    }
}