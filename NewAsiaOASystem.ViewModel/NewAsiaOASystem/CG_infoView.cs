using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class CG_infoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public virtual string GysId { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public virtual string GysDm { get; set; }

        /// <summary>
        /// 订单产生时间
        /// </summary>
        public virtual DateTime Cg_xdtime { get; set; }

        /// <summary>
        /// 生管需求交期
        /// </summary>
        public virtual DateTime? Cg_sgjqtime { get; set; }

        /// <summary>
        /// 供应商交期
        /// </summary>
        public virtual DateTime? Cg_jqqrtime { get; set; }

        /// <summary>
        /// 是否到货 0 未到货 1 到货
        /// </summary>
        public virtual int Cg_Isdh { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public virtual DateTime? Cg_dhtime { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public virtual string Cg_xdname { get; set; }
    }
}
