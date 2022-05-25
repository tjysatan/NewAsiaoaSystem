using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class EP_jlinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 寄件人 ID
        /// </summary>
        public virtual string JjId { get; set; }

        /// <summary>
        /// 收件人ID
        /// </summary>
        public virtual string SjId { get; set; }

        /// <summary>
        /// 寄件时间
        /// </summary>
        public virtual DateTime Jjdatetime { get; set; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        public virtual string Kdgs { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public virtual string Kd_no { get; set; }

        /// <summary>
        /// 是否录入快递单号
        /// </summary>
        public virtual int RL_Is { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }


        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime? RL_datetime { get; set; }

        /// <summary>
        /// 到货数量
        /// </summary>
        public virtual string DHno { get; set; }

        /// <summary>
        /// 预计到货时间
        /// </summary>
        public virtual DateTime? DHyjdatetime { get; set; }

        /// <summary>
        /// 实际到货时间
        /// </summary>
        public virtual DateTime? DHsjdatetime { get; set; }

        /// <summary>
        /// 是否已经发送过微信通知 0未发送 1 发送
        /// </summary>
        public virtual int? IsSend { get; set; }

        /// <summary>
        /// 记录来源 0 本平台  1电商平台  2 推送到快递管家 3 德邦云下单
        /// </summary>
        public virtual int? Source { get; set; }

        /// <summary>
        /// 收件人Id
        /// </summary>
        public virtual string SjaddId { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual int? DDId { get; set; }


        /// <summary>
        /// 实际签收人
        /// </summary>
        public virtual string QRsjName { get; set; }

        /// <summary>
        /// k3订单的单号
        /// </summary>
        public virtual string k3_ddno { get; set; }

        /// <summary>
        /// 是否启用 0 启用 1 禁用
        /// </summary>
        public virtual int? IsDis { get; set; }

        /// <summary>
        /// 0 推送成功  1推送失败 (中通快递/德邦快递/顺丰快递) 2 下单取消 （德邦快递/顺丰快递） 
        /// </summary>
        public virtual int? Istscg { get; set; }

        /// <summary>
        /// 返回说明
        /// </summary>
        public virtual string fhsmg { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public virtual DateTime? fhtime { get; set; }

        /// <summary>
        /// 0 已经打印  1 未打印  该字段只有德邦云下单 和顺丰快递下单
        /// </summary>
        public virtual int? isdy { get; set; }

        //下单填写选择的字段 运输方式
        /// <summary>
        /// 运输方式对应的值
        /// </summary>
        public virtual string transportType { get; set; }


        /// <summary>
        /// 货物名称
        /// </summary>
        public virtual string cargoName { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public virtual string totalWeight { get; set; }

        /// <summary>
        /// 送货方式 1、自提； 2、送货进仓； 3、送货（不含上楼）； 4、送货上楼； 5、大件上楼
        /// </summary>
        public virtual string deliveryType { get; set; }

        /// <summary>
        /// 	0:发货人付款（现付） 1:收货人付款（到付） 2：发货人付款（月结） （电子运单客户不支持寄付）
        /// </summary>
        public virtual string payType { get; set; }

        /// <summary>
        ///  签收回单  0:无需返单 1：签收单原件返回 2:客户签收单传真返回 3:运单签收联原件返回 4: 运单到达联传真返回
        /// </summary>
        public virtual string backSignBill { get; set; }

        /// <summary>
        /// 保价金额
        /// </summary>
        public virtual string insuranceValue { get; set; }

        /// <summary>
        /// 接口返回原因
        /// </summary>
        public virtual string reason { get; set; }

        /// <summary>
        /// 接口返回的 下单订单号
        /// </summary>
        public virtual string logisticID { get; set; }

        /// <summary>
        /// 接口返回 请求唯一编号
        /// </summary>
        public virtual string uniquerRequestNumber { get; set; }


        /// <summary>
        /// 接口返回的 最终到达外场
        /// </summary>
        public virtual string arrivedOutFieldName { get; set; }

        /// <summary>
        /// 顺丰 打单时的路由标签信息如果是大网的路由标签,这里的值是目的地网点代码, 如果是同城配的路由标签,这里的值是根据同城配的设置映射出来的值,不同的配置结果会不一样,不能根据-符号切分(如:上海同城配, 可能是:集散点-目的地网点-接驳点, 也有可能是目的地网代码-集散点-接驳点)
        /// </summary>
        public virtual string destRouteLabel { get; set; }

        /// <summary>
        /// 顺丰二维码
        /// </summary>
        public virtual string twoDimensionCode { get; set; }

        /// <summary>
        /// 顺丰 入港映射码 eg:S10
        /// </summary>
        public virtual string codingMapping { get; set; }

        /// <summary>
        /// 顺丰
        /// </summary>
        public virtual string codingMappingOut { get; set; }

        /// <summary>
        /// 顺丰
        /// </summary>
        public virtual string proName { get; set; }

        /// <summary>
        /// 顺丰 运单号类型1：母单 2 :子单 3 : 签回单
        /// </summary>
        public virtual string waybillType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string proCode { get; set; }

    }
}
