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
    public class DKX_CONTROL_LIST_DETAILDao : ServiceConversion<DKX_CONTROL_LIST_DETAILView, DKX_CONTROL_LIST_DETAIL>, IDKX_CONTROL_LIST_DETAILDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_CONTROL_LIST_DETAIL).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_CONTROL_LIST_DETAILView GetView(DKX_CONTROL_LIST_DETAIL data)
        {
            DKX_CONTROL_LIST_DETAILView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_CONTROL_LIST_DETAILView model)
        {
            DKX_CONTROL_LIST_DETAIL listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_CONTROL_LIST_DETAILView model)
        {
            DKX_CONTROL_LIST_DETAIL listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_CONTROL_LIST_DETAILView model)
        {
            DKX_CONTROL_LIST_DETAIL listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_CONTROL_LIST_DETAILView> model)
        {
            IList<DKX_CONTROL_LIST_DETAIL> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_CONTROL_LIST_DETAILView> NGetList()
        {
            List<DKX_CONTROL_LIST_DETAIL> listdata = base.GetList() as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_CONTROL_LIST_DETAILView> NGetList_id(string id)
        {
            List<DKX_CONTROL_LIST_DETAIL> list = base.GetList_id(id) as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_CONTROL_LIST_DETAIL> NGetListID(string id)
        {
            IList<DKX_CONTROL_LIST_DETAIL> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_CONTROL_LIST_DETAILView NGetModelById(string id)
        {
            DKX_CONTROL_LIST_DETAILView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过订单NO查找需求明细
        public IList<DKX_CONTROL_LIST_DETAILView> Getliaodanmxbyorderno(string ORDER_NO)
        {
            string Hqlstr = string.Format(" from DKX_CONTROL_LIST_DETAIL where CONTROL_INFO_NO in(select CONTROL_INFO_NO from DKX_PAY_CONTROL_INFO where  ORDER_NO='{0}')", ORDER_NO);
            List<DKX_CONTROL_LIST_DETAIL> list = HibernateTemplate.Find<DKX_CONTROL_LIST_DETAIL>(Hqlstr) as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion

        #region //通过需求单号查询非控制部分的料单明细
        /// <summary>
        /// 通过需求单号查询非控制部分的料单明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <returns></returns>
        public IList<DKX_CONTROL_LIST_DETAILView> GetliaodanFKZbyXQno(string xqno)
        {
            string Hqlstr = string.Format("from DKX_CONTROL_LIST_DETAIL where CONTROL_INFO_NO='{0}' and PARTS_TYPE not in (7,16,17,18,25,26,27) ORDER BY PARTS_TYPE ASC", xqno);
            List<DKX_CONTROL_LIST_DETAIL> list = HibernateTemplate.Find<DKX_CONTROL_LIST_DETAIL>(Hqlstr) as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion

        #region //通过需求单号查询控制部分的料单明细
        /// <summary>
        /// 通过需求单号查询控制部分的料单明细
        /// </summary>
        /// <param name="xqno">需求单号</param>
        /// <returns></returns>
        public IList<DKX_CONTROL_LIST_DETAILView> GetliaodanKZbyXQNO(string xqno)
        {
            string Hqlstr = string.Format("from DKX_CONTROL_LIST_DETAIL where CONTROL_INFO_NO='{0}' and PARTS_TYPE in (7,16,17,18,25,26,27) ORDER BY PARTS_TYPE ASC", xqno);
            List<DKX_CONTROL_LIST_DETAIL> list = HibernateTemplate.Find<DKX_CONTROL_LIST_DETAIL>(Hqlstr) as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion

        #region //通过需求单号和订单号查询料单明细
        /// <summary>
        /// 通过需求单号和订单号查询料单明细
        /// </summary>
        /// <param name="xqNo">报价系统关联号</param>
        /// <param name="oId">订单号</param>
        /// <returns></returns>
        public IList<DKX_CONTROL_LIST_DETAILView> GetliaodanmxbyxqnoandoId(string xqNo, string oId)
        {
            string Hqstr = string.Format("from DKX_CONTROL_LIST_DETAIL where CONTROL_INFO_NO in(select CONTROL_INFO_NO from  DKX_PAY_CONTROL_INFO where dd_Id='{0}' and ORDER_NO='{1}') and dd_Id='{2}' ORDER BY PARTS_TYPE ASC", oId, xqNo, oId);
            List<DKX_CONTROL_LIST_DETAIL> list = HibernateTemplate.Find<DKX_CONTROL_LIST_DETAIL>(Hqstr) as List<DKX_CONTROL_LIST_DETAIL>;
            IList<DKX_CONTROL_LIST_DETAILView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion
    }
}
