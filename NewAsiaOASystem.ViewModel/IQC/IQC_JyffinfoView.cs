using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 检验方法实体
    /// </summary>
    public class IQC_JyffinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public virtual string Jyffcont { get; set; }

        /// <summary>
        /// 送检作业标准单Id
        /// </summary>
        public virtual string sopId { get; set; }

        /// <summary>
        /// 检验项目 0电气性能 1 尺寸 2 外观 3可靠性 4 其他
        /// </summary>
        public virtual int? Jyxmtype { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
