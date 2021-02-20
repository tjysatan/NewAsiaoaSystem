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
    public class EP_jlinfoDao:ServiceConversion<EP_jlinfoView,EP_jlinfo>,IEP_jlinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(EP_jlinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override EP_jlinfoView GetView(EP_jlinfo data)
        {
            EP_jlinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(EP_jlinfoView model)
        {
            EP_jlinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(EP_jlinfoView model)
        {
            EP_jlinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(EP_jlinfoView model)
        {
            EP_jlinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<EP_jlinfoView> model)
        {
            IList<EP_jlinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<EP_jlinfoView> NGetList()
        {
            List<EP_jlinfo> listdata = base.GetList() as List<EP_jlinfo>;
            IList<EP_jlinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<EP_jlinfoView> NGetList_id(string id)
        {
            List<EP_jlinfo> list = base.GetList_id(id) as List<EP_jlinfo>;
            IList<EP_jlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<EP_jlinfo> NGetListID(string id)
        {
            IList<EP_jlinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EP_jlinfoView NGetModelById(string id)
        {
            EP_jlinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //保存后返回ID
        public string InsertID(EP_jlinfoView modelView)
        {
            EP_jlinfo model = GetData(modelView);
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

        public PagerInfo<EP_jlinfoView> GetCinfoList(string Name, string lxrname, string RL_is, string Stratrldate, string Endrldate, string dyStratrldate, string dyEndrldate,string kdgs,string K3DDNO, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.SjId in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
            if (!string.IsNullOrEmpty(lxrname))
                TempHql.AppendFormat(" and (u.SjId in(select Id from NACustomerinfo where LxrName like '%{0}%') or u.SjaddId in (select Id from NA_AddresseeInfo where Aname like '%{0}%'))", lxrname);
            if (!string.IsNullOrEmpty(RL_is))
                TempHql.AppendFormat(" and u.RL_Is like '%{0}%' ", RL_is);
            if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
                TempHql.AppendFormat("and RL_datetime>='{0}' and RL_datetime<='{1}'", Stratrldate + " 00:00:00", Endrldate+" 23:59:59");
            if (!string.IsNullOrEmpty(dyStratrldate) && !string.IsNullOrEmpty(dyEndrldate))
                TempHql.AppendFormat("and Jjdatetime>='{0}' and Jjdatetime<='{1}'", dyStratrldate + " 00:00:00", dyEndrldate + " 23:59:59");
            if (!string.IsNullOrEmpty(kdgs))
                TempHql.AppendFormat(" and Kdgs='{0}'",kdgs);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat("and u.JjId  in ('{0}')", user.Id);
            }
            if (!string.IsNullOrEmpty(K3DDNO))
                TempHql.AppendFormat(" and k3_ddno='{0}'",K3DDNO);
            TempHql.AppendFormat(" and IsDis='0'");
            TempHql.AppendFormat(" order by RL_datetime desc, Jjdatetime desc");
            PagerInfo<EP_jlinfoView> list = Search();
            return list;
        }

        //导出数据查询
        public IList<EP_jlinfoView> GetEP_JLINFExportJson(string Name, string RL_is, string Stratrldate, string Endrldate, string dyStratrldate, string dyEndrldate, string kdgs,string K3DDNO, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.SjId in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
            if (!string.IsNullOrEmpty(RL_is))
                TempHql.AppendFormat(" and u.RL_Is like '%{0}%' ", RL_is);
            if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
                TempHql.AppendFormat("and RL_datetime>='{0}' and RL_datetime<='{1}'", Stratrldate + " 00:00:00", Endrldate + " 23:59:59");
            if (!string.IsNullOrEmpty(dyStratrldate) && !string.IsNullOrEmpty(dyEndrldate))
                TempHql.AppendFormat("and Jjdatetime>='{0}' and Jjdatetime<='{1}'", dyStratrldate + " 00:00:00", dyEndrldate + " 23:59:59");
            if (!string.IsNullOrEmpty(kdgs))
                TempHql.AppendFormat(" and Kdgs='{0}'", kdgs);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat("and u.JjId  in ('{0}')", user.Id);
            }
            if (!string.IsNullOrEmpty(K3DDNO))
                TempHql.AppendFormat(" and k3_ddno='{0}'", K3DDNO);
            TempHql.AppendFormat(" and IsDis='0'");
            TempHql.AppendFormat(" order by Jjdatetime desc");
            //string sqstr = string.Format("from EP_jlinfo where 1=1 {0}}", TempHql.ToString());
            string HQLstr = string.Format("from EP_jlinfo u where 1=1 {0}", TempHql.ToString());
            List<EP_jlinfo> list = HibernateTemplate.Find<EP_jlinfo>(HQLstr) as List<EP_jlinfo>;
            IList<EP_jlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        #region //根据多个Id查找多条打印记录根据答应寄件时间排序
        public IList<EP_jlinfoView> GetlistIdEp_jl(string Id)
        {
            string tempHql = string.Format(" from  EP_jlinfo  where  Id in ({0}) and IsDis='0'  order by Jjdatetime desc", Id);
            try
            {
                List<EP_jlinfo> list = Session.CreateQuery(tempHql).List<EP_jlinfo>() as List<EP_jlinfo>;
                IList<EP_jlinfoView> listmodel = GetViewlist(list);
                return listmodel;

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        //根据DD_Id查询打印记录
        #region //根据DD_Id查询打印记录
        public EP_jlinfoView GetEP_jlmodelbydd_Id(string dd_Id)
        {
            string sqstr = string.Format("from EP_jlinfo where DDId={0} and IsDis='0'", dd_Id);
            List<EP_jlinfo> list = HibernateTemplate.Find<EP_jlinfo>(sqstr) as List<EP_jlinfo>;
            IList<EP_jlinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        } 
        #endregion

        #region //根据寄件时间查询打印记录
        /// <summary>
        /// 根据寄件时间查询打印记录
        /// </summary>
        /// <param name="starttime">寄件时间</param>
        /// <param name="endtime">寄件时间</param>
        /// <param name="user">当前登录的角色</param>
        /// <returns></returns>
        public IList<EP_jlinfoView> GetEP_JLinfobyjjtime(string starttime, string endtime, SessionUser user)
        {
            string HQLstr;
            if (user.RoleType != "0")
            {
                HQLstr = string.Format(" from EP_jlinfo where  Jjdatetime>='{0}' and Jjdatetime<='{1}' and JjId='{2}' and RL_Is='2' and IsDis='0');", starttime + " 00:00:00", endtime + " 23:59:59", user.Id);
            }
            else
            {
                HQLstr = string.Format(" from EP_jlinfo where  Jjdatetime>='{0}' and Jjdatetime<='{1}' and RL_Is='2' and  IsDis='0'", starttime + " 00:00:00", endtime + " 23:59:59");
            }
            List<EP_jlinfo> list = HibernateTemplate.Find<EP_jlinfo>(HQLstr) as List<EP_jlinfo>;
            IList<EP_jlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion


    }
}
