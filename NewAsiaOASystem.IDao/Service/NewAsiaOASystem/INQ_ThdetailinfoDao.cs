using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_ThdetailinfoDao:IBaseDao<NQ_ThdetailinfoView>
    {

        #region //根据返退货流程ID 查找退货明细
        IList<NQ_ThdetailinfoView> Gettfinfoby_rid(string R_Id); 
        #endregion

      
    }
}
