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
    public  class WX_MenusDao:ServiceConversion<WX_MenusView,WX_Menus>,IWX_MenusDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WX_Menus).Name, TempHql.ToString());
        }
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_MenusView GetView(WX_Menus data)
        {
            WX_MenusView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_MenusView model)
        {
            WX_Menus listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_MenusView model)
        {
            WX_Menus listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_MenusView model)
        {
            WX_Menus listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_MenusView> model)
        {
            IList<WX_Menus> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_MenusView> NGetList()
        {
            List<WX_Menus> listdata = base.GetList() as List<WX_Menus>;
            IList<WX_MenusView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_MenusView> NGetList_id(string id)
        {
            List<WX_Menus> list = base.GetList_id(id) as List<WX_Menus>;
            IList<WX_MenusView> listmodel = GetViewlist(list);
            return listmodel;
        }


       

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_Menus> NGetListID(string id)
        {
            IList<WX_Menus> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_MenusView NGetModelById(string id)
        {
            WX_MenusView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //通过父级ID 查找二级菜单
        public IList<WX_MenusView> wx_GetejMenu_by(string Id)
        {
            string tempHql = string.Format(" from  WX_Menus  where  M_ParentID = '{0}'", Id);
            try
            {
                List<WX_Menus> list = Session.CreateQuery(tempHql).List<WX_Menus>() as List<WX_Menus>;
                IList<WX_MenusView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //只查询一级菜单
        public IList<WX_MenusView> wx_Geteonemenu()
        {
            string tempHql = string.Format(" from  WX_Menus  where  M_ParentID = ''");
            try
            {
                List<WX_Menus> list = Session.CreateQuery(tempHql).List<WX_Menus>() as List<WX_Menus>;
                IList<WX_MenusView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //自定义菜单 管理列表的分页数据
        public PagerInfo<WX_MenusView> GetWx_MenusList()
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            TempHql.AppendFormat(" order by Sort desc");
            PagerInfo<WX_MenusView> list = Search();
            return list;
        } 
        #endregion

        #region //
        public IList<WX_MenusView> Getallist()
        {
            string tempHql = string.Format("from WX_Menus order by Sort desc");
            try
            {
                List<WX_Menus> list = Session.CreateQuery(tempHql).List<WX_Menus>() as List<WX_Menus>;
                IList<WX_MenusView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region //微信公众号菜单树形菜单数据
        public string GetWX_MenusTreeData()
        {
            string HQL = "from WX_Menus";
            List<WX_Menus> list = HibernateTemplate.Find<WX_Menus>(HQL) as List<WX_Menus>;
            List<WX_MenusView> listView = GetViewlist(list) as List<WX_MenusView>;
            Base<WX_MenusView> _Base = new Base<WX_MenusView>();
            string str = _Base.AddNodelayui(listView, "Id", "M_ParentID", null, "M_Name",true, 1);
            return str;
        }
        #endregion

    }
}
