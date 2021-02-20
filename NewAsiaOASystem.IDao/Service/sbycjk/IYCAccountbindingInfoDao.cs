using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IYCAccountbindingInfoDao:IBaseDao<YCAccountbindingInfoView>
    {
        #region //根据帐号和OPENId查询绑定数据
        /// <summary>
        /// 根据帐号和OPENId查询绑定数据
        /// </summary>
        /// <param name="user">帐号</param>
        /// <param name="openId">openId</param>
        /// <returns></returns>
        YCAccountbindingInfoView GetAccbindingbyuserandopenId(string user, string openId);
        #endregion


        #region //通过openId查找绑定的帐号数据list
        /// <summary>
        /// 通过openId查找绑定的帐号数据list
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        IList<YCAccountbindingInfoView> GetacclistinfobyopenId(string openId);
        #endregion

        #region //通过openId统计绑定帐号的数量
        /// <summary>
        /// 通过openId统计绑定帐号的数量
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        decimal bdzhcountbyopenId(string openId);
        #endregion

        #region //通过Id 查找绑定的帐号数据
        /// <summary>
        /// 通过Id 查找绑定的帐号数据
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        YCAccountbindingInfoView GetbdzhinfobyId(string Id);
        #endregion
    }
}
