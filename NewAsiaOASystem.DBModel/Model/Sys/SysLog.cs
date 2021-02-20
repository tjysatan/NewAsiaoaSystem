using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
	 	//SysLog  系统日志表
		public class SysLog
	{
	
      	/// <summary>
		/// Id    日志编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Source   日志来源
        /// </summary>
        public virtual string Source
        {
            get; 
            set; 
        }        
		/// <summary>
		/// TypeId  日志类型编号
        /// </summary>
        public virtual string TypeId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// TypeName  日志类型名称
        /// </summary>
        public virtual string TypeName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Content  日志内容
        /// </summary>
        public virtual string Content
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Remark  日志备注
        /// </summary>
        public virtual string Remark
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Personid  操作人编号
        /// </summary>
        public virtual string Personid
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Personname  操作人名称
        /// </summary>
        public virtual string Personname
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Datetime  操作时间
        /// </summary>
        public virtual DateTime? Datetime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreatePerson 创建人
        /// </summary>
        public virtual string CreatePerson
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateTime 创建时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdatePerson 更新人
        /// </summary>
        public virtual string UpdatePerson
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdateTime 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get; 
            set; 
        }        
		   
	}
}