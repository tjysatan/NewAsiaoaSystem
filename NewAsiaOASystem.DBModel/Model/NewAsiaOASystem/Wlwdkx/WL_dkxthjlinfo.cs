using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 物联网电控箱退货记录信息表
    /// </summary>
    public class WL_dkxthjlinfo
    {
        /// <summary>
        /// 唯一识别号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// SID号码
        /// </summary>
        public virtual string SID { get; set; }

        /// <summary>
        /// 退货时间（扫码时间）
        /// </summary>
        public virtual DateTime? THdatetime { get; set; }

        /// <summary>
        /// 上次出货时间
        /// </summary>
        public virtual DateTime? scxsdtetime { get; set; }

        /// <summary>
        /// 上次销售关联的订单Id
        /// </summary>
        public virtual string XsordId { get; set; }

        /// <summary>
        /// 退货类型 0 正常订单退货 1 扫错码 初始化退货
        /// </summary>
        public virtual int? jltype { get; set; }
    }
}
