using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_XSqitainfoDao:IBaseDao<NA_XSqitainfoView>
    {
        
        #region //通过电商的订单的Id查询销售订单的其他信息
        /// <summary>
        /// 通过电商的订单的Id查询销售订单的其他信息
        /// </summary>
        /// <param name="DsOrder_Id">电商销售订单Id</param>
        /// <returns></returns>s
        NA_XSqitainfoView GetModelByDsOrder_Id(string DsOrder_Id); 
        #endregion
    }
}
