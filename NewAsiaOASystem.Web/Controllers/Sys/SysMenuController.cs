using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;


namespace NewAsiaOASystem.Web.Controllers
{
    public class SysMenuController : Controller
    {


        ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;

        #region 获取数据列表
        public ActionResult Index(int? pageIndex)
        {
            PagerInfo<SysMenuView> listmodel = GetListPager(pageIndex, null);
            return View("SysMenuGrid",listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<SysMenuView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">社区名称</param>
        /// <param name="CommID">所属区域ID</param>
        /// <returns></returns>
        private PagerInfo<SysMenuView> GetListPager(int? pageIndex, string Name)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysMenuDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysMenuDao.SetPagerPageSize(11);
            PagerInfo<SysMenuView> listmodel = _ISysMenuDao.GetPageList(Name);
            return listmodel;
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public SysMenuView GetModelById(string ID)
        {
            return _ISysMenuDao.NGetModelById(ID);
        }
        #endregion

        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysMenuView model)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                bool flay = false;
                model.Url = Request.Form["ActionData"];

                if (Request.Form["ActionData"] == "" || Request.Form["Name"] == "")
                {
                    return Json(new { result = "MenuNameerror" }, "text/html");
                }



                model.PId = Request.Form["MenuListData"];
                if (model.PId == null)
                    model.PId = "";
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = user.UserName;
                    flay = _ISysMenuDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = model.Id;
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = user.UserName;
                    flay = _ISysMenuDao.NUpdate(model);
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

        #endregion

        public ActionResult Delete(string id)
        {
            bool i = false;
            List<SysMenuView> sys = _ISysMenuDao.NGetList_id(id) as List<SysMenuView>;
            if (null != sys)
            {
                i = _ISysMenuDao.NDelete(sys);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            AlbumDropdown(null,null);
            return View("SysMenuEdit", new SysMenuView());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            SysMenuView sys = new SysMenuView();
            if (!string.IsNullOrEmpty(id))
                sys = GetModelById(id);
            //根据上级ID获取到上级记录
            if (!string.IsNullOrEmpty(sys.PId))
            {
                SysMenuView Sys1 = GetModelById(sys.PId);
                ViewBag.result = Sys1.Name;
            }
            AlbumDropdown(sys.Url,sys.PId);
            return View("SysMenuEdit", sys);
        }


        #region 初始化设置下拉框值
       /// <summary>
        /// 初始化设置下拉框值
       /// </summary>
       /// <param name="SelectedUrlID">设置默认显示的路径</param>
       /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedUrlID, string SelectedPID)
        {
            //获取菜单路径
            string url = "";
            GetControllerInfo ControllerInfo = new GetControllerInfo();
            List<GetControllerInfoView> controller = ControllerInfo.GetFromControllerInfo();
            List<GetControllerInfoView> controllerUrl = new List<GetControllerInfoView>();
            GetControllerInfoView ControllerInfoView = new GetControllerInfoView();
            ControllerInfoView.ControllerName = "//";
            ControllerInfoView.ActionName = "请选择";
            controllerUrl.Add(ControllerInfoView);
            foreach (var Conitem in controller)
            {
                url = "/" + Conitem.ControllerName + "/" + Conitem.ActionName;
                ControllerInfoView = new GetControllerInfoView();
                ControllerInfoView.ControllerName = url;
                ControllerInfoView.ActionName = url;
                controllerUrl.Add(ControllerInfoView);
            }
            if (SelectedUrlID != null)
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName", SelectedUrlID);
            else
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName");

            //获取所有菜单列表
          List<SysMenuView> MenuViewList=  _ISysMenuDao.NGetList() as List<SysMenuView>;
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
                  menu.Name = item.Name;
                  MenuList.Add(menu);
              }
          }
          if (SelectedPID != null)
              ViewData["MenuList"] = new SelectList(MenuList, "Id", "Name", SelectedPID);
          else
              ViewData["MenuList"] = new SelectList(MenuList, "Id", "Name");
        }

        #endregion

    }
}
