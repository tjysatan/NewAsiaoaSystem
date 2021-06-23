using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_MRPmainDao:IBaseDao<NA_MRPmainView>
    {
        
        #region //保存后返回ID
        string InsertID(NA_MRPmainView modelView);
        #endregion

        
        #region //MRP计算单据的分页结果
        /// <summary>
        /// 计算的订单时间
        /// </summary>
        /// <param name="orderxdtime"></param>
        /// <returns></returns>
        PagerInfo<NA_MRPmainView> GetNA_MRPorderlistpage(string orderxdtime); 
        #endregion
    }
}
