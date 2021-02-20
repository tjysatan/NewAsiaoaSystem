using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class WeChat_AnnualmeetingSignView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public virtual string openid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string nickname { get; set; }

        /// <summary>
        /// 1男 2女 0 未知
        /// </summary>
        public virtual int? sex { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public virtual string province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public virtual string city { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public virtual string country { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public virtual string headimgurl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
