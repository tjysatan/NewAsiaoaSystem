using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 团队收支明细实体
    /// </summary>
    public class PP_TTShouruandzhichuDetailsinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 团队Id
        /// </summary>
        public virtual string TeamId { get; set; }

        /// <summary>
        /// 团队收支Id
        /// </summary>
        public virtual string ProfutId { get; set; }

        /// <summary>
        /// 明细类型 0 收入 1 支出
        /// </summary>
        public virtual int? type { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public virtual decimal? Sum { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal? Total { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual string jl_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_Name { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Up_time { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Up_Name { get; set; }

        /// <summary>
        /// 团队收入支出的类型 0 固定分配的 1不固定分配
        /// </summary>
        public virtual int TT_type { get; set; }

        /// <summary>
        /// 是否完成分配  固定分配的默认完成分配 0   1 未完成分配
        /// </summary>
        public virtual int Iswcfp { get; set; }

        /// <summary>
        /// 完成分配时间
        /// </summary>
        public virtual DateTime? Wcfpdatetime { get; set; }
    }
}
