using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace  NewAsiaOASystem.IDao
{
    public interface ISysRoleDao : IBaseDao<SysRoleView>
    {
        bool SaveSysRoleAuth(SysRoleView data);
        string GetSelectedAuth(string Roleid);
        string GetRoleTreeData();
        string GetRoleDeptData(string keyId);
 
        IList<SysRole> NGetListdata_id(string id);
        /// <summary>
        /// 根据用户ID  查询所分配到的角色和权限信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        IList<SysRoleView> GetRoleSession(string id);
        PagerInfo<SysRoleView> GetRole(string UserId, string RoleName);
    }
}
