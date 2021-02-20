using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 续费订单
    /// </summary>
    public class WL_XFOrderinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 续费订单价格
        /// </summary>
        public virtual decimal? price { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime? xddatetime { get; set; }

        /// <summary>
        /// 下次缴费时间
        /// </summary>
        public virtual DateTime? Xcxfdatetime { get; set; }

        /// <summary>
        /// sid
        /// </summary>
        public virtual string Sid { get; set; }

        /// <summary>
        /// 产品型号名称
        /// </summary>
        public virtual string modele_name { get; set; }

        /// <summary>
        /// 监控点名称
        /// </summary>
        public virtual string monitor_name { get; set; }

        /// <summary>
        /// 数据获取时间 同步远程数据时间
        /// </summary>
        public virtual DateTime? Huoqudate { get; set; }

        /// <summary>
        /// 远程Ids
        /// </summary>
        public virtual int? Ids { get; set; }

        /// <summary>
        /// 是否分润订单 0 是 1 否 （经销商分润）
        /// </summary>
        public virtual int? Isfr { get; set; }
    }
}
