using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 个人盈利汇总实体
    /// </summary>
    public class PP_ProfitSummaryinfo
    {
        /// <summary>
        /// 编号Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 个人Id
        /// </summary>
        public virtual string StafitId { get; set; }

        /// <summary>
        /// 团队Id
        /// </summary>
        public virtual string Sat_teamId { get; set; }

        /// <summary>
        /// 汇总类型 0 按月汇总 1按年汇总
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 收入
        /// </summary>
        public virtual decimal Shouru { get; set; }

        /// <summary>
        /// 支出
        /// </summary>
        public virtual decimal zhichu { get; set; }

        /// <summary>
        /// 盈利
        /// </summary>
        public virtual decimal yingli { get; set; }

        /// <summary>
        /// 累计收入
        /// </summary>
        public virtual decimal ljshouru { get; set; }

        /// <summary>
        /// 累计支出
        /// </summary>
        public virtual decimal ljzhichu { get; set; }

        /// <summary>
        /// 累计盈利
        /// </summary>
        public virtual decimal ljyingli { get; set; }

        /// <summary>
        /// 盈利的时间 月或者年（按月汇总 例：201706  按年汇总 例：2017）
        /// </summary>
        public virtual string MorY { get; set; }

        /// <summary>
        /// 汇总时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 汇总人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 最终更新时间
        /// </summary>
        public virtual DateTime?  Up_time { get; set; }

        /// <summary>
        /// 最终更新人
        /// </summary>
        public virtual string Up_name { get; set; }
    }
}
