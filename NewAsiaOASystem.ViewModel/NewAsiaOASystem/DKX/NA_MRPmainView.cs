using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NA_MRPmainView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 计算的订单下单时间
        /// </summary>
        public virtual DateTime? Ordertime { get; set; }

        /// <summary>
        /// 参与计算的订单数量
        /// </summary>
        public virtual int? Ordersum { get; set; }

        /// <summary>
        /// 计算时间
        /// </summary>
        public virtual DateTime? c_time { get; set; }

        /// <summary>
        /// 操作计算人Id
        /// </summary>
        public virtual string c_userId { get; set; }

        /// <summary>
        /// 操作计算人名
        /// </summary>
        public virtual string C_username { get; set; }

        /// <summary>
        /// 状态 0 启用 1 禁用
        /// </summary>
        public virtual int? state { get; set; }
    }
}
