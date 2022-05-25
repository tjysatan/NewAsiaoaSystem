using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 销售出货明细
    /// </summary>
    public class ERP_SASalAinfoView
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 日期 202110
        /// </summary>
        public virtual string AbsID { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public virtual string CrdID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CrdName { get; set; }

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string ItmName { get; set; }

        /// <summary>
        /// 物料型号
        /// </summary>
        public virtual string ItmSpec { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? BaseQty { get; set; }

        /// <summary>
        /// 未税单价
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public virtual decimal? TaxAfPrice { get; set; }

        /// <summary>
        /// 未税金额
        /// </summary>
        public virtual decimal? LineSum { get; set; }

        /// <summary>
        /// 含税金额 数量*含税单价
        /// </summary>
        public virtual decimal? XSCostPrice { get; set; }

        /// <summary>
        /// 销售成本单价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public virtual string EmpName { get; set; }

        /// <summary>
        /// 责任工程师
        /// </summary>
        public virtual string Z_EmpName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public virtual string DocNum { get; set; }
    }
}
