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
    public class WeChat_OrderInfoDao:ServiceConversion<WeChat_OrderInfoView,WeChat_OrderInfo>,IWeChat_OrderInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_OrderInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_OrderInfoView GetView(WeChat_OrderInfo data)
        {
            WeChat_OrderInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_OrderInfoView model)
        {
            WeChat_OrderInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_OrderInfoView model)
        {
            WeChat_OrderInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_OrderInfoView model)
        {
            WeChat_OrderInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_OrderInfoView> model)
        {
            IList<WeChat_OrderInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_OrderInfoView> NGetList()
        {
            List<WeChat_OrderInfo> listdata = base.GetList() as List<WeChat_OrderInfo>;
            IList<WeChat_OrderInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_OrderInfoView> NGetList_id(string id)
        {
            List<WeChat_OrderInfo> list = base.GetList_id(id) as List<WeChat_OrderInfo>;
            IList<WeChat_OrderInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_OrderInfo> NGetListID(string id)
        {
            IList<WeChat_OrderInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_OrderInfoView NGetModelById(string id)
        {
            WeChat_OrderInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //保存后返回ID
        public string InsertID(WeChat_OrderInfoView modelView)
        {
            WeChat_OrderInfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        //检测是否有重复的openid
        //public bool Jcopenid(string openid)
        //{
        //    string HQLstr = string.Format("from WeChat_forwardInfo where openid='{0}'", openid);
        //    List<WeChat_forwardInfo> list = HibernateTemplate.Find<WeChat_forwardInfo>(HQLstr) as List<WeChat_forwardInfo>;
        //    return list.Count > 0 ? true : false;
        //}



        //public PagerInfo<WeChat_forwardInfoView> GetWXHDcusList()
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    //if (!string.IsNullOrEmpty(CPName))
        //    //    TempHql.AppendFormat(" and u.Yqj_Name like '%{0}%' ", CPName);
        //    //if (!string.IsNullOrEmpty(P_Pianhao))
        //    //    TempHql.AppendFormat(" and u.Yqj_bianhao='{0}' ", P_Pianhao);
        //    //if (!string.IsNullOrEmpty(P_Issc))
        //    //    TempHql.AppendFormat(" and u.Type='{0}'", P_Issc);
        //    //if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        //    //    TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
        //    TempHql.AppendFormat(" order by u.C_time asc");
        //    PagerInfo<WeChat_forwardInfoView> list = Search();
        //    return list;
        //}

        //微信(好友成交数量)直接收益
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0 直接  1 间接  2 再间接</param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int Getchengjiaoshuli(string type, string OpenId) 
        {
            string temHql;
            if (type == "0")
            {
                temHql = string.Format("from WeChat_OrderInfo where OpenId in (select openid from WeChat_forwardInfo where P_openid='{0}') and Type='1'", OpenId);
            }
           else  if (type == "1")
            {
                temHql = string.Format("from WeChat_OrderInfo where OpenId in (select openid from WeChat_forwardInfo where  P_openid in (select openid from WeChat_forwardInfo where P_openid='{0}'))  and Type='1'", OpenId);
            }
            else{
                temHql = string.Format("from WeChat_OrderInfo where OpenId in (select openid from WeChat_forwardInfo where  P_openid in (select openid from WeChat_forwardInfo where  P_openid in (select openid from WeChat_forwardInfo where P_openid='{0}')))  and Type='1'", OpenId);
            }
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", temHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }


        //好友最新成交记录
        public IList<WeChat_OrderInfoView> Getcjjltop(string OpenId)
        {
            string temHql = string.Format("from WeChat_OrderInfo where OpenId in (select openid from WeChat_forwardInfo where P_openid='{0}') and Type='1' order by C_time desc", OpenId);
            List<WeChat_OrderInfo> list = HibernateTemplate.Find<WeChat_OrderInfo>(temHql) as List<WeChat_OrderInfo>;
            IList<WeChat_OrderInfoView> listmodel = GetViewlist(list);
            return listmodel;
           
        }

    }
}
