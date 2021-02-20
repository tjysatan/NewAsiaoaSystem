using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_chuhuoinfonDao:IBaseDao<NQ_chuhuoinfonView>
    {
        PagerInfo<NQ_chuhuoinfonView> GetCinfoList(string Name, SessionUser user);
    }
}
