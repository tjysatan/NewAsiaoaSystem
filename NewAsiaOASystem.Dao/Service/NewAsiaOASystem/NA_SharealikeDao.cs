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
    public class NA_SharealikeDao:ServiceConversion<NA_SharealikeView, NA_Sharealike>,INA_SharealikeDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_Sharealike).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_SharealikeView GetView(NA_Sharealike data)
        {
            NA_SharealikeView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_SharealikeView model)
        {
            NA_Sharealike listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_SharealikeView model)
        {
            NA_Sharealike listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_SharealikeView model)
        {
            NA_Sharealike listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_SharealikeView> model)
        {
            IList<NA_Sharealike> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_SharealikeView> NGetList()
        {
            List<NA_Sharealike> listdata = base.GetList() as List<NA_Sharealike>;
            IList<NA_SharealikeView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_SharealikeView> NGetList_id(string id)
        {
            List<NA_Sharealike> list = base.GetList_id(id) as List<NA_Sharealike>;
            IList<NA_SharealikeView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_Sharealike> NGetListID(string id)
        {
            IList<NA_Sharealike> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_SharealikeView NGetModelById(string id)
        {
            if (GetModelById(id) != null)
            {
                NA_SharealikeView listmodel = GetView(base.GetModelById(id));
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        #region //根据明细Id查找并删除
        /// <summary>
        /// 根据明细Id查找并删除
        /// </summary>
        /// <param name="mxId">明细Id</param>
        /// <returns></returns>
        public bool Getlistbymxiddellist(string mxId)
        {
            try
            {
                List<NA_Sharealike> list = HibernateTemplate.Find<NA_Sharealike>("from NA_Sharealike where Mx_id='" + mxId + "'") as List<NA_Sharealike>;
                if (list == null)
                    return true;
                else
                {
                  bool res= NDelete(list);
                   return res;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region //查询售后费用分页数据

        #endregion

        #region //根据责任部门分组查询
        public List<Object> GetStatisticsgroupResdepartment(string strattime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(strattime))
                TempHql.AppendFormat("and Wxtime>='{0}' and Wxtime<='{1}'  ", strattime + " 00:00:00", endtime + " 23:59:59");
            else
                TempHql.AppendFormat("and Wxtime>='{0}' and Wxtime<='{1}' ", ksdate + " 00:00:00", jsdate + " 23:59:59");
            TempHql.AppendFormat(" group by Resdepartment  order by SUM(cost) desc");
            string HQLstr = string.Format("select Resdepartment,SUM(cost) from NA_Sharealike where 1=1 {0}", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

        #region //根据退货客户分组查询
        public List<Object> GetStatisticsgroupThcusId(string strattime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(strattime))
                TempHql.AppendFormat("and Wxtime>='{0}' and Wxtime<='{1}'  ", strattime + " 00:00:00", endtime + " 23:59:59");
            else
                TempHql.AppendFormat("and Wxtime>='{0}' and Wxtime<='{1}' ", ksdate + " 00:00:00", jsdate + " 23:59:59");
            TempHql.AppendFormat(" group by ThcusId,Thcusname  order by SUM(cost) desc");
            string HQLstr = string.Format("select ThcusId,Thcusname,SUM(cost) from NA_Sharealike where 1=1 {0}", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

    }
}
