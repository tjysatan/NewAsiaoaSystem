using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NACuscxView
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string lxrname { get; set; }


        /// <summary>
        /// 电话
        /// </summary>
        public virtual string tel { get; set; }

        /// <summary>
        /// 客户来源
        /// </summary>
        public virtual string Isly { get; set; }

        /// <summary>
        /// 经销商类别
        /// </summary>
        public virtual string jxstype { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual string Isqy { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int? pageIndex { get; set; }
    }
}
