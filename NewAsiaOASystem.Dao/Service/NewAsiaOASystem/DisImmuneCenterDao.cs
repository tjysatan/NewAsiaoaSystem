using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using NHibernate;
using Newtonsoft.Json;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    /// <summary>
    /// 部门业务处理
    /// tjy_satan
    /// </summary>
    public class DisImmuneCenterDao : ServiceConversion<DisImmuneCenterView, DisImmuneCenter>, IDisImmuneCenterDao
    {
        #region 数据转换及参数定义
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DisImmuneCenter).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DisImmuneCenterView GetView(DisImmuneCenter data)
        {
            DisImmuneCenterView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// DATA 转换成 TDO（只给分页的时候使用，这样可以避免与其他页面只获取免疫点部分数据冲突
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DisImmuneCenterView GetPageView(DisImmuneCenter data)
        {
            DisImmuneCenterView view = ConvertToDTO(data);
            //if (data != null && data.ListCommunity != null)
            //{
            //    view.ListCommunity = JsonConvert.SerializeObject(data.ListCommunity);
            //}
            return view;
        }

        /// <summary>
        /// TDO 转换成 DATA
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public override DisImmuneCenter GetData(DisImmuneCenterView view)
        {
            DisImmuneCenter data = new DisImmuneCenter();
            data = ConvertToData(view);
            return data;
        }

        #endregion

        #region 增删改
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DisImmuneCenterView model)
        {
            try
            {
    
             DisImmuneCenter listmodel = GetData(model);
         if (base.insert(listmodel))
          {
                    

                return true;
              }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DisImmuneCenterView model)
        {
        
            DisImmuneCenter listmodel = GetData(model);
            if (base.Update(listmodel))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DisImmuneCenterView model)
        {
            DisImmuneCenter listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 传入集合进行批量删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DisImmuneCenterView> model)
        {
            IList<DisImmuneCenter> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DisImmuneCenterView> NGetList()
        {
            List<DisImmuneCenter> listdata = base.GetList() as List<DisImmuneCenter>;
            IList<DisImmuneCenterView> listmodel = GetViewlist(listdata.FindAll(x => x.Status == 1));
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DisImmuneCenterView> NGetList_id(string id)
        {
            try
            {
                List<DisImmuneCenter> list = base.GetList_id(id) as List<DisImmuneCenter>;
                IList<DisImmuneCenterView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DisImmuneCenter> NGetList_idData(string id)
        {
            List<DisImmuneCenter> list = base.GetList_id(id) as List<DisImmuneCenter>;
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DisImmuneCenterView NGetModelById(string id)
        {
            DisImmuneCenterView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DisImmuneCenter GetModelById(string id)
        {
            DisImmuneCenter listmodel = base.GetModelById(id);
            return listmodel;
        }

        /// <summary>
        /// 根据父ID递归查找所有子ID
        /// </summary>
        /// <param name="ID">父ID</param>
        //private void GetAllNodes(string ID, List<Administrative_divisionsView> commList)
        //{
        //    List<Administrative_divisionsView> commNodeList = commList.FindAll(o => o.A_PId == ID);
        //    if (commNodeList == null || commNodeList.Count <= 0)
        //        return;
        //    foreach (var item in commNodeList)
        //    {
        //        if (item != null)
        //        {
        //            TempList.Add(item.ImmId);
        //            GetAllNodes(item.Id, commList);
        //        }
        //    }
        //}
        /// <summary>
        /// 免疫点条件查询
        /// </summary>
        /// <param name="Name">免疫点名称</param>
        /// <param name="ComId">社区ID</param>
        /// <returns></returns>
        public PagerInfo<DisImmuneCenterView> GetIcList(string Name, string ComId, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
            if (!string.IsNullOrEmpty(ComId))
            {
            //    IAdministrative_divisionsDao _IAdministrative_divisionsDao =
            //      ContextRegistry.GetContext().GetObject("Administrative_divisionsDao") as IAdministrative_divisionsDao;
            //    List<Administrative_divisionsView> commList = _IAdministrative_divisionsDao.NGetList() as List<Administrative_divisionsView>;
            //    //递归获取所有子ID并保存到全集变量TempList中
            //    GetAllNodes(ComId, commList);
            //    //获取选中的社区记录
            //    Administrative_divisionsView comView = commList.Find(o => o.Id == ComId);
                //将获取到的社区记录添加到TempList变量中去，因为递归的时候并没有将该条社区的免疫点ID添加进去
                //if (comView != null && comView.ImmId != null)
                //    TempList.Add(comView.ImmId);
                //将该免疫点ID用逗号隔开
                string CommID = "'";
                CommID = CommID + string.Join("','", TempList.ToArray());
                CommID = CommID + "'";
                TempHql.AppendFormat(" and u.Id in ({0}) ", CommID);
            }

            //过滤条件,默认禁止查看
            string FilterSql = " and 1=2";
            //判断权限不是超级管理员时进行限制，这边根据社区编号进行过滤
            if (user != null)
            {
                string sql = string.Format("select ImmCenterId from SysPerson where 1=1 and Name='{0}'", user.UserName);
                //管理员查看所有
                if (user.RoleType.Equals("0"))
                    FilterSql = " and 1=1 ";
                //非管理员
                else if (!user.RoleType.Equals("0"))
                {
                    IList<object> ImmuneIdList = Session.CreateSQLQuery(sql).List<object>();
                    if (ImmuneIdList != null && ImmuneIdList.Count > 0)
                        FilterSql = string.Format(" and Id in ('{0}') ", Convert.ToString(ImmuneIdList[0]));
                }
            }

            TempHql.Append(FilterSql);
            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<DisImmuneCenterView> list = Search();
            return list;
        }
        #endregion

        #region 设置下拉框值
        /// <summary>
        /// 设置下拉框值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public string AlbumDropdown(string CommId, SessionUser user)
        {
           // try
           // {
               // IAdministrative_divisionsDao _IAdministrative_divisionsDao = ContextRegistry.GetContext().GetObject("Administrative_divisionsDao") as IAdministrative_divisionsDao;
               // List<Administrative_divisions> SelectADList = null;
               // if (!string.IsNullOrEmpty(CommId))
               // {
                    //定义变量保存已选中的社区ID
                    //SelectADList = _IAdministrative_divisionsDao.NGetList_HQL(string.Format(" where ImmId='{0}'", CommId)) as List<Administrative_divisions>;
               // }

                //过滤条件,默认禁止查看
             //  string FilterSql = " and 1=2";
                //判断权限不是超级管理员时进行限制，这边根据社区编号进行过滤
               // if (user != null)
               // {
                    //管理员查看所有
                   // if (user.RoleType.Equals("0"))
                     //   FilterSql = " and 1=1 ";
                    //非管理员
                    //else if (!user.RoleType.Equals("0") && !string.IsNullOrEmpty(user.FilterImmName))
                        //FilterSql = string.Format(" and A_Name in {0} ", user.FilterImmName);
               // }

             //   IList<Administrative_divisions> getADList =
                  // Session.CreateQuery(string.Format(" from Administrative_divisions where 1=1 {0} and State=1 ", FilterSql)).List<Administrative_divisions>();

               // StringBuilder sb = new StringBuilder();
               // sb.Append("[");
               // if (getADList != null)
                //{
                   // foreach (var item in getADList)
                    //{
                       // if (item != null)
                       // {
                            //表示改免疫点存在绑定过的社区
                           // if (SelectADList != null)
                           // {
                              //  var temp = SelectADList.Find(x => x.Id == item.Id);
                               // if (temp != null)
                                  //  sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',checked:true},");
                               // else
                                   // sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',checked:false},");
                          //  }

                            //else
                            //{
                              //  sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',checked:false},");
                           // }
                       // }
                  //  }

                   // if (sb.Length > 1)
                       // sb.Remove(sb.Length - 1, 1);
                //}
               // sb.Append("]");
                //return sb.ToString();
          //  }

          //  catch
           // {
                return "";
           // }
        }

        #endregion

        #region 获取社区值
        /// <summary>
        /// 获取社区值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        //public string GetSheQu(SessionUser user)
        //{
        //    try
        //    {
        //        StringBuilder hql = new StringBuilder();
        //        hql.Append(" from Administrative_divisions u where 1=1 ");

        //        //判断权限不是超级管理员时进行限制
        //        if (user != null)
        //        {
        //            if (!user.RoleType.Equals("0") && !string.IsNullOrEmpty(user.FilterImmName))
        //            {
        //                hql.AppendFormat(" and u.A_Name in {0}", user.FilterImmName);
        //            }
        //        }

        //        IList<Administrative_divisions> ImmuneIList = Session.CreateQuery(hql.ToString()).List<Administrative_divisions>();
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("[");
        //        if (ImmuneIList != null)
        //        {
        //            foreach (var item in ImmuneIList)
        //            {
        //                if (item != null)
        //                {
        //                    sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',Code:'" + item.A_Code + "',checked:false},");
        //                    //if (!string.IsNullOrEmpty(item.A_PId))
        //                    //sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',Code:'" + item.A_Code + "',checked:false},");
        //                    //else
        //                    //    sb.Append("{id:'" + item.Id + "',pId:'" + item.A_PId + "',name:'" + item.A_Name + "',Code:'" + item.A_Code + "',checked:false, nocheck:true},");
        //                }
        //            }

        //            if (sb.Length > 1)
        //                sb.Remove(sb.Length - 1, 1);
        //        }
        //        sb.Append("]");
        //        return sb.ToString();
        //    }

        //    catch
        //    {
        //        return "";
        //    }
        //}

        #endregion

        public override PagerInfo<DisImmuneCenterView> Search()
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
            IList<DisImmuneCenter> tempData = query.List<DisImmuneCenter>();
            pager.DataList = (tempData as List<DisImmuneCenter>).ConvertAll(x => GetPageView(x));
            pager.GetPagingDataJson = JsonConvert.SerializeObject(pager.DataList);
            return pager;
        }


    }
}
