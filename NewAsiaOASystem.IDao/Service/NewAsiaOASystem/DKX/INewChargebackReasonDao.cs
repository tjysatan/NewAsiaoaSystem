using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INewChargebackReasonDao:IBaseDao<NewChargebackReasonView>
    {
        #region //管理订单退单原因的分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">原因名称模糊查询</param>
        /// <param name="type">类型 0 生产退单</param>
        /// <param name="IsRemarks">是否需要备注 0不需要 1需要 </param>
        /// <param name="IsDis">是否启用</param>
        /// <returns></returns>
        PagerInfo<NewChargebackReasonView> GetChargebackReasonlistpage(string name, string type, string IsRemarks, string IsDis);
        #endregion

        #region //查询全部的生产退单原因数据
        IList<NewChargebackReasonView> GetSCCBRData(); 
        #endregion
    }
}
