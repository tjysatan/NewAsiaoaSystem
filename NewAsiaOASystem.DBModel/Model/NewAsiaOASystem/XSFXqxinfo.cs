using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// name: 销售目标分析 区县销售量信息表
    /// </summary>
    public class XSFXqxinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }


        /// <summary>
        /// 区县名称
        /// </summary>
        public virtual string Dqname { get; set; }

        /// <summary>
        /// 大区Id 
        /// </summary>
        public virtual string dqId { get; set; }

        /// <summary>
        /// 目标数量
        /// </summary>
        public virtual int XSL { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public virtual int WCXL { get; set; }
    }
}
