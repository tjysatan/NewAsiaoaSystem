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
    public class K3_wuliaoinfoDao:ServiceConversion<K3_wuliaoinfoView,K3_wuliaoinfo>,IK3_wuliaoinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(K3_wuliaoinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override K3_wuliaoinfoView GetView(K3_wuliaoinfo data)
        {
            K3_wuliaoinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(K3_wuliaoinfoView model)
        {
            K3_wuliaoinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(K3_wuliaoinfoView model)
        {
            K3_wuliaoinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(K3_wuliaoinfoView model)
        {
            K3_wuliaoinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<K3_wuliaoinfoView> model)
        {
            IList<K3_wuliaoinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<K3_wuliaoinfoView> NGetList()
        {
            List<K3_wuliaoinfo> listdata = base.GetList() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<K3_wuliaoinfoView> NGetList_id(string id)
        {
            List<K3_wuliaoinfo> list = base.GetList_id(id) as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<K3_wuliaoinfo> NGetListID(string id)
        {
            IList<K3_wuliaoinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public K3_wuliaoinfoView NGetModelById(string id)
        {
            K3_wuliaoinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //查询自增号最大的数据
        public K3_wuliaoinfoView GetwuliaoMaxfitem()
        {
            string hqlstr = string.Format("from K3_wuliaoinfo where fitem in(select max(fitem) from K3_wuliaoinfo)");
            List<K3_wuliaoinfo> list = Session.CreateQuery(hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //查询普实创建时间字段最近的数据
        public K3_wuliaoinfoView GetwuliaoMaxOpDate()
        {
            string hqlstr = string.Format("from K3_wuliaoinfo where OpDate = (SELECT MAX(OpDate) FROM K3_wuliaoinfo)");
            List<K3_wuliaoinfo> list = Session.CreateQuery(hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //通过物料代码查询出物料代码
        /// <summary>
        /// 通过物料代码查询出物料代码
        /// </summary>
        /// <param name="fnumber">物料代码</param>
        /// <returns></returns>
        public K3_wuliaoinfoView Getwuliaobyfnumber(string fnumber)
        {
            string Hqlstr = string.Format(" from K3_wuliaoinfo where fnumber='{0}'",fnumber);
            List<K3_wuliaoinfo> list = Session.CreateQuery(Hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据物料的类型查询物料数据
        /// <summary>
        /// 根据物料的类型查询物料数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IList<K3_wuliaoinfoView> Getwuliaobytype(string type)
        {
            string Hqlstr = string.Format(" from K3_wuliaoinfo where Type in({0})",type);
            List<K3_wuliaoinfo> list = Session.CreateQuery(Hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //k3基本数据的分页数据
        /// <summary>
        /// k3基本数据的分页数据
        /// </summary>
        /// <param name="fnumber">物料代码</param>
        /// <param name="fname">物料名称</param>
        /// <param name="fmodel">物料型号</param>
        /// <param name="Type">类别</param>
        /// <returns></returns>
        public PagerInfo<K3_wuliaoinfoView> GetK3_wuliaoinfoList(string fnumber, string fname, string fmodel, string Type)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(fnumber))
                TempHql.AppendFormat(" and fnumber='{0}'", fnumber);
            if (!string.IsNullOrEmpty(fname))
                TempHql.AppendFormat(" and fname like '%{0}%'", fname);
            if (!string.IsNullOrEmpty(fmodel))
                TempHql.AppendFormat(" and fmodel like '%{0}%'", fmodel);
            if (!string.IsNullOrEmpty(Type))
                TempHql.AppendFormat(" and Type='{0}'", Type);
            TempHql.AppendFormat(" order by fitem desc");
            PagerInfo<K3_wuliaoinfoView> list = Search();
            return list;
        }
        #endregion

        public K3_wuliaoinfoView GetBaseitemByItemId(string Id)
        {
            List<K3_wuliaoinfo> list = HibernateTemplate.Find<K3_wuliaoinfo>("from K3_wuliaoinfo where fitem ='" + Id + "'") as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }

        public IList<K3_wuliaoinfoView> getSearchList(string fname, string fnumber, string fmodel, string fstatus)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(fname))
            {
                TempHql.AppendFormat(" and u.FName like '%{0}%' ", fname);
            }
            if (!string.IsNullOrEmpty(fnumber))
            {
                TempHql.AppendFormat(" and u.FNumber like '%{0}%' ", fnumber);
            }
            if (!string.IsNullOrEmpty(fmodel))
            {
                TempHql.AppendFormat(" and u.fmodel like '%{0}%' ", fmodel);
            }

            TempHql.AppendFormat(" order by u.C_Time desc , u.fnumber asc");

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

            return (query.List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>).ConvertAll(x => GetView(x));
        }

        public suStatus setSuStatus(K3_wuliaoinfoView oasu)
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

        private suStatus suAttStatus(K3_wuliaoinfoView oasu)
        {
            IList<NQ_BaseitemAttachmentView> attlist = (ContextRegistry.GetContext().GetObject("NQ_BaseitemAttachmentDao") as INQ_BaseitemAttachmentDao).GetAttachmentByBaseitemId(oasu.Id.ToString());
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


        #region //根据物料代理的前俩位和中间三位查找物料数据

        /// <summary>
        /// 根据物料代理的前俩位和中间三位查找物料数据
        /// </summary>
        /// <param name="str1">物料前俩位</param>
        /// <param name="str2">物料中间三位</param>
        /// <returns></returns>
        public IList<K3_wuliaoinfoView> Getwuliaobyfnumber23(string str1, string str2)
        {
            string Hqlstr = string.Format(" from K3_wuliaoinfo where str1='{0}' and str2='{1}'",str1,str2);
            List<K3_wuliaoinfo> list = Session.CreateQuery(Hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据物料的来源查询 SourceID 采购/制造
        /// <summary>
        /// 根据物料的来源查询 SourceID 采购/制造
        /// </summary>
        /// <param name="SourceID">物料的来源查询  采购/制造</param>
        /// <returns></returns>
        public IList<K3_wuliaoinfoView> Get_info_bySourceID(string SourceID)
        {
            string Hqlstr = string.Format(" from K3_wuliaoinfo where SourceID='{0}'", SourceID);
            List<K3_wuliaoinfo> list = Session.CreateQuery(Hqlstr).List<K3_wuliaoinfo>() as List<K3_wuliaoinfo>;
            IList<K3_wuliaoinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

    }
}
