using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 收入支出明细实体
    /// </summary>
    public  class PP_ShouruandzhichuDetailsinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 个人（员工）Id
        /// </summary>
        public virtual string StafitId { get; set; }

        /// <summary>
        /// 收入或支持项的Id
        /// </summary>
        public virtual string ProfutId { get; set; }

        /// <summary>
        /// 明细类别 0 收入 1支出 2 管理费用收入（生产）
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal Sum { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal Total { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public virtual string jl_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual string C_Name { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? Up_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual string Up_Name { get; set; }

        /// <summary>
        /// 来源类型 0 个人（收入支出） 1 团体（收入支出）
        /// </summary>
        public virtual int Lytype { get; set; }

        /// <summary>
        /// 团队收入支出明细 Id
        /// </summary>
        public virtual string TTmxId { get; set; }
    }
}
