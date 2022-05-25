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
    /// 返退单业务处理
    /// tjy_Satan
    /// </summary>
    public class NAReturnListDao : ServiceConversion<NAReturnListView, NAReturnList>, INAReturnListDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NAReturnList).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NAReturnListView GetView(NAReturnList data)
        {
            NAReturnListView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NAReturnListView model)
        {
            NAReturnList listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NAReturnListView model)
        {
            NAReturnList listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NAReturnListView model)
        {
            NAReturnList listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NAReturnListView> model)
        {
            IList<NAReturnList> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NAReturnListView> NGetList()
        {
            List<NAReturnList> listdata = base.GetList() as List<NAReturnList>;
            IList<NAReturnListView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NAReturnListView> NGetList_id(string id)
        {
            List<NAReturnList> list = base.GetList_id(id) as List<NAReturnList>;
            IList<NAReturnListView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NAReturnList> NGetListID(string id)
        {
            IList<NAReturnList> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NAReturnListView NGetModelById(string id)
        {
            NAReturnListView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        /// <summary>
        /// 返退货列表数据
        /// </summary>
        /// <param name="Name">客户名称</param>
        /// <param name="type">所需要的数据 0 全部的数据 1待维修及以后的状态的数据 2 待定责及以后状态的数据 3待处理及以后状态的数据 4 待审核及以后的数据,5 更具状态查询</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        public PagerInfo<NAReturnListView> GetCinfoList(string Name, string Szt, string type, string R_pId,string fthbianhao,string CPname,string Isjsbpz, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
            if (!string.IsNullOrEmpty(R_pId)) 
                TempHql.AppendFormat(" and u.R_pId='{0}'", R_pId);
            if (!string.IsNullOrEmpty(fthbianhao))
                TempHql.AppendFormat("and u.fthbianhao like '%{0}%'", fthbianhao);
            if (!string.IsNullOrEmpty(CPname))
                TempHql.AppendFormat(" and u.Id in (select R_Id from NQ_Thdetailinfo where P_Id in (select Id from NQ_productinfo where Pname like '%{0}%'))",CPname);
            if (!string.IsNullOrEmpty(Szt))
            {
                TempHql.AppendFormat("and u.L_type in ({0})", Szt);
            }
            else
            {
                if (type == "1")
                    TempHql.AppendFormat("and u.L_type in(2,3,4,5,6,7)");//待维修及已经维修好的数据
                if (type == "2")
                    TempHql.AppendFormat("and u.L_type in(3,4,5,6)");//待定责以及以后的数据
                if (type == "3")
                    TempHql.AppendFormat("and u.L_type in(4,5,6)");//待处理及处理好的数据
                if (type == "4")
                    TempHql.AppendFormat("and u.L_type in(5,6)");//待审核及审核好的数据

            }
            if (!string.IsNullOrEmpty(Isjsbpz))
                TempHql.AppendFormat(" and u.Isjsbpz='{0}'", Isjsbpz);
            TempHql.AppendFormat("and u.Status='0'");
            TempHql.AppendFormat("order by u.Sort asc,CreateTime desc");
            PagerInfo<NAReturnListView> list = Search();
            return list;
        }

        #region //查询反退货订单的数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Szt"></param>
        /// <param name="type"></param>
        /// <param name="R_pId"></param>
        /// <param name="fthbianhao"></param>
        /// <param name="CPname"></param>
        /// <param name="Isjsbpz"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<NAReturnListView> Getdatelist(string Name, string Szt, string type, string R_pId, string fthbianhao, string CPname, string Isjsbpz, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
            if (!string.IsNullOrEmpty(R_pId))
                TempHql.AppendFormat(" and u.R_pId='{0}'", R_pId);
            if (!string.IsNullOrEmpty(fthbianhao))
                TempHql.AppendFormat("and u.fthbianhao like '%{0}%'", fthbianhao);
            if (!string.IsNullOrEmpty(CPname))
                TempHql.AppendFormat(" and u.Id in (select R_Id from NQ_Thdetailinfo where P_Id in (select Id from NQ_productinfo where Pname like '%{0}%'))", CPname);
            if (!string.IsNullOrEmpty(Szt))
            {
                TempHql.AppendFormat("and u.L_type in ({0})", Szt);
            }
            else
            {
                if (type == "1")
                    TempHql.AppendFormat("and u.L_type in(2,3,4,5,6,7)");//待维修及已经维修好的数据
                if (type == "2")
                    TempHql.AppendFormat("and u.L_type in(3,4,5,6)");//待定责以及以后的数据
                if (type == "3")
                    TempHql.AppendFormat("and u.L_type in(4,5,6)");//待处理及处理好的数据
                if (type == "4")
                    TempHql.AppendFormat("and u.L_type in(5,6)");//待审核及审核好的数据

            }
            if (!string.IsNullOrEmpty(Isjsbpz))
            TempHql.AppendFormat(" and u.Isjsbpz='{0}'", Isjsbpz);
            TempHql.AppendFormat("and u.Status='0'");
            TempHql.AppendFormat("order by u.Sort asc,CreateTime desc");
            string HQLstr = string.Format("from NAReturnList u where 1=1 {0}", TempHql.ToString());
            List<NAReturnList> list = HibernateTemplate.Find<NAReturnList>(HQLstr) as List<NAReturnList>;
            IList<NAReturnListView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //可以开单出货的数据
        public PagerInfo<NAReturnListView> Getchinfolist(string name,string bianhao, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
        
            if (user.RoleType == "0")
            {
                if (!string.IsNullOrEmpty(name))
                    TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' )", name);
                if (!string.IsNullOrEmpty(bianhao))
                    TempHql.AppendFormat(" and u.fthbianhao like'%{0}%'",bianhao);
                TempHql.AppendFormat("and u.L_type in(6)");//查找全部已经完成的数据
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                    // TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' ) and CreatePerson='{1}'", name, user.Id);
                    TempHql.AppendFormat(" and u.C_Id in (select Id from NACustomerinfo where Name like '%{0}%')",name);
                if (!string.IsNullOrEmpty(bianhao))
                    TempHql.AppendFormat(" and u.fthbianhao like '%{0}%'",bianhao);
                TempHql.AppendFormat("and u.L_type in(6) and Kfzy='{0}'", user.Id);//查找全部已经完成的数据
            }
            TempHql.AppendFormat("order by u.Sort asc,CreateTime desc");
            PagerInfo<NAReturnListView> list = Search();
            return list;

        } 
        #endregion
        
        #region //返退货 出货品保确认数据
        public PagerInfo<NAReturnListView> Getchqrinfolist(string name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (user.RoleType == "0")
            {
                if (!string.IsNullOrEmpty(name))
                    TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' ) and u.L_type in(6)", name);
                TempHql.AppendFormat("and u.L_type in(6) and u.NQ_Type in (1,2,3) ");//查找全部已经完成的数据 且待确认的数据
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                    TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' ) and u.L_type in(6) NQ_Type in(1,2,3) ", name);
                TempHql.AppendFormat("and u.L_type in(6) and NQ_Type in(1,2,3) ");//查找全部已经完成的数据
            }

            TempHql.AppendFormat("order by u.Sort asc,CreateTime desc");
            PagerInfo<NAReturnListView> list = Search();
            return list;
        } 
        #endregion

        #region //返退货 出货 仓库出库数据
        public PagerInfo<NAReturnListView> Getckchinfolist(string name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (user.RoleType == "0")
            {
                if (!string.IsNullOrEmpty(name))
                    TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' ) and u.L_type in(6)", name);
                TempHql.AppendFormat("and u.L_type in(6) and u.NQ_Type in (2,3) ");//查找全部已经完成的数据 且待确认的数据
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                    TempHql.AppendFormat(" and u.C_Id in(select Id from NACustomerinfo where Name like '%{0}%' ) and u.L_type in(6) NQ_Type in(1,2,3) ", name);
                TempHql.AppendFormat("and u.L_type in(6) and NQ_Type in(2,3) ");//查找全部已经完成的数据 其待出货的数据
            }

            TempHql.AppendFormat("order by u.Sort asc,CreateTime desc");
            PagerInfo<NAReturnListView> list = Search();
            return list;
        } 
        #endregion

        #region //保存后返回ID
        public string InsertID(NAReturnListView modelView)
        {
            NAReturnList model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        /// <summary>
        /// 查询当天返退货的条数
        /// </summary>
        /// <returns></returns>
        public int GetPPcount()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd");
                string temHql = string.Format("from NAReturnList where CreateTime>='{0}' and CreateTime<='{1}'", datetime + " 00:00:00", datetime + " 23:59:59");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }



    }
}
