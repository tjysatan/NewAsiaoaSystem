using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;

namespace NewAsiaOASystem.IDao
{
  public  interface ISysLog_historyDao:IBaseDao<SysLog_historyView>
    {
        /// <summary>
        /// datatable转换成 json 格式的方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
      string CreateJsonParameters(DataTable dt); 
    }
}
