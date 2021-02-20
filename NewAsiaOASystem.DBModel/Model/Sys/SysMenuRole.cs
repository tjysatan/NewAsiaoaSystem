using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    //SysMenuRole   菜单角色关系表
    public class SysMenuRole
    {

        /// <summary>
        /// RoleId  角色编号
        /// </summary>
        public virtual string RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// MenuId   菜单编号
        /// </summary>
        public virtual string MenuId
        {
            get;
            set;
        }

    }
}