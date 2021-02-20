using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 电控箱生产查询条件
    /// </summary>
    public class DKXDDCXTJView
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string DD_Bianhao { get; set; }

        /// <summary>
        /// 报价单号
        /// </summary>
        public virtual string BJno { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public virtual string DD_Type { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string KHname { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public virtual string Lxname { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual string DD_ZT { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual string startctime { get; set; }

        public virtual string endctiome { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public virtual string DHtype { get; set; }
    }
}
