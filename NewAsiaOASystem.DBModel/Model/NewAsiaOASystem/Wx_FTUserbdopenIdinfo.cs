using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退货微信提醒帐号绑定实体
    /// </summary>
    public class Wx_FTUserbdopenIdinfo
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// 帐号Id
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        public virtual string OpenId { get; set; }

        /// <summary>
        /// 部门责任 0 客服  1客服主管  2 维修电控箱 3 维修温控器 4维修其他 5 品保  6 电气工程师  7 箱体确认  8 其他物料确认
        /// 
        /// </summary>
        public virtual int?  Bmtype { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
