using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_MRPdetailedDao : IBaseDao<NA_MRPdetailedView>
    {
        
        #region //查询MRP计算的结果
        List<Object> GetMRPresult(string Mid);
        #endregion

        
        #region //查询MRP的细表
        /// <summary>
        /// 查询MRP的细表
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        IList<NA_MRPdetailedView> GetallMRPdetailedByMid(string Mid); 
        #endregion
    }
}
