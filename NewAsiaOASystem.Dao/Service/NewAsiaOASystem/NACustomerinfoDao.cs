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
    /// <summary>
    /// 客户业务处理
    /// tjy_satan
    /// </summary>
   public class NACustomerinfoDao:ServiceConversion<NACustomerinfoView,NACustomerinfo> ,INACustomerinfoDao
    {

        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NACustomerinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       public override NACustomerinfoView GetView(NACustomerinfo data)
        {
            NACustomerinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool Ninsert(NACustomerinfoView model)
        {
            NACustomerinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NUpdate(NACustomerinfoView model)
        {
            NACustomerinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NDelete(NACustomerinfoView model)
        {
            NACustomerinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NDelete(List<NACustomerinfoView> model)
        {
            IList<NACustomerinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
       public IList<NACustomerinfoView> NGetList()
        {
            List<NACustomerinfo> listdata = base.GetList() as List<NACustomerinfo>;
            IList<NACustomerinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public IList<NACustomerinfoView> NGetList_id(string id)
        {
            List<NACustomerinfo> list = base.GetList_id(id) as List<NACustomerinfo>;
            IList<NACustomerinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
       public IList<NACustomerinfo> NGetListID(string id)
        {
            IList<NACustomerinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public NACustomerinfoView NGetModelById(string id)
        {
            NACustomerinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


       public PagerInfo<NACustomerinfoView> GetCinfoList(string Name,string lxrname,string isjxs,string Isjy,string Tel,string Isds, SessionUser user)
       {
           TempList = new List<string>();
           TempHql = new StringBuilder();
           if (!string.IsNullOrEmpty(Name))
               TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
           if(!string.IsNullOrEmpty(lxrname))
               TempHql.AppendFormat(" and u.LxrName like '%{0}%' ", lxrname);
           if (!string.IsNullOrEmpty(isjxs))
               TempHql.AppendFormat(" and u.isjxs='{0}'", isjxs);
           if (!string.IsNullOrEmpty(Isjy))
               TempHql.AppendFormat(" and u.Status='{0}'",Isjy);
           if (!string.IsNullOrEmpty(Tel))
               TempHql.AppendFormat(" and u.Tel like '%{0}%'",Tel);
           if (!string.IsNullOrEmpty(Isds))
               TempHql.AppendFormat(" and u.Isds='{0}'",Isds);
           TempHql.AppendFormat(" order by u.dycs desc,CreateTime desc");
           PagerInfo<NACustomerinfoView> list = Search();
           return list;
       }
        #region //维护过K3CODE的客户信息
        /// <summary>
        /// 维护过K3CODE的客户信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="lxrname"></param>
        /// <param name="isjxs"></param>
        /// <param name="Isjy"></param>
        /// <param name="Tel"></param>
        /// <param name="Isds"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<NACustomerinfoView> GetCinfokecodeList(string Name, string lxrname, string isjxs, string Isjy, string Tel, string Isds, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
            if (!string.IsNullOrEmpty(lxrname))
                TempHql.AppendFormat(" and u.LxrName like '%{0}%' ", lxrname);
            if (!string.IsNullOrEmpty(isjxs))
                TempHql.AppendFormat(" and u.isjxs='{0}'", isjxs);
            if (!string.IsNullOrEmpty(Isjy))
                TempHql.AppendFormat(" and u.Status='{0}'", Isjy);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and u.Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(Isds))
                TempHql.AppendFormat(" and u.Isds='{0}'", Isds);
            TempHql.AppendFormat(" and K3CODE IS NOT NULL");
            TempHql.AppendFormat(" order by u.dycs desc,CreateTime desc");
            PagerInfo<NACustomerinfoView> list = Search();
            return list;
        } 
        #endregion

        #region //物联网经销权的客户分页列表
        /// <summary>
        /// 物联网经销权的客户分页列表
        /// </summary>
        /// <param name="name">公司名称</param>
        /// <param name="lxrname">联系人</param>
        /// <returns></returns>
        public PagerInfo<NACustomerinfoView> GetJXQcinfoList(string name, string lxrname)
       {
           TempList = new List<string>();
           TempHql = new StringBuilder();
           if (!string.IsNullOrEmpty(name))
               TempHql.AppendFormat(" and u.Name like '%{0}%'",name);
           if (!string.IsNullOrEmpty(lxrname))
               TempHql.AppendFormat("  and u.LxrName like '%{0}%'",lxrname);
           TempHql.AppendFormat(" and u.isjxs='1'");
           TempHql.AppendFormat(" order by CreateTime desc");
           PagerInfo<NACustomerinfoView> list = Search();
           return list;
       }
       #endregion


       #region //查询全部物联网经销商信息（电商平台下单的账号）
       public IList<NACustomerinfoView> Getalljxqinfolist()
       {
           string strslq = string.Format(" from NACustomerinfo where Isds='1'");
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(strslq) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           return listmodel;
       }
       #endregion
 
       #region //查询全部的客户信息
       public IList<NACustomerinfoView> GetlistCust()
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo order by Sort asc ") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           return listmodel;
       }
       #endregion

       #region //电商客户信息接口 检测是否有重复添加
       public bool Jccfkhbykhname(string Khname)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where Name = '" + Khname + "' ") as List<NACustomerinfo>;
           if (list != null&&list.Count!=0)
           {
               return false;//存在 重复的 返回false
           }
           else
           {
               return true;//不存在重复的 返回true
           }
       } 
       #endregion

       #region //根据UID 查找客户的信息
       public NACustomerinfoView GetCusModelbyDsUid(string Uid)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where DsUid = '" + Uid + "' ") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null && listmodel.Count != 0)
           {
               return listmodel[0];
           }
           else
           {
               return null;
           }
       } 
       #endregion

       #region //根据客户公司名称和收货人查找客户信息
       public NACustomerinfoView GetKHinfobykhname(string khname,string lxrname)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where Name = '" + khname + "' and LxrName='"+lxrname+"' ") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null && listmodel.Count != 0)
           {
               return listmodel[0];
           }
           else
           {
               return null;
           }
       } 
       #endregion

       #region //根据公司名称模糊查询客户信息
       public NACustomerinfoView GetCusinfobylikekhname(string khname)
       {
           string Hqlstr = string.Format("from NACustomerinfo where Name like '%{0}%' order by dycs desc", khname);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null)
               return listmodel[0];
           else
               return null;
       }
        #endregion

        #region //根据公司名称模糊查询客户信息K3CODE不为空
        public NACustomerinfoView GetCusinfok3codebylikekhname(string khname)
        {
            string Hqlstr = string.Format("from NACustomerinfo where Name like '%{0}%' and K3CODE is not null order by dycs desc", khname);
            List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
            IList<NACustomerinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据省份ID查询该省份下的经销商
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfId">省份ID</param>
        /// <param name="type">1 物联网经销商  2 只参加买一送一</param>
        /// <returns></returns>
        public IList<NACustomerinfoView> GetKhinfobysfIf(string sfId, string type)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where qyId = '" + sfId + "' and isjxs='" + type + "' ") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null && listmodel.Count != 0)
           {
               return listmodel;
           }
           else
           {
               return null;
           }
       }
       
       #endregion

       #region //根据库户公司名称查找客户信息
       public NACustomerinfoView GetKHinfobyname(string name)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where Name = '" + name + "'") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null && listmodel.Count != 0)
           {
               return listmodel[0];
           }
           else
           {
               return null;
           }
       } 

       //模糊查询更加公司名称查询
       /// <summary>
       /// 模糊查询更加公司名称查询
       /// </summary>
       /// <param name="name">公司名称</param>
       /// <returns></returns>
       public IList<NACustomerinfoView> Getcusinfolikename(string name)
       {
           string Hqlstr = string.Format("from NACustomerinfo where Name like '%{0}%'",name);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           return listmodel;
       }
       #endregion

       #region //通过电商用户UID查找客户资料
       /// <summary>
       /// 通过电商用户UID查找客户资料
       /// </summary>
       /// <param name="UId">电商用户Id</param>
       /// <returns></returns>
       public NACustomerinfoView GetCustomerbyUId(string UId)
       {
           string Hqlstr = string.Format(" from NACustomerinfo  where DsUid='{0}'", UId);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null)
               return listmodel[0];
           else
               return null;
       }
       #endregion

       //根据Id查找客户信息
       public NACustomerinfoView GetcusInfobyId(string Id)
       {
           string Hqlstr = string.Format("from NACustomerinfo where Id='{0}'", Id);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null)
               return listmodel[0];
           else
               return null;
       }

       #region //保存后返回ID
       public string InsertID(NACustomerinfoView modelView)
       {
           NACustomerinfo model = GetData(modelView);
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
   
       #region //查找物联网经销商
       /// <summary>
       /// 查找经销商有经销权的和只参加了买一送一经销权的
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public IList<NACustomerinfoView> GetCustinfoisjxs(string type)
       {
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo where isjxs ='"+type+"' order by CreateTime desc ") as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           return listmodel;
       } 
       #endregion

       #region //微信公众号中上线收益情况统计
       public string GetCusNamebyOpenID(string openId)
       {
           string sqltemp = string.Format("from NACustomerinfo where Id=(select Person_Id from WX_OpenID where OpenId='{0}')", openId);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(sqltemp) as List<NACustomerinfo>;
           if (list != null && list.Count != 0)
               return list[0].Name;
           else
               return null;
       } 
       #endregion

       #region //根据经销商全名查询电商同步的客户信息
       /// <summary>
       /// 根据经销商全名查询电商同步的客户信息
       /// </summary>
       /// <param name="khname">经销商名称</param>
       /// <returns></returns>
       public NACustomerinfoView GetcusinfobykhnameandIsds(string khname)
       {
           string Hqlstr = string.Format("from NACustomerinfo where Name='{0}' and Isds='1'", khname);
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           if (listmodel != null)
               return listmodel[0];
           else
               return null;
       }
      #endregion

        #region //按照时间顺序查找全部的客户信息
       /// <summary>
       /// 按照时间顺序查找全部的客户信息
       /// </summary>
       /// <returns></returns>
       public IList<NACustomerinfoView> GetAllCusinfoordertime()
       {
           string Hqlstr = string.Format("from NACustomerinfo where Status='1' order by CreateTime desc");
           List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
           IList<NACustomerinfoView> listmodel = GetViewlist(list);
           return listmodel;
       }
        #endregion

        #region //通过客户的编码查询客户信息
        /// <summary>
        /// 客户编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public NACustomerinfoView Getcusinfobycode(string code)
        {
            string Hqlstr = string.Format("from NACustomerinfo where K3CODE='{0}'", code);
            List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(Hqlstr) as List<NACustomerinfo>;
            IList<NACustomerinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

    }
}
