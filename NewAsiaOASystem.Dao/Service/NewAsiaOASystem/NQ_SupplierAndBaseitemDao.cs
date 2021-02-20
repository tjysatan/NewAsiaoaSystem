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
using System.Data;

namespace NewAsiaOASystem.Dao
{
    public class NQ_SupplierAndBaseitemDao : ServiceConversion<NQ_SupplierAndBaseitemView, NQ_SupplierAndBaseitem>, INQ_SupplierAndBaseitemDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_SupplierAndBaseitem).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_SupplierAndBaseitemView GetView(NQ_SupplierAndBaseitem data)
        {
            NQ_SupplierAndBaseitemView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_SupplierAndBaseitemView model)
        {
            NQ_SupplierAndBaseitem listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_SupplierAndBaseitemView model)
        {
            NQ_SupplierAndBaseitem listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_SupplierAndBaseitemView model)
        {
            //NQ_Supplier listmodel = GetData(model);
            //return base.Delete(listmodel);
            return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_SupplierAndBaseitemView> model)
        {
            //IList<NQ_Supplier> listmodel = GetDatalist(model);
            //return base.NDelete(listmodel);
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_SupplierAndBaseitemView> NGetList()
        {
            List<NQ_SupplierAndBaseitem> listdata = base.GetList() as List<NQ_SupplierAndBaseitem>;
            IList<NQ_SupplierAndBaseitemView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_SupplierAndBaseitemView> NGetList_id(string id)
        {
            List<NQ_SupplierAndBaseitem> list = base.GetList_id(id) as List<NQ_SupplierAndBaseitem>;
            IList<NQ_SupplierAndBaseitemView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_SupplierAndBaseitem> NGetListID(string id)
        {
            IList<NQ_SupplierAndBaseitem> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierAndBaseitemView NGetModelById(string id)
        {
            NQ_SupplierAndBaseitemView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierAndBaseitemView NGetModelByFitemid(string FItemid)
        {
            return null;
        }

        public IList<NQ_SupplierAndBaseitemView> getSupplierByItemid(string itemid)
        {
            List<NQ_SupplierAndBaseitem> list = HibernateTemplate.Find<NQ_SupplierAndBaseitem>("from NQ_SupplierAndBaseitem where itemid='" + itemid + "'") as List<NQ_SupplierAndBaseitem>;
            IList<NQ_SupplierAndBaseitemView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        public IList<NQ_SupplierAndBaseitemView> getBaseitemBySupplierid(string supplierid)
        {
            List<NQ_SupplierAndBaseitem> list = HibernateTemplate.Find<NQ_SupplierAndBaseitem>("from NQ_SupplierAndBaseitem where supplierid='" + supplierid + "'") as List<NQ_SupplierAndBaseitem>;
            IList<NQ_SupplierAndBaseitemView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        public NQ_SupplierAndBaseitemView getBySupplierAndItem(string supplierid, string itemid)
        {
            List<NQ_SupplierAndBaseitem> list = HibernateTemplate.Find<NQ_SupplierAndBaseitem>("from NQ_SupplierAndBaseitem where supplierid='" + supplierid + "' and itemid = '" + itemid + "'") as List<NQ_SupplierAndBaseitem>;
            IList<NQ_SupplierAndBaseitemView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }
    }
        
}
