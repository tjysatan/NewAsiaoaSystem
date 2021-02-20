using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;
using Newtonsoft.Json;
using NHibernate;

namespace NewAsiaOASystem.Dao
{
    public class YCnoticeinfoDao:ServiceConversion<YCnoticeinfoView, YCnoticeinfo>,IYCnoticeinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(YCnoticeinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(YCnoticeinfoView model)
        {
            YCnoticeinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(YCnoticeinfoView model)
        {
            YCnoticeinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(YCnoticeinfoView model)
        {
            YCnoticeinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<YCnoticeinfoView> model)
        {
            IList<YCnoticeinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<YCnoticeinfoView> NGetList()
        {
            List<YCnoticeinfo> listdata = base.GetList() as List<YCnoticeinfo>;
            IList<YCnoticeinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<YCnoticeinfo> listdata = base.GetList() as List<YCnoticeinfo>;
            IList<YCnoticeinfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCnoticeinfoView> NGetList_id(string id)
        {
            List<YCnoticeinfo> list = base.GetList_id(id) as List<YCnoticeinfo>;
            IList<YCnoticeinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCnoticeinfo> NGetListModel(string id)
        {
            IList<YCnoticeinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YCnoticeinfoView NGetModelById(string id)
        {
            YCnoticeinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //当天发送的数量
        public decimal GetTodayFXsum(string openId)
        {
            string Hqlstr = string.Format(" from YCnoticeinfo where openId='{0}' and DateDiff(dd,C_time,getdate())=0 ", openId);
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*) {0}", Hqlstr));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //近五天内的告警数据
        /// <summary>
        /// 近五天内的告警数据
        /// </summary>
        /// <param name="openId">微信openId</param>
        /// <returns></returns>
        public IList<YCnoticeinfoView> GetwutianFxnoticeinfo(string openId)
        {
            string Hqlstr = string.Format(" from YCnoticeinfo where openId='{0}' and DateDiff(dd,C_time,getdate())<=5", openId);
            List<YCnoticeinfo> list = HibernateTemplate.Find<YCnoticeinfo>(Hqlstr) as List<YCnoticeinfo>;
            IList<YCnoticeinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel;
        }
        #endregion
    }
}
