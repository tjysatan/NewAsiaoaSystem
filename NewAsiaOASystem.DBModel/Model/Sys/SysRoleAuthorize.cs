using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    //SysRoleAuthorize   角色授权关系表
    public class SysRoleAuthorize
    {

        /// <summary>
        /// RoleId   角色编号
        /// </summary>
        public virtual string RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// AuthorizeId  授权编号
        /// </summary>
        public virtual string AuthorizeId
        {
            get;
            set;
        }

    }
}