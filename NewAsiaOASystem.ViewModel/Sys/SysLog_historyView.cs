using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
  public  class SysLog_historyView
    {
      //SysLog_history  登录历史表 
      	/// <summary>
		/// Id   编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Personid  登录人编号
        /// </summary>
        public virtual string Personid
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Personname  登录人名称
        /// </summary>
        public virtual string Personname
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Client_name 客户端机器名称
        /// </summary>
        public virtual string Client_name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LogonIP  登录IP
        /// </summary>
        public virtual string LogonIP
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LogonTime  登录时间
        /// </summary>
        public virtual DateTime? LogonTime
        {
            get; 
            set; 
        }        
    }
}
