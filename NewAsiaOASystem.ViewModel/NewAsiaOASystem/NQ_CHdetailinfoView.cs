using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 开单明细
    /// </summary>
    public class NQ_CHdetailinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货流程  ID
        /// </summary>
        public virtual string R_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 退货产品 Id
        /// </summary>
        public virtual string P_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? shuliang
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        public virtual string clfs { get; set; }
    }
}
