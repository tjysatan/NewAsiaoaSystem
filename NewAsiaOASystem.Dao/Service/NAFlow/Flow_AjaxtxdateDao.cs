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
    public class Flow_AjaxtxdateDao:ServiceConversion<Flow_AjaxtxdateView,Flow_Ajaxtxdate>,IFlow_AjaxtxdateDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private StringBuilder NTempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_Ajaxtxdate).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_AjaxtxdateView GetView(Flow_Ajaxtxdate data)
        {
            Flow_AjaxtxdateView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_AjaxtxdateView model)
        {
            Flow_Ajaxtxdate listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_AjaxtxdateView model)
        {
            Flow_Ajaxtxdate listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_AjaxtxdateView model)
        {
            Flow_Ajaxtxdate listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_AjaxtxdateView> model)
        {
            IList<Flow_Ajaxtxdate> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_AjaxtxdateView> NGetList()
        {
            List<Flow_Ajaxtxdate> listdata = base.GetList() as List<Flow_Ajaxtxdate>;
            IList<Flow_AjaxtxdateView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_AjaxtxdateView> NGetList_id(string id)
        {
            List<Flow_Ajaxtxdate> list = base.GetList_id(id) as List<Flow_Ajaxtxdate>;
            IList<Flow_AjaxtxdateView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_Ajaxtxdate> NGetListID(string id)
        {
            IList<Flow_Ajaxtxdate> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_AjaxtxdateView NGetModelById(string id)
        {
            Flow_AjaxtxdateView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 查询需要提醒的通知数据
        /// </summary>
        /// <param name="Type">当前需要通知的状态</param>
        /// <param name="tzdtype"></param>
        /// <returns></returns>
        public IList<Flow_AjaxtxdateView> GetWTZajaxtxdate(string Type, string tzdtype)
        {
            string HQLstr = string.Format("from Flow_Ajaxtxdate where Type in ({0}) and tzdtype in ({1}) and Istz='1'", Type, tzdtype);
            List<Flow_Ajaxtxdate> list = HibernateTemplate.Find<Flow_Ajaxtxdate>(HQLstr) as List<Flow_Ajaxtxdate>;
            IList<Flow_AjaxtxdateView> listmodel = GetViewlist(list);
            return listmodel;
        }


    }
}
