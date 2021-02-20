using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class NewConfig
    {
        /// <summary>
        ///  APPID：绑定支付的APPID（必须配置）
        /// </summary>
        public string APPID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MCHID { get; set; }

        /// <summary>
        /// KEY：商户支付密钥，参考开户邮件设置（必须配置）
        /// </summary>
        public string KEY { get; set; }

        /// <summary>
        /// APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        /// </summary>
        public string APPSECRET { get; set; }

        /// <summary>
        /// 支付结果通知回调url，用于商户接收支付结果
        /// </summary>
        public string NOTIFY_URL { get; set; }
    }
}