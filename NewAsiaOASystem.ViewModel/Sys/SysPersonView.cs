using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    //用户表
  public  class SysPersonView
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
        /// Name  用户帐号
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// UserName 用户姓名
        /// </summary>
        public virtual string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Url 用户头像图片存放地址
       /// </summary>
        public virtual string Url
        {
            get;
            set;
        }

      /// <summary>
        /// Tel 用户的联系电话
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
        /// Password  密码
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
        /// UpdatePerson 修改人
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
        /// CreateTime  创建时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get;
            set;
        }

      /// <summary>
      /// Department 对应的部门信息
      /// </summary>
        public virtual string  Department
        {
            get;
            set;
        }
      /// <summary>
        /// Role 对应的用户信息
      /// </summary>
        public virtual string Role
        {
            get;
            set;
        }

      /// <summary>
      /// 免疫点
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
