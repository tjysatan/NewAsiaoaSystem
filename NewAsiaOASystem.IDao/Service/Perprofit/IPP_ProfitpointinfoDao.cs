using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_ProfitpointinfoDao:IBaseDao<PP_ProfitpointinfoView>
    {
        /// <summary>
        /// 盈利业务分页数据
        /// </summary>
        /// <param name="rwname">业务名称</param>
        /// <param name="temname">团队名称</param>
        /// <returns></returns>
        PagerInfo<PP_ProfitpointinfoView> Getprofitpage(string rwname, string temname,string protype, SessionUser user);

        /// <summary>
        /// 根据团队Id 查找属于该团队的(个人/团队)收入项数据
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="protype">收入项的类别</protype>
        /// <returns></returns>
        IList<PP_ProfitpointinfoView> GetProfitinfobyteamId(string teamId,string protype);

        /// <summary>
        /// 根据团队Id查找属于该团队的（个人/团队）支出项数据
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="protype">支出项的类别</protype>
        /// <returns></returns>
        IList<PP_ProfitpointinfoView> GetProfitzhichuinfobyteamId(string teamId, string protype);

        /// <summary>
        /// 支出项分页数据
        /// </summary>
        /// <param name="rwname">业务名称</param>
        /// <param name="temname">团队名称</param>
        /// <returns></returns>
        PagerInfo<PP_ProfitpointinfoView> Getprrofitzhichupage(string rwname, string temname,string protype, SessionUser user);


        /// <summary>
        /// 个人收入和支出的项目的分页数据
        /// </summary>
        /// <param name="name">项目名称</param>
        /// <param name="stafId">个人（员工）Id</param>
        /// <param name="type">收入0或者支出1</param>
        /// <returns></returns>
        PagerInfo<PP_ProfitpointinfoView> GetgerenProfitpage(string name, string stafId, string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rwname">项目名称</param>
        /// <param name="type">收入项 0/支出项 1</param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<PP_ProfitpointinfoView> TTProfitshouruzhichupage(string rwname, string type, SessionUser user);

        /// <summary>
        /// 团队团体收入支出项目（不固定分配）分页数据
        /// </summary>
        /// <param name="rwname">项目名称</param>
        /// <param name="type">收入项 0/支出项 1</param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<PP_ProfitpointinfoView> TTProfitsrzctwepagelist(string rwname, string type, SessionUser user);
    }
}
