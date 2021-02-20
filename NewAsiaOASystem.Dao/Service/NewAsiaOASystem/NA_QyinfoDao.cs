using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using NHibernate;
using Newtonsoft.Json;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public class NA_QyinfoDao:ServiceConversion<NA_QyinfoView,NA_Qyinfo>,INA_QyinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_Qyinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_QyinfoView GetView(NA_Qyinfo data)
        {
            NA_QyinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_QyinfoView model)
        {
            NA_Qyinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_QyinfoView model)
        {
            NA_Qyinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_QyinfoView model)
        {
            NA_Qyinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_QyinfoView> model)
        {
            IList<NA_Qyinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_QyinfoView> NGetList()
        {
            List<NA_Qyinfo> listdata = base.GetList() as List<NA_Qyinfo>;
            IList<NA_QyinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_QyinfoView> NGetList_id(string id)
        {
            List<NA_Qyinfo> list = base.GetList_id(id) as List<NA_Qyinfo>;
            IList<NA_QyinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_Qyinfo> NGetListID(string id)
        {
            IList<NA_Qyinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_QyinfoView NGetModelById(string id)
        {
            NA_QyinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region // 区域管理 分页列表数据 +GetQyinfoList（）
        /// <summary>
        /// 区域管理 分页列表数据
        /// </summary>
        /// <param name="Name">区域名称</param>
        /// <param name="user">当前登录用户</param>
        /// <returns></returns>
        public PagerInfo<NA_QyinfoView> GetQyinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Qyname like '%{0}%'", Name);
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by Sort desc");
            PagerInfo<NA_QyinfoView> list = Search();
            return list;
        } 
        #endregion


        #region //查找父级区域
        public IList<NA_QyinfoView> GetlistbyPqy()
        {
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>("from NA_Qyinfo where Pid ='' order by Sort desc ") as List<NA_Qyinfo>;
            IList<NA_QyinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据父级区域Id 查找该区域下面所有的区域信息
        public IList<NA_QyinfoView> GetlistCqybypid(string Pid)
        {
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>("from NA_Qyinfo where Pid ='" + Pid + "' order by Sort desc ") as List<NA_Qyinfo>;
            IList<NA_QyinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //区域树形菜单数据
        public string GetQYTreeData()
        {
            string HQL = "from NA_Qyinfo where Qy_type is not null order by Sort desc ";
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>(HQL) as List<NA_Qyinfo>;
            List<NA_QyinfoView> listView = GetViewlist(list) as List<NA_QyinfoView>;
            Base<NA_QyinfoView> _Base = new Base<NA_QyinfoView>();
            string str = _Base.AddNodelayui(listView, "Id", "Pid", null, "Qyname",false, 1);
            return str;
        } 
        #endregion

        #region //根据省份名称或者市名称查询
        /// <summary>
        /// 根据区域名称查询Id
        /// </summary>
        /// <param name="username">区域名称</param>
        /// <returns></returns>
        public string GetqyinfoIdbyname(string username)
        {
            string hqlstr = string.Format("from NA_Qyinfo where Qyname like '{0}%'",username);
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>(hqlstr) as List<NA_Qyinfo>;
            if (list != null)
                return list[0].Id;
            return null;
        }
        #endregion

        #region //查询全部的地级市的名称数据
        public IList<NA_QyinfoView> Gettemporarydata()
        {
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>("from NA_Qyinfo where   Qy_type='1'") as List<NA_Qyinfo>;
            IList<NA_QyinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
