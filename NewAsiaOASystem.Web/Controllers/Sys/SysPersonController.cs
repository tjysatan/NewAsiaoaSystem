using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.Web;
using Spring.Context.Support;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class SysPersonController : Controller
    {

        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
        ISysDepartmentDao _ISysDepartmentDao = ContextRegistry.GetContext().GetObject("SysDepartmentDao") as ISysDepartmentDao;

        #region 获取数据列表
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<SysPersonView> listmodel = GetRoleListPager(pageIndex, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            //账号名
            string LoginName = Request.Form["txtSearch_Login"];
            //用户姓名
            string UserName = Request.Form["txtSearch_UserName"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<SysPersonView> listmodel = GetRoleListPager(pageIndex, LoginName, UserName);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="LoginName">账号</param>
        /// <param name="Name">姓名</param>
        /// <returns></returns>
        private PagerInfo<SysPersonView> GetRoleListPager(int? pageIndex, string LoginName, string Name)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysPersonDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysPersonDao.SetPagerPageSize(11);
            PagerInfo<SysPersonView> listmodel = _ISysPersonDao.GetPersonData(LoginName, Name, user);
            return listmodel;
        }

        #endregion

        public string GetALLDepartment()
        {

            return _ISysDepartmentDao.GetDepTreeData();
        }

        public JsonResult Edit(SysPersonView model, FormCollection from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //操作是否成功
                bool flay = false;


                if (Request.Form["Name"] == "" || Request.Form["UserName"] == "" || Request.Form["SelectComm"] == "" || Request.Form["ADListData"] == "请选择" || Request.Form["Password"] == "" || Request.Form["RePassword"] == "")
                {
                    return Json(new { result = "error" }, "text/html");
                }



                model.DisImmuneCenter = Request.Form["ADListData"];
                model.Role = Request.Form["SelectComm"];//获取选中的角色
                model.Sort = 0;
                //判断Id是否存在
                if (string.IsNullOrEmpty(model.Id))
                {
                    //检查用户名是否存在
                    if (_ISysPersonDao.check_repeat(model.Name) == false)
                    {
                        model.CreatePerson = user.UserName;
                        model.CreateTime = DateTime.Now;
                        model.Department = Convert.ToString(from["D_id"]);
                        //model.Role = Convert.ToString(from["R_id"]);         
                        flay = _ISysPersonDao.Ninsert(model);
                    }
                    else
                    {
                        return Json(new { result = "Repeat" }, "text/html");
                    }
                }
                else
                {
                    model.UpdatePerson = user.UserName;
                    model.UpdateTime = DateTime.Now;
                    model.Department = Convert.ToString(from["D_id"]);
                    //model.Role = Convert.ToString(from["R_id"]);       
                    flay = _ISysPersonDao.NUpdate(model);
                }
                if (flay)
                {
                    return Json(new { result = "success" }, "text/html");
                }

                else
                {
                    return Json(new { result = "error" }, "text/html");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }

        }
        /// <summary>
        /// 增加 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            AlbumUnidDropdown(null);
            ViewBag.PassWd = "";
            return View("Edit", new SysPersonView());
        }
        /// <summary>
        /// 修改跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            ViewBag.action = "EditPage";
            string id = Request.QueryString["id"].ToString();
            SysPersonView sys = _ISysPersonDao.NGetModelById(id);
            AlbumUnidDropdown(sys.DisImmuneCenter);
            ViewBag.PassWd = sys.Password;
            return View("Edit", sys);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                //操作是否成功 
                string id = Request.QueryString["id"].ToString();
                List<SysPersonView> sys = _ISysPersonDao.NGetList_id(id) as List<SysPersonView>;
                if (null != sys)
                {
                    if (_ISysPersonDao.NDelete(sys) == true)
                    {
                        return RedirectToAction("List");

                    }
                    else
                    {
                        return Content("<script>alert('删除失败');window.history.back();</script>");
                    }
                }
            }
            catch
            {
                return Content("<script>alert('删除失败');window.history.back();</script>");
            }
            return RedirectToAction("List");
        }


        /// <summary>
        /// 设置下拉框值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public void AlbumDropdown(string SelectedID)
        {
            string url = "";
            GetControllerInfo ControllerInfo = new GetControllerInfo();
            List<GetControllerInfoView> controller = ControllerInfo.GetFromControllerInfo();
            List<GetControllerInfoView> controllerUrl = new List<GetControllerInfoView>();

            foreach (var Conitem in controller)
            {
                url = "/" + Conitem.ControllerName + "/" + Conitem.ActionName;
                GetControllerInfoView ControllerInfoView = new GetControllerInfoView();
                ControllerInfoView.ControllerName = url;
                ControllerInfoView.ActionName = url;
                controllerUrl.Add(ControllerInfoView);
            }
            if (SelectedID != null)
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName", SelectedID);
            else
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName");
        }

        /// <summary>
        /// 获取单位下拉框值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public void AlbumUnidDropdown(string SelectedID)
        {
            IDisImmuneCenterDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("DisImmuneCenterDao") as IDisImmuneCenterDao;
            List<DisImmuneCenterView> UnitList = new List<DisImmuneCenterView>();
            if (UnitList != null)
            {
                DisImmuneCenterView Imm = new DisImmuneCenterView();
                Imm.Name = "请选择";
                Imm.Id = "请选择";
                UnitList.Add(Imm);
                Imm = new DisImmuneCenterView();
                List<DisImmuneCenterView> getUnitList = _IDisImmuneCenterDao.NGetList() as List<DisImmuneCenterView>;
                if (getUnitList != null)
                    UnitList.AddRange(getUnitList);

                if (SelectedID != null)
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name", SelectedID);
                else
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name");
            }
        }

        /// <summary>
        /// 设置角色下拉框值(编辑页面时)
        /// </summary>
        /// <param name="personId">需要选中的Value值</param>
        public JsonResult RoleAlbumDropdown()
        {
            string personId = Request.Form["personId"];
            string json = _ISysPersonDao.RoleAlbumDropdown(personId);
            return Json(new { result = json }, "text/html");
        }

        /// <summary>
        /// 跳转到密码修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GoPasswordPage()
        {
            return View("EditPwd");
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdatePassword(string OldPassword, string NewPassword)
        {
            SessionUser session = Session[SessionHelper.User] as SessionUser;
            string jsonStr= _ISysPersonDao.UpdatePassword(session.UserName, OldPassword, NewPassword);
            return Json(new { result = jsonStr });
        }



    }
}
