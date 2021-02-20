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
    public class PP_ShouruandzhichuDetailsinfoDao:ServiceConversion<PP_ShouruandzhichuDetailsinfoView,PP_ShouruandzhichuDetailsinfo>,IPP_ShouruandzhichuDetailsinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_ShouruandzhichuDetailsinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_ShouruandzhichuDetailsinfoView GetView(PP_ShouruandzhichuDetailsinfo data)
        {
            PP_ShouruandzhichuDetailsinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_ShouruandzhichuDetailsinfoView model)
        {
            PP_ShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_ShouruandzhichuDetailsinfoView model)
        {
            PP_ShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_ShouruandzhichuDetailsinfoView model)
        {
            PP_ShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_ShouruandzhichuDetailsinfoView> model)
        {
            IList<PP_ShouruandzhichuDetailsinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> NGetList()
        {
            List<PP_ShouruandzhichuDetailsinfo> listdata = base.GetList() as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> NGetList_id(string id)
        {
            List<PP_ShouruandzhichuDetailsinfo> list = base.GetList_id(id) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_ShouruandzhichuDetailsinfo> NGetListID(string id)
        {
            IList<PP_ShouruandzhichuDetailsinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_ShouruandzhichuDetailsinfoView NGetModelById(string id)
        {
            PP_ShouruandzhichuDetailsinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据个人（员工）Id查找收入和支持明细

        /// <summary>
        /// 根据个人（员工）Id查找收入和支持明细
        /// </summary>
        /// <param name="staffId">个人（员工）Id</param>
        /// <param name="type">收入和支出类别</param>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> GetshouruhichuMxbystaffId(string staffId, string type, string jltime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string starttime = d1.ToString("yyyyMMdd");
            string endtime = d2.ToString("yyyyMMdd");
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where StafitId='{0}' and type='{1}' and jl_time>='{2}' and jl_time<='{3}' order by jl_time", staffId, type, starttime, endtime);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据个人Id和项目Id已经完成时间查找是否存在相同记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="ProfitId"></param>
        /// <param name="wctime"></param>
        /// <returns>存在返回 true,不存在返回 false</returns>
        public bool JcshouruzhichuMxbystafidandproIdandwctime(string staffId, string ProfitId, string wctime)
        {
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where StafitId='{0}' and ProfutId='{1}' and jl_time='{2}'",staffId,ProfitId,wctime);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            return list.Count > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="ProfitId"></param>
        /// <param name="wctime"></param>
        /// <returns>返回 明细实体</returns>
        public PP_ShouruandzhichuDetailsinfoView Getshouruzhichuinfobywctime(string staffId, string ProfitId, string wctime)
        {
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where StafitId='{0}' and ProfutId='{1}' and jl_time='{2}'", staffId, ProfitId, wctime);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //个人收入支出明细分页数据
        /// <summary>
        /// 个人收入支出明细分页数据
        /// </summary>
        /// <param name="Name">个人员工名单</param>
        /// <param name="xmName">项目名单</param>
        /// <param name="type">收入和支出类别</param>
        /// <param name="startc_time">记录时间</param>
        /// <param name="endc_time">记录时间</param>
        /// <param name="startwctime">完成时间</param>
        /// <param name="endwctime">完成时间</param>
        /// <param name="user">当前登录帐号</param>
        /// <returns>分页数据</returns>
        public PagerInfo<PP_ShouruandzhichuDetailsinfoView> Getshouruzhichupage(string Name, string xmName, string type, string startc_time, string endc_time,
            string startwctime,string endwctime, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.StafitId in (select Id from PP_Staffinfo where Sat_Name like '%{0}%')", Name);
            if (!string.IsNullOrEmpty(xmName))
                TempHql.AppendFormat(" and u.ProfutId in (select Id from PP_Profitpointinfo where Rwname like '%{0}%')", xmName);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and u.type='{0}'",type);
            if (!string.IsNullOrEmpty(startc_time) && !string.IsNullOrEmpty(endc_time))
                TempHql.AppendFormat(" and u.C_time>='{0}' and u.C_time<='{1}'", startc_time + " 00:00:00", endc_time + " 23:59:59");
            if (!string.IsNullOrEmpty(startwctime) && !string.IsNullOrEmpty(endwctime))
            {
                startwctime = Convert.ToDateTime(startwctime).ToString("yyyyMMdd");
                endwctime = Convert.ToDateTime(endwctime).ToString("yyyyMMdd");
                TempHql.AppendFormat(" and u.jl_time>='{0}' and u.jl_time<='{1}'", startwctime, endwctime);
            }
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat(" and u.C_Name='{0}'", user.Id);
            }
            TempHql.AppendFormat("order by u.jl_time desc");
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> list = Search();
            return list;
        }
        #endregion

        #region //根据时间和员工Id计算总收入
        /// <summary>
        /// 
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stafId"></param>
        /// <returns></returns>
        public decimal Getsumshourubystafid(string starttime, string endtime, string stafId)
        {
            try
            {
                string temHql = string.Format("from PP_ShouruandzhichuDetailsinfo where jl_time>='{0}' and jl_time<='{1}' and type in('0','2') and StafitId='{2}'", starttime, endtime,stafId);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //根据时间和员工Id计算总支出
        /// <summary>
        /// 根据时间和员工Id计算总支出
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stafId"></param>
        /// <returns></returns>
        public decimal GetsumzhichubystafId(string starttime, string endtime, string stafId)
        {
            try
            {
                string temHql = string.Format("from PP_ShouruandzhichuDetailsinfo where jl_time>='{0}' and jl_time<='{1}' and type='1' and StafitId='{2}'", starttime, endtime, stafId);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //根据个人（员工）Id计算累计收入
        /// <summary>
        /// 根据个人（员工）Id计算累计收入
        /// </summary>
        /// <param name="stafId"></param>
        /// <returns></returns>
        public decimal GetleijishourubystafId(string stafId)
        {
            try
            {
                string temHql = string.Format("from PP_ShouruandzhichuDetailsinfo where  type in('0','2') and StafitId='{0}'", stafId);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //根据个人（员工）Id计算累计支出
        /// <summary>
        /// 根据个人（员工）Id计算累计支出
        /// </summary>
        /// <param name="stafId"></param>
        /// <returns></returns>
        public decimal GetleijizhichubystafId(string stafId)
        {
            try
            {
                string temHql = string.Format("from PP_ShouruandzhichuDetailsinfo where  type='1' and StafitId='{0}'", stafId);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //生产管理费用
        #region //查询生产团队的当天的的生产收入（管理费用除外）
         /// <summary>
        /// 查询生产团队的当天的的生产收入（管理费用除外）
         /// </summary>
         /// <param name="wctime">完成时间</param>
        /// <param name="userId">当前登录的Id</param>
        /// <returns></returns>
        public decimal Getscteamshourusumbysj(string wctime, string userId)
        {
            try
            {
                string temHql = string.Format(" from PP_ShouruandzhichuDetailsinfo where type='0' and jl_time='{0}' and StafitId in (select Id from PP_Staffinfo where Sat_teamId in (select Id from PP_Teaminfo where Team_glyuser='{1}'))", wctime, userId);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //查找生产部管理人员当天的管理费用
        /// <summary>
        /// 查找生产部管理人员当天的管理费用
        /// </summary>
        /// <param name="wctime"></param>
        /// <param name="stafid"></param>
        /// <returns></returns>
        public PP_ShouruandzhichuDetailsinfoView Getshouruguanlifeiyongbywctimeandstafid(string wctime, string stafid)
        {
            try
            {
                string temHql = string.Format("from PP_ShouruandzhichuDetailsinfo where type='2'and jl_time='{0}' and StafitId='{1}' ", wctime, stafid);
                List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(temHql) as List<PP_ShouruandzhichuDetailsinfo>;
                IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
                if (listmodel != null)
                    return listmodel[0];
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion 
        #endregion

        #region //根据团体项目明细Id查找个人分摊的收支明细
        /// <summary>
        /// 团体项目收支明细Id
        /// </summary>
        /// <param name="TTszmxId"></param>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTszmxdatabyttmxId(string TTszmxId)
        {
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where TTmxId='{0}'",TTszmxId);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据完成时间和员工Id查询收支数据
        /// <summary>
        /// 根据完成时间和员工Id查询收支数据
        /// </summary>
        /// <param name="startwctime">开始时间</param>
        /// <param name="endwctime">结束时间</param>
        /// <param name="stafId">员工Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public decimal GethtgrszbystafId(string startwctime, string endwctime, string stafId,string type)
        {
            //
            string Hqlstr = "";
            if (!string.IsNullOrEmpty(startwctime))
            {
                startwctime = Convert.ToDateTime(startwctime).ToString("yyyyMMdd");
            }
            else
            {
                startwctime = DateTime.Now.ToString("yyyyMMdd");
            }
            if (!string.IsNullOrEmpty(endwctime))
            {
                endwctime = Convert.ToDateTime(endwctime).ToString("yyyyMMdd");
            }
            else
            {
                endwctime = DateTime.Now.ToString("yyyyMMdd");
            }
            if (type == "0")
            {
                Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where type in('0','2') and StafitId='{0}' and jl_time>='{1}' and jl_time<='{2}'", stafId, startwctime, endwctime);
            }
            else
            {
                Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where type='1' and StafitId='{0}' and jl_time>='{1}' and jl_time<='{2}'", stafId, startwctime, endwctime);
            }
            IQuery queryCount = Session.CreateQuery(string.Format("select sum(Total)  {0} ", Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;

        }
        #endregion

        #region //获取团体项目收入明细Id（不固定分配）查询分配个人的收入的明细
        /// <summary>
        /// 获取团体项目收入明细Id（不固定分配）查询分配个人的收入的明细
        /// </summary>
        /// <param name="TTsrmxId">团队收入明细Id</param>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTtwesrmxbyttmxId(string TTsrmxId)
        {
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where TTmxId='{0}'",TTsrmxId);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据项目收入明细Id、完成时间、员工Id 查找是否存在该收入明细
        /// <summary>
        /// 根据项目收入明细Id、完成时间、员工Id 查找是否存在该收入明细
        /// </summary>
        /// <param name="TTsrmxId"></param>
        /// <param name="wctime"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public PP_ShouruandzhichuDetailsinfoView GetgerenttsrtwebyttsrmxIdandwctimeandstaffId(string TTsrmxId, string wctime, string staffId)
        {
            string Hqlstr = string.Format(" from PP_ShouruandzhichuDetailsinfo where TTmxId='{0}' and jl_time='{1}' and StafitId='{2}'",TTsrmxId,wctime,staffId);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //更加团体收入项明细Id 查找分配明细
        /// <summary>
        /// 更加团体收入项明细Id 查找分配明细
        /// </summary>
        /// <param name="TTsrmxId"></param>
        /// <returns></returns>
        public IList<PP_ShouruandzhichuDetailsinfoView> GetgerenTTsrtwefpmxbyttmxId(string TTsrmxId)
        {
            string Hqlstr = string.Format("from PP_ShouruandzhichuDetailsinfo where TTmxId='{0}'",TTsrmxId);
            List<PP_ShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_ShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_ShouruandzhichuDetailsinfo>;
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
