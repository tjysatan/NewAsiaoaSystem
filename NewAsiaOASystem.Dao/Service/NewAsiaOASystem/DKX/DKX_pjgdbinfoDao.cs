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
    public class DKX_pjgdbinfoDao:ServiceConversion<DKX_pjgdbinfoView,DKX_pjgdbinfo>,IDKX_pjgdbinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_pjgdbinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_pjgdbinfoView GetView(DKX_pjgdbinfo data)
        {
            DKX_pjgdbinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_pjgdbinfoView model)
        {
            DKX_pjgdbinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_pjgdbinfoView model)
        {
            DKX_pjgdbinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_pjgdbinfoView model)
        {
            DKX_pjgdbinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_pjgdbinfoView> model)
        {
            IList<DKX_pjgdbinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_pjgdbinfoView> NGetList()
        {
            List<DKX_pjgdbinfo> listdata = base.GetList() as List<DKX_pjgdbinfo>;
            IList<DKX_pjgdbinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_pjgdbinfoView> NGetList_id(string id)
        {
            List<DKX_pjgdbinfo> list = base.GetList_id(id) as List<DKX_pjgdbinfo>;
            IList<DKX_pjgdbinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_pjgdbinfo> NGetListID(string id)
        {
            IList<DKX_pjgdbinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_pjgdbinfoView NGetModelById(string id)
        {
            DKX_pjgdbinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过物料代码查找底板信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wlno">物料代码</param>
        /// <returns></returns>
        public DKX_pjgdbinfoView Getpjdbinfobywlno(string wlno)
        {
            string Hqlstr = string.Format(" from DKX_pjgdbinfo where dbwlno='{0}'", wlno);
            List<DKX_pjgdbinfo> list = HibernateTemplate.Find<DKX_pjgdbinfo>(Hqlstr) as List<DKX_pjgdbinfo>;
            IList<DKX_pjgdbinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //分页数据
        /// <summary>
        /// 拼接柜底板数据
        /// </summary>
        /// <param name="dbname">型号名称</param>
        /// <param name="dbwlno">物料代码</param>
        /// <returns></returns>
        public PagerInfo<DKX_pjgdbinfoView> GetDKXpjgdbinfopagelist(string dbname,string dbwlno)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(dbname))
                TempHql.AppendFormat(" and u.dbname like '%{0}%'",dbname);
            if (!string.IsNullOrEmpty(dbwlno))
                TempHql.AppendFormat(" and u.dbwlno like'%{0}%'", dbwlno);
            TempHql.AppendFormat("order by u.c_time desc");
            PagerInfo<DKX_pjgdbinfoView> list = Search();
            return list;
        }
        #endregion
    }
}
