using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_SharealikeDao:IBaseDao<NA_SharealikeView>
    {
        
        #region //根据明细Id查找并删除
        /// <summary>
        /// 根据明细Id查找并删除
        /// </summary>
        /// <param name="mxId">明细Id</param>
        /// <returns></returns>
        bool Getlistbymxiddellist(string mxId);
        #endregion

        
        #region //根据责任部门分组查询
        List<Object> GetStatisticsgroupResdepartment(string strattime, string enedtime);
        #endregion

         
        #region //根据退货客户分组查询
        List<Object> GetStatisticsgroupThcusId(string strattime, string endtime); 
        #endregion
    }
}
