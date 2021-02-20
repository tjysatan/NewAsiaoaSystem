using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_CONTROL_LIST_DETAILDao:IBaseDao<DKX_CONTROL_LIST_DETAILView>
    {
        #region //通过订单NO查找需求明细
        IList<DKX_CONTROL_LIST_DETAILView> Getliaodanmxbyorderno(string ORDER_NO); 
        #endregion

        #region //通过需求单号查询非控制部分的料单明细
        /// <summary>
        /// 通过需求单号查询非控制部分的料单明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <returns></returns>
        IList<DKX_CONTROL_LIST_DETAILView> GetliaodanFKZbyXQno(string xqno); 
        #endregion

        #region //通过需求单号查询控制部分的料单明细
        /// <summary>
        /// 通过需求单号查询控制部分的料单明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <returns></returns>
        IList<DKX_CONTROL_LIST_DETAILView> GetliaodanKZbyXQNO(string xqno); 
        #endregion


        #region //通过需求单号和订单号查询料单明细
        /// <summary>
        /// 通过需求单号和订单号查询料单明细
        /// </summary>
        /// <param name="xqNo">报价系统关联号</param>
        /// <param name="oId">订单号</param>
        /// <returns></returns>
        IList<DKX_CONTROL_LIST_DETAILView> GetliaodanmxbyxqnoandoId(string xqNo, string oId); 
        #endregion
    }
}
