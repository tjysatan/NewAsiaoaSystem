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
    public class XSFXdqinfoController : Controller
    {
        //
        // GET: /XSFXdqinfo/
        IXSFXdqinfoDao _IXSFXdqinfoDao = ContextRegistry.GetContext().GetObject("XSFXdqinfoDao") as IXSFXdqinfoDao;

        IXSFXqxinfoDao _IXSFXqxinfoDao = ContextRegistry.GetContext().GetObject("XSFXqxinfoDao") as IXSFXqxinfoDao;

        IXSFXkhinfoDao _IXSFXkhinfoDao = ContextRegistry.GetContext().GetObject("XSFXkhinfoDao") as IXSFXkhinfoDao;


        #region //数据列表页面

        public ActionResult List(int? pageIndex)
        {
            PagerInfo<XSFXdqinfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<XSFXdqinfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<XSFXdqinfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IXSFXdqinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IXSFXdqinfoDao.SetPagerPageSize(11);
            PagerInfo<XSFXdqinfoView> listmodel = _IXSFXdqinfoDao.GetxsfxList(Name,Suser);
            return listmodel;
        }
        #endregion


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(XSFXdqinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
    
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.dqname = model.dqname;//区域名称
                    model.XSL = model.XSL;//目标销售量
                    model.WCSL = model.WCSL;//已经完成
                    model.XSYear = model.XSYear;//销售目标年费
                    model.zrr = model.zrr;//责任人
                    model.C_datetime = DateTime.Now;//制定时间
                    flay = _IXSFXdqinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.dqname = model.dqname;//区域名称
                    model.XSL = model.XSL;//目标销售量
                    model.WCSL = model.WCSL;//已经完成
                    model.XSYear = model.XSYear;//销售目标年费
                    model.zrr = model.zrr;//责任人
                    model.C_datetime = model.C_datetime;
                    flay = _IXSFXdqinfoDao.NUpdate(model);
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
        public ActionResult addPage()
        {
            return View("Edit", new XSFXdqinfoView());
        }
        #endregion

        #region //跳转到编辑完成量页面
        /// <summary>
        /// 跳转到编辑完成量页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            XSFXdqinfoView sys = new XSFXdqinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IXSFXdqinfoDao.NGetModelById(id);
            return View(sys);
        }
        #endregion


        #region //查看编辑页面
        public ActionResult ckview(string Id)
        {
            ViewData["xsfxId"] = Id;
            return View();
        } 
        #endregion



        #region //通过Id 查找区域销售信息json
        [HttpPost]
        public string GetxsfxdqinfobyIdjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_IXSFXdqinfoDao.NGetModelById(Id));
            return json;
        } 
        #endregion


        #region //区县 销售信息添加页面
        public ActionResult QXADD(string Id)
        {
            ViewData["QYId"] = Id;
            return View();
        } 
        #endregion

        #region //保存 区县销售信息
        [HttpPost]
        public JsonResult qxEdit(string dqId, string qxname, string xsl)
        {
            try
            {
                bool flay = false;
                XSFXqxinfoView model = new XSFXqxinfoView();
                model.dqId = dqId;
                model.Dqname = qxname;
                model.XSL = Convert.ToInt32(xsl);

                flay = _IXSFXqxinfoDao.Ninsert(model);
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

        [HttpPost]
        public string Getxsfxqxinfobydqidjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_IXSFXqxinfoDao.Getxsfxqxlist(Id));
            return json;
        }

        //客户 销售信息添加
        public ActionResult KHADD(string Id,string qxId)
        {
            ViewData["QYId"] = Id;
            ViewData["QXId"] = qxId;
            return View();
        }

        [HttpPost]
        public JsonResult KHEdit(string QXId, string qxname, string xsl,string beizhu)
        {
            try
            {
                bool flay = false;
                XSFXkhinfoView model = new XSFXkhinfoView();
                model.QxId = QXId;
                model.KHName = qxname;
                model.XSL = Convert.ToInt32(xsl);
                model.beizhu = beizhu;
                flay = _IXSFXkhinfoDao.Ninsert(model);
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

        [HttpPost]
        public string Getxsfxkhinfobydxidjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_IXSFXkhinfoDao.Getxsfxkhlist(Id));
            return json;
        }

    }
}
