using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 区域管理 接口
    /// </summary>
    public interface INA_QyinfoDao:IBaseDao<NA_QyinfoView>
    {
        #region //区域管理 分页数据 接口
        PagerInfo<NA_QyinfoView> GetQyinfoList(string Name, SessionUser user); 
        #endregion

        #region //父级区域数据
        IList<NA_QyinfoView> GetlistbyPqy(); 
        #endregion

        #region //区域树形菜单数据
        string GetQYTreeData(); 
        #endregion


        #region //根据父级区域Id 查找该区域下面所有的区域信息
        IList<NA_QyinfoView> GetlistCqybypid(string Pid);
        #endregion

        
        #region //根据省份名称或者市名称查询
        /// <summary>
        /// 根据区域名称查询Id
        /// </summary>
        /// <param name="username">区域名称</param>
        /// <returns></returns>
        string GetqyinfoIdbyname(string username);
        #endregion


        #region //查询全部的地级市的名称数据
        IList<NA_QyinfoView> Gettemporarydata(); 
        #endregion
    }
}
