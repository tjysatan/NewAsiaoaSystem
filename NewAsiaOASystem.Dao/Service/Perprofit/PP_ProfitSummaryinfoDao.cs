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
    public class PP_ProfitSummaryinfoDao:ServiceConversion<PP_ProfitSummaryinfoView,PP_ProfitSummaryinfo>,IPP_ProfitSummaryinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_ProfitSummaryinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_ProfitSummaryinfoView GetView(PP_ProfitSummaryinfo data)
        {
            PP_ProfitSummaryinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_ProfitSummaryinfoView model)
        {
            PP_ProfitSummaryinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_ProfitSummaryinfoView model)
        {
            PP_ProfitSummaryinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_ProfitSummaryinfoView model)
        {
            PP_ProfitSummaryinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_ProfitSummaryinfoView> model)
        {
            IList<PP_ProfitSummaryinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_ProfitSummaryinfoView> NGetList()
        {
            List<PP_ProfitSummaryinfo> listdata = base.GetList() as List<PP_ProfitSummaryinfo>;
            IList<PP_ProfitSummaryinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_ProfitSummaryinfoView> NGetList_id(string id)
        {
            List<PP_ProfitSummaryinfo> list = base.GetList_id(id) as List<PP_ProfitSummaryinfo>;
            IList<PP_ProfitSummaryinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_ProfitSummaryinfo> NGetListID(string id)
        {
            IList<PP_ProfitSummaryinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_ProfitSummaryinfoView NGetModelById(string id)
        {
            PP_ProfitSummaryinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据员工Id和汇总时间查找按月汇总的数据
        public PP_ProfitSummaryinfoView GetprofitSinfobystafidanhztime(string stafid, string hztime)
        {
            string Hqlstr = string.Format("from PP_ProfitSummaryinfo where StafitId='{0}' and Type='0' and MorY='{1}'", stafid, hztime);
            List<PP_ProfitSummaryinfo> list = HibernateTemplate.Find<PP_ProfitSummaryinfo>(Hqlstr) as List<PP_ProfitSummaryinfo>;
            IList<PP_ProfitSummaryinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据员工Id和汇总时间查找安年汇总的数据
        /// <summary>
        /// 和汇总时间查找安年汇总的数据
        /// </summary>
        /// <param name="stafid">员工Id</param>
        /// <param name="hztime">汇总时间</param>
        /// <returns></returns>
        public PP_ProfitSummaryinfoView GetprofitSinfobystafidandhztimeY(string stafid, string hztime)
        {
            string Hqlstr = string.Format("from PP_ProfitSummaryinfo where StafitId='{0}' and Type='1' and MorY='{1}'", stafid, hztime);
            List<PP_ProfitSummaryinfo> list = HibernateTemplate.Find<PP_ProfitSummaryinfo>(Hqlstr) as List<PP_ProfitSummaryinfo>;
            IList<PP_ProfitSummaryinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //按月查询汇总盈利数据
        /// <summary>
        /// 按月查询汇总盈利数据
        /// </summary>
        /// <param name="stafid"></param>
        /// <param name="hztime"></param>
        /// <returns></returns>
        public IList<PP_ProfitSummaryinfoView> GetprofitSuminfobyhztimeM( string hztime)
        {
            string Hqlstr = string.Format("from PP_ProfitSummaryinfo where  Type='0' and MorY='{0}' order by yingli desc, Sat_teamId",hztime);
            List<PP_ProfitSummaryinfo> list = HibernateTemplate.Find<PP_ProfitSummaryinfo>(Hqlstr) as List<PP_ProfitSummaryinfo>;
            IList<PP_ProfitSummaryinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
