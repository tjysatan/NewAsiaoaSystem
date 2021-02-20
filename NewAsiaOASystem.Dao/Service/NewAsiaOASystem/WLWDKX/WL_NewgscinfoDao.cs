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
    public class WL_NewgscinfoDao:ServiceConversion<WL_NewgscinfoView,WL_Newgscinfo>,IWL_NewgscinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_Newgscinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_NewgscinfoView GetView(WL_Newgscinfo data)
        {
            WL_NewgscinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_NewgscinfoView model)
        {
            WL_Newgscinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_NewgscinfoView model)
        {
            WL_Newgscinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_NewgscinfoView model)
        {
            WL_Newgscinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_NewgscinfoView> model)
        {
            IList<WL_Newgscinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_NewgscinfoView> NGetList()
        {
            List<WL_Newgscinfo> listdata = base.GetList() as List<WL_Newgscinfo>;
            IList<WL_NewgscinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_NewgscinfoView> NGetList_id(string id)
        {
            List<WL_Newgscinfo> list = base.GetList_id(id) as List<WL_Newgscinfo>;
            IList<WL_NewgscinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_Newgscinfo> NGetListID(string id)
        {
            IList<WL_Newgscinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_NewgscinfoView NGetModelById(string id)
        {
            WL_NewgscinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询最新更新的工程商对应的sid 的信息
        /// <summary>
        /// 查询最新更新的工程商对应的sid 的信息
        /// </summary>
        /// <returns></returns>
        public WL_NewgscinfoView GetNewnewgcsinfo()
        {
            string Hqlstr = " from WL_Newgscinfo order by Ids desc";
            List<WL_Newgscinfo> list = Session.CreateQuery(Hqlstr).SetFirstResult(0).SetMaxResults(1).List<WL_Newgscinfo>() as List<WL_Newgscinfo>;
            IList<WL_NewgscinfoView> listmodel = GetViewlist(list);
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

        #region //根据远程Ids查询工程商对应sid 的信息
        /// <summary>
        /// 根据远程Ids查询工程商对应sid 的信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public WL_NewgscinfoView GetnewgcsinfobyIds(string Ids)
        {
            string Hqlstr = string.Format(" from WL_Newgscinfo where Ids='{0}'", Ids);
            List<WL_Newgscinfo> list = Session.CreateQuery(Hqlstr).List<WL_Newgscinfo>() as List<WL_Newgscinfo>;
            IList<WL_NewgscinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据SID查找工程商信息
        /// <summary>
        /// 根据SID查找工程商信息
        /// </summary>
        /// <param name="sid">SID</param>
        /// <returns></returns>
        public WL_NewgscinfoView Getnewgscinfobysid(string sid)
        {
            string Hqlstr = string.Format(" from WL_Newgscinfo where sid='{0}'", sid);
            List<WL_Newgscinfo> list = Session.CreateQuery(Hqlstr).List<WL_Newgscinfo>() as List<WL_Newgscinfo>;
            IList<WL_NewgscinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
