using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class PP_ProfitpointinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public virtual string Rwname { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        public virtual string Rwms { get; set; }

        /// <summary>
        /// 业务备注
        /// </summary>
        public virtual string Rwbz { get; set; }


        /// <summary>
        /// 单位盈利（元）
        /// </summary>
        public virtual decimal? Rwfz { get; set; }

        /// <summary>
        /// 单位（天/件/人）
        /// </summary>
        public virtual string Rwdw { get; set; }

        /// <summary>
        /// 业务关系团队Id
        /// </summary>
        public virtual string Rw_teamId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Up_time { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Up_name { get; set; }

        /// <summary>
        /// 是否启用 0 启用 1 禁用
        /// </summary>
        public virtual int state { get; set; }

        /// <summary>
        /// 排序 数字越大越靠前
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 项类型 0 收入项  1 支出项
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// 0 个人收入/支出项  1 团队收入/支出项（固定分配）  2团队收入 （不固定分配）
        /// </summary>
        public virtual int ProType { get; set; }
    }
}
