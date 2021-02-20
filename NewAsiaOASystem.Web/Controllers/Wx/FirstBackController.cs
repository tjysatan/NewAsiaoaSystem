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
    public class FirstBackController : Controller
    {
        //
        // GET: /FirstBack/
        IWX_MessageDao _IWX_MessageDao = ContextRegistry.GetContext().GetObject("WX_MessageDao") as IWX_MessageDao;
        IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
        IWX_SendCDao _IWX_SendCDao = ContextRegistry.GetContext().GetObject("WX_SendCDao") as IWX_SendCDao;
        public ActionResult Index(int? pageIndex)
        {
            //int CurrentPageIndex = Convert.ToInt32(pageIndex);
            //if (CurrentPageIndex == 0)
            //    CurrentPageIndex = 1;
            //_IWX_MessageDao.SetPagerPageIndex(CurrentPageIndex);
            //_IWX_MessageDao.SetPagerPageSize(15);
            PagerInfo<WX_MessageView> listmodel = GetListPager(pageIndex,null,null,null);
            int SMcount = _IWX_SendCDao.GetSM(0);//统计当月群发次数
            ViewData["SMcount"] = SMcount;
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string A_KeyWord = Request.Form["A_KeyWord"];
            string MsgType = Request.Form["MsgType"];
            string IsDefaultMessage = Request.Form["IsDefaultMessage"];// 
          
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WX_MessageView> listmodel = GetListPager(pageIndex, A_KeyWord, MsgType, IsDefaultMessage);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<WX_MessageView> GetListPager(int? pageIndex, string A_KeyWord, string MsgType, string IsDefaultMessage)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWX_MessageDao.SetPagerPageIndex(CurrentPageIndex);
            _IWX_MessageDao.SetPagerPageSize(10);
            PagerInfo<WX_MessageView> listmodel = _IWX_MessageDao.GetMseeagaPagelist(A_KeyWord, MsgType, IsDefaultMessage, Suser);
            return listmodel;
        }
        #endregion

        #region //图文消息管理列表页
        public ActionResult Newslist(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWX_MessageDao.SetPagerPageIndex(CurrentPageIndex);
            _IWX_MessageDao.SetPagerPageSize(15);
            PagerInfo<WX_Message_NewsView> listmodel = _IWX_Message_NewsDao.Search();
            //AlbumDropdown(null);
            return View(listmodel);
        }
        #endregion

        #region //图文管理的编辑方法
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewsEdit(WX_Message_NewsView model, HttpPostedFileBase image)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.N_Id))
                {
                    if (image != null)
                    {
                        string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                        image.SaveAs(filePath);
                        model.PicUrl = "/Content/Img/" + fileName;
                    }
                    model.MType = 0;
                    //model.Content = model.Content;
                    flay = _IWX_Message_NewsDao.Ninsert(model);
                }
                //修改
                else
                {
                    if (image != null)
                    {
                        string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                        image.SaveAs(filePath);
                        model.PicUrl = "/Content/Img/" + fileName;
                    }

                    flay = _IWX_Message_NewsDao.NUpdate(model);
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

        #region //跳转图文添加页面
        public ActionResult Newaddpage()
        {
            return View("NewsEdit", new WX_Message_NewsView());
        } 
        #endregion

        #region //调转图文并更新页面
        public ActionResult NewsUpdatepage(string id)
        {
            WX_Message_NewsView sys = new WX_Message_NewsView();
            sys = _IWX_Message_NewsDao.NGetModelById(id);
            return View("NewsEdit", sys);
        } 
        #endregion

        /// <summary>
        /// 设置图文下来框的值(编辑页面时)
        /// </summary>

        #region //设置图文下拉框的值
        public JsonResult MnewsAlbumDropdown()
        {
            string Mid = Request.Form["Mid"];
            string json = _IWX_MessageDao.MnewsAlbumDropdown(Mid);
            return Json(new { result = json }, "text/html");
        } 
        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region //删除图文
        public ActionResult NewDelete(string id)
        {
            bool i = false;
            List<WX_Message_NewsView> sys = _IWX_Message_NewsDao.GetWX_Message_newby_Nid(id) as List<WX_Message_NewsView>;
            if (null != sys)
            {
                i = _IWX_Message_NewsDao.NDelete(sys);
            }

            return RedirectToAction("Newslist");
        } 
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(WX_MessageView model)
        {
            try
            {
                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(model.A_id))
                {
                    model.A_CreateTime = DateTime.Now;
                    model.A_CreateUser = Convert.ToString(Session["user"]);
                    model.MsgType = "text";
                    flay = _IWX_MessageDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.A_id = model.A_id;
                    model.MsgType = "text";
                    flay = _IWX_MessageDao.NUpdate(model);
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

        #region //保存图文的处理方法
        [HttpPost]
        public JsonResult Edit_new(WX_MessageView model, FormCollection from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model.wx_Message_News = Request.Form["SelectComm"];//获取选择的图文
                //添加
                if (string.IsNullOrEmpty(model.A_id))
                {
                    model.A_CreateTime = DateTime.Now;
                    model.A_CreateUser = user.UserName;
                    model.MsgType = "news";
                    //  flay = _IWX_MessageDao.Ninsert(model);
                    flay = _IWX_MessageDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.A_id = model.A_id;
                    model.MsgType = "news";
                    flay = _IWX_MessageDao.NUpdate(model);
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

        /// <summary>
        /// 跳转到文本添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            return View("Edit", new WX_MessageView());
        }

        /// <summary>
        /// 跳转到图文的添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage_New()
        {
            WX_MessageView model = new WX_MessageView
            {
                MsgType = "news"
            };
            return View("Edit_new", new WX_MessageView());
        }

        public ActionResult addPage_Mnew(WX_Message Mode, string Id)
        {
            return View("Edit_Mnew", new WX_Message());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WX_MessageView sys = new WX_MessageView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWX_MessageDao.NGetModelById(id);
            if (sys.MsgType == "text")
            {
                return View("Edit", sys);
            }
            else
            {
                WX_MessageView Dsys = new WX_MessageView();
                if (!string.IsNullOrEmpty(id))
                    Dsys = _IWX_MessageDao.NGetModelById(id);
                return View("Edit_new", Dsys);
            }

        }

        /// <summary>
        /// 删除关键字回复
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region //删除关键字回复
        public ActionResult Delete(string id)
        {
            bool i = false;
            List<WX_Message_News> sys = _IWX_Message_NewsDao.GetWX_Message_newby_id(id) as List<WX_Message_News>;//通过A_id查找图文表中的数据
            if (null != sys)
            {
                i = _IWX_Message_NewsDao.wx_MNdelete(sys); //删除图文版中查找的到的数据
            }
            List<WX_MessageView> wxmessage = _IWX_MessageDao.GetWX_MessageViewby_id(id) as List<WX_MessageView>;//通过A_id查找关键字表中的数据
            if (null != wxmessage)
            {
                i = _IWX_MessageDao.NDelete(wxmessage);//删除关键字表中查找到的数据
            }
            return RedirectToAction("Index");
        } 
        #endregion

        #region //删除关键字恢复
        public string NewKeyDel(string id)
        {
            bool i = false;
             List<WX_Message_News> sys = _IWX_Message_NewsDao.GetWX_Message_newby_id(id) as List<WX_Message_News>;//通过A_id查找图文表中的数据
             if (sys != null)
             {
                 i = _IWX_Message_NewsDao.wx_MNdelete(sys); //删除图文版中查找的到的数据
             }
             List<WX_MessageView> wxmessage = _IWX_MessageDao.GetWX_MessageViewby_id(id) as List<WX_MessageView>;//通过A_id查找关键字表中的数据
             if (null != wxmessage)
             {
                 i = _IWX_MessageDao.NDelete(wxmessage);//删除关键字表中查找到的数据
             }
            
             if (i)
                 return "0";
             else
                 return "1";
        }
        #endregion

        #region //群发消息
        public ActionResult S_ALL(string Id,string type)
        {
            int SMcount = _IWX_SendCDao.GetSM(0);//获取当月发送的次数
            if (SMcount < 4)
            {
                //获取系统文件的物理路径
                string filPath = Server.MapPath("~");
                WX_MessageView sys = new WX_MessageView();
                if (!string.IsNullOrEmpty(Id))
                    sys = _IWX_MessageDao.NGetModelById(Id);
                if (sys.MsgType == "text")//群发文本消息
                {
                    string I = MassManager.S_Mass(Id,filPath,type, sys.MsgType);
                     WX_SendCView sendcmodel = new WX_SendCView();
                     if (I == "0")
                     {
                         sendcmodel.S_Month = Convert.ToString(DateTime.Now.Month);
                         sendcmodel.S_Year = Convert.ToString(DateTime.Now.Year);
                         sendcmodel.S_Type = 0;
                         sendcmodel.S_Time = DateTime.Now;
                         if (!_IWX_SendCDao.Ninsert(sendcmodel))
                         {
                             return RedirectToAction("Rs");
                         }
                         else
                         {
                             return RedirectToAction("Su");
                         }
                     }
                     else
                     {
                         return RedirectToAction("Rs");
                     }
                    //return Content("<script>alert('发送图文消息！');window.history.back();</script>");
                }
                else  //群发图文消息
                {
                 
                    string I = MassManager.S_Mass(Id, filPath,type,sys.MsgType);
                    WX_SendCView sendcmodel = new WX_SendCView();
                    if (I == "0")
                    {
                        sendcmodel.S_Month = Convert.ToString(DateTime.Now.Month);
                        sendcmodel.S_Year = Convert.ToString(DateTime.Now.Year);
                        sendcmodel.S_Type = 0;
                        sendcmodel.S_Time = DateTime.Now;
                        if (!_IWX_SendCDao.Ninsert(sendcmodel))
                        {
                            return RedirectToAction("Su");
                        }
                        else
                        {
                            return RedirectToAction("Su");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Rs");
                    }
                }
            }
            else
            {
                return Content("<script>alert('已经超出当月最大的发送的条数！');window.history.back();</script>");
            }


        } 
        #endregion

        [HttpPost]
        public JsonResult test()
        {
            string key = "";
            IList<WX_Message> list = _IWX_MessageDao.GetWX_MessageList(key);
            if (list!=null &&list.Count>0)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        //详情页
        public ActionResult Details()
        {
            string Id = Request.QueryString["Id"];
            WX_Message_NewsView model = _IWX_Message_NewsDao.NGetModelById(Id);
            return View(model);
        }

        public ActionResult Su()
        {
            return View();
        }

        public ActionResult Rs()
        {
            return View();
        }
 
    }
}
