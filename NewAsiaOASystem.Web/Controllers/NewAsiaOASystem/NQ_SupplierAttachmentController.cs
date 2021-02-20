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
    public class NQ_SupplierAttachmentController : Controller
    {
        // GET: /NQ_Supplier/0
        INQ_OASupplierDao _INQ_OASupplierDao = ContextRegistry.GetContext().GetObject("NQ_OASupplierDao") as INQ_OASupplierDao;
        INQ_SupplierAttachmentDao _INQ_SupplierAttachmentDao = ContextRegistry.GetContext().GetObject("NQ_SupplierAttachmentDao") as INQ_SupplierAttachmentDao;

        public ActionResult List(int? pageIndex)
        {
            IList<NQ_SupplierAttachmentView> listmodel = _INQ_SupplierAttachmentDao.getAttachBySunameAndType( "", "");

            foreach (var mo in listmodel)
            {
                NQ_OASupplierView model = _INQ_OASupplierDao.GetSupplierById(mo.FSupplierid.ToString()) as NQ_OASupplierView;
                mo.FSuName = model.FName;
                if (mo.isPermanent == 1)
                {
                    mo.FShowdate = "永不过期";
                }
                else
                {
                    mo.FShowdate = mo.FAttDeadline.ToString("yyyy-MM-dd");
                }
            }

            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            PagerInfo<NQ_SupplierAttachmentView> rtnlist = _INQ_SupplierAttachmentDao.setPagerInfo(listmodel, CurrentPageIndex, 11);
            return View(rtnlist);
        }

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            IList<NQ_SupplierAttachmentView> sys = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);
            return View("Edit", sys);
        }
        #endregion

        private PagerInfo<NQ_SupplierAttachmentView> GetListPager(int? pageIndex, string suName)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_SupplierAttachmentDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_SupplierAttachmentDao.SetPagerPageSize(11);
            PagerInfo<NQ_SupplierAttachmentView> listmodel = _INQ_SupplierAttachmentDao.GetCinfoList(suName, Suser);
            return listmodel;
        }

        #region //获取供应商对应附件信息
        public ActionResult SupplierAttachEdit(string supplierid, string atttype, string seq)
        {
            NQ_OASupplierView supplier = new NQ_OASupplierView();
            NQ_SupplierAttachmentView attach = new NQ_SupplierAttachmentView();
            if (!string.IsNullOrEmpty(supplierid) && !string.IsNullOrEmpty(atttype))
            {
                NQ_OASupplierView tempsupplier = new NQ_OASupplierView();
                NQ_SupplierAttachmentView tempattach = new NQ_SupplierAttachmentView();
                tempsupplier = _INQ_OASupplierDao.GetSupplierById(supplierid);
                tempattach = _INQ_SupplierAttachmentDao.GetAttachBySupplierAndTypeAndSeq(supplierid, atttype,seq);
                if (tempsupplier != null)
                {
                    supplier = tempsupplier;
                    supplier.supplierStatus = _INQ_OASupplierDao.setSuStatus(supplier);
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
            ViewBag.SupplierName = supplier.FName;
            ViewBag.SupplierType = supplier.FSupplierType;
            ViewBag.SupplierID = supplier.Id;
            ViewBag.SupplierStatus = supplier.supplierStatus.iStatus;
            attach.FAttType = int.Parse(atttype);
            attach.seq = int.Parse(seq);
            return View("SupplierAttachEdit", attach);
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_SupplierView model, FrameController from)
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
        public JsonResult SupplierAttachSave()
        {
            bool flay = false;
            HttpPostedFileBase file = null;
            if (Request.Files.Count > 0)
            {
                file = Request.Files[0];
            }
            string fattid = Request.Form["Id"];
            string seq = Request.Form["seq"];
            string fsuppliername = Request.Form["fsuppliername"].ToString();
            string fsuppliertype = Request.Form["fsuppliertype"];
            string fatttype = Request.Form["fatttype"];
            string txtAttdeadline = Request.Form["txtAttdeadline"];
            string fatturl = Request.Form["fatturl"];
            string fsupplierid = Request.Form["fsupplierid"];
            string fatttext = Request.Form["fatttext"];
            string[] values = Request.Form.GetValues("isNeverExpired");
            var a = Request.QueryString["supplierid"];
            string subname = "supplieratt";
            NQ_SupplierAttachmentView atttemp = new NQ_SupplierAttachmentView();
            NQ_SupplierAttachmentView att = new NQ_SupplierAttachmentView();
            NQ_OASupplier supplier = _INQ_OASupplierDao.GetSupplierByIdNoView(fsupplierid);
            atttemp = _INQ_SupplierAttachmentDao.GetAttachBySupplierAndTypeAndSeq(fsupplierid, fatttype,seq);
            if (atttemp != null)
            {
                att = atttemp;
            }
            else
            {
                att.FSupplierid = int.Parse(fsupplierid);
                att.FAttType = int.Parse(fatttype);
                att.FAttDeadline = DateTime.Now;
                att.seq = int.Parse(seq);
            }
            att.FAttText = fatttext;
            if (txtAttdeadline != "永不过期")
            {
                att.FAttDeadline = Convert.ToDateTime(txtAttdeadline);
                att.isPermanent = 0;
            }
            if (file != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/" + subname), fileName);
                file.SaveAs(filePath);
                att.FAttURL = "/Content/upload/" + subname + "/" + fileName;
            }
            if (values != null)
            {
                att.isPermanent = 1;
            }
            else
            {
                att.isPermanent = 0;
            }
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (att.id > 0)
            {
                flay = _INQ_SupplierAttachmentDao.NUpdate(att);
                NAHelper.Insertczjl(att.id.ToString(), "更新附件:type" + att.FAttType, user.Id);
            }
            else
            {
                flay = _INQ_SupplierAttachmentDao.Ninsert(att);
                NAHelper.Insertczjl(att.id.ToString(), "新增附件:type" + att.FAttType, user.Id);
            }
            supplier.FIsChecked = 0;
            _INQ_OASupplierDao.baseUpdate(supplier);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        //删除对应附件
        [HttpPost]
        public ActionResult clearAtt(string supplierid , string atttype, string seq)
        {
            try
            {
                NQ_SupplierAttachmentView att = new NQ_SupplierAttachmentView();
                NQ_OASupplierView supplier = _INQ_OASupplierDao.GetSupplierById(supplierid);
                att = _INQ_SupplierAttachmentDao.GetAttachBySupplierAndTypeAndSeq(supplierid, atttype,seq);
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                string filepath = "";
                string atturl = att.FAttURL;
                if (atturl != null)
                {
                    filepath = Server.MapPath("~" + atturl);
                    if (System.IO.File.Exists(filepath))//判断文件是否存在
                    {
                        System.IO.File.Delete(filepath);//执行IO文件删除,需引入命名空间System.IO;    
                    }
                }
                flay = _INQ_SupplierAttachmentDao.NDelete(att);
                NAHelper.Insertczjl(att.id.ToString(), "删除附件:type" + att.FAttType, user.Id);

                //设置成审核
                //smodel.FIsChecked = 1;
                //flay = _INQ_OASupplierDao.baseUpdate(smodel);
                if (flay)
                {
                    return Json(new { result = "success", id = 0 });
                }
                else
                    return Json(new { result = "error", id = 0 });
            }
            catch (Exception e)
            {
                return Json(new { result = "error", id = 0 });
            }
        }


        private static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        public JsonResult SearchList()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string name = Request.Form["txtSearch_Name"];
            string status = Request.Form["statusList"];
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            IList<NQ_SupplierAttachmentView> listmodel = _INQ_SupplierAttachmentDao.getAttachBySunameAndType(name, "");
            foreach (var mo in listmodel)
            {
                NQ_OASupplierView model = _INQ_OASupplierDao.GetSupplierById(mo.FSupplierid.ToString()) as NQ_OASupplierView;
                mo.FSuName = model.FName;
                if (mo.isPermanent == 1)
                {
                    mo.FShowdate = "永不过期";
                }
                else
                {
                    mo.FShowdate = mo.FAttDeadline.ToString("yyyy-MM-dd");
                }
            }
            PagerInfo<NQ_SupplierAttachmentView> rtnlist = _INQ_SupplierAttachmentDao.setPagerInfo(listmodel, pageIndex, 11);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(rtnlist.CurrentPageIndex, rtnlist.PageSize, rtnlist.RecordCount);
            if (rtnlist != null)
                return Json(new { result = rtnlist.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        //获取附件信息
        public ActionResult getAttatch(string supplierid, string atttype, string seq)
        {
            NQ_SupplierAttachmentView attach = new NQ_SupplierAttachmentView();
            if (!string.IsNullOrEmpty(supplierid) && !string.IsNullOrEmpty(atttype))
            {
                NQ_OASupplierView tempsupplier = new NQ_OASupplierView();
                NQ_SupplierAttachmentView tempattach = new NQ_SupplierAttachmentView();
                tempsupplier = _INQ_OASupplierDao.GetSupplierById(supplierid);
                tempattach = _INQ_SupplierAttachmentDao.GetAttachBySupplierAndTypeAndSeq(supplierid, atttype, seq);
                
                if (tempattach != null)
                {
                    attach = tempattach;
                }
                else
                {
                    return Json(new { result = "error" });
                }

            }

            return Json(new { result = "success" , isPermanent = attach.isPermanent, deadline = attach.FAttDeadline.ToString("D") });

        }

        public ActionResult getAttatchCheck(string supplierid, string atttype, string seq)
        {
            NQ_SupplierAttachmentView attach = new NQ_SupplierAttachmentView();
            if (!string.IsNullOrEmpty(supplierid) && !string.IsNullOrEmpty(atttype))
            {
                NQ_OASupplierView tempsupplier = new NQ_OASupplierView();
                NQ_SupplierAttachmentView tempattach = new NQ_SupplierAttachmentView();
                tempsupplier = _INQ_OASupplierDao.GetSupplierById(supplierid);
                tempattach = _INQ_SupplierAttachmentDao.GetAttachBySupplierAndTypeAndSeq(supplierid, atttype, seq);

                if (tempattach != null)
                {
                    attach = tempattach;
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }

            suStatus supplierStatus = new suStatus();

            supplierStatus = _INQ_OASupplierDao.fileDateStatus(attach);

            return Json(new { result = "success", isPermanent = attach.isPermanent,
                deadline = attach.FAttDeadline.ToString("D"), name=supplierStatus.strStatusName,
                status = supplierStatus.iStatus, url = (attach.FAttURL == null ? "" : attach.FAttURL)
            });

        }
    }
}
