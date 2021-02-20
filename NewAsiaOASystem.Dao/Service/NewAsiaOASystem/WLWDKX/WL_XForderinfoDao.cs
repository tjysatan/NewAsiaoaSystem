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
    public class WL_XForderinfoDao : ServiceConversion<WL_XFOrderinfoView, WL_XFOrderinfo>, IWL_XFOrderinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_XFOrderinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_XFOrderinfoView GetView(WL_XFOrderinfo data)
        {
            WL_XFOrderinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_XFOrderinfoView model)
        {
            WL_XFOrderinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_XFOrderinfoView model)
        {
            WL_XFOrderinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_XFOrderinfoView model)
        {
            WL_XFOrderinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_XFOrderinfoView> model)
        {
            IList<WL_XFOrderinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_XFOrderinfoView> NGetList()
        {
            List<WL_XFOrderinfo> listdata = base.GetList() as List<WL_XFOrderinfo>;
            IList<WL_XFOrderinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_XFOrderinfoView> NGetList_id(string id)
        {
            List<WL_XFOrderinfo> list = base.GetList_id(id) as List<WL_XFOrderinfo>;
            IList<WL_XFOrderinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_XFOrderinfo> NGetListID(string id)
        {
            IList<WL_XFOrderinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_XFOrderinfoView NGetModelById(string id)
        {
            WL_XFOrderinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询最新更新的订单号的信息
        /// <summary>
        /// 查询最新更新的订单号的信息
        /// </summary>
        /// <returns></returns>
        public WL_XFOrderinfoView GetNewOderinfo()
        {
            string Hqlstr = " from WL_XFOrderinfo order by Ids desc";
            List<WL_XFOrderinfo> list = Session.CreateQuery(Hqlstr).SetFirstResult(0).SetMaxResults(1).List<WL_XFOrderinfo>() as List<WL_XFOrderinfo>;
            IList<WL_XFOrderinfoView> listmodel = GetViewlist(list);
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

        #region //根据远程Ids查询订单信息
        /// <summary>
        /// 根据远程Ids查询订单信息
        /// </summary>
        /// <param name="Ids">远程Ids</param>
        /// <returns></returns>
        public WL_XFOrderinfoView GetxforderinfobyIds(string Ids)
        {
            string Hqlstr = string.Format(" from WL_XFOrderinfo where Ids='{0}'",Ids);
            List<WL_XFOrderinfo> list = Session.CreateQuery(Hqlstr).List<WL_XFOrderinfo>() as List<WL_XFOrderinfo>;
            IList<WL_XFOrderinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据sid查找订单是否存在
        /// <summary>
        /// 根据sid查找订单是否存在
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        public bool GetxforderIsczbysid(string sid)
        {
            string Hqlstr = string.Format(" from WL_XFOrderinfo where Sid='{0}'",sid);
            List<WL_XFOrderinfo> list = Session.CreateQuery(Hqlstr).List<WL_XFOrderinfo>() as List<WL_XFOrderinfo>;
            if (list != null)
                return true;
            else
                return false;
        }
        #endregion


        #region //续费订单的分页数据
        /// <summary>
        /// 续费订单的分页数据
        /// </summary>
        /// <param name="SID">sid</param>
        /// <param name="XDdatetime">下单时间</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<WL_XFOrderinfoView> GetWL_xforderinfoList(string SID, string XDstartdatetime,string XDenddatetime, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(SID))
                TempHql.AppendFormat(" and Sid='{0}'", SID);
            if (!string.IsNullOrEmpty(XDstartdatetime)&&!string.IsNullOrEmpty(XDenddatetime))
                TempHql.AppendFormat("and xddatetime>='{0}' and  xddatetime<='{1}'", XDstartdatetime + " 00:00:00", XDenddatetime + " 23:59:59");
            TempHql.AppendFormat("order by xddatetime desc");
            PagerInfo<WL_XFOrderinfoView> list = Search();
            return list;
        }
        #endregion

        #region //根据经销商uId 返回续费订单的数量
        public int jxqGetordersumbyuid(string uid)
        {
            string tempHql;
            tempHql = string.Format("from WL_XFOrderinfo where Sid in(select WL_SSD from WL_DkxInfo where Xs_jxsId='{0}')", uid);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion
    }
}
