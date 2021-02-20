using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_CPInfoDao:IBaseDao<WL_CPInfoView>
    {
        #region //分页数据
        PagerInfo<WL_CPInfoView> GetWLcpinfoList(string Name, SessionUser user); 
        #endregion
    }
}
