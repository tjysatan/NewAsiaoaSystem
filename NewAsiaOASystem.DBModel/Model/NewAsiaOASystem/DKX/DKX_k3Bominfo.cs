using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class DKX_k3Bominfo
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
        /// BOM表的编号
        /// </summary>
        public virtual string FnumberBom { get; set; }

        /// <summary>
        /// 元器件的物料代码
        /// </summary>
        public virtual string FNumber { get; set; }

        /// <summary>
        /// 元器件的物料名称
        /// </summary>
        public virtual string FItemName { get; set; }

        /// <summary>
        /// 物料型号
        /// </summary>
        public virtual string FModel { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual string FBaseUnitID { get; set; }

        /// <summary>
        /// 单位用量
        /// </summary>
        public virtual decimal? FAuxQty { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string Beizhu1 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string Beizhu2 { get; set; }

        /// <summary>
        /// 硬件成本 只有从报价系统获取的时候有这个字段
        /// </summary>
        public virtual decimal? yjcb { get; set; }
    }
}
