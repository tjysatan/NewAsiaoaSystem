using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_PAY_CONTROL_INFODao:IBaseDao<DKX_PAY_CONTROL_INFOView>
    {
        #region //通过ORDER_NO 查找需求单数据
        //通过ORDER_NO 查找需求单数据
        DKX_PAY_CONTROL_INFOView GetDKXxuqiubyORDER_NO(string ORDER_NO); 
        #endregion

        #region //通过ORDER_NO 和订单Id查找需求单
        /// <summary>
        /// 通过报价系统的订单号和订单的Id 查找需求单
        /// </summary>
        /// <param name="ORDER_NO"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        DKX_PAY_CONTROL_INFOView GetDKXxuqiubyORDER_NOandId(string ORDER_NO, string Id); 
        #endregion
    }
}
