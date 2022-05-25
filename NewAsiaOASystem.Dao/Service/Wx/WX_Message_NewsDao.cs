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
    public class WX_Message_NewsDao : ServiceConversion<WX_Message_NewsView, WX_Message_News>, IWX_Message_NewsDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WX_Message_News).Name, TempHql.ToString());
        }
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_Message_NewsView GetView(WX_Message_News data)
        {
            WX_Message_NewsView view = ConvertToDTO(data);

            return view;
        }

        #region //保存后返回ID
        public string InsertID(WX_Message_NewsView modelView)
        {
            WX_Message_News model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.N_Id;
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

        /// <summary>
        /// 覆写查询的的HQL 语句
        /// </summary>
        /// <returns></returns>
        //public override String GetSearchHql()
        //{
        //    return " from WX_Message_News  where  MType='0'";
        //}

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_Message_NewsView model)
        {
            WX_Message_News listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_Message_NewsView model)
        {
            WX_Message_News listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_Message_NewsView model)
        {
            WX_Message_News listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_Message_NewsView> model)
        {
            IList<WX_Message_News> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        public bool wx_MNdelete(List<WX_Message_News> mode)
        {
            return base.NDelete(mode);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_Message_NewsView> NGetList()
        {
            List<WX_Message_News> listdata = base.GetList() as List<WX_Message_News>;
            IList<WX_Message_NewsView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_Message_NewsView> NGetList_id(string id)
        {
            List<WX_Message_News> list = base.GetList_id(id) as List<WX_Message_News>;
            IList<WX_Message_NewsView> listmodel = GetViewlist(list);
            return listmodel;
        }

        #region //根据多个Id 查询多条记录
        public IList<WX_Message_News> NGetListdata_id(string id)
        {
            //List<WX_Message_News> list = base.GetList_id(id) as List<WX_Message_News>;
            //return list;
            try
            {
                WX_Message_News t = default(WX_Message_News);
                t = Activator.CreateInstance<WX_Message_News>();
                return HibernateTemplate.Find<WX_Message_News>(string.Format(" from WX_Message_News where N_Id in ({1})", t.GetType().Name, id));
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_Message_News> NGetListID(string id)
        {
            IList<WX_Message_News> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_Message_NewsView NGetModelById(string id)
        {
            WX_Message_NewsView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //根据ID查找 GetWX_Messageby_id（）
        public IList<WX_Message_News> GetWX_Message_newby_id(string id)
        {
            string tempHql = string.Format(" from  WX_Message_News  where  A_id = '{0}'", id);
            try
            {
                IList<WX_Message_News> list = Session.CreateQuery(tempHql).List<WX_Message_News>();
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region //根据ID查找 GetWX_Messageby_id（）
        public IList<WX_Message_NewsView> GetWX_Message_newby_Nid(string id)
        {
            string tempHql = string.Format(" from  WX_Message_News  where  N_Id  in ({0})", id);
            try
            {
                // List<WX_Message> list = Session.CreateQuery(tempHql) as List<WX_Message>;
                List<WX_Message_News> list = Session.CreateQuery(tempHql).List<WX_Message_News>() as List<WX_Message_News>;
                IList<WX_Message_NewsView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //根据图文的类型查找
        public WX_Message_NewsView GetWX_Message_newby_type(int type)
        {
            string tempHql = string.Format(" from  WX_Message_News  where  MType='{0}'", type);
            try
            {
                // List<WX_Message> list = Session.CreateQuery(tempHql) as List<WX_Message>;
                List<WX_Message_News> list = Session.CreateQuery(tempHql).List<WX_Message_News>() as List<WX_Message_News>;
                IList<WX_Message_NewsView> listmodel = GetViewlist(list);
                if (listmodel != null && listmodel.Count != 0)
                {
                    return listmodel[0];
                }
                return null;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //根据图文的类型查找
        public IList<WX_Message_NewsView> GetWX_Message_newlistby_type(int type)
        {
            string tempHql = string.Format(" from  WX_Message_News  where  MType='{0}'", type);
            try
            {
                // List<WX_Message> list = Session.CreateQuery(tempHql) as List<WX_Message>;
                List<WX_Message_News> list = Session.CreateQuery(tempHql).List<WX_Message_News>() as List<WX_Message_News>;
                IList<WX_Message_NewsView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region //回复地图时修改URL
        public bool UpdateUrl(WX_Message_News model)
        {
            return base.Update(model);
        }
        #endregion


        #region //查询分页数据
        public PagerInfo<WX_Message_NewsView> GetCinfoList(string Title,string Description)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Title))
                TempHql.AppendFormat(" and u.Title like '%{0}%'", Title);
            if (!string.IsNullOrEmpty(Description))
                TempHql.AppendFormat(" and u.Description like '%{0}%'", Description);
            PagerInfo<WX_Message_NewsView> list = Search();
            return list;
        }
        #endregion
    }
}
