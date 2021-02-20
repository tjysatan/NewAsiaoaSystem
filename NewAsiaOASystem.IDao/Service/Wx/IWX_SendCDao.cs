using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
  public interface   IWX_SendCDao:IBaseDao<WX_SendCView>
    {

        #region //统计当月发送的次数
        int GetSM(int type); 
        #endregion
    }
}
