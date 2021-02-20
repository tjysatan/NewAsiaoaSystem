using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao 
{
    public interface IXSFXdqinfoDao:IBaseDao<XSFXdqinfoView>
    {
        PagerInfo<XSFXdqinfoView> GetxsfxList(string Name, SessionUser user);
    }
}
