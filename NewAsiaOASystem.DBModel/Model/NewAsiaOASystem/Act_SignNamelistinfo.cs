using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 会议 与会签到者信息实体
    /// </summary>
    public  class Act_SignNamelistinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 与会者Id代码
        /// </summary>
        public virtual string Dm { get; set; }

        /// <summary>
        /// 与会者名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 与会者联系方式
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// 与会者公司名称
        /// </summary>
        public virtual string Company { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否上墙 0 无需上墙 1要上墙 （0 未签到 1 已经签到）
        /// </summary>
        public virtual int? Issq { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public virtual DateTime? Qddatetime { get; set; }

        /// <summary>
        /// 是否原定与会者 0 是  1 不是
        /// </summary>
        public virtual int? Isyd { get; set; }


        /// <summary>
        /// 是否滚动过 0 未滚动过 1 滚动过
        /// </summary>
        public virtual int? Ispg { get; set; }

    }
}
