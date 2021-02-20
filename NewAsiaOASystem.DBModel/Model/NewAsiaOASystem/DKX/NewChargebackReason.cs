using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 退单原因
    /// </summary>
    public class NewChargebackReason
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 退单原因名称
        /// </summary>
        public virtual string Reason_name { get; set; }

        /// <summary>
        /// 是否需要手动备注 0 不需要 1 需要
        /// </summary>
        public virtual int? IsRemarks { get; set; }

        /// <summary>
        /// 退单的阶段 0 生产退单
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_userId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Up_time { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Up_userId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual int? IsDis { get; set; }


    }
}
