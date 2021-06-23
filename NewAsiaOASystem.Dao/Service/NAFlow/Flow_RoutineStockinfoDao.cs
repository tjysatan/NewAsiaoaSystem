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
    public  class Flow_RoutineStockinfoDao:ServiceConversion<Flow_RoutineStockinfoView,Flow_RoutineStockinfo>,IFlow_RoutineStockinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_RoutineStockinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_RoutineStockinfoView GetView(Flow_RoutineStockinfo data)
        {
            Flow_RoutineStockinfoView view = ConvertToDTO(data);
            return view;
        }




        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_RoutineStockinfoView model)
        {
            Flow_RoutineStockinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_RoutineStockinfoView model)
        {
            Flow_RoutineStockinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_RoutineStockinfoView model)
        {
            Flow_RoutineStockinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_RoutineStockinfoView> model)
        {
            IList<Flow_RoutineStockinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> NGetList()
        {
            List<Flow_RoutineStockinfo> listdata = base.GetList() as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> NGetList_id(string id)
        {
            List<Flow_RoutineStockinfo> list = base.GetList_id(id) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_RoutineStockinfo> NGetListID(string id)
        {
            IList<Flow_RoutineStockinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_RoutineStockinfoView NGetModelById(string id)
        {
            Flow_RoutineStockinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //温控产品
        /// <summary>
        /// 常规产品的库存信息数据
        /// </summary>
        /// <param name="Sort">排序方式 0 倒序asc   1正序DESC </param>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> GetxsinfobyOrderCode(string Sort, string Category, string cpname, string wlSort)
        {
            string HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' order by A_Sum asc ", Category);
            if (Sort != "null")
            {
                if (Sort == "0")
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' and P_Name like '%{1}%' order by A_Sum asc ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' order by A_Sum asc ", Category);
                    }
                }
                else
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' and P_Name like '%{1}%' order by A_Sum DESC ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' order by A_Sum DESC ", Category);
                    }
                }
            }
            if (wlSort != "null")
            {
                if (wlSort == "0")
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' and P_Name like '%{1}%'  order by P_Bianhao asc ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' order by P_Bianhao asc ", Category);
                    }
                }
                else
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' and P_Name like '%{1}%' order by P_Bianhao DESC ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='0' order by P_Bianhao DESC ", Category);
                    }
                }
            }

            List<Flow_RoutineStockinfo> list = HibernateTemplate.Find<Flow_RoutineStockinfo>(HQLstr) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据 是否被锁定的插好 产品的库存信息
        /// </summary>
        /// <param name="Isscing"></param>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> GetCpkcInfo(string Isscing)
        {
            string hqlstr = string.Format("from Flow_RoutineStockinfo where Isscing='{0}' and state='0' order by A_Sum DESC", Isscing);
            List<Flow_RoutineStockinfo> list = HibernateTemplate.Find<Flow_RoutineStockinfo>(hqlstr) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        //通过物料代码查询常规产品的信息
        public Flow_RoutineStockinfoView Getmodelinfobyp_bianhao(string p_bianhao)
        {
            string HQLstr = string.Format("from Flow_RoutineStockinfo where P_Bianhao='{0}'", p_bianhao);
            List<Flow_RoutineStockinfo> list = HibernateTemplate.Find<Flow_RoutineStockinfo>(HQLstr) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        } 

        #endregion

        #region //常规电控箱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sort">告警数量排序 0 倒序 1正序</param>
        /// <param name="Category">0 成品 1半成品</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="wlSort">物料代码排序</param>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> DKGetcgDATAinfo(string Sort, string Category, string cpname, string wlSort)
        {
            string HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' order by A_Sum asc ", Category);
            if (Sort != "null")
            {
                if (Sort == "0")
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' and P_Name like '%{1}%' order by A_Sum asc ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' order by A_Sum asc ", Category);
                    }
                }
                else
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' and P_Name like '%{1}%' order by A_Sum DESC ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' order by A_Sum DESC ", Category);
                    }
                }
            }
            if (wlSort != "null")
            {
                if (wlSort == "0")
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' and P_Name like '%{1}%'  order by P_Bianhao asc ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' order by P_Bianhao asc ", Category);
                    }
                }
                else
                {
                    if (cpname != "null")
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='1' and P_Name like '%{1}%' order by P_Bianhao DESC ", Category, cpname);
                    }
                    else
                    {
                        HQLstr = string.Format("from Flow_RoutineStockinfo where Category='{0}' and state='0' and type='12' order by P_Bianhao DESC ", Category);
                    }
                }
            }

            List<Flow_RoutineStockinfo> list = HibernateTemplate.Find<Flow_RoutineStockinfo>(HQLstr) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 常规电控箱不在生产的中的数据
        /// </summary>
        /// <param name="Isscing">是否锁定 0 未锁定 1锁定</param>
        /// <returns></returns>
        public IList<Flow_RoutineStockinfoView> DKXGetCpkcInfo(string Isscing)
        {
            string hqlstr = string.Format("from Flow_RoutineStockinfo where Isscing='{0}' and state='0' and type='1' order by A_Sum DESC",Isscing);
            List<Flow_RoutineStockinfo> list = HibernateTemplate.Find<Flow_RoutineStockinfo>(hqlstr) as List<Flow_RoutineStockinfo>;
            IList<Flow_RoutineStockinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        #endregion

        #region //常规电控箱的分页数据
        /// <summary>
        /// 常规电控温控的分页数据
        /// </summary>
        /// <param name="cpname">产品名称</param>
        /// <param name="wlno">物料号</param>
        /// <param name="type">类型 0 温控 1电控</param>
        /// <returns></returns>
        public PagerInfo<Flow_RoutineStockinfoView> Getcgdiankongpagerlist(string cpname, string wlno, string type,string category)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(cpname))
                TempHql.AppendFormat(" and P_Name like '%{0}%'", cpname);
            if (!string.IsNullOrEmpty(wlno))
                TempHql.AppendFormat(" and P_Bianhao like '%{0}%'", wlno);
            if (!string.IsNullOrEmpty(category))
                TempHql.AppendFormat(" and Category='{0}'", category);
            TempHql.AppendFormat(" and type='{0}'", type);
            string HQLstr = string.Format("from DKX_DDinfo u where 1=1 {0}", TempHql.ToString());
            TempHql.AppendFormat("order by A_Sum DESC");
            PagerInfo<Flow_RoutineStockinfoView> list = Search();
            return list;


        }
        #endregion


    }
}
