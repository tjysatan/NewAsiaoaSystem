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
using NewAsiaOASystem.Web;

namespace NewAsiaOASystem.Web.Controllers
{
    public class SysRoleController : Controller
    {

        ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
        public ActionResult Index(int? pageIndex)
        {
            PagerInfo<SysRoleView> listmodel = GetRoleListPager(pageIndex, null);
            return View("SysRoleGrid", listmodel);
        }

        #region 获取数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public SysRoleView GetModelById(string ID)
        {
            return _ISysRoleDao.NGetModelById(ID);
        }
        #endregion

        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysRoleView model)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                bool flay = false;


                if (Request.Form["Name"] == "")
                {
                    return Json(new { result = "error" }, "text/html");
                }

                model.Pid = Request.Form["RoleListData"];
                string RoleTypeData = Request.Form["RoleTypeData"];
                if (string.IsNullOrEmpty(RoleTypeData))
                    model.RoleType = 2;
                else
                    model.RoleType = Convert.ToInt32(RoleTypeData);


                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = user.UserName;
                    flay = _ISysRoleDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = model.Id;
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = user.UserName;
                    flay = _ISysRoleDao.NUpdate(model);
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
            List<SysRoleView> sys = _ISysRoleDao.NGetList_id(id) as List<SysRoleView>;
            if (null != sys)
            {
                i = _ISysRoleDao.NDelete(sys);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            SetRoleTypeDropdown(null);
            return View("SysRoleEdit", new SysRoleView());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            SysRoleView sys = new SysRoleView();
            if (!string.IsNullOrEmpty(id))
                sys = GetModelById(id);
            AlbumDropdown(sys.Pid);
            SetRoleTypeDropdown(Convert.ToString(sys.RoleType));
            return View("SysRoleEdit", sys);
        }

        /// <summary>
        /// 跳转到角色设定窗口
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleManagePage(string id)
        {
            ViewBag.RoleId = id;
            return View("SysRoleManage");
        }

        /// <summary>
        /// 跳转到角色成员窗口
        /// </summary>
        /// <returns></returns>
        public ActionResult SysRoleMember(string id)
        {
            ViewBag.RoleId = id;
            return View("SysRoleMember");
        }

        /// <summary>
        /// 设置下拉框值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public void AlbumDropdown(string SelectedID)
        {
            List<SysRoleView> roleList = new List<SysRoleView>();

            if (roleList != null)
            {
                SysRoleView role = new SysRoleView();
                role.Name = "请选择";
                roleList.Add(role);
                List<SysRoleView> getRoleList = _ISysRoleDao.NGetList() as List<SysRoleView>;
                if (getRoleList != null)
                    roleList.AddRange(getRoleList);

                if (SelectedID != null)
                    ViewData["RoleList"] = new SelectList(roleList, "Id", "Name", SelectedID);
                else
                    ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            }
        }

        /// <summary>
        /// 设置角色类型
        /// 角色类型（0表示超级管理员，1表示普通管理员，2表示普通用户）
        /// 超级管理员可以查看所有数据，普通管理员只能查看本单位或部门数据，普通用户只能查看自己数据
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public void SetRoleTypeDropdown(string SelectedID)
        {
            List<SysRoleTypeView> roleList = new List<SysRoleTypeView>();

            if (roleList != null)
            {
                SysRoleTypeView role = new SysRoleTypeView();
                role.Name = "请选择";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "0";
                role.Name = "超级管理员";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "1";
                role.Name = "一般管理员";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "2";
                role.Name = "普通管理员";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "3";
                role.Name = "业务员";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "4";
                role.Name = "电气工程师";
                roleList.Add(role);

                role = new SysRoleTypeView();
                role.Id = "5";
                role.Name = "电气工程师主管";
                roleList.Add(role);

                if (SelectedID != null)
                    ViewData["RoleTypeList"] = new SelectList(roleList, "Id", "Name", SelectedID);
                else
                    ViewData["RoleTypeList"] = new SelectList(roleList, "Id", "Name");
            }
        }

        //按角色名称查询
        public JsonResult SearchRoleList()
        {
            string RoleName = Request.Form["txtSearch_RoleName"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<SysRoleView> listmodel = GetRoleListPager(pageIndex, RoleName);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            //ViewBag.PageNavigate = PageNavigate;
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        private PagerInfo<SysRoleView> GetRoleListPager(int? pageIndex, string RoleName)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysRoleDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysRoleDao.SetPagerPageSize(5);
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string LoginId = Convert.ToString(Suser.UserName);
            PagerInfo<SysRoleView> listmodel = _ISysRoleDao.GetRole(LoginId, RoleName);
            return listmodel;
        }

    }
}
