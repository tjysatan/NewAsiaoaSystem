﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class DKX_CPInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 电控箱产品名称
        /// </summary>
        public virtual string Cpname { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string Xinghao { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public virtual string Power { get; set; }

        /// <summary>
        /// 单位 P KW
        /// </summary>
        public virtual string  DW { get; set; }

        /// <summary>
        /// 产品类型 0小系统 1 大系统 2 物联网 
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 是否分体 0 一体 1 分体
        /// </summary>
        public virtual int? Isft { get; set; }

        /// <summary>
        /// 来源 0 报价系统 1 K3(常规)
        /// </summary>
        public virtual int? Lytype { get; set; }

        /// <summary>
        /// 工程师的名字 产品负责的工程师
        /// </summary>
        public virtual string Gcs_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 入库的时间
        /// </summary>
        public virtual DateTime? RK_time { get; set; }

        /// <summary>
        /// 最初产品订单Id
        /// </summary>
        public virtual string Dd_Id { get; set; }

        /// <summary>
        /// 是否禁用 0 启用 1 禁用(异常)  2 待审核 3 删除
        /// </summary>
        public virtual int? IsDis { get; set; }

        /// <summary>
        /// 最后一次整个的原因简述
        /// </summary>
        public virtual string zgyy { get; set; }

        /// <summary>
        /// 最近一次提出整改的时间
        /// </summary>
        public virtual DateTime? zgtime { get; set; }

        /// <summary>
        /// 提出整改的人
        /// </summary>
        public virtual string zgname { get; set; }

        /// <summary>
        /// 产品功能简述
        /// </summary>
        public virtual string cpgnjs { get; set; }

        /// <summary>
        /// 产品数据来源 0 下单数据 1 现成方案
        /// </summary>
        public virtual int? cplytype { get; set; }

        /// <summary>
        /// 大系统单价
        /// </summary>
        public virtual decimal? DXTDJ { get; set; }
    }
}