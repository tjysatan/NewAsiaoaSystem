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
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.Dao;
using System.IO;


namespace NewAsiaOASystem.Web.Controllers
{
    public class Vote_QuestionController : Controller
    {
        //
        // GET: /Vote_Question/
        IVote_QuestionDao _IVote_QuestionDao = ContextRegistry.GetContext().GetObject("Vote_QuestionDao") as IVote_QuestionDao;
        IVote_TitleDao _IVote_TitleDao = ContextRegistry.GetContext().GetObject("Vote_TitleDao") as IVote_TitleDao;
        IVote_SubjectDao _IVote_SubjectDao = ContextRegistry.GetContext().GetObject("Vote_SubjectDao") as IVote_SubjectDao;
        IVote_ConfigDao _IVote_ConfigDao = ContextRegistry.GetContext().GetObject("Vote_ConfigDao") as IVote_ConfigDao;

        public ActionResult Index(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IVote_QuestionDao.SetPagerPageIndex(CurrentPageIndex);
            _IVote_QuestionDao.SetPagerPageSize(15);
            PagerInfo<Vote_QuestionView> listmodel = _IVote_QuestionDao.Search();
            return View(listmodel);
        }


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Vote_QuestionView model,FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.S_Id))
                {
                    model.Q_time = DateTime.Now;
                    model.Q_Person = Convert.ToString(user.UserName);
                    model.S_Id = Request.Form["S_ListData"];
                    model.T_Id = Request.Form["aa"];
                    flay = _IVote_QuestionDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.S_Id = model.S_Id;
                    flay = _IVote_QuestionDao.NUpdate(model);
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

        #region //添加跳转页面
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("Edit", new Vote_QuestionView());
        }
        #endregion

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            Vote_QuestionView sys = new Vote_QuestionView();
            if (!string.IsNullOrEmpty(id))
                sys = _IVote_QuestionDao.NGetModelById(id);
            AlbumDropdown(sys.S_Id);
            return View("Edit", sys);
        }

        public void AlbumDropdown(string SelectedPID)
        {
            //获取所有主题列表
            List<Vote_SubjectView> SViewList = _IVote_SubjectDao.NGetList() as List<Vote_SubjectView>;
            List<GetMenuList> MenuList = new List<GetMenuList>();
            GetMenuList menu = new GetMenuList();
            menu.Name = "请选择";
            MenuList.Add(menu);
            if (SViewList != null)
            {
                foreach (var item in SViewList)
                {
                    menu = new GetMenuList();
                    menu.Id = item.S_Id;
                    menu.Name = item.S_title;
                    MenuList.Add(menu);
                }
            }
            if (SelectedPID != null)
                ViewData["SList"] = new SelectList(MenuList, "Id", "Name", SelectedPID);
            else
                ViewData["SList"] = new SelectList(MenuList, "Id", "Name");
        }

        public ActionResult Vote_TitleName(string id)
        {
            IList<Vote_TitleView> list = _IVote_TitleDao.GetVotetitleby_sid(id);
            if (Request.IsAjaxRequest())
            {
                return Json(list, JsonRequestBehavior.AllowGet);  //JSON传输
            }
            else
            {
                return Json("");
            }
        }



     


    }
}
