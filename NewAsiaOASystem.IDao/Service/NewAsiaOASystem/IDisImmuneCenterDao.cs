using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDisImmuneCenterDao : IBaseDao<DisImmuneCenterView> 
    {
        PagerInfo<DisImmuneCenterView> GetIcList(string Name, string ComId, SessionUser user);
        IList<DisImmuneCenter> NGetList_idData(string id);
        string AlbumDropdown(string CommId, SessionUser user);//设置下拉框
        DisImmuneCenter GetModelById(string id);
      //  string GetSheQu(SessionUser user);
    }
}
