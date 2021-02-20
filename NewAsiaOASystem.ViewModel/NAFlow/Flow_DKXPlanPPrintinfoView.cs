using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 电控箱的生产任务单打印数据
    /// </summary>
    public class Flow_DKXPlanPPrintinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 生产单Id
        /// </summary>
        public virtual string Plan_Id { get; set; }

        /// <summary>
        /// 生产批号
        /// </summary>
        public virtual string Scph { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public virtual decimal? Scsl { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Cusname { get; set; }

        /// <summary>
        /// 要货时间
        /// </summary>
        public virtual DateTime? Yhdate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string CPname { get; set; }

        /// <summary>
        /// 产品功率
        /// </summary>
        public virtual decimal? Gl { get; set; }

        /// <summary>
        /// 功率单位
        /// </summary>
        public virtual string Dw { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public virtual string Xdname { get; set; }

        /// <summary>
        /// 下单打印的时间
        /// </summary>
        public virtual DateTime? C_datetime { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        public virtual string Jdname { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }
    }
}
