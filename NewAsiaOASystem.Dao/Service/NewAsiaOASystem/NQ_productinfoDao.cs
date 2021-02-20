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
    public class NQ_productinfoDao : ServiceConversion<NQ_productinfoView, NQ_productinfo>,INQ_productinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_productinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_productinfoView GetView(NQ_productinfo data)
        {
            NQ_productinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_productinfoView model)
        {
            NQ_productinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_productinfoView model)
        {
            NQ_productinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_productinfoView model)
        {
            NQ_productinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_productinfoView> model)
        {
            IList<NQ_productinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_productinfoView> NGetList()
        {
            List<NQ_productinfo> listdata = base.GetList() as List<NQ_productinfo>;
            IList<NQ_productinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_productinfoView> NGetList_id(string id)
        {
            List<NQ_productinfo> list = base.GetList_id(id) as List<NQ_productinfo>;
            IList<NQ_productinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_productinfo> NGetListID(string id)
        {
            IList<NQ_productinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_productinfoView NGetModelById(string id)
        {
            NQ_productinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public PagerInfo<NQ_productinfoView> GetCinfoList(string Name,string P_xh,string bianhao, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat("and Pname like '%{0}%' ", Name);
            if (!string.IsNullOrEmpty(P_xh))
                TempHql.AppendFormat("and P_xh like '%{0}%'",P_xh);
            if (!string.IsNullOrEmpty(bianhao))
                TempHql.AppendFormat("and P_bianhao like '%{0}%'",bianhao);
            TempHql.AppendFormat("order by CreateTime desc");
            PagerInfo<NQ_productinfoView> list = Search();
            return list;

        }

        //#region //根据客户公司名称和收货人查找客户信息
        //public NACustomerinfoView GetKHinfobykhname(string khname, string lxrname)
        //{
        //    List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where Name = '" + khname + "' and LxrName='" + lxrname + "' ") as List<NACustomerinfo>;
        //    IList<NACustomerinfoView> listmodel = GetViewlist(list);
        //    if (listmodel != null && listmodel.Count != 0)
        //    {
        //        return listmodel[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        #region //根据产品编号查找产品信息
        public NQ_productinfoView GetProinfobyname(string sku)
        {
            List<NQ_productinfo> list = HibernateTemplate.Find<NQ_productinfo>("from NQ_productinfo where P_bianhao = '" + sku + "'") as List<NQ_productinfo>;
            IList<NQ_productinfoView> listmodel = GetViewlist(list);
            if (listmodel != null && listmodel.Count != 0)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        } 
        #endregion

        #region //根据产品 检测该订单是否需要扫描
        public bool JcProisSm(string sku)
        {
            List<NQ_productinfo> list = HibernateTemplate.Find<NQ_productinfo>("from NQ_productinfo where P_bianhao = '" + sku + "' and SMFH='1'") as List<NQ_productinfo>;
            if (list != null&&list.Count!=0)
            {
                return true;//需要扫描
            }
            else
            {
                return false;//不需要扫描
            }
        } 
        #endregion

        #region //保存后返回ID
        public string InsertID(NQ_productinfoView modelView)
        {
            NQ_productinfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //退货维修分析
        //按照产品分组查询
        public List<Object> GetTHwxfxgroupcpId(string starttime, string enedtime)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and u.wx_time>='{0}' and wx_time<='{1}' ", starttime + " 00:00:00", enedtime + " 23:59:59");
            TempHql.AppendFormat(" group by P_Id");
            string HQLstr = string.Format("select Pname  from NQ_productinfo where Id in (select P_Id from NQ_THinfoFX where 1=1 {0})", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

        #region //查询状态为空的数据
        /// <summary>
        /// 查询状态为空的数据
        /// </summary>
        /// <returns></returns>
        public IList<NQ_productinfoView> Getproductbystaitisnull()
        {
            string Hqlstr = string.Format(" from  NQ_productinfo where stait is null");
            List<NQ_productinfo> list = HibernateTemplate.Find<NQ_productinfo>(Hqlstr) as List<NQ_productinfo>;
            IList<NQ_productinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据物料号查询产品数据
        /// <summary>
        /// 根据物料号查询产品数据
        /// </summary>
        /// <param name="fnumber">物料号</param>
        /// <returns></returns>
        public NQ_productinfoView Getproductbyfnumber(string fnumber)
        {
            string Hqlstr = string.Format(" from NQ_productinfo where P_bianhao='{0}'",fnumber);
            List<NQ_productinfo> list = HibernateTemplate.Find<NQ_productinfo>(Hqlstr) as List<NQ_productinfo>;
            IList<NQ_productinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
