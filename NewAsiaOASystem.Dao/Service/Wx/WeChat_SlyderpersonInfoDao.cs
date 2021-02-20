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
    public class WeChat_SlyderpersonInfoDao:ServiceConversion<WeChat_SlyderpersonInfoView,WeChat_SlyderpersonInfo>,IWeChat_SlyderpersonInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_SlyderpersonInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_SlyderpersonInfoView GetView(WeChat_SlyderpersonInfo data)
        {
            WeChat_SlyderpersonInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_SlyderpersonInfoView model)
        {
            WeChat_SlyderpersonInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_SlyderpersonInfoView model)
        {
            WeChat_SlyderpersonInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_SlyderpersonInfoView model)
        {
            WeChat_SlyderpersonInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_SlyderpersonInfoView> model)
        {
            IList<WeChat_SlyderpersonInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_SlyderpersonInfoView> NGetList()
        {
            List<WeChat_SlyderpersonInfo> listdata = base.GetList() as List<WeChat_SlyderpersonInfo>;
            IList<WeChat_SlyderpersonInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_SlyderpersonInfoView> NGetList_id(string id)
        {
            List<WeChat_SlyderpersonInfo> list = base.GetList_id(id) as List<WeChat_SlyderpersonInfo>;
            IList<WeChat_SlyderpersonInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_SlyderpersonInfo> NGetListID(string id)
        {
            IList<WeChat_SlyderpersonInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_SlyderpersonInfoView NGetModelById(string id)
        {
            WeChat_SlyderpersonInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //检测是否有重复的openid
        public bool Jcopenid(string openid)
        {
            try
            {
                string HQLstr = string.Format("from WeChat_SlyderpersonInfo where openid='{0}'", openid);
                List<WeChat_SlyderpersonInfo> list = HibernateTemplate.Find<WeChat_SlyderpersonInfo>(HQLstr) as List<WeChat_SlyderpersonInfo>;
                return list.Count > 0 ? true : false;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("查看：" + e);
                return false;
            }
        }

        //根据OPENID 查找微信好友的信息
        public WeChat_SlyderpersonInfoView Getwxinfobyopenid(string openid)
        {
            string temHql = string.Format("from WeChat_SlyderpersonInfo where openid='{0}'", openid);
            //string HQLstr = string.Format("from Flow_RoutineStockinfo where P_Bianhao='{0}'", p_bianhao);
            List<WeChat_SlyderpersonInfo> list = HibernateTemplate.Find<WeChat_SlyderpersonInfo>(temHql) as List<WeChat_SlyderpersonInfo>;
            IList<WeChat_SlyderpersonInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }

        #region //Json 获取中奖名单
        public string  GetZJjson()
        {
            string temHql = string.Format(" from WeChat_SlyderpersonInfo where IsWin in ('1','2') order by C_time desc");
            List<WeChat_SlyderpersonInfo> list = HibernateTemplate.Find<WeChat_SlyderpersonInfo>(temHql) as List<WeChat_SlyderpersonInfo>;
            string strjson;
            strjson = JsonConvert.SerializeObject(list);
            return strjson;

        }
        #endregion

        #region //根据中奖字符串 查找
        public WeChat_SlyderpersonInfoView Getinfobyzjstr(string Winstr)
        {
            string temHql = string.Format("from WeChat_SlyderpersonInfo where Winstr='{0}'", Winstr);
            List<WeChat_SlyderpersonInfo> list = HibernateTemplate.Find<WeChat_SlyderpersonInfo>(temHql) as List<WeChat_SlyderpersonInfo>;
            IList<WeChat_SlyderpersonInfoView> listmodel = GetViewlist(list);
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

        #region //抽奖列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zjstr">领奖字符串</param>
        /// <param name="wintype">领奖状态</param>
        /// <returns></returns>
        public PagerInfo<WeChat_SlyderpersonInfoView> GetCinfoList(string zjstr,string wintype)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(zjstr))
                TempHql.AppendFormat(" and u.Winstr='{0}' ", zjstr);
            if (!string.IsNullOrEmpty(wintype))
                TempHql.AppendFormat(" and u.IsWin='{0}' ", wintype);
            TempHql.AppendFormat(" order by u.C_time desc");
            PagerInfo<WeChat_SlyderpersonInfoView> list = Search();
            return list;
        }
        #endregion

       
    }
}
