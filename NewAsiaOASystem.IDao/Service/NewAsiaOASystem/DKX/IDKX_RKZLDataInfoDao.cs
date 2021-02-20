using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_RKZLDataInfoDao:IBaseDao<DKX_RKZLDataInfoView>
    {
        #region //根据产品Id查询入库资料库
        /// <summary>
        /// 根据产品Id查询入库资料库
        /// </summary>
        /// <param name="cpId">产品Id</param>
        /// <returns></returns>
        IList<DKX_RKZLDataInfoView> GetDKXCPZLdatalist(string cpId); 
        #endregion

        #region //通过产品Id 和类型查询资料的list 数据
        /// <summary>
        /// 通过产品Id 和类型查询资料的list 数据
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <param name="type">资料类型</param>
        /// <returns></returns>
        IList<DKX_RKZLDataInfoView> GetRKzljsonbyId(string Id, string type); 
        #endregion


        #region //根据订单Id,和资料类型查询管理资料数据
        /// <summary>
        /// 根据订单Id,和资料类型查询管理资料数据model关联的资料
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DKX_RKZLDataInfoView GetzldatamodelbyIdandtype(string Id, string type); 
        #endregion
    }
}
