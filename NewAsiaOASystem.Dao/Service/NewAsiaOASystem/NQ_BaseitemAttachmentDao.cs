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
    public class NQ_BaseitemAttachmentDao : ServiceConversion<NQ_BaseitemAttachmentView, NQ_BaseitemAttachment>, INQ_BaseitemAttachmentDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_BaseitemAttachment).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_BaseitemAttachmentView GetView(NQ_BaseitemAttachment data)
        {
            NQ_BaseitemAttachmentView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_BaseitemAttachmentView model)
        {
            NQ_BaseitemAttachment listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_BaseitemAttachmentView model)
        {
            NQ_BaseitemAttachment listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_BaseitemAttachmentView model)
        {
            //NQ_Baseitem listmodel = GetData(model);
            //return base.Delete(listmodel);
            return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_BaseitemAttachmentView> model)
        {
            //IList<NQ_Baseitem> listmodel = GetDatalist(model);
            //return base.NDelete(listmodel);
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_BaseitemAttachmentView> NGetList()
        {
            List<NQ_BaseitemAttachment> listdata = base.GetList() as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_BaseitemAttachmentView> NGetList_id(string id)
        {
            List<NQ_BaseitemAttachment> list = base.GetList_id(id) as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_BaseitemAttachment> NGetListID(string id)
        {
            IList<NQ_BaseitemAttachment> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_BaseitemAttachmentView NGetModelById(string id)
        {
            NQ_BaseitemAttachmentView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_BaseitemAttachmentView NGetModelByFitemid(string FItemid)
        {
            //NQ_BaseitemAttachment listmodel = GetView(base.GetModelById(FItemid));
            return null;
        }


        public PagerInfo<NQ_BaseitemAttachmentView> GetCinfoList(string suName, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(suName))
            {
                TempHql.AppendFormat(" and u.FName like '%{0}%' ", suName);
            }
            TempHql.AppendFormat(" and u.FLevel = 2  ");
            TempHql.AppendFormat(" order by u.FNumber asc,u.FItemID desc");
            PagerInfo<NQ_BaseitemAttachmentView> list = Search();
            return list;
        }


        #region //查询全部的客户信息
        public IList<NQ_BaseitemAttachmentView> GetlistCust()
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_Baseitem order by FItemid asc ") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据供应商代码查找供应商代码
        public string GetBaseitemById(string dm)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_Baseitem where FName='" + dm + "'") as List<NQ_BaseitemAttachment>;
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

        #region //根据供应商代码查，附件类型,元器件,查找对应附件.
        public NQ_BaseitemAttachmentView GetAttachByBaseitemAndTypeAndSupplier(string Baseitemid, string attType, string supplierid)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_BaseitemAttachment where FItemid ='" + Baseitemid + "' and FAttType ='" + attType + "'and FSupplierid ='" + supplierid + "'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
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

        #region //根据供应商代码查,元器件,查找对应附件.
        public IList<NQ_BaseitemAttachmentView> GetAttachByBaseitemAndSupplier(string itemid, string supplierid)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_BaseitemAttachment where FItemid ='" + itemid + "'and FSupplierid ='" + supplierid + "'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel;
            }
            else
            {
                return null;
            }
        }
        #endregion



        #region //根据供应商代码查找供应商信息
        public IList<NQ_BaseitemAttachmentView> GetAttachmentByBaseitemId(string FBaseitemID)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_BaseitemAttachment where FItemid='" + FBaseitemID + "'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            return listmodel;

        }
        #endregion

        #region //根据供应商名称查找供应商信息
        public NQ_BaseitemAttachmentView GetAttachmentByName(string name)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_Baseitem where FName like '%" + name + "%'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            return listmodel[0];
        }
        #endregion

        public bool saveOrUpdate(NQ_BaseitemAttachmentView att)
        {
            ISession session = GetSession();

            HibernateTemplate.SaveOrUpdate(att);
            return true;
        }

        public IList<NQ_BaseitemAttachmentView> getRelBySupplierid(string Supplierid)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_BaseitemAttachment where supplierid='" + Supplierid + "'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        public IList<NQ_BaseitemAttachmentView> getRelByItemid(string FItemID)
        {
            List<NQ_BaseitemAttachment> list = HibernateTemplate.Find<NQ_BaseitemAttachment>("from NQ_BaseitemAttachment where itemid='" + FItemID + "'") as List<NQ_BaseitemAttachment>;
            IList<NQ_BaseitemAttachmentView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel;
            }
            else
            {
                return null;
            }
        }

        public suStatus setSuStatus(NQ_SupplierAndBaseitemView oasu)
        {
            suStatus sturctStatus = new suStatus();

            //switch (oasu.ischecked)
            //{
            //    case 0:
            //        sturctStatus.iStatus = oasu.ischecked;
            //        sturctStatus.strStatusName = "待审核";
            //        sturctStatus.strStatusColor = "orange";
            //        break;

            //    case 2:
            //        sturctStatus.iStatus = oasu.ischecked;
            //        sturctStatus.strStatusName = "未通过";
            //        sturctStatus.strStatusColor = "gray";
            //        break;

            //    case 1:
            //        sturctStatus.iStatus = oasu.ischecked;
            //        sturctStatus.strStatusName = "通过审核";
            //        sturctStatus.strStatusColor = "gray";
            //        sturctStatus = suAttStatus(oasu);
            //        break;
            //}

            return sturctStatus;
        }
        private suStatus suAttStatus(NQ_SupplierAndBaseitemView oasu)
        {
            IList<NQ_BaseitemAttachmentView> attlist = (ContextRegistry.GetContext().GetObject("NQ_BaseitemAttachmentDao") as INQ_BaseitemAttachmentDao).GetAttachByBaseitemAndSupplier(oasu.itemid.ToString(), oasu.supplierid.ToString());

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
                            }
                            break;
                        case 3:
                            if (temp.iStatus > sturctStatus.iStatus)
                            {
                                sturctStatus = temp;
                                sturctStatus.strStatusColor = "blue";
                            }
                            break;
                    }
                }
            }
            return sturctStatus;
        }

        public suStatus fileDateStatus(NQ_BaseitemAttachmentView att)
        {
            suStatus sturctStatus = new suStatus();

            sturctStatus.strStatusName = "";
            int tsDiff = 0;
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
            return sturctStatus;
        }
    }


}
