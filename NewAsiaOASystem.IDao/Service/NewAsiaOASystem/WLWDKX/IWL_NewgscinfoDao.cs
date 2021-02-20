using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_NewgscinfoDao:IBaseDao<WL_NewgscinfoView>
    {
         /// <summary>
        /// 查询最新更新的工程商对应的sid 的信息
        /// </summary>
        /// <returns></returns>
        WL_NewgscinfoView GetNewnewgcsinfo();

        /// <summary>
        /// 根据远程Ids查询工程商对应sid 的信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        WL_NewgscinfoView GetnewgcsinfobyIds(string Ids);

        /// <summary>
        /// 根据SID查找工程商信息
        /// </summary>
        /// <param name="sid">SID</param>
        /// <returns></returns>
        WL_NewgscinfoView Getnewgscinfobysid(string sid);
    }
}
