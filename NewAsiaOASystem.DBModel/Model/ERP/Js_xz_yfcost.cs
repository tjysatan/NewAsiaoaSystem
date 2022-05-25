using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class Js_xz_yfcost
    {
        /// <summary>
        /// 编号Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 小组名称
        /// </summary>
        public virtual string Js_xz_name { get; set; }

        /// <summary>
        /// 费用时间
        /// </summary>
        public virtual string Fy_data { get; set; }

    

        /// <summary>
        /// 费用1 研发人员工资福利
        /// </summary>
        public virtual decimal? Fy_jine1 { get; set; }

        /// <summary>
        /// 费用2 研发人员费用
        /// </summary>
        public virtual decimal? Fy_jine2 { get; set; }

        /// <summary>
        /// 费用3 售后费用
        /// </summary>
        public virtual decimal? Fy_jine3 { get; set; }

        /// <summary>
        /// 创建的时间
        /// </summary>

        public virtual DateTime? c_time { get; set; }
    }
}
