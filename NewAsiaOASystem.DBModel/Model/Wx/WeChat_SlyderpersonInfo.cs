using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 大转盘抽奖信息
    /// </summary>
    public class WeChat_SlyderpersonInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 微信用户
        /// </summary>
        public virtual string openid { get; set; }

        /// <summary>
        /// 中奖状态 0 未中奖  1 已中奖 2 已经领奖
        /// </summary>
        public virtual int? IsWin { get; set; }

        /// <summary>
        /// 中奖名称
        /// </summary>
        public virtual string Winname { get; set; }

        /// <summary>
        /// 参加时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 中奖串号
        /// </summary>
        public virtual string Winstr { get; set; }
    }
}
