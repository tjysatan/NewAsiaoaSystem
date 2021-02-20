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

namespace NewAsiaOASystem.Dao
{

    public class SysMenuDao : ServiceConversion<SysMenuView, SysMenu>, ISysMenuDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} where 1=1 {1}", typeof(SysMenu).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override SysMenuView GetView(SysMenu data)
        {
            SysMenuView view = new SysMenuView();
            if (data != null)
                view = ConvertToDTO(data);
            return view;
        }




        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysMenuView model)
        {
            SysMenu listmodel = GetData(model);
            return base.insert(listmodel);
        }

        public SysMenu GetSysMenu(string id)
        {
            return base.GetModelById(id);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysMenuView model)
        {
            SysMenu listmodel = GetData(model);
            SysMenu OldModel = GetSysMenu(model.Id);
            //if (OldModel != null)
            //    listmodel.Sysbutton = OldModel.Sysbutton;
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysMenuView model)
        {
            SysMenu listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysMenuView> model)
        {
            IList<SysMenu> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysMenuView> NGetList()
        {
            List<SysMenu> listdata =
                Session.CreateQuery(" from SysMenu order by CreateTime desc").List<SysMenu>().ToList<SysMenu>();
            IList<SysMenuView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<SysMenuView> NGetList_id(string id)
        {
            List<SysMenu> list = base.GetList_id(id) as List<SysMenu>;
            IList<SysMenuView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<SysMenu> NGetListID(string id)
        {
            IList<SysMenu> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回json数据</returns>
        public string NGetListJson_id(string id)
        {
            return JsonConvert.SerializeObject(NGetList_id(id));
        }

        /// <summary>
        /// 根据多个ID  查询多条数据并返回树形菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回json数据</returns>
        public string NGetListJsonTree_id(string id)
        {
            List<SysMenuView> listView = NGetList_id(id) as List<SysMenuView>;
            Base<SysMenuView> _Base = new Base<SysMenuView>();
            string jsonTree = _Base.AddNode(listView, "Id", "PId", null, "Name", 1);
            return jsonTree;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysMenuView NGetModelById(string id)
        {
            SysMenuView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 获取树形菜单数据
        /// </summary>
        /// <returns></returns>
        public string GetMenuTreeData()
        {
            string HQL = "Sort asc";
            List<SysMenu> list = base.GetList_orderby(HQL) as List<SysMenu>;
            List<SysMenuView> listView = GetViewlist(list) as List<SysMenuView>;
            Base<SysMenuView> _Base = new Base<SysMenuView>();
            string str = _Base.AddNode(listView, "Id", "PId", null, "Name", 1);
            return str;
        }

        /// <summary>
        /// 获取树形菜单与按钮数据
        /// </summary>
        /// <returns></returns>
        public string GetMenuButtonTreeData(string menuId)
        {
            List<SysMenu> list = base.GetList_id(menuId) as List<SysMenu>;
            List<SysMenuView> listView = GetViewlist(list) as List<SysMenuView>;
            Base<SysMenuView> _Base = new Base<SysMenuView>();
            string[] pars = new[] { "Name", "Sort", "CreatePerson", "CreateTime", "Sysbutton" };
            string str = _Base.AddNode(listView, "Id", "PId", null, 1, pars);
            return str;
        }
        /// <summary>
        /// 获取左侧导航的菜单
        /// </summary>
        /// <returns></returns>
        public string GetleftnavMenu(string Id)
        {
            string HQL;
            if (Id != null)
            {
                Id = "'" + Id.Replace(",", "','") + "'";
                HQL = "where u.Status=1  Id not in(" + Id + ") order by Sort asc";
            }
            else
            {
                HQL = "where u.Status=1 order by Sort asc";
            }
            List<SysMenu> list = base.GetList_HQL(HQL) as List<SysMenu>;
            List<SysMenuView> listView = GetViewlist(list) as List<SysMenuView>;
            if (listView == null)
                return "[]";
            Base<SysMenuView> _Base = new Base<SysMenuView>();
            string[] pars = new[] { "Name", "Url", "Ico" };
            string str = _Base.AddNode(listView, "Id", "PId", null, 1, pars);
            return str;
        }


        /// <summary>
        /// 获取菜单数据列表
        /// </summary>
        /// <returns></returns>
        public string GetDatelistMenu()
        {
            List<SysMenu> list = base.GetList() as List<SysMenu>;
            List<SysMenuView> listView = GetViewlist(list) as List<SysMenuView>;
            Base<SysMenuView> _Base = new Base<SysMenuView>();
            string[] pars = new[] { "Name", "Sort", "Description", "Url", "CreateTime", "Ico", "Status" };
            string str = _Base.AddNode(listView, "Id", "PId", null, 1, pars);
            return str;
        }


        #region 根据用户ID 获取用户角色对应的 菜单权限
        /// <summary>
        /// 根据用户ID 获取用户角色对应的 菜单权限
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns>返回菜单列表</returns>
        public string GetRole_Menu(string Id)
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Id);
            List<SysMenuView> MenuView = new List<SysMenuView>();
            IList<SysRole> RoleList = sysp.Role;

            //权限判定
            if (RoleList != null && RoleList.ToList<SysRole>().Find(x => x != null && Convert.ToInt32(x.RoleType) == 0) != null)
            {
                //管理员直接查询所有菜单
                List<SysMenu> Menulist = base.GetList().ToList<SysMenu>();
                if (GetViewlist(Menulist) != null)
                {
                    MenuView = GetViewlist(Menulist).ToList<SysMenuView>();
                    MenuView.RemoveAll(x => x != null && x.Name == "区域查询");
                }

            }
            else
            {
                List<SysMenu> Menulist = new List<SysMenu>();
                if (RoleList != null)
                    foreach (var Role in RoleList)
                    {
                        if (Role != null)
                        {
                            Menulist.AddRange(Role.SysMenu);
                        }
                    }

                if (GetViewlist(Menulist) != null)
                    MenuView = GetViewlist(Menulist).ToList<SysMenuView>();
            }
            string menuStr = GetMenuList(MenuView, "", 1);
            return menuStr;
        }
        #endregion

        #region 获取用户角色对应的菜单list
        public List<string> GetRole_listMenu(string Id)
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Id);
            IList<SysRole> RoleList = sysp.Role;
            List<SysMenu> Menulist = new List<SysMenu>();
            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null)
                        Menulist.AddRange(Role.SysMenu);
                }
            List<string> list = new List<string>();
            if (Menulist != null)
            {
                foreach (var Menu in Menulist)
                {
                    if (Menu != null)
                        list.Add(Menu.Url);
                }
            }
            return list;

        }
        #endregion


        /// <summary>
        /// 树形菜单
        /// </summary>
        /// <param name="List">集合</param>
        /// <param name="Pid">上级ID</param>
        /// <param name="d">开始深度，默认为1</param>
        /// <returns></returns>
        public string GetMenuList(List<SysMenuView> MenuList, string Pid, int d)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<SysMenuView> tempList = new List<SysMenuView>();
                tempList = MenuList.FindAll(x => x.PId == Pid);

                if (tempList != null)
                {
                    //冒泡排序
                    for (int i = 0, j = tempList.Count; i < j; i++)
                    {
                        for (int k = 0; k < j - i - 1; k++)
                        {
                            SysMenuView menu = new SysMenuView();
                            if (tempList[k].Sort > tempList[k + 1].Sort)
                            {
                                menu = tempList[k];
                                tempList[k] = tempList[k + 1];
                                tempList[k + 1] = menu;
                            }
                        }
                    }

                    int h = 0;
                    foreach (var item in tempList)
                    {
                        if (h == 0)//如果是某一层的开始，需要“[”开始  
                        {
                            if (d == 1)//如果深度为null或"",即第一层  
                                sb.Append("[");
                            else//否则，为第二层或更深  
                                sb.Append(",'children':[");
                        }

                        else
                        {
                            sb.Append(",");
                        }

                        sb.Append("{");
                        sb.Append("'Id\':'").Append(item.Id).Append("',");
                        sb.Append("'Name\':'").Append(item.Name).Append("',");
                        sb.Append("'Url\':'").Append(item.Url).Append("',");
                        sb.Append("'Ico\':'").Append(item.Ico).Append("',");
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(GetMenuList(MenuList, item.Id, d + 1));//递归  
                        sb.Append("}");
                        h = h + 1;
                        if (tempList.Count == h)//如果某一层到了末尾,需要"]"结束  
                            sb.Append("]");
                    }
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "[]";
            }
        }

        /// <summary>
        /// 菜单条件查询
        /// </summary>
        /// <param name="Name">菜单名称</param>
        /// <returns></returns>
        public PagerInfo<SysMenuView> GetPageList(string Name)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and Name like '%{0}%' ", Name);
            TempHql.AppendFormat(" order by CreateTime desc");
            PagerInfo<SysMenuView> list = Search();
            return list;
        }

        public override PagerInfo<SysMenuView> Search()
        {
            ISession session = GetSession();
            string hql = GetSearchHql();
            string tempHql = hql;
            ///得到第一个from
            int inde = tempHql.IndexOf("from");

            if (tempHql.Contains("group"))
            {
                int inde1 = tempHql.IndexOf("group");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("GROUP"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("GROUP");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("order"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("order");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("ORDER"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("ORDER");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else
            {
                tempHql = tempHql.Substring(inde);
            }

            IQuery queryCount = session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            //计算总记录数       
            pager.RecordCount = Convert.ToInt32(queryCount.UniqueResult());
            IQuery query = session.CreateQuery(hql);
            //计算起始的行
            int index = (pager.CurrentPageIndex - 1) * pager.PageSize;
            if (index > pager.RecordCount)
            {
                index = 0;
                pager.CurrentPageIndex = 1;
            }
            query.SetFirstResult(index);
            query.SetMaxResults(pager.PageSize);
            IList<SysMenu> tempData = query.List<SysMenu>();
            pager.DataList = (tempData as List<SysMenu>).ConvertAll(x => GetView(x));
            pager.GetPagingDataJson = JsonConvert.SerializeObject(pager.DataList);
            return pager;
        }

    }

}
