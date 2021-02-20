using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 安全库存
    /// </summary>
    public class CG_aqkc
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 元器件 物料代码
        /// </summary>
        public virtual string YqjDm { get; set; }

        /// <summary>
        /// 安全库存数量
        /// </summary>
        public virtual int KqNo { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string UpdatePerson { get; set; }
    }
}
