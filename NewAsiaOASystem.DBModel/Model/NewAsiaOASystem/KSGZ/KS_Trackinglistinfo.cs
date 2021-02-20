using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 客诉跟踪单实体
    /// </summary>
    public class KS_Trackinglistinfo
    {
        /// <summary>
        /// 编号Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 客诉但编号 
        /// </summary>
        public virtual string KSbianma { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string khname { get; set; }

        /// <summary>
        /// 投诉人
        /// </summary>
        public virtual string Tsname { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Lxtel { get; set; }

        /// <summary>
        /// 现场人员
        /// </summary>
        public virtual string Xcname { get; set; }

        /// <summary>
        /// 现场电话
        /// </summary>
        public virtual string Xctel { get; set; }

        /// <summary>
        /// 使用地
        /// </summary>
        public virtual string SYD { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual string CpnameId { get; set; }

        /// <summary>
        /// 生产批号
        /// </summary>
        public virtual string CPSCPH { get; set; }

        /// <summary>
        /// 接诉人
        /// </summary>
        public virtual string Jsname { get; set; }

        /// <summary>
        /// 接诉时间
        /// </summary>
        public virtual DateTime? Jstime { get; set; }

        /// <summary>
        /// 设备使用情况 0 调式中 1使用中
        /// </summary>
        public virtual int? SBqk { get; set; }

        /// <summary>
        /// 设备使用时长
        /// </summary>
        public virtual string SBsysc { get; set; }

        /// <summary>
        /// 故障现象（图文混编）
        /// </summary>
        public virtual string gzxxstrcon { get; set; }

        /// <summary>
        /// 控制器问题 （硬件问题、软件问题、制造、）
        /// </summary>
        public virtual string Kzqwt { get; set; }

        /// <summary>
        /// 情况说明
        /// </summary>
        public virtual string Kzqqksm { get; set; }

        /// <summary>
        /// 是否等不良品退回 0 是 1否
        /// </summary>
        public virtual int? Isth { get; set; }

        /// <summary>
        /// 接线问题说明
        /// </summary>
        public virtual string Jxwtsm { get; set; }

        /// <summary>
        /// 图纸设计问题
        /// </summary>
        public virtual string Tzsjwtsm { get; set; }

        /// <summary>
        /// 前期沟通问题
        /// </summary>
        public virtual string Qqgtwtsm { get; set; }

        /// <summary>
        /// 远程问题说明
        /// </summary>
        public virtual string Ycwtsm { get; set; }

        /// <summary>
        /// 物流问题说明
        /// </summary>
        public virtual string WLwtsm { get; set; }

        /// <summary>
        /// 客户端安装和使用问题（接线、参数设置、其他）
        /// </summary>
        public virtual string KHazsywt { get; set; }

        /// <summary>
        /// 客户端安装和使用问题说明
        /// </summary>
        public virtual string KHazsywtsm { get; set; }

        /// <summary>
        /// 客户设置说明
        /// </summary>
        public virtual string Dqwt { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public virtual string CLname { get; set; }

        /// <summary>
        /// 处理接单时间
        /// </summary>
        public virtual DateTime? JDTIME { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? Cltime { get; set; }

        /// <summary>
        /// 是否定责 是否定责 0 未定责1 已定责
        /// </summary>
        public virtual int? Isdz { get; set; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public virtual string Zrbm { get; set; }

        /// <summary>
        /// 定责人
        /// </summary>
        public virtual string Dzname { get; set; }

        /// <summary>
        /// 定责时间
        /// </summary>
        public virtual DateTime? Dztime { get; set; }

        /// <summary>
        /// 订单状态 -1未提交 0 待接单 1处理中 2 待定责 3 待完成 4 已完成 
        /// </summary>
        public virtual int? KSDtype { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? Wctime { get; set; }

        /// <summary>
        /// 状态 0 启用 1禁用
        /// </summary>
        public virtual int? Static { get; set; }
    }
}
