using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 各区域年销售目标
    /// </summary>
    public class WL_SalestargetView
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
        /// 销售目标
        /// </summary>
        public virtual int S_salesvolume { get; set; }

        /// <summary>
        /// 省份 Id
        /// </summary>
        public virtual string S_provinceId { get; set; }

        /// <summary>
        /// 地级市Id
        /// </summary>
        public virtual string S_cityId { get; set; }

        /// <summary>
        /// 销售经理Id(该地区负责人)
        /// </summary>
        public virtual string S_JLId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
