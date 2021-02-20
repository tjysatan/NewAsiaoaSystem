using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
             /// <summary>
    /// 来料通知单
    /// </summary>
    public class IQC_llNoticeordinfoView
    {

 
        /// <summary>
        /// 唯一值
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// k3 来料通知单编号
        /// </summary>
        public virtual string ddno {get;set;}

        /// <summary>
        /// K3 自增值Id
        /// </summary>
        public virtual int? ddId { get; set; }

        /// <summary>
        /// K3 的时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public virtual DateTime? Tbtime { get; set; }

        /// <summary>
        /// 同步人
        /// </summary>
        public virtual string tbname { get; set; }

        /// <summary>
        /// 是否处理 0 未处理 1 处理中 1 完成
        /// </summary>
        public virtual int?  Iscz { get; set; }

        /// <summary>
        /// 录单时间
        /// </summary>
        public virtual DateTime? Rdtime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? wctime { get; set; }

        /// <summary>
        /// 录单人
        /// </summary>
        public virtual string Rdname { get; set; }

        /// <summary>
        /// 是否禁用 0 启用 1 禁用
        /// </summary>
        public virtual string Isdis { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }
    }
}
