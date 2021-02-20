using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IYCACCandSIDInfoDao:IBaseDao<YCACCandSIDInfoView>
    {
        #region //根据帐号绑定的Id 和sid
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameId"></param>
        /// <param name="SID"></param>
        /// <returns></returns>
        YCACCandSIDInfoView GetSIDbynameIdandsid(string nameId, string SID);
        #endregion

        #region //根据远程帐号绑定的Id查找要发送和不要发送通知的SID
        /// <summary>
        /// 根据远程帐号绑定的Id查找要发送和不要发送通知的SID
        /// </summary>
        /// <param name="nameId">远程帐号绑定微信的唯一值</param>
        /// <param name="type">是否发送 0 发送 1 不发送</param>
        /// <returns></returns>
        IList<YCACCandSIDInfoView> GetSIDlistIsfsbyzhId(string nameId, string type);
        #endregion

        #region //通过SID查找绑定帐号的信息
        /// <summary>
        /// //通过SID查找绑定帐号的信息
        /// </summary>
        /// <param name="SID">SID</param>
        /// <returns></returns>
        IList<YCACCandSIDInfoView> GetSIDBDZHlistbySID(string SID);
        #endregion

        #region //根据绑定的帐号的Id 查找监控点数据
        /// <summary>
        /// 根据绑定的帐号的Id 查找监控点数据
        /// </summary>
        /// <param name="nameId">绑定帐号Id</param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<YCACCandSIDInfoView> GetALLSIDlistbyzhId(string nameId);
        #endregion
    }
}
