using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IJs_xz_yfcostDao:IBaseDao<Js_xz_yfcostView>
    {
        #region //通过日期和小组名称查询小组数据研发成本数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Js_xz_name">小组名称</param>
        /// <param name="Fy_data">费用日期</param>
        /// <returns></returns>
        Js_xz_yfcostView GetJx_xzcostbyxznameanddate(string Js_xz_name, string Fy_data); 
        #endregion
    }
}
