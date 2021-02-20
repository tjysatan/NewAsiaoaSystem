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
    public class WL_dkxthjlinfoDao:ServiceConversion<WL_dkxthjlinfoView,WL_dkxthjlinfo>,IWL_dkxthjlinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_dkxthjlinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_dkxthjlinfoView GetView(WL_dkxthjlinfo data)
        {
            WL_dkxthjlinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_dkxthjlinfoView model)
        {
            WL_dkxthjlinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_dkxthjlinfoView model)
        {
            WL_dkxthjlinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_dkxthjlinfoView model)
        {
            WL_dkxthjlinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_dkxthjlinfoView> model)
        {
            IList<WL_dkxthjlinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_dkxthjlinfoView> NGetList()
        {
            List<WL_dkxthjlinfo> listdata = base.GetList() as List<WL_dkxthjlinfo>;
            IList<WL_dkxthjlinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_dkxthjlinfoView> NGetList_id(string id)
        {
            List<WL_dkxthjlinfo> list = base.GetList_id(id) as List<WL_dkxthjlinfo>;
            IList<WL_dkxthjlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_dkxthjlinfo> NGetListID(string id)
        {
            IList<WL_dkxthjlinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_dkxthjlinfoView NGetModelById(string id)
        {
            WL_dkxthjlinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询当天扫码退货的数据
        public IList<WL_dkxthjlinfoView> GetsmthdataToday()
        {
            string tempHql = "from WL_dkxthjlinfo where jltype='0' and DateDiff(dd,THdatetime,getdate())=0 order by THdatetime desc";
            List<WL_dkxthjlinfo> list = Session.CreateQuery(tempHql).List<WL_dkxthjlinfo>() as List<WL_dkxthjlinfo>;
            IList<WL_dkxthjlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //SID退货历史记录

        /// <summary>
        /// SID退货历史记录
        /// </summary>
        /// <param name="jxsname">经销商名称</param>
        /// <param name="startTHdatetime">退货时间</param>
        /// <param name="endTHdatetime"></param>
        /// <returns></returns>
        public PagerInfo<WL_dkxthjlinfoView> GetWL_dkxthlistPage(string SID, string jxsname, string startTHdatetime, string endTHdatetime)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(SID))
                TempHql.AppendFormat(" and u.SID='{0}'",SID);
           if (!string.IsNullOrEmpty(jxsname))
               TempHql.AppendFormat(" and u.XsordId in(select Id from NA_XSinfo where KhId in (select Id from NACustomerinfo where Name like'%{0}%'))", jxsname);
           if (!string.IsNullOrEmpty(startTHdatetime))
               TempHql.AppendFormat("and THdatetime>='{0}' and THdatetime<='{1}'", startTHdatetime + " 00:00:00", endTHdatetime + " 23:59:59");
           TempHql.AppendFormat("order by THdatetime desc");
           PagerInfo<WL_dkxthjlinfoView> list = Search();
           return list;
        }
        #endregion


        #region //根据经销商Id查询出sid退货的数量
        public int jxqGetTHsidbyjxsId(string jxsId)
        {
            string tempHql;
            tempHql = string.Format("from WL_dkxthjlinfo where XsordId in(select Id from NA_XSinfo where KhId='{0}')", jxsId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion
        
    }
}
