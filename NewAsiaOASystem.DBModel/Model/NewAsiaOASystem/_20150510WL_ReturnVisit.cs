using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 工程商和客户的回访
    /// </summary>
    public class _20150510WL_ReturnVisit
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 电控箱Id
        /// </summary>
        public virtual string DKXId { get; set; }

        /// <summary>
        /// 回访记录
        /// </summary>
        public virtual string RVContent { get; set; }

        /// <summary>
        /// 回访时间
        /// </summary>
        public virtual DateTime? RVDateTime { get; set; }

        /// <summary>
        /// 回访人
        /// </summary>
        public virtual string RV_Usid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Rmarks { get; set; }

        /// <summary>
        /// 回访对象类型 0 工程商 1 用户
        /// </summary>
        public virtual int RVtype { get; set; }
    }
}
