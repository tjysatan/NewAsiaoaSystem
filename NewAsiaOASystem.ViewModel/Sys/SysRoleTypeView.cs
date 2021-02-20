using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 角色类型实体（0表示超级管理员，1表示普通管理员，2表示普通用户）
    /// 超级管理员可以查看所有数据，普通管理员只能查看本单位或部门数据，普通用户只能查看自己数据
    /// </summary>
    public class SysRoleTypeView
    {

        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
    }
}