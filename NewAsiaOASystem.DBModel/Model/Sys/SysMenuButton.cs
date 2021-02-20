using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
    //菜单按钮关系表
    public class SysMenuButton
    {
        /// <summary>
        /// MenuId  菜单编号
        /// </summary>
        public virtual string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// ButtonId 按钮操作编号
        /// </summary>
        public virtual string ButtonId
        {
            get;
            set;
        }

    }
}