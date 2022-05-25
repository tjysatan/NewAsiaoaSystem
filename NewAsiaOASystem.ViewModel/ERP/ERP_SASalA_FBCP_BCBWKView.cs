using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// ERP 销售出货明细 中非标产品BOM 中包含的温控数据实体
    /// </summary>
    public class ERP_SASalA_FBCP_BCBWKView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 销售出货的明细的Id
        /// </summary>
        public virtual string F_Id { get; set; }

        /// <summary>
        /// 账期
        /// </summary>
        public virtual string AbsID { get; set; }

        /// <summary>
        /// 父级产品物料号
        /// </summary>
        public virtual string F_ItmID { get; set; }

        /// <summary>
        /// 父级产品的物料产品名称
        /// </summary>
        public virtual string F_ItmName { get; set; }

        /// <summary>
        /// 父级产品的物料产品的规格型号
        /// </summary>
        public virtual string F_ItmSpec { get; set; }

        /// <summary>
        /// 父级产品的销售数量
        /// </summary>
        public virtual decimal? F_BaseQty { get; set; }

        /// <summary>
        /// 父级产品的销售价格
        /// </summary>
        public virtual decimal? F_CostPrice { get; set; }

        /// <summary>
        /// 父级产品的责任工程师
        /// </summary>
        public virtual string F_Z_EmpName { get; set; }

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string ItmName { get; set; }

        /// <summary>
        /// 物料的规格型号
        /// </summary>
        public virtual string ItmSpec { get; set; }

        /// <summary>
        /// 用量 BOM* F_BaseQty
        /// </summary>
        public virtual decimal? BaseQty { get; set; }

        /// <summary>
        /// 获取系统的价格 单价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }

        /// <summary>
        /// 责任工程师
        /// </summary>
        public virtual string Z_EmpName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }


    }
}
