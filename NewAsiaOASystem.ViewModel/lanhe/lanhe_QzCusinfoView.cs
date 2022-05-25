using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class lanhe_QzCusinfoView
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string Cusname { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string lxname { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string lxtle { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public virtual string hy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? c_time { get; set; }
    }
}
