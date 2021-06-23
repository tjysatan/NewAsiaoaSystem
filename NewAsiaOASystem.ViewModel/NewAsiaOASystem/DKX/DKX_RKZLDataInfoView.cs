using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 电控箱产品的 相关入库资料
    /// </summary>
    public  class DKX_RKZLDataInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 最初的订单Id
        /// </summary>
        public virtual string dd_Id { get; set; }

        /// <summary>
        /// 最初的订单
        /// 
        /// 常规产品的数据 绑定的是物料编号
        /// </summary>
        public virtual string cpId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string wjName { get; set; }

        /// <summary>
        /// 文件的地址
        /// </summary>
        public virtual string wjurl { get; set; }

        /// <summary>

        /// 资料类型 0 需求 (客户需求)
        /// 1 料单 （料单（BOM）） 
        /// 2 图纸 (箱体图)
        /// 3 照片 
        /// 4 验收单  
        /// 5（其他图纸） （电器原理图）
        /// 6 电器排布图 （布局图） 
        /// 7 系统简图  （=）
        /// 8 软件程序-烧录软件 （仅仅plc 项目有）
        /// 9 操作手册 （仅仅plc 项目有）
        /// 10 线号表 （可以不填）
        /// 11软件程序-源程序（仅仅plc 项目有）
        /// </summary>
        /// 常规产品的数据(物联网)2 底板装配一  3 底板装配二 4接控制线一 5 接控制线二 6 接主回路线 7 上温控线绕管 8 面板装箱 9 底板装箱 10 调式  11 包装
        /// 常规产品的数据(NAK)   2 底板装配一  3 接控制线一 4接主回路线 5 上温控线绕管 6 面板装箱 7 底板装箱 8 接温控线 9 焊灯 10 调式  11 包装
        /// 温控产品数据  2 SMT   3 插件  4 焊接  5 洗板 6 看板 7 烧录 8 初检 9 灌胶 10 老化 11 防潮 12 装配 13 总检 14 包装
        public virtual int? Zl_type { get; set; }

        /// <summary>
        /// 是否关联 0 附件 1 关联报价系统 2 关联K3
        /// </summary>
        public virtual int? Isgl { get; set; }

        /// <summary>
        /// 报价订单号
        /// </summary>
        public virtual string Bjno { get; set; }

        /// <summary>
        /// k3BOM表的号码
        /// </summary>
        public virtual string BomNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public virtual string D_name { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? D_datetime { get; set; }

        /// <summary>
        /// 是否禁用 0 启用 1禁用
        /// </summary>
        public virtual int? Start { get; set; }
    }
}
