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
    public class NQ_SupplierAttachmentDao : ServiceConversion<NQ_SupplierAttachmentView, NQ_SupplierAttachment>, INQ_SupplierAttachmentDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_SupplierAttachment).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_SupplierAttachmentView GetView(NQ_SupplierAttachment data)
        {
            NQ_SupplierAttachmentView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_SupplierAttachmentView model)
        {
            NQ_SupplierAttachment listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_SupplierAttachmentView model)
        {
            NQ_SupplierAttachment listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_SupplierAttachmentView model)
        {
            NQ_SupplierAttachment listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_SupplierAttachmentView> model)
        {
            //IList<NQ_Supplier> listmodel = GetDatalist(model);
            //return base.NDelete(listmodel);
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_SupplierAttachmentView> NGetList()
        {
            List<NQ_SupplierAttachment> listdata = base.GetList() as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_SupplierAttachmentView> NGetList_id(string id)
        {
            List<NQ_SupplierAttachment> list = base.GetList_id(id) as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_SupplierAttachment> NGetListID(string id)
        {
            IList<NQ_SupplierAttachment> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierAttachmentView NGetModelById(string id)
        {
            NQ_SupplierAttachmentView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_SupplierAttachmentView NGetModelByFitemid(string FItemid)
        {
            //NQ_SupplierAttachment listmodel = GetView(base.GetModelById(FItemid));
            return null;
        }


        public PagerInfo<NQ_SupplierAttachmentView> GetCinfoList(string suName, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(suName))
            {
                TempHql.AppendFormat(" and u.FName like '%{0}%' ", suName);
            }
            TempHql.AppendFormat(" and u.FLevel = 2  ");
            TempHql.AppendFormat(" order by u.FNumber asc,u.FItemID desc");
            PagerInfo<NQ_SupplierAttachmentView> list = Search();
            return list;
        }


        #region //查询全部的客户信息
        public IList<NQ_SupplierAttachmentView> GetlistCust()
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_Supplier order by FItemid asc ") as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据供应商代码查找供应商代码
        public string GetSupplierById(string dm)
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_Supplier where FName='" + dm + "'") as List<NQ_SupplierAttachment>;
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

        #region //根据供应商代码查，以及附近类型查找附件
        public NQ_SupplierAttachmentView GetAttachBySupplierAndType(string supplierid, string attType)
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_SupplierAttachment where FSupplierid ='" + supplierid + "' and FAttType ='" + attType + "' ") as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }

        public NQ_SupplierAttachmentView GetAttachBySupplierAndTypeAndSeq(string supplierid, string attType, string seq)
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_SupplierAttachment where FSupplierid ='" + supplierid + "' and FAttType ='" + attType + "'" + "and seq ='" + seq+ "'") as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
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
        public IList<NQ_SupplierAttachmentView> GetAttachmentBySupplierId(string FSupplierID)
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_SupplierAttachment where FSupplierid='" + FSupplierID + "'") as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
            return listmodel;

        }
        #endregion

        #region //根据供应商名称查找供应商信息
        public NQ_SupplierAttachmentView GetAttachmentByName(string name)
        {
            List<NQ_SupplierAttachment> list = HibernateTemplate.Find<NQ_SupplierAttachment>("from NQ_Supplier where FName like '%" + name + "%'") as List<NQ_SupplierAttachment>;
            IList<NQ_SupplierAttachmentView> listmodel = GetViewlist(list);
            return listmodel[0];
        }
        #endregion

        public bool GetUpdatedSupplier()
        {
            string maxTimestamp = GetMaxTimestamp();

            IList<NQ_SupplierAttachment> list = getUpdatedSupplier(maxTimestamp);

            if (maxTimestamp == null)
            {
                InsertSupplier(list);
            }


            //List<NQ_Supplier> list = HibernateTemplate
            return true;
        }

        private IList<NQ_SupplierAttachment> getUpdatedSupplier(string timestamp)
        {
            //string url;
            //url = "http://222.92.203.58:83/Baseitem.asmx/getUpdatedSupplier?modifytime=" + "0";
            //string result = HttpUtility.GetData(url);
            //return result;
            return null;
        }

        private bool InsertSupplier(IList<NQ_SupplierAttachment> supplier)
        {
            return false;
        }

        private bool UpdateSupplier(IList<NQ_SupplierAttachment> supplier)
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

        public bool saveOrUpdateList(List<NQ_SupplierAttachmentView> atts)
        {
            try
            {
                ISession session = GetSession();

                foreach (var att in atts)
                {
                    HibernateTemplate.SaveOrUpdate(att);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool saveOrUpdate(NQ_SupplierAttachmentView att)
        {
            //try
            //{
                ISession session = GetSession();

                HibernateTemplate.SaveOrUpdate(att);
                return true;

            //}
            //catch (Exception e)
            //{
            //    return false;
            //}

        }

        public IList<NQ_SupplierAttachmentView> getAttachBySunameAndType(string suname, string ftype)
        {
            ISession session = GetSession();
            string sql = "";
            sql += "select	{yj.*} from	NQ_SupplierAttachment {yj} ";
            sql += "left	join	NQ_OASupplier sb on sb.ID = {yj}.FSupplierid  ";
            sql += "where	sb.FName like '%"+ suname + "%'";
            if ( !String.IsNullOrEmpty( ftype ) )
            {
                sql += "and  	sb.Fatttype = '" + ftype + "'";
            }

            IQuery query = session.CreateSQLQuery(sql)
                .AddEntity("yj", typeof(NQ_SupplierAttachment));
                //.SetString("supplierid", supplierid);
            return (query.List<NQ_SupplierAttachment>() as List<NQ_SupplierAttachment>).ConvertAll(x => GetView(x));
        }

        public PagerInfo<NQ_SupplierAttachmentView> setPagerInfo(IList<NQ_SupplierAttachmentView> list, int pageIndex, int pageSize)
        {
            pager.CurrentPageIndex = pageIndex;
            pager.PageSize = pageSize;

            pager.RecordCount = list.Count();

            //计算起始的行
            int index = (pager.CurrentPageIndex - 1) * pager.PageSize;
            if (index > pager.RecordCount)
            {
                index = 0;
                pager.CurrentPageIndex = 1;
            }

            //query.SetFirstResult(index);
            //query.SetMaxResults(pager.PageSize);

            pager.DataList = list
                            .Skip(pager.PageSize * (pager.CurrentPageIndex - 1))
                            .Take(pager.PageSize)
                            .ToList();

            pager.GetPagingDataJson = JsonConvert.SerializeObject(pager.DataList);
            return pager;

        }

    }
}
