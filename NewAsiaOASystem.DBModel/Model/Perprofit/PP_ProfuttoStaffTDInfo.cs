﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 个人与团体支出收入项的关系以及分配比例实体
    /// </summary>
    public class PP_ProfuttoStaffTDInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 支出或者收入项Id
        /// </summary>
        public virtual string ProfutId { get; set; }

        /// <summary>
        /// 个人（员工）Id
        /// </summary>
        public virtual string StaffId { get; set; }

        /// <summary>
        /// 类型 0 收入 1 支出
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 分配比例
        /// </summary>
        public virtual decimal? Proportion { get; set; }


    }
}
