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
    public class Flow_PleasepurchaseinfoDao:ServiceConversion<Flow_PleasepurchaseinfoView,Flow_Pleasepurchaseinfo>,IFlow_PleasepurchaseinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_Pleasepurchaseinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_PleasepurchaseinfoView GetView(Flow_Pleasepurchaseinfo data)
        {
            Flow_PleasepurchaseinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_PleasepurchaseinfoView model)
        {
            Flow_Pleasepurchaseinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_PleasepurchaseinfoView model)
        {
            Flow_Pleasepurchaseinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_PleasepurchaseinfoView model)
        {
            Flow_Pleasepurchaseinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_PleasepurchaseinfoView> model)
        {
            IList<Flow_Pleasepurchaseinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_PleasepurchaseinfoView> NGetList()
        {
            List<Flow_Pleasepurchaseinfo> listdata = base.GetList() as List<Flow_Pleasepurchaseinfo>;
            IList<Flow_PleasepurchaseinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_PleasepurchaseinfoView> NGetList_id(string id)
        {
            List<Flow_Pleasepurchaseinfo> list = base.GetList_id(id) as List<Flow_Pleasepurchaseinfo>;
            IList<Flow_PleasepurchaseinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_Pleasepurchaseinfo> NGetListID(string id)
        {
            IList<Flow_Pleasepurchaseinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_PleasepurchaseinfoView NGetModelById(string id)
        {
            Flow_PleasepurchaseinfoView listmodel = GetView(base.GetModelById(id));
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
        public PagerInfo<Flow_PleasepurchaseinfoView> GetCinfoList(string CPName, string P_Pianhao, string P_Issc, string starttime, string endtime,string cgy)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(CPName))
                TempHql.AppendFormat(" and u.Yqj_Name like '%{0}%' ", CPName);
            if (!string.IsNullOrEmpty(P_Pianhao))
                TempHql.AppendFormat(" and u.Yqj_bianhao='{0}' ", P_Pianhao);
            if (!string.IsNullOrEmpty(P_Issc))
                TempHql.AppendFormat(" and u.Type='{0}'", P_Issc);
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            if (!string.IsNullOrEmpty(cgy))
                TempHql.AppendFormat("and cgy='{0}'",cgy); 
            TempHql.AppendFormat(" order by u.C_time asc");
            PagerInfo<Flow_PleasepurchaseinfoView> list = Search();
            return list;
        }


        //通过生产计划单的Id 和元器件WL_DM 检测是否已经下单
        public bool Jcqgd(string P_Id, string wl_dm)
        {
            string HQLstr = string.Format("from Flow_Pleasepurchaseinfo where P_Id='{0}' and Yqj_bianhao='{1}'", P_Id,wl_dm);
            List<Flow_Pleasepurchaseinfo> list = HibernateTemplate.Find<Flow_Pleasepurchaseinfo>(HQLstr) as List<Flow_Pleasepurchaseinfo>;
            return list.Count > 0 ? true : false;
        }

        //通过元器件的物料代码查询采购计划单中是否有未处理的
        public bool checkqgdbywlbm(string wl_dm)
        {
            string HQLstr = string.Format("from Flow_Pleasepurchaseinfo where Type='0' and Yqj_bianhao='{0}'",wl_dm);
            List<Flow_Pleasepurchaseinfo> list = HibernateTemplate.Find<Flow_Pleasepurchaseinfo>(HQLstr) as List<Flow_Pleasepurchaseinfo>;
            return list.Count > 0 ? true : false;
        }

        //通过元器件的物料大妈查找采购计划中采购员没有出来的采购计划单
        public Flow_PleasepurchaseinfoView Getqgdmodelbynm(string wl_dm)
        {
            string HQLstr = string.Format("from Flow_Pleasepurchaseinfo where Type='0' and Yqj_bianhao='{0}'", wl_dm);
            List<Flow_Pleasepurchaseinfo> list = HibernateTemplate.Find<Flow_Pleasepurchaseinfo>(HQLstr) as List<Flow_Pleasepurchaseinfo>;
            IList<Flow_PleasepurchaseinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
    }
}
