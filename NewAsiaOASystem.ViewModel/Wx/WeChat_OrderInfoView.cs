using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
   public class WeChat_OrderInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 用户opneid
        /// </summary>
        public virtual string OpenId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Cpname { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? Sl { get; set; }

        /// <summary>
        /// 选购方案  0 包含三年通讯费  1 包含一年通讯费
        /// </summary>
        public virtual int? Fangan { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public virtual decimal? cpJiage { get; set; }

        /// <summary>
        /// 通讯价格
        /// </summary>
        public virtual decimal? txjg { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public virtual int? Jiage { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 支付成功时间
        /// </summary>
        public virtual DateTime? Zf_time { get; set; }

       /// <summary>
       /// 收货地址
       /// </summary>
        public virtual string addr { get; set; }

       /// <summary>
       /// 收货电话
       /// </summary>
        public virtual string Tel { get; set; }
    }
}
