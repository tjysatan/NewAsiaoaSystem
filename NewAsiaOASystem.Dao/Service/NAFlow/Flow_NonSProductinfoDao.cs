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
    public class Flow_NonSProductinfoDao:ServiceConversion<Flow_NonSProductinfoView,Flow_NonSProductinfo>,IFlow_NonSProductinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private StringBuilder NTempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_NonSProductinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_NonSProductinfoView GetView(Flow_NonSProductinfo data)
        {
            Flow_NonSProductinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_NonSProductinfoView model)
        {
            Flow_NonSProductinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_NonSProductinfoView model)
        {
            Flow_NonSProductinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_NonSProductinfoView model)
        {
            Flow_NonSProductinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_NonSProductinfoView> model)
        {
            IList<Flow_NonSProductinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_NonSProductinfoView> NGetList()
        {
            List<Flow_NonSProductinfo> listdata = base.GetList() as List<Flow_NonSProductinfo>;
            IList<Flow_NonSProductinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_NonSProductinfoView> NGetList_id(string id)
        {
            List<Flow_NonSProductinfo> list = base.GetList_id(id) as List<Flow_NonSProductinfo>;
            IList<Flow_NonSProductinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_NonSProductinfo> NGetListID(string id)
        {
            IList<Flow_NonSProductinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_NonSProductinfoView NGetModelById(string id)
        {
            Flow_NonSProductinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

 

        /// <summary>
        /// 获取非标产品的数据
        /// </summary>
        /// <param name="Sort">排序方式 0 倒序asc   1正序DESC  </param>
        /// <param name="Category">0 销售产品  1工程产品</param>
        /// <param name="wldm">物料代理</param>
        /// <param name="Pname">产品名称</param>
        /// <returns></returns>
        public IList<Flow_NonSProductinfoView> GetNonsdata(string Sort, string Category, string wldm, string Pname)
        {
            //string HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' order by A_Sum asc ", Category);

             TempHql = new StringBuilder();
             if (!string.IsNullOrEmpty(Category))
                 TempHql.AppendFormat(" and u.Category in ({0})",Category);
             if (!string.IsNullOrEmpty(wldm))
                 TempHql.AppendFormat(" and u.Pbianma='{0}'",wldm);
             if (!string.IsNullOrEmpty(Pname))
                 TempHql.AppendFormat(" and u.Pname like '%{0}%'",Pname);
             if (Sort == "0")
                 TempHql.AppendFormat(" order by Pbianma asc");
            if(Sort=="1")
                TempHql.AppendFormat("order by Pbianma  desc");
            if (Sort == "null")
                TempHql.AppendFormat("order by C_time desc");
            string HQLstr = string.Format(" from Flow_NonSProductinfo  u where 1=1 {0}", TempHql.ToString());
            List<Flow_NonSProductinfo> list = HibernateTemplate.Find<Flow_NonSProductinfo>(HQLstr) as List<Flow_NonSProductinfo>;
            IList<Flow_NonSProductinfoView> listmodel = GetViewlist(list);
            return listmodel;

        }

        #region //根据物料代码查找产品信息 
        public Flow_NonSProductinfoView Getmodelbywldm(string wldm)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(wldm))
                TempHql.AppendFormat(" and u.Pbianma='{0}'", wldm);
            string HQLstr = string.Format(" from Flow_NonSProductinfo  u where 1=1 {0}", TempHql.ToString());
            List<Flow_NonSProductinfo> list = HibernateTemplate.Find<Flow_NonSProductinfo>(HQLstr) as List<Flow_NonSProductinfo>;
            IList<Flow_NonSProductinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        } 
        #endregion


    }
}
