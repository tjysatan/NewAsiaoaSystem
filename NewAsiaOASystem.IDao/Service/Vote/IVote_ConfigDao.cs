using NewAsiaOASystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.IDao
{
    public interface   IVote_ConfigDao:IBaseDao<Vote_ConfigView>
    {
        #region //查找第一个
        Vote_ConfigView NGetone(); 
        #endregion
    }
}
