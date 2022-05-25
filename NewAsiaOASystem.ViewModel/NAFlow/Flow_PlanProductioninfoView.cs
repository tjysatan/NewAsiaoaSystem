using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 生产计划实体
    /// </summary>
    public class Flow_PlanProductioninfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 产品的Id
        /// </summary>
        public virtual string P_CPId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string P_CPname { get; set; }

        /// <summary>
        /// 产品的物料代码
        /// </summary>
        public virtual string P_Pianhao { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string P_Model { get; set; }

        /// <summary>
        /// 需要生产数量
        /// </summary>
        public virtual decimal? P_SCnumber { get; set; }


        /// <summary>
        /// 计划单状态 0 生产中 1缺料 2 待生产 3 完成  4 技术待确认  
        /// </summary>
        public virtual int? P_Issc { get; set; }

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
        public virtual int? Sort { get; set; }

        /// <summary>
        /// 0 启用 1禁止
        /// </summary>
        public virtual int? Status { get; set; }

        /// <summary>
        /// 生产通知单是否 打印 '' 未打印  0 未打印 1 已打印
        /// </summary>
        public virtual int? Isprint { get; set; }

        /// <summary>
        ///  0 常规温控  1非标（销售）温控  2 非标（工程）温控  3 小批量试产温控   4 常规电控箱
        /// </summary>
        public virtual int? P_type { get; set; }

        /// <summary>
        /// 预计交期
        /// </summary>
        public virtual DateTime? YJdatetime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public virtual DateTime? wcdatetime { get; set; }

        /// <summary>
        /// 工位1  领料、拆包打底板
        /// </summary>
        public virtual string gwy { get; set; }

        /// <summary>
        /// 工位2 接控制线
        /// </summary>
        public virtual string gwe { get; set; }

        /// <summary>
        /// 工位3 接主线
        /// </summary>
        public virtual string gws { get; set; }

        /// <summary>
        /// 工位4 上温控线、绕绕管
        /// </summary>
        public virtual string gwsi { get; set; }

        /// <summary>
        /// 工位5 面板装箱
        /// </summary>
        public virtual string gww { get; set; }

        /// <summary>
        /// 工位6 底板装箱
        /// </summary>
        public virtual string gwl { get; set; }

        /// <summary>
        /// 工位7 接温控限
        /// </summary>
        public virtual string gwq { get; set; }

        /// <summary>
        /// 工位8 焊灯
        /// </summary>
        public virtual string gwb { get; set; }

        /// <summary>
        /// 工位9  调式
        /// </summary>
        public virtual string gwj { get; set; }

        /// <summary>
        /// 工位10 打包入库
        /// </summary>
        public virtual string gwshi { get; set; }

        /// <summary>
        /// 生产编号年月日+当天的单数
        /// </summary>
        public virtual string scbianhao { get; set; }

        /// <summary>
        /// 生产中缺少的元器件
        /// </summary>
        public virtual string  Ppurchaselist { get; set; }

        /// <summary>
        /// 是否生产任务单是否同步K3 0未同步 1 同步过
        /// </summary>
        public virtual int? Istbwork { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public virtual DateTime? tbworktime { get; set; }

        /// <summary>
        /// 具体的排单日期 默认是创建日期
        /// </summary>
        public virtual DateTime? Nc_time { get; set; }

        /// <summary>
        /// 开工时间
        /// </summary>
        public virtual DateTime? kgdatetime { get; set; }

        /// <summary>
        /// 是否同步工位机 1 已经同步
        /// </summary>
        public virtual int? Gwj_Istb { get; set; }

        /// <summary>
        /// 工位机最近的同步时间
        /// </summary>
        public virtual DateTime? Gwj_tbtime { get; set; }

        /// <summary>
        /// 平台订单编号（温控）
        /// </summary>
        public virtual string orderbianhao { get; set; }
    }
}
