using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_ZLDataInfoDao:IBaseDao<DKX_ZLDataInfoView>
    {
        #region //根据订单Id,和资料类型查询资料数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">资料类型 0 需求 1 料单 2 料单 3 照片 4 调式单</param>
        /// <returns></returns>
        IList<DKX_ZLDataInfoView> GetzljsonbyId(string Id, string type); 
        #endregion


        #region //根据订单Id,和资料类型查询管理资料数据
        /// <summary>
        /// 根据订单Id,和资料类型查询管理资料数据model关联的资料
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DKX_ZLDataInfoView GetzldatamodelbyIdandtype(string Id, string type); 
        #endregion


        #region //根据订单的Id查询全部的资料数据
        /// <summary>
        /// 根据订单的Id查询全部的资料数据
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <returns></returns>
        IList<DKX_ZLDataInfoView> GetAllzldatabyId(string Id); 
        #endregion
    }
}
