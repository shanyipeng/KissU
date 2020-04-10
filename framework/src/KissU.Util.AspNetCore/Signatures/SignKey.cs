﻿using KissU.Core.Signatures;

namespace KissU.Util.AspNetCore.Signatures
{
    /// <summary>
    /// 签名密钥
    /// </summary>
    public class SignKey : ISignKey
    {
        /// <summary>
        /// 私钥
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// 公钥
        /// </summary>
        private readonly string _publicKey;

        /// <summary>
        /// 初始化签名密钥
        /// </summary>
        /// <param name="key">私钥</param>
        /// <param name="publicKey">公钥</param>
        public SignKey(string key, string publicKey = "")
        {
            _key = key;
            _publicKey = publicKey;
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetKey()
        {
            return _key;
        }

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetPublicKey()
        {
            return _publicKey;
        }
    }
}