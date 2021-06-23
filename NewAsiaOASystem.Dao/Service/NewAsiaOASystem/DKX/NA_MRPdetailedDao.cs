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
    public class NA_MRPdetailedDao : ServiceConversion<NA_MRPdetailedView, NA_MRPdetailed>, INA_MRPdetailedDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_MRPdetailed).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_MRPdetailedView GetView(NA_MRPdetailed data)
        {
            NA_MRPdetailedView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_MRPdetailedView model)
        {
            NA_MRPdetailed listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_MRPdetailedView model)
        {
            NA_MRPdetailed listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_MRPdetailedView model)
        {
            NA_MRPdetailed listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_MRPdetailedView> model)
        {
            IList<NA_MRPdetailed> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_MRPdetailedView> NGetList()
        {
            List<NA_MRPdetailed> listdata = base.GetList() as List<NA_MRPdetailed>;
            IList<NA_MRPdetailedView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_MRPdetailedView> NGetList_id(string id)
        {
            List<NA_MRPdetailed> list = base.GetList_id(id) as List<NA_MRPdetailed>;
            IList<NA_MRPdetailedView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_MRPdetailed> NGetListID(string id)
        {
            IList<NA_MRPdetailed> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_MRPdetailedView NGetModelById(string id)
        {
            NA_MRPdetailedView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询MRP计算的结果
        public List<Object> GetMRPresult(string Mid)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Mid))
                TempHql.AppendFormat(" and MainId='{0}'",Mid);
            TempHql.AppendFormat(" group by wlno ,wlname,wlmodel,kcsum");
            string HQLstr = string.Format("select wlno,wlname,wlmodel,SUM(qusum) as quzl,kcsum  from NA_MRPdetailed where 1=1 {0}", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

        #region //查询MRP的细表
        /// <summary>
        /// 查询MRP的细表
        /// </summary>
        /// <param name="Mid"></param>
        /// <returns></returns>
        public IList<NA_MRPdetailedView> GetallMRPdetailedByMid(string Mid)
        {
            string Hqlstr = string.Format(" from NA_MRPdetailed  where  MainId='{0}' ", Mid);
            List<NA_MRPdetailed> list = HibernateTemplate.Find<NA_MRPdetailed>(Hqlstr) as List<NA_MRPdetailed>;
            IList<NA_MRPdetailedView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
