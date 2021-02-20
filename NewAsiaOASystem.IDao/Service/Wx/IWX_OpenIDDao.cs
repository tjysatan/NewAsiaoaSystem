using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
  public interface   IWX_OpenIDDao:IBaseDao<WX_OpenIDView>
    {
        #region //根据OpenID 判断是否已经绑定
         IList<WX_OpenIDView>  GetCount_byOpenId(string OpenId,string type); 
        #endregion

       #region //检查帐号是否已经绑定其他微信号
         IList<WX_OpenIDView> GetCount_byP_Id(string P_Id,string type);
       #endregion

         #region //读取已经绑定的微信帐号
         string GetbdOpenId(); 
         #endregion

         #region //根据OpenId 判断是否是绑定用户
         bool IsnotBD(string Id);
         #endregion

         #region //内部绑定的 信息
         IList<WX_OpenIDView> GetNBopenid(); 
         #endregion
      
    }
}
