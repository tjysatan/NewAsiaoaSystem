using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// name:销售上线各个月的目标比例
    /// </summary>
    public  class WL_SalesONlineratioinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public virtual string R_month { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public virtual string R_Year { get; set; }

        /// <summary>
        /// 月份销售比例
        /// </summary>
        public virtual int XS_ratio { get; set; }

        /// <summary>
        /// 月份上线比例
        /// </summary>
        public virtual int SX_ratio { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
