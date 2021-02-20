using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel 
{
   public class XSFXdqinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 大区名称
        /// </summary>
        public virtual string dqname { get; set; }

        /// <summary>
        /// 目标销售量
        /// </summary>
        public virtual int XSL { get; set; }

        /// <summary>
        /// 制定目标的年费
        /// </summary>
        public virtual string XSYear { get; set; }

        /// <summary>
        /// 制定时间
        /// </summary>
        public virtual DateTime? C_datetime { get; set; }

        /// <summary>
        /// 已完成数量
        /// </summary>
        public virtual int WCSL { get; set; }

        public virtual string zrr { get; set; }
    }
}
