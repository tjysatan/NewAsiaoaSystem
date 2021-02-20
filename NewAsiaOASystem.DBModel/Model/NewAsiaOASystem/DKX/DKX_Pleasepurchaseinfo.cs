using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class DKX_Pleasepurchaseinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 元器件
        /// </summary>
        public virtual string Yqj_bianhao { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Yqj_Name { get; set; }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqj_Namexh { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public virtual decimal? Cg_shuliang { get; set; }

        /// <summary>
        /// 预计交期
        /// </summary>
        public virtual DateTime? Jqtime { get; set; }

        /// <summary>
        /// 状态 0 未采购 1 采购中 2 完成采购
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_Name { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public virtual string Cg_name { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? Wc_datetime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string Cz_name { get;set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime? Cz_time { get; set; }

        /// <summary>
        /// 实际采购数量
        /// </summary>
        public virtual decimal? Sjcgsl { get; set; }
    }
}
