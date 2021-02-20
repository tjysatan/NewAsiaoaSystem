using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public  interface INQ_GysInfoDao:IBaseDao<NQ_GysInfoView>
    {
        PagerInfo<NQ_GysInfoView> GetCinfoList(string Name, SessionUser user);

        IList<NQ_GysInfoView> GetlistCust();

        #region //根据供应商代码查找供应商信息
        string Getgysmodelbydm(string dm); 
        #endregion

        #region //根据供应商代码查找供应商信息
        NQ_GysInfoView Getmodelbydm(string dm); 
        #endregion

        #region //根据供应商名称查找供应商信息
        NQ_GysInfoView Getmodelbygysname(string name); 
        #endregion
    }
}
