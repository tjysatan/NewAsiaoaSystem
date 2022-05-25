using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_k3BominfoDao:IBaseDao<DKX_k3BominfoView>
    {
        #region //根据订单Id和K3的bom表的编码查询料单
        /// <summary>
        /// 根据订单Id和K3的bom表的编码查询料单
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="bomno">bom编号</param>
        /// <returns></returns>
        IList<DKX_k3BominfoView> GetliaodanlistbyIdandbomno(string Id, string bomno);
        #endregion

        
        #region //根据订单(方案库产品)Id 查询料单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">订单Id 或者产品Id</param>
        /// <returns></returns>
        IList<DKX_k3BominfoView> GetliaodanlistbyId(string Id); 
        #endregion
    }
}
