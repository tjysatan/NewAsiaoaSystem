using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_JxqnumberDao:IBaseDao<_20150509WL_JxqnumberView>
    {
        PagerInfo<_20150509WL_JxqnumberView> GetCinfoList(string Name, string Year, SessionUser user);

        bool checkrepeat(string Year, string Cu_Id);
    }
}
