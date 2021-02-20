using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// name:经销商（物联网）销售目标
    /// </summary>
    public class WL_DealerSalestargetView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public virtual string S_Year { get; set; }

        /// <summary>
        /// 目标销售
        /// </summary>
        public virtual int S_salesvolume { get; set; }

        /// <summary>
        /// 经销商（物联网）Id
        /// </summary>
        public virtual string Cus_Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual string C_time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }
    }
}
