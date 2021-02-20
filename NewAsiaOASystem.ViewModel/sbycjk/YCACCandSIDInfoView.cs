using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 帐号绑定SID实体
    /// </summary>
    public class YCACCandSIDInfoView
    {
        /// <summary>
        /// 唯一值编号
        /// </summary>
        public virtual string Id { get; set; }


        /// <summary>
        /// 帐号与微信绑定之后的唯一值
        /// </summary>
        public virtual string nameId { get; set; }

        /// <summary>
        /// SID
        /// </summary>
        public virtual string SID { get; set; }

        /// <summary>
        /// 监控点名称
        /// </summary>
        public virtual string jkdname { get; set; }
        /// <summary>
        /// sid ID
        /// </summary>
        public virtual int? SIDId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual DateTime? SIDTime { get; set; }

        /// <summary>
        /// 是否发送
        /// </summary>
        public virtual int? Isfs { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime? Cz_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
