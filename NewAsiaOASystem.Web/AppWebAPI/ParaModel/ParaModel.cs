using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class UserLogin
    {
        public string username { get; set; }
        public string userpass { get; set; }
    }

    public class UserUpdatePassword
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class Authorizationuser
    {
        public string username { get; set; }

        public string userId { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public string authTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string expiresAt { get; set; }


    }

    public class Result
    {
        public string code { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }

    #region //同步K3 生产任务单的实体
    public class ICMOSysmodel
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string FCustName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string FNumber { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public int FQty { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string FBatchNo { get; set; }

        /// <summary>
        /// BOM编号
        /// </summary>
        public string FBOMNumber { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        public string FDeptNumber { get; set; }

        /// <summary>
        /// 生产类型名称  普通订单/返工
        /// </summary>
        public string FWorktypeName { get; set; }

        /// <summary>
        /// 计划开工日期
        /// </summary>
        public DateTime? FPlanCommitDate { get; set; }

        /// <summary>
        /// 计划完工日期
        /// </summary>
        public DateTime? FPlanFinishDate { get; set; }

    }
    #endregion

    #region //普实 销售订单实体
 

    public class pushsoftorder { 
        /// <summary>
        /// 客户编码
        /// </summary>
        public virtual string CrdID { get; set; }

        /// <summary>
        /// 新亚订单号
        /// </summary>
        public virtual string NumAtCrd { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public virtual DateTime?  DocDate { get; set; }

        /// <summary>
        /// 交货期限
        /// </summary>
        public virtual string Z_JHQX { get; set; }

        /// <summary>
        /// 交货地点
        /// </summary>
        public virtual string Z_JHDD { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public virtual string Z_YSFS { get; set; }

        public virtual int? DocKind { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public virtual string Z_OpUserSign { get; set; }

        /// <summary>
        /// 订单主表的备注
        /// </summary>
        public virtual string Notes { get; set; }

        public virtual IList<pushsoftorderDetails> Details { get; set; }
    }

    public class pushsoftorderDetails {

        /// <summary>
        /// 物料编码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? Qty { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public virtual decimal? TaxAfPriceFC { get; set; }

        /// <summary>
        /// 需求日期
        /// </summary>

        public virtual DateTime? ReqDate { get; set; }

        /// <summary>
        /// 行备注
        /// </summary>
        public virtual string FreeTxt { get; set; }
    }
    #endregion

    #region //普实 同步非标产品的实体
    public class Ps_fbcpmodel
    {
        /// <summary>
        /// 新物料编码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 规格
        /// </summary>

        public virtual string ItmSpec { get; set; }

        /// <summary>
        /// 物料编码前三段
        /// </summary>

        public virtual string Z_ItmID { get; set; }

        /// <summary>
        /// 硬件单价
        /// </summary>

        public virtual string Z_Price { get; set; }
    }
    #endregion

    #region //普实 非标产品BOM实体
    public class Ps_Bommodel {
        /// <summary>
        /// Bom编码
        /// </summary>
        public virtual string BomID { get; set; }

        /// <summary>
        /// Bom名称
        /// </summary>
        public virtual string BomName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public virtual string VerNum { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public virtual string RouID { get; set; }

        /// <summary>
        /// 工艺表数据
        /// </summary>
        public virtual IList<Proceduresmodel> Procedures { get; set; }

        /// <summary>
        /// 用料表数据
        /// </summary>
        public virtual IList<Itemsmodel> Items { get; set; }
    }

    #region //工艺表数据传值
    public class Proceduresmodel
    {
        /// <summary>
        /// 行号
        /// </summary>
        public virtual string LineNum { get; set; }

        /// <summary>
        /// 工序编号
        /// </summary>
        public virtual string PrcID { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public virtual string PrcName { get; set; }

        /// <summary>
        /// 工作中心编码
        /// </summary>
        public virtual string WcnID { get; set; }

        /// <summary>
        /// 工作中心名称
        /// </summary>
        public virtual string WcnName { get; set; }

        /// <summary>
        /// 前置行
        /// </summary>
        public virtual string PreLineNum { get; set; }
    }
    #endregion

    #region //用料表数据传值
    public class Itemsmodel
    {
        /// <summary>
        /// 用料编码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 净需求
        /// </summary>

        public virtual decimal? NetQty { get; set; }
        /// <summary>
        /// 损耗率
        /// </summary>

        public virtual string ScrapRate { get; set; }

        /// <summary>
        /// 行类别
        /// </summary>

        public virtual string LineType { get; set; }
        /// <summary>
        /// 工序行
        /// </summary>

        public virtual string OperationLine { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public virtual string Position { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string FreeTxt { get; set; }
    }
    #endregion
    #endregion

    #region //普实 接口返回实体
    public class pushsoftErrmodel
    {
        /// <summary>
        /// 异常说明
        /// </summary>
        public virtual string ErrMsg { get; set; }

        /// <summary>
        /// 异常编码，0成功，1为未编码的异常，其它为已编码的异常
        /// </summary>
        public virtual string ErrCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual psErrData Data { get; set; }
    }


    public class psErrData {
        /// <summary>
        /// 回写单据的单据号
        /// </summary>
        public virtual string DocEntry { get; set; }
        /// <summary>
        /// 回写单据的凭证编号
        /// </summary>

        public virtual string DocNum { get; set; }
    }
    #endregion

    #region //
    //public class feibiaogongx
    //{
    //    底板装配,
    //}
    #endregion

    #region //普实 BOM细表更新参数实体
    public class FInstaerMDBomA
    {
        /// <summary>
        /// 主表编号
        /// </summary>
        public virtual string DocEntry { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public virtual string LineNum { get; set; }

        /// <summary>
        /// 物料代理
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 需求量
        /// </summary>
        public virtual decimal? NetQty { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 行类别
        /// </summary>
        public virtual string LineType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string FreeTxt { get; set; }
    }
    #endregion

    #region //普实 暂收送检的明细实体
    public class pszanshousjmodel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual string DocEntry { get; set; }

        /// <summary>
        /// 凭证日期
        /// </summary>
        public virtual string DocDate { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public virtual string CrdID { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string CrdName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string ItmName { get; set; }

        /// <summary>
        /// 物料型号
        /// </summary>
        public virtual string ItmSpec { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual string Qty { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual string TaxAfPriceFC { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual string TaxAfLineSumFC { get; set; }
    }
    #endregion

    #region //普实 发货拣配单
    public class psJPordermodel
    {
        /// <summary>
        /// 拣配单 单号
        /// </summary>
        public virtual string DocEntry { get; set; }

        /// <summary>
        /// 单据时间
        /// </summary>
        public virtual string OpDate { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string CrdID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CrdName { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public virtual string OpUserName { get; set; }
    }
    #endregion

    #region //普实发货拣配单明细
    public class psJPordermodelA
    {

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string ItmID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string ItmName { get; set; }

        /// <summary>
        /// 物料型号
        /// </summary>
        public virtual string ItmSpec { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public virtual string Qty { get; set; }
    }
    #endregion


    #region //报价系统接口返回参数
    //接口返回的参数
    public class BJmodel
    {

        /// <summary>
        /// 接口调用状态 ok: 成功、error: 失败
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }

        public string msg { get; set; }
    }
    #endregion

    #region //报价系统BOM返回的需要的实体参数
    public class BJ_BOMmodel
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public virtual string MATERIAL_NUM { get; set; }

        public virtual string PARTS_TYPE { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string PARTS_NAME { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual string  P_COUNT { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }

        /// <summary>
        /// 硬件成本
        /// </summary>
        public virtual string UNIT_PRICE { get; set; }
    }
    #endregion

    #region //报价系统报价单价格清单返回的需要的实体参数
    public class BJ_QDModel
    {
        public virtual string PRICE { get; set; }

        public virtual string DIS_PRICE { get; set; }
    }
    #endregion

    #region //工位机平板 订单同步接口返回的实体参数
    public class GWJ_ODMode
    {
        /// <summary>
        ///  1是成功其他都异常
        /// </summary>
        public virtual string code { get; set; }
    }
    #endregion

    #region //远程发送告警的model
    public class YCNoticeModel
    {
        public virtual string Id { get; set; }

        public virtual string Username { get; set; }

        public virtual string SId { get; set; }

        public virtual string jkdName { get; set; }

        public virtual string NoticeMsg { get; set; }

        public virtual string type { get; set; }
    }
    #endregion

    #region //普实物料工序工时
    public class Ps_wlGXGSModel
    {
        public virtual string ItmID { get; set; }

        public virtual string Z_DQZZDBGS { get; set; }

        public virtual string Z_DQZZKZXGS { get; set; }

        public virtual string Z_DQZZZHLGS { get; set; }

        public virtual string Z_DQZZMBZXGS { get; set; }

        public virtual string Z_DQZZMBJXGS { get; set; }

        /// <summary>
        /// 内部售价
        /// </summary>
        public virtual string Z_NBPrice { get; set; }
    }
    #endregion

    #region //首页统计实体

    #region //各类订单总数据和当月总数据model
    public class DDDataMode
    {
        /// <summary>
        /// 订单的总数量【或者当年总数量】
        /// </summary>
        public virtual string TotalSum { get; set; }

        /// <summary>
        /// 订单的当月数量
        /// </summary>
        public virtual string TotaSameMonthSum { get; set; }
    }
    #endregion
    #endregion
}