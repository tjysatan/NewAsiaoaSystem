using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 返退货微信提醒帐号绑定实体
    /// </summary>
    public  class Wx_FTUserbdopenIdinfoView
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
        /// 部门责任 0 客服 1客服主管  2 维修电控箱 3 维修温控器 4维修其他 5 品保
        /// 1 客服主管 2 电控维修 3 温控维修 4 配件维修 5 品保经理 6 电气工程师 7 生产主管 8 客服  9 采购 10 箱体确认 11 其他物料 12 仓库 13 主管工程师
        /// </summary>
        public virtual int? Bmtype { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }
    }
}
