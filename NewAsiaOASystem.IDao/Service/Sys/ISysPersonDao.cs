using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface ISysPersonDao : IBaseDao<SysPersonView>
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        bool login(string name, string pwd);
        IList<SysPersonView> GetModelByname(string name);
        bool check_repeat(string name);
        string NGetListJson(string RoleId);
        bool NUpdatedata(string id);
        SysPerson NGetModeldataById(string id);
        SysPerson GetModelByLogin(string Login);
        SessionUser GetSessionuser(string name);
        PagerInfo<SysPersonView> GetPersonData(string LoginName, string Name, SessionUser user);
        string RoleAlbumDropdown(string personId);
         /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="LoginId">登录账号</param>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        string UpdatePassword(string LoginId, string OldPassword, string NewPassword);

         SysPerson GetDModelbyId(string Id);

         #region //根据用户Id查找用户信息 转换成Json
         string BDGetpersonbyID(string Id); 
         #endregion

        #region //根据用户名称查找用户ID
         string GetUesrIdbyUserName(string UserName);
	    #endregion


         void loginsession(SessionUser user);

         #region //根据角色类型查询该类型下面的用户成员 0 超级管理员  1 管理员 2 一级代理 3 二级代理 4 三级代理
         /// <summary>
         /// 
         /// </summary>
         /// <param name="type">角色类型</param>
         /// <returns></returns>
         IList<SysPersonView> GetPersonbyRoletype(string type); 
         #endregion

         #region //根据角色名称查该类型下的用户成功
         /// <summary>
         /// 根据角色名称查该类型下的用户成功
         /// </summary>
         /// <param name="Rolename">角色名称</param>
         /// <returns></returns>
         IList<SysPersonView> GetPersonbyRolename(string Rolename);
        #endregion

       
        #region //通过账户的联系方式查询账户信息中ERP的账户编号
        /// <summary>
        /// 通过账户的联系方式查询账户信息中ERP的账户编号
        /// </summary>
        /// <param name="tel">电话</param>
        /// <returns></returns>
        string GetERP_NObytel(string tel); 
        #endregion
    }
}
