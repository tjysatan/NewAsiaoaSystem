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
    public class SysWebApiController : ApiController
    {
        ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
        ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
        ISysDepartmentDao _ISysDepartmentDao = ContextRegistry.GetContext().GetObject("SysDepartmentDao") as ISysDepartmentDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IDisImmuneCenterDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("DisImmuneCenterDao") as IDisImmuneCenterDao;
        //public string GetProductsByCategory()


        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysMenu([FromBody]string MenuId)
        {
            ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
            if (string.IsNullOrEmpty(MenuId))
            {
                return _ISysMenuDao.GetMenuTreeData();
            }

            else
            {
                return _ISysMenuDao.GetMenuButtonTreeData(MenuId);
            }

        }




        /// <summary>
        /// 获取功能权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysFunc()
        {
            ISysFunctionDao _ISysFunctionDao = ContextRegistry.GetContext().GetObject("SysFunctionDao") as ISysFunctionDao;
            return _ISysFunctionDao.NGetListJson();
        }

        /// <summary>
        /// 获取授权代码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysAuthorize()
        {
            ISysAuthorizeDao _ISysAuthorizeDao = ContextRegistry.GetContext().GetObject("SysAuthorizeDao") as ISysAuthorizeDao;
            return _ISysAuthorizeDao.GetSysAuthorizeTreeData();
        }

        /// <summary>
        /// 获取角色菜单字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysRoleMenuField([FromBody]string MenuId)
        {
            ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
            return _ISysMenuDao.NGetListJsonTree_id(MenuId);
        }

        /// <summary>
        /// 获取部门的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysDepartment()
        {
            return _ISysDepartmentDao.GetDepTreeData();
        }
        /// <summary>
        /// 获取角色的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysRole()
        {
            return _ISysRoleDao.GetRoleTreeData();
        }

       
        /// <summary>
        /// 获取菜单列表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetDatelistMenu()
        {
            return _ISysMenuDao.GetDatelistMenu();
        }

        /// <summary>
        /// 保存角色具有的权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveSysRoleAuth(SysRoleView data)
        {
            ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
            bool flay = _ISysRoleDao.SaveSysRoleAuth(data);
            if (flay)
                return "success";
            else
                return "error";
        }

        /// <summary>
        /// 获取角色具有的权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetSelectedAuth([FromBody] string RoleId)
        {
            ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
            return _ISysRoleDao.GetSelectedAuth(RoleId);
        }


       
        /// <summary>
        /// 保存角色所具有的用户成员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetPersonData([FromBody]string RoleId)
        {
            ISysPersonDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            return _ISysRoleDao.NGetListJson(RoleId);
        }

        /// <summary>
        /// 各年流动儿童统计（按年份分类汇总）
        /// </summary>
        /// <param name="StartDate">开始年份</param>
        /// <param name="EndDate">结束年份</param>
        /// <param name="Age">年龄段(0 表示0-7岁儿童，1表示7-16岁儿童)</param>
        /// <returns></returns>
        //[HttpPost]
        //public string GNLD_Statistics(GNLD_StatisticsView view)
        //{
        //    IGNLD_StatisticsDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("GNLD_StatisticsDao") as IGNLD_StatisticsDao;
        //    string json = _IDisImmuneCenterDao.GNLD_Statistics(view.StartDate, view.EndDate, view.Age);
        //    return json;
        //}


        #region //查找全部免疫点经纬度
        [HttpPost]
        public string GetAllMY()
        {
            string josndata = JsonConvert.SerializeObject(_IDisImmuneCenterDao.NGetList());
            return josndata;

        } 
        #endregion


         [HttpPost]
         public string GetPersonName_byId([FromBody]string Id)
        {
            string json = _ISysPersonDao.BDGetpersonbyID(Id);
            return json;
        }

         [HttpPost]
         public string GetIcNameby_Id([FromBody]string Id)
         {
             SysPerson pmodel = _ISysPersonDao.GetDModelbyId(Id);//通过用户ID查找用户信息
             DisImmuneCenterView icmodel = new DisImmuneCenterView();
            string json;
            if (pmodel != null && pmodel.DisImmuneCenter != null && pmodel.DisImmuneCenter!="")
             {
                 icmodel = _IDisImmuneCenterDao.NGetModelById(pmodel.DisImmuneCenter);//通过免疫点ID查找免疫点信息
                 json = JsonConvert.SerializeObject(icmodel);
             }
             else
             {
                 json = null;
             }
           
            return json;
         }

        #region //根据Id查找用户信息
         [HttpPost]
        public string GetPersonjson_Id([FromBody] string Id)
        {
            SysPerson pmodel = _ISysPersonDao.GetDModelbyId(Id);//通过用户ID查找用户信息
            string json;
            if (pmodel != null)
            {
                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion
    }
}
