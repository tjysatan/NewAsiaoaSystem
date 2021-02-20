using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWx_FTUserbdopenIdinfoDao:IBaseDao<Wx_FTUserbdopenIdinfoView>
    {
        #region //根据OpenID 判断是否已经绑定
        IList<Wx_FTUserbdopenIdinfoView> GetCount_byOpenId(string OpenId); 
        #endregion

        #region //根据OpenID 判断是否已经绑定
        IList<Wx_FTUserbdopenIdinfoView> GetCount_byUserId(string UserId); 
        #endregion

        #region //根据部门类型查找绑定的微信信息
        IList<Wx_FTUserbdopenIdinfoView> GetwxftbmopenIdbybmtype(string bmtype); 
        #endregion

        #region //根据绑定的用户Id查找绑定的微信信息
        /// <summary>
        /// 根据绑定的用户Id查找绑定的微信信息
        /// </summary>
        /// <param name="zhId">账户Id</param>
        /// <returns></returns>
        IList<Wx_FTUserbdopenIdinfoView> GetwxopenIdbybdzhuserId(string zhId); 
        #endregion


        #region //分页数据
        /// <summary>
        /// 微信绑定的的分页数据
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="type">部门类型 1 客服主管 2 电控维修 3 温控维修 4 配件维修 5 品保经理 6 电气工程师 7 生产主管 8 客服  9 采购 10 箱体确认 11 其他物料 12 仓库</param>
        /// <returns></returns>
        PagerInfo<Wx_FTUserbdopenIdinfoView> GetwxbdinfoPagerList(string user, string type); 
        #endregion
    }
}
