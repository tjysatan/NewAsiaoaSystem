using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 来访微信的信息实体
    /// </summary>
    public class Wx_openIdinfo
    {
        /// <summary>
        /// 唯一识别号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public virtual string openid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string nickname { get; set; }

        /// <summary>
        /// 性别
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
        /// 头像
        /// </summary>
        public virtual string headimgurl { get; set; }

        /// <summary>
        /// 代理帐号
        /// </summary>
        public virtual string Dlname { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
