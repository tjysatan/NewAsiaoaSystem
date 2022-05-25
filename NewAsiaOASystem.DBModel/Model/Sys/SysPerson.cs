using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel 
{
	 	//SysPerson  用户表
    public class SysPerson
	{
	
      	/// <summary>
		/// Id   用户编号
        /// </summary>
        public virtual string Id
        {
            get; 
            set; 
        }        
		/// <summary>
		///   登录账号
        /// </summary>
        public virtual string Name
        {
            get; 
            set; 
        }

        /// <summary>
        ///  用户姓名
        /// </summary>
        public virtual string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Url 用户头像图片的地址
        /// </summary>
        public virtual string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Tel 用户联系电话
        /// </summary>
        public virtual string Tel
        {
            get;
            set;
        }
		/// <summary>
		/// Sort 排序
        /// </summary>
        public virtual int? Sort
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Description   描述
        /// </summary>
        public virtual string Description
        {
            get; 
            set; 
        }        
		/// <summary>
		///   密码
        /// </summary>
        public virtual string Password
        {
            get; 
            set; 
        }        
		/// <summary>
		/// State  是否启用
        /// </summary>
        public virtual int? State
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LogonNum  登录次数
        /// </summary>
        public virtual int? LogonNum
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LastLogonTime  最后一次登录时间
        /// </summary>
        public virtual DateTime? LastLogonTime
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
		/// UpdatePerson  创建时间
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
		/// <summary>
		/// CreateTime  更新人
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get; 
            set; 
        }

        private IList<SysDepartment> _SysDepartment;
        /// <summary>
        /// _SysPersonId 一个部门对应多个用户
        /// </summary>
        public virtual IList<SysDepartment> Department
        {

            get
            {
                if (_SysDepartment == null)
                {
                    _SysDepartment = new List<SysDepartment>();
                }
                return _SysDepartment;
            }
            set { _SysDepartment = value; }
        }

        private IList<SysRole> _SysRole;
        /// <summary>
        /// 一个用户对应多个角色
        /// </summary>
        public virtual IList<SysRole> Role
        {
            get {
                if (_SysRole == null)
                {
                    _SysRole = new List<SysRole>();
                }
                return _SysRole;
            }
            set { _SysRole = value; }
        }

        /// <summary>
        /// 单位ID()
        /// </summary>
        public virtual string DisImmuneCenter
        {
            get;
            set;
        }

        /// <summary>
        /// erp中员工编号
        /// </summary>
        public virtual string ERP_YGNO
        {
            get;
            set;
        }


    }
}