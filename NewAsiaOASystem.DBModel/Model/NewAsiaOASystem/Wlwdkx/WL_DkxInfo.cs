using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 物联网电控箱信息表
    /// </summary>
    public class WL_DkxInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// SSD编码（物联网电控箱唯一识别码）
        /// </summary>
        public virtual string WL_SSD { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public virtual DateTime? SC_time { get; set; }

        /// <summary>
        /// 生产批号
        /// </summary>
        public virtual string SC_PH { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public virtual DateTime? CS_time { get; set; }

        /// <summary>
        /// 测试状态 0 未测试  1 已测试
        /// </summary>
        public virtual int CS_zt { get; set; }

        /// <summary>
        /// 销售时间
        /// </summary>
        public virtual DateTime? Xs_datetime { get; set; }

        /// <summary>
        /// 经销商Id
        /// </summary>
        public virtual string Xs_jxsId { get; set; }

        /// <summary>
        /// 工程商Id
        /// </summary>
        public virtual string Xs_gcsId { get; set; }

        /// <summary>
        /// 上线时间（正式使用）
        /// </summary>
        public virtual DateTime? Sx_time { get; set; }


        /// <summary>
        /// 保修时间开始（正式上线算起）
        /// </summary>
        public virtual DateTime? WL_BXStarttime { get; set; }

        /// <summary>
        /// 保修时间结束
        /// </summary>
        public virtual DateTime? WL_BXEndtime { get; set; }

        /// <summary>
        /// 物联网电控箱状态 0 未售 1 已售 2 在线 3 返修 
        /// </summary>
        public virtual int WL_zt { get; set; }


        /// <summary>
        /// 是否销售 0 未销售  1 已销售
        /// </summary>
        public virtual int? Is_xs { get; set; }

        /// <summary>
        /// 是否上线 0 未上线  1 已上线
        /// </summary>
        public virtual int? Is_sx { get; set; }


        /// <summary>
        /// 缴费状态 0 正常 1欠费
        /// </summary>
        public virtual int Jf_zt { get; set; }

        /// <summary>
        /// 上次缴费时间
        /// </summary>
        public virtual DateTime?  Jf_starttime { get; set; }

        /// <summary>
        /// 下次缴费时间
        /// </summary>
        public virtual DateTime? Jf_endtime { get; set; }

        /// <summary>
        /// 缴费次数
        /// </summary>
        public virtual int Jf_cs { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 记录状态 0 启用 1 禁用
        /// </summary>
        public virtual int States { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        /// <summary>
        /// 订单明细Id
        /// </summary>
        public virtual string OrdermxId { get; set; }

        /// <summary>
        /// 远程监控sid序号
        /// </summary>
        public virtual int? sidxh { get; set; }

        /// <summary>
        /// 远程监控sid 创建时间
        /// </summary>
        public virtual DateTime? sidc_time { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public virtual string monitor_name { get; set; }

        /// <summary>
        /// erp 发货拣配单的单号
        /// </summary>
        public virtual string erp_JPorderno { get; set; }

        /// <summary>
        /// erp 拣配单客户的code
        /// </summary>
        public virtual string erp_jxscode { get; set; }

        /// <summary>
        /// erp 拣配单客户名称
        /// </summary>
        public virtual string erp_jxsname { get; set; }

        /// <summary>
        /// 出货类型 0 销售订单   1 erp 发货拣配单
        /// </summary>
        public virtual int? chtype { get; set; }

    }
}
