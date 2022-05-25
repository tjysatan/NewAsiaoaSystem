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
    public class YCAccountbindingInfoDao:ServiceConversion<YCAccountbindingInfoView,YCAccountbindingInfo>,IYCAccountbindingInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(YCAccountbindingInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(YCAccountbindingInfoView model)
        {
            YCAccountbindingInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(YCAccountbindingInfoView model)
        {
            YCAccountbindingInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(YCAccountbindingInfoView model)
        {
            YCAccountbindingInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<YCAccountbindingInfoView> model)
        {
            IList<YCAccountbindingInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<YCAccountbindingInfoView> NGetList()
        {
            List<YCAccountbindingInfo> listdata = base.GetList() as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<YCAccountbindingInfo> listdata = base.GetList() as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCAccountbindingInfoView> NGetList_id(string id)
        {
            List<YCAccountbindingInfo> list = base.GetList_id(id) as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCAccountbindingInfo> NGetListModel(string id)
        {
            IList<YCAccountbindingInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YCAccountbindingInfoView NGetModelById(string id)
        {
            YCAccountbindingInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据帐号和OPENId查询绑定数据
        /// <summary>
        /// 根据帐号和OPENId查询绑定数据
        /// </summary>
        /// <param name="user">帐号</param>
        /// <param name="openId">openId</param>
        /// <returns></returns>
        public YCAccountbindingInfoView GetAccbindingbyuserandopenId(string user, string openId)
        {
            string HQLstr = string.Format("from YCAccountbindingInfo where Username='{0}' and openId='{1}'", user, openId);
            List<YCAccountbindingInfo> list = HibernateTemplate.Find<YCAccountbindingInfo>(HQLstr) as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //通过openId查找绑定的帐号数据list
        /// <summary>
        /// 通过openId查找绑定的帐号数据list
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public IList<YCAccountbindingInfoView> GetacclistinfobyopenId(string openId)
        {
            string HQLstr = string.Format("from YCAccountbindingInfo where openId='{0}' order by C_time desc", openId);
            List<YCAccountbindingInfo> list = HibernateTemplate.Find<YCAccountbindingInfo>(HQLstr) as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel;
        }
        #endregion

        //#region //无限卡激活总数
        ///// <summary>
        ///// 无限卡激活总数
        ///// </summary>
        ///// <returns></returns>
        //public decimal WXcardJHcoun(string gsdm)
        //{
        //    string Hqlstr = string.Format("from NewICCIDinfo where tctype='2' and Iscs='1' and IsDis='0' and GSDM='{0}'", gsdm);
        //    IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*)  {0} ", Hqlstr));
        //    int count = Convert.ToInt32(queryCount.UniqueResult());
        //    return count;
        //}
        //#endregion

        #region //通过openId统计绑定帐号的数量
        /// <summary>
        /// 通过openId统计绑定帐号的数量
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public decimal bdzhcountbyopenId(string openId)
        {
            string Hqlstr = string.Format(" from YCAccountbindingInfo where openId='{0}'", openId);
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*) {0}", Hqlstr));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //通过Id 查找绑定的帐号数据
        /// <summary>
        /// 通过Id 查找绑定的帐号数据
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        public YCAccountbindingInfoView GetbdzhinfobyId(string Id)
        {
            string Hqlstr = string.Format(" from YCAccountbindingInfo where Id='{0}'", Id);
            List<YCAccountbindingInfo> list = HibernateTemplate.Find<YCAccountbindingInfo>(Hqlstr) as List<YCAccountbindingInfo>;
            IList<YCAccountbindingInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion
    }
}
