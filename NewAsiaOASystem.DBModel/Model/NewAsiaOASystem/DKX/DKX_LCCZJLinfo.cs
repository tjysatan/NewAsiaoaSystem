﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class DKX_LCCZJLinfo
    {
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 操作的数据记录Id
        /// </summary>
        public virtual string dd_Id { get; set; }

        /// <summary>
        /// 数据操作的名称或内容
        /// </summary>
        public virtual string caozuo { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string c_name { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime? c_time { get; set; }

        /// <summary>
        /// 订单编号(非标订单生产订单的操作记录)
        /// </summary>
        public virtual string dd_bianhao { get; set; }

        /// <summary>
        /// 工程师Id(非标订单生产订单的操作记录)
        /// </summary>
        public virtual string gcs_Id { get; set; }

        /// <summary>
        /// 操作类型 -1 其他操作 0 生产退单 
        /// </summary>
        public virtual int caozuotype { get; set; }

        /// <summary>
        /// 退单原因Id
        /// </summary>
        public virtual string CBRId { get; set; }

        /// <summary>
        /// 退单原因名称
        /// </summary>
        public virtual string CBRName { get; set; }

        /// <summary>
        /// 退单原因备注
        /// </summary>
        public virtual string CBRRemarks { get; set; }

        /// <summary>
        /// 生产退单之后操作的 1
        /// </summary>
        public virtual int? IsPdrefund { get; set; }
    }
}
