using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
	 	//SysRoleMenuButton   角色菜单操作按钮关系表
    public class SysRoleMenuButton
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
		/// MenuId  菜单编号
        /// </summary>
        public virtual string MenuButtonId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ButtonId  操作按钮编号
        /// </summary>
        public virtual string ButtonId
        {
            get; 
            set; 
        }        
		   
	}
}