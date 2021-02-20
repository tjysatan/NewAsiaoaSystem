using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
    //菜单按钮关系表
    public class SysRoleFun
    {
        /// <summary>
        /// MenuId  菜单编号
        /// </summary>
        public virtual string RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// ButtonId 按钮操作编号
        /// </summary>
        public virtual string FunctionId
        {
            get;
            set;
        }

    }
}