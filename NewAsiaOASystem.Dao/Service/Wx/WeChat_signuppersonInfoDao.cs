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
    public  class WeChat_signuppersonInfoDao:ServiceConversion<WeChat_signuppersonInfoView,WeChat_signuppersonInfo>,IWeChat_signuppersonInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_signuppersonInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_signuppersonInfoView GetView(WeChat_signuppersonInfo data)
        {
            WeChat_signuppersonInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_signuppersonInfoView model)
        {
            WeChat_signuppersonInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_signuppersonInfoView model)
        {
            WeChat_signuppersonInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_signuppersonInfoView model)
        {
            WeChat_signuppersonInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_signuppersonInfoView> model)
        {
            IList<WeChat_signuppersonInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_signuppersonInfoView> NGetList()
        {
            List<WeChat_signuppersonInfo> listdata = base.GetList() as List<WeChat_signuppersonInfo>;
            IList<WeChat_signuppersonInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_signuppersonInfoView> NGetList_id(string id)
        {
            List<WeChat_signuppersonInfo> list = base.GetList_id(id) as List<WeChat_signuppersonInfo>;
            IList<WeChat_signuppersonInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_signuppersonInfo> NGetListID(string id)
        {
            IList<WeChat_signuppersonInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_signuppersonInfoView NGetModelById(string id)
        {
            WeChat_signuppersonInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //检测是否有重复的openid
        public bool Jcopenid(string openid)
        {
            try
            {
                string HQLstr = string.Format("from WeChat_signuppersonInfo where openid='{0}'", openid);
                List<WeChat_signuppersonInfo> list = HibernateTemplate.Find<WeChat_signuppersonInfo>(HQLstr) as List<WeChat_signuppersonInfo>;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("查看：" + list.Count);
                return list.Count > 0 ? true : false;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("查看："+e);
                return false;
            }
        }

        //根据OPENID 查找微信好友的信息
        public WeChat_signuppersonInfoView Getwxinfobyopenid(string openid)
        {
            string temHql = string.Format("from WeChat_signuppersonInfo where openid='{0}'", openid);
            //string HQLstr = string.Format("from Flow_RoutineStockinfo where P_Bianhao='{0}'", p_bianhao);
            List<WeChat_signuppersonInfo> list = HibernateTemplate.Find<WeChat_signuppersonInfo>(temHql) as List<WeChat_signuppersonInfo>;
            IList<WeChat_signuppersonInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }

        //查找报名数量
        public int Getsignupcount()
        {
            try
            {
                string temHql = string.Format("from WeChat_signuppersonInfo");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
    }
}
    