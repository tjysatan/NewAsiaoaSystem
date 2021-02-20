using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 逾期通知数据实体
    /// </summary>
    public class DKX_DDCLyqNoteInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual string DD_Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string DD_DH { get; set; }

        /// <summary>
        /// 逾期类型 0 制图接单逾期 1 制图逾期 2 箱体库存确认逾期 3 其他物料库存确认逾期 4 生产接单逾期 5 生产逾期 6 发货逾期  7 审核逾期
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public virtual string Xml { get; set; }

        /// <summary>
        /// 接受人
        /// </summary>
        public virtual string Jsname { get; set; }

        /// <summary>
        /// 0 主接受人 1 抄送人员
        /// </summary>
        public virtual int? fsdxtype { get; set; }

        /// <summary>
        /// 发送时间段 0 上午 1 下午
        /// </summary>
        public virtual int? fstype { get; set; }
    }
}
