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
    public class NA_XSdetailsinfoDao : ServiceConversion<NA_XSdetailsinfoView, NA_XSdetailsinfo>, INA_XSdetailsinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_XSdetailsinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_XSdetailsinfoView GetView(NA_XSdetailsinfo data)
        {
            NA_XSdetailsinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_XSdetailsinfoView model)
        {
            NA_XSdetailsinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_XSdetailsinfoView model)
        {
            NA_XSdetailsinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_XSdetailsinfoView model)
        {
            NA_XSdetailsinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_XSdetailsinfoView> model)
        {
            IList<NA_XSdetailsinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_XSdetailsinfoView> NGetList()
        {
            List<NA_XSdetailsinfo> listdata = base.GetList() as List<NA_XSdetailsinfo>;
            IList<NA_XSdetailsinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_XSdetailsinfoView> NGetList_id(string id)
        {
            List<NA_XSdetailsinfo> list = base.GetList_id(id) as List<NA_XSdetailsinfo>;
            IList<NA_XSdetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_XSdetailsinfo> NGetListID(string id)
        {
            IList<NA_XSdetailsinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_XSdetailsinfoView NGetModelById(string id)
        {
            NA_XSdetailsinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据销售订单ID 查找订单明细
        public IList<NA_XSdetailsinfoView> GetxsdetaillistbyxsId(string xsid)
        {
            List<NA_XSdetailsinfo> list = HibernateTemplate.Find<NA_XSdetailsinfo>("from NA_XSdetailsinfo where xsId='" + xsid + "'") as List<NA_XSdetailsinfo>;
            IList<NA_XSdetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据销售订单Id,统计产品数量的
        /// <summary>
        /// 根据销售订单Id,统计产品数量的
        /// </summary>
        /// <param name="xsid">销售订单Id</param>
        /// <returns></returns>
        public decimal GetchsumbyxsId(string xsid)
        {
            try
            {
                string temHql = string.Format("from NA_XSdetailsinfo where   xsId='{0}'", xsid);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(SL)  {0} ", temHql));
                decimal count = Convert.ToDecimal(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

    }
}
