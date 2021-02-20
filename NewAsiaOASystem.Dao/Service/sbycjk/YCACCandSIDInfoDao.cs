using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Dao
{
    public class YCACCandSIDInfoDao:ServiceConversion<YCACCandSIDInfoView,YCACCandSIDInfo>,IYCACCandSIDInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(YCACCandSIDInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(YCACCandSIDInfoView model)
        {
            YCACCandSIDInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(YCACCandSIDInfoView model)
        {
            YCACCandSIDInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(YCACCandSIDInfoView model)
        {
            YCACCandSIDInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<YCACCandSIDInfoView> model)
        {
            IList<YCACCandSIDInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<YCACCandSIDInfoView> NGetList()
        {
            List<YCACCandSIDInfo> listdata = base.GetList() as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<YCACCandSIDInfo> listdata = base.GetList() as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCACCandSIDInfoView> NGetList_id(string id)
        {
            List<YCACCandSIDInfo> list = base.GetList_id(id) as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<YCACCandSIDInfo> NGetListModel(string id)
        {
            IList<YCACCandSIDInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YCACCandSIDInfoView NGetModelById(string id)
        {
            YCACCandSIDInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据帐号绑定的Id 和sid
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameId"></param>
        /// <param name="SID"></param>
        /// <returns></returns>
        public YCACCandSIDInfoView GetSIDbynameIdandsid(string nameId, string SID)
        {
            string HQLstr = string.Format("from YCACCandSIDInfo where nameId='{0}' and SID='{1}'", nameId, SID);
            List<YCACCandSIDInfo> list = HibernateTemplate.Find<YCACCandSIDInfo>(HQLstr) as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //根据远程帐号绑定的Id查找要发送和不要发送通知的SID
        /// <summary>
        /// 根据远程帐号绑定的Id查找要发送和不要发送通知的SID
        /// </summary>
        /// <param name="nameId">远程帐号绑定微信的唯一值</param>
        /// <param name="type">是否发送 0 发送 1 不发送</param>
        /// <returns></returns>
        public IList<YCACCandSIDInfoView> GetSIDlistIsfsbyzhId(string nameId, string type)
        {
            string HQLstr = string.Format("from YCACCandSIDInfo where nameId='{0}' and Isfs='{1}'", nameId, type);
            List<YCACCandSIDInfo> list = HibernateTemplate.Find<YCACCandSIDInfo>(HQLstr) as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel;
        }
        #endregion

        #region //根据绑定的帐号的Id 查找监控点数据
        /// <summary>
        /// 根据绑定的帐号的Id 查找监控点数据
        /// </summary>
        /// <param name="nameId">绑定帐号Id</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<YCACCandSIDInfoView> GetALLSIDlistbyzhId(string nameId)
        {
            string HQLstr = string.Format("from YCACCandSIDInfo where nameId='{0}'", nameId);
            List<YCACCandSIDInfo> list = HibernateTemplate.Find<YCACCandSIDInfo>(HQLstr) as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel;
        }
        #endregion

        #region //通过SID查找绑定帐号的信息
        /// <summary>
        /// //通过SID查找绑定帐号的信息
        /// </summary>
        /// <param name="SID">SID</param>
        /// <returns></returns>
        public IList<YCACCandSIDInfoView> GetSIDBDZHlistbySID(string SID)
        {
            string HQLstr = string.Format(" from YCACCandSIDInfo where SID='{0}'", SID);
            List<YCACCandSIDInfo> list = HibernateTemplate.Find<YCACCandSIDInfo>(HQLstr) as List<YCACCandSIDInfo>;
            IList<YCACCandSIDInfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel;
        }
        #endregion

    }
}
