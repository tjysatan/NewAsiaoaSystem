using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 员工实体
    /// </summary>
    public  class PP_Staffinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public virtual string Sat_Name { get; set; }

        /// <summary>
        /// 员工电话
        /// </summary>
        public virtual string Sat_Tel { get; set; }

        /// <summary>
        /// 员工性别
        /// </summary>
        public virtual int? Sat_Sex { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public virtual DateTime? Sat_rztime { get; set; }

        /// <summary>
        /// 所属团队
        /// </summary>
        public virtual string Sat_teamId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否是管理员 0 普通员工 1管理者
        /// </summary>
        public virtual int type { get; set; }

        /// <summary>
        /// 管理费用占比
        /// </summary>
        public virtual decimal Proportion { get; set; }
    }
}
