using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退产品统计 产品种类 返退数量汇总临时表
    /// </summary>
    public class NQ_TjFtwxCPTypeSuminfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual string Cp_Id { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string CP_bianhao { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string CP_name { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string CP_XH { get; set; }

        /// <summary>
        /// 返退数量
        /// </summary>
        public virtual int? FT_SUM { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }

        /// <summary>
        /// 返退年份
        /// </summary>
        public virtual string FT_Year { get; set; }

        /// <summary>
        /// 返退月份
        /// </summary>
        public virtual string FT_Mone { get; set; }

        /// <summary>
        /// 统计类型 0 产品统计  1 不良原因统计 2元器件统计
        /// </summary>
        public virtual int? Tj_Type { get; set; }
    }
}
