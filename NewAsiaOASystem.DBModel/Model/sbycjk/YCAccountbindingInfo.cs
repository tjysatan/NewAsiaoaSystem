using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 远程监控帐号绑定的实体
    /// </summary>
    public class YCAccountbindingInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        public virtual string openId { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// 绑定时候的密码
        /// </summary>
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否绑定（预留/设计解绑直接删除）
        /// </summary>
        public virtual int? Isbd { get; set; }

        /// <summary>
        /// 解绑时间（预留/解绑直接删除）
        /// </summary>
        public virtual DateTime? Jbtime { get; set; }
    }
}
