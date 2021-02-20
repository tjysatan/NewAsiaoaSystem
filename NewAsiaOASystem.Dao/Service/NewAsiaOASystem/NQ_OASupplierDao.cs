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
    public class NQ_OASupplierDao : ServiceConversion<NQ_OASupplierView, NQ_OASupplier>, INQ_OASupplierDao
    {

        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_OASupplier).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_OASupplierView GetView(NQ_OASupplier data)
        {
            NQ_OASupplierView view = ConvertToDTO(data);
            if (data.baseitems != null)
            {
                List<NQ_YJinfo> items = data.baseitems.ToList();
                items = items.Where(x => x != null).ToList<NQ_YJinfo>();
                view.baseitems = JsonConvert.SerializeObject(items);
            }
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_OASupplierView model)
        {
            NQ_OASupplier listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_OASupplierView model)
        {
            NQ_OASupplier listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_OASupplierView model)
        {
            NQ_OASupplier listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_OASupplierView> model)
        {
            //IList<NQ_OASupplier> listmodel = GetDatalist(model);
            //return base.NDelete(listmodel);
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_OASupplierView> NGetList()
        {
            List<NQ_OASupplier> listdata = base.GetList() as List<NQ_OASupplier>;
            IList<NQ_OASupplierView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_OASupplierView> NGetList_id(string id)
        {
            List<NQ_OASupplier> list = base.GetList_id(id) as List<NQ_OASupplier>;
            IList<NQ_OASupplierView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_OASupplierView NGetModelById(string id)
        {
            NQ_OASupplierView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }
        public NQ_OASupplier NGetModeldataById(string id)
        {
            NQ_OASupplier listmodel = base.GetModelById(id);
            return listmodel;
        }
        public bool baseUpdate(NQ_OASupplier smodel)
        {
            base.Update(smodel);
            return true;
        }

        public bool baseInsert(NQ_OASupplier smodel)
        {
            base.insert(smodel);
            return true;
        }


        public PagerInfo<NQ_OASupplierView> getListByNameConTel(string suName, string suCon, string suTel, string suStatus, SessionUser user)
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

            TempHql.AppendFormat(" order by u.FCreateDate desc , u.FNumber asc");
            PagerInfo<NQ_OASupplierView> list = Search();
            return list;
        }

        public IList<NQ_OASupplierView> getSearchList(string suName, string suCon, string suTel)
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

            TempHql.AppendFormat(" order by u.FCreateDate desc , u.FNumber asc");

            ISession session = GetSession();

            string hql = GetSearchHql();

            string tempHql = hql;
            ///得到第一个from
            int inde = tempHql.IndexOf("from");
            if (tempHql.Contains("group"))
            {
                int inde1 = tempHql.IndexOf("group");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("GROUP"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("GROUP");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("order"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("order");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("ORDER"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("ORDER");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else
            {
                tempHql = tempHql.Substring(inde);
            }

            IQuery query = session.CreateQuery(hql);

            //list = query.List<NQ_OASupplier>();

            return (query.List<NQ_OASupplier>() as List<NQ_OASupplier>).ConvertAll(x => GetView(x));
        }

        public PagerInfo<NQ_OASupplierView> setPagerInfo(IList<NQ_OASupplierView> list, int pageIndex, int pageSize)
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

            //return null;
        }

        public PagerInfo<NQ_OASupplierView> Search()
        {
            //ISession session = GetSession();
            //string hql = GetSearchHql();

            //string tempHql = hql;
            /////得到第一个from
            //int inde = tempHql.IndexOf("from");
            //if (tempHql.Contains("group"))
            //{
            //    int inde1 = tempHql.IndexOf("group");
            //    tempHql = tempHql.Substring(inde, inde1 - inde);
            //}
            //else if (tempHql.Contains("GROUP"))///获得from和后边的字符
            //{
            //    int inde1 = tempHql.IndexOf("GROUP");
            //    tempHql = tempHql.Substring(inde, inde1 - inde);
            //}
            //else if (tempHql.Contains("order"))///获得from和后边的字符
            //{
            //    int inde1 = tempHql.IndexOf("order");
            //    tempHql = tempHql.Substring(inde, inde1 - inde);
            //}
            //else if (tempHql.Contains("ORDER"))///获得from和后边的字符
            //{
            //    int inde1 = tempHql.IndexOf("ORDER");
            //    tempHql = tempHql.Substring(inde, inde1 - inde);
            //}
            //else
            //{
            //    tempHql = tempHql.Substring(inde);
            //}

            //IQuery queryCount = session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            ////计算总记录数       
            ////pager.RecordCount = Convert.ToInt32(queryCount.UniqueResult());

            //IQuery query = session.CreateQuery(hql);

            //////计算起始的行
            ////int index = (pager.CurrentPageIndex - 1) * pager.PageSize;
            ////if (index > pager.RecordCount)
            ////{
            ////    index = 0;
            ////    pager.CurrentPageIndex = 1;
            ////}

            ////query.SetFirstResult(index);
            ////query.SetMaxResults(pager.PageSize);

            //IList<NQ_OASupplier> tempData = query.List<NQ_OASupplier>();
            ////tempData.AsQueryable();
            //IList<NQ_OASupplierView> tempDataView = new List<NQ_OASupplierView>();

            //tempDataView = (tempData as List<NQ_OASupplier>).ConvertAll(x => GetView(x));

            //if (suStatus != null)
            //{
            //    tempDataView = (tempData as List<NQ_OASupplier>).ConvertAll(x => GetView(x))
            //                .Where(w => w.supplierStatus.iStatus == int.Parse(suStatus))
            //                .ToList();
            //}

            ////计算总记录数       
            //pager.RecordCount = Convert.ToInt32(tempDataView.Count());

            ////计算起始的行
            //int index = (pager.CurrentPageIndex - 1) * pager.PageSize;
            //if (index > pager.RecordCount)
            //{
            //    index = 0;
            //    pager.CurrentPageIndex = 1;
            //}

            //pager.DataList = tempDataView
            //                .Skip(pager.PageSize * (pager.CurrentPageIndex - 1))
            //                .Take(pager.PageSize)
            //                .ToList();

            ////if (!string.IsNullOrEmpty(suStatus) && suStatus != "-1")
            ////{
            ////    pager.DataList = pager.DataList.Where(w => w.supplierStatus.iStatus == int.Parse(suStatus)).ToList();
            ////}

            //pager.GetPagingDataJson = JsonConvert.SerializeObject(pager.DataList);
            //return pager;
            return null;
        }

        #region //根据供应商代码查找供应商信息
        public NQ_OASupplierView GetSupplierByFNumber(string fnumber)
        {
            List<NQ_OASupplier> list = HibernateTemplate.Find<NQ_OASupplier>("from NQ_OASupplier where fnumber='" + fnumber + "'") as List<NQ_OASupplier>;
            IList<NQ_OASupplierView> listmodel = GetViewlist(list);
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

        public NQ_OASupplierView GetSupplierById(string Id)
        {
            List<NQ_OASupplier> list = HibernateTemplate.Find<NQ_OASupplier>("from NQ_OASupplier where Id='" + Id + "'") as List<NQ_OASupplier>;
            IList<NQ_OASupplierView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }

        

        public NQ_OASupplier GetSupplierByIdNoView(string Id)
        {
            List<NQ_OASupplier> list = HibernateTemplate.Find<NQ_OASupplier>("from NQ_OASupplier where Id='" + Id + "'") as List<NQ_OASupplier>;
            if (list != null)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public NQ_OASupplier GetSuById(string Id)
        {
            List<NQ_OASupplier> list = HibernateTemplate.Find<NQ_OASupplier>("from NQ_OASupplier where Id='" + Id + "'") as List<NQ_OASupplier>;

            if (list != null)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public IList<NQ_OASupplierView> setItemCount(IList<NQ_OASupplierView> suppliers)
        {
            INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;

            foreach (var mo in suppliers)
            {
                mo.itemsCount = _INQ_YJinfoDao.GetItemsWithSupplierNoView(mo.Id.ToString()).Count();
            }
            return suppliers;
        }


        public IList<NQ_OASupplierView> setListStatus(IList<NQ_OASupplierView> suppliers)
        {
            foreach (var mo in suppliers)
            {
                mo.supplierStatus = setSuStatus(mo);
            }
            return suppliers;
        }

        public suStatus setSuStatus(NQ_OASupplierView oasu)
        {
            suStatus sturctStatus = new suStatus();

            switch (oasu.FIsChecked)
            {
                case 0:
                    sturctStatus.iStatus = oasu.FIsChecked;
                    sturctStatus.strStatusName = "待审核";
                    sturctStatus.strStatusColor = "orange";
                    break;

                case 2:
                    sturctStatus.iStatus = oasu.FIsChecked;
                    sturctStatus.strStatusName = "未通过";
                    sturctStatus.strStatusColor = "gray";
                    break;

                case 1:
                    sturctStatus.iStatus = oasu.FIsChecked;
                    sturctStatus.strStatusName = "通过审核";
                    sturctStatus.strStatusColor = "gray";

                    sturctStatus = suAttStatus(oasu);
                    break;
            }

            return sturctStatus;
        }
        private suStatus suAttStatus(NQ_OASupplierView oasu)
        {
            IList<NQ_SupplierAttachmentView> attlist = (ContextRegistry.GetContext().GetObject("NQ_SupplierAttachmentDao") as INQ_SupplierAttachmentDao).GetAttachmentBySupplierId(oasu.Id.ToString());
            suStatus sturctStatus = new suStatus();

            sturctStatus.strStatusName = "";
            sturctStatus.iStatus = -1;
            suStatus temp = new suStatus();
            sturctStatus.strStatusColor = "blue";

            if (attlist != null)
            {
                foreach (var attach in attlist)
                {
                    temp = fileDateStatus(attach);
                    switch (temp.iStatus)
                    {
                        case 5:
                            sturctStatus = temp;
                            return sturctStatus;
                        case 4:
                            if (temp.iStatus > sturctStatus.iStatus)
                            {
                                sturctStatus = temp;
                                //temp.iStatus = sturctStatus.iStatus;
                            }
                            break;
                        case 3:
                            if (temp.iStatus > sturctStatus.iStatus)
                            {
                                sturctStatus = temp;
                                sturctStatus.strStatusColor = "blue";
                                //temp.iStatus = sturctStatus.iStatus;
                            }
                            break;
                    }
                }
            }
            return sturctStatus;
        }

        public suStatus fileDateStatus(NQ_SupplierAttachmentView att)
        {
            suStatus sturctStatus = new suStatus();

            sturctStatus.strStatusName = "";
            int tsDiff = 0;
            if (att.isPermanent == 1)
            {
                sturctStatus.strStatusName = "永不过期";
                sturctStatus.iStatus = 3;
                sturctStatus.strStatusColor = "green";
            }
            else
            {
                if (att == null)
                {
                    sturctStatus.strStatusName = "未设置";
                    sturctStatus.iStatus = 0;
                }
                else
                {
                    tsDiff = att.FAttDeadline.Subtract(DateTime.Now.Date).Days;
                }
                if (tsDiff > 30)
                {
                    sturctStatus.strStatusName = "正常";
                    sturctStatus.iStatus = 3;
                    sturctStatus.strStatusColor = "green";
                }
                else if (tsDiff <= 30 && tsDiff >= 0)
                {
                    sturctStatus.strStatusName = "即将过期";
                    sturctStatus.iStatus = 4;
                    sturctStatus.strStatusColor = "red";
                }
                else
                {
                    sturctStatus.strStatusName = "已经过期";
                    sturctStatus.iStatus = 5;
                    sturctStatus.strStatusColor = "red";
                }
            }
            return sturctStatus;
        }

        public bool saveOrUpdateSupplier(string supplierid, string itemid)
        {
            try
            {
                NQ_OASupplier sumodel = GetSuById(supplierid);
                INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;

                //IList<NQ_YJinfo> hadlist = _INQ_YJinfoDao.GetItemsWithSupplierNoView(supplierid) as IList<NQ_YJinfo>;

                NQ_YJinfo itemmodel = _INQ_YJinfoDao.GetItemOnlyById(itemid); ;

                //sumodel.baseitems = new List<NQ_YJinfo>();
                //sumodel.baseitems = hadlist;
                sumodel.baseitems.Add(itemmodel);

                if (base.Update(sumodel))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }

        }

        public bool deleteItemSupplier(string supplierid, string itemid)
        {
            try
            {
                NQ_OASupplier sumodel = GetSuById(supplierid);
                INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
                IList<NQ_YJinfo> hadlist = _INQ_YJinfoDao.GetItemsWithSupplierNoView(supplierid) as IList<NQ_YJinfo>;

                sumodel.baseitems = new List<NQ_YJinfo>();

                sumodel.baseitems = hadlist;

                for (int i = 0; i < sumodel.baseitems.Count; i++)
                {
                    if (sumodel.baseitems[i].Id == itemid)
                    {
                        sumodel.baseitems.RemoveAt(i);
                        break;
                    }
                }

                if (base.Update(sumodel))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }
        }

    }
}
