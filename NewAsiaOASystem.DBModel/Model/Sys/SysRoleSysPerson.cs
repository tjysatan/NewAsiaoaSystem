using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
	 	//SysRoleSysPerson   角色用户关系表
		public class SysRoleSysPerson
	{
	
      	/// <summary>
		/// SysPersonId  用户编号
        /// </summary>
        public virtual string SysPersonId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// SysRoleId  角色编号
        /// </summary>
        public virtual string SysRoleId
        {
            get; 
            set; 
        }        
		   
	}
}