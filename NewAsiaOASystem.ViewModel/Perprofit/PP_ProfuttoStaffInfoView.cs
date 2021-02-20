using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class PP_ProfuttoStaffInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 收入项Id
        /// </summary>
        public virtual string ProfutId { get; set; }

        /// <summary>
        /// 个人（员工）Id
        /// </summary>
        public virtual string StaffId { get; set; }

        /// <summary>
        /// 关系类型 0 与收入项的关系  1 与支出项的关系
        /// </summary>
        public virtual int type { get; set; }
    }
}
