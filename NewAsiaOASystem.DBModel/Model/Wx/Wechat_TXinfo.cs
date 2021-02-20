using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public  class Wechat_TXinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 用户 OpenId
        /// </summary>
        public virtual string OpenId { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public virtual int? Jine { get; set; }

        /// <summary>
        /// 提现时间
        /// </summary>
        public virtual DateTime? Tx_time { get; set; }

        /// <summary>
        /// 确认状态 0 待提现  1 完成
        /// </summary>
        public virtual int? QR_type { get; set; }

        /// <summary>
        /// 确认提现时间
        /// </summary>
        public virtual DateTime? QR_time { get; set; }
    }
}
