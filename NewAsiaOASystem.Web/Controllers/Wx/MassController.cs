using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class MassController : Controller
    {
        //
        // GET: /Mass/
        IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
        IWX_SendCDao _IWX_SendCDao = ContextRegistry.GetContext().GetObject("WX_SendCDao") as IWX_SendCDao;
        public static readonly string ym = WebConfigurationManager.AppSettings["ym"];
        //图文添加列表
        public ActionResult Index()
        {
            //int CurrentPageIndex = Convert.ToInt32(pageIndex);
            //if (CurrentPageIndex == 0)
            //    CurrentPageIndex = 1;
            //_IWX_Message_NewsDao.SetPagerPageIndex(CurrentPageIndex);
            //_IWX_Message_NewsDao.SetPagerPageSize(5);
            //PagerInfo<WX_Message_NewsView> listmodel = _IWX_Message_NewsDao.Search();
            int SMcount = _IWX_SendCDao.GetSM(0);//统计当月群发次数
            ViewData["SMcount"] = SMcount;
            return View(); 
        }


        #region //分页数据
        public ActionResult GetWX_MessagePager(int? page, int limit, string Title, string Description)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWX_Message_NewsDao.SetPagerPageIndex(CurrentPageIndex);
            _IWX_Message_NewsDao.SetPagerPageSize(limit);
            PagerInfo<WX_Message_NewsView> listmodel = _IWX_Message_NewsDao.GetCinfoList(Title, Description);
            var result = new
            {
                code = 0,
                msg = "",
                count = listmodel.RecordCount,
                data = listmodel.DataList,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult JXPage()
        {
            WX_Message_NewsView sys = new WX_Message_NewsView();
            sys = _IWX_Message_NewsDao.GetWX_Message_newby_type(1);
            int jxcount = _IWX_SendCDao.GetSM(1);//统计当月群发次数
            ViewData["jxcount"] = jxcount;
            return View("JsFS", sys);
        }

        //消息群发选择
        [HttpPost]
        public JsonResult JsFS(WX_Message_NewsView model)
        {
            try
            {
                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(model.N_Id))
                {
                    model.MType = 1;
                    model.Url = ym + "DisPerformanceAppraisal/P_PerformanceOrderByPage";
                    flay = _IWX_Message_NewsDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Url = ym + "DisPerformanceAppraisal/P_PerformanceOrderByPage";
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

        /// <summary>
        /// 添加修改页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(WX_Message_NewsView model, HttpPostedFileBase image)
        {
            try
            {
                bool flay = false;
                //string I = null;
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
                    flay = _IWX_Message_NewsDao.Ninsert(model); 
                }
                //修改
                else
                {
                    if (image!=null)
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

        /// <summary>
        /// 跳转图文添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {

            return View("Edit", new WX_Message_NewsView());
        }


        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WX_Message_NewsView sys = new WX_Message_NewsView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWX_Message_NewsDao.NGetModelById(id);
            return View("Edit", sys);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            bool i = false;
            List<WX_Message_NewsView> sys = _IWX_Message_NewsDao.GetWX_Message_newby_Nid(id) as List<WX_Message_NewsView>;
            if (null != sys)
            {
                i = _IWX_Message_NewsDao.NDelete(sys);
            }

            return RedirectToAction("Index");
        }

        #region //所有微信群发
        public ActionResult S_ALL(string Id,string type)
        {
          
            int SMcount = _IWX_SendCDao.GetSM(0);//获取当月发送的次数
            if (SMcount < 4)
            {
                //获取系统文件的物理路径
                string filPath = Server.MapPath("~");
                string I = MassManager.S_Mass(Id, filPath,type,"111");
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
            }
            else
            {
                return Content("<script>alert('已经超出当月最大的发送的条数！');window.history.back();</script>");
            }

     
            
        } 
        #endregion

        #region //群发绑定的微信
        public ActionResult S_bd(string Id)
        {
            string I = MassManager.Mb_S_mass(Id);
            if (I == "0")
            {
                return RedirectToAction("Su");
            }
            else
            {
                return RedirectToAction("Rs");
            }
        } 
        #endregion

        //详情页
        public ActionResult Details()
        {
            return View();
        }

        public ActionResult test()
        {
            string I = MassManager.Mb_S_Icmass();
            return RedirectToAction("Index");
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
