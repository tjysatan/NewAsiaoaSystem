using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_DKXPlanPPrintinfoDao:IBaseDao<Flow_DKXPlanPPrintinfoView>
    {
        /// <summary>
        /// 根据订单查询打印信息
        /// </summary>
        /// <param name="Plan_Id">生产计划单Id</param>
        /// <returns></returns>
        Flow_DKXPlanPPrintinfoView GetFlow_PlanprinmodelbypId(string Plan_Id);
    }
}
