using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// session存储的数据
    /// </summary>
   public class SessionUser
    {
       /// <summary>
       /// 用户ID
       /// </summary>
       public string Id { get; set; }
       /// <summary>
       /// 用户账号
       /// </summary>
       public string UserName { get; set; }


       /// <summary>
       /// 真实姓名
       /// </summary>
       public string RName { get; set; }
       /// <summary>
       /// 角色
       /// </summary>
       public List<SysRoleView> Role { get; set; }

       /// <summary>
       /// 部门
       /// </summary>
       public List<SysDepartmentView> Dept { get; set; }//部门

       public string MenuLeft { get; set; }//用户所在角色对应的菜单（左侧json格式）

       public List<string> Menulist { get; set; }//用户所在角色对应的菜单列表（即该角色所具有的菜单）

       public List<string> Funlist { get; set; }//用户所在角色对应被禁止的功能权限

       /// <summary>
       /// 过滤数据权限条件语句，根据登录时当前用户所属角色过滤
       /// </summary>
       public string FilterSql { get; set; }

       /// <summary>
       /// 保存当前用户能查看到的所有社区代码
       /// </summary>
       //public string FilterImmCode { get; set; }

       /// <summary>
       /// 保存当前用户能查看到的所有社区名称
       /// </summary>
       public string FilterImmName { get; set; }

      /// <summary>
       ///（当RoleType等于0时表示超级管理员,等于1时表示一般管理员，等于2时表示普通用户）
       ///超级管理员可查看所有数据，一般管理员（客服主管）,普通用户只能查看自己数据
      /// </summary>
       public string RoleType { get; set; }

       //public List<string> PersonId { get; set; } //保存当前角色可管理的所有用户ID
    }
}
