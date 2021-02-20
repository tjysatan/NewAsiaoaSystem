using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_BlinfoController : Controller
    {
         //不良原因
        // GET: /NQ_Blinfo/
        INQ_BlinfoDao _INQ_BlinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlinfoDao") as INQ_BlinfoDao;

        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_BlinfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_BlinfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        } 
        #endregion

        #region //获取数据列表
        private PagerInfo<NQ_BlinfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_BlinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_BlinfoDao.SetPagerPageSize(11);
            PagerInfo<NQ_BlinfoView> listmodel = _INQ_BlinfoDao.GetCinfoList(Name, Suser);
            return listmodel;
        } 
        #endregion

        #region //增加跳转页面
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("Edit", new NQ_BlinfoView());
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_BlinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model.ParentId = Request.Form["ReasonListData"];
                if (model.ParentId == null || model.ParentId == "")
                    model.ParentId = "0";
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;
                    model.CreatePerson = Convert.ToString(user.Id);
                    model.CreateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_BlinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;
                    model.UpdatePerson = Convert.ToString(user.Id);
                    model.UpdateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_BlinfoDao.NUpdate(model);
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

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_BlinfoView sys = new NQ_BlinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_BlinfoDao.NGetModelById(id);
            AlbumDropdown(sys.ParentId);
            return View("Edit", sys);
        }
        #endregion

        #region 初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedPID)
        {
            List<NQ_BlinfoView> ReasonlistView = _INQ_BlinfoDao.GetlistisPar() as List<NQ_BlinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (ReasonlistView != null)
            {
                foreach (var item in ReasonlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //查找全部的不良现象
        [HttpPost]
        public string GetAllParlist()
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlinfoDao.GetlistisPar());
            return json;
        } 
        #endregion

        #region //根据父级ID 查找不良原因
        [HttpPost]
        public string GetAllblyylist(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlinfoDao.Getlistreason_byPid(Id));
            return json;
        } 
        #endregion

        #region //根据ID 查找不良原因
        [HttpPost]
        public string GetblyymodelbyId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlinfoDao.NGetModelById(Id));
            return json;
        } 
        #endregion



    }
}
