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
    public class NA_MRPmainDao:ServiceConversion<NA_MRPmainView,NA_MRPmain>,INA_MRPmainDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_MRPmain).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_MRPmainView GetView(NA_MRPmain data)
        {
            NA_MRPmainView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_MRPmainView model)
        {
            NA_MRPmain listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_MRPmainView model)
        {
            NA_MRPmain listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_MRPmainView model)
        {
            NA_MRPmain listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_MRPmainView> model)
        {
            IList<NA_MRPmain> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_MRPmainView> NGetList()
        {
            List<NA_MRPmain> listdata = base.GetList() as List<NA_MRPmain>;
            IList<NA_MRPmainView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_MRPmainView> NGetList_id(string id)
        {
            List<NA_MRPmain> list = base.GetList_id(id) as List<NA_MRPmain>;
            IList<NA_MRPmainView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_MRPmain> NGetListID(string id)
        {
            IList<NA_MRPmain> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_MRPmainView NGetModelById(string id)
        {
            NA_MRPmainView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //保存后返回ID
        public string InsertID(NA_MRPmainView modelView)
        {
            NA_MRPmain model = GetData(modelView);
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

        #region //MRP计算单据的分页结果
        /// <summary>
        /// 计算的订单时间
        /// </summary>
        /// <param name="orderxdtime"></param>
        /// <returns></returns>
        public PagerInfo<NA_MRPmainView> GetNA_MRPorderlistpage(string orderxdtime)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(orderxdtime))
                TempHql.AppendFormat(" and Ordertime='{0}'", orderxdtime);
            TempHql.AppendFormat(" and state='0'");
            TempHql.AppendFormat("order by  c_time desc");
            PagerInfo<NA_MRPmainView> list = Search();
            return list;
        }
        #endregion


    }
}
