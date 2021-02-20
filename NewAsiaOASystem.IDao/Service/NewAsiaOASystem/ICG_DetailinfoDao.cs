using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface ICG_DetailinfoDao:IBaseDao<CG_DetailinfoView>
    {
        #region //根据采购单ID 查找采购单明细
        IList<CG_DetailinfoView> Getcgmxlist(string id); 
        #endregion
    }
}
