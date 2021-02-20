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
    public class Vote_TitleController : Controller
    {
        //
        // GET: /Vote_Title/
        IVote_TitleDao _IVote_TitleDao = ContextRegistry.GetContext().GetObject("Vote_TitleDao") as IVote_TitleDao;
        IVote_SubjectDao _IVote_SubjectDao = ContextRegistry.GetContext().GetObject("Vote_SubjectDao") as IVote_SubjectDao;
        IVote_QuestionDao _IVote_QuestionDao = ContextRegistry.GetContext().GetObject("Vote_QuestionDao") as IVote_QuestionDao;
        public ActionResult Index(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IVote_TitleDao.SetPagerPageIndex(CurrentPageIndex);
            _IVote_TitleDao.SetPagerPageSize(15);
            PagerInfo<Vote_TitleView> listmodel = _IVote_TitleDao.Search();
            return View(listmodel);
        }

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Vote_TitleView model, FormCollection from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.S_Id = Request.Form["S_ListData"];
                    model.T_time = DateTime.Now;
                    model.T_Person = user.UserName;
                    model.T_Acount = 0;
                  //  flay = _IVote_TitleDao.Ninsert(model);
                    string T_Id = _IVote_TitleDao.InsertID(model);
                    if (T_Id != "" && T_Id != null)
                    {
                        flay = SavaQ(T_Id, Request.Form["S_ListData"], from["Q_Name"]);
                    }
                }
                //修改
                else
                {
                    model.S_Id = Request.Form["S_ListData"];
                    model.Id = model.Id;
                    flay = DeleteQ(from["Q_id"]);//先删除该标题下原先的选项
                    if (flay == true)
                    {
                        flay=_IVote_TitleDao.NUpdate(model);
                        if (flay==true)
                        {
                            flay = SavaQ(model.Id, Request.Form["S_ListData"], from["Q_Name"]);
                        }
                        else
                        {
                            flay = false;
                        }

                    }
                    else
                    {
                        flay = false;
                    }
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
            return View("Edit", new Vote_TitleView());
        }
        #endregion

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            Vote_TitleView sys = new Vote_TitleView();
            if (!string.IsNullOrEmpty(id))
                sys =_IVote_TitleDao.NGetModelById(id);
         
            AlbumDropdown(sys.S_Id);
            return View("Edit", sys);
        }


        public ActionResult Delete(string id)
        {
            bool i = false;
            List<Vote_Title> sys = _IVote_TitleDao.NGetListID(id) as List<Vote_Title>;
            if (null != sys)
            {
                i = _IVote_TitleDao.VotedataDelete(sys);
            }

            return RedirectToAction("Index");
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


        public string[] Q_Name(string name)
        {
            string str = name;
            str = str.TrimEnd('|');
            string[] strArr = str.Split('|');
            return strArr;
        }

        /// <summary>
        /// 保存题目下面的选项
        /// </summary>
        /// <param name="T_ID">标题Id</param>
        /// <param name="S_Id">主题Id</param>
        /// <param name="name">选项名称字符</param>
        /// <returns></returns>
        public bool SavaQ(string T_ID, string S_Id,string name)
        {
            bool fay = false;
            if (T_ID != "" && T_ID != null)
            {
                string Qname = name;
                string[] str = Q_Name(Qname);
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                for (int i = 0, j = str.Length; i < j; i++)
                {
                    Vote_QuestionView Qmodel = new Vote_QuestionView();
                    Qmodel.T_Id = T_ID;
                    Qmodel.S_Id = S_Id;
                    Qmodel.Q_Count = 0;
                    Qmodel.Q_Question = str[i];
                    Qmodel.Q_Person = user.UserName;
                    Qmodel.Q_time = DateTime.Now;
                    Qmodel.Q_Order = i;
                    fay = _IVote_QuestionDao.Ninsert(Qmodel);
                }

            }
            return fay;
        }

        /// <summary>
        /// 删除标题下的选项
        /// </summary>
        /// <param name="id">一个或者多个选项ID</param>
        /// <returns></returns>
        public bool DeleteQ(string id)
        {
            bool i = false;
            List<Vote_QuestionView> sys = _IVote_QuestionDao.NGetList_id(id) as List<Vote_QuestionView>;
            if (null != sys)
            {
                i = _IVote_QuestionDao.NDelete(sys);
            }
            return i;
        } 
 


    }
}
