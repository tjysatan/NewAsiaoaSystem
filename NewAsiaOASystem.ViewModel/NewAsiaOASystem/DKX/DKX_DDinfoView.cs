using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 电控箱下单实体
    /// </summary>
    public class DKX_DDinfoView
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// 生产单编号
        /// </summary>
        public virtual string DD_Bianhao { get; set; }

        /// <summary>
        /// 关联报价编号
        /// </summary>
        public virtual string BJno { get; set; }

        /// <summary>
        /// k3 bom表
        /// </summary>
        public virtual string KBomNo { get; set; }

        /// <summary>
        /// 需求是否关联
        /// </summary>
        public virtual int? xqIsgl { get; set; }

        /// <summary>
        /// 料单是否关联
        /// </summary>
        public virtual int? ldIsgl { get; set; }

        /// <summary>
        /// 订单类型 0 小系统 1大系统 3 物联网
        /// </summary>
        public virtual int? DD_Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string KHname { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Lxname { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// 订货类型
        /// </summary>
        public virtual string DD_DHType { get; set; }

        /// <summary>
        /// 物联网电控箱的类型 0 一体式 1分体式
        /// </summary>
        public virtual int? DD_WLWtype { get; set; }

        /// <summary>
        /// 功率 KW  P
        /// </summary>
        public virtual string POWER { get; set; }


        /// <summary>
        /// 单位 P KW
        /// </summary>
        public virtual string dw { get; set; }


        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal NUM { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 工程师Id
        /// </summary>
        public virtual string Gcs_nameId { get; set; }

        /// <summary>
        /// 订单状态 -1 未提交 0已提交 1 待制图 2 制图中 
        /// （-1 未提交 0已提交 1 待制图 2 制图中  -2 待审核 3 待生产） 3 待生产(待发料)  4生产中 5 缺料 6 待发货 7 完成  ( 4 可生产  5 缺料 6 生产中 -3 待品审  7 待发货 8 完成 )
        /// </summary>
        public virtual int DD_ZT { get; set; }

        /// <summary>
        /// 备注(客服)
        /// </summary>
        public virtual string REMARK { get; set; }

        /// <summary>
        /// 备注（工程师）
        /// </summary>
        public virtual string REMARK2 { get; set; }

        /// <summary>
        /// 备注（生产）
        /// </summary>
        public virtual string REMARK3 { get; set; }

        /// <summary>
        /// 工程师接单时间
        /// </summary>
        public virtual DateTime? Gscjdtime { get; set; }


        /// <summary>
        /// 工程师提交审核时间
        /// </summary>
        public virtual DateTime? Gstjshtime { get; set; }

        /// <summary>
        /// 工程师完成制图时间
        /// </summary>
        public virtual DateTime? Gscwctime { get; set; }

        /// <summary>
        /// 生产接单时间
        /// </summary>
        public virtual DateTime? Scjdtime { get; set; }

        /// <summary>
        /// 验收时间 
        /// </summary>
        public virtual DateTime? Ysdatetime { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime? Fhdatetime { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public virtual DateTime? Rkdatetime { get; set; }

        /// <summary>
        /// 0 未入库 1 入库 2 方案库存在
        /// </summary>
        public virtual int? Isrk { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UP_datetime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual string UP_name { get; set; }

        /// <summary>
        /// 是否禁用 0 启用 1禁用
        /// </summary>
        public virtual int Start { get; set; }

        /// <summary>
        /// 是否上传需求 0 没有 1存在
        /// </summary>
        public virtual int Isxq { get; set; }

        /// <summary>
        /// 是否关联料单
        /// </summary>
        public virtual int Isld { get; set; }

        /// <summary>
        /// 是否上传图纸
        /// </summary>
        public virtual int Istz { get; set; }

        /// <summary>
        /// 是否上传程序
        /// </summary>
        public virtual int Iscx { get; set; }

        /// <summary>
        /// 工程师返回上一级 原因
        /// </summary>
        public virtual string gcsfh { get; set; }

        /// <summary>
        /// 工程师返回上一级的时间
        /// </summary>
        public virtual DateTime? gcsfhdatetime { get; set; }

        /// <summary>
        /// 生产返回上一级 原因
        /// </summary>
        public virtual string scfh { get; set; }

        /// <summary>
        /// 生产返回的时间
        /// </summary>
        public virtual DateTime? scfhdatetime { get; set; }

        /// <summary>
        /// 发货返回上一级的原因
        /// </summary>
        public virtual string fhfh { get; set; }

        /// <summary>
        /// 发货返回时间
        /// </summary>
        public virtual DateTime? fhfhdatetime { get; set; }

        /// <summary>
        /// 产品批号
        /// </summary>
        public virtual string cpph { get; set; }

        /// <summary>
        /// 生产操作员
        /// </summary>
        public virtual string scczname { get; set; }

        /// <summary>
        /// 生产调式员
        /// </summary>
        public virtual string scDSname { get; set; }

        /// <summary>
        /// 备用字段1  (是否存在 箱体图 0 不存在 1 存在 ) 箱体图无需审核
        /// </summary>
        public virtual string Bnote { get; set; }

        /// <summary>
        /// 备用字段2 （其他图纸 0 未上传 1 通过 2 待审核 3不通过）
        /// </summary>
        public virtual string Bnote1 { get; set; }

        /// <summary>
        /// 备用字段3 （电器排布图 0 未上传 1 通过 2 待审核 3 不通过）
        /// </summary>
        public virtual string Bnote2 { get; set; }

        /// <summary>
        /// 备用字段4 （系统简图 0未上传 1 上传）
        /// </summary>
        public virtual string Bnote3 { get; set; }


        /// <summary>
        /// 箱体缺货情况（0 未 1 缺 2齐）
        /// </summary>
        public virtual int? xtIsq { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? xtqrtime { get; set; }

        /// <summary>
        /// 箱体到货时间
        /// </summary>
        public virtual DateTime? xtdhtime { get; set; }

        /// <summary>
        /// 其他料库存情况（0 未 1 缺 2齐）
        /// </summary>
        public virtual int? qtIsq { get; set; }

        /// <summary>
        /// 其他料缺货处理时间
        /// </summary>
        public virtual DateTime? qtqrtime { get; set; }

        /// <summary>
        /// 其他料到货时间
        /// </summary>
        public virtual DateTime? qtdhtime { get; set; }

        /// <summary>
        /// 物料都齐的时间
        /// </summary>
        public virtual DateTime? wlsqtime { get; set; }

        /// <summary>
        /// 箱体资料最新上传时间
        /// </summary>
        public virtual DateTime? xtsctime { get; set; }

        /// <summary>
        /// 电器排布图最新上传时间
        /// </summary>
        public virtual DateTime? dqpbsctime { get; set; }

        /// <summary>
        /// 系统简图上传时间
        /// </summary>
        public virtual DateTime? xtjtsctime { get; set; }

        /// <summary>
        /// 其他资料的最新上传时间
        /// </summary>
        public virtual DateTime? qtsctime { get; set; }

        /// <summary>
        /// 电器排布图处理时间（主管工程师审核时间）
        /// </summary>
        public virtual DateTime? xttshtime { get; set; }

        /// <summary>
        /// 其他图处理时间（主管工程师审核时间）
        /// </summary>
        public virtual DateTime? qttshtime { get; set; }

        /// <summary>
        /// 电器排布图审核人
        /// </summary>
        public virtual string dqpbshuserId { get; set; }

        /// <summary>
        /// 其他图纸审核人
        /// </summary>
        public virtual string qtshuserId { get; set; }

        /// <summary>
        /// 产品功能简述
        /// </summary>
        public virtual string gnjsstr { get; set; }

        /// <summary>
        /// 是否直接生成的（0 否 1 是）
        /// </summary>
        public virtual int? Iszjsc { get; set; }

        /// <summary>
        ///  大系统单价
        /// </summary>
        public virtual decimal? DXTDJ { get; set; }

        /// <summary>
        /// 品保审核时间
        /// </summary>
        public virtual DateTime? pbshtime { get; set; }

        /// <summary>
        /// 品保审核的账户
        /// </summary>
        public virtual string pdshuserId { get; set; }

        /// <summary>
        /// 品保审核状态 0 未审核  1 通过 1 未通过
        /// </summary>
        public virtual int? pbshzt { get; set; }

        /// <summary>
        /// 品保审核不通过原因
        /// </summary>
        public virtual string pbshbtgyy { get; set; }

        /// <summary>
        /// 设定交期
        /// </summary>
        public virtual DateTime? YJJQtime { get; set; }

        /// <summary>
        /// 电器图纸是否需要审核 0 无需  1 需要审核
        /// </summary>
        public virtual int? Isdqsh { get; set; }

        /// <summary>
        /// 工程师最近提交审核的时间
        /// </summary>
        public virtual DateTime? dqtjshtime { get; set; }

        /// <summary>
        /// 电气工程师最近审核的时间
        /// </summary>
        public virtual DateTime? dqshtime { get; set; }

        /// <summary>
        /// 电气图纸审核的账号
        /// </summary>
        public virtual string dqshuserId { get; set; }

        /// <summary>
        /// 电气审核的结果 0 待审核  1 通过  2 未通过  3 未提交审核
        /// </summary>
        public virtual int? dqshres { get; set; }

        /// <summary>
        /// 电气审核说明 不通过的原因 通过 默认“通过”
        /// </summary>
        public virtual string dqshmsg { get; set; }

        /// <summary>
        /// 助力检查状态 0 需检查 1 待检查 2 确认检查 3 检查驳回 
        /// </summary>
        public virtual int? dragstart { get; set; }

        /// <summary>
        /// 检查人Id
        /// </summary>
        public virtual string draguserId { get; set; }

        /// <summary>
        /// 检查人名称
        /// </summary>
        public virtual string dragusername { get; set; }

        /// <summary>
        /// 助力检查时间
        /// </summary>
        public virtual DateTime? dragtime { get; set; }

        /// <summary>
        /// 助力检查驳回的原因
        /// </summary>
        public virtual string dragsm { get; set; }

        /// <summary>
        /// 合同的单位售价
        /// </summary>
        public virtual decimal? price { get; set; }
    }
}
