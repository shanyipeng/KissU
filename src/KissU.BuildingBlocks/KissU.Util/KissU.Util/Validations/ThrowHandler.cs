﻿using System.Linq;
using KissU.Util.Exceptions;

namespace KissU.Util.Validations
{
    /// <summary>
    /// 验证失败，抛出异常 - 默认验证处理器
    /// </summary>
    public class ThrowHandler : IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        /// <exception cref="Warning"></exception>
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            throw new Warning(results.First().ErrorMessage);
        }
    }
}
