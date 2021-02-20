using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_XFOrderinfoDao : IBaseDao<WL_XFOrderinfoView>
    {
        /// <summary>
        /// 查询最新更新的订单号的信息
        /// </summary>
        /// <returns></returns>
        WL_XFOrderinfoView GetNewOderinfo();

        /// <summary>
        /// 根据远程Ids查询订单信息
        /// </summary>
        /// <param name="Ids">远程Ids</param>
        /// <returns></returns>
        WL_XFOrderinfoView GetxforderinfobyIds(string Ids);

         /// <summary>
        /// 续费订单的分页数据
        /// </summary>
        /// <param name="SID">sid</param>
        /// <param name="XDdatetime">下单时间</param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<WL_XFOrderinfoView> GetWL_xforderinfoList(string SID, string XDstartdatetime, string XDenddatetime, SessionUser user);

        #region //根据经销商uId 返回续费订单的数量
        int jxqGetordersumbyuid(string uid); 
        #endregion

        
        #region //根据sid查找订单是否存在
        /// <summary>
        /// 根据sid查找订单是否存在
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        bool GetxforderIsczbysid(string sid); 
        #endregion
    }
}
