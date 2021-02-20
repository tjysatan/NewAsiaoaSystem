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
using System.Data;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_OASupplierController : Controller
    {
        INQ_OASupplierDao _INQ_OASupplierDao = ContextRegistry.GetContext().GetObject("NQ_OASupplierDao") as INQ_OASupplierDao;
        INQ_SupplierAttachmentDao _INQ_SupplierAttachmentDao = ContextRegistry.GetContext().GetObject("NQ_SupplierAttachmentDao") as INQ_SupplierAttachmentDao;
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <param name="pageIndex">分页页数</param>
        /// <returns>当前页面数据列表</returns>
        public ActionResult List(int? pageIndex)
        {
            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(null, null, null);
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

            ViewBag.statusList = new List<string>();


            //0:待审核,1:审核通过,2:审核未通过,3正常,4即将过期,5已经过期
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="全部",Value="-1"},
                new SelectListItem(){Text="待审核",Value="0"},
                //new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"},
                new SelectListItem(){Text="正常",Value="3"},
                new SelectListItem(){Text="即将过期",Value="4"},
                new SelectListItem(){Text="已经过期",Value="5"}
            };

            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Suser.Id);
            IList<SysRole> RoleList = sysp.Role;

            ViewBag.isShowNew = 0;
            ViewBag.isShowEdit = 0;

            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null && Role.Name.Contains("品保"))
                    {
                        ViewBag.isShowEdit = 1;
                    }
                    if (Role != null && Role.Name.Contains("采购"))
                    {
                        ViewBag.isShowNew = 1;
                        ViewBag.isShowEdit = 1;
                    }
                }
            ViewBag.searchStatus = list;
            return View(rtnlist);
        }


        public ActionResult CheckList(int? pageIndex)
        {
            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(null, null, null);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

            ViewBag.statusList = new List<string>();


            //0:待审核,1:审核通过,2:审核未通过,3正常,4即将过期,5已经过期
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="全部",Value="-1"},
                new SelectListItem(){Text="待审核",Value="0"},
                //new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"},
                new SelectListItem(){Text="正常",Value="3"},
                new SelectListItem(){Text="即将过期",Value="4"},
                new SelectListItem(){Text="已经过期",Value="5"}
            };

            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Suser.Id);
            IList<SysRole> RoleList = sysp.Role;

            ViewBag.isShowChcek = 0;
            ViewBag.isShowView = 0;
            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null && Role.Name.Contains("品保管理员"))
                    {
                        ViewBag.isShowChcek = 1;
                        ViewBag.isShowView = 1;
                    }
                }

            ViewBag.searchStatus = list;

            return View(rtnlist);
        }

        public ActionResult SupplierAttatchList(int? pageIndex)
        {
            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(null, null, null);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

            ViewBag.statusList = new List<string>();


            //0:待审核,1:审核通过,2:审核未通过,3正常,4即将过期,5已经过期
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="全部",Value="-1"},
                new SelectListItem(){Text="待审核",Value="0"},
                //new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"},
                new SelectListItem(){Text="正常",Value="3"},
                new SelectListItem(){Text="即将过期",Value="4"},
                new SelectListItem(){Text="已经过期",Value="5"}
            };

            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Suser.Id);
            IList<SysRole> RoleList = sysp.Role;

            ViewBag.isShowChcek = 0;
            ViewBag.isShowView = 0;

            ViewBag.searchStatus = list;

            return View(rtnlist);
        }

        public ActionResult QualifiedList(int? pageIndex)
        {
            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(null, null, null);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            if (int.Parse(Suser.RoleType) == 1 || int.Parse(Suser.RoleType) == 0)
            {
                ViewBag.isShow = 1;
            }
            else
            {
                ViewBag.isShow = 0;
            }


            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);
            listmodel = listmodel.Where(w => w.supplierStatus.iStatus == 3 || w.supplierStatus.iStatus == 4).ToList();

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

            ViewBag.statusList = new List<string>();

            //0:待审核,1:审核通过,2:审核未通过,3正常,4即将过期,5已经过期
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="全部",Value="-1"},
                new SelectListItem(){Text="待审核",Value="0"},
                //new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"},
                new SelectListItem(){Text="正常",Value="3"},
                new SelectListItem(){Text="即将过期",Value="4"},
                new SelectListItem(){Text="已经过期",Value="5"}
            };

            ViewBag.searchStatus = list;

            return View(rtnlist);
        }


        /// <summary>
        /// 删除页面
        /// </summary>
        /// <param name="id">删除id</param>
        /// <returns>跳转列表页面</returns>
        [HttpPost]
        public ActionResult Delete(string supplierid)
        {
            try
            {
                //操作是否成功 
                //string id = Request.QueryString["id"].ToString();
                NQ_OASupplierView model = _INQ_OASupplierDao.GetSupplierById(supplierid) as NQ_OASupplierView;

                if (null != model)
                {
                    if (model.FIsChecked == 1)
                    {
                        return Json(new { result = "ischecked" });
                    }

                    SessionUser Suser = Session[SessionHelper.User] as SessionUser;

                    if (_INQ_OASupplierDao.NDelete(model))
                    {
                        NAHelper.Insertczjl(model.Id.ToString(), "删除供应商:" + (model.FName == null ? "" : model.FName), Suser.Id);
                        //return RedirectToAction("List");
                        return Json(new { result = "success" });
                    }
                    else
                        return Json(new { result = "fail" });
                }
                return Json(new { result = "fail" });
            }
            catch
            {
                return Json(new { result = "error" });
            }
            //return View("../NQ_OASupplier/List");
        }

        /// <summary>
        /// 保存更新供应商
        /// </summary>
        /// <param name=""></param>
        /// <returns>跳转编辑页面</returns>
        [HttpPost]
        public JsonResult updateOASupplier(FrameController from)
        {
            string Id = Request.Form["Id"];
            string FAddress = Request.Form["FAddress"];
            string FContact = Request.Form["FContact"];
            string FName = Request.Form["FName"];
            string FPhone = Request.Form["FPhone"];


            if (FAddress == "")
            { return Json(new { result = "FAddress", id = Id }); }

            if (FContact == "")
            { return Json(new { result = "FContact", id = Id }); }

            if (FName == "")
            { return Json(new { result = "FName", id = Id }); }

            if (FPhone == "")
            { return Json(new { result = "FPhone", id = Id }); }

            string rtnid = "";
            NQ_OASupplier smodel = new NQ_OASupplier();

            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //修改
                if ((int.Parse(Id)) > 0)
                {
                    smodel = _INQ_OASupplierDao.GetSupplierByIdNoView(Id);

                    //smodel = _INQ_OASupplierDao.GetSupplierById(Id);
                    smodel.FAddress = FAddress;
                    smodel.FContact = FContact;
                    smodel.FMobilePhone = Request.Form["FMobilePhone"];
                    smodel.FName = FName;
                    smodel.FPhone = FPhone;
                    smodel.FSupplierType = int.Parse(Request.Form["FSupplierType"]);
                    smodel.FUpdateUser = Convert.ToString(user.UserName);
                    smodel.FUpdateDate = DateTime.Now;
                    smodel.FIsChecked = 0;

                    flay = _INQ_OASupplierDao.baseUpdate(smodel);
                    NAHelper.Insertczjl(smodel.Id.ToString(), "更新供应商:" + (smodel.FName == null ? "" : smodel.FName), user.Id);

                    rtnid = smodel.Id.ToString();
                }
                //添加
                else
                {

                    smodel.FAddress = FAddress;
                    smodel.FContact = FContact;
                    smodel.FMobilePhone = Request.Form["FMobilePhone"];
                    smodel.FName = FName;
                    smodel.FNumber = Guid.NewGuid().ToString();
                    smodel.FPhone = FPhone;
                    smodel.FSupplierType = int.Parse(Request.Form["FSupplierType"]);
                    smodel.FCreateUser = Convert.ToString(user.UserName);
                    smodel.FCreateDate = DateTime.Now;
                    smodel.FUpdateDate = DateTime.Now;
                    smodel.FIsChecked = 0;

                    flay = _INQ_OASupplierDao.baseInsert(smodel);

                    NQ_OASupplierView insertedmodel = _INQ_OASupplierDao.GetSupplierByFNumber(smodel.FNumber);
                    NAHelper.Insertczjl(smodel.Id.ToString(), "新增供应商:" + (smodel.FName == null ? "" : smodel.FName), user.Id);
                    rtnid = insertedmodel.Id.ToString();
                }

                if (flay)
                {
                    return Json(new { result = "success", id = rtnid }, "text/html");
                }
                else
                    return Json(new { result = "error", id = Id }, "text/html");
            }
            catch (Exception e)
            {
                return Json(new { result = "error", id = Id }, "text/html");
            }
        }

        /// <summary>
        /// 审核供应商
        /// </summary>
        /// <param name=""></param>
        /// <returns>json数据</returns>
        [HttpPost]
        public JsonResult checkOASupplier(FrameController from)
        {
            string Id = Request.Form["Id"];
            string check = Request.Form["checkList"];
            string rtnid = "";

            NQ_OASupplier smodel = new NQ_OASupplier();
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //审核
                if ((int.Parse(Id)) > 0)
                {
                    smodel = _INQ_OASupplierDao.GetSupplierByIdNoView(Id);
                    smodel.FIsChecked = int.Parse(check);
                    smodel.FUpdateUser = Convert.ToString(user.UserName);
                    smodel.FUpdateDate = DateTime.Now;

                    //flay = _INQ_OASupplierDao.NUpdate(smodel);
                    flay = _INQ_OASupplierDao.baseUpdate(smodel);
                    NAHelper.Insertczjl(smodel.Id.ToString(), "审核供应商:" + (smodel.FName == null ? "" : smodel.FName), user.Id);

                    rtnid = smodel.Id.ToString();
                }

                if (flay)
                {
                    return Json(new { result = "success", id = rtnid }, "text/html");
                }
                else
                    return Json(new { result = "error", id = Id }, "text/html");
            }
            catch (Exception e)
            {
                return Json(new { result = "error", id = Id }, "text/html");
            }
        }

        /// <summary>
        ///跳转新建页面
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult NewPage()
        {
            NQ_OASupplierView oasupplier = new NQ_OASupplierView();

            return View("Edit", oasupplier);
        }

        /// <summary>
        ///跳转编辑页面
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_OASupplierView oasupplier = new NQ_OASupplierView();

            IList<NQ_SupplierAttachmentView> attachments = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                oasupplier = _INQ_OASupplierDao.GetSupplierById(id);

                oasupplier.supplierStatus = _INQ_OASupplierDao.setSuStatus(oasupplier);

                attachments = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);

                ViewBag.attachments = attachments;

                //ViewBag.FLicenceExist = 0; // 营业执照是否存在
                //ViewBag.FTaxNumExist = 0; //税号
                //ViewBag.FISO9001Exist = 0; //iso9001
                //ViewBag.FISO14000Exist = 0; //iso14000
                //ViewBag.FPatentExist = 0; //专利证书
                //ViewBag.FOtherExist = 0; //其他证书
                //ViewBag.FQuestionnaireExist = 0; //供应商调查表
                //ViewBag.FAgentExist = 0; //代理证
                //ViewBag.FSampleEvaluationExist = 0; //样品评估
                //ViewBag.FTrialProductionExist = 0; //小批量试产
                //ViewBag.FQualityAgreementExist = 0; //质量协议
                //ViewBag.FSupplierQuestionnaireExist = 0;//供应商评估表

                //ViewBag.FLicencePermanent = 0; // 营业执照是否不过期
                //ViewBag.FTaxNumPermanent = 0; //税号
                //ViewBag.FISO9001Permanent = 0; //iso9001
                //ViewBag.FISO14000Permanent = 0; //iso14000
                //ViewBag.FPatentPermanent = 0; //专利证书
                //ViewBag.FOtherPermanent = 0; //其他证书
                //ViewBag.FQuestionnairePermanent = 0; //供应商调查表
                //ViewBag.FAgentPermanent = 0; //代理证
                //ViewBag.FSampleEvaluationPermanent = 0; //样品评估
                //ViewBag.FTrialProductionPermanent = 0; //小批量试产
                //ViewBag.FQualityAgreementPermanent = 0; //质量协议
                //ViewBag.FSupplierQuestionnairePermanent = 0;//供应商评估表

                //ViewBag.FLicenceCount = 0; // 
                //ViewBag.FTaxNumCount = 0; //
                //ViewBag.FISO9001Count = 0; //
                //ViewBag.FISO14000Count = 0; //
                //ViewBag.FPatentCount = 0; //
                //ViewBag.FOtherCount = 0; //
                //ViewBag.FQuestionnaireCount = 0; //
                //ViewBag.FAgentCount = 0; //
                //ViewBag.FSampleEvaluationCount = 0; //
                //ViewBag.FTrialProductionCount = 0; //
                //ViewBag.FQualityAgreementCount = 0; //
                //ViewBag.FSupplierQuestionnaireCount = 0;//


                //if (attachments != null)
                //{
                //    foreach (var attach in attachments)
                //    {
                //        if (attach.seq == 1)
                //        {
                //            switch (attach.FAttType)
                //            {
                //                case 0:
                //                    ViewBag.FLicenceExist = 1;
                //                    ViewBag.FLicenceURL = attach.FAttURL;
                //                    ViewBag.FLicenceDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FLicencePermanent = attach.isPermanent;
                //                    ViewBag.FLicenceCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 1:
                //                    ViewBag.FTaxNumExist = 1;
                //                    ViewBag.FTaxNumURL = attach.FAttURL;
                //                    ViewBag.FTaxDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FTaxNumPermanent = attach.isPermanent;
                //                    //ViewBag.FTaxCount = ViewBag.FTaxCount + 1;
                //                    ViewBag.FTaxCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 2:
                //                    ViewBag.FISO9001Exist = 1;
                //                    ViewBag.FISO9001URL = attach.FAttURL;
                //                    ViewBag.FISO9001DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FISO9001Permanent = attach.isPermanent;
                //                    //ViewBag.FISO9001Count = ViewBag.FISO9001Count + 1;
                //                    ViewBag.FISO9001Count = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 3:
                //                    ViewBag.FISO14000Exist = 1;
                //                    ViewBag.FISO14000URL = attach.FAttURL;
                //                    ViewBag.FISO14000DeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FISO14000Permanent = attach.isPermanent;
                //                    //ViewBag.FISO14000Count = ViewBag.FISO14000Count + 1;
                //                    ViewBag.FISO14000Count = ViewBag.FISO9001Count = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 4:
                //                    ViewBag.FPatentExist = 1;
                //                    ViewBag.FPatentURL = attach.FAttURL;
                //                    ViewBag.FPatentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FPatentPermanent = attach.isPermanent;
                //                    //ViewBag.FPatentCount = ViewBag.FPatentCount + 1;
                //                    ViewBag.FPatentCount = ViewBag.FISO9001Count = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 5:
                //                    ViewBag.FOtherExist = 1;
                //                    ViewBag.FOtherURL = attach.FAttURL;
                //                    ViewBag.FOtherDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FOtherPermanent = attach.isPermanent;
                //                    //ViewBag.FOtherCount = ViewBag.FOtherCount + 1;
                //                    ViewBag.FOtherCount = ViewBag.FISO9001Count = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 6:
                //                    ViewBag.FQuestionnaireExist = 1;
                //                    ViewBag.FQuestionnaireURL = attach.FAttURL;
                //                    ViewBag.FQuestionnaireDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FQuestionnairePermanent = attach.isPermanent;
                //                    //ViewBag.FQuestionnaireCount = ViewBag.FQuestionnaireCount + 1;
                //                    ViewBag.FQuestionnaireCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 7:
                //                    ViewBag.FAgentExist = 1;
                //                    ViewBag.FAgentURL = attach.FAttURL;
                //                    ViewBag.FAgentDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FAgentPermanent = attach.isPermanent;
                //                    //ViewBag.FAgentCount = ViewBag.FAgentCount + 1;
                //                    ViewBag.FAgentCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();
                //                    break;
                //                case 8:
                //                    ViewBag.FSampleEvaluationExist = 1;
                //                    ViewBag.FSampleEvaluationURL = attach.FAttURL;
                //                    ViewBag.FSampleEvaluationDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FSampleEvaluationPermanent = attach.isPermanent;
                //                    //ViewBag.FSampleEvaluationCount = ViewBag.FSampleEvaluationCount + 1;
                //                    ViewBag.FSampleEvaluationCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();

                //                    break;
                //                case 9:
                //                    ViewBag.FTrialProductionExist = 1;
                //                    ViewBag.FTrialProductionURL = attach.FAttURL;
                //                    ViewBag.FTrialProductionDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FTrialProductionPermanent = attach.isPermanent;
                //                    //ViewBag.FTrialProductionCount = ViewBag.FTrialProductionCount + 1;
                //                    ViewBag.FTrialProductionCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();

                //                    break;
                //                case 10:
                //                    ViewBag.FQualityAgreementExist = 1;
                //                    ViewBag.FQualityAgreementURL = attach.FAttURL;
                //                    ViewBag.FQualityAgreementDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FQualityAgreementPermanent = attach.isPermanent;
                //                    //ViewBag.FQualityAgreementCount = ViewBag.FQualityAgreementCount + 1;
                //                    ViewBag.FQualityAgreementCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();

                //                    break;
                //                case 11:
                //                    ViewBag.FSupplierQuestionnaireExist = 1;
                //                    ViewBag.FSupplierQuestionnaireURL = attach.FAttURL;
                //                    ViewBag.FSupplierQuestionnaireDeadLine = attach.FAttDeadline == null ? DateTime.Now.Date : attach.FAttDeadline;
                //                    ViewBag.FSupplierQuestionnairePermanent = attach.isPermanent;
                //                    //ViewBag.FSupplierQuestionnaireCount = ViewBag.FSupplierQuestionnaireCount + 1;
                //                    ViewBag.FSupplierQuestionnaireCount = attachments.Where(x => x.FAttType == attach.FAttType).Count();

                //                    break;
                //            }
                //        }
                //    }
                //}
            }

            return View("Edit", oasupplier);
        }



        /// <summary>
        ///跳转审核页面
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns></returns>
        //[QAManagerFilter]
        public ActionResult CheckPage(string id)
        {

            NQ_OASupplierView supplier = new NQ_OASupplierView();
            IList<NQ_SupplierAttachmentView> attachments = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                supplier = _INQ_OASupplierDao.GetSupplierById(id);
                attachments = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);

                List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="待审核",Value="0"},
                new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"}
            };

                ViewBag.attachments = attachments;

                ViewBag.nameList = list;

                ViewBag.FLicenceExist = 0; // 营业执照是否存在
                ViewBag.FTaxNumExist = 0; //税号
                ViewBag.FISO9001Exist = 0; //iso9001
                ViewBag.FISO14000Exist = 0; //iso14000
                ViewBag.FPatentExist = 0; //专利证书
                ViewBag.FOtherExist = 0; //其他证书
                ViewBag.FQuestionnaireExist = 0; //供应商调查表
                ViewBag.FAgentExist = 0; //代理证
                ViewBag.FSampleEvaluationExist = 0; //样品评估
                ViewBag.FTrialProductionExist = 0; //小批量试产
                ViewBag.FQualityAgreementExist = 0; //质量协议
                ViewBag.FSupplierQuestionnaireExist = 0;//供应商评估表


                ViewBag.FLicenceStatusName = "未设置";
                ViewBag.FTaxNumStatusName = "未设置";
                ViewBag.FISO9001StatusName = "未设置";
                ViewBag.FISO14000StatusName = "未设置";
                ViewBag.FPatentStatusName = "未设置";
                ViewBag.FOtherStatusName = "未设置";
                ViewBag.FQuestionnaireStatusName = "未设置";
                ViewBag.FAgentStatusName = "未设置";
                ViewBag.FSampleEvaluationName = "未设置";
                ViewBag.FTrialProductionName = "未设置";
                ViewBag.FQualityAgreementName = "未设置";
                ViewBag.FSupplierQuestionnaireName = "未设置";


                ViewBag.FLicenceStatus = 0;
                ViewBag.FTaxNumStatus = 0;
                ViewBag.FISO9001Status = 0;
                ViewBag.FISO14000Status = 0;
                ViewBag.FPatentStatus = 0;
                ViewBag.FOtherStatus = 0;
                ViewBag.FQuestionnaireStatus = 0;
                ViewBag.FAgentStatus = 0;
                ViewBag.FSampleEvaluationStatus = 0;
                ViewBag.FTrialProductionStatus = 0;
                ViewBag.FQualityAgreementStatus = 0;
                ViewBag.FSupplierQuestionnaireStatus = 0;


                int fstatus = 0;
                suStatus supplierStatus = new suStatus();

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        supplierStatus = _INQ_OASupplierDao.fileDateStatus(attach);
                        switch (attach.FAttType)
                        {
                            case 0:
                                ViewBag.FLicenceStatusName = supplierStatus.strStatusName;
                                ViewBag.FLicenceStatus = supplierStatus.iStatus;
                                break;
                            case 1:
                                ViewBag.FTaxNumStatusName = supplierStatus.strStatusName;
                                ViewBag.FTaxNumStatus = supplierStatus.iStatus;
                                ViewBag.FTaxNumExist = 1;
                                break;
                            case 2:
                                ViewBag.FISO9001StatusName = supplierStatus.strStatusName;
                                ViewBag.FISO9001Status = supplierStatus.iStatus;
                                ViewBag.FISO9001Exist = 1;
                                break;
                            case 3:
                                ViewBag.FISO14000StatusName = supplierStatus.strStatusName;
                                ViewBag.FISO14000Status = supplierStatus.iStatus;
                                ViewBag.FISO14000Exist = 1;
                                break;
                            case 4:
                                ViewBag.FPatentStatusName = supplierStatus.strStatusName;
                                ViewBag.FPatentStatus = supplierStatus.iStatus;
                                ViewBag.FPatentExist = 1;
                                break;
                            case 5:
                                ViewBag.FOtherStatusName = supplierStatus.strStatusName;
                                ViewBag.FOtherStatus = supplierStatus.iStatus;
                                ViewBag.FOtherExist = 1;
                                break;
                            case 6:
                                ViewBag.FQuestionnaireStatusName = supplierStatus.strStatusName;
                                ViewBag.FQuestionnaireStatus = supplierStatus.iStatus;
                                ViewBag.FQuestionnaireExist = 1;
                                break;
                            case 7:
                                ViewBag.FAgentStatusName = supplierStatus.strStatusName;
                                ViewBag.FAgentStatus = supplierStatus.iStatus;
                                ViewBag.FAgentExist = 1;
                                break;
                            case 8:
                                if (ViewBag.FSampleEvaluationExist == 0)
                                { 
                                ViewBag.FSampleEvaluationName = supplierStatus.strStatusName;
                                ViewBag.FSampleEvaluationStatus = supplierStatus.iStatus;
                                ViewBag.FSampleEvaluationExist = 1;
                                }
                                break;
                            case 9:
                                if (ViewBag.FTrialProductionExist == 0)
                                {
                                    ViewBag.FTrialProductionName = supplierStatus.strStatusName;
                                    ViewBag.FTrialProductionStatus = supplierStatus.iStatus;
                                    ViewBag.FTrialProductionExist = 1;
                                }

                                break;
                            case 10:
                                ViewBag.FQualityAgreementName = supplierStatus.strStatusName;
                                ViewBag.FQualityAgreementStatus = supplierStatus.iStatus;
                                ViewBag.FQualityAgreementExist = 1;
                                break;
                            case 11:
                                ViewBag.FSupplierQuestionnaireName = supplierStatus.strStatusName;
                                ViewBag.FSupplierQuestionnaireStatus = supplierStatus.iStatus;
                                ViewBag.FSupplierQuestionnaireExist = 1;
                                break;
                        }
                    }
                }
            }
            return View("Check", supplier);
        }


        public ActionResult SupplierAttatch(string id)
        {

            NQ_OASupplierView supplier = new NQ_OASupplierView();
            IList<NQ_SupplierAttachmentView> attachments = new List<NQ_SupplierAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                supplier = _INQ_OASupplierDao.GetSupplierById(id);
                attachments = _INQ_SupplierAttachmentDao.GetAttachmentBySupplierId(id);

                List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="待审核",Value="0"},
                new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"}
            };

                ViewBag.attachments = attachments;

                ViewBag.nameList = list;

                ViewBag.FLicenceStatusName = "未设置";
                ViewBag.FTaxNumStatusName = "未设置";
                ViewBag.FISO9001StatusName = "未设置";
                ViewBag.FISO14000StatusName = "未设置";
                ViewBag.FPatentStatusName = "未设置";
                ViewBag.FOtherStatusName = "未设置";
                ViewBag.FQuestionnaireStatusName = "未设置";
                ViewBag.FAgentStatusName = "未设置";
                ViewBag.FSampleEvaluationName = "未设置";
                ViewBag.FTrialProductionName = "未设置";
                ViewBag.FQualityAgreementName = "未设置";
                ViewBag.FSupplierQuestionnaireName = "未设置";


                ViewBag.FLicenceExist = 0; // 营业执照是否存在
                ViewBag.FTaxNumExist = 0; //税号
                ViewBag.FISO9001Exist = 0; //iso9001
                ViewBag.FISO14000Exist = 0; //iso14000
                ViewBag.FPatentExist = 0; //专利证书
                ViewBag.FOtherExist = 0; //其他证书
                ViewBag.FQuestionnaireExist = 0; //供应商调查表
                ViewBag.FAgentExist = 0; //代理证
                ViewBag.FSampleEvaluationExist = 0; //样品评估
                ViewBag.FTrialProductionExist = 0; //小批量试产
                ViewBag.FQualityAgreementExist = 0; //质量协议
                ViewBag.FSupplierQuestionnaireExist = 0;//供应商评估表

                ViewBag.FLicenceStatus = 0;
                ViewBag.FTaxNumStatus = 0;
                ViewBag.FISO9001Status = 0;
                ViewBag.FISO14000Status = 0;
                ViewBag.FPatentStatus = 0;
                ViewBag.FOtherStatus = 0;
                ViewBag.FQuestionnaireStatus = 0;
                ViewBag.FAgentStatus = 0;
                ViewBag.FSampleEvaluationStatus = 0;
                ViewBag.FTrialProductionStatus = 0;
                ViewBag.FQualityAgreementStatus = 0;
                ViewBag.FSupplierQuestionnaireStatus = 0;


                suStatus supplierStatus = new suStatus();

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        supplierStatus = _INQ_OASupplierDao.fileDateStatus(attach);

                        switch (attach.FAttType)
                        {
                            case 0:
                                ViewBag.FLicenceStatusName = supplierStatus.strStatusName;
                                ViewBag.FLicenceStatus = supplierStatus.iStatus;
                                break;
                            case 1:
                                ViewBag.FTaxNumStatusName = supplierStatus.strStatusName;
                                ViewBag.FTaxNumStatus = supplierStatus.iStatus;
                                ViewBag.FTaxNumExist = 1;
                                break;
                            case 2:
                                ViewBag.FISO9001StatusName = supplierStatus.strStatusName;
                                ViewBag.FISO9001Status = supplierStatus.iStatus;
                                ViewBag.FISO9001Exist = 1;
                                break;
                            case 3:
                                ViewBag.FISO14000StatusName = supplierStatus.strStatusName;
                                ViewBag.FISO14000Status = supplierStatus.iStatus;
                                ViewBag.FISO14000Exist = 1;
                                break;
                            case 4:
                                ViewBag.FPatentStatusName = supplierStatus.strStatusName;
                                ViewBag.FPatentStatus = supplierStatus.iStatus;
                                ViewBag.FPatentExist = 1;
                                break;
                            case 5:
                                ViewBag.FOtherStatusName = supplierStatus.strStatusName;
                                ViewBag.FOtherStatus = supplierStatus.iStatus;
                                ViewBag.FOtherExist = 1;
                                break;
                            case 6:
                                ViewBag.FQuestionnaireStatusName = supplierStatus.strStatusName;
                                ViewBag.FQuestionnaireStatus = supplierStatus.iStatus;
                                ViewBag.FQuestionnaireExist = 1;
                                break;
                            case 7:
                                ViewBag.FAgentStatusName = supplierStatus.strStatusName;
                                ViewBag.FAgentStatus = supplierStatus.iStatus;
                                ViewBag.FAgentExist = 1;
                                break;
                            case 8:
                                if (ViewBag.FSampleEvaluationExist == 0)
                                {
                                    ViewBag.FSampleEvaluationName = supplierStatus.strStatusName;
                                    ViewBag.FSampleEvaluationStatus = supplierStatus.iStatus;
                                    ViewBag.FSampleEvaluationExist = 1;
                                }
                                break;
                            case 9:
                                if (ViewBag.FTrialProductionExist == 0)
                                {
                                    ViewBag.FTrialProductionName = supplierStatus.strStatusName;
                                    ViewBag.FTrialProductionStatus = supplierStatus.iStatus;
                                    ViewBag.FTrialProductionExist = 1;
                                }
                                break;
                            case 10:
                                ViewBag.FQualityAgreementName = supplierStatus.strStatusName;
                                ViewBag.FQualityAgreementStatus = supplierStatus.iStatus;
                                ViewBag.FQualityAgreementExist = 1;
                                break;
                            case 11:
                                ViewBag.FSupplierQuestionnaireName = supplierStatus.strStatusName;
                                ViewBag.FSupplierQuestionnaireStatus = supplierStatus.iStatus;
                                ViewBag.FSupplierQuestionnaireExist = 1;
                                break;
                        }
                    }
                }
            }
            return View("SupplierAttatch", supplier);
        }

        /// <summary>
        ///搜索list
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public JsonResult SearchList()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            string name = Request.Form["txtSearch_Name"];
            string con = Request.Form["txtSearch_Con"];
            string tel = Request.Form["txtSearch_Tel"];
            string status = Request.Form["statusList"];
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);

            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(name, con, tel);

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);

            if (!string.IsNullOrEmpty(status) && status != "-1")
            {
                listmodel = listmodel.Where(w => w.supplierStatus.iStatus == int.Parse(status)).ToList();
            }

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, pageIndex, 11);

            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(rtnlist.CurrentPageIndex, rtnlist.PageSize, rtnlist.RecordCount);

            if (rtnlist != null)
                return Json(new { result = rtnlist.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        /// <summary>
        ///搜索list
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public JsonResult SearchQaList()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            string name = Request.Form["txtSearch_Name"];

            int pageIndex = 1;

            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);

            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(name, null, null);

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = _INQ_OASupplierDao.setItemCount(listmodel);
            listmodel = listmodel.Where(w => w.supplierStatus.iStatus == 3 || w.supplierStatus.iStatus == 4).ToList();

            PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, pageIndex, 11);

            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(rtnlist.CurrentPageIndex, rtnlist.PageSize, rtnlist.RecordCount);

            if (rtnlist != null)
                return Json(new { result = rtnlist.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        public ActionResult setUncheck(string supplierid)
        {
            try
            {
                NQ_OASupplier smodel = _INQ_OASupplierDao.GetSupplierByIdNoView(supplierid);

                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //设置成未审核
                smodel.FIsChecked = 2;

                flay = _INQ_OASupplierDao.baseUpdate(smodel);
                NAHelper.Insertczjl(smodel.Id.ToString(), "审核供应商未通过:" + (smodel.FName == null ? "" : smodel.FName), user.Id);

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

        public ActionResult setCheck(string supplierid)
        {
            try
            {
                NQ_OASupplier smodel = _INQ_OASupplierDao.GetSupplierByIdNoView(supplierid);

                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //设置成审核
                smodel.FIsChecked = 1;

                flay = _INQ_OASupplierDao.baseUpdate(smodel);
                NAHelper.Insertczjl(smodel.Id.ToString(), "审核供应商:" + (smodel.FName == null ? "" : smodel.FName), user.Id);

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




        public FileResult QualifiedItemList(string txtSearch_Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(txtSearch_Name, null, null);

            listmodel = _INQ_OASupplierDao.setListStatus(listmodel);
            listmodel = listmodel.Where(w => w.supplierStatus.iStatus == 3 || w.supplierStatus.iStatus == 4).ToList();

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("供应商");
            row1.CreateCell(1).SetCellValue("联系人");
            row1.CreateCell(2).SetCellValue("电话");
            row1.CreateCell(3).SetCellValue("手机");
            row1.CreateCell(4).SetCellValue("地址");
            row1.CreateCell(5).SetCellValue("元器件名称");
            row1.CreateCell(6).SetCellValue("元器件型号");
            row1.CreateCell(7).SetCellValue("状态");

            if (listmodel != null)//数据不为空
            {
                int k = 0;
                for (int i = 0; i < listmodel.Count; i++)
                {
                    IList<NQ_YJinfoView> itemlist = _INQ_YJinfoDao.GetItemsWithSupplier(listmodel[i].Id.ToString());

                    for (int j = 0; j < itemlist.Count; j++)
                    {
                        k++;
                        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(k);
                        rowtemp.CreateCell(0).SetCellValue(listmodel[i].FName);
                        rowtemp.CreateCell(1).SetCellValue(listmodel[i].FContact);
                        rowtemp.CreateCell(2).SetCellValue(listmodel[i].FPhone);
                        rowtemp.CreateCell(3).SetCellValue(listmodel[i].FMobilePhone);
                        rowtemp.CreateCell(4).SetCellValue(listmodel[i].FAddress);
                        rowtemp.CreateCell(5).SetCellValue(itemlist[j].Y_Name);
                        rowtemp.CreateCell(6).SetCellValue(itemlist[j].Y_Ggxh);
                        rowtemp.CreateCell(7).SetCellValue(listmodel[i].supplierStatus.strStatusName);
                    }
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "合格供应商明细.xls");
        }

        private PagerInfo<NQ_OASupplierView> GetListPager(int? pageIndex, string suName, string suCon, string suTel, string suStatus)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_OASupplierDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_OASupplierDao.SetPagerPageSize(11);
            PagerInfo<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getListByNameConTel(suName, suCon, suTel, suStatus, Suser);
            return listmodel;
        }



    }
}
