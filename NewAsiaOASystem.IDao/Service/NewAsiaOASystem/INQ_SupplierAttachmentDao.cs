using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_SupplierAttachmentDao : IBaseDao<NQ_SupplierAttachmentView>
    {
        PagerInfo<NQ_SupplierAttachmentView> GetCinfoList(string Name, SessionUser user);
        IList<NQ_SupplierAttachmentView> GetlistCust();



        #region //根据供应商代码查找供应商信息
        //string GetSupplierById(string fnumber);
        #endregion

        #region //根据供应商名称查找供应商信息
        NQ_SupplierAttachmentView GetAttachBySupplierAndType(string supplierid,string attType);

        NQ_SupplierAttachmentView GetAttachBySupplierAndTypeAndSeq(string supplierid, string attType,string seq);

        #endregion



        #region //根据供应商名称查找供应商信息
        IList<NQ_SupplierAttachmentView> GetAttachmentBySupplierId(string FItemID);
        #endregion

        bool GetUpdatedSupplier();

        bool saveOrUpdate(NQ_SupplierAttachmentView att);

        bool saveOrUpdateList(List<NQ_SupplierAttachmentView> atts);

        //IDictionary<string, string> getTopSupplier();

        IList<NQ_SupplierAttachmentView> getAttachBySunameAndType(string suname, string ftype);

        PagerInfo<NQ_SupplierAttachmentView> setPagerInfo(IList<NQ_SupplierAttachmentView> list, int pageIndex, int pageSize);
    }
}
 