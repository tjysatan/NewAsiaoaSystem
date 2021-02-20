using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_XSdetailsinfoDao:IBaseDao<NA_XSdetailsinfoView>
    {
         #region //根据销售订单ID 查找订单明细
        IList<NA_XSdetailsinfoView> GetxsdetaillistbyxsId(string xsid); 
        #endregion

        /// <summary>
        /// 根据销售订单Id,统计产品数量的
        /// </summary>
        /// <param name="xsid">销售订单Id</param>
        /// <returns></returns>
        decimal GetchsumbyxsId(string xsid);
    }
}
