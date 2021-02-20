using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public class WeChat_forwardInfoDao:ServiceConversion<WeChat_forwardInfoView,WeChat_forwardInfo>,IWeChat_forwardInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_forwardInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_forwardInfoView GetView(WeChat_forwardInfo data)
        {
            WeChat_forwardInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_forwardInfoView model)
        {
            WeChat_forwardInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_forwardInfoView model)
        {
            WeChat_forwardInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_forwardInfoView model)
        {
            WeChat_forwardInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_forwardInfoView> model)
        {
            IList<WeChat_forwardInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_forwardInfoView> NGetList()
        {
            List<WeChat_forwardInfo> listdata = base.GetList() as List<WeChat_forwardInfo>;
            IList<WeChat_forwardInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_forwardInfoView> NGetList_id(string id)
        {
            List<WeChat_forwardInfo> list = base.GetList_id(id) as List<WeChat_forwardInfo>;
            IList<WeChat_forwardInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_forwardInfo> NGetListID(string id)
        {
            IList<WeChat_forwardInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_forwardInfoView NGetModelById(string id)
        {
            WeChat_forwardInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //检测是否有重复的openid
        public bool Jcopenid(string openid)
        {
            string HQLstr = string.Format("from WeChat_forwardInfo where openid='{0}'", openid);
            List<WeChat_forwardInfo> list = HibernateTemplate.Find<WeChat_forwardInfo>(HQLstr) as List<WeChat_forwardInfo>;
            return list.Count > 0 ? true : false;
        }



        public PagerInfo<WeChat_forwardInfoView> GetWXHDcusList()
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            //if (!string.IsNullOrEmpty(CPName))
            //    TempHql.AppendFormat(" and u.Yqj_Name like '%{0}%' ", CPName);
            //if (!string.IsNullOrEmpty(P_Pianhao))
            //    TempHql.AppendFormat(" and u.Yqj_bianhao='{0}' ", P_Pianhao);
            //if (!string.IsNullOrEmpty(P_Issc))
            //    TempHql.AppendFormat(" and u.Type='{0}'", P_Issc);
            //if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            //    TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat(" order by u.C_time asc");
            PagerInfo<WeChat_forwardInfoView> list = Search();
            return list;
        }



        #region //直接点击量和间接统计量
        /// <summary>
        /// //直接点击量
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int GetzjtjcunotbyOpenId(string OpenId)
        {
            string temHql = string.Format("from WeChat_forwardInfo where P_openid='{0}'", OpenId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", temHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        } 
       

        /// <summary>
        /// 间接点击量
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int GetjjtjcunotbyOpenId(string OpenId)
        {
            string temHql = string.Format("from WeChat_forwardInfo where P_openid in (select openid from WeChat_forwardInfo where P_openid='{0}')", OpenId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", temHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        } 
        #endregion

        //根据OPENID 查找微信好友的信息
        public WeChat_forwardInfoView Getwxinfobyopenid(string openid)
        {
            string temHql = string.Format("from WeChat_forwardInfo where openid='{0}'", openid);
            //string HQLstr = string.Format("from Flow_RoutineStockinfo where P_Bianhao='{0}'", p_bianhao);
            List<WeChat_forwardInfo> list = HibernateTemplate.Find<WeChat_forwardInfo>(temHql) as List<WeChat_forwardInfo>;
            IList<WeChat_forwardInfoView> listmodel = GetViewlist(list);
            return listmodel[0];
        }


 
    }
}
