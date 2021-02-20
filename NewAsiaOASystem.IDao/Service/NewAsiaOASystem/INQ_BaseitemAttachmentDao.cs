using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_BaseitemAttachmentDao : IBaseDao<NQ_BaseitemAttachmentView>
    {
        PagerInfo<NQ_BaseitemAttachmentView> GetCinfoList(string Name, SessionUser user);
        IList<NQ_BaseitemAttachmentView> GetlistCust();



        #region //根据供应商代码查找供应商信息
        //string GetBaseitemById(string fnumber);
        #endregion

        #region //根据供应商名称查找供应商信息
        NQ_BaseitemAttachmentView GetAttachByBaseitemAndTypeAndSupplier(string Baseitemid,string attType,string supplierid);
        #endregion

        IList<NQ_BaseitemAttachmentView> GetAttachByBaseitemAndSupplier(string Baseitemid, string supplierid);

        #region //根据供应商名称查找供应商信息
        IList<NQ_BaseitemAttachmentView> GetAttachmentByBaseitemId(string FItemID);
        #endregion

        suStatus setSuStatus(NQ_SupplierAndBaseitemView oasu);

        suStatus fileDateStatus(NQ_BaseitemAttachmentView att);

    }
}
