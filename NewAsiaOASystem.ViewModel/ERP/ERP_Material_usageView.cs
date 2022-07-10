using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 库存物料使用情况 表
    /// </summary>
    public class ERP_Material_usageView
    {

        public virtual string Id { get; set; }

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string fnumber { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string fname { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string fmodel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IsClose { get; set; }

        /// <summary>
        /// 物料属性
        /// </summary>
        public virtual string Z_WLSX { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? updatetime { get; set; }

        /// <summary>
        /// 及时库存
        /// </summary>
        public virtual decimal? OnHand { get; set; }

        /// <summary>
        /// 采购在定量
        /// </summary>
        public virtual decimal? OnOrder { get; set; }

        /// <summary>
        /// 生产领料 1
        /// </summary>
        public virtual decimal? SC_Qty_ONE { get; set; }

        /// <summary>
        /// 其他出入库数量1
        /// </summary>
        public virtual decimal? QT_Qty_ONE { get; set; }

        /// <summary>
        /// 总出库数量 1
        /// </summary>
        public virtual decimal? HJ_ONE { get; set; }

        /// <summary>
        /// 生产领料 2
        /// </summary>
        public virtual decimal? SC_Qty_TWE { get; set; }

        /// <summary>
        /// 其他出入库数量2
        /// </summary>
        public virtual decimal? QT_Qty_TWE { get; set; }

        /// <summary>
        /// 总出库数量 2
        /// </summary>
        public virtual decimal? HJ_TWE { get; set; }

        /// <summary>
        /// 生产领料 3
        /// </summary>
        public virtual decimal? SC_Qty_THREE { get; set; }

        /// <summary>
        /// 其他出入库数量3
        /// </summary>
        public virtual decimal? QT_Qty_THREE { get; set; }

        /// <summary>
        /// 总出库数量 3
        /// </summary>
        public virtual decimal? HJ_THREE { get; set; }
    }
}
