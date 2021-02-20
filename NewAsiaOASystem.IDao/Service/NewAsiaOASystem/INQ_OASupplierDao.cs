using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_OASupplierDao : IBaseDao<NQ_OASupplierView>
    {
        PagerInfo<NQ_OASupplierView> getListByNameConTel(string name, string con, string tel, string status, SessionUser user);

        #region //根据供应商名称查找供应商信息
        NQ_OASupplierView GetSupplierByFNumber(string name);
        #endregion

        
        NQ_OASupplierView GetSupplierById(string ID);


        NQ_OASupplier GetSupplierByIdNoView(string ID);

        NQ_OASupplier GetSuById(string ID);
        

        IList<NQ_OASupplierView> getSearchList(string suName, string suCon, string suTel);

        PagerInfo<NQ_OASupplierView> setPagerInfo(IList<NQ_OASupplierView> list, int pageIndex, int pageSize);

        IList<NQ_OASupplierView> setListStatus(IList<NQ_OASupplierView> suppliers);

        suStatus setSuStatus(NQ_OASupplierView oasu);

        suStatus fileDateStatus(NQ_SupplierAttachmentView att);

        bool saveOrUpdateSupplier(string supplierid, string itemid );

        bool deleteItemSupplier(string supplierid, string itemid);

        IList<NQ_OASupplierView> setItemCount(IList<NQ_OASupplierView> suppliers);
        NQ_OASupplier NGetModeldataById(string id);

        bool baseUpdate(NQ_OASupplier smodel);
        bool baseInsert(NQ_OASupplier smodel);

        //DataSet getQualifiedItems(string suppliername);


    }
}
 