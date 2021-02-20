using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
	 	//SysDepartmentRole   机构角色关系表
		public class SysDepartmentRole
	{
	
      	/// <summary>
		/// DepartmentId   机构编号
        /// </summary>
        public virtual string DepartmentId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RoleId  角色编号
        /// </summary>
        public virtual string RoleId
        {
            get; 
            set; 
        }        
		   
	}
}