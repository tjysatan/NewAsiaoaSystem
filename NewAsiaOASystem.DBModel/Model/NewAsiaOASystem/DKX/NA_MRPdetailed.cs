using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public  class NA_MRPdetailed
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 主表Id
        /// </summary>

        public virtual string MainId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>

        public virtual string dd_ddno { get; set; }
        /// <summary>
        /// 物料代码
        /// </summary>

        public virtual string wlno { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string wlname { get; set; }
        /// <summary>
        /// 物料型号
        /// </summary>
        public virtual string wlmodel { get; set; }
        /// <summary>
        /// 物料需求数量
        /// </summary>
        public virtual decimal? qusum { get; set; }
        /// <summary>
        /// 当前库存
        /// </summary>
        public virtual decimal? kcsum { get; set; }
    }
}
