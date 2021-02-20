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
    /// <summary>
    /// name:销售目标底层
    /// </summary>
    public  class WL_SalestargetDao:ServiceConversion<WL_SalestargetView,WL_Salestarget>,IWL_SalestargetDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(CG_info).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_SalestargetView GetView(WL_Salestarget data)
        {
            WL_SalestargetView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_SalestargetView model)
        {
            WL_Salestarget listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_SalestargetView model)
        {
            WL_Salestarget listmodel = GetData(model);
            return base.Update(listmodel);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_SalestargetView model)
        {
            WL_Salestarget listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_SalestargetView> model)
        {
            IList<WL_Salestarget> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_SalestargetView> NGetList()
        {
            List<WL_Salestarget> listdata = base.GetList() as List<WL_Salestarget>;
            IList<WL_SalestargetView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_SalestargetView> NGetList_id(string id)
        {
            List<WL_Salestarget> list = base.GetList_id(id) as List<WL_Salestarget>;
            IList<WL_SalestargetView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_Salestarget> NGetListID(string id)
        {
            IList<WL_Salestarget> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_SalestargetView NGetModelById(string id)
        {
            WL_SalestargetView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //public PagerInfo<WL_SalestargetView> GetCginfoList(string Name, string RL_is, string Stratrldate, string Endrldate, SessionUser user)
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(Name))
        //        TempHql.AppendFormat(" and u.GysId ='{0}'", Name);
        //    if (!string.IsNullOrEmpty(RL_is))
        //        TempHql.AppendFormat(" and u.Cg_Isdh ='{0}' ", RL_is);
        //    if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
        //        TempHql.AppendFormat("and Cg_xdtime>='{0}' and Cg_xdtime<='{1}'", Stratrldate, Endrldate);
        //    TempHql.AppendFormat(" order by Cg_xdtime desc");
        //    PagerInfo<WL_SalestargetView> list = Search();
        //    return list;
        //}
    }
}
