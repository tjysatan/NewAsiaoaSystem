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
    public class NQ_SupplierDao : ServiceConversion<NQ_SupplierView, NQ_Supplier>, INQ_SupplierDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_Supplier).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_SupplierView GetView(NQ_Supplier data)
        {
            NQ_SupplierView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_SupplierView model)
        {
            NQ_Supplier listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_SupplierView model)
        {
            NQ_Supplier listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_SupplierView model)
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
        public bool NDelete(List<NQ_SupplierView> model)
        {
            //IList<NQ_Supplier> listmodel = GetDatalist(model);
            //return base.NDelete(listmodel);
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_SupplierView> NGetList()
        {
            List<NQ_Supplier> listdata = base.GetList() as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_SupplierView> NGetList_id(string id)
        {
            List<NQ_Supplier> list = base.GetList_id(id) as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_Supplier> NGetListID(string id)
        {
            IList<NQ_Supplier> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierView NGetModelById(string id)
        {
            NQ_SupplierView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierView NGetModelByFitemid(string FItemid)
        {
            NQ_SupplierView listmodel = GetView(base.GetModelById(FItemid));
            return listmodel;
        }


        public PagerInfo<NQ_SupplierView> GetCinfoList(string suName, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(suName))
            {
                TempHql.AppendFormat(" and u.FName like '%{0}%' ", suName);
            }

            TempHql.AppendFormat(" and u.FLevel = 2  ");
            TempHql.AppendFormat(" order by u.FNumber asc,u.FItemID desc");
            PagerInfo<NQ_SupplierView> list = Search();
            return list;
        }

        public PagerInfo<NQ_SupplierView> getListByNameConTel(string suName,string suCon,string suTel, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(suName))
            {
                TempHql.AppendFormat(" and u.FName like '%{0}%' ", suName);
            }
            if (!string.IsNullOrEmpty(suCon))
            {
                TempHql.AppendFormat(" and u.FContact like '%{0}%' ", suCon);
            }
            if (!string.IsNullOrEmpty(suTel))
            {
                TempHql.AppendFormat(" and u.FPhone like '%{0}%' ", suTel);
            }

            TempHql.AppendFormat(" and u.FLevel = 2  ");
            TempHql.AppendFormat(" order by u.FNumber asc,u.FItemID desc");
            PagerInfo<NQ_SupplierView> list = Search();
            return list;
        }


        #region //查询全部的客户信息
        public IList<NQ_SupplierView> GetlistCust()
        {
            List<NQ_Supplier> list = HibernateTemplate.Find<NQ_Supplier>("from NQ_Supplier order by FItemid asc ") as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据供应商代码查找供应商代码
        public string GetSupplierById(string dm)
        {
            List<NQ_Supplier> list = HibernateTemplate.Find<NQ_Supplier>("from NQ_Supplier where FName='" + dm + "'") as List<NQ_Supplier>;
            string json;
            if (list != null)
            {
                json = JsonConvert.SerializeObject(list[0]);
            }
            else
            {
                json = null;
            }
            return json;
        }
        #endregion

        #region //根据供应商代码查找供应商信息
        public NQ_SupplierView GetSupplierByName(string dm)
        {
            List<NQ_Supplier> list = HibernateTemplate.Find<NQ_Supplier>("from NQ_Supplier where FName='" + dm + "'") as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
            

        }
        #endregion

        #region //根据供应商代码查找供应商信息
        public NQ_SupplierView GetSupplierByFItemId(string FItemID)
        {
            List<NQ_Supplier> list = HibernateTemplate.Find<NQ_Supplier>("from NQ_Supplier where FItemID='" + FItemID + "'") as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(list);
            return listmodel[0];

        }
        #endregion

        #region //根据供应商名称查找供应商信息
        public NQ_SupplierView Getmodelbygysname(string name)
        {
            List<NQ_Supplier> list = HibernateTemplate.Find<NQ_Supplier>("from NQ_Supplier where FName like '%" + name + "%'") as List<NQ_Supplier>;
            IList<NQ_SupplierView> listmodel = GetViewlist(list);
            return listmodel[0];
        }
        #endregion

        public bool GetUpdatedSupplier()
        {
            string maxTimestamp = GetMaxTimestamp();

            IList<NQ_Supplier> list = getUpdatedSupplier(maxTimestamp);

            if (maxTimestamp == null)
            {
                InsertSupplier(list);
            }


            //List<NQ_Supplier> list = HibernateTemplate
            return true;
        }

        private IList<NQ_Supplier> getUpdatedSupplier(string timestamp)
        {
            //string url;
            //url = "http://222.92.203.58:83/Baseitem.asmx/getUpdatedSupplier?modifytime=" + "0";
            //string result = HttpUtility.GetData(url);
            //return result;
            return null;
        }

        private bool InsertSupplier(IList<NQ_Supplier> supplier)
        {
            return false;
        }


        public string GetMaxTimestamp()
        {
            ISession session = GetSession();
            ISQLQuery query = session.CreateSQLQuery(@"SELECT max(FModifyTime) AS timestamp 
                                                        FROM    NQ_Supplier
                                                        where   FItemClassID = 8
                                                        and     FDeleted = 0")
                                                        .AddScalar("timestamp", NHibernateUtil.String);
            string timebyte = Convert.ToString(query.UniqueResult());

            return timebyte;
        }

        public bool saveOrUpdate(IList<NQ_SupplierView> suppliers)
        {
            ISession session = GetSession();

            foreach (var supplier in suppliers)
            {
                if (supplier.FItemID > 0)
                {
                    NUpdate(supplier);
                }
                else
                {
                    Ninsert(supplier);
                }
                //HibernateTemplate.SaveOrUpdate(supplier);
            }

            return true;
        }

        public IDictionary<string,string> getTopSupplier()
        {
            string query = @"SELECT  FNumber,FName
                             FROM    NQ_Supplier
                             where   FItemClassID = 8
                             and     FDeleted = 0 
                             and     FLevel = 1 ";

            DataSet ds = base.GetDataSet(query);

            Dictionary<string, string> fnumberList = new Dictionary<string, string>();

            foreach (DataRow mDr in ds.Tables[0].Rows )
            {
                fnumberList.Add(mDr["fnumber"].ToString(), mDr["fname"].ToString());
            }

            return fnumberList;
        }

    }
}
