using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao 
{
    public interface IXSFXqxinfoDao:IBaseDao<XSFXqxinfoView>
    {
        //根据大区ID 查找分区明细
        IList<XSFXqxinfoView> Getxsfxqxlist(string id);
    }
}
