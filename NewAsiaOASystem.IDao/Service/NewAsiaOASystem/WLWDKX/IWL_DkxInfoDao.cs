using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_DkxInfoDao:IBaseDao<WL_DkxInfoView>
    {
        PagerInfo<WL_DkxInfoView> GetWLdkxinfoList(string Name, string Name2, string Is_sx, string Is_xs, string wl_zt, string wl_sid, string Is_qf, string startdate, string enddate, string sxstartdate, string sxenddate, SessionUser user);

        #region //根据SID码 查找电控箱信息
        WL_DkxInfoView GetDkxinfo(string Sid); 
        #endregion

        #region //根据订单明细Id 查找出货数量
        int GetChcunotbyorderId(string OrdermxId);
        #endregion

       
        #region //根据ERP拣配单单号查找出SID扫码数量
        int GetGetChcunotbyjpno(string jpno); 
        #endregion

        #region //根据订单Id 查询SID
        IList<WL_DkxInfoView> GetSIDbyOrderId(string orderId); 
        #endregion

        #region //省份销售数量查询
        int GetdkxxscountbySFIdandsj(string sfId, string month,string Enddate,int type);
        #endregion

        #region //查询经销商该时间内 买了多少套以及上线了多少套
        int GetdkxxscounybyKHIdandsj(string KhId, string month, string endtime, int type); 
        #endregion


        IList<WL_DkxInfoView> GetSIDbyKhId(string UId, int SetFrist, int SetMax);

         /// <summary>
        /// 统计上线数量
        /// </summary>
        /// <param name="UId"></param>
        /// <returns></returns>
        int GetcountdkxbyUId(string UId);


        PagerInfo<WL_DkxInfoView> GetWLdkxinfolistbyRVgcs(string Name, string Name2, string RVtype, string isrv, string SId, string sxstartdate, string sxenddate);

        #region //微信统计
        /// <summary>
        /// 经销商全部的电控箱
        /// </summary>
        /// <param name="OpenID">微信openID</param>
        /// <returns></returns>
        int WXGetcountdkxbyCusId(string OpenID); 


         /// <summary>
        /// 经销商本月上线数量
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前时间年月</param>
        /// <returns></returns>
        int WXdkxsxcountbyCusIdM(string OpenID, string datetime, string Enddatetime);


         /// <summary>
        /// 全部上线的数量
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <returns></returns>
        int WXdkxallsxbyCusId(string OpenID);


         /// <summary>
        /// 本月收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前年月</param>
        /// <returns></returns>
        int WXdkxjfbyCusIdM(string OpenID, string datetime, string Enddatetime);


        
        /// <summary>
        /// 累计收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <returns></returns>
        int WXdkxcountsybyCusId(string OpenID);

        /// <summary>
        /// 次月预计收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前年月</param>
        /// <returns></returns>
        int WXdkxjfbyCusIdCM(string OpenID, string datetime);

        #endregion

        #region //查找全部未上线已经出售的电控箱
        /// <summary>
        /// 查找全部未上线已经出售的电控箱
        /// </summary>
        /// <returns></returns>
        IList<WL_DkxInfoView> Getscwsxinfo(); 
        #endregion


        
        #region //查找已经扫码出售的电控箱数据
        /// <summary>
        /// 查找已经扫码出售的电控箱数据
        /// </summary>
        /// <returns></returns>
        IList<WL_DkxInfoView> GetYSsidinfo(); 
        #endregion

        #region //物联网电控箱销售上线情况统计
        //各个省份的销售上线情况
        string DkxsaleOnlineJson(string month, string Enddate);

        //各个经销商的销售上线情况
        string CusDkxsaleOnlineJson(string month, string Enddate);

        #endregion

        #region //查找最新的一条的销售订单
        WL_DkxInfoView Getwldkxinfonewdate(); 
        #endregion

        #region //根据sid查询sid信息
		/// <summary>
        /// SID
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        WL_DkxInfoView GetDkxinfobySID(string SID);
	    #endregion

        /// <summary>
        /// 查询没有上线的数据根据
        /// </summary>
        /// <param name="startsum">起始条数</param>
        /// <param name="endsum">条数</param>
        /// <returns></returns>
        IList<WL_DkxInfoView> Getwl_dkxbysum(string startsum, string endsum);

        #region //查询已经上线没有监控点未空的
        IList<WL_DkxInfoView> Getwldkxinfosxjkdnull(); 
        #endregion

        #region //根据经销商Id 查找经销商名下的sid数量
        int jxqGetcountYGdkxsumbyjxsId(string jxsId); 
        #endregion

        #region //根据经销商Id 查找已经上线的sid数量
        int jxqGetcountSXdkxsumbyjxsId(string jxsId); 
        #endregion

    
        #region //根据经销商Id 查找已经SID数量
        /// <summary>
        /// 根据经销商Id 查找已经SID数量
        /// </summary>
        /// <param name="jxsId">经销商Id</param>
        /// <returns></returns>
        IList<WL_DkxInfoView> GetSIDlistbyjxsId(string jxsId);
        #endregion

        #region //根据ERP拣配单单号查询出SID明细
        /// <summary>
        /// sid 的明细
        /// </summary>
        /// <param name="jpno">拣配单单号</param>
        /// <returns></returns>
        IList<WL_DkxInfoView> GetChSIDinfotbyjpno(string jpno); 
        #endregion
    }
}
