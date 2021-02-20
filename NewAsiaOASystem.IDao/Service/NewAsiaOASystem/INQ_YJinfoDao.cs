using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_YJinfoDao : IBaseDao<NQ_YJinfoView>
    {
        PagerInfo<NQ_YJinfoView> GetCinfoList(string Name, string Yname, string isbj, SessionUser user);

        IList<NQ_YJinfoView> GetlistCust();

        IList<NQ_YJinfoView> Getlistbyname(string name);

        #region //根据 元器件代码查找元器件信息
        NQ_YJinfoView GetYqjModelbyW_dm(string W_dm);
        #endregion

        NQ_YJinfoView GetItemById(string id);

        #region //根据供应商代码查找 元器件
        IList<NQ_YJinfoView> Getlistbygysdm(string gysdm);
        #endregion

        /// <summary>
        /// 通过元器件的名称或者型号或者物料代码查询元器件
        /// </summary>
        /// <param name="paraDat">参数（名称or型号or代码）</param>
        /// <returns></returns>
        IList<NQ_YJinfoView> GetyjlistbyNameorxhordm(string paraDat);

        IList<NQ_YJinfoView> GetItemsWithSupplier(string supplierid);

        IList<NQ_YJinfo> GetItemsWithSupplierNoView(string supplierid);

        

        IList<NQ_YJinfoView> GetItemsNotWithSupplier(string supplierid);

        PagerInfo<NQ_YJinfoView> setPagerInfo(IList<NQ_YJinfoView> list, int pageIndex, int pageSize);

        NQ_YJinfo GetItemOnlyById(string id);

    }
}
