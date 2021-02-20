using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public  class Flow_NonSProductinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Pname { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string Pmodel { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public virtual string Pbianma { get; set; }

        /// <summary>
        /// 当前库存
        /// </summary>
        public virtual decimal? A_Sum { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public virtual int? Category { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
