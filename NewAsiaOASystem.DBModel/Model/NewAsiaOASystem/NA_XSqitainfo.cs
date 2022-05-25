using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class NA_XSqitainfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 电商订单的单号
        /// </summary>
        public virtual string DsOrder_Id { get; set; }

        /// <summary>
        /// 交货期限
        /// </summary>
        public virtual string Dsjhqx { get; set; }

        /// <summary>
        /// 需求日期
        /// </summary>
        public virtual DateTime? xqtime { get; set; }

        /// <summary>
        /// 运费承担
        /// </summary>
        public virtual string Dsyfcd { get; set; }

        /// <summary>
        /// 交货地点，方式
        /// </summary>
        public virtual string Dsjhdd { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual string DsJSFS { get; set; }

        /// <summary>
        /// 是否同步K3
        /// </summary>
        public virtual string DsIsK3 { get; set; }

        /// <summary>
        /// 电商信息添加时间
        /// </summary>
        public virtual string Dsaddtime { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public virtual string Dsywry { get; set; }

        public virtual string DsIsseed { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Dscell { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public virtual decimal? Dsyf { get; set; }

        /// <summary>
        /// 调式费
        /// </summary>
        public virtual decimal? DSF { get; set; }

        /// <summary>
        /// 电商订单编号
        /// </summary>
        public virtual string Dsordercode { get; set; }

        /// <summary>
        /// 记录创建的时间
        /// </summary>
        public virtual DateTime? C_Time { get; set; }

        /// <summary>
        /// 供方电话
        /// </summary>
        public virtual string gftel { get; set; }
    }
}
