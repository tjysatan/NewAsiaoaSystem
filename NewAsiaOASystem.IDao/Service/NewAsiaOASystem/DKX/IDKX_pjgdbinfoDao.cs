using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_pjgdbinfoDao:IBaseDao<DKX_pjgdbinfoView>
    {
        #region //拼接柜底板数据
        /// <summary>
        /// 拼接柜底板数据
        /// </summary>
        /// <param name="dbname">型号名称</param>
        /// <param name="dbwlno">物料代码</param>
        /// <returns></returns>
        PagerInfo<DKX_pjgdbinfoView> GetDKXpjgdbinfopagelist(string dbname, string dbwlno); 
        #endregion

        #region //通过物料代码查找底板信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wlno">物料代码</param>
        /// <returns></returns>
        DKX_pjgdbinfoView Getpjdbinfobywlno(string wlno); 
        #endregion
    }
}
