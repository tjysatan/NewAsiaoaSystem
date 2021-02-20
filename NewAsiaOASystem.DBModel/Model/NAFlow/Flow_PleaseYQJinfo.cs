using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class Flow_PleaseYQJinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 元器件编号
        /// </summary>
        public virtual string Yqj_bianhao { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Yqj_Name { get; set; }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqj_Namexh {get;set;}

        /// <summary>
        /// 采购数量
        /// </summary>
        public virtual decimal? Cg_shuliang { get; set; }

        /// <summary>
        /// 状态  0 待回复  1 采购中  2 采购完成
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 采购预计交期
        /// </summary>
        public virtual DateTime? Jptime { get; set; }

        /// <summary>
        /// 实际采购完成日期
        /// </summary>
        public virtual DateTime? Wc_time { get; set; }

        /// <summary>
        /// 记录下单时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
 
    }
}
