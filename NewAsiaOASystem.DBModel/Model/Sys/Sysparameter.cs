using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
	 	//Sysparameter  系统参数表
		public class Sysparameter
	{
	
      	/// <summary>
		/// Id  编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Value   参数值
        /// </summary>
        public virtual string Value
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Edit  用户是否可以编辑
        /// </summary>
        public virtual int? Edit
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Description  描述
        /// </summary>
        public virtual string Description
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreatePerson  创建人
        /// </summary>
        public virtual string CreatePerson
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateTime  创建时间
        /// </summary>
        public virtual decimal? CreateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdatePerson  更新人
        /// </summary>
        public virtual string UpdatePerson
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdateTime  更新时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get; 
            set; 
        }        
		   
	}
}