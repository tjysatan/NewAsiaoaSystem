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
    public class NQ_YJinfoDao:ServiceConversion<NQ_YJinfoView,NQ_YJinfo>,INQ_YJinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_YJinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_YJinfoView GetView(NQ_YJinfo data)
        {
            NQ_YJinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_YJinfoView model)
        {
            NQ_YJinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_YJinfoView model)
        {
            NQ_YJinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_YJinfoView model)
        {
            NQ_YJinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_YJinfoView> model)
        {
            IList<NQ_YJinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_YJinfoView> NGetList()
        {
            List<NQ_YJinfo> listdata = base.GetList() as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_YJinfoView> NGetList_id(string id)
        {
            List<NQ_YJinfo> list = base.GetList_id(id) as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_YJinfo> NGetListID(string id)
        {
            IList<NQ_YJinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_YJinfoView NGetModelById(string id)
        {
            NQ_YJinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        public PagerInfo<NQ_YJinfoView> GetCinfoList(string Name, string Yname,string isbj, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Y_Dm like '%{0}%' ", Name);
            if (!string.IsNullOrEmpty(Yname))
                TempHql.AppendFormat(" and u.Y_Name like '%{0}%' ", Yname);
            if (!string.IsNullOrEmpty(isbj))
                TempHql.AppendFormat("and u.Y_iscg ='{0}'", isbj);
            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<NQ_YJinfoView> list = Search();
            return list;

        }

        #region //查询全部的客户信息
        public IList<NQ_YJinfoView> GetlistCust()
        {
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo order by Sort asc ") as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据元器件名称查询元器件信息
        public IList<NQ_YJinfoView> Getlistbyname(string name)
        {
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo where Y_Name like '%" + name + "%'") as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据 元器件代码查找元器件信息
        public NQ_YJinfoView GetYqjModelbyW_dm(string W_dm)
        {
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo where Y_Dm = '" + W_dm + "'") as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            return null;
        }
        #endregion

        #region //根据 元器件代码查找元器件信息
        public NQ_YJinfoView GetItemById(string id)
        {
                List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo where Id='" + id + "'") as List<NQ_YJinfo>;
                IList<NQ_YJinfoView> listmodel = GetViewlist(list);
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

        public NQ_YJinfo GetItemOnlyById(string id)
        {
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo where Id='" + id + "'") as List<NQ_YJinfo>;
            if (list != null)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        #region //根据供应商代码查找 元器件
        public IList<NQ_YJinfoView> Getlistbygysdm(string gysdm)
        {
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>("from NQ_YJinfo where G_Dm = '" + gysdm + "'") as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion


        //通过元器件的名称或者型号或者物料代码查询元器件
        #region //通过元器件的名称或者型号或者物料代码查询元器件
        /// <summary>
        /// 通过元器件的名称或者型号或者物料代码查询元器件
        /// </summary>
        /// <param name="paraDat">参数（名称or型号or代码）</param>
        /// <returns></returns>
        public IList<NQ_YJinfoView> GetyjlistbyNameorxhordm(string paraDat)
        {
            string hqlstr = string.Format(" from NQ_YJinfo where Y_Name like '%{0}%' or Y_Ggxh like '%{0}%' or Y_Dm='{0}'", paraDat);
            List<NQ_YJinfo> list = HibernateTemplate.Find<NQ_YJinfo>(hqlstr) as List<NQ_YJinfo>;
            IList<NQ_YJinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        public IList<NQ_YJinfoView> GetItemsWithSupplier(string supplierid)
        {
            ISession session = GetSession();
            string sql = "";
            sql += "select	{yj.*} from	NQ_YJinfo {yj} ";
            sql += "inner	join	NQ_SupplierAndBaseitem sb on sb.itemid = {yj}.Id ";
            sql += "where	sb.supplierid= :supplierid";

            IQuery query = session.CreateSQLQuery(sql)
                .AddEntity("yj",typeof(NQ_YJinfo))
                .SetString("supplierid",supplierid);

            return (query.List<NQ_YJinfo>() as List<NQ_YJinfo>).ConvertAll(x => GetView(x));

        }

        public IList<NQ_YJinfo> GetItemsWithSupplierNoView(string supplierid)
        {
            ISession session = GetSession();
            string sql = "";
            sql += "select	{yj.*} from	NQ_YJinfo {yj} ";
            sql += "inner	join	NQ_SupplierAndBaseitem sb on sb.itemid = {yj}.Id ";
            sql += "where	sb.supplierid= :supplierid";

            IQuery query = session.CreateSQLQuery(sql)
                .AddEntity("yj", typeof(NQ_YJinfo))
                .SetString("supplierid", supplierid);

            return (query.List<NQ_YJinfo>() as List<NQ_YJinfo>);

        }
        public IList<NQ_YJinfoView> GetItemsNotWithSupplier(string supplierid)
        {
            ISession session = GetSession();
            string sql = "";
            sql += "select	{yj.*} from	NQ_YJinfo {yj} ";
            sql += "left	join	NQ_SupplierAndBaseitem sb on sb.itemid = {yj}.Id and sb.supplierid= :supplierid ";
            sql += "where	sb.itemid is null";

            IQuery query = session.CreateSQLQuery(sql)
                .AddEntity("yj", typeof(NQ_YJinfo))
                .SetString("supplierid", supplierid);

            return (query.List<NQ_YJinfo>() as List<NQ_YJinfo>).ConvertAll(x => GetView(x));

        }

        

        public PagerInfo<NQ_YJinfoView> setPagerInfo(IList<NQ_YJinfoView> list, int pageIndex, int pageSize)
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


    }
}
