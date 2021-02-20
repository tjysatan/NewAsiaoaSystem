using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_DDinfoDao:IBaseDao<DKX_DDinfoView>
    {
        #region //电控箱生产单数量分页数据
        /// <summary>
        /// 电控箱生产单数量分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="start">是否禁用</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> Getdkxtypelistpage(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs,string kfId, SessionUser user);
        #endregion


        #region //电控箱生产列表分页数据（工程师）
        /// <summary>
        /// 电控箱生产列表分页数据（工程师）
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="KBomNo">关联号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start">是否禁用</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> GetdkxlistpageGCS(string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz, SessionUser user); 
        #endregion

        #region //电控箱生产列表分页数据（电气资料审核）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="start"></param>
        /// <param name="GCSId"></param>
        /// <param name="DHtype"></param>
        /// <param name="dqshjd"></param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> Getdkxlistpagedqsh(string DD_Bianhao, string DD_Type, string KHname, string startctime, string endctiome,
         string DD_ZT, string start, string GCSId, string DHtype, string dqshjd, SessionUser user); 
        #endregion

        #region //电控箱生产列表分页数据（仓库发货）注：只显示待发货和完成发货状态的数据
        /// <summary>
        /// 电控箱生产列表分页数据（仓库发货）注：只显示待发货和完成发货状态的数据
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="BJno"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="Lxname"></param>
        /// <param name="Tel"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start"></param>
        /// <param name="DHtype"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> GetDKXDDlistpageCK(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string DHtype,string YQtype, SessionUser user); 
        #endregion

        #region //电控箱生产列表分页数据（生产）
        /// <summary>
        /// 电控箱生产列表分页数据（生产）
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start">是否禁用</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> GetdkxlistpageSC(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start,string DHtype,string YQtype, SessionUser user); 
        #endregion

         
        #region //电控箱生产列表分页数据（品保）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="BJno"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="Lxname"></param>
        /// <param name="Tel"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start"></param>
        /// <param name="DHtype"></param>
        /// <param name="YQtype"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> GetdkxlistpagePb(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
             string startctime, string endctiome, string start, string DHtype, string YQtype, SessionUser user); 
        #endregion

        #region //返回当天的电控箱的下单数量
        /// <summary>
        /// 返回当天的电控箱的下单数量
        /// </summary>
        /// <returns></returns>
        int GetDKXcount(); 
        #endregion

	    #region //电控箱生产单数量分页数据 huizong
        /// <summary>
        /// 电控箱生产单数量分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="start">是否禁用</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> Getdkxhzlistpage(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz,string gnjs,
             string wcstarttime, string wcendtime);
	#endregion

        #region //显存订单返回Id
        string InsertID(DKX_DDinfoView modelView); 
        #endregion

        #region //操作逾期数据
        /// <summary>
        /// 操作逾期数据
        /// </summary>
        /// <param name="type">逾期类型  0 制图接单逾期 1 制图逾期 2 箱体库存确认逾期 3 其他物料库存确认逾期 4 生产接单逾期 5 生产逾期 6 发货逾期</param>
        /// <returns></returns>
        IList<DKX_DDinfoView> CZYQDATAList(string type); 
        #endregion

        #region //按照时间顺序查询全部的订单
        /// <summary>
        /// 按照时间顺序查询全部的订单
        /// </summary>
        /// <returns></returns>
        IList<DKX_DDinfoView> GetallzcDDlist(string DD_Bianhao, string DHtype, string ddtype, string gcsname, string khname, string xdstrattime, string xdendtime, string ysstrattime, string ysendtime);
        #endregion

        #region //生产订单的统计情况
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="KHname"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="Isyq">是否逾期分俩种 0 进行中逾期  1 完成的订单 逾期</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> Getscddinfopagerlist(string DD_Bianhao, string KHname, string DD_ZT, string Isyq, string starttime, string endtime);


        #region //下单总数量
        /// <summary>
        /// 下单总数量
        /// </summary>
        /// <returns></returns>
        decimal Getzxdsum(string starttime, string endtime);
        #endregion

        #region //各个状态下的数量
        /// <summary>
        /// 各个状态下的数量
        /// </summary>
        /// <param name="ddzt">状态</param>
        /// <returns></returns>
        decimal GetZTdsumbyddzt(string ddzt, string starttime, string endtime);
        #endregion

        #region //在途数量
        /// <summary>
        /// 在途数量
        /// </summary>
        /// <returns></returns>
        decimal Getzaitusum(string starttime, string endtime);
        #endregion

        #region //逾期完成和正常完成数量
        /// <summary>
        /// 逾期完成和正常完成数量
        /// </summary>
        /// <param name="type">0 逾期完成 1 正常完成</param>
        /// <returns></returns>
        decimal Getyqwcandzcwcsum(string type, string starttime, string endtime);
        #endregion

        #region //导出数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dd_no"></param>
        /// <param name="khname"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="Isyq"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        IList<DKX_DDinfoView> GetddinfoExportJson(string dd_no, string khname, string DD_ZT, string Isyq, string starttime, string endtime); 
        #endregion
        #endregion

        
        #region //根据产品名称 功率单位查询时间内已经完成的订单产品数量
        /// <summary>
        /// 功率单位查询时间内已经完成的订单产品数量
        /// </summary>
        /// <param name="cpname">产品名称</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="starttime">完成时间</param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        int GetSCCPSUM(string cpname, string gl, string dw, string starttime, string endtime);
        #endregion

        #region //查询需要工程师助力检查的订单数据
        /// <summary>
        /// 查询需要工程师助力检查的订单数据
        /// </summary>
        /// <param name="dragstart">检查助力状态 0 需要检查 1 待检查 2 完成检查 3 驳回检查</param>
        /// <param name="user">当前登陆的账号</param>
        /// <returns></returns>
        int Getdragsumbyzt(string dragstart, SessionUser user);
        #endregion

        #region //查询需要工程师助力检查的订单分页数据
        /// <summary>
        /// 查询需要工程师助力检查的订单分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="user">当前登陆的账号</param>
        /// <returns></returns>
        PagerInfo<DKX_DDinfoView> GetdragddinfoPagerlist(string DD_Bianhao, SessionUser user);
        #endregion

        
        #region //导出订单数据
        IList<DKX_DDinfoView> GetExcelExportDDdata(string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz, SessionUser user);
        #endregion

         
        #region //查询2020和2021年有效订单数据
        IList<DKX_DDinfoView> GetorderdataToyear(); 
        #endregion
    }
}
