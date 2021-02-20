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
    public  class WL_GcsinfoDao:ServiceConversion<WL_GcsinfoView,WL_Gcsinfo>,IWL_GcsinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_Gcsinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_GcsinfoView GetView(WL_Gcsinfo data)
        {
            WL_GcsinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_GcsinfoView model)
        {
            WL_Gcsinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_GcsinfoView model)
        {
            WL_Gcsinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_GcsinfoView model)
        {
            WL_Gcsinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_GcsinfoView> model)
        {
            IList<WL_Gcsinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_GcsinfoView> NGetList()
        {
            List<WL_Gcsinfo> listdata = base.GetList() as List<WL_Gcsinfo>;
            IList<WL_GcsinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_GcsinfoView> NGetList_id(string id)
        {
            List<WL_Gcsinfo> list = base.GetList_id(id) as List<WL_Gcsinfo>;
            IList<WL_GcsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_Gcsinfo> NGetListID(string id)
        {
            IList<WL_Gcsinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_GcsinfoView NGetModelById(string id)
        {
            WL_GcsinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //  分页列表数据 +GetQyinfoList（）
        /// <summary>
        /// 分页列表数据
        /// </summary>
        /// <param name="Name">区域名称</param>
        /// <param name="user">当前登录用户</param>
        /// <returns></returns>
        public PagerInfo<WL_GcsinfoView> GetGcsinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.DW_name like '%{0}%'", Name);
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by Sort desc");
            PagerInfo<WL_GcsinfoView> list = Search();
            return list;
        }
        #endregion


        //检查是否已经有该工程商
        public bool JccfbyGcs_no(string Gcs_no)
        {
            List<WL_Gcsinfo> list = HibernateTemplate.Find<WL_Gcsinfo>("from WL_Gcsinfo where Gcs_no = '" + Gcs_no + "' ") as List<WL_Gcsinfo>;
            if (list != null && list.Count != 0)
            {
                return false;//存在 重复的 返回false
            }
            else
            {
                return true;//不存在重复的 返回true
            }
        }

        //public IList<NACustomerinfoView> GetlistCust()
        //{
        //    List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo order by Sort asc ") as List<NACustomerinfo>;
        //    IList<NACustomerinfoView> listmodel = GetViewlist(list);
        //    return listmodel;
        //}

        #region //根据Gcs_no 查找工程商信息
        public WL_GcsinfoView GetGcsinfo(string Gcs_no)
        {
             List<WL_Gcsinfo> list = HibernateTemplate.Find<WL_Gcsinfo>("from WL_Gcsinfo where Gcs_no = '" + Gcs_no + "' ") as List<WL_Gcsinfo>;
             IList<WL_GcsinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            return null;
        }
        #endregion
    }
}
