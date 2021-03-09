using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WxMenuController : Controller
    {
        public static readonly string EncodingAESKey = WebConfigurationManager.AppSettings["WeixinEncodingAESKey"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];
        //
        // GET: /WxMenu/
        IWX_MenusDao _IWX_MenusDao = ContextRegistry.GetContext().GetObject("WX_MenusDao") as IWX_MenusDao;
        public ActionResult Index()
        {
            //int CurrentPageIndex = Convert.ToInt32(pageIndex);
            //if (CurrentPageIndex == 0)
            //    CurrentPageIndex = 1;
            //_IWX_MenusDao.SetPagerPageIndex(CurrentPageIndex);
            //_IWX_MenusDao.SetPagerPageSize(20);
            //PagerInfo<WX_MenusView> listmodel = _IWX_MenusDao.GetWx_MenusList();
            return View();
        }

        #region //查询自定义菜单的树形结构数据
        [HttpPost]
        public string GetWX_MenusMenu()
        {
            return _IWX_MenusDao.GetWX_MenusTreeData();
        }
        #endregion

        /// <summary>
        /// 保存处理的方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(WX_MenusView model)
        {
            try
            {
               bool flay = false;
               model.M_ParentID=Request.Form["MenuListData"];
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.M_CreateTime = DateTime.Now;
                    model.M_CreateUser = Convert.ToString(Session["user"]);
                    flay = _IWX_MenusDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = model.Id;
                   // model.MsgType = "text";
                    flay = _IWX_MenusDao.NUpdate(model);
                }
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            catch
            {
                return Json(new { result = "error" });
            }
        }


        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("Edit", new WX_MenusView());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WX_MenusView sys = new WX_MenusView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWX_MenusDao.NGetModelById(id);
            //根据上级ID获取到上级记录
  
            AlbumDropdown(sys.M_ParentID);
            return View("Edit", sys);
        }

        public ActionResult Delete(string id)
        {
            bool i = false;
            List<WX_MenusView> sys = _IWX_MenusDao.NGetList_id(id) as List<WX_MenusView>;
            if (null != sys)
            {
                i = _IWX_MenusDao.NDelete(sys);
            }

            return RedirectToAction("Index");
        }

        public void AlbumDropdown(string SelectedPID)
        {
            ///获取全部的菜单
            List<WX_MenusView> MenuViewList = _IWX_MenusDao.wx_Geteonemenu() as List<WX_MenusView>;
            List<GetMenuList> MenuList = new List<GetMenuList>();
            GetMenuList menu = new GetMenuList();
              menu.Name = "请选择";
          MenuList.Add(menu);
          if (MenuViewList != null)
          {
              foreach (var item in MenuViewList)
              {
                  menu = new GetMenuList();
                  menu.Id = item.Id;
                  menu.Name = item.M_Name;
                  MenuList.Add(menu);
              }
          }
          if (SelectedPID != null)
              ViewData["MenuList"] = new SelectList(MenuList, "Id", "Name", SelectedPID);
          else
              ViewData["MenuList"] = new SelectList(MenuList, "Id", "Name");
        }

        #region //根据Id查询实体数据
        public string GetmodelbyId(string Id)
        {
            return JsonConvert.SerializeObject(_IWX_MenusDao.NGetModelById(Id));
        }
        #endregion

        /// <summary>
        /// 生成自定义菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWx_Menu()
         {
            //删除自定义菜单
            string D = MenuManager.DeleteMenu();
            if (D.Contains("ok"))
            {
                //创建自定义菜单
                string C = MenuManager.CreateMenu(MenuManager.LoadMenu());
                if (C.Contains("ok"))
                {
                    string I = MenuManager.GetMenu();
                    if (I != null)
                    {
                        return Json(new { result = "success" });
                    }
                    else
                    {
                        return Json(new { result = "error" });
                    }
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }
            else
            {
                return Json(new { result = "error" });
            }
        }

        public JsonResult DeleteWx_Menu()
        {
          string I=  MenuManager.DeleteMenu();
          if (I.Contains("ok"))
          {
              return Json(new { result = "success" });
          }
          else
          {
              return Json(new { result = "error" });
          }
        }

        public JsonResult CreateWx_Menu()
        {
            string I = MenuManager.CreateMenu(MenuManager.LoadMenu());
            if (I.Contains("ok"))
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }

        }

    }

