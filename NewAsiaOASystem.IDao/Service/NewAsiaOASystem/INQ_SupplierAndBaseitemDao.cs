using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_SupplierAndBaseitemDao : IBaseDao<NQ_SupplierAndBaseitemView>
    {
        IList<NQ_SupplierAndBaseitemView> getBaseitemBySupplierid(string supplierid);

        IList<NQ_SupplierAndBaseitemView> getSupplierByItemid(string itemid);

        NQ_SupplierAndBaseitemView getBySupplierAndItem(string supplierid,string  itemid);

         
    }
}
 