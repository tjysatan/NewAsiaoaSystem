using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 留言弹幕实体
    /// </summary>
    public  class WeChat_AnnualmeetingBarrageView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public virtual string OpenId { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public virtual string nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public virtual string headimgurl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否上过墙
        /// </summary>
        public virtual int? Issq { get; set; }

        /// <summary>
        /// 上墙内容
        /// </summary>
        public virtual string Conter { get; set; }
    }
}
