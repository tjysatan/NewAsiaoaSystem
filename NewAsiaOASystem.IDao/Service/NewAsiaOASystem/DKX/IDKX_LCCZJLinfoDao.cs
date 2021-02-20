using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_LCCZJLinfoDao:IBaseDao<DKX_LCCZJLinfoView>
    {
        #region //查找记录根据订单Id
        /// <summary>
        /// 查找记录根据订单Id
        /// </summary>
        /// <param name="oId">订单Id</param>
        /// <returns></returns>
        IList<DKX_LCCZJLinfoView> GetlcczjldatabyoId(string oId); 
        #endregion

        #region //根据条件查找操作记录列表
        /// <summary>
        /// 根据条件查找操作记录列表
        /// </summary>
        /// <param name="constr">操作内容</param>
        /// <param name="ddid">订单Id</param>
        /// <param name="gcsid">工程师Id</param>
        /// <param name="ddbianhao">订单编号</param>
        /// <param name="c_timestart">创建时间</param>
        /// <param name="c_timeend"></param>
        /// <returns></returns>
        IList<DKX_LCCZJLinfoView> Getlcczjldatabycondition( string ddid, string gcsid, string ddbianhao, string c_timestart, string c_timeend,string CBRId); 
        #endregion

        #region //查询所有工程师ID为null的生产退单
        IList<DKX_LCCZJLinfoView> Getzllistdata();
        #endregion


    }
}
