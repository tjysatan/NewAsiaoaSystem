using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_NonSProductinfoDao:IBaseDao<Flow_NonSProductinfoView>
    {
        /// <summary>
        /// 获取非标产品数据
        /// </summary>
        /// <param name="Sort"></param>
        /// <param name="Category"></param>
        /// <param name="wldm"></param>
        /// <param name="Pname"></param>
        /// <returns></returns>
        IList<Flow_NonSProductinfoView> GetNonsdata(string Sort, string Category, string wldm, string Pname);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wldm"></param>
        /// <returns></returns>
       Flow_NonSProductinfoView Getmodelbywldm(string wldm);

       
        #region //非标试产产品的分页数据
        PagerInfo<Flow_NonSProductinfoView> Getfeibiaowkpagerlist(string cpname, string wlno, string category); 
        #endregion
    }
}
