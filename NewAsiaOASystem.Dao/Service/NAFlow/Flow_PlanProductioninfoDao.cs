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
    public class Flow_PlanProductioninfoDao:ServiceConversion<Flow_PlanProductioninfoView,Flow_PlanProductioninfo>,IFlow_PlanProductioninfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_PlanProductioninfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_PlanProductioninfoView GetView(Flow_PlanProductioninfo data)
        {
            Flow_PlanProductioninfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_PlanProductioninfoView model)
        {
            Flow_PlanProductioninfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_PlanProductioninfoView model)
        {
            Flow_PlanProductioninfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_PlanProductioninfoView model)
        {
            Flow_PlanProductioninfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_PlanProductioninfoView> model)
        {
            IList<Flow_PlanProductioninfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_PlanProductioninfoView> NGetList()
        {
            List<Flow_PlanProductioninfo> listdata = base.GetList() as List<Flow_PlanProductioninfo>;
            IList<Flow_PlanProductioninfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_PlanProductioninfoView> NGetList_id(string id)
        {
            List<Flow_PlanProductioninfo> list = base.GetList_id(id) as List<Flow_PlanProductioninfo>;
            IList<Flow_PlanProductioninfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_PlanProductioninfo> NGetListID(string id)
        {
            IList<Flow_PlanProductioninfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_PlanProductioninfoView NGetModelById(string id)
        {
            Flow_PlanProductioninfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        /// <summary>
        /// 生产计划单管理
        /// </summary>
        /// <param name="CPName">产品名称</param>
        /// <param name="P_Pianhao">产品编号</param>
        /// <param name="P_Issc">状态 0 生产中 1 缺料 2待生产 3完成</param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public PagerInfo<Flow_PlanProductioninfoView> GetCinfoList(string CPName,string P_Pianhao, string P_Issc,string starttime,string endtime,string p_type)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(CPName))
                TempHql.AppendFormat(" and u.P_CPname like '%{0}%' ", CPName);
            if (!string.IsNullOrEmpty(P_Pianhao))
                TempHql.AppendFormat(" and u.P_Pianhao='{0}' ", P_Pianhao);
            if (!string.IsNullOrEmpty(P_Issc))
                TempHql.AppendFormat(" and u.P_Issc='{0}'",P_Issc);
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            if (!string.IsNullOrEmpty(p_type))
            {
                TempHql.AppendFormat(" and P_type in({0})", p_type);
            }
            else
            {
                TempHql.AppendFormat(" and P_type in('0','1','2','3')");
            }
            TempHql.AppendFormat(" and Status='0'");
            TempHql.AppendFormat(" order by u.C_time desc");
            PagerInfo<Flow_PlanProductioninfoView> list = Search();
            return list;
        }

        /// <summary>
        /// 查询当天生产通知单的条数
        /// </summary>
        /// <returns></returns>
        public int GetPPcount()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd");
                string temHql = string.Format("from Flow_PlanPPrintinfo where Kfdatetime>='{0}' and Kfdatetime<='{1}'", datetime + " 00:00:00", datetime + " 23:59:59");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
 

        #region //保存后返回ID
        public string InsertID(Flow_PlanProductioninfoView modelView)
        {
            Flow_PlanProductioninfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
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

        #region //电控箱的生产计划管理

        #region //电控箱分页数据
        /// <summary>
        /// 电控箱分页数据
        /// </summary>
        /// <param name="CPName">产品名称</param>
        /// <param name="P_Pianhao">产品物料代理</param>
        /// <param name="P_Issc">状态 0 生产中 1 缺料 2 待生产 3 完成</param>
        /// <param name="starttime">下单时间</param>
        /// <param name="endtime"></param>
        /// <param name="p_type">订单类型 4 常规电控箱</param>
        /// <returns></returns>
        public PagerInfo<Flow_PlanProductioninfoView> DKXGetPDATAPagerlist(string CPName, string P_Pianhao, string P_Issc, string starttime, string endtime, string p_type,string scbianhao)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(CPName))
                TempHql.AppendFormat(" and u.P_CPname like '%{0}%' ", CPName);
            if (!string.IsNullOrEmpty(P_Pianhao))
                TempHql.AppendFormat(" and u.P_Pianhao='{0}' ", P_Pianhao);
            if (!string.IsNullOrEmpty(P_Issc))
                TempHql.AppendFormat(" and u.P_Issc='{0}'", P_Issc);
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            if (!string.IsNullOrEmpty(p_type))
            {
                TempHql.AppendFormat(" and P_type in({0})", p_type);
            }
            else
            {
                TempHql.AppendFormat(" and P_type in('4')");
            }
            if (!string.IsNullOrEmpty(scbianhao))
                TempHql.AppendFormat("and scbianhao like '%{0}%'",scbianhao);
            TempHql.AppendFormat(" and Status='0'");
            TempHql.AppendFormat(" order by u.C_time desc");
            PagerInfo<Flow_PlanProductioninfoView> list = Search();
            return list;
        }
        #endregion
        #endregion


        #region //返回当天的常规电控箱的下单数量
        /// <summary>
        /// 返回当天的电控箱的下单数量
        /// </summary>
        /// <returns></returns>
        public int GetDKXcount()
        {
            try
            {
                string temHql = string.Format(" from Flow_PlanProductioninfo where DateDiff(dd,C_time,getdate())=0 and P_type='4'");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
