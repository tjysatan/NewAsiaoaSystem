using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// Name:销售订单明细
    /// author：tjy_satan
    /// </summary>
    public class NA_XSdetailsinfo
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
        /// 产品Id
        /// </summary>
        public virtual string cpId
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string cpname
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual string xsId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售数量
        /// </summary>
        public virtual int SL
        {
            get;
            set;
        }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Je
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_datetime
        {
            get;
            set;
        }

        /// <summary>
        /// 库存类型 0 正常 1 缺货
        /// </summary>
        public virtual int kcType
        {
            get;
            set;
        }
    }
}
