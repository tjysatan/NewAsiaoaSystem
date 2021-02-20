using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_ProfitSummaryinfoDao:IBaseDao<PP_ProfitSummaryinfoView>
    {
        //根据员工Id和汇总时间查找按月汇总的数据
        PP_ProfitSummaryinfoView GetprofitSinfobystafidanhztime(string stafid, string hztime);

        /// <summary>
        /// 和汇总时间查找安年汇总的数据
        /// </summary>
        /// <param name="stafid">员工Id</param>
        /// <param name="hztime">汇总时间</param>
        /// <returns></returns>
        PP_ProfitSummaryinfoView GetprofitSinfobystafidandhztimeY(string stafid, string hztime);

        /// <summary>
        /// 按月查询汇总盈利数据
        /// </summary>
        /// <param name="stafid"></param>
        /// <param name="hztime"></param>
        /// <returns></returns>
        IList<PP_ProfitSummaryinfoView> GetprofitSuminfobyhztimeM(string hztime);
    }
}
