using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using NewAsiaOASystem.DBModel;
using NHibernate;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_BaseitemAttachmentController : Controller
    {
        //
        // GET: /NQ_Baseitem/0
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
        INQ_BaseitemAttachmentDao _INQ_BaseitemAttachmentDao = ContextRegistry.GetContext().GetObject("NQ_BaseitemAttachmentDao") as INQ_BaseitemAttachmentDao;
        INQ_OASupplierDao _INQ_OASupplierDao = ContextRegistry.GetContext().GetObject("NQ_OASupplierDao") as INQ_OASupplierDao;
        //INQ_SupplierAndBaseitem _INQ_SupplierAndBaseitemDao = ContextRegistry.GetContext().GetObject("INQ_SupplierAndBaseitem") as INQ_SupplierAndBaseitemDao;

        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_BaseitemAttachmentView> listmodel = GetListPager(pageIndex, null);

            Dictionary<string, string> dicOption = new Dictionary<string, string>();

            return View(listmodel);
        }

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            IList<NQ_BaseitemAttachmentView> sys = new List<NQ_BaseitemAttachmentView>();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_BaseitemAttachmentDao.GetAttachmentByBaseitemId(id);
            return View("Edit", sys);
        }
        #endregion

        private PagerInfo<NQ_BaseitemAttachmentView> GetListPager(int? pageIndex, string suName)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_BaseitemAttachmentDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_BaseitemAttachmentDao.SetPagerPageSize(11);
            PagerInfo<NQ_BaseitemAttachmentView> listmodel = _INQ_BaseitemAttachmentDao.GetCinfoList(suName, Suser);
            return listmodel;
        }

        public ActionResult BaseitemAttachEdit(string itemid, string atttype,string supplierid)
        {
            K3_wuliaoinfoView baseitem = new K3_wuliaoinfoView();
            NQ_BaseitemAttachmentView attach = new NQ_BaseitemAttachmentView();
            NQ_OASupplierView supplier = new NQ_OASupplierView();

            if (!string.IsNullOrEmpty(itemid) && !string.IsNullOrEmpty(atttype))
            {
                K3_wuliaoinfoView tempBaseitem = new K3_wuliaoinfoView();
                NQ_BaseitemAttachmentView tempattach = new NQ_BaseitemAttachmentView();

                tempBaseitem = _IK3_wuliaoinfoDao.GetBaseitemByItemId(itemid);
                tempattach = _INQ_BaseitemAttachmentDao.GetAttachByBaseitemAndTypeAndSupplier(itemid, atttype, supplierid);
                supplier = _INQ_OASupplierDao.GetSupplierById(supplierid);

                if (tempBaseitem != null)
                {
                    baseitem = tempBaseitem;
                    baseitem.itemStatus = _IK3_wuliaoinfoDao.setSuStatus(baseitem);
                }

                if (tempattach != null)
                {
                    attach = tempattach;
                }
                else
                {
                    attach.FAttDeadline = DateTime.Now.Date;
                }
            }

            ViewBag.ItemName = baseitem.fname;
            ViewBag.ItemModel = baseitem.fmodel;
            ViewBag.ItemID = baseitem.fitem;
            ViewBag.ItemNumber = baseitem.fnumber;
            ViewBag.ItemStatus = baseitem.itemStatus.iStatus;
            ViewBag.SupplierName = supplier.FName;
            ViewBag.SupplierId = supplier.Id;
            ViewBag.SupplierNumber = supplier.FNumber;
            attach.FAttType = int.Parse(atttype);

            return View("BaseitemAttachEdit", attach);
        }


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(K3_wuliaoinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

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
        public JsonResult BaseitemAttachSave()
        {
            bool flay = false;

            HttpPostedFileBase file = null;
            if (Request.Files.Count > 0)
            {
                file = Request.Files[0];
            }

            string fattid = Request.Form["Id"];
            string fitemname = Request.Form["fitemname"].ToString();
            //string fitemtype = Request.Form["fBaseitemtype"];
            string fatttype = Request.Form["fatttype"];
            string fattdeadline = Request.Form["fattdeadline"];
            string fatturl = Request.Form["fatturl"];
            string fitemid = Request.Form["fitemid"];
            string fatttext = Request.Form["fatttext"];
            var a = Request.QueryString["Baseitemid"];
            string supplierid = Request.Form["fsupplierid"];
            string subname = "itematt";

            NQ_BaseitemAttachmentView atttemp = new NQ_BaseitemAttachmentView();
            NQ_BaseitemAttachmentView att = new NQ_BaseitemAttachmentView();
            K3_wuliaoinfoView baseitem = _IK3_wuliaoinfoDao.GetBaseitemByItemId(fitemid);
            atttemp = _INQ_BaseitemAttachmentDao.GetAttachByBaseitemAndTypeAndSupplier(fitemid, fatttype,supplierid);

            if (atttemp != null)
            {
                att = atttemp;
            }
            else
            {
                att.FItemid = int.Parse(fitemid);
                att.FAttType = int.Parse(fatttype);
                att.FSupplierid = int.Parse(supplierid);

            }
            att.FAttDeadline = Convert.ToDateTime(fattdeadline);
            //att.FAttURL = tempurl;
            att.FAttText = fatttext;

            if (file != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);

                string filePath = Path.Combine(Server.MapPath("~/Content/upload/" + subname), fileName);

                file.SaveAs(filePath);

                att.FAttURL = "/Content/upload/" + subname + "/" + fileName;

            }

            if (att.id > 0)
                flay = _INQ_BaseitemAttachmentDao.NUpdate(att);
            else
                flay = _INQ_BaseitemAttachmentDao.Ninsert(att);

            baseitem.FIsChecked = 0;

            _IK3_wuliaoinfoDao.NUpdate(baseitem);

            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }



        private static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

    }
}
