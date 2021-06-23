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
    }
    #endregion
}