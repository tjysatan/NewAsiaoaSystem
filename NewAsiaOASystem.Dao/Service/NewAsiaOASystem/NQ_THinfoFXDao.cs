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
    public class NQ_THinfoFXDao:ServiceConversion<NQ_THinfoFXView,NQ_THinfoFX>,INQ_THinfoFXDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_THinfoFX).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_THinfoFXView GetView(NQ_THinfoFX data)
        {
            NQ_THinfoFXView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_THinfoFXView model)
        {
            NQ_THinfoFX listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_THinfoFXView model)
        {
            NQ_THinfoFX listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_THinfoFXView model)
        {
            NQ_THinfoFX listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_THinfoFXView> model)
        {
            IList<NQ_THinfoFX> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> NGetList()
        {
            List<NQ_THinfoFX> listdata = base.GetList() as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> NGetList_id(string id)
        {
            List<NQ_THinfoFX> list = base.GetList_id(id) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_THinfoFX> NGetListID(string id)
        {
            IList<NQ_THinfoFX> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_THinfoFXView NGetModelById(string id)
        {
            NQ_THinfoFXView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //public PagerInfo<NQ_THinfoFXView> GetCinfoList(string Name, SessionUser user)
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(Name))
        //        TempHql.AppendFormat("and Pname like '%{0}%' ", Name);

        //    TempHql.AppendFormat("order by CreateTime desc");
        //    PagerInfo<NQ_productinfoView> list = Search();
        //    return list;

        //}

        #region //根据 流程ID 查找可退产品分析 信息
        public IList<NQ_THinfoFXView> GetTHFCinfobyR_Id(string R_Id)
        {
            string hqlstr = string.Format("from NQ_THinfoFX where R_Id='{0}' order by P_Id ", R_Id);
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(hqlstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据产品的ID 和返退货单子Id查询出
        /// <summary>
        /// 根据产品Id和反退货单子Id
        /// </summary>
        /// <param name="r_Id">返退货单Id</param>
        /// <param name="c_Id">产品Id</param>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> GetTHFXinfobyR_IdandCpId(string r_Id, string c_Id)
        {
            string Hqlstr = string.Format("from NQ_THinfoFX where R_Id='{0}' and P_Id='{1}'", r_Id,c_Id);
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(Hqlstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;
        }

        #endregion

        #region //根据返退单Id 检测存在没有定责的产品的记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r_Id">返退单Id</param>
        /// <returns></returns>
        public bool JcTHFXweidingzebyr_Id(string r_Id)
        {
            string Hqlstr = string.Format(" from NQ_THinfoFX where R_Id='{0}' and Isdz='0'",r_Id);
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(Hqlstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return true;
            else
                return listmodel.Count > 0 ? false : true;
        }
        #endregion

        #region //根据返退Id 检测未处理的数据
        public bool JcTHFXweichulibyr_Id(string r_id)
        {
            string Hqlstr = string.Format(" from NQ_THinfoFX where R_Id='{0}' and Iscl='0'", r_id);
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(Hqlstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return true;
            else
                return listmodel.Count > 0 ? false : true;
        }
        #endregion


        #region //退货明细分页列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="khname">客户名称</param>
        /// <param name="CPName">产品名称</param>
        /// <param name="SC_PH">生产批号</param>
        /// <param name="BL_XX">不良现象</param>
        /// <param name="BL_Yy">不良原因</param>
        /// <param name="yqj_Id">元器件名称</param>
        /// <param name="xz">性质（18月内、3年内、3年外）</param>
        /// <param name="starttime">维修时间开始</param>
        /// <param name="enedtime">维修时间结束</param>
        /// <param name="user"></param>
        /// <param name="r_pId">产品类  0 电器原料 1 电子原料 2 辅料 3温控器 4 电控箱 5温控器（半成品） 6软件 7其他项目</param>
        /// <returns></returns>
        public PagerInfo<NQ_THinfoFXView> GetCinfoList(string khname, string CPName, string SC_PH, string BL_XX, string BL_Yy, string yqj_name,string yqj_xh, string xzstart, string xzend,
          string starttime,string enedtime,string wxstarttime,string wxendtime, string r_pId ,SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(khname))
                // TempHql.AppendFormat(" and u.R_Id in (select Id from NAReturnList where C_Id  in (select Id from NACustomerinfo where Name like '%{0}%'))", khname);
                TempHql.AppendFormat(" and u.R_Id in (select Id from NAReturnList where C_Id='{0}')",khname);
            if (!string.IsNullOrEmpty(CPName))
                TempHql.AppendFormat(" and u.P_Id in (select Id from NQ_productinfo where Pname like '%{0}%')", CPName);
            if (!string.IsNullOrEmpty(SC_PH))
                TempHql.AppendFormat(" and u.TH_Ph like '%{0}%'", SC_PH);
            if (!string.IsNullOrEmpty(BL_XX))
                TempHql.AppendFormat(" and u.TH_BLXXX in ({0}) ", BL_XX);
            if (!string.IsNullOrEmpty(BL_Yy))
                TempHql.AppendFormat(" and u.TH_BLXX='{0}'", BL_Yy);
            if (!string.IsNullOrEmpty(yqj_name))
                TempHql.AppendFormat(" and TH_YQJname in (select Id from NQ_YJinfo where Y_Name like '%{0}%')", yqj_name);
            if (!string.IsNullOrEmpty(yqj_xh))
                TempHql.AppendFormat(" and TH_YQJname in (select Id from NQ_YJinfo where Y_Ggxh like '%{0}%')", yqj_xh);
            if (!string.IsNullOrEmpty(xzstart))
                TempHql.AppendFormat("and u.TH_zbshj>'{0}' and u.TH_zbshj<='{1}' ", xzstart, xzend);
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", enedtime + " 23:59:59");
            if (!string.IsNullOrEmpty(wxstarttime))
                TempHql.AppendFormat(" and u.wx_time>='{0}' and wx_time<='{1}'", wxstarttime + " 00:00:00", wxendtime + " 23:59:59");
            if (!string.IsNullOrEmpty(r_pId))
                TempHql.AppendFormat("and u.P_Id in (select Id from NQ_productinfo where Nptype in ({0}))",r_pId);
                //TempHql.AppendFormat(" and u.R_Id in (select Id from NAReturnList  where R_pId='{0}')",r_pId);
            TempHql.AppendFormat("order by u.C_time desc");
            PagerInfo<NQ_THinfoFXView> list = Search();
            return list;
        }
        #endregion

        #region //根据反退货流程单Id 查询出返退明细的分页数据 （维修的）
        /// <summary>
        /// 根据反退货流程单Id 查询出返退明细的分页数据
        /// </summary>
        /// <param name="Id">返退货Id</param>
        /// <returns></returns>
        public PagerInfo<NQ_THinfoFXView> Getftmxinfopage(string Id,string CPName,string Iscl)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Id))
                TempHql.AppendFormat(" and u.R_Id='{0}'",Id);
            if (!string.IsNullOrEmpty(CPName))
                TempHql.AppendFormat(" and u.P_Id in (select Id from NQ_productinfo where Pname like '%{0}%')", CPName);
            if (!string.IsNullOrEmpty(Iscl))
                TempHql.AppendFormat(" and u.Iscl='{0}'",Iscl);
            TempHql.AppendFormat("order by u.C_time desc");
            PagerInfo<NQ_THinfoFXView> list = Search();
            return list;
        }
        #endregion

        #region //根据反退货流程单Id 查询出返退明细的分页数据 （定责的）

        /// <summary>
        /// 根据反退货流程单Id 查询出返退明细的分页数据 （定责的）
        /// </summary>
        /// <param name="Id">返退货单Id</param>
        /// <param name="CPname">产品名称</param>
        /// <param name="Isdz">是否定责</param>
        /// <returns></returns>
        public PagerInfo<NQ_THinfoFXView> Getftdzmxinfopage(string Id, string CPname, string Isdz)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Id))
                TempHql.AppendFormat(" and u.R_Id='{0}'", Id);
            if (!string.IsNullOrEmpty(CPname))
                TempHql.AppendFormat(" and u.P_Id in (select Id from NQ_productinfo where Pname like '%{0}%')", CPname);
            if (!string.IsNullOrEmpty(Isdz))
                TempHql.AppendFormat(" and u.Isdz='{0}'", Isdz);
            TempHql.AppendFormat("order by u.C_time desc");
            PagerInfo<NQ_THinfoFXView> list = Search();
            return list;
        }
        #endregion

        #region //退货明细导出数据
        /// <summary>
        /// 退货明细导出数据
        /// </summary>
        /// <param name="khname">客户名称</param>
        /// <param name="CPname">产品名称</param>
        /// <param name="sc_ph">生产批号</param>
        /// <param name="bl_xx">不良现象</param>
        /// <param name="bl_yy">不良原因</param>
        /// <param name="yqj_name">元器件名称</param>
        /// <param name="yqj_xh">元器件型号</param>
        /// <param name="xzstart">性质开始时间</param>
        /// <param name="xzend">性质结束</param>
        /// <param name="starttime">维修时间</param>
        /// <param name="enedtime">维修时间</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> DCWXFXDATA(string khname, string CPname, string sc_ph, string bl_xx, string bl_yy, string yqj_name, string yqj_xh, string xzstart,
            string xzend, string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(khname))
                TempHql.AppendFormat(" and u.R_Id in (select Id from NAReturnList where C_Id='{0}')", khname);
            if (!string.IsNullOrEmpty(CPname))
                TempHql.AppendFormat(" and u.P_Id in (select Id from NQ_productinfo where Pname like '%{0}%')", CPname);
            if (!string.IsNullOrEmpty(sc_ph))
                TempHql.AppendFormat(" and u.TH_Ph like '%{0}%'", sc_ph);
            if (!string.IsNullOrEmpty(bl_xx))
                TempHql.AppendFormat(" and u.TH_BLXXX in ({0}) ", bl_xx);
            if (!string.IsNullOrEmpty(bl_yy))
                TempHql.AppendFormat(" and u.TH_BLXX='{0}'", bl_yy);
            if (!string.IsNullOrEmpty(yqj_name))
                TempHql.AppendFormat(" and TH_YQJname in (select Id from NQ_YJinfo where Y_Name like '%{0}%')", yqj_name);
            if (!string.IsNullOrEmpty(yqj_xh))
                TempHql.AppendFormat(" and TH_YQJname in (select Id from NQ_YJinfo where Y_Ggxh like '%{0}%')", yqj_xh);
            if (!string.IsNullOrEmpty(xzstart))
                TempHql.AppendFormat("and u.TH_zbshj>'{0}' and u.TH_zbshj<='{1}' ", xzstart, xzend);
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and u.C_time>='{0}' and  C_time<='{1}'", starttime + " 00:00:00", enedtime + " 23:59:59");
            if (!string.IsNullOrEmpty(wxstarttime))
                TempHql.AppendFormat(" and u.wx_time>='{0}' and wx_time<='{1}'", wxstarttime + " 00:00:00", wxendtime + " 23:59:59");
            if (!string.IsNullOrEmpty(r_pId))
                TempHql.AppendFormat("and u.P_Id in (select Id from NQ_productinfo where Nptype in ({0}))", r_pId);
                //TempHql.AppendFormat(" and u.R_Id in (select Id from NAReturnList  where R_pId='{0}')", r_pId);
            TempHql.AppendFormat("order by u.C_time desc");
            string HQLstr = string.Format("from NQ_THinfoFX u where 1=1 {0}", TempHql.ToString());
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(HQLstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //退货维修分析
        //按照产品分组查询
        public List<Object> GetTHwxfxgroupcpId(string starttime, string enedtime)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", enedtime + " 23:59:59");
            TempHql.AppendFormat(" group by P_Id");
            string HQLstr = string.Format("select Id, Pname,P_bianhao from NQ_productinfo where Id in (select P_Id from NQ_THinfoFX where 1=1 {0})", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return  obj;
        }

        //按照坏掉的元器件分组查询
        /// <summary>
        /// 按照坏掉的元器件分组查询
        /// </summary>
        /// <param name="starttime">维修时间</param>
        /// <param name="endtime">维修时间</param>
        /// <returns></returns>
        public List<Object> GetTHwxfxgroupTH_YQJname(string starttime, string endtime)
        {
            TempHql = new StringBuilder();
            if(!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat(" group by TH_YQJname");
            string HQLstr = string.Format("select Id,Y_Name,Y_Ggxh,Y_Dm from NQ_YJinfo where Id in (select TH_YQJname from NQ_THinfoFX where 1=1 {0})", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }

        //按照返退品不良原因分组
        /// <summary>
        /// 按照返退品不良原因分组
        /// </summary>
        /// <param name="starttime">维修时间</param>
        /// <param name="endtime">维修时间</param>
        /// <returns></returns>
        public List<Object> GetTHwxfxgroupTH_BLXX(string starttime, string endtime)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat(" group by TH_BLXX");
            string HQLstr = string.Format("select Id,Name from NQ_Blinfo where Id in (select TH_BLXX from NQ_THinfoFX where 1=1 {0})", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }

        //按照维修的时候出现的不良原因分组查询
        //public List<Object> GetTHwxfxgroupTH_BLYY(string starttime, string endtime)
        //{
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(starttime))
        //        TempHql.AppendFormat(" and wx_time>='{0}' and wx_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
        //    TempHql.AppendFormat(" group by TH_BLYY");
        //    string HQLstr = string.Format("");
        //}

        //根据产品Id查找返退的数量
        public int GetCPsumbycpId(string startime, string endtime,string cp_Id)
        {

            TempHql = new StringBuilder();
            if(!string.IsNullOrEmpty(startime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' and  ", startime + " 00:00:00", endtime + " 23:59:59");
          
            TempHql.AppendFormat("and P_Id='{0}'",cp_Id);
            string HQLstr = string.Format("select count(*)  from NQ_THinfoFX where 1=1 {0}", TempHql);
            IQuery queryCount = Session.CreateQuery(HQLstr);
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }

        //根据元器件Id查找该元器件维修的数量
        /// <summary>
        /// 根据元器件Id查找该元器件维修的数量
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="YqjId"></param>
        /// <returns></returns>
        public int GetYQJsumbyYQJId(string starttime, string endtime, string YqjId)
        {
            TempHql = new StringBuilder();
            if(!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat("and TH_YQJname='{0}'", YqjId);
            string HQLstr = string.Format("select count(*) from NQ_THinfoFX where 1=1 {0}",TempHql);
            IQuery queryCount = Session.CreateQuery(HQLstr);
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }


        //根据不良原因查找维修数量
        /// <summary>
        /// 根据不良原因查找维修数量
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="blyyId"></param>
        /// <returns></returns>
        public int GetBLyysumbyblyyId(string starttime, string endtime, string blyyId)
        {
            TempHql = new StringBuilder();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat("and TH_BLXX='{0}'",blyyId);
            string HQLstr = string.Format("select count(*) from NQ_THinfoFX where 1=1 {0}",TempHql);
            IQuery queryCount = Session.CreateQuery(HQLstr);
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据处理方式查找维修分析记录
        /// <summary>
        /// 根据处理方式查找维修分析记录
        /// </summary>
        /// <param name="clfs">处理状态</param>
        /// <param name="r_Id">返退货单Id</param>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> GetNq_thinfofxbythbz(string clfs,string r_Id)
        {
            string Hqlstr = string.Format("from NQ_THinfoFX where R_Id='{0}' and TH_bz='{1}'",r_Id,clfs);
            List<NQ_THinfoFX> list = HibernateTemplate.Find<NQ_THinfoFX>(Hqlstr) as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据订单Id和需要合计的字段类型合计金额
        /// <summary>
        /// 根据订单Id和需要合计的字段类型合计金额
        /// </summary>
        /// <param name="RId">返退货订单Id</param>
        /// <param name="type"> TH_yqjjg 元器件金额 TH_RGF 人工费 TH_BCF 包材费</param>
        /// <returns></returns>
        public decimal GetTotalcostbyRIdcosttype(string RId,string type)
        {
            string Hqlstr = string.Format(" from NQ_THinfoFX where R_Id='{0}'", RId);
            IQuery queryCount = Session.CreateQuery(string.Format("select sum({0})  {1} ", type, Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据产品Id分组to
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strattime"></param>
        /// <param name="enedtime"></param>
        /// <returns></returns>
        public List<Object> GetStatisticsgroupcpid(string strattime, string enedtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(strattime))
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' and Isdz='1' ", strattime + " 00:00:00", enedtime + " 23:59:59");
            else
                TempHql.AppendFormat("and wx_time>='{0}' and wx_time<='{1}' and Isdz='1' ", ksdate + " 00:00:00", jsdate + " 23:59:59");
            TempHql.AppendFormat(" group by P_Id,Cpname,Cpwlno  order by count(P_Id) desc");
            string HQLstr = string.Format("select P_Id ,Cpname ,Cpwlno,count(P_Id),SUM(TH_XJ) from NQ_THinfoFX where 1=1 {0}", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

        #region //查出产品名称为空的退货明细
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<NQ_THinfoFXView> TemporaryGetcpnameisnulldata()
        {
            string temHql = string.Format(" from NQ_THinfoFX where Cpname is null");
            IQuery query = Session.CreateQuery(temHql);
            query.SetFirstResult(0);
            query.SetMaxResults(10000);
            List<NQ_THinfoFX> list = query.List<NQ_THinfoFX>() as List<NQ_THinfoFX>;
            IList<NQ_THinfoFXView> listmodel = GetViewlist(list);
            return listmodel;

        }

        #endregion
    }
}
