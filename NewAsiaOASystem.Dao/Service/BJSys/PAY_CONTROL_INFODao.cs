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
    public class PAY_CONTROL_INFODao: ServiceConversion<PAY_CONTROL_INFOView,PAY_CONTROL_INFO>,IPAY_CONTROL_INFODao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PAY_CONTROL_INFO).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PAY_CONTROL_INFOView GetView(PAY_CONTROL_INFO data)
        {
            PAY_CONTROL_INFOView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PAY_CONTROL_INFOView model)
        {
            PAY_CONTROL_INFO listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PAY_CONTROL_INFOView model)
        {
            PAY_CONTROL_INFO listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PAY_CONTROL_INFOView model)
        {
            PAY_CONTROL_INFO listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PAY_CONTROL_INFOView> model)
        {
            IList<PAY_CONTROL_INFO> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PAY_CONTROL_INFOView> NGetList()
        {
            List<PAY_CONTROL_INFO> listdata = base.GetList() as List<PAY_CONTROL_INFO>;
            IList<PAY_CONTROL_INFOView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PAY_CONTROL_INFOView> NGetList_id(string id)
        {
            List<PAY_CONTROL_INFO> list = base.GetList_id(id) as List<PAY_CONTROL_INFO>;
            IList<PAY_CONTROL_INFOView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PAY_CONTROL_INFO> NGetListID(string id)
        {
            IList<PAY_CONTROL_INFO> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PAY_CONTROL_INFOView NGetModelById(string id)
        {
            PAY_CONTROL_INFOView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询序号最大的数据
        public PAY_CONTROL_INFOView GetMaxCONTROL_INFO_ID(string bjdtype)
        {
            string Hqlstr = string.Format("from PAY_CONTROL_INFO where CONTROL_INFO_ID=(select  max(CONTROL_INFO_ID)  from PAY_CONTROL_INFO  where bjdtype='{0}')", bjdtype);
            List<PAY_CONTROL_INFO> list = HibernateTemplate.Find<PAY_CONTROL_INFO>(Hqlstr) as List<PAY_CONTROL_INFO>;
            IList<PAY_CONTROL_INFOView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ORDER_NO">订单编号</param>
        /// <param name="CUST_NAME">客户名称</param>
        /// <returns></returns>
        public PagerInfo<PAY_CONTROL_INFOView> GetBJdlistPagerInfo(string ORDER_NO, string CUST_NAME)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(ORDER_NO))
                TempHql.AppendFormat(" and ORDER_NO like '%{0}%'", ORDER_NO);
            if (!string.IsNullOrEmpty(CUST_NAME))
                TempHql.AppendFormat(" and CUST_NAME like '%{0}%'", CUST_NAME);
            TempHql.AppendFormat("order by u.CONTROL_INFO_ID desc");
            PagerInfo<PAY_CONTROL_INFOView> list = Search();
            return list;
        }
        #endregion
    }
}
