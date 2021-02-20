using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
	 	//SysRoleColumn  角色列权限表
		public class SysRoleColumn
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
        public virtual string MenuId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Type  类型
        /// </summary>
        public virtual string Type
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Field_name  字段名称
        /// </summary>
        public virtual string Field_name
        {
            get; 
            set; 
        }        
		   
	}
}