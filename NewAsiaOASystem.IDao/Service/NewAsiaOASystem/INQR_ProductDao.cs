using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 返退货管理产品名称管理 借口 
    /// tjy_satan
    /// </summary>
    public interface  INQR_ProductDao:IBaseDao<NQR_ProductView>
    {
        PagerInfo<NQR_ProductView> GetCinfoList(string Name, SessionUser user);
    }
}
