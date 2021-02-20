using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class Wx_configinfoView
    {
        /// <summary>
        /// 唯一识别号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 微信名称
        /// </summary>
        public virtual string weixinname { get; set; }

        /// <summary>
        /// 微信 APPID
        /// </summary>
        public virtual string APPID { get; set; }

        /// <summary>
        /// 微信商户号
        /// </summary>
        public virtual string MCHID { get; set; }

        /// <summary>
        /// key
        /// </summary>
        public virtual string KEYvalue { get; set; }

        /// <summary>
        /// 微信 APPSECRET
        /// </summary>
        public virtual string APPSECRET { get; set; }

        /// <summary>
        /// 微信支付回调地址
        /// </summary>
        public virtual string NOTIFY_URL { get; set; }

        /// <summary>
        /// 公众号 类别
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_datetime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Up_name { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Up_datetime { get; set; }
    }
}
