using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退货产品的费用均摊记录的明细表
    /// </summary>
    public class NA_Sharealike
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public virtual string Resdepartment { get; set; }

        /// <summary>
        /// 退货明细记录Id
        /// </summary>
        public virtual string Mx_id { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual string pdId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string pdname { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string pdmodel { get; set; }

        /// <summary>
        /// 均摊的费用
        /// </summary>
        public virtual decimal? cost { get; set; }

        /// <summary>
        /// 返退单记录的Id
        /// </summary>
        public virtual string RId { get;set; }

        /// <summary>
        /// 返退单记录的编号
        /// </summary>
        public virtual string Rbianhao { get; set; }

        /// <summary>
        /// 退货的客户Id
        /// </summary>
        public virtual string ThcusId { get; set; }

        /// <summary>
        /// 退货的客户名称
        /// </summary>
        public virtual string Thcusname { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public virtual DateTime? Wxtime { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public virtual DateTime? c_time { get; set; }
    }
}
