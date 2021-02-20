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
    public class DKX_PAY_CONTROL_INFODao:ServiceConversion<DKX_PAY_CONTROL_INFOView,DKX_PAY_CONTROL_INFO>,IDKX_PAY_CONTROL_INFODao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_PAY_CONTROL_INFO).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_PAY_CONTROL_INFOView GetView(DKX_PAY_CONTROL_INFO data)
        {
            DKX_PAY_CONTROL_INFOView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_PAY_CONTROL_INFOView model)
        {
            DKX_PAY_CONTROL_INFO listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_PAY_CONTROL_INFOView model)
        {
            DKX_PAY_CONTROL_INFO listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_PAY_CONTROL_INFOView model)
        {
            DKX_PAY_CONTROL_INFO listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_PAY_CONTROL_INFOView> model)
        {
            IList<DKX_PAY_CONTROL_INFO> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_PAY_CONTROL_INFOView> NGetList()
        {
            List<DKX_PAY_CONTROL_INFO> listdata = base.GetList() as List<DKX_PAY_CONTROL_INFO>;
            IList<DKX_PAY_CONTROL_INFOView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_PAY_CONTROL_INFOView> NGetList_id(string id)
        {
            List<DKX_PAY_CONTROL_INFO> list = base.GetList_id(id) as List<DKX_PAY_CONTROL_INFO>;
            IList<DKX_PAY_CONTROL_INFOView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_PAY_CONTROL_INFO> NGetListID(string id)
        {
            IList<DKX_PAY_CONTROL_INFO> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_PAY_CONTROL_INFOView NGetModelById(string id)
        {
            DKX_PAY_CONTROL_INFOView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过ORDER_NO 查找需求单数据
        //通过ORDER_NO 查找需求单数据
        public DKX_PAY_CONTROL_INFOView GetDKXxuqiubyORDER_NO(string ORDER_NO)
        {
            string Hqlstr = string.Format(" from DKX_PAY_CONTROL_INFO where ORDER_NO='{0}'", ORDER_NO);
            List<DKX_PAY_CONTROL_INFO> list = HibernateTemplate.Find<DKX_PAY_CONTROL_INFO>(Hqlstr) as List<DKX_PAY_CONTROL_INFO>;
            IList<DKX_PAY_CONTROL_INFOView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        } 
        #endregion

        #region //通过ORDER_NO 和订单Id查找需求单
        /// <summary>
        /// 通过报价系统的订单号和订单的Id 查找需求单
        /// </summary>
        /// <param name="ORDER_NO"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DKX_PAY_CONTROL_INFOView GetDKXxuqiubyORDER_NOandId(string ORDER_NO, string Id)
        {
            string Hqlstr = string.Format(" from DKX_PAY_CONTROL_INFO where ORDER_NO='{0}' and dd_Id='{1}'", ORDER_NO, Id);
            List<DKX_PAY_CONTROL_INFO> list = HibernateTemplate.Find<DKX_PAY_CONTROL_INFO>(Hqlstr) as List<DKX_PAY_CONTROL_INFO>;
            IList<DKX_PAY_CONTROL_INFOView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

 
    }
}
