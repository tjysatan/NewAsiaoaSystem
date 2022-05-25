using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPAY_CONTROL_INFODao:IBaseDao<PAY_CONTROL_INFOView>
    {
        
        #region //查询序号最大的数据
        PAY_CONTROL_INFOView GetMaxCONTROL_INFO_ID(string bjdtype);
        #endregion

        
        #region //分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ORDER_NO">订单编号</param>
        /// <param name="CUST_NAME">客户名称</param>
        /// <returns></returns>
        PagerInfo<PAY_CONTROL_INFOView> GetBJdlistPagerInfo(string ORDER_NO, string CUST_NAME); 
        #endregion
    }
}
