using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;


namespace NewAsiaOASystem.IDao
{
    public interface IWX_MenusDao:IBaseDao<WX_MenusView>
    {

        #region // 通过父级ID 查找二级菜单 接口
        IList<WX_MenusView> wx_GetejMenu_by(string Id); 
        #endregion

       #region //只查询一级菜单
        IList<WX_MenusView> wx_Geteonemenu();
	   #endregion

        #region //自定义菜单 管理列表的分页数据
        PagerInfo<WX_MenusView> GetWx_MenusList(); 
        #endregion

        #region //按序查询全部自定义菜单
        IList<WX_MenusView> Getallist();
        #endregion

        #region //微信公众号菜单树形菜单数据
        string GetWX_MenusTreeData(); 
        #endregion

    }
}
