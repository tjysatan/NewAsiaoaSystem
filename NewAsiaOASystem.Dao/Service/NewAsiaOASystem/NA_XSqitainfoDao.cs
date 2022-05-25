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
    public class NA_XSqitainfoDao: ServiceConversion<NA_XSqitainfoView, NA_XSqitainfo>, INA_XSqitainfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_XSqitainfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_XSqitainfoView GetView(NA_XSqitainfo data)
        {
            NA_XSqitainfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_XSqitainfoView model)
        {
            NA_XSqitainfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_XSqitainfoView model)
        {
            NA_XSqitainfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_XSqitainfoView model)
        {
            NA_XSqitainfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_XSqitainfoView> model)
        {
            IList<NA_XSqitainfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_XSqitainfoView> NGetList()
        {
            List<NA_XSqitainfo> listdata = base.GetList() as List<NA_XSqitainfo>;
            IList<NA_XSqitainfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_XSqitainfoView> NGetList_id(string id)
        {
            List<NA_XSqitainfo> list = base.GetList_id(id) as List<NA_XSqitainfo>;
            IList<NA_XSqitainfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_XSqitainfo> NGetListID(string id)
        {
            IList<NA_XSqitainfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_XSqitainfoView NGetModelById(string id)
        {
            if (GetModelById(id) != null)
            {
                NA_XSqitainfoView listmodel = GetView(base.GetModelById(id));
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        #region //通过电商的订单的Id查询销售订单的其他信息
        /// <summary>
        /// 通过电商的订单的Id查询销售订单的其他信息
        /// </summary>
        /// <param name="DsOrder_Id">电商销售订单Id</param>
        /// <returns></returns>
        public NA_XSqitainfoView GetModelByDsOrder_Id(string DsOrder_Id)
        {
            List<NA_XSqitainfo> list = HibernateTemplate.Find<NA_XSqitainfo>("from NA_XSqitainfo where DsOrder_Id='" + DsOrder_Id + "'") as List<NA_XSqitainfo>;
            IList<NA_XSqitainfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion
    }
}
