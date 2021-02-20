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
    public  class WeChat_AnnualmeetingSignDao:ServiceConversion<WeChat_AnnualmeetingSignView,WeChat_AnnualmeetingSign>,IWeChat_AnnualmeetingSignDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_AnnualmeetingSign).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_AnnualmeetingSignView GetView(WeChat_AnnualmeetingSign data)
        {
            WeChat_AnnualmeetingSignView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_AnnualmeetingSignView model)
        {
            WeChat_AnnualmeetingSign listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_AnnualmeetingSignView model)
        {
            WeChat_AnnualmeetingSign listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_AnnualmeetingSignView model)
        {
            WeChat_AnnualmeetingSign listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_AnnualmeetingSignView> model)
        {
            IList<WeChat_AnnualmeetingSign> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_AnnualmeetingSignView> NGetList()
        {
            List<WeChat_AnnualmeetingSign> listdata = base.GetList() as List<WeChat_AnnualmeetingSign>;
            IList<WeChat_AnnualmeetingSignView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_AnnualmeetingSignView> NGetList_id(string id)
        {
            List<WeChat_AnnualmeetingSign> list = base.GetList_id(id) as List<WeChat_AnnualmeetingSign>;
            IList<WeChat_AnnualmeetingSignView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_AnnualmeetingSign> NGetListID(string id)
        {
            IList<WeChat_AnnualmeetingSign> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_AnnualmeetingSignView NGetModelById(string id)
        {
            WeChat_AnnualmeetingSignView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //检测是否有重复的openid
        public bool Jcopenid(string openid)
        {
            try
            {
                string HQLstr = string.Format("from WeChat_AnnualmeetingSign where openid='{0}'", openid);
                List<WeChat_AnnualmeetingSign> list = HibernateTemplate.Find<WeChat_AnnualmeetingSign>(HQLstr) as List<WeChat_AnnualmeetingSign>;
                return list.Count > 0 ? true : false;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("查看：" + e);
                return false;
            }
        }

        //根据OPENID 查找微信好友的信息
        public WeChat_AnnualmeetingSignView Getwxinfobyopenid(string openid)
        {
            string temHql = string.Format("from WeChat_AnnualmeetingSign where openid='{0}'", openid);
            List<WeChat_AnnualmeetingSign> list = HibernateTemplate.Find<WeChat_AnnualmeetingSign>(temHql) as List<WeChat_AnnualmeetingSign>;
            IList<WeChat_AnnualmeetingSignView> listmodel = GetViewlist(list);
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
                string temHql = string.Format("from WeChat_AnnualmeetingSign");
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
