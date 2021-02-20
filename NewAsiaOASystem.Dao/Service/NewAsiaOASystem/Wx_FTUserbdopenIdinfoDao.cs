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
    public class Wx_FTUserbdopenIdinfoDao:ServiceConversion<Wx_FTUserbdopenIdinfoView,Wx_FTUserbdopenIdinfo>,IWx_FTUserbdopenIdinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Wx_FTUserbdopenIdinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Wx_FTUserbdopenIdinfoView GetView(Wx_FTUserbdopenIdinfo data)
        {
            Wx_FTUserbdopenIdinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Wx_FTUserbdopenIdinfoView model)
        {
            Wx_FTUserbdopenIdinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Wx_FTUserbdopenIdinfoView model)
        {
            Wx_FTUserbdopenIdinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Wx_FTUserbdopenIdinfoView model)
        {
            Wx_FTUserbdopenIdinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Wx_FTUserbdopenIdinfoView> model)
        {
            IList<Wx_FTUserbdopenIdinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Wx_FTUserbdopenIdinfoView> NGetList()
        {
            List<Wx_FTUserbdopenIdinfo> listdata = base.GetList() as List<Wx_FTUserbdopenIdinfo>;
            IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Wx_FTUserbdopenIdinfoView> NGetList_id(string id)
        {
            List<Wx_FTUserbdopenIdinfo> list = base.GetList_id(id) as List<Wx_FTUserbdopenIdinfo>;
            IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Wx_FTUserbdopenIdinfo> NGetListID(string id)
        {
            IList<Wx_FTUserbdopenIdinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wx_FTUserbdopenIdinfoView NGetModelById(string id)
        {
            Wx_FTUserbdopenIdinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //根据OpenID 判断是否已经绑定
        public IList<Wx_FTUserbdopenIdinfoView> GetCount_byOpenId(string OpenId)
        {
            string tempHql = string.Format(" from  Wx_FTUserbdopenIdinfo  where  OpenId = '{0}'", OpenId);
            try
            {
                List<Wx_FTUserbdopenIdinfo> list = Session.CreateQuery(tempHql).List<Wx_FTUserbdopenIdinfo>() as List<Wx_FTUserbdopenIdinfo>;
                IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(list);
                return listmodel;

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region //根据OpenID 判断是否已经绑定
        public IList<Wx_FTUserbdopenIdinfoView> GetCount_byUserId(string UserId)
        {
            string tempHql = string.Format(" from  Wx_FTUserbdopenIdinfo  where  UserId = '{0}'", UserId);
            try
            {
                List<Wx_FTUserbdopenIdinfo> list = Session.CreateQuery(tempHql).List<Wx_FTUserbdopenIdinfo>() as List<Wx_FTUserbdopenIdinfo>;
                IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(list);
                return listmodel;

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //根据部门类型查找绑定的微信信息
        public IList<Wx_FTUserbdopenIdinfoView> GetwxftbmopenIdbybmtype(string bmtype)
        {
            string HQLstr = string.Format("from Wx_FTUserbdopenIdinfo where Bmtype in ({0}) ", bmtype);
            List<Wx_FTUserbdopenIdinfo> list = HibernateTemplate.Find<Wx_FTUserbdopenIdinfo>(HQLstr) as List<Wx_FTUserbdopenIdinfo>;
            IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion


        #region //根据绑定的用户Id查找绑定的微信信息
        /// <summary>
        /// 根据绑定的用户Id查找绑定的微信信息
        /// </summary>
        /// <param name="zhId">账户Id</param>
        /// <returns></returns>
        public IList<Wx_FTUserbdopenIdinfoView> GetwxopenIdbybdzhuserId(string zhId)
        {
            string HQLstr = string.Format("from Wx_FTUserbdopenIdinfo where UserId in('{0}')",zhId);
            List<Wx_FTUserbdopenIdinfo> list = HibernateTemplate.Find<Wx_FTUserbdopenIdinfo>(HQLstr) as List<Wx_FTUserbdopenIdinfo>;
            IList<Wx_FTUserbdopenIdinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


        #region //分页数据
        /// <summary>
        /// 微信绑定的的分页数据
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="type">部门类型 1 客服主管 2 电控维修 3 温控维修 4 配件维修 5 品保经理 6 电气工程师 7 生产主管 8 客服  9 采购 10 箱体确认 11 其他物料 12 仓库</param>
        /// <returns></returns>
        public PagerInfo<Wx_FTUserbdopenIdinfoView> GetwxbdinfoPagerList(string user,string type)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(user))
                TempHql.AppendFormat(" and u.UserId in (select Id from SysPerson where Name like '%{0}%' )",user);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and u.Bmtype='{0}'",type);
            TempHql.AppendFormat("order by C_time desc");
            PagerInfo<Wx_FTUserbdopenIdinfoView> list = Search();
            return list;

        }
        #endregion

        //根据采购单查找采购明细
        //public IList<Wx_FTUserbdopenIdinfoView> Getcgmxlist(string id)
        //{
        //    List<Wx_FTUserbdopenIdinfo> list = HibernateTemplate.Find<CG_Detailinfo>("from CG_Detailinfo where cgId='" + id + "'") as List<CG_Detailinfo>;
        //    IList<CG_DetailinfoView> listmodel = GetViewlist(list);
        //    return listmodel;
        //}
    }
}
