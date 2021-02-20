using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 销售目标分析 客户销售量信息表
    /// </summary>
    public class XSFXkhinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string KHName { get; set; }

        /// <summary>
        /// 目标数量
        /// </summary>
        public virtual int XSL { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public virtual int WCSL { get; set; }

        /// <summary>
        /// 区县ID
        /// </summary>
        public virtual string QxId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string beizhu { get; set; }
    }
}
