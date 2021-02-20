using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface  INACustomerinfoDao:IBaseDao<NACustomerinfoView>
    {
        PagerInfo<NACustomerinfoView> GetCinfoList(string Name, string lxrname, string isjxs,string IsIsjy,string Tel,string Isds, SessionUser user);

        #region //查询客户信息
        IList<NACustomerinfoView> GetlistCust(); 
        #endregion

        #region //检测是否有 重复的客户信息
        bool Jccfkhbykhname(string Khname); 
        #endregion

        #region //根据客户公司名称和收货人查找客户信息
        NACustomerinfoView GetKHinfobykhname(string khname, string lxrname); 
        #endregion

        #region //保存后返回ID
        string InsertID(NACustomerinfoView modelView); 
        #endregion

         #region //根据库户公司名称查找客户信息
        NACustomerinfoView GetKHinfobyname(string name); 
        #endregion

        #region //查找物联网经销商
        IList<NACustomerinfoView> GetCustinfoisjxs(string type); 
        #endregion

        #region //根据省份ID查询该省份下的经销商
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfId">省份ID</param>
        /// <param name="type">1 物联网经销商  2 只参加买一送一</param>
        /// <returns></returns>
        IList<NACustomerinfoView> GetKhinfobysfIf(string sfId, string type); 
        #endregion


        NACustomerinfoView GetCusModelbyDsUid(string Uid);

      
		  #region //微信公众号中上线收益情况统计
        string GetCusNamebyOpenID(string openId);
	     #endregion

        
        #region //查询全部物联网经销商信息（电商平台下单的账号）
        IList<NACustomerinfoView> Getalljxqinfolist(); 
        #endregion
       /// <summary>
       /// 模糊查询更加公司名称查询
       /// </summary>
       /// <param name="name">公司名称</param>
       /// <returns></returns>
       IList<NACustomerinfoView> Getcusinfolikename(string name);

          //根据Id查找客户信息
       NACustomerinfoView GetcusInfobyId(string Id);

       #region //通过电商用户UID查找客户资料
       /// <summary>
       /// 通过电商用户UID查找客户资料
       /// </summary>
       /// <param name="UId">电商用户Id</param>
       /// <returns></returns>
       NACustomerinfoView GetCustomerbyUId(string UId); 
       #endregion

       #region //物联网经销权的客户分页列表
       /// <summary>
       /// 物联网经销权的客户分页列表
       /// </summary>
       /// <param name="name">公司名称</param>
       /// <param name="lxrname">联系人</param>
       /// <returns></returns>
       PagerInfo<NACustomerinfoView> GetJXQcinfoList(string name, string lxrname); 
       #endregion

       #region //根据公司名称模糊查询客户信息
       NACustomerinfoView GetCusinfobylikekhname(string khname); 
       #endregion

       #region //根据经销商全名查询电商同步的客户信息
       /// <summary>
       /// 根据经销商全名查询电商同步的客户信息
       /// </summary>
       /// <param name="khname">经销商名称</param>
       /// <returns></returns>
       NACustomerinfoView GetcusinfobykhnameandIsds(string khname); 
       #endregion

    
       #region //按照时间顺序查找全部的客户信息
       /// <summary>
       /// 按照时间顺序查找全部的客户信息
       /// </summary>
       /// <returns></returns>
       IList<NACustomerinfoView> GetAllCusinfoordertime(); 
       #endregion
    }
}
