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
    public class WX_OpenIDDao : ServiceConversion<WX_OpenIDView, WX_OpenID>, IWX_OpenIDDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_OpenIDView GetView(WX_OpenID data)
        {
            WX_OpenIDView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_OpenIDView model)
        {
            WX_OpenID listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_OpenIDView model)
        {
            WX_OpenID listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除3
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_OpenIDView model)
        {
            WX_OpenID listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_OpenIDView> model)
        {
            IList<WX_OpenID> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_OpenIDView> NGetList()
        {
            List<WX_OpenID> listdata = base.GetList() as List<WX_OpenID>;
            IList<WX_OpenIDView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_OpenIDView> NGetList_id(string id)
        {
            List<WX_OpenID> list = base.GetList_id(id) as List<WX_OpenID>;
            IList<WX_OpenIDView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_OpenID> NGetListID(string id)
        {
            IList<WX_OpenID> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_OpenIDView NGetModelById(string id)
        {
            WX_OpenIDView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


         
        #region //根据OpenID 判断是否已经绑定
        public IList<WX_OpenIDView> GetCount_byOpenId(string OpenId,string type)
        {
            string tempHql = string.Format(" from  WX_OpenID  where  OpenId = '{0}' and Type='{1}'", OpenId,type);
            try
            {
                List<WX_OpenID> list = Session.CreateQuery(tempHql).List<WX_OpenID>() as List<WX_OpenID>;
                IList<WX_OpenIDView> listmodel = GetViewlist(list);
                return listmodel;

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //检查帐号是否已经绑定其他微信号
        public IList<WX_OpenIDView> GetCount_byP_Id(string P_Id,string type)
        {
            string tempHql = string.Format(" from  WX_OpenID  where  Person_Id = '{0}' and Type='{1}'", P_Id,type);
            try
            {
                List<WX_OpenID> list = Session.CreateQuery(tempHql).List<WX_OpenID>() as List<WX_OpenID>;
                IList<WX_OpenIDView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        //查找全部内部绑定的 微信
        public IList<WX_OpenIDView> GetNBopenid()
        {
            string tempHql = string.Format(" from WX_OpenID where Type='{0}'", 1);
            try
            {
                List<WX_OpenID> list = Session.CreateQuery(tempHql).List<WX_OpenID>() as List<WX_OpenID>;
                IList<WX_OpenIDView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        #region //读取绑定的微信OPENID
        public string GetbdOpenId()
        {
            IList<WX_OpenIDView> list = NGetList();
            String[] st = new String[list.Count];
            for (int i = 0, j = list.Count; i < j; i++)
            {
                st[i] = list[i].OpenId;
            }
            return string.Join("\",\"", st);
        } 
        #endregion

        #region //根据OpenId 判断是否是绑定用户
         public bool  IsnotBD(string Id)
        {
            string tempSqldc =Id;
            string tempHql = string.Format("from WX_OpenID where  OpenId= '{0}'", tempSqldc);
            if (!string.IsNullOrEmpty(tempSqldc))
            {
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count > 0 ? true : false;
               // return Convert.ToInt32(queryCount.UniqueResult());
            }
            else
            {
                return false;
            }
        }
        #endregion


      


    }
}
