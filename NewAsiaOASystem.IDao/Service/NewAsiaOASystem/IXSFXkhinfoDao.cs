using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao 
{
    public interface IXSFXkhinfoDao:IBaseDao<XSFXkhinfoView>
    {
            //根据区县ID 查找客户明细
        IList<XSFXkhinfoView> Getxsfxkhlist(string id);
    }
}
