using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public class SysPersonDao : ServiceConversion<SysPersonView, SysPerson>, ISysPersonDao
    {

        //重写sql语句
        private StringBuilder TempHql = null;
        /// <summary>
        /// 覆写查询的的HQL 语句
        /// </summary>
        /// <returns></returns>
        public override String GetSearchHql()
        {
            return string.Format(" from {0} where 1=1 {1}", typeof(SysPerson).Name, TempHql.ToString());
        }



        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override SysPersonView GetView(SysPerson data)
        {
            SysPersonView view = ConvertToDTO(data);
            if (data.Department != null)
            {
                List<SysDepartment> dept = data.Department.ToList();
                dept = dept.Where(x => x != null).ToList<SysDepartment>();
                view.Department = JsonConvert.SerializeObject(dept);
            }
            if (data.Role != null)
            {
                List<SysRole> dept = data.Role.ToList();
                dept = dept.Where(x => x != null).ToList<SysRole>();
                view.Role = JsonConvert.SerializeObject(dept);
            }
            return view;
        }

        /// <summary>
        /// TDO 转换成 DATA
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public override SysPerson GetData(SysPersonView view)
        {
            try
            {
                SysPerson data = new SysPerson();
                data = ConvertToData(view);
                if (view.Department != null)
                {
                    ISysDepartmentDao _ISysDepartmentDao = ContextRegistry.GetContext().GetObject("SysDepartmentDao") as ISysDepartmentDao;
                    string sysdepartment = view.Department;
                    sysdepartment = "'" + sysdepartment.Replace(",", "','") + "'";
                    data.Department = _ISysDepartmentDao.NGetList_idData(sysdepartment);
                }
                if (view.Role != null)
                {
                    ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
                    data.Role = _ISysRoleDao.NGetListdata_id(view.Role);
                }

                return data;
            }

            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysPersonView model)
        {
            SysPerson listmodel = GetData(model);
            return base.insert(listmodel);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysPersonView model)
        {
            SysPerson listmodel = GetData(model);
            return base.Update(listmodel);
        }



        /// <summary>
        /// 直接修改DBmodel实体 记录用户登录的次数和最后一次登录的时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool NUpdatedata(string id)
        {
            // SysPerson list = base.GetModelById(id);
            List<SysPerson> list = base.GetList_HQL(string.Format(" where u.Name='{0}'", id)) as List<SysPerson>;
            SysPerson person = null;
            if (list != null && list.Count > 0)
            {
                person = list[0];
                if (person.LogonNum == null)
                {
                    person.LogonNum = 1;
                }
                else
                {
                    person.LogonNum = person.LogonNum + 1;
                }
                person.LastLogonTime = DateTime.Now;
                return base.Update(person);
            }

            return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysPersonView model)
        {
            SysPerson listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysPersonView> model)
        {
            IList<SysPerson> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 获取角色拥有的用户成员
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public string NGetListJson(string RoleId)
        {
            List<SysPerson> listdata = base.GetList_HQL(string.Format("  inner join fetch  u.Role as s where  s.Id='{0}'", RoleId)) as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(listdata);
            if (listmodel == null)
                return "[]";
            string jsonStr = JsonConvert.SerializeObject(listmodel);
            return jsonStr;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysPersonView> NGetList()
        {
            List<SysPerson> listdata = base.GetList() as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysPersonView> NGetList_id(string id)
        {
            List<SysPerson> list = base.GetList_id(id) as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysPersonView NGetModelById(string id)
        {
            SysPersonView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        public SysPerson GetDModelbyId(string Id)
        {
            SysPerson model = base.GetModelById(Id);
            return model;
        }

        

        public SysPerson NGetModeldataById(string id)
        {
            SysPerson listmodel = base.GetModelById(id);
            return listmodel;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool login(string name, string pwd)
        {
            List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + name + "' and Password='" + pwd + "' and State=1") as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return false;
            return listmodel.Count > 0 ? true : false;
        }

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<SysPersonView> GetModelByname(string name)
        {
            List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + name + "'") as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据用户名查询直接返回Data
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysPerson GetModelByLogin(string Login)
        {
            List<SysPerson> list = Session.CreateQuery("from SysPerson where Name='" + Login + "'").List<SysPerson>().ToList();
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool check_repeat(string name)
        {
            IList<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + name + "'");
            return list.Count > 0 ? true : false;
        }

        #region 保存用户角色对应的权限和用户名称到 session 中
        /// <summary>
        /// 保存用户角色对应的权限和用户名称到 session 中
        /// 这个系统中社区普通用户与管理员有相同数据权限即都只能查看本免疫点数据
        /// 而超级管理员可以查看到所有免疫点数据
        /// </summary>
        /// <param name="name">当前登录账号</param>
        /// <returns></returns>
        public SessionUser GetSessionuser(string name)
        {
            SessionUser sessionuser = new SessionUser();
            ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
            ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
            ISysFunctionDao _ISysFunctionDao = ContextRegistry.GetContext().GetObject("SysFunctionDao") as ISysFunctionDao;
            List<SysPerson> PersonList = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + name + "'") as List<SysPerson>;
            if (PersonList != null && PersonList.Count > 0)
            {
                //根据用户登录账号查询当前用户所在免疫点所管理的社区集合
                //IList<Administrative_divisions> IListdivisions = Session.CreateQuery
                //    (string.Format(" from Administrative_divisions u where u.ImmId='{0}'",
                //     PersonList[0].DisImmuneCenter)).List<Administrative_divisions>();

                List<SysRole> RoleList = new List<SysRole>();
                //保存用户账号
                StringBuilder SqlStr = new StringBuilder();
                //保存社区代码
               // StringBuilder SheQu_Code = new StringBuilder();
                if (PersonList[0].Role != null)
                {
                    RoleList = PersonList[0].Role.ToList<SysRole>();
                }

                //判断该用户是否包含超级管理员角色
                //（当RoleType等于0时表示超级管理员,等于1时表示普通管理员，等于2时表示普通用户）
                //超级管理员可查看所有数据，普通管理员只能查看本免疫点数据,普通用户只能查看自己数据
                if (RoleList.Find(x => x != null && Convert.ToInt32(x.RoleType) == 0) != null)
                {
                    sessionuser.RoleType = "0";
                    SqlStr.AppendFormat(" 1=1");//超级管理员可以查看所有用户数据
                }
                else if (RoleList.Find(x => x != null && Convert.ToInt32(x.RoleType) == 1) != null)
                {
                    //当前为普通管理员角色，只能查看本免疫点数据
                    sessionuser.RoleType = "1";
                    //根据用户登录账号查询当前用户所在免疫点所有用户数据
                    IList<SysPerson> IListPerson = Session.CreateQuery
                        (string.Format(" from SysPerson u where u.DisImmuneCenter='{0}'",
                         PersonList[0].DisImmuneCenter)).List<SysPerson>();

                    if (IListPerson != null)
                    {
                        List<SysPerson> PersonCommList = IListPerson.ToList<SysPerson>();
                        SqlStr.Append("(");
                        foreach (var item in PersonCommList)
                        {
                            SqlStr.AppendFormat("'{0}',", item.Name);//循环获取用户账号
                        }

                        if (PersonCommList.Count > 0)
                            SqlStr.Remove(SqlStr.Length - 1, 1);
                        else
                        {
                            //如果没有与该用户相同免疫点的用户那么直接查看自己数据
                            SqlStr.AppendFormat("'{0}'", name);
                        }
                        SqlStr.Append(")");
                    }

                    else
                    {
                        //如果没有与该用户相同免疫点的用户那么直接查看自己数据
                        SqlStr.AppendFormat("'{0}'", name);
                    }

                    //社区集合不为空时遍历集合获取社区代码
                    //if (IListdivisions != null)
                    //{
                    //    SheQu_Code.Append("(");
                    //    foreach (var item in IListdivisions)
                    //    {
                    //        SheQu_Code.AppendFormat("'{0}',", item.A_Name.Trim());//循环获取用户账号
                    //    }

                    //    if (IListdivisions.Count > 0)
                    //        SheQu_Code.Remove(SheQu_Code.Length - 1, 1);
                    //    else
                    //    {
                    //        //如果用户所在免疫点没有社区那么不能查看到任何数据，这边先使用1=2过滤掉所有数据
                    //        SheQu_Code.AppendFormat("'1=2'");
                    //    }
                    //    SheQu_Code.Append(")");
                    //}

                    //else
                    //{
                    //    //如果用户所在免疫点没有社区那么不能查看到任何数据，这边先使用1=2过滤掉所有数据
                    //    SheQu_Code.AppendFormat("'1=2'");
                    //}
                }

                else
                {
                    //当前为普通用户角色，只能查看自己数据
                    sessionuser.RoleType = "2";
                    sessionuser.RoleType = RoleList[0].RoleType.ToString();
                    SqlStr.AppendFormat(string.Format("('{0}')", name));

                    //社区集合不为空时遍历集合获取社区代码
                    //if (IListdivisions != null)
                    //{
                    //    SheQu_Code.Append("(");
                    //    foreach (var item in IListdivisions)
                    //    {
                    //        SheQu_Code.AppendFormat("'{0}',", item.A_Name.Trim());//循环获取社区名称，目前根据社区名称过滤用户数据
                    //    }

                    //    if (IListdivisions.Count > 0)
                    //        SheQu_Code.Remove(SheQu_Code.Length - 1, 1);
                    //    else
                    //    {
                    //        //如果用户所在免疫点没有社区那么不能查看到任何数据，这边先使用1=2过滤掉所有数据
                    //        SheQu_Code.AppendFormat("'1=2'");
                    //    }
                    //    SheQu_Code.Append(")");
                    //}

                    //else
                    //{
                    //    //如果用户所在免疫点没有社区那么不能查看到任何数据，这边先使用1=2过滤掉所有数据
                    //    SheQu_Code.AppendFormat("'1=2'");
                    //}
                }
                //保存当前登录用户能查看到的用户账号
                sessionuser.FilterSql = SqlStr.ToString();
                //保存当前用户能查看到的所有社区名称（目前根据社区名称过滤用户数据）
               // sessionuser.FilterImmName = SheQu_Code.ToString();
            }

            else
            {
                //保存当前登录用户能查看到的用户账号
                sessionuser.FilterSql = string.Format("('{0}')", name);
                //保存当前用户能查看到的所有社区代码
                //如果用户所在免疫点没有社区那么不能查看到任何数据，这边先使用1=2过滤掉所有数据
                sessionuser.FilterImmName = "('1=2')";
            }
            //保存账号
            sessionuser.UserName = name;
            sessionuser.RName = PersonList[0].UserName;
           
            //保存帐号ID
            sessionuser.Id = PersonList[0].Id;
            //通过用户名查询ID
            string value = getusetID(name);
            sessionuser.MenuLeft = _ISysMenuDao.GetRole_Menu(value);//保存左边菜单数据
            sessionuser.Menulist = _ISysMenuDao.GetRole_listMenu(value);//保存菜单的action列表数据
            sessionuser.Funlist = _ISysFunctionDao.GetRole_Fun(value);//保存功能权限
            return sessionuser;
        }
        #endregion



        public  void loginsession(SessionUser user)
        {
           GetSessionuser(user.UserName);
        }


        #region //根据帐号查找Id
        public string getusetID(string name)
        {
            List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + name + "'") as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(list);
            // IList<SysPersonView> list = _ISysPersonDao.GetModelByname(user);
            string value = string.Empty;
            if (listmodel != null && listmodel.Count > 0)
            {
                value = listmodel[0].Id;
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 根据用户名称查找Id
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetUesrIdbyUserName(string UserName)
        {
            List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where UserName='" + UserName + "'") as List<SysPerson>;
            string value = string.Empty;
            if (list != null && list.Count > 0)
            {
                value = list[0].Id;
            }
            return value;
        }

        /// <summary>
        /// 条件查询获取用户数据列表
        /// </summary>
        /// <param name="LoginName">账号</param>
        /// <param name="Name">姓名</param>
        /// <returns></returns>
        #region //条件查询获取用户数据列表
        public PagerInfo<SysPersonView> GetPersonData(string LoginName, string Name, SessionUser user)
        {
             //过滤条件,默认禁止查看
            string FilterSql = " and 1=2";
            //判断权限不是超级管理员时进行限制，这边根据社区编号进行过滤
            if (user != null)
            {
                //管理员查看所有
                if (user.RoleType.Equals("0"))
                    FilterSql = " and 1=1 ";
                //非管理员
                else if (!user.RoleType.Equals("0"))
                {
                    IList<SysPerson> PersonList = base.GetList_HQL(string.Format(" where Name='{0}'", user.UserName));
                    if (PersonList != null && PersonList.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(PersonList[0].DisImmuneCenter))
                        {
                            FilterSql = string.Format(" and DisImmuneCenter='{0}' ", PersonList[0].DisImmuneCenter);
                        }
                    }
                }
            }

            TempHql = new StringBuilder();
            TempHql.Append(FilterSql);
            if (!string.IsNullOrEmpty(LoginName))
                TempHql.AppendFormat(" and Name like '%{0}%' ", LoginName);//账号模糊查询
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and UserName like '%{0}%' ", Name);//用户姓名模糊查询
            TempHql.AppendFormat(" order by Sort asc,CreateTime desc");
            PagerInfo<SysPersonView> list = Search();
            return list;
        }

        /// <summary>
        /// 设置角色下拉框值(编辑页面时)
        /// </summary>
        /// <param name="personId">需要选中的Value值</param>
        public string RoleAlbumDropdown(string personId)
        {
            ISysRoleDao _ISysDepartmentDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
            SysPerson person = null;
            //获取用户所具有角色
            List<SysRole> RoleList = null;
            if (!string.IsNullOrEmpty(personId))
            {
                person = base.GetModelById(personId);
                if (person != null && person.Role != null)
                    RoleList = person.Role.ToList<SysRole>();
            }
            //获取系统所有角色
            List<SysRoleView> AllRoleList = _ISysDepartmentDao.NGetList() as List<SysRoleView>;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (AllRoleList != null)
                {
                    foreach (var Role in AllRoleList)
                    {
                        if (RoleList != null && Role != null)
                        {
                            var temp = RoleList.Find(o => o != null && o.Id == Role.Id);
                            if (temp != null)
                                sb.Append("{id:'" + Role.Id + "',pId:'" + Role.Pid + "',name:'" + Role.Name + "',checked:true},");
                            else
                                sb.Append("{id:'" + Role.Id + "',pId:'" + Role.Pid + "',name:'" + Role.Name + "',checked:false},");
                        }
                        else
                        {
                            sb.Append("{id:'" + Role.Id + "',pId:'" + Role.Pid + "',name:'" + Role.Name + "',checked:false},");
                        }
                    }
                    if (sb.Length > 1)
                        sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="LoginId">登录账号</param>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public string UpdatePassword(string LoginId, string OldPassword, string NewPassword)
        {
            IList<SysPerson> PersonList = Session.CreateQuery(string.Format(" from SysPerson where Name='{0}' and Password='{1}'",
                LoginId, OldPassword)).List<SysPerson>();
            //原始密码输入错误
            if (PersonList == null || PersonList.Count <= 0)
                return "{result:'0'}";

            else
            {
                SysPerson person = PersonList[0];
                person.Password = NewPassword;
                person.UpdatePerson = LoginId;
                person.UpdateTime = DateTime.Now;
                if (base.Update(person))
                    return "{result:'1'}";//修改成功
                else
                    return "{result:'2'}";//修改失败
            }
        }


        #region //通过用户Id查找 免疫点Id
        public IList<SysPersonView> wx_GeteICID_byPid(string Id)
        {
            string tempHql = string.Format(" from  SysPerson  where  Id = '{0}'", Id);
            try
            {
                List<SysPerson> list = Session.CreateQuery(tempHql).List<SysPerson>() as List<SysPerson>;
                IList<SysPersonView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //根据用户Id查找用户信息 转换成Json
        public string BDGetpersonbyID(string Id)
        {
            SysPersonView model = NGetModelById(Id);//根据用户Id获取用户信息
            string json;
            if (model != null)
            {
                json = JsonConvert.SerializeObject(model);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //根据角色类型查询该类型下面的用户成员 0 超级管理员  1 管理员 2 一级代理 3 二级代理 4 三级代理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">角色类型</param>
        /// <returns></returns>
        public IList<SysPersonView> GetPersonbyRoletype(string type)
        {
            List<SysPerson> listdata = base.GetList_HQL(string.Format("  inner join fetch  u.Role as s where  s.RoleType in({0})", type)) as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(listdata);
            if (listmodel == null)
                return null;
            else
                return listmodel;
        }
        #endregion

        #region //根据角色名称查该类型下的用户成功 
        /// <summary>
        /// 根据角色名称查该类型下的用户成功
        /// </summary>
        /// <param name="Rolename">角色名称</param>
        /// <returns></returns>
        public IList<SysPersonView> GetPersonbyRolename(string Rolename)
        {
            List<SysPerson> listdata = base.GetList_HQL(string.Format("  inner join fetch  u.Role as s where  s.Name in({0})", Rolename)) as List<SysPerson>;
            IList<SysPersonView> listmodel = GetViewlist(listdata);
            if (listmodel == null)
                return null;
            else
                return listmodel;
        }
        #endregion
      
    }
}
