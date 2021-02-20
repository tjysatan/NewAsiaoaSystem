using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.Web.WebAPI
{
    public class DisWebApiController : ApiController
    {

        /// <summary>
        /// 获取流动儿童数据
        /// </summary>
        /// <param name="view">流动儿童数据参数</param>
        /// <returns></returns>
        //public string InsertFloatingChildService(DisNoFloatingChildView view)
        //{
        //    IDisFloatingChildDao _IDisFloatingChildDao = 
        //        ContextRegistry.GetContext().GetObject("DisFloatingChildDao") as IDisFloatingChildDao;
        //   return  _IDisFloatingChildDao.InsertFloatingChildService(view);
        //}
        #region //获取注销的流动儿童数据
        //public string UpdateFloatingChildService(DisNoFloatingChildView view)
        //{
        //    IDisFloatingChildDao _IDisFloatingChildDao =
        //                  ContextRegistry.GetContext().GetObject("DisFloatingChildDao") as IDisFloatingChildDao;
        //    return _IDisFloatingChildDao.UpdateFloatingChildService(view);
        //} 
        #endregion

        /// <summary>
        /// 获取没有联系方式的流动儿童
        /// </summary>
        /// <returns></returns>
        //public string SelectNoPhoneChildService()
        //{
        //    IDisFloatingChildDao _IDisFloatingChildDao =
        //        ContextRegistry.GetContext().GetObject("DisFloatingChildDao") as IDisFloatingChildDao;
        //    return _IDisFloatingChildDao.SelectNoPhoneChildService();
        //}


        /// <summary>
        /// 更新没有联系方式儿童的家属信息
        /// </summary>
        /// <returns></returns>
        //public string UpdateFamilyMembersService(DisFamilyMembersView Family)
        //{
        //    IDisFloatingChildDao _IDisFloatingChildDao =
        //       ContextRegistry.GetContext().GetObject("DisFloatingChildDao") as IDisFloatingChildDao;
        //    return _IDisFloatingChildDao.UpdateFamilyMembersService(Family);
        //}
    }
}
