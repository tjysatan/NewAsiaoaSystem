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
    public class PP_ProfitpointinfoDao:ServiceConversion<PP_ProfitpointinfoView,PP_Profitpointinfo>,IPP_ProfitpointinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_Profitpointinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_ProfitpointinfoView GetView(PP_Profitpointinfo data)
        {
            PP_ProfitpointinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_ProfitpointinfoView model)
        {
            PP_Profitpointinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_ProfitpointinfoView model)
        {
            PP_Profitpointinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_ProfitpointinfoView model)
        {
            PP_Profitpointinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_ProfitpointinfoView> model)
        {
            IList<PP_Profitpointinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_ProfitpointinfoView> NGetList()
        {
            List<PP_Profitpointinfo> listdata = base.GetList() as List<PP_Profitpointinfo>;
            IList<PP_ProfitpointinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_ProfitpointinfoView> NGetList_id(string id)
        {
            List<PP_Profitpointinfo> list = base.GetList_id(id) as List<PP_Profitpointinfo>;
            IList<PP_ProfitpointinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_Profitpointinfo> NGetListID(string id)
        {
            IList<PP_Profitpointinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_ProfitpointinfoView NGetModelById(string id)
        {
            PP_ProfitpointinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //收入业务分页数据

        /// <summary>
        /// 盈利业务分页数据
        /// </summary>
        /// <param name="rwname">业务名称</param>
        /// <param name="temname">团队名称</param>
        /// <param name="protype">收入业务类型 0 个人收入项 1 团队收入项</param>
        /// <returns></returns>
        public PagerInfo<PP_ProfitpointinfoView> Getprofitpage(string rwname, string temname,string protype, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(rwname))
                TempHql.AppendFormat(" and u.Rwname like '%{0}%'", rwname);
            if (!string.IsNullOrEmpty(temname))
                TempHql.AppendFormat(" and u.Rw_teamId ='{0}'", temname);
            if (!string.IsNullOrEmpty(protype))
                TempHql.AppendFormat(" and u.ProType='{0}'",protype);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat(" and u.Rw_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')",user.Id);
            }
            TempHql.AppendFormat(" and state='1'");
            TempHql.AppendFormat(" and type='0'");
            TempHql.AppendFormat("order by Sort desc,C_time");
            PagerInfo<PP_ProfitpointinfoView> list = Search();
            return list;

        }
        #endregion

        #region //根据团队Id 查找属于该团队的(个人/团队)收入项数据
        /// <summary>
        /// 根据团队Id 查找属于该团队的（个人/团队）收入业务数据
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="protype">收入项的类别</protype>
        /// <returns></returns>
        public IList<PP_ProfitpointinfoView> GetProfitinfobyteamId(string teamId,string protype)
        {
            string sql = string.Format("from PP_Profitpointinfo where Rw_teamId='{0}' and type='0' and ProType='{1}' order by Sort", teamId,protype);
            List<PP_Profitpointinfo> list = HibernateTemplate.Find<PP_Profitpointinfo>(sql) as List<PP_Profitpointinfo>;
            IList<PP_ProfitpointinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //支出项分页数据
        /// <summary>
        /// 支出项分页数据
        /// </summary>
        /// <param name="rwname">业务名称</param>
        /// <param name="temname">团队名称</param>
        /// <param name="protype">支出业务类型 0 个人支出项 1 团队支出项</param>
        /// <returns></returns>
        public PagerInfo<PP_ProfitpointinfoView> Getprrofitzhichupage(string rwname, string temname,string protype, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(rwname))
                TempHql.AppendFormat(" and u.Rwname like '%{0}%'", rwname);
            if (!string.IsNullOrEmpty(temname))
                TempHql.AppendFormat(" and u.Rw_teamId ='{0}'", temname);
            if (!string.IsNullOrEmpty(protype))
                TempHql.AppendFormat(" and u.ProType='{0}'", protype);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat(" and u.Rw_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')", user.Id);
            }
            TempHql.AppendFormat(" and state='1'");
            TempHql.AppendFormat(" and type='1'");
            TempHql.AppendFormat("order by Sort desc,C_time");
            PagerInfo<PP_ProfitpointinfoView> list = Search();
            return list;
        }
        #endregion

        #region //个人收入和支出的项目的分页数据
        /// <summary>
        /// 个人收入和支出的项目的分页数据
        /// </summary>
        /// <param name="name">项目名称</param>
        /// <param name="stafId">个人（员工）Id</param>
        /// <param name="type">收入0或者支出1</param>
        /// <returns></returns>
        public PagerInfo<PP_ProfitpointinfoView> GetgerenProfitpage(string name, string stafId, string type)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(name))
                TempHql.AppendFormat(" and u.Rwname like '%{0}%'", name);
            if (!string.IsNullOrEmpty(stafId))
                TempHql.AppendFormat(" and u.Id in (select ProfutId from PP_ProfuttoStaffInfo where type ='{0}' and StaffId='{1}')",type,stafId);
            TempHql.AppendFormat(" and state='1'");
            TempHql.AppendFormat("order by Sort desc");
            PagerInfo<PP_ProfitpointinfoView> list = Search();
            return list;
        }
        #endregion

        #region //根据团队Id查找属于该团队的（个人/团队）支出项数据
        /// <summary>
        /// 根据团队Id查找属于该团队的（个人/团队）支出项数据
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="protype">支出项的类别</protype>
        /// <returns></returns>
        public IList<PP_ProfitpointinfoView> GetProfitzhichuinfobyteamId(string teamId,string protype)
        {
            string Hqlstr = string.Format("from PP_Profitpointinfo where Rw_teamId='{0}' and type='1' and ProType='{1}'", teamId,protype);
            List<PP_Profitpointinfo> list = HibernateTemplate.Find<PP_Profitpointinfo>(Hqlstr) as List<PP_Profitpointinfo>;
            IList<PP_ProfitpointinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //团队团体收入支出项目分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rwname">项目名称</param>
        /// <param name="type">收入项 0/支出项 1</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<PP_ProfitpointinfoView> TTProfitshouruzhichupage(string rwname, string type, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(rwname))
                TempHql.AppendFormat(" and u.Rwname like '%{0}%'", rwname);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and u.type='{0}'", type);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat(" and u.Rw_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')", user.Id);
            }
            TempHql.AppendFormat(" and state='1'");
            TempHql.AppendFormat(" and ProType='1'");//团体项目(固定收入)
            TempHql.AppendFormat("order by Sort desc,C_time");
            PagerInfo<PP_ProfitpointinfoView> list = Search();
            return list;
        }
        #endregion

        #region //团队团体收入支出项目（不固定分配）分页数据
        /// <summary>
        /// 团队团体收入支出项目（不固定分配）分页数据
        /// </summary>
        /// <param name="rwname">项目名称</param>
        /// <param name="type">收入项 0/支出项 1</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<PP_ProfitpointinfoView> TTProfitsrzctwepagelist(string rwname, string type, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(rwname))
                TempHql.AppendFormat(" and u.Rwname like '%{0}%'", rwname);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and u.type='{0}'",type);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat(" and u.Rw_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')", user.Id);
            }
            TempHql.AppendFormat(" and state='1'");
            TempHql.AppendFormat(" and ProType='2'");//团体项目(固定收入)
            TempHql.AppendFormat("order by Sort desc,C_time");
            PagerInfo<PP_ProfitpointinfoView> list = Search();
            return list;
        }

        #endregion

    }
}
