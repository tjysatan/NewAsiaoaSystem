using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 返退货 出货单
    /// </summary>
    public class NQ_chuhuoinfonView
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
        /// 流程跟踪单ID
        /// </summary>
        public virtual string RlId
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu
        {
            get;
            set;
        }

        /// <summary>
        /// 品保签字
        /// </summary>
        public virtual string PbId
        {
            get;
            set;
        }

        /// <summary>
        /// 品保签字时间
        /// </summary>
        public virtual DateTime? Pb_DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 出货单状态
        /// </summary>
        public virtual int NQ_Type
        {
            get;
            set;
        }

        /// <summary>
        /// 开单业务员
        /// </summary>
        public virtual string CreatePerson
        {
            get;
            set;
        }

        /// <summary>
        /// 开单时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库出货人
        /// </summary>
        public virtual string UpdatePerson
        {
            get;
            set;
        }

        /// <summary>
        ///  仓库出货时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }
    }
}
