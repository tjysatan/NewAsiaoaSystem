using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_ShouruandzhichuDetailsinfoDao:IBaseDao<PP_ShouruandzhichuDetailsinfoView>
    {
        /// <summary>
        /// 根据个人（员工）Id查找收入和支持明细
        /// </summary>
        /// <param name="staffId">个人（员工）Id</param>
        /// <param name="type">收入和支出类别</param>
        /// <returns></returns>
        //IList<PP_ShouruandzhichuDetailsinfoView> GetshouruhichuMxbystaffId(string staffId, string type,string jl_time);

        IList<PP_ShouruandzhichuDetailsinfoView> GetshouruhichuMxbystaffId(string staffId, string type, string jltime);

        bool JcshouruzhichuMxbystafidandproIdandwctime(string staffId, string ProfitId, string wctime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="ProfitId"></param>
        /// <param name="wctime"></param>
        /// <returns>返回 明细实体</returns>
        PP_ShouruandzhichuDetailsinfoView Getshouruzhichuinfobywctime(string staffId, string ProfitId, string wctime);

                /// <summary>
        /// 个人收入支出明细分页数据
        /// </summary>
        /// <param name="Name">个人员工名单</param>
        /// <param name="xmName">项目名单</param>
        /// <param name="type">收入和支出类别</param>
        /// <param name="startc_time">记录时间</param>
        /// <param name="endc_time">记录时间</param>
        /// <param name="startwctime">完成时间</param>
        /// <param name="endwctime">完成时间</param>
        /// <param name="user">当前登录帐号</param>
        /// <returns>分页数据</returns>
        PagerInfo<PP_ShouruandzhichuDetailsinfoView> Getshouruzhichupage(string Name, string xmName, string type, string startc_time, string endc_time,
            string startwctime, string endwctime, SessionUser user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stafId"></param>
        /// <returns></returns>
        decimal Getsumshourubystafid(string starttime, string endtime, string stafId);

        /// <summary>
        /// 根据时间和员工Id计算总支出
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stafId"></param>
        /// <returns></returns>
        decimal GetsumzhichubystafId(string starttime, string endtime, string stafId);


        /// <summary>
        /// 根据个人（员工）Id计算累计收入
        /// </summary>
        /// <param name="stafId"></param>
        /// <returns></returns>
       decimal GetleijishourubystafId(string stafId);

        /// <summary>
        /// 根据个人（员工）Id计算累计支出
        /// </summary>
        /// <param name="stafId"></param>
        /// <returns></returns>
       decimal GetleijizhichubystafId(string stafId);


        /// <summary>
        /// 查询生产团队的当天的的生产收入（管理费用除外）
        /// </summary>
        /// <param name="wctime">完成时间</param>
        /// <returns></returns>
       decimal Getscteamshourusumbysj(string wctime, string stafid);

        /// <summary>
        /// 查找生产部管理人员当天的管理费用
        /// </summary>
        /// <param name="wctime"></param>
        /// <param name="stafid"></param>
        /// <returns></returns>
        PP_ShouruandzhichuDetailsinfoView Getshouruguanlifeiyongbywctimeandstafid(string wctime, string stafid);

        /// <summary>
        /// 团体项目收支明细Id
        /// </summary>
        /// <param name="TTszmxId"></param>
        /// <returns></returns>
        IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTszmxdatabyttmxId(string TTszmxId);

        /// <summary>
        /// 根据完成时间和员工Id查询收支数据
        /// </summary>
        /// <param name="startwctime">开始时间</param>
        /// <param name="endwctime">结束时间</param>
        /// <param name="stafId">员工Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        decimal GethtgrszbystafId(string startwctime, string endwctime, string stafId, string type);


        /// <summary>
        /// 获取团体项目收入明细Id（不固定分配）查询分配个人的收入的明细
        /// </summary>
        /// <param name="TTsrmxId">团队收入明细Id</param>
        /// <returns></returns>
        IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTtwesrmxbyttmxId(string TTsrmxId);


        /// <summary>
        /// 根据项目收入明细Id、完成时间、员工Id 查找是否存在该收入明细
        /// </summary>
        /// <param name="TTsrmxId"></param>
        /// <param name="wctime"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        PP_ShouruandzhichuDetailsinfoView GetgerenttsrtwebyttsrmxIdandwctimeandstaffId(string TTsrmxId, string wctime, string staffId);

        /// <summary>
        /// 更加团体收入项明细Id 查找分配明细
        /// </summary>
        /// <param name="TTsrmxId"></param>
        /// <returns></returns>
        IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTsrtwefpmxbyttmxId(string TTsrmxId);
 
    }
}
