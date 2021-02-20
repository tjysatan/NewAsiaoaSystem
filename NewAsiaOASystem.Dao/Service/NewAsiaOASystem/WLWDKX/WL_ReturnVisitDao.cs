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
    /// <summary>
    /// NAME:回访记录方法
    /// </summary>
    public class WL_ReturnVisitDao:ServiceConversion<_20150510WL_ReturnVisitView,_20150510WL_ReturnVisit>,IWL_ReturnVisitDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(_20150510WL_ReturnVisit).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override _20150510WL_ReturnVisitView GetView(_20150510WL_ReturnVisit data)
        {
            _20150510WL_ReturnVisitView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(_20150510WL_ReturnVisitView model)
        {
            _20150510WL_ReturnVisit listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(_20150510WL_ReturnVisitView model)
        {
            _20150510WL_ReturnVisit listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(_20150510WL_ReturnVisitView model)
        {
            _20150510WL_ReturnVisit listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<_20150510WL_ReturnVisitView> model)
        {
            IList<_20150510WL_ReturnVisit> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<_20150510WL_ReturnVisitView> NGetList()
        {
            List<_20150510WL_ReturnVisit> listdata = base.GetList() as List<_20150510WL_ReturnVisit>;
            IList<_20150510WL_ReturnVisitView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<_20150510WL_ReturnVisitView> NGetList_id(string id)
        {
            List<_20150510WL_ReturnVisit> list = base.GetList_id(id) as List<_20150510WL_ReturnVisit>;
            IList<_20150510WL_ReturnVisitView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<_20150510WL_ReturnVisit> NGetListID(string id)
        {
            IList<_20150510WL_ReturnVisit> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public _20150510WL_ReturnVisitView NGetModelById(string id)
        {
            _20150510WL_ReturnVisitView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //
        /// <summary>
        ///  根据电控箱的ID 和回访对象类型查询回访记录
        /// </summary>
        /// <param name="Sid">电控箱信息的ID</param>
        /// <param name="Rvtype">回访对象 0 工程商 1电箱用户</param>
        /// <returns></returns>
        public _20150510WL_ReturnVisitView GetModelbySidrvtype(string Sid, string Rvtype)
        {
            string TempHql = string.Format("from _20150510WL_ReturnVisit where DKXId='{0}' and RVtype='{1}'",Sid,Rvtype);
            List<_20150510WL_ReturnVisit> list = HibernateTemplate.Find<_20150510WL_ReturnVisit>(TempHql) as List<_20150510WL_ReturnVisit>;
            if(list!=null&&list.Count!=0){
                _20150510WL_ReturnVisitView listmodel = GetView(list[0]);
                 return listmodel;
            }else
            {
                return null;
            }
        } 
        #endregion

        //public PagerInfo<EP_jlinfoView> GetCinfoList(string Name, string RL_is, string Stratrldate, string Endrldate, string dyStratrldate, string dyEndrldate, SessionUser user)
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(Name))
        //        TempHql.AppendFormat(" and u.SjId in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
        //    if (!string.IsNullOrEmpty(RL_is))
        //        TempHql.AppendFormat(" and u.RL_Is like '%{0}%' ", RL_is);
        //    if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
        //        TempHql.AppendFormat("and RL_datetime>='{0}' and RL_datetime<='{1}'", Stratrldate + " 00:00:00", Endrldate + " 23:59:59");
        //    if (!string.IsNullOrEmpty(dyStratrldate) && !string.IsNullOrEmpty(dyEndrldate))
        //        TempHql.AppendFormat("and Jjdatetime>='{0}' and Jjdatetime<='{1}'", dyStratrldate + " 00:00:00", dyEndrldate + " 23:59:59");
        //    if (user.RoleType != "0")
        //    {
        //        TempHql.AppendFormat("and u.JjId  in ('{0}')", user.Id);
        //    }
        //    TempHql.AppendFormat(" order by Jjdatetime desc");
        //    PagerInfo<EP_jlinfoView> list = Search();
        //    return list;
        //}
    }
}
