using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.ViewModel
{
    //授权代码表
    public class SysFunctionView
    {

        /// <summary>
        /// Id 编码
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name  授权代码名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// action路径
        /// </summary>
        public virtual string ActionUrl
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

        /// <summary>
        /// 状态(0禁用,1启用)
        /// </summary>
        public virtual int Status { get; set; }
    }
}