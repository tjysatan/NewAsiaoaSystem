using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 远程告警通知实体
    /// </summary>
    public class YCnoticeinfo
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 微信的OPenId
        /// </summary>
        public virtual string openId { get; set; }

        /// <summary>
        /// 远程帐号
        /// </summary>
        public virtual string username { get; set; }

        /// <summary>
        /// SID
        /// </summary>
        public virtual string SID { get; set; }

        /// <summary>
        /// 监控点名称
        /// </summary>
        public virtual string JKDname { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public virtual string Tzcon { get; set; }

        /// <summary>
        /// xml
        /// </summary>
        public virtual string Xml { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否发送 0 发送 1 未发送
        /// </summary>
        public virtual int? Isfs { get; set; }

        /// <summary>
        /// 通知类型 0 告警  1 通知
        /// </summary>
        public virtual int? type { get; set; }
    }
}
