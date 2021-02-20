using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace  NewAsiaOASystem.IDao
{
    public interface ISysMenuDao : IBaseDao<SysMenuView>
    {
        string GetMenuTreeData();
        string NGetListJson_id(string id);
        string GetMenuButtonTreeData(string menuId);
        string NGetListJsonTree_id(string id);
        IList<SysMenu> NGetListID(string id);
        string GetleftnavMenu(string Id);

        string GetDatelistMenu();
        PagerInfo<SysMenuView> GetPageList(string Name);

        #region 根据用户ID 获取用户角色对应的 菜单权限
        string GetRole_Menu(string Id);
        List<string> GetRole_listMenu(string Id);
        #endregion
    }
}
