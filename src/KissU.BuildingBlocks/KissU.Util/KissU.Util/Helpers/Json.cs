﻿using Newtonsoft.Json;

namespace KissU.Util.Helpers
{
    /// <summary>
    /// Json操作
    /// </summary>
    public static partial class Json
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>T.</returns>
        public static T ToObject<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        /// <returns>System.String.</returns>
        public static string ToJson(object target, bool isConvertToSingleQuotes = false)
        {
            if (target == null)
                return string.Empty;
            var result = JsonConvert.SerializeObject(target);
            if (isConvertToSingleQuotes)
                result = result.Replace("\"", "'");
            return result;
        }
    }
}