using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_CONTROL_LISTDao:IBaseDao<DKX_CONTROL_LISTView>
    {
        #region //通过ORDER_NO 查找需求单的ID查找料单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ORDER_NO"></param>
        /// <returns></returns>
        DKX_CONTROL_LISTView Getliaodanbyorderno(string ORDER_NO); 
        #endregion

        #region //通过需求单号 查询料单
        /// <summary>
        /// 通过需求单号 查询料单
        /// </summary>
        /// <param name="xqno">xqno</param>
        /// <returns></returns>
        DKX_CONTROL_LISTView Getliaodanbyxqno(string xqno); 
        #endregion

        #region //通过需求单号和订单号查找料单主表明细
        /// <summary>
        /// 通过需求单号和订单号查找料单主表明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <param name="oId">订单Id</param>
        /// <returns></returns>
        DKX_CONTROL_LISTView GetliaodanbyxqnoandoId(string xqno, string oId); 
        #endregion

    }
}
