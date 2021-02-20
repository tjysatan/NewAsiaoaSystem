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
    public class DKX_CONTROL_LISTDao:ServiceConversion<DKX_CONTROL_LISTView,DKX_CONTROL_LIST>,IDKX_CONTROL_LISTDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_CONTROL_LIST).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_CONTROL_LISTView GetView(DKX_CONTROL_LIST data)
        {
            DKX_CONTROL_LISTView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_CONTROL_LISTView model)
        {
            DKX_CONTROL_LIST listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_CONTROL_LISTView model)
        {
            DKX_CONTROL_LIST listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_CONTROL_LISTView model)
        {
            DKX_CONTROL_LIST listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_CONTROL_LISTView> model)
        {
            IList<DKX_CONTROL_LIST> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_CONTROL_LISTView> NGetList()
        {
            List<DKX_CONTROL_LIST> listdata = base.GetList() as List<DKX_CONTROL_LIST>;
            IList<DKX_CONTROL_LISTView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_CONTROL_LISTView> NGetList_id(string id)
        {
            List<DKX_CONTROL_LIST> list = base.GetList_id(id) as List<DKX_CONTROL_LIST>;
            IList<DKX_CONTROL_LISTView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_CONTROL_LIST> NGetListID(string id)
        {
            IList<DKX_CONTROL_LIST> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_CONTROL_LISTView NGetModelById(string id)
        {
            DKX_CONTROL_LISTView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过ORDER_NO 查找需求单的ID查找料单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ORDER_NO"></param>
        /// <returns></returns>
        public DKX_CONTROL_LISTView Getliaodanbyorderno(string ORDER_NO)
        {
            string Hqlstr = string.Format(" from DKX_CONTROL_LIST where CONTROL_INFO_NO in(select CONTROL_INFO_NO from DKX_PAY_CONTROL_INFO where  ORDER_NO='{0}')", ORDER_NO);
            List<DKX_CONTROL_LIST> list = HibernateTemplate.Find<DKX_CONTROL_LIST>(Hqlstr) as List<DKX_CONTROL_LIST>;
            IList<DKX_CONTROL_LISTView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion



        #region //通过需求单号 查询料单
        /// <summary>
        /// 通过需求单号 查询料单
        /// </summary>
        /// <param name="xqno">xqno</param>
        /// <returns></returns>
        public DKX_CONTROL_LISTView Getliaodanbyxqno(string xqno)
        {
            string Hqlstr = string.Format("from DKX_CONTROL_LIST where CONTROL_INFO_NO='{0}'", xqno);
            List<DKX_CONTROL_LIST> list = HibernateTemplate.Find<DKX_CONTROL_LIST>(Hqlstr) as List<DKX_CONTROL_LIST>;
            IList<DKX_CONTROL_LISTView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //通过需求单号和订单号查找料单主表明细
        /// <summary>
        /// 通过需求单号和订单号查找料单主表明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <param name="oId">订单Id</param>
        /// <returns></returns>
        public DKX_CONTROL_LISTView GetliaodanbyxqnoandoId(string xqno, string oId)
        {
            string Hqlstr = string.Format("from DKX_CONTROL_LIST where CONTROL_INFO_NO='{0}' and dd_Id='{1}'", xqno,oId);
            List<DKX_CONTROL_LIST> list = HibernateTemplate.Find<DKX_CONTROL_LIST>(Hqlstr) as List<DKX_CONTROL_LIST>;
            IList<DKX_CONTROL_LISTView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
