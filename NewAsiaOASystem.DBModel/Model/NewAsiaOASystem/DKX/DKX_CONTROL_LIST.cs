using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 报价料单
    /// </summary>
    public class DKX_CONTROL_LIST
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual string dd_Id { get; set; }

        /// <summary>
        /// RECORD ID，自動編號
        /// </summary>
        public virtual string LIST_ID { get; set; }

        /// <summary>
        /// 需求NO
        /// </summary>
        public virtual string CONTROL_INFO_NO { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string C_DESC { get; set; }

        /// <summary>
        /// 箱柜价格
        /// </summary>
        public virtual float? SUBTOTAL { get; set; }

        /// <summary>
        /// 包装及运费
        /// </summary>
        public virtual float? SUB_PACK { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public virtual float? SUB { get; set; }

        /// <summary>
        /// 设计费
        /// </summary>
        public virtual float? TAXATION { get; set; }

        /// <summary>
        /// 电器含税费
        /// </summary>
        public virtual float? DQ { get; set; }

        /// <summary>
        /// 控制器部份
        /// </summary>
        public virtual float? KZ { get; set; }

        /// <summary>
        /// 辅料
        /// </summary>
        public virtual float? FL { get; set; }

        /// <summary>
        /// 包装费
        /// </summary>
        public virtual float? BZ { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public virtual float? YF { get; set; }

        /// <summary>
        /// 组装调试费
        /// </summary>
        public virtual float? ZZF { get; set; }

        /// <summary>
        /// 现场管理费
        /// </summary>
        public virtual float? GLF { get; set; }

        /// <summary>
        /// 利润
        /// </summary>
        public virtual float? LR { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public virtual float? SF { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual float? PRICE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual string CREATE_BY { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CREATE_TIME { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public virtual string UPDATE_BY { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UPDATE_TIME { get; set; }

        /// <summary>
        /// 优惠价
        /// </summary>
        public virtual float? DIS_PRICE { get; set; }
    }
}
