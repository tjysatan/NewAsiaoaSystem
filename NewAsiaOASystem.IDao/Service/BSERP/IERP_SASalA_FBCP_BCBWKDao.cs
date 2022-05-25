using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 普实erp 销售出货数据
    /// </summary>
    public interface IERP_SASalA_FBCP_BCBWKDao : IBaseDao<ERP_SASalA_FBCP_BCBWKView>
    {


        #region //根据账期查询非标产品包含的半成品温控的数据
        /// <summary>
        /// 根据账期查询非标产品包含的半成品温控的数据
        /// </summary>
        /// <param name="AbsID"></param>
        /// <returns></returns>
        IList<ERP_SASalA_FBCP_BCBWKView> GetFB_WKDATA_by_AbsID(string AbsID);
       #endregion
    }
}
