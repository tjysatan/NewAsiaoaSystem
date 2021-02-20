using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_TeaminfoDao:IBaseDao<PP_TeaminfoView>
    {
        
        /// <summary>
        /// 团队信息分析数据
        /// </summary>
        /// <param name="Name">团队名称</param>
        /// <param name="zrname">责任人</param>
        /// <param name="tel">电信</param>
        /// <returns></returns>
        PagerInfo<PP_TeaminfoView> GetTeamList(string Name, string zrname, string tel);

                /// <summary>
        /// 根据团队Id 查找团队名称
        /// </summary>
        /// <param name="Id">团队Id</param>
        /// <returns></returns>
        PP_TeaminfoView Getpp_teambyId(string Id);

        /// <summary>
        /// 根据管理员Id 查找团队信息
        /// </summary>
        /// <param name="Id">当前登录用户的Id</param>
        /// <returns></returns>
        PP_TeaminfoView Getpp_teambyguanliId(string Id);

 
    }
}
