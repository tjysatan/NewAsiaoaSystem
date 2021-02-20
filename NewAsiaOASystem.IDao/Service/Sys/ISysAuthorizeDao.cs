using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace  NewAsiaOASystem.IDao
{
    public interface ISysAuthorizeDao : IBaseDao<SysAuthorizeView>
    {
        string GetSysAuthorizeTreeData();
        string GetTreeAuthorize();
        IList<SysAuthorize> NGetListID(string id);
    }
}
