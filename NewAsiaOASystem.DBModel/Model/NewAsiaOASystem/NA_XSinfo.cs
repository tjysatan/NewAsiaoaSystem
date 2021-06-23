using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 销售订单实体
    /// author：tjy_satan
    /// </summary>
    public class NA_XSinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// 销售类型 0 常规订单 1 电商订单
        /// </summary>
        public virtual int Xs_type
        {
            get;
            set;
        }

        /// <summary>
        /// 商城订单的ID
        /// </summary>
        public virtual string Sc_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 销售时间
        /// </summary>
        public virtual DateTime? Xs_datetime
        {
            get;
            set;
        }

        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual string KhId
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Khname
        {
            get;
            set;
        }

        /// <summary>
        /// 收件人信息Id
        /// </summary>
        public virtual string AddresseeId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售金额
        /// </summary>
        public virtual decimal? Xs_je
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态 0 未付款 1 已付款 2 配货中 3 已发货 4已签收 5已取消
        /// </summary>
        public virtual int Ddzt
        {
            get;
            set;
        }

        /// <summary>
        /// 付款方式 0 支付宝 1线下付款
        /// </summary>
        public virtual int? Fk_type
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime C_datetime
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_Name
        {
            get;
            set;
        }

        /// <summary>
        /// 状态 0 启用 1禁用
        /// </summary>
        public virtual int States
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu
        {
            get;
            set;
        }

        /// <summary>
        /// 货运状态
        /// </summary>
        public virtual int ShippingStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        public virtual int PaymentStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 是否需要扫描 0 不需要 1需要
        /// </summary>
        public virtual int Issm
        {
            get;
            set;
        }

        /// <summary>
        /// 扫码状态 0 未扫码 1 已扫码
        /// </summary>
        public virtual int SM_ZT
        {
            get;
            set;
        }

        /// <summary>
        /// 订单的扫码的数量
        /// </summary>
        public virtual decimal? S_Number
        {
            get;
            set;
        }

        /// <summary>
        /// 订单产品总数量
        /// </summary>
        public virtual decimal? CP_Number
        {
            get;
            set;
        }

        /// <summary>
        /// pushsoft 单据的单据号
        /// </summary>
        public virtual string Ps_DocEntry { get; set; }

        /// <summary>
        /// pushsoft 单据的凭证编号
        /// </summary>
        public virtual string Ps_DocNu { get; set; }


    }
}
