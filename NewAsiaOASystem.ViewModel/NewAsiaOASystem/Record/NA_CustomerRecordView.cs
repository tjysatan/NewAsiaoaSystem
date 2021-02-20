using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NA_CustomerRecordView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 备案企业名称
        /// </summary>
        public virtual string Rcusname { get; set; }

        /// <summary>
        /// 备案企业法人
        /// </summary>
        public virtual string Rlegalperson { get; set; }

        /// <summary>
        /// 备案企业电话
        /// </summary>
        public virtual string Rlptel { get; set; }

        /// <summary>
        /// 采购姓名
        /// </summary>
        public virtual string Rpurchasename { get; set; }

        /// <summary>
        /// 采购电话
        /// </summary>
        public virtual string RPTel { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public virtual string Rtyle { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public virtual string Rcptyle { get; set; }

        /// <summary>
        /// 预计年需求量
        /// </summary>
        public virtual decimal? RYearnum { get; set; }

        /// <summary>
        /// 备案人Id
        /// </summary>
        public virtual string RnameId { get; set; }

        /// <summary>
        /// 备案人姓名
        /// </summary>
        public virtual string Rname { get; set; }

        /// <summary>
        /// 备案时间
        /// </summary>
        public virtual DateTime? Rdatatime { get; set; }

        /// <summary>
        /// 备案到期时间
        /// </summary>
        public virtual DateTime? Rexpiredatetime { get; set; }

        /// <summary>
        /// 备案工程师Id
        /// </summary>
        public virtual string RengineerId { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        public virtual string Rengineer { get; set; }

        /// <summary>
        /// 工程师电话
        /// </summary>
        public virtual string RengneerTel { get; set; }

        /// <summary>
        /// 是否过期 0 未过期 1 过期 2待延期
        /// </summary>
        public virtual int? Roverdue { get; set; }

        /// <summary>
        /// 是否成交 0 未成交  1 成交
        /// </summary>
        public virtual int? Rdeal { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual DateTime? c_time { get; set; }
    }
}
