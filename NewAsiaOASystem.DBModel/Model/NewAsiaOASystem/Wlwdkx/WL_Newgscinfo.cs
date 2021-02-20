using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 新的工程上信息对应的SID数据实体
    /// </summary>
    public class WL_Newgscinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 工程商名称
        /// </summary>
        public virtual string user_name { get; set; }

        /// <summary>
        /// 工程商电话
        /// </summary>
        public virtual string mobile { get; set; }

        /// <summary>
        /// 远程ID
        /// </summary>
        public virtual int? Ids { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string company { get; set; }

        /// <summary>
        /// sid
        /// </summary>
        public virtual string sid { get; set; }

        /// <summary>
        /// 数据同步时间
        /// </summary>
        public virtual DateTime? huoqutime { get; set; }
    }
}
