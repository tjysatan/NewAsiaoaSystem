using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_SupplierDao : IBaseDao<NQ_SupplierView>
    {
        PagerInfo<NQ_SupplierView> GetCinfoList(string name, SessionUser user);

        PagerInfo<NQ_SupplierView> getListByNameConTel(string name,string con, string tel, SessionUser user);

        IList<NQ_SupplierView> GetlistCust();

        //#region //根据供应商代码查找供应商信息
        //string GetSupplierById(string dm);
        //#endregion

        #region //根据供应商代码查找供应商信息
        string GetSupplierById(string fnumber);
        #endregion

        #region //根据供应商名称查找供应商信息
        NQ_SupplierView GetSupplierByName(string name);
        #endregion

        #region //根据供应商名称查找供应商信息
        NQ_SupplierView GetSupplierByFItemId(string FItemID);
        #endregion

        bool GetUpdatedSupplier();

        bool saveOrUpdate(IList<NQ_SupplierView> suppliers);


        IDictionary<string, string> getTopSupplier();

    }
}
 