﻿using System.Threading.Tasks;
using KissU.Util.Biz.Payments.Core;
using KissU.Util.Biz.Payments.Wechatpay.Parameters.Requests;

namespace KissU.Util.Biz.Payments.Wechatpay.Abstractions {
    /// <summary>
    /// 微信App支付服务
    /// </summary>
    public interface IWechatpayAppPayService {
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="request">支付参数</param>
        Task<PayResult> PayAsync( WechatpayAppPayRequest request );
    }
}
