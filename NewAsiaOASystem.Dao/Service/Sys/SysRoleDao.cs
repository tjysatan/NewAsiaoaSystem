using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;
using Newtonsoft.Json;
using Spring.Context.Support;
using NHibernate;
using System.Collections;


namespace NewAsiaOASystem.Dao
{

    public class SysRoleDao : ServiceConversion<SysRoleView, SysRole>, ISysRoleDao
    {

        private string TempHql { get; set; }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysRoleView model)
        {
            SysRole listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysRoleView model)
        {
            SysRole listmodel = GetData(model);
            SysRole TempRole = base.GetModelById(model.Id);
            if (TempRole != null)
            {
                listmodel.SysAuth = TempRole.SysAuth;
                listmodel.SysFun = TempRole.SysFun;
                listmodel.SysMenu = TempRole.SysMenu;
                //listmodel.SysDept = TempRole.SysDept;
                //listmodel.SysPerson = TempRole.SysPerson;
            }
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysRoleView model)
        {
            SysRole listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysRoleView> model)
        {
            IList<SysRole> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysRoleView> NGetList()
        {
            List<SysRole> listdata = base.GetList() as List<SysRole>;
            IList<SysRoleView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysRoleView> NGetList_id(string id)
        {
            List<SysRole> list = base.GetList_id(id) as List<SysRole>;
            IList<SysRoleView> listmodel = GetViewlist(list);
            return listmodel;
        }

        public IList<SysRole> NGetListdata_id(string id)
        {
            List<SysRole> list = base.GetList_id(id) as List<SysRole>;
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRoleView NGetModelById(string id)
        {
            SysRoleView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public override SysRoleView GetView(SysRole data)
        {
            SysRoleView view = ConvertToDTO(data);
            if (data.SysMenu != null && data.SysMenu.Count > 0)
                view.SysMenu = JsonConvert.SerializeObject(data.SysMenu);
            if (data.SysAuth != null && data.SysAuth.Count > 0)
                view.SysAuth = JsonConvert.SerializeObject(data.SysAuth);
            if (data.SysFun != null && data.SysFun.Count > 0)
                view.SysFun = JsonConvert.SerializeObject(data.SysFun);
            return view;
        }


        # region 保存权限
        public bool SaveSysRoleAuth(SysRoleView data)
        {
            try
            {
                if (data == null)
                    return false;
                SysRole role = base.GetModelById(data.Id);
                ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
                role.SysMenu = _ISysMenuDao.NGetListID(data.SysMenu);
                ISysFunctionDao _ISysFunctionDao = ContextRegistry.GetContext().GetObject("SysFunctionDao") as ISysFunctionDao;
                role.SysFun = _ISysFunctionDao.NGetListModel(data.SysFun);
                return base.Update(role);
                //ISysAuthorizeDao _ISysAuthorizeDao = ContextRegistry.GetContext().GetObject("SysAuthorizeDao") as ISysAuthorizeDao;
                //role.SysAuth = _ISysAuthorizeDao.NGetListID(data.SysAuth);
                //if (base.Update(role))
                //{
                //    if (SaveField(data))
                //        return true;
                //}

                //return false;
            }

            catch
            {
                return false;
            }
        }

        #endregion

        #region  放弃使用
        /// <summary>
        /// 保存选中的角色菜单按钮处理方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /*  private bool SaveButton(SysRoleView data)
          {
              try
              {
                  string ButtonId = data.SysButton;
                  List<SysButtonPara> dict = JsonConvert.DeserializeObject<List<SysButtonPara>>(ButtonId);
                  //StringBuilder sbDel = new StringBuilder();
                  StringBuilder sbIns = new StringBuilder();
                  if (dict != null)
                  {
                      foreach (var item in dict)
                      {
                          //sbDel.AppendFormat(" delete from SysRoleMenuButton where RoleId='{0}' and MenuButtonId='{1}' and ButtonId='{2}'; "
                          //    , data.Id, item.menuId, item.buttonId);
                          sbIns.AppendFormat(" insert into SysRoleMenuButton(RoleId,MenuId,ButtonId) values('{0}','{1}','{2}') "
                            , data.Id, item.menuId, item.buttonId);
                      }

                      string DelSql = string.Format(" delete from SysRoleMenuButton where RoleId='{0}' ", data.Id);
                      int i = Session.CreateSQLQuery(DelSql).ExecuteUpdate();
                      if (i >= 0)
                      {
                          i = Session.CreateSQLQuery(sbIns.ToString()).ExecuteUpdate();
                          if (i >= 0)
                              return true;
                      }

                      return false;
                  }

                  else
                      return true;
              }

              catch
              {
                  return false;
              }
          }*/

        #endregion

        #region 保存字段方法(本系统中未使用该权限)
        /// <summary>
        /// 保存选中的角色菜单字段处理方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool SaveField(SysRoleView data)
        {
            try
            {
                string AuthId = data.SysField;
                List<SysFieldPara> dict = JsonConvert.DeserializeObject<List<SysFieldPara>>(AuthId);
                StringBuilder sbIns = new StringBuilder();
                if (dict != null)
                {
                    foreach (var item in dict)
                    {
                        sbIns.AppendFormat(" insert into SysRoleColumn(RoleId,MenuId,[Type],[Field_name]) values('{0}','{1}','{2}','{3}') "
                          , data.Id, item.menuId, item.Status, item.Field);
                    }

                    string DelSql = string.Format(" delete from SysRoleColumn where RoleId='{0}' ", data.Id);
                    int i = Session.CreateSQLQuery(DelSql.ToString()).ExecuteUpdate();
                    if (i >= 0)
                    {
                        if (!string.IsNullOrEmpty(sbIns.ToString()))
                            i = Session.CreateSQLQuery(sbIns.ToString()).ExecuteUpdate();
                        else
                            i = 0;

                        if (i >= 0)
                            return true;
                    }

                    return false;
                }

                else
                    return true;
            }

            catch
            {
                return false;
            }
        }

        #endregion

        #region 获取该角色选中的权限并返回json字符串
        /// <summary>
        ///  获取该角色选中的权限并返回json字符串
        /// </summary>
        /// <param name="Roleid">角色ID</param>
        /// <returns></returns>
        public string GetSelectedAuth(string Roleid)
        {
            SysRoleView role = NGetModelById(Roleid);
            IList<object[]> roleColumnObject = Session.CreateSQLQuery(string.Format("select * from SysRoleColumn where RoleId='{0}'", Roleid)).List<object[]>();
            string menu = role.SysMenu;
            string func = role.SysFun;//选中功能权限
            string Auth = role.SysAuth;//授权代码
            if (menu == null)
                menu = "[]";
            if (func == null)
                func = "[]";
            if (Auth == null)
                Auth = "[]";
            List<SysRoleColumn> RoleColumnList = new List<SysRoleColumn>();
            foreach (var item in roleColumnObject)
            {
                SysRoleColumn column = new SysRoleColumn();
                column.RoleId = Convert.ToString(item[0]);
                column.MenuId = Convert.ToString(item[1]);
                column.Type = Convert.ToString(item[2]);
                column.Field_name = Convert.ToString(item[3]);
                RoleColumnList.Add(column);
            }
            string RoleColumn = JsonConvert.SerializeObject(RoleColumnList);
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");
            sb.Append("'menu':'");
            sb.Append(menu);
            sb.Append("','func':'");
            sb.Append(func);
            sb.Append("','Auth':'");
            sb.Append(Auth);
            sb.Append("','RoleColumn':'");
            sb.Append(RoleColumn);
            sb.Append("'}]");
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 获取树形菜单数据
        /// </summary>
        /// <returns></returns>
        public string GetRoleTreeData()
        {
            List<SysRole> list = base.GetList() as List<SysRole>;
            List<SysRoleView> listView = GetViewlist(list) as List<SysRoleView>;
            //  string str = JsonConvert.SerializeObject(listView);
            Base<SysRoleView> _Base = new Base<SysRoleView>();
            string str = _Base.AddNode(listView, "Id", "Pid", null, "Name", 1);
            return str;
        }


        /// <summary>
        /// 获取角色所具有的部门成员
        /// </summary>
        /// <returns></returns>
        public string GetRoleDeptData(string keyId)
        {
            SysRole role = base.GetModelById(keyId);
            if (role == null || role.SysDept == null)
                return "[]";
            return JsonConvert.SerializeObject(role.SysDept);
        }


        public IList<SysRoleView> GetRoleSession(string id)
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SysPerson sysdata = _ISysPersonDao.NGetModeldataById(id);
            IList<SysRole> sysrole = sysdata.Role;
            IList<SysRoleView> sysroleview = GetViewlist(sysrole as List<SysRole>);
            return sysroleview;
        }

        /// <summary>
        /// 查询角色数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public PagerInfo<SysRoleView> GetRole(string UserId, string RoleName)
        {
            /*TempHql = " and 1=2";//这样设置可默认用户无法查看角色
            if (UserId == "admin")
            {

                if (!string.IsNullOrEmpty(RoleName))
                    TempHql = string.Format(" and  Name like '%{0}%'", RoleName);
                else
                    TempHql = string.Format(" and 1=1");
            }
            else
            {
                GetRoleChild(UserId, RoleName);
            }*/

            if (!string.IsNullOrEmpty(RoleName))
                TempHql = string.Format(" and  Name like '%{0}%'", RoleName);
            return Search();
        }

        public override String GetSearchHql()
        {
            return string.Format(" from {0} where 1=1 {1}", typeof(SysRole).Name, TempHql);
        }


        /// <summary>
        /// 查询该用户所拥有的子角色
        /// </summary>
        /// <param name="UserId">登录账户</param>
        /// <param name="RoleName">角色名称（用于条件查询可为null）</param>
        private void GetRoleChild(string UserId, string RoleName)
        {
            try
            {

                ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
                SysPerson Person = _ISysPersonDao.GetModelByLogin(UserId);
                if (Person != null)
                {
                    IList<SysRole> RoleList = Person.Role;
                    ArrayList arr = new ArrayList();
                    if (RoleList != null)
                    {
                        foreach (var Role in RoleList)
                        {
                            arr.Add("'" + Role.Id + "'");
                        }
                    }
                    if (arr.Count > 0)
                    {
                        string[] arrString = (string[])arr.ToArray(typeof(string));
                        string sql = string.Format(@" WITH  temp AS ( SELECT   *  FROM  SysRole  WHERE Pid in({0})
                       UNION ALL SELECT   d.*  FROM  SysRole d INNER JOIN temp ON d.pid = temp.Id )
                       SELECT distinct ID FROM temp ", string.Join(",", arrString));
                        IList list = Session.CreateSQLQuery(sql).AddScalar("ID", NHibernateUtil.String).List();
                        if (list != null)
                        {
                            arr.Clear();
                            foreach (var item in list)
                            {
                                arr.Add("'" + item + "'");
                            }
                            if (arr.Count > 0)
                            {
                                arrString = (string[])arr.ToArray(typeof(string));
                                //根据用户名查询
                                if (!string.IsNullOrEmpty(RoleName))
                                    TempHql = string.Format(" and Id in({0}) and  Name like '%{1}%'", string.Join(",", arrString), RoleName);
                                else
                                    TempHql = string.Format(" and Id in({0})", string.Join(",", arrString));
                            }
                        }
                    }
                }
            }

            catch
            {

            }
        }

    }
}
