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
using System.Collections.Specialized;
using System.Globalization;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_SupplierController : Controller
    {
        //
        // GET: /NQ_Supplier/0
        INQ_SupplierDao _INQ_SupplierDao = ContextRegistry.GetContext().GetObject("NQ_SupplierDao") as INQ_SupplierDao;
        INQ_SupplierAttachmentDao _INQ_SupplierAttachmentDao = ContextRegistry.GetContext().GetObject("NQ_SupplierAttachmentDao") as INQ_SupplierAttachmentDao;

        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_SupplierView> listmodel = GetListPager(pageIndex, null,null,null);

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
            NQ_SupplierView supplier = new NQ_SupplierView();
            IList<NQ_SupplierAttachmentView> attachments = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                supplier = _INQ_SupplierDao.GetSupplierByFItemId(id);
                attachments = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);

                ViewBag.FLicenceExist = 0;
                ViewBag.FTaxNumExist = 0;
                ViewBag.FISO9001Exist = 0;
                ViewBag.FISO14000Exist = 0;
                ViewBag.FPatentExist = 0;
                ViewBag.FOtherExist = 0;
                ViewBag.FQuestionnaireExist = 0;
                ViewBag.FAgentExist = 0;

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        switch (attach.FAttType)
                        {
                            case 0:
                                ViewBag.FLicenceExist = 1;
                                ViewBag.FLicenceURL = attach.FAttURL;
                                ViewBag.FLicenceDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 1:
                                ViewBag.FTaxNumExist = 1;
                                ViewBag.FTaxNumURL = attach.FAttURL;
                                ViewBag.FTaxDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 2:
                                ViewBag.FISO9001Exist = 1;
                                ViewBag.FISO9001URL = attach.FAttURL;
                                ViewBag.FISO9001DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 3:
                                ViewBag.FISO14000Exist = 1;
                                ViewBag.FISO14000URL = attach.FAttURL;
                                ViewBag.FISO14000DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 4:
                                ViewBag.FPatentExist = 1;
                                ViewBag.FPatentURL = attach.FAttURL;
                                ViewBag.FPatentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 5:
                                ViewBag.FOtherExist = 1;
                                ViewBag.FOtherURL = attach.FAttURL;
                                ViewBag.FOtherDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 6:
                                ViewBag.FQuestionnaireExist = 1;
                                ViewBag.FQuestionnaireURL = attach.FAttURL;
                                ViewBag.FQuestionnaireDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 7:
                                ViewBag.FAgentExist = 1;
                                ViewBag.FAgentURL = attach.FAttURL;
                                ViewBag.FAgentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                        }
                    }
                }
            }
            return View("Edit", supplier);
        }
        #endregion

        public ActionResult CheckPage(string id)
        {
            NQ_SupplierView supplier = new NQ_SupplierView();
            IList<NQ_SupplierAttachmentView> attachments = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                supplier = _INQ_SupplierDao.GetSupplierByFItemId(id);
                attachments = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);

                ViewBag.FLicenceExist = 0;
                ViewBag.FTaxNumExist = 0;
                ViewBag.FISO9001Exist = 0;
                ViewBag.FISO14000Exist = 0;
                ViewBag.FPatentExist = 0;
                ViewBag.FOtherExist = 0;
                ViewBag.FQuestionnaireExist = 0;
                ViewBag.FAgentExist = 0;

                ViewBag.FLicenceStatusName = "未设置";
                ViewBag.FTaxNumStatusName = "未设置";
                ViewBag.FISO9001StatusName = "未设置";
                ViewBag.FISO14000StatusName = "未设置";
                ViewBag.FPatentStatusName = "未设置";
                ViewBag.FOtherStatusName = "未设置";
                ViewBag.FQuestionnaireStatusName = "未设置";
                ViewBag.FAgentStatusName = "未设置";


                ViewBag.FLicenceStatus = 0;
                ViewBag.FTaxNumStatus = 0;
                ViewBag.FISO9001Status = 0;
                ViewBag.FISO14000Status = 0;
                ViewBag.FPatentStatus = 0;
                ViewBag.FOtherStatus =0;
                ViewBag.FQuestionnaireStatus = 0;
                ViewBag.FAgentStatus =0;

                int fstatus = 0;

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        switch (attach.FAttType)
                        {
                            case 0:
                                ViewBag.FLicenceStatusName = fileDateStatus(attach,out fstatus);
                                ViewBag.FLicenceStatus = fstatus;
                                ViewBag.FLicenceExist = 1;
                                ViewBag.FLicenceURL = attach.FAttURL;
                                ViewBag.FLicenceDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 1:
                                ViewBag.FTaxNumStatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FTaxNumStatus = fstatus;
                                ViewBag.FTaxNumExist = 1;
                                ViewBag.FTaxNumURL = attach.FAttURL;
                                ViewBag.FTaxDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 2:
                                ViewBag.FISO9001StatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FISO9001Status = fstatus;
                                ViewBag.FISO9001Exist = 1;
                                ViewBag.FISO9001URL = attach.FAttURL;
                                ViewBag.FISO9001DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 3:
                                ViewBag.FISO14000StatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FISO14000Status = fstatus;
                                ViewBag.FISO14000Exist = 1;
                                ViewBag.FISO14000URL = attach.FAttURL;
                                ViewBag.FISO14000DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 4:
                                ViewBag.FPatentStatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FPatentStatus = fstatus;
                                ViewBag.FPatentExist = 1;
                                ViewBag.FPatentURL = attach.FAttURL;
                                ViewBag.FPatentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 5:
                                ViewBag.FOtherStatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FOtherStatus = fstatus;
                                ViewBag.FOtherExist = 1;
                                ViewBag.FOtherURL = attach.FAttURL;
                                ViewBag.FOtherDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 6:
                                ViewBag.FQuestionnaireStatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FQuestionnaireStatus = fstatus;
                                ViewBag.FQuestionnaireExist = 1;
                                ViewBag.FQuestionnaireURL = attach.FAttURL;
                                ViewBag.FQuestionnaireDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                            case 7:
                                ViewBag.FAgentStatusName = fileDateStatus(attach, out fstatus);
                                ViewBag.FAgentStatus = fstatus;
                                ViewBag.FAgentExist = 1;
                                ViewBag.FAgentURL = attach.FAttURL;
                                ViewBag.FAgentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                                break;
                        }
                    }
                }
            }
            return View("Check", supplier);
        }

        private PagerInfo<NQ_SupplierView> GetListPager(int? pageIndex, string suName,string suCon,string suTel)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_SupplierDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_SupplierDao.SetPagerPageSize(11);
            PagerInfo<NQ_SupplierView> listmodel = _INQ_SupplierDao.getListByNameConTel(suName,suCon,suTel, Suser);
            return listmodel;
        }

        public JsonResult SearchList()
        {
            string name = Request.Form["txtSearch_Name"];
            string con = Request.Form["txtSearch_Con"];
            string tel = Request.Form["txtSearch_Tel"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_SupplierView> listmodel = GetListPager(pageIndex, name,con,tel);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        
        public JsonResult getAndUpdateSupplier()
        {
            string url;
            url = "http://222.92.203.58:83/Baseitem.asmx/getUpdatedSupplier?modifytime=" + "0";

            string result = HttpUtility11.GetData(url);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(result);

            string sSupplier = doc.InnerText;

            List<NQ_SupplierView> supplierList = DeserializeJsonToList<NQ_SupplierView>(sSupplier);

            _INQ_SupplierDao.saveOrUpdate(supplierList);

            return Json(new { result = "success" },JsonRequestBehavior.AllowGet);
        }

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult updateSupplierAndAttachment(FormContext fcontext, HttpPostedFileBase image)
        {
            try
            {
                //bool flay = false;
                //string fitemid = Request.Form["FItemId"];
                //SessionUser user = Session[SessionHelper.User] as SessionUser;

                //NQ_SupplierView smodel = _INQ_SupplierDao.GetSupplierByFItemId(fitemid);

                //smodel.FISO9001 = Request.Form["FISO9001"].ToString();
                //smodel.FISO14000 = Request.Form["FISO14000"].ToString();
                //smodel.FPatent = Request.Form["FPatent"].ToString();
                //smodel.FOther = Request.Form["FOther"].ToString();
                //smodel.FQuestionnaire = Request.Form["FQuestionnaire"].ToString();
                //smodel.FAgent = Request.Form["FAgent"].ToString();
                //smodel.FSupplierType = Int32.Parse(Request.Form["FSupplierType"]);
                //flay = _INQ_SupplierDao.NUpdate(smodel);

                //if (flay)
                //    return Json(new { result = "success" });
                //else
                //    return Json(new { result = "error" });
                return Json(new { result = "error" }); //do nothing
            }
            catch
            {
                return Json(new { result = "error" });
            }
        }
        #endregion


        private static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        private string fileDateStatus(NQ_SupplierAttachmentView att,out int statusType)
        {
            string rtnStatus = "";
            int tsDiff = 0;
            if (att == null)
            {
                rtnStatus = "未设置";
                statusType = 0;
            }
            else
            {
                tsDiff = att.FAttDeadline.Subtract(DateTime.Now.Date).Days;
            }
            if (tsDiff > 30)
            {
                rtnStatus = "正常";
                statusType = 3;
            }
            else if (tsDiff <= 30 && tsDiff >= 0)
            {
                rtnStatus = "即将过期";
                statusType = 4;
            }
            else
            {
                rtnStatus = "已经过期";
                statusType = 5;
            }

            return rtnStatus;
        }
    }
}
