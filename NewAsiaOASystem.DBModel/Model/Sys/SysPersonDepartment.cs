using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
	 	//SysPersonDepartment     用户机构关系表
		public class SysPersonDepartment
	{
	
      	/// <summary>
		/// DepartmentId  机构编号
        /// </summary>
        public virtual string DepartmentId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// PersonId   用户编号
        /// </summary>
        public virtual string PersonId
        {
            get; 
            set; 
        }        
		   
	}
}