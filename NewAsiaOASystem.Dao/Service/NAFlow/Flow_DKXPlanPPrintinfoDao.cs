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
    public class Flow_DKXPlanPPrintinfoDao:ServiceConversion<Flow_DKXPlanPPrintinfoView,Flow_DKXPlanPPrintinfo>,IFlow_DKXPlanPPrintinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_DKXPlanPPrintinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_DKXPlanPPrintinfoView GetView(Flow_DKXPlanPPrintinfo data)
        {
            Flow_DKXPlanPPrintinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_DKXPlanPPrintinfoView model)
        {
            Flow_DKXPlanPPrintinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_DKXPlanPPrintinfoView model)
        {
            Flow_DKXPlanPPrintinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_DKXPlanPPrintinfoView model)
        {
            Flow_DKXPlanPPrintinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_DKXPlanPPrintinfoView> model)
        {
            IList<Flow_DKXPlanPPrintinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_DKXPlanPPrintinfoView> NGetList()
        {
            List<Flow_DKXPlanPPrintinfo> listdata = base.GetList() as List<Flow_DKXPlanPPrintinfo>;
            IList<Flow_DKXPlanPPrintinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_DKXPlanPPrintinfoView> NGetList_id(string id)
        {
            List<Flow_DKXPlanPPrintinfo> list = base.GetList_id(id) as List<Flow_DKXPlanPPrintinfo>;
            IList<Flow_DKXPlanPPrintinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_DKXPlanPPrintinfo> NGetListID(string id)
        {
            IList<Flow_DKXPlanPPrintinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_DKXPlanPPrintinfoView NGetModelById(string id)
        {
            Flow_DKXPlanPPrintinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据订单查询打印信息
        /// </summary>
        /// <param name="Plan_Id">生产计划单Id</param>
        /// <returns></returns>
        public Flow_DKXPlanPPrintinfoView GetFlow_PlanprinmodelbypId(string Plan_Id)
        {
            string HQLstr = string.Format("from Flow_DKXPlanPPrintinfo where Plan_Id='{0}'", Plan_Id);
            List<Flow_DKXPlanPPrintinfo> list = HibernateTemplate.Find<Flow_DKXPlanPPrintinfo>(HQLstr) as List<Flow_DKXPlanPPrintinfo>;
            IList<Flow_DKXPlanPPrintinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
    }
}
