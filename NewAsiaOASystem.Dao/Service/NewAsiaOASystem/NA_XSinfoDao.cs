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
    public class NA_XSinfoDao : ServiceConversion<NA_XSinfoView,NA_XSinfo>, INA_XSinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_XSinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_XSinfoView GetView(NA_XSinfo data)
        {
            NA_XSinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_XSinfoView model)
        {
            NA_XSinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_XSinfoView model)
        {
            NA_XSinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_XSinfoView model)
        {
            NA_XSinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_XSinfoView> model)
        {
            IList<NA_XSinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_XSinfoView> NGetList()
        {
            List<NA_XSinfo> listdata = base.GetList() as List<NA_XSinfo>;
            IList<NA_XSinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_XSinfoView> NGetList_id(string id)
        {
            List<NA_XSinfo> list = base.GetList_id(id) as List<NA_XSinfo>;
            IList<NA_XSinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_XSinfo> NGetListID(string id)
        {
            IList<NA_XSinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_XSinfoView NGetModelById(string id)
        {
            if (GetModelById(id) != null)
            {
                NA_XSinfoView listmodel = GetView(base.GetModelById(id));
                return listmodel;
            }
            else
            {
                return null;
            }
        }


        #region // 销售订单 分页列表数据 +GetQyinfoList（）
        /// <summary>
        /// 销售订单 分页列表数据
        /// </summary>
        /// <param name="Name">客户名称</param>
        /// <param name="user">当前登录用户</param>
        /// <returns></returns>
        public PagerInfo<NA_XSinfoView> GetXSinfoList(string Name,string Issm,string sm_zt, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.KhId in(select Id from NACustomerinfo where Name like '%{0}%')", Name);
            if (!string.IsNullOrEmpty(Issm))
                TempHql.AppendFormat(" and u.Issm='{0}'",Issm);
            if (!string.IsNullOrEmpty(sm_zt))
                TempHql.AppendFormat(" and u.SM_ZT='{0}'",sm_zt);
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by Xs_datetime desc");
            PagerInfo<NA_XSinfoView> list = Search();
            return list;
        }
        #endregion


        #region //保存后返回ID
        public string InsertID(NA_XSinfoView modelView)
        {
            NA_XSinfo model = GetData(modelView);
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

        #region //检测订单是否存在重复的
        public bool jccfbySc_Id(string Sc_Id)
        {
            List<NA_XSinfo> list = HibernateTemplate.Find<NA_XSinfo>("from NA_XSinfo where Sc_Id='" + Sc_Id + "'") as List<NA_XSinfo>;
            if (list != null && list.Count != 0)
            {
                return false;//存在重复 返回false
            }
            else
            {
                return true;//不存在重复的 返回ture
            }
        } 
        #endregion

        #region //根据订单编号查找订单数据
        public NA_XSinfoView GetxsinfobyOrderCode(string ordercode)
        {
            List<NA_XSinfo> list = HibernateTemplate.Find<NA_XSinfo>("from NA_XSinfo where Sc_Id='" + ordercode + "'") as List<NA_XSinfo>;
            IList<NA_XSinfoView> listmodel = GetViewlist(list);
            return listmodel[0];
        } 
        #endregion

        #region //查找最新的一条的销售订单
        public NA_XSinfoView GetxsinfoNewdate()
        {
            string tempHql = "from  NA_XSinfo order by Sort desc";
            List<NA_XSinfo> list = Session.CreateQuery(tempHql).SetFirstResult(0).SetMaxResults(1).List<NA_XSinfo>() as List<NA_XSinfo>;
            IList<NA_XSinfoView> listmodel = GetViewlist(list);
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

        #region //根据SORT 电商平台订单ID 查找订单信息
        public NA_XSinfoView GetxsInfobySort(string Sort)
        {
            string tempHql = "from NA_XSinfo where Sort='" + Sort + "'";
            List<NA_XSinfo> list = HibernateTemplate.Find<NA_XSinfo>(tempHql) as List<NA_XSinfo>;
            IList<NA_XSinfoView> listmodel = GetViewlist(list);
            return listmodel[0];
        }
        #endregion
       
    }
}
