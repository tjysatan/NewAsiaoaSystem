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
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NA_QyinfoController : Controller
    {
        //
        // GET: /NA_Qyinfo/
        INA_QyinfoDao _INA_QyinfoDao = ContextRegistry.GetContext().GetObject("NA_QyinfoDao") as INA_QyinfoDao;

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List()
        {
            //PagerInfo<NA_QyinfoView> listmodel = GetListPager(pageIndex, null);
            return View();
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["Qyname"];//区域名称
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NA_QyinfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<NA_QyinfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INA_QyinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INA_QyinfoDao.SetPagerPageSize(11);
            PagerInfo<NA_QyinfoView> listmodel = _INA_QyinfoDao.GetQyinfoList(Name, Suser);
            return listmodel;
        }
        #endregion  
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NA_QyinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
               
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Qyname = model.Qyname;//区域名称
                    model.Pid = Request.Form["PQYListData"];
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//状态
                    model.Beizhu = model.Beizhu;//备注
                    model.C_time = DateTime.Now;//创建时间
                    model.C_name = user.Id;//创建人
                    flay = _INA_QyinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Qyname = model.Qyname;//区域名称
                    model.Pid = Request.Form["Pdid"];
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//状态
                    model.Beizhu = model.Beizhu;//备注
                    flay = _INA_QyinfoDao.NUpdate(model);
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

        #region //增加跳转页面
        public ActionResult addPage(string Pdid)
        {
            AlbumDropdown(Pdid);
            ViewData["Pdid"] = Pdid;
            return View("Edit", new NA_QyinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NA_QyinfoView sys = new NA_QyinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INA_QyinfoDao.NGetModelById(id);
            AlbumDropdown(sys.Pid);
            return View("Edit", sys);
        }
        #endregion

        #region //删除

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
                List<NA_QyinfoView> sys = _INA_QyinfoDao.NGetList_id(id) as List<NA_QyinfoView>;
                if (null != sys)
                {
                    if (_INA_QyinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NA_Qyinfo/List");
        }
        #endregion

        #region 初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedPID)
        {
            List<NA_QyinfoView> ReasonlistView = _INA_QyinfoDao.GetlistbyPqy() as List<NA_QyinfoView>;
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
                    Reasonmodel.Name = item.Qyname;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //区域树形菜单数据
        [HttpPost]
        public string GetqyMenu()
        {
            return _INA_QyinfoDao.GetQYTreeData();
        } 
        #endregion

        #region //查找所有父级区域 
        [HttpPost]
        public string GetPqyinfojson()
        {
            IList<NA_QyinfoView> plist = _INA_QyinfoDao.GetlistbyPqy();
            string json;
            if (plist != null)
            {
                json = JsonConvert.SerializeObject(plist);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //根据父级Id 查找该父级下面所有的区域
        [HttpPost]
        public string GetallCqybyPid(string Pid)
        {
            string json;
            IList<NA_QyinfoView> list = _INA_QyinfoDao.GetlistCqybypid(Pid);
            if (list != null)
            {
                json = JsonConvert.SerializeObject(list);
            }
            else
            {
                json = null;
            }
            return json;
        }
        #endregion

        #region //增加区域
        [HttpPost]
        public JsonResult Addqy(string Pid, string name,string qy_type)
        {
            try {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                NA_QyinfoView model = new NA_QyinfoView();
                model.Pid = Pid;
                model.Qyname = name;
                model.C_time = DateTime.Now;//创建时间
                if (qy_type != "")
                {
                    model.Qy_type = Convert.ToInt32(qy_type);
                }
                model.C_name = user.Id;//创建人
                model.Sort = "1";//排序
                flay = _INA_QyinfoDao.Ninsert(model);
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            } 
            catch {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //修改区域名称
        [HttpPost]
        public JsonResult updateqy(string id, string name)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                NA_QyinfoView model = _INA_QyinfoDao.NGetModelById(id);
                if (model != null)
                {
                    model.Qyname = name;
                    flay = _INA_QyinfoDao.NUpdate(model);
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

        #region //删除
        [HttpPost]
        public JsonResult Delqy(string id)
        {
            try
            {
                bool flay = false;
                NA_QyinfoView model = _INA_QyinfoDao.NGetModelById(id);
                if (null != model)
                {
                    if (model.Pid != "") {
                    List<NA_QyinfoView> sys = _INA_QyinfoDao.GetlistCqybypid(id) as List<NA_QyinfoView>;
                    if (sys!= null)
                    {
                        _INA_QyinfoDao.NDelete(sys);
                           
                        }
                   
                    flay = _INA_QyinfoDao.NDelete(model);
                    
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

        #region //临时方法
        public string linshi()
        {
            string json = JsonConvert.SerializeObject(_INA_QyinfoDao.Gettemporarydata());
            return json;

        }
        #endregion

        #region //通过父级Id 查询下级数据
        public ActionResult Getxjdatabypid(string Pid)
        {
            IList<NA_QyinfoView> modellist = _INA_QyinfoDao.GetlistCqybypid(Pid);
            //var jsonstr = JsonConvert.SerializeObject(modellist);
            var result = new
            {
                code = 0,
                msg = "",
                //count = modellist.Count,
                data = modellist,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //新增下级页面
        public ActionResult addxjView(string pid,string qy_type)
        {
            ViewData["pid"] = pid;
            ViewData["qy_type"] = qy_type;
            return View();
        }
        #endregion

        #region //编辑区域
        public ActionResult updateqyView(string id,string Qyname)
        {
            ViewData["id"] = id;
            ViewData["Qyname"] = Qyname;
            return View();
        }
        #endregion
    }                                  
}
