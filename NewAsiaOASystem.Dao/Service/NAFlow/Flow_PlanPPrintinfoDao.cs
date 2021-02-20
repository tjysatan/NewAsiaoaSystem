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
    public class Flow_PlanPPrintinfoDao:ServiceConversion<Flow_PlanPPrintinfoView,Flow_PlanPPrintinfo>,IFlow_PlanPPrintinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_PlanPPrintinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_PlanPPrintinfoView GetView(Flow_PlanPPrintinfo data)
        {
            Flow_PlanPPrintinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_PlanPPrintinfoView model)
        {
            Flow_PlanPPrintinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_PlanPPrintinfoView model)
        {
            Flow_PlanPPrintinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_PlanPPrintinfoView model)
        {
            Flow_PlanPPrintinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_PlanPPrintinfoView> model)
        {
            IList<Flow_PlanPPrintinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_PlanPPrintinfoView> NGetList()
        {
            List<Flow_PlanPPrintinfo> listdata = base.GetList() as List<Flow_PlanPPrintinfo>;
            IList<Flow_PlanPPrintinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_PlanPPrintinfoView> NGetList_id(string id)
        {
            List<Flow_PlanPPrintinfo> list = base.GetList_id(id) as List<Flow_PlanPPrintinfo>;
            IList<Flow_PlanPPrintinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_PlanPPrintinfo> NGetListID(string id)
        {
            IList<Flow_PlanPPrintinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_PlanPPrintinfoView NGetModelById(string id)
        {
            Flow_PlanPPrintinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据订单查询打印信息
        /// </summary>
        /// <param name="Plan_Id">生产计划单Id</param>
        /// <returns></returns>
        public Flow_PlanPPrintinfoView GetFlow_PlanprinmodelbypId(string Plan_Id)
        {
            string HQLstr = string.Format("from Flow_PlanPPrintinfo where Plan_Id='{0}'", Plan_Id);
            List<Flow_PlanPPrintinfo> list = HibernateTemplate.Find<Flow_PlanPPrintinfo>(HQLstr) as List<Flow_PlanPPrintinfo>;
            IList<Flow_PlanPPrintinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }

 
     
    }
}
