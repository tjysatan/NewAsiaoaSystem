using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 仓库 登记返退货产品明细
    /// </summary>
    public class NQ_ThdetailinfoView
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货流程ID
        /// </summary>
        public virtual string R_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public virtual string P_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int SL
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time
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
        /// 客户描述的不良现象
        /// </summary>
        public virtual string Cust_Baddescribe { get; set; }
    }
}
