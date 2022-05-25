using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_THinfoFXDao:IBaseDao<NQ_THinfoFXView>
    {
        #region //根据 流程ID 查找可退产品分析 信息
        IList<NQ_THinfoFXView> GetTHFCinfobyR_Id(string R_Id); 
        #endregion

        #region //维修分析列表
        PagerInfo<NQ_THinfoFXView> GetCinfoList(string khname, string CPName, string SC_PH, string BL_XX, string BL_Yy, string yqj_name, string yqj_xh, string xzstart, string xzend,
             string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId, SessionUser user);
        #endregion

        #region MyRegion
        /// <summary>
        /// 返退明细列表分页数据
        /// </summary>
        /// <param name="khname">客户名称</param>
        /// <param name="cpname">产品名称型号</param>
        /// <param name="SC_PH">生产批次</param>
        /// <param name="starttime">创建时间</param>
        /// <param name="enedtime"></param>
        /// <param name="wxstarttime">维修时间</param>
        /// <param name="wxendtime"></param>
        /// <param name="r_pId">产品类</param>
        /// <param name="Isdz">定责情况</param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<NQ_THinfoFXView> GetNewCinfoList(string khname, string cpname, string SC_PH, string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId, string Isdz, string dzstarttime, string dzendtime, SessionUser user);
        #endregion

        
        #region //退货明细导出数据(2022211)
        /// <summary>
        /// 退货明细导出数据(2022211)
        /// </summary>
        /// <param name="khname"></param>
        /// <param name="cpname"></param>
        /// <param name="SC_PH"></param>
        /// <param name="starttime"></param>
        /// <param name="enedtime"></param>
        /// <param name="wxstarttime"></param>
        /// <param name="wxendtime"></param>
        /// <param name="r_pId"></param>
        /// <param name="Isdz"></param>
        /// <returns></returns>
        IList<NQ_THinfoFXView> NewDCWXFXDATA(string khname, string cpname, string SC_PH, string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId, string Isdz, string dzstarttime, string dzendtime); 
        #endregion

        //按照产品分组查询
        List<Object> GetTHwxfxgroupcpId(string starttime, string enedtime);

                /// <summary>
        /// 按照坏掉的元器件分组查询
        /// </summary>
        /// <param name="starttime">维修时间</param>
        /// <param name="endtime">维修时间</param>
        /// <returns></returns>
        List<Object> GetTHwxfxgroupTH_YQJname(string starttime, string endtime);

           //根据产品Id查找返退的数量
        int GetCPsumbycpId(string startime, string endtime, string cp_Id);

             /// <summary>
        /// 根据元器件Id查找该元器件维修的数量
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="YqjId"></param>
        /// <returns></returns>
        int GetYQJsumbyYQJId(string starttime, string endtime, string YqjId);

        /// <summary>
        /// 根据返退货单Id和产品Id
        /// </summary>
        /// <param name="r_Id"></param>
        /// <param name="c_Id"></param>
        /// <returns></returns>
        IList<NQ_THinfoFXView> GetTHFXinfobyR_IdandCpId(string r_Id, string c_Id);

 
        #region //根据返退单Id 检测存在没有定责的产品的记录 
        /// <summary>
        ///  存在返回false 不存在返回true
        /// </summary>
        /// <param name="r_Id"></param>
        /// <returns></returns>
        bool JcTHFXweidingzebyr_Id(string r_Id); 
        #endregion


        /// <summary>
        /// 根据处理方式查找维修分析记录
        /// </summary>
        /// <param name="clfs">处理状态</param>
        /// <param name="r_Id">返退货单Id</param>
        /// <returns></returns>
        IList<NQ_THinfoFXView> GetNq_thinfofxbythbz(string clfs, string r_Id);

        //按照返退品不良原因分组
        /// <summary>
        /// 按照返退品不良原因分组
        /// </summary>
        /// <param name="starttime">维修时间</param>
        /// <param name="endtime">维修时间</param>
        /// <returns></returns>
        List<Object> GetTHwxfxgroupTH_BLXX(string starttime, string endtime);

        //根据不良原因查找维修数量
        /// <summary>
        /// 根据不良原因查找维修数量
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="blyyId"></param>
        /// <returns></returns>
        int GetBLyysumbyblyyId(string starttime, string endtime, string blyyId);

        #region //维修分析数据导出
        IList<NQ_THinfoFXView> DCWXFXDATA(string khname, string CPname, string sc_ph, string bl_xx, string bl_yy, string yqj_name, string yqj_xh, string xzstart,
string xzend, string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId, SessionUser user); 
        #endregion

        #region 根据反退货流程单Id 查询出返退明细的分页数据(维修的)
        /// <summary>
        /// 根据反退货流程单Id 查询出返退明细的分页数据
        /// </summary>
        /// <param name="Id">返退货Id</param>
        /// <returns></returns>
        PagerInfo<NQ_THinfoFXView> Getftmxinfopage(string Id, string CPName, string Iscl);
        #endregion



        #region 根据反退货流程单Id 查询出返退明细的分页数据 （定责的）
        /// <summary>
        /// 根据反退货流程单Id 查询出返退明细的分页数据 （定责的）
        /// </summary>
        /// <param name="Id">返退货单Id</param>
        /// <param name="CPname">产品名称</param>
        /// <param name="Isdz">是否定责</param>
        /// <returns></returns>
        PagerInfo<NQ_THinfoFXView> Getftdzmxinfopage(string Id, string CPname, string Isdz);
        #endregion


        #region //根据返退Id 检测未处理的数据
        bool JcTHFXweichulibyr_Id(string r_id);
        #endregion

        #region //根据订单Id和需要合计的字段类型合计金额
        /// <summary>
        /// 根据订单Id和需要合计的字段类型合计金额
        /// </summary>
        /// <param name="RId">返退货订单Id</param>
        /// <param name="type"> TH_yqjjg 元器件金额 TH_RGF 人工费 TH_BCF 包材费</param>
        /// <returns></returns>
        decimal GetTotalcostbyRIdcosttype(string RId, string type);
        #endregion

        
        #region //根据产品Id分组to
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strattime"></param>
        /// <param name="enedtime"></param>
        /// <returns></returns>
        List<Object> GetStatisticsgroupcpid(string strattime, string enedtime); 
        #endregion

        #region //查出产品名称为空的退货明细
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<NQ_THinfoFXView> TemporaryGetcpnameisnulldata(); 
        #endregion
    }
}
