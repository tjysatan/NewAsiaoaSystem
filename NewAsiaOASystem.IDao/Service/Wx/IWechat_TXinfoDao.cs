using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWechat_TXinfoDao:IBaseDao<Wechat_TXinfoView>
    {
        #region //提现记录根据openID 查询
        IList<Wechat_TXinfoView> GetTxinfobyopenid(string openid); 
        #endregion

         
        #region //检测是否 存在未处理的 提现申请
        bool Jcwclsq(string openid); 
        #endregion
    }
}
