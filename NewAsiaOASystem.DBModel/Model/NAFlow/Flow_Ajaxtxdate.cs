using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// ajax通知提醒（生产通知单）
    /// </summary>
    public class Flow_Ajaxtxdate
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 状态类型  0 待技术确认 1 待生产  2 生产中 3 缺料  4 完成
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 是否已经通知到
        /// </summary>
        public virtual int? Istz { get; set; }

        /// <summary>
        /// 接受到通知时间
        /// </summary>
        public virtual DateTime? Tzdatetime { get; set; }

        /// <summary>
        /// 接受通知的
        /// </summary>
        public virtual string Js_userId { get; set; }

        /// <summary>
        /// 是否通知过 0 未通知 1 已经通知
        /// </summary>
        public virtual int? tzdtype { get; set; }

        /// <summary>
        /// 生产产品类型 （0非标销售  1非标工程）
        /// </summary>
        public virtual int? Sccptype { get; set; }
    }
}
