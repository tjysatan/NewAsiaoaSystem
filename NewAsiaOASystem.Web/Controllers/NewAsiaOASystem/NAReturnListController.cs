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
using System.IO;
using Newtonsoft.Json.Linq;
using NewAsiaOASystem.DBModel;
using NPOI.SS.Formula.Functions;

namespace NewAsiaOASystem.Web.Controllers
{
    /// <summary>
    /// 返退货但管理
    /// </summary>
    public class NAReturnListController : Controller
    {
        //
        // GET: /NAReturnList/
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INQR_ProductDao _INQR_ProductDao = ContextRegistry.GetContext().GetObject("NQR_ProductDao") as INQR_ProductDao;
        INQ_THinfoFXDao _INQ_THinfoFXDao = ContextRegistry.GetContext().GetObject("NQ_THinfoFXDao") as INQ_THinfoFXDao;
        INQ_CHdetailinfoDao _INQ_CHdetailinfoDao = ContextRegistry.GetContext().GetObject("NQ_CHdetailinfoDao") as INQ_CHdetailinfoDao;
        INQ_ThdetailinfoDao _INQ_ThdetailinfoDao = ContextRegistry.GetContext().GetObject("NQ_ThdetailinfoDao") as INQ_ThdetailinfoDao;
        INQ_BlinfoDao _INQ_BlinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlinfoDao") as INQ_BlinfoDao;
        INQ_BlxxinfoDao _INQ_BlxxinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlxxinfoDao") as INQ_BlxxinfoDao;
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        INQ_TjFtwxCPTypeSuminfoDao _INQ_TjFtwxCPTypeSuminfoDao = ContextRegistry.GetContext().GetObject("NQ_TjFtwxCPTypeSuminfoDao") as INQ_TjFtwxCPTypeSuminfoDao;
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
        INQ_GysInfoDao _INQ_GysInfoDao = ContextRegistry.GetContext().GetObject("NQ_GysInfoDao") as INQ_GysInfoDao;
        INA_SharealikeDao _INA_SharealikeDao=ContextRegistry.GetContext().GetObject("NA_SharealikeDao") as INA_SharealikeDao;


        public ActionResult Index()
        {
            return View();
        }

        #region //仓库根据客户 登记退货信息  客户列表
        public ActionResult ckkdlist(int? pageIndex)
        {

            PagerInfo<NACustomerinfoView> listmodel = GetCustinfo(pageIndex, null, null,"1",null,null);
            return View(listmodel);
        } 
        #endregion

        #region //客户信息 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NACustomerinfoView> GetCustinfo(int? pageIndex, string Name, string lxrname,string Isjy,string Tel,string Isds)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(11);
            PagerInfo<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetCinfoList(Name, lxrname, null, Isjy,Tel,Isds, Suser);
            return listmodel;
        }
        #endregion

        //条件查询
        public JsonResult ckSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//单位名称
            string lxrName = Request.Form["txtSearch_LXname"];//联系人姓名
           string Isjy=Request.Form["Isjy"];//是否启用
            string Tel=Request.Form["Tel"];//联系方式
            string Isds=Request.Form["Isds"];//是否电商
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NACustomerinfoView> listmodel = GetCustinfo(pageIndex, Name, lxrName,Isjy,Tel,Isds);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //仓库 返退货单管理
        //列表页面
        public ActionResult List(int? pageIndex)
        {
            Session[SessionHelper.SSerch] = null;
           // Session.Remove(SessionHelper.SSerch);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex,"0",null,null,null,null,null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string sTZ = Request.Form["ztstr"];
            string R_PId = Request.Form["R_PId"];
            string fthbianhao = Request.Form["txt_Fthbianhao"];
            string CPname = Request.Form["txt_CPname"];
            if (sTZ == "qt") {
                sTZ = null;
            }
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "0", Name, sTZ, R_PId,fthbianhao,CPname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //仓库编辑返退单
        /// <summary>
        /// 仓库编辑返退单
        /// </summary>
        /// <returns></returns>
        public ActionResult CKaddPage()
        {
            AlbumDropdown(null);
            return View("ckEdit", new NAReturnListView());
        } 
        #endregion

        #endregion

        #region //仓库编辑返退保存
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model = _INAReturnListDao.NGetModelById(Request.Form["R_Id"]);//根据ID查找
                model.R_Number =Convert.ToInt32(Request.Form["zsl"]);
                flay = _INAReturnListDao.NUpdate(model);
                if (flay)
                {
                    //查询客户名称
                    NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(model.C_Id);
                    //返退编号+客户名称
                    string str = model.fthbianhao;
                    if (Cusmodel != null)
                    {
                        str = model.fthbianhao + "(" + Cusmodel.Name + ")";
                    }
                    MassManager.FMB_FTtypeupdateNotice("0,1", str, model.L_type.ToString());//发送微信提醒
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }
            catch
            {
                return Json(new { result = "error" });
            }
        } 

        //仓库提交保存 ajax
        public string CKajaxEdit(string r_Id, string zsl)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                NAReturnListView model = _INAReturnListDao.NGetModelById(r_Id);//根据Id查找返退货单数据
                model.R_Number = Convert.ToInt32(zsl);//退货总数量
                flay = _INAReturnListDao.NUpdate(model);
                if (flay)
                {
                    return "0";//保存成功
                }
                else
                {
                    return "1";//提交失败
                }
            }
            catch
            {
                return "2";//提交异常
            }
        }
        #endregion

        public ActionResult CKEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            AlbumDropdown(NARmodel.C_Id);
            return View("ckEdit", NARmodel);
        }

        //仓库 登记返退货保存返退货流程单并且跳转到 编辑退货产品明细页面
        public ActionResult CkkdEit(string C_Id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            NAReturnListView model = new NAReturnListView();
            model.C_Id = C_Id;//客户名称
            model.CreatePerson = user.Id;//仓库Id
            model.CreateTime = DateTime.Now;//仓库开单时间
            model.L_type = 0;//未处理状态
            //当天时间
            string Newdatestr = DateTime.Now.ToString("yyyyMMdd");
            string Newdatecountstr = (_INAReturnListDao.GetPPcount()+1).ToString();
            model.fthbianhao = Newdatestr +"-"+ Newdatecountstr;//返退货编号
            model.Status = 0;//启用
            string R_Id = _INAReturnListDao.InsertID(model);
            ViewData["R_Id"] = R_Id;
            ViewData["C_Id"] = C_Id;
            return View("CkkdView", ViewData["R_Id"]);
        }

        //仓库编辑 退货明细页面
        public ActionResult CkkdView(string Id)
        {
           // string R_Id =Convert.ToString(ViewData["R_Id"]);
            ViewData["R_Id"] = Id;
            return View("CkkdView");
        }

        /// <summary>
        /// 查询返退货流程单信息 json 
        /// </summary>
        /// <param name="Id">返退货流程ID</param>
        /// <returns></returns>
        public string GetReturnmodeljson(string Id) {
            string json;
            json =JsonConvert.SerializeObject(_INAReturnListDao.NGetModelById(Id));
            return json;
        }
        
        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NAReturnListView> GetListPager(int? pageIndex,string type, string Name,string Szt,string R_pId,string fthbianhao,string CPname)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INAReturnListDao.SetPagerPageIndex(CurrentPageIndex);
            _INAReturnListDao.SetPagerPageSize(11);
            PagerInfo<NAReturnListView> listmodel = _INAReturnListDao.GetCinfoList(Name,Szt,type,R_pId,fthbianhao,CPname,Suser);
            return listmodel;
        }
        #endregion

        #region  返退货原因初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedPID)
        {
            List<NACustomerinfoView> ReasonlistView = _INACustomerinfoDao.GetlistCust() as List<NACustomerinfoView>;
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
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        /// <summary>
        /// 流程跟踪页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LCGZ(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("LCGZ", NARmodel);
        }

        #region //客服流程跟踪单管理列表
        public ActionResult kflist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex,"0", null,"0",null,null,null);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["roletype"] = user.RoleType;//获取当前登录角色的类型
            return View(listmodel);
        } 
        #endregion

        #region //客服编辑返退货流程单 页面

        public ActionResult kfEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            cpDropdown(NARmodel.R_pId);
            return View("kfEditPage", NARmodel);
        } 
        #endregion

        #region //客服编辑返退保存
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult kfEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                    NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                    Rlmodel.R_pId = Request.Form["PerListData"];//产品类型
                    Rlmodel.R_FTdate = model.R_FTdate;//返退货时间
                    Rlmodel.R_FTyyp = Request.Form["SelectedCLData"];//返退原因1
                    if (Request.Form["SelectedZTData"] != "")
                    {
                        Rlmodel.R_FTyy = Request.Form["SelectedZTData"];//返退原因2
                    }
                    Rlmodel.R_FTyysm = model.R_FTyysm;//其他原因
                    Rlmodel.R_CLjg = Request.Form["SelectedyqclData"];
                    if (Request.Form["SelectedyqclData"] == "1")
                    {
                        Rlmodel.R_CLFY = model.R_CLFY;//翻新入库后减帐费用
                    }
                    if (Request.Form["SelectedyqclData"] == "2")
                    {
                        Rlmodel.R_CLQTSM = model.R_CLQTSM;//其他要求处理的结果
                    }
                    Rlmodel.R_Yf = model.R_Yf;//运费
                    Rlmodel.R_sp = model.R_sp;//客户索赔
                    Rlmodel.Remark1 = model.Remark1;//补充要求
                    //查询客户名称
                    NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(model.C_Id);
                    //返退编号+客户名称
                    string str = model.fthbianhao;
                    if (Cusmodel != null)
                    {
                        str = model.fthbianhao + "(" + Cusmodel.Name + ")";
                    }
                    if (user.RoleType == "1" || user.RoleType=="0")
                    {
                        if (model.Kfzy != null)
                        {
                            Rlmodel.Kfzy = model.Kfzy;//客服专员
                            Rlmodel.ClDate = DateTime.Now;//客服专员处理日期
                        }
                        else
                        {
                            Rlmodel.Kfzy = Convert.ToString(user.Id);//客服专员
                            Rlmodel.ClDate = DateTime.Now;//客服专员处理日期
                        }
                        Rlmodel.ClDate = model.ClDate;//
                        Rlmodel.L_type = 2;//待维修
                        Rlmodel.Kfzg = Convert.ToString(user.Id);//客服主管
                        Rlmodel.Kfzgqrdate = DateTime.Now;//客服主管确认日期
                       
                        if (Rlmodel.R_pId == "c9ab3dfcc90e4feb9bfb93b8efb73cec")//电控箱
                        {
                            MassManager.FMB_FTtypeupdateNotice("2", str, Rlmodel.L_type.ToString());//给维修的发送
                        }
                        else if (Rlmodel.R_pId == "7bb3c225a0564b49816c2a36144262af")//温控器
                        {
                            MassManager.FMB_FTtypeupdateNotice("3", str, Rlmodel.L_type.ToString());//给维修的发送
                        }
                        else if (Rlmodel.R_pId == "c962d65ae3df45b3ac4f898d3e2f4456")//其他
                        {
                            MassManager.FMB_FTtypeupdateNotice("4", str, Rlmodel.L_type.ToString());//给维修的发送
                        }
                        else
                        {
                            MassManager.FMB_FTtypeupdateNotice("2,3,4", str, Rlmodel.L_type.ToString());//给维修的发送
                        }
                    }
                    else
                    {
                        Rlmodel.Kfzy = Convert.ToString(user.Id);//客服专员
                        Rlmodel.ClDate = DateTime.Now;//客服专员处理日期
                        Rlmodel.L_type = 1;//待确定
                        MassManager.FMB_FTtypeupdateNotice("1", str, Rlmodel.L_type.ToString());
                    }
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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

        #region //客服主管一键确认
        public ActionResult Onekeyconfirm()
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            try {
                string id = Request.QueryString["id"].ToString();
                List<NAReturnListView> modellist = _INAReturnListDao.NGetList_id(id) as List<NAReturnListView>;
                if (null != modellist)
                {
                    for (int i = 0, j = modellist.Count; i < j; i++)
                    {
                        //查询客户名称
                        NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(modellist[i].C_Id);
                        //返退编号+客户名称
                        string str = modellist[i].fthbianhao;
                        if (Cusmodel != null)
                        {
                            str = modellist[i].fthbianhao + "(" + Cusmodel.Name + ")";
                        }
                        if (modellist[i].L_type == 1)
                        {
                            modellist[i].L_type = 2;//待维修
                            modellist[i].Kfzg = Convert.ToString(user.Id);//客服主管
                            modellist[i].Kfzgqrdate = DateTime.Now;//客服主管确认日期
                            if (modellist[i].R_pId == "c9ab3dfcc90e4feb9bfb93b8efb73cec")//电控箱
                            {
                                MassManager.FMB_FTtypeupdateNotice("2", str, modellist[i].L_type.ToString());//给维修的发送
                            }
                            else if (modellist[i].R_pId == "7bb3c225a0564b49816c2a36144262af")//温控器
                            {
                                MassManager.FMB_FTtypeupdateNotice("3", str, modellist[i].L_type.ToString());//给维修的发送
                            }
                            else if (modellist[i].R_pId == "c962d65ae3df45b3ac4f898d3e2f4456")//其他
                            {
                                MassManager.FMB_FTtypeupdateNotice("4", str, modellist[i].L_type.ToString());//给维修的发送
                            }
                            else
                            {
                                MassManager.FMB_FTtypeupdateNotice("2,3,4",str, modellist[i].L_type.ToString());//给维修的发送
                            }
                            _INAReturnListDao.NUpdate(modellist[i]);
                        }
                    }
                }
            }
            catch
            {
                return Content("<script>alert('确认失败！');window.history.back();</script>");
            }
            return RedirectToAction("kflist");
        }
        #endregion

        #region  返退货原因初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID"></param>
        public void cpDropdown(string SelectedPID)
        {
            List<NQR_ProductView> ReasonlistView = _INQR_ProductDao.NGetList() as List<NQR_ProductView>;
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
                ViewData["perList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["perList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //删除反退货数据
        public ActionResult DeleteFTH()
        {
            string url;
            try
            {
                string id = Request.QueryString["id"].ToString();
                url = Request.QueryString["dz"].ToString();
                List<NAReturnListView> modellist = _INAReturnListDao.NGetList_id(id) as List<NAReturnListView>;
                if (null != modellist)
                {
                    for (int i = 0, j = modellist.Count; i < j; i++) 
                    {
                      
                            modellist[i].Status = 1;//禁用
                            _INAReturnListDao.NUpdate(modellist[i]);
                            List<NQ_THinfoFXView> FTmxlist = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(modellist[i].Id) as List<NQ_THinfoFXView>;
                            if (FTmxlist!=null)
                            {
                                _INQ_THinfoFXDao.NDelete(FTmxlist);
                            }
                    }
                }
            }
            catch
            {
                return Content("<script>alert('删除失败！');window.history.back();</script>");
            }
            return RedirectToAction("" + url + "");
        }
        #endregion

        /*车间维修*/

        #region //车间返退货维修管理 list
        public ActionResult wxlist(int? pageIndex)
        {

            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "1", null,"2",null,null,null);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["roletype"] = user.RoleType;//获取当前登录角色的类型
            cpDropdown(null);//返退产品
            return View(listmodel);
        }

        public JsonResult wxSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string Szt = Request.Form["ztstr"];
            string R_PId = Request.Form["R_PId"];
            string fthbianhao=Request.Form["txt_Fthbianhao"];
            string CPname = Request.Form["txt_CPname"];
            if (Szt == "qt")
            {
                Szt = null;
            }
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "1", Name, Szt, R_PId, fthbianhao,CPname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //车间返退货维修编辑页面
        public ActionResult wxEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("wxEditPage", NARmodel);
        } 
        #endregion

        #region //车间维修编辑返退保存
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult wxEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                int fptype =Convert.ToInt32(Request.Form["SelectedftypeData"]);//返退类型 （0 维修 1 返退）
                string Iswxwctype = Request.Form["isxwtype"];//是否维修完成 0完成 1还有配件待维修
                Rlmodel.FTtype = fptype;//返退类型
                if (fptype == 0)//判断返退类型是 维修
                {
                    Rlmodel.R_isbxqn =Convert.ToInt32(Request.Form["SelectedsfzbData"]);//是否在保修期内
                    Rlmodel.R_Fxjlcon = null;//翻新记录为空
                    Rlmodel.R_Pzpd = null;//品质判定为空
                }else if(fptype==1){//判断返退类型 翻新
                    Rlmodel.R_isbxqn = null;//是否在保为空
                    Rlmodel.R_Fxjlcon = model.R_Fxjlcon;//翻新记录
                    var pzpd = Convert.ToInt32(Request.Form["SelectedpzpdData"]);//品质判定
                    Rlmodel.R_Pzpd = pzpd;
                    if (pzpd == 3)
                    { //其他原因
                        Rlmodel.R_qtqksm = model.R_qtqksm;
                    }
                    else
                    {
                        Rlmodel.R_qtqksm = "";
                    }

                }
                Rlmodel.R_YQJ = model.R_YQJ;//元器件 
                Rlmodel.R_RGF = model.R_RGF;//人工费
                Rlmodel.R_BCF = model.R_BCF;//包材费
                Rlmodel.R_XJ = hj(model.R_YQJ, model.R_RGF, model.R_BCF);//合计
                //查询客户名称
                NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(Rlmodel.C_Id);
                //返退编号+客户名称
                string str = Rlmodel.fthbianhao;
                if (Cusmodel != null)
                {
                    str = Rlmodel.fthbianhao + "(" + Cusmodel.Name + ")";
                }
                if (Iswxwctype == "0")//全部维修完成
                {
                    if (_INQ_THinfoFXDao.JcTHFXweichulibyr_Id(Rlmodel.Id))
                    {
                        Rlmodel.L_type = 3;//待定责
                        MassManager.FMB_FTtypeupdateNotice("5", str, Rlmodel.L_type.ToString());
                    }
                    else {
                        return Json(new { result = "error1" });
                    }

                }
                if (Iswxwctype == "1")
                {
                    Rlmodel.L_type = 7;//维修未完成 配件待维修
                    MassManager.FMB_FTtypeupdateNotice("4", str, Rlmodel.L_type.ToString());
                }
                Rlmodel.R_CJFZR =Convert.ToString(user.Id);//电气车间负责人
                Rlmodel.R_cjcldate = DateTime.Now;//维修时间
                flay = _INAReturnListDao.NUpdate(Rlmodel);
                //查询分析记录数据
                IList<NQ_THinfoFXView> DXinfo = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Rlmodel.Id);
                if (DXinfo != null)
                {
                    for (int i = 0,j=DXinfo.Count(); i < j; i++)
                    {
                        NQ_THinfoFXView fxmodel = new NQ_THinfoFXView();
                        fxmodel = DXinfo[i];
                        fxmodel.wx_time = DateTime.Now;
                        _INQ_THinfoFXDao.NUpdate(fxmodel);
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

        #region //小计
        public decimal? hj(decimal? a, decimal? c, decimal? b)
        {
            decimal? xj;
            if (a == null)
            {
                a = 0;
            }
            else if (c == null) {
                c = 0;
            }
            else if (b == null) {
                b = 0;
            }
            xj = a + c + b;
            return xj;
        } 
        #endregion


        /*品保定责*/

        #region //返退货产品定责 list
        public ActionResult dzlist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = new PagerInfo<NAReturnListView>();
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (Session[SessionHelper.SSerch] != null)
            {
                FTHCHTJView ssearch = Session[SessionHelper.SSerch] as FTHCHTJView;
                ViewData["Name"] = ssearch.Name;
                ViewData["Szt"] = ssearch.Szt;
                ViewData["R_PId"] = ssearch.R_PId;
                ViewData["fthbianhao"] = ssearch.fthbianhao;
                ViewData["CPname"] = ssearch.CPname;
                ViewData["roletype"] = user.RoleType;
                listmodel = GetListPager(pageIndex,"2",ssearch.Name,ssearch.Szt,ssearch.R_PId,ssearch.fthbianhao,ssearch.CPname);
            }
            else
            {
                listmodel = GetListPager(pageIndex, "2", null, "3", null, null, null);
                ViewData["roletype"] = user.RoleType;//获取当前登录角色的类型
            }
            return View(listmodel);
        }

        public JsonResult dzSearchList()
        {
            Session[SessionHelper.SSerch] = null;
            Session.Remove(SessionHelper.SSerch);
            FTHCHTJView view = new FTHCHTJView();
            view.Name = Request.Form["txtSearch_Name"];
            view.Szt = Request.Form["ztstr"];
            view.R_PId = Request.Form["R_PId"];
            view.fthbianhao = Request.Form["txt_Fthbianhao"];
            view.CPname = Request.Form["txt_CPname"];
            Session[SessionHelper.SSerch] = view;
            //string Name = Request.Form["txtSearch_Name"];
            //string Szt = Request.Form["ztstr"];
            //string R_PId = Request.Form["R_PId"];
            //string fthbianhao = Request.Form["txt_Fthbianhao"];
            //string CPname = Request.Form["txt_CPname"];
            //if (Szt == "qt")
            //{
            //    Szt = null;
            //}
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex,"2",view.Name,view.Szt,view.R_PId,view.fthbianhao,view.CPname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        } 
        #endregion
         
        #region //品保返退货编辑页面
        public ActionResult dzEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("dzEditPage", NARmodel);
        } 
        #endregion

        #region //品保返退货保存
        [HttpPost]
        public JsonResult dzEide(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                Rlmodel.R_bbzrpd = model.R_bbzrpd;//责任判定
                Rlmodel.R_bbzrbm =Request.Form["zrbm"];//责任部门 0 品保部 1技术部 2制造部 3营销部 4其他部门 5客户单位
                Rlmodel.R_bbgly = Convert.ToString(user.Id);//品保管理员
                Rlmodel.R_bbcldate = DateTime.Now;//定责时间
                Rlmodel.L_type = 4;//待处理
                flay = _INAReturnListDao.NUpdate(Rlmodel);
                //查询客户名称
                NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(Rlmodel.C_Id);
                //返退编号+客户名称
                string str = Rlmodel.fthbianhao;
                if (Cusmodel != null)
                {
                    str = Rlmodel.fthbianhao + "(" + Cusmodel.Name + ")";
                }
                MassManager.FMB_FTtypeupdateNotice("0,1", str, Rlmodel.L_type.ToString());
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


        /*营销中心处理*/

        #region //返退货产品营销中心处理意见
        public ActionResult cllist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "3", null,"4",null,null,null);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["roletype"] = user.RoleType;//获取当前登录角色的类型
            return View(listmodel);
        }

        public JsonResult clSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string Szt = Request.Form["ztstr"];
            string R_PId = Request.Form["R_PId"];
            string fthbianhao = Request.Form["txt_Fthbianhao"];
            string CPname = Request.Form["txt_CPname"];
            if (Szt == "qt")
            {
                Szt = null;
            }
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "3", Name, Szt, R_PId, fthbianhao, CPname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }  
        #endregion

        #region //营销部门返退货编辑页面
        public ActionResult clEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("clEditPage", NARmodel);
        } 
        #endregion

        #region //营销中心返退货保存
        [HttpPost]
        public JsonResult clEide(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                Rlmodel.R_xybclyj = model.R_xybclyj;//营销部处理意见
                Rlmodel.R_qyjl = model.R_qyjl;//区域经理
                Rlmodel.R_yxzj = user.Id;//营销总监
                Rlmodel.R_yxcldate = DateTime.Now;//营销部处理意见
               // Rlmodel.L_type = 5;//待审核(取消总经办审核 直接完成)
                Rlmodel.L_type = 6;//完成
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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


        /*经理办公室审核*/

        #region //返退货产品经理办公室审核意见

        public ActionResult shlist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "4", null,null,null,null,null);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["roletype"] = user.RoleType;//获取当前登录角色的类型
            return View(listmodel);
        }

        public JsonResult shSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string Szt = Request.Form["ztstr"];
            string R_PId = Request.Form["R_PId"];
            string fthbianhao = Request.Form["txt_Fthbianhao"];
            string CPname = Request.Form["txt_CPname"];
            if (Szt == "qt")
            {
                Szt = null;
            }
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetListPager(pageIndex, "4", Name, Szt, R_PId, fthbianhao, CPname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }  
        #endregion

        #region //经理办公室返退货编辑页面
        public ActionResult shEditPage(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("shEditPage", NARmodel);
        } 
        #endregion

        #region //保存
        [HttpPost]
        public JsonResult shEide(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                Rlmodel.R_JLSHYJ = model.R_JLSHYJ;//审核意见
                Rlmodel.R_ZJL = user.Id;//总经理
                Rlmodel.R_SHDATE = DateTime.Now;//审核时间
                Rlmodel.L_type = 6;//完成
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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


        /*返退货 出货单  注：流程完成之后就能产生出货单， 客服登录之后根据自己处理过的返退货，对应的开单出货的列表。品保经理可以看到所有要出货的单子 确认之后仓库可以看到所有要出货的单子，出货后修改一下记录的状态。*/

        #region //开出货单 出货单列表
        public ActionResult chlist(int? pageIndex)
        {
            Session[SessionHelper.SSerch] = null;
            PagerInfo<NAReturnListView> listmodel = GetchListPager(pageIndex,null,null);
            return View(listmodel);
        } 
        #endregion
        
        #region //客服开单出货 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NAReturnListView> GetchListPager(int? pageIndex,string Name,string bianhao)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INAReturnListDao.SetPagerPageIndex(CurrentPageIndex);
            _INAReturnListDao.SetPagerPageSize(11);
            PagerInfo<NAReturnListView> listmodel = _INAReturnListDao.Getchinfolist(Name,bianhao,Suser);
            return listmodel;
        }
        #endregion

        #region //返退货 出货单页面
        public ActionResult chview(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("chview", NARmodel);
        } 
        #endregion

        #region //保存返退货 出货单 改变出货单的 状态
        [HttpPost]
        public JsonResult BcEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                Rlmodel.NQ_Type = 2;//待屏保确认(免去品保确认 直接完成)
                Rlmodel.R_isty =0;//同意出货
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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

        #region //保存返退货 出货单 品保签字确认 给变退货单状态
        [HttpPost]
        public JsonResult pbqrEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);

                if (model.R_isty == 0)
                {
                    Rlmodel.NQ_Type = 2;//同意出货 仓库待出货
                    Rlmodel.R_isty = model.R_isty;//是否同意
                    Rlmodel.R_ThYY = "";//退货原因
                    Rlmodel.PbId = user.Id;//品保Id
                    Rlmodel.Pb_DateTime = DateTime.Now;//签字时间
                }
                else
                {
                    Rlmodel.NQ_Type = 1;//不同意出货
                    Rlmodel.R_isty = model.R_isty;//是否同意出货
                    Rlmodel.R_ThYY = model.R_ThYY;//退货原因
                }
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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

        #region //出货单 打印
        public ActionResult CHPrintView(string Id)
        {
            NAReturnListView model = _INAReturnListDao.NGetModelById(Id);
            //客户名称
            NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(model.C_Id);//查询客户信息
            string Cus_name = Cusmodel.Name;//客户名称
            ViewData["Cusname"] = Cus_name;
            string Adreess = Cusmodel.Adress;//地址
            ViewData["Adress"] = Adreess;//
            string Tel = Cusmodel.Tel;//电话
            ViewData["Tel"] = Tel;//
            string bh = model.fthbianhao;//编号
            ViewData["bh"] = bh;
            string kddate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["kddate"] = kddate;
            ViewData["R_Id"] = Id;
            ViewData["kfzy"] = model.Kfzy;
            return View();
        }
        #endregion

        //条件查询
        #region //客服开单出货查询
        public JsonResult chSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string bianhao = Request.Form["txtbianhao"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetchListPager(pageIndex,Name,bianhao);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        } 
        #endregion

        #region //出货单 品保确认数据
        private PagerInfo<NAReturnListView> GetchqrListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INAReturnListDao.SetPagerPageIndex(CurrentPageIndex);
            _INAReturnListDao.SetPagerPageSize(11);
            PagerInfo<NAReturnListView> listmodel = _INAReturnListDao.Getchqrinfolist(Name, Suser);
            return listmodel;
        } 
        #endregion

        #region //返退货品保部 开单确认列表
        public ActionResult chqrlist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = GetchqrListPager(pageIndex, null);
            return View(listmodel);
        } 
        #endregion

        //出货单审核页面
        public ViewResult chshView(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("chshView", NARmodel);
        }

        #region //返退货 仓库 出货列表分页数据
        private PagerInfo<NAReturnListView> GetckchListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INAReturnListDao.SetPagerPageIndex(CurrentPageIndex);
            _INAReturnListDao.SetPagerPageSize(11);
            PagerInfo<NAReturnListView> listmodel = _INAReturnListDao.Getckchinfolist(Name, Suser);
            return listmodel;
        } 
        #endregion

        #region //返退货 仓库出库列表
        public ActionResult ckchlist(int? pageIndex)
        {
            PagerInfo<NAReturnListView> listmodel = GetckchListPager(pageIndex, null);
            return View(listmodel);
        } 
        #endregion

        #region //仓库出货 列表数据查询
        public JsonResult ckchSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NAReturnListView> listmodel = GetckchListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        //仓库 出货单确认页面
        public ViewResult ckchView(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("ckchView", NARmodel);
        }

        #region //保存返退货 出货单 仓库出货 改变退货单状态
        [HttpPost]
        public JsonResult ckchEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);

                Rlmodel.NQ_Type = 3;//已出货
                Rlmodel.ckqr = user.Id;//仓库人
                Rlmodel.ckchdatetime = DateTime.Now;//仓库出货时间
                flay = _INAReturnListDao.NUpdate(Rlmodel);
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

        #region //根据流程ID 查找对应的返退产品
        /// <summary>
        /// 返退货产品分析 
        /// </summary>
        /// <param name="r_id">流程ID</param>
        /// <returns></returns>
        [HttpPost]
        public string GetAJxaTHfxcpbyr_id(string R_Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHFCinfobyR_Id(R_Id));
            return json;
        } 
        #endregion

        #region //退货产品 列表页面
        public ViewResult ThFXlist(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("ThFXlist", NARmodel);
        } 
        #endregion

        #region //返退货产品 分析记录查看页面
        public ViewResult ThFXlistck(string Id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(Id))
                NARmodel = _INAReturnListDao.NGetModelById(Id);
            return View("ThFXlistck", NARmodel);
        } 
        #endregion

        #region //分析编辑页面
        public ViewResult FxEditPage(string Id)
        {
            NQ_THinfoFXView model = new NQ_THinfoFXView();
            model = _INQ_THinfoFXDao.NGetModelById(Id);
            return View("FxEdit",model);
        } 
        #endregion

        [HttpPost]
        public JsonResult FxEdit(NQ_THinfoFXView model, FormContext form, HttpPostedFileBase image, HttpPostedFileBase image2)
        {
            bool flay = false;
            if (image != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                image.SaveAs(filePath);
                model.TH_imgurl1 = "/Content/Img/" + fileName;
            }
            if (image2 != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image2.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                image2.SaveAs(filePath);
                model.TH_imgurl2 = "/Content/Img/" + fileName;
            }
            if (model.TH_yqjjg.ToString() == "")
                model.TH_yqjjg = 0;
            if (model.TH_BCF.ToString() == "")
                model.TH_BCF = 0;
            if (model.TH_RGF.ToString() == "")
                model.TH_RGF = 0;
            model.TH_SCdate = model.TH_SCdate;//生产日期
            model.TH_Ph = model.TH_Ph;//生产批号
            model.TH_YQJname = Request.Form["yjid"];//元器件ID
            //model.TH_BLXXX = Request.Form["SelectedxxxData"];//不良现象
            //model.TH_BLXX = Request.Form["SelectedxxData"];//不良原因1
            //model.TH_BLYY = Request.Form["SelectedyyData"];//不良原因2
            model.Iscl = 1;
            model.wx_time = DateTime.Now;
            model.Isdz = 0;//未定责
            NAReturnListView R_Model = new NAReturnListView();
            R_Model = _INAReturnListDao.NGetModelById(model.R_Id);
            model.TH_zbshj =Convert.ToInt32(DateDiff(DatePart.MM,Convert.ToDateTime(model.TH_SCdate),Convert.ToDateTime(R_Model.R_FTdate)));
            flay = _INQ_THinfoFXDao.NUpdate(model);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        //客退品分析（维修产品增加）
        [HttpPost]
        public JsonResult AddFxEdit(string cpID, string R_Id, string SL)
        {
            try
            {
                bool flay = false;
                NAReturnListView NRemodel = _INAReturnListDao.NGetModelById(R_Id);
                NRemodel.R_Number = NRemodel.R_Number + Convert.ToInt32(SL);
                _INAReturnListDao.NUpdate(NRemodel);
                NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(cpID);
                for (int i = 0; i < Convert.ToInt32(SL); i++)
                {
                    NQ_THinfoFXView thmodel = new NQ_THinfoFXView();
                    thmodel.P_Id = cpID;//
                    thmodel.R_Id = R_Id;//
                    thmodel.TH_RGF = 0;//包材费
                    thmodel.TH_yqjjg = 0;//元器件价格
                    thmodel.TH_BCF = 0;//人工费
                    thmodel.TH_YF = 0;//
                    thmodel.TH_XJ = 0;//
                    thmodel.Isdz = 0;//是否定责
                    thmodel.C_time = DateTime.Now;//添加时间
                    if (cpmodel != null)
                    {
                        thmodel.Cpname = cpmodel.Pname;
                        thmodel.Cpmode = cpmodel.P_xh;
                        thmodel.Cpwlno = cpmodel.P_bianhao;
                    }
                    flay =_INQ_THinfoFXDao.Ninsert(thmodel); 
                }
              
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            catch {
                return Json(new { result = "error" });
            }
        }

        #region //客退品分析（删除）

        /// <summary>
        ///  //客退品分析 （维修产品 删除）
        /// </summary>
        /// <param name="FxmxId">客退品分析明细 记录Id</param>
        /// <param name="R_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DelFxEdit(string FxmxId, string R_Id)
        {
            try
            {
                bool flay = false;
                NAReturnListView NRemodel = _INAReturnListDao.NGetModelById(R_Id);
                NRemodel.R_Number = NRemodel.R_Number - 1;
                _INAReturnListDao.NUpdate(NRemodel);
                NQ_THinfoFXView thmodel = _INQ_THinfoFXDao.NGetModelById(FxmxId);
                flay = _INQ_THinfoFXDao.NDelete(thmodel);
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

        //客退品分析批量删除
        [HttpPost]
        public JsonResult PLDELFxEdit(string FxmxId, string R_Id)
        {
            try
            {
                string Id = FxmxId;
                Id = Id.Replace(",", "','");
                IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.NGetList_id(Id);
                if (modellist != null)
                {
                    int Total = modellist.Count();
                    foreach (var a in modellist)
                    {
                        NQ_THinfoFXView thmodel = a;
                        _INQ_THinfoFXDao.NDelete(thmodel);
                    }
                    NAReturnListView NRemodel = _INAReturnListDao.NGetModelById(R_Id);
                    NRemodel.R_Number = NRemodel.R_Number - Total;
                    _INAReturnListDao.NUpdate(NRemodel);
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }
            catch
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region 枚举值
        /// <summary>
        /// 日期枚举值
        /// </summary>
        public enum DatePart
        {
            /// <summary>
            /// 年
            /// </summary>
            YY,
            /// <summary>
            /// 月
            /// </summary>
            MM,
            /// <summary>
            /// 日
            /// </summary>
            DD,
            /// <summary>
            /// 时
            /// </summary>
            HH,
            /// <summary>
            /// 分
            /// </summary>
            MI,
            /// <summary>
            /// 秒
            /// </summary>
            SS,
            /// <summary>
            /// 毫秒
            /// </summary>
            MS
        }

        #endregion

        #region DateDiff()，返回两个日期的时间差
        /// <summary>
        /// 返回两个日期的时间差
        /// </summary>
        /// <param name="datepart">DatePart枚举值</param>
        /// <param name="starttime">起始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public static long DateDiff(DatePart datepart, DateTime starttime, DateTime endtime)
        {
            long rtn = 0;
            TimeSpan start = new TimeSpan(starttime.Ticks);
            TimeSpan end = new TimeSpan(endtime.Ticks);
            TimeSpan delta = end.Subtract(start);
            long year = endtime.Year - starttime.Year;
            long month = year * 12 + (endtime.Month - starttime.Month);
            long day = endtime.Day-starttime.Day;
           // long day = (long)delta.TotalDays;
            long hour = (long)delta.TotalHours;
            long minute = (long)delta.TotalMinutes;
            long second = (long)delta.TotalSeconds;
            long milliseconds = (long)delta.TotalMilliseconds;
            switch (datepart)
            {
                case DatePart.YY:
                    rtn = year;
                    break;
                case DatePart.MM:
                    if (day > 0)
                      month=month + 1;
                    rtn = month;
                    break;
                case DatePart.DD:
                    rtn = day;
                    break;
                case DatePart.HH:
                    rtn = hour;
                    break;
                case DatePart.MI:
                    rtn = minute;
                    break;
                case DatePart.SS:
                    rtn = second;
                    break;
                case DatePart.MS:
                    rtn = milliseconds;
                    break;
            }
            return rtn;
        }

        #endregion

        #region //js计算俩个日期之前的月份
        public string JcMonecount(string scdatetime,string r_Id)
        {
            NAReturnListView R_Model = new NAReturnListView();
            R_Model = _INAReturnListDao.NGetModelById(r_Id);
            string TH_zbshj = Convert.ToString(DateDiff(DatePart.MM, Convert.ToDateTime(scdatetime), Convert.ToDateTime(R_Model.R_FTdate)));
            if (Convert.ToInt32(TH_zbshj) < 18)
            {
                return "18个月内";
            }
            if (Convert.ToInt32(TH_zbshj) >= 18 && Convert.ToInt32(TH_zbshj) <= 36)
            {
                return "18个月至3年";
            }
            if (Convert.ToInt32(TH_zbshj) > 36)
            {
                return "3年外";
            }
            return "无法计算";
        }
        #endregion

        #region //返退货跟踪单打印

        //打印页面
        public ActionResult NAReturnPrintView(string Id)
        {
            NAReturnListView model = _INAReturnListDao.NGetModelById(Id);
            return View(model);
        }

        #endregion

        #region //维修分析列表

        public ActionResult WXFXlistView(int? pageIndex)
        {
            AlblxxDropdown(null);
            AlblyyDropdown(null);
          //  cpDropdown(null);//返退产品
            PagerInfo<NQ_THinfoFXView> listmodel = GetWxfxListPager(pageIndex, null, null, null, null, null,null, null, null, null,null,null,null,null,null);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            return View(listmodel);
        }

        //查询
        public JsonResult FXSearchList()
        {
            string Khname = Request.Form["txtSearch_khname"];//客户名称
            string cpname=Request.Form["txtSearch_cpname"];//产品名称
            string sc_ph = Request.Form["txtSearch_scph"];//生产批号
            string bl_xx = Request.Form["blxxSelectComm"];
            string bl_Yy = Request.Form["txtSearch_blYy"];//不良原因
            string yqj_name = Request.Form["txtSearch_yqjname"];//元器件名称
            string yqj_xh = Request.Form["txtSearch_yqjxh"];//元器件型号
            string zx=Request.Form["txtSearch_zx"];//性质  18个月内 3年内  3年外
            string starttime = Request.Form["txtSearch_starttime"];//退货时间开始
            string enedtime = Request.Form["txtSearch_enedtime"];//退货时间结束
            string wxstarttime = Request.Form["txtwxstarttime"];//维修时间
            string wxendtime = Request.Form["wxendtime"];//
            string zxstart = "";
            string zxend = "";
            string r_pid=Request.Form["txtSearch_rpid"];
            if (zx == "0") {
                zxstart = "0";
                zxend = "18";
            }
            if (zx == "1") {
                zxstart = "18";
                zxend = "36";
            }
            if (zx == "2") {
                zxstart = "36";
                zxend = "99999";
            }
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_THinfoFXView> listmodel = GetWxfxListPager(pageIndex, Khname, cpname, sc_ph, bl_xx, bl_Yy, yqj_name,yqj_xh, zxstart, zxend, starttime, enedtime,wxstarttime,wxendtime, r_pid);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        //获取维修分析分页数据
        private PagerInfo<NQ_THinfoFXView> GetWxfxListPager(int? pageIndex, string khname, string CPName, string SC_PH,string BL_XX,string BL_Yy,string yqj_name,string yqj_xh,
            string xzstart, string xzend, string starttime, string enedtime, string wxstarttime, string wxendtime, string R_Pid)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_THinfoFXDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_THinfoFXDao.SetPagerPageSize(10);
            PagerInfo<NQ_THinfoFXView> listmodel = _INQ_THinfoFXDao.GetCinfoList(khname, CPName, SC_PH, BL_XX, BL_Yy, yqj_name,yqj_xh, xzstart, xzend, starttime, enedtime,wxstarttime,wxendtime, R_Pid, Suser);
            return listmodel;
        }
       
        //根据反退货Id 查找客户名称
        public string Getkfnamebyr_Id(string Id)
        {
            try
            {
                NAReturnListView model = _INAReturnListDao.NGetModelById(Id);
                if (model != null)
                {
                    NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(model.C_Id);//查询客户信息
                    if (Cusmodel != null)
                    {
                        string Cus_name = Cusmodel.Name;//客户名称
                        return Cus_name;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        //不良现象
        public void AlblxxDropdown(string SelectedPID)
        {
            //List<NACustomerinfoView> CustlistView = _INACustomerinfoDao.GetCustinfoisjxs("1") as List<NACustomerinfoView>;
            List<NQ_BlxxinfoView> BlinfoView = _INQ_BlxxinfoDao.NGetList() as List<NQ_BlxxinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonlist.Add(Reasonmodel);
            if (BlinfoView != null)
            {
                foreach (var item in BlinfoView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name");

        }

        //不良现象多选下拉数据
        /// <summary>
        /// 不良现象多选下拉数据(列表页面时)
        /// </summary>
        /// <param name="personId">需要选中的Value值</param>
        public JsonResult BlxxAlbumDropdown()
        {
            //string personId = Request.Form["personId"];
            string json = _INQ_BlxxinfoDao.BLXXAlbumDropdown(null);
            return Json(new { result = json }, "text/html");
        }

        #region //不良原因父级数据
        public JsonResult Allblyyjsondata()
        {
            string json = JsonConvert.SerializeObject(_INQ_BlinfoDao.GetlistisPar());
            return Json(new { result = json }, "text/html");
        }

        /// <summary>
        /// 通过父级Id查询下级不良原因数据
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public JsonResult AllblyyChiljsondata(string Pid)
        {
            string json = JsonConvert.SerializeObject(_INQ_BlinfoDao.Getlistreason_byPid(Pid));
            return Json(new { result = json }, "text/html");
        }
        #endregion

        //不良原因
        public void AlblyyDropdown(string SelectedPID)
        {
            List<NQ_BlinfoView> BlinfoView = _INQ_BlinfoDao.GetlistisPar() as List<NQ_BlinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonlist.Add(Reasonmodel);
            if (BlinfoView != null)
            {
                foreach (var item in BlinfoView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["BLyyList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["BLyyList"] = new SelectList(Reasonlist, "Id", "Name");
        }

        #region //返退货维修处理记录数据 导出Excl表格
        public FileResult ERWEIDATA(string khname, string CPName, string SC_PH, string BL_XX, string BL_Yy, string yqj_name, string yqj_xh,
            string xzstr, string starttime, string enedtime, string wxstarttime, string wxendtime, string r_pId)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string zxstart = "";
            string zxend = "";
            if (xzstr == "0")
            {
                zxstart = "0";
                zxend = "18";
            }
            if (xzstr == "1")
            {
                zxstart = "18";
                zxend = "36";
            }
            if (xzstr == "2")
            {
                zxstart = "36";
                zxend = "99999";
            }
            IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.DCWXFXDATA(khname, CPName, SC_PH, BL_XX, BL_Yy, yqj_name, yqj_xh, zxstart, zxend, starttime, enedtime,wxstarttime,wxendtime, r_pId, Suser);
           
                //创建Excel文件的对象
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                //添加一个sheet
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
                //给sheet1添加第一行的头部标题
                NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                row1.CreateCell(0).SetCellValue("序号");
                row1.CreateCell(1).SetCellValue("维修时间");
                row1.CreateCell(2).SetCellValue("客户名称");
                row1.CreateCell(3).SetCellValue("物料代码");
                row1.CreateCell(4).SetCellValue("产品名称");
                row1.CreateCell(5).SetCellValue("产品型号规格");
                row1.CreateCell(6).SetCellValue("生产批号");
                row1.CreateCell(7).SetCellValue("不良现象");
                row1.CreateCell(8).SetCellValue("不良原因");
                row1.CreateCell(9).SetCellValue("元器件");
                row1.CreateCell(10).SetCellValue("元件品牌");
                row1.CreateCell(11).SetCellValue("性质");
                row1.CreateCell(12).SetCellValue("责任归属");
                row1.CreateCell(13).SetCellValue("备注");
                row1.CreateCell(14).SetCellValue("处理方式");
                row1.CreateCell(15).SetCellValue("退货时间");
                if (modellist != null)
                {
                    NAReturnListView NARMODEL = new NAReturnListView();
                    NQ_productinfoView cpmodel = new NQ_productinfoView();
                int n = 0;
                    for (int i = 0; i < modellist.Count; i++)
                    {
                        NARMODEL = _INAReturnListDao.NGetModelById(modellist[i].R_Id);//反退货订单
                        if (NARMODEL.Status == 0)
                        {
                            n = n + 1;
                            cpmodel = _INQ_productinfoDao.NGetModelById(modellist[i].P_Id);
                            NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                            rowtemp.CreateCell(0).SetCellValue(n);//序号
                            rowtemp.CreateCell(1).SetCellValue(modellist[i].wx_time.ToString());//维修时间
                            string cusname = GetCusnameNew(NARMODEL.C_Id);
                            rowtemp.CreateCell(2).SetCellValue(cusname);//序号
                            rowtemp.CreateCell(3).SetCellValue(cpmodel.P_bianhao);//物料代码
                            rowtemp.CreateCell(4).SetCellValue(cpmodel.Pname);//产品名称
                            rowtemp.CreateCell(5).SetCellValue(cpmodel.P_xh);//产品名称
                            rowtemp.CreateCell(6).SetCellValue(modellist[i].TH_Ph);//生产批号
                            string blxx = getblxx(modellist[i].TH_BLXXX);
                            rowtemp.CreateCell(7).SetCellValue(blxx);//不良现象
                            string blyy = getblyy(modellist[i].TH_BLXX, modellist[i].TH_BLYY);
                            rowtemp.CreateCell(8).SetCellValue(blyy);//不良原因
                            string yqjname = getyqj(modellist[i].TH_YQJname);
                            rowtemp.CreateCell(9).SetCellValue(yqj_name);//元器件
                            string yqjppname = getyqjpp(modellist[i].TH_YQJname);
                            rowtemp.CreateCell(10).SetCellValue(yqjppname);//元器件品牌
                            string zbsj = getzbsj(modellist[i].TH_zbshj.ToString());
                            rowtemp.CreateCell(11).SetCellValue(zbsj);
                            rowtemp.CreateCell(12).SetCellValue(modellist[i].zrbm);//责任部门
                            rowtemp.CreateCell(13).SetCellValue(modellist[i].TH_bz2);
                            rowtemp.CreateCell(14).SetCellValue(modellist[i].TH_bz);
                            rowtemp.CreateCell(15).SetCellValue(NARMODEL.R_FTdate.ToString());
                        }
                }
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel",  "维修记录表.xls");
        }

        //返回客户名称
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">返退货单Id</param>
        /// <returns></returns>
        public string getcusname(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    NAReturnListView rmodel = _INAReturnListDao.NGetModelById(Id);
                    if (rmodel != null)
                    {
                        NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(rmodel.C_Id);
                        if (model != null)
                        {
                            return model.Name;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        //返回客户名称 根据客户Id
        public string GetCusnameNew(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(Id);
                    if (model != null)
                    {
                        return model.Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        //返回产品型号
        //public NQ_productinfoView getcpname(string Id)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(Id))
        //        {
        //            NQ_productinfoView model = _INQ_productinfoDao.NGetModelById(Id);
        //            if (model != null)
        //            {
        //                return model.Pname;
        //            }
        //            else
        //            {
        //                return "";
        //            }
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}


        //不良现象
        public string getblxx(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    NQ_BlxxinfoView model = _INQ_BlxxinfoDao.NGetModelById(Id);
                    if (model != null)
                    {
                        return model.Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        //不良原因
        public string getblyy(string blyy1, string blyy2)
        {
            try
            {
                if (!string.IsNullOrEmpty(blyy2))
                {
                    NQ_BlinfoView model = _INQ_BlinfoDao.NGetModelById(blyy2);
                    if (model != null)
                    {
                        return model.Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else if (!string.IsNullOrEmpty(blyy1))
                {
                    NQ_BlinfoView model = _INQ_BlinfoDao.NGetModelById(blyy1);
                    if (model != null)
                    {
                        return model.Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }
        //元器件
        public string getyqj(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    NQ_YJinfoView model = _INQ_YJinfoDao.NGetModelById(Id);
                    if (model != null)
                    {
                        return model.Y_Name + "/" + model.Y_Ggxh;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        //元器件品牌
        public string getyqjpp(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    NQ_YJinfoView yjqmodel = _INQ_YJinfoDao.NGetModelById(Id);
                    if (yjqmodel != null)
                    {
                        NQ_GysInfoView model = _INQ_GysInfoDao.Getmodelbydm(yjqmodel.G_Dm);
                        return model.G_Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        //返回再保时间
        public string getzbsj(string zbsj)
        {
            if (!string.IsNullOrEmpty(zbsj))
            {
                int n = Convert.ToInt32(zbsj);
                if (n <= 18)
                {
                    return "18月内";
                }
                else if (n > 18 && n <= 36)
                {
                    return "18月-三年";
                }
                else
                {

                    return "三年外";
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region //维修分析统计
        //维修分析测试
        public void wxfxtest()
        {
            _INQ_THinfoFXDao.GetTHwxfxgroupcpId(null, null);
        }

        //返退产品的种类
        public string wxfxcpzl()
        {
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHwxfxgroupcpId(null, null));
            return Jsonstr;
        }

        //返退查出的数量
        public string wxfxcpsumbyId(string startime,string endtime,string cpId)
        {
            int sum;
            sum = _INQ_THinfoFXDao.GetCPsumbycpId(startime,endtime,cpId);
            return sum.ToString();
        }

        //返退产品类型表中更新
        public string InsertOrupdateTJFTCPtypeinfo()
        {
            //查处全部类型
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHwxfxgroupcpId(null, null));
            JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
            if (ja != null)
            {
                for (int i = 0, j = ja.Count(); i < j; i++)
                {
                    if (!_INQ_TjFtwxCPTypeSuminfoDao.JccpIscz(ja[i][0].ToString()))
                    {
                        NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                        model.Cp_Id = ja[i][0].ToString();//产品Id
                        model.CP_name = ja[i][1].ToString();//产品名称
                        model.CP_bianhao = ja[i][2].ToString();//产品编号
                        _INQ_TjFtwxCPTypeSuminfoDao.Ninsert(model);
                    }
                }
            }
            return "1";
        }

        //反退货产品数量更新
        public string UpdateFTsumbytime(string starttime,string endtime)
        {
            //产出全部返退产品种类
            IList<NQ_TjFtwxCPTypeSuminfoView> Cptype = _INQ_TjFtwxCPTypeSuminfoDao.NGetList();
            if (Cptype != null)
            {
                for (int i = 0,j=Cptype.Count(); i < j; i++)
                {
                    NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                    model = Cptype[i];
                    int sum =Convert.ToInt32(wxfxcpsumbyId(starttime, endtime, model.Cp_Id));
                    model.FT_SUM = sum;
                    model.Updatetime = DateTime.Now;
                    _INQ_TjFtwxCPTypeSuminfoDao.NUpdate(model);
                }
            }
            return "1";
        }

        #region //维修分析统计页面
        //分析统计产品种类
        public ActionResult TJ_WxfxcpView()
        {
            return View();
        }

        #region //返退产品数据的统计页面
        public ActionResult StatisticsCpsumView()
        {
            return View();
        }


        #region //产品分组统计数据
        /// <summary>
        /// 产品分组统计数据
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="enedtime"></param>
        /// <returns></returns>
        public string GetStatisticsCpsum(string starttime, string enedtime)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetStatisticsgroupcpid(starttime, enedtime));

            return json;
        }
        #endregion

        #region //返退产品的数据导出


    public FileResult ExcelExportStatisticsCpsum(string starttime, string enedtime)
        {
            IList<Object> list = _INQ_THinfoFXDao.GetStatisticsgroupcpid(starttime, enedtime);
            
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("产品名称");
            row1.CreateCell(2).SetCellValue("物料代码");
            row1.CreateCell(3).SetCellValue("退货数量");
            row1.CreateCell(4).SetCellValue("售后费用");
            if (list != null)
            {
               
                int n = 0;
                foreach (var item in list)
                {
                    object[] dd = (object[])item;
                    string[] strs = new string[dd.Length];
                    for (int i = 0; i < dd.Length; i++)
                    {
                        strs[i] = dd[i].ToString();
                    }
                    n = n + 1;
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                    rowtemp.CreateCell(0).SetCellValue(n);//序号
                    rowtemp.CreateCell(1).SetCellValue(strs[1]);//产品名称
                    rowtemp.CreateCell(2).SetCellValue(strs[2]);//物料代码
                    rowtemp.CreateCell(3).SetCellValue(strs[3]);//退货数量
                    rowtemp.CreateCell(4).SetCellValue(strs[4]);//售后费用
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "返退产品数量统计及售后费用.xls");
        }
        #endregion
        #endregion

        //分析统计维修的元器件种类
        public ActionResult TJ_WxYQJView()
        {
            return View();
        } 

        //分析统计维修产品的不良原因
        public ActionResult TJ_WxBlYYView()
        {
            return View();
        }
        #endregion

        //数量统计数据json
        public string GetFtcpinfo(string type)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_TjFtwxCPTypeSuminfoDao.GetALLInfoorderby(type));
            return json;
        }

        //返退产品实体
        public class Ftcpmodel
        {
            public virtual string cpId { get; set; }
            public virtual string cpname { get; set; }
            public virtual string cpbianhao { get; set; }
        }

        //各个月返退账单页面
        #region //各个月返退账单页面

        #region //获取各月返退产品
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult FTbillView()
        {
            return View();
        }

        //获取该月返退产品
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        public string InsertorupdateTjFTCPtypeinfobytime(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);//获取该月的最后一天
            //查出当前月内的返退的产品
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHwxfxgroupcpId(starttime, endtime));
            JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
            if (ja.Count() > 0)
            {
                for (int i = 0, j = ja.Count(); i < j; i++)
                {
                    if (!_INQ_TjFtwxCPTypeSuminfoDao.JccpIsczbycpIDandYM(ja[i][0].ToString(), Year, Mon))
                    {
                        NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                        model.Cp_Id = ja[i][0].ToString();//产品Id
                        model.CP_name = ja[i][1].ToString();//产品名称
                        model.CP_bianhao = ja[i][2].ToString();//产品编号
                        model.FT_Year = Year;//年
                        model.FT_Mone = Mon;//月
                        model.Tj_Type = 0;//按照产品统计
                        _INQ_TjFtwxCPTypeSuminfoDao.Ninsert(model);
                    }
                }
            }
            else
            {
                return "0";//当月没有退货产品
            }
            return "1";
        }

        //获取各个月产品的返退数量
        public string UpdateFTcpsumbytypeandYm(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);
            //该月返退产品种类
            IList<NQ_TjFtwxCPTypeSuminfoView> Cptype = _INQ_TjFtwxCPTypeSuminfoDao.GetTjftinfobytjtypeandYM("0", Year, Mon);
            if (Cptype != null)
            {
                for (int i = 0, j = Cptype.Count(); i < j; i++)
                {
                    NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                    model = Cptype[i];
                    int sum = Convert.ToInt32(wxfxcpsumbyId(starttime, endtime, model.Cp_Id));
                    model.FT_SUM = sum;
                    model.Updatetime = DateTime.Now;
                    _INQ_TjFtwxCPTypeSuminfoDao.NUpdate(model);
                }
            }
            else
            {
                return "0";//不存在产品
            }
            return "1";
        }

        //产品返退数理统计显示（默认当月的）
        public string GetFTcpsuminfobyYmandtype(string Year, string Mon, string pxType)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_TjFtwxCPTypeSuminfoDao.GetTjinfobytypeandYm("0", Year, Mon, pxType));
            return json;
        }

        #endregion

        #region //获取各月维修元器件
        //获取各月维修的元器件
        public string InsertorupdateTjYQJtypeinfobytime(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);//获取该月的最后一天
            //查出当前月维修的元器件
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHwxfxgroupTH_YQJname(starttime, endtime));
            JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
            if (ja.Count() > 0)
            {
                for (int i = 0, j = ja.Count(); i < j; i++)
                {
                    if (!_INQ_TjFtwxCPTypeSuminfoDao.JcYQJIsczbyyqjIdandYM(ja[i][0].ToString(), Year, Mon))
                    {
                        NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                        model.Cp_Id = ja[i][0].ToString();//元器件Id
                        model.CP_name = ja[i][1].ToString();//元器件名称
                        model.CP_XH = ja[i][2].ToString();//元器件型号
                        model.CP_bianhao = ja[i][3].ToString();//产品编号
                        model.FT_Year = Year;//年
                        model.FT_Mone = Mon;//月
                        model.Tj_Type = 2;//按照元器件统计
                        _INQ_TjFtwxCPTypeSuminfoDao.Ninsert(model);
                    }
                }
            }
            else
            {
                return "0";//当月没有
            }
            return "1";
        }

        //更新维修元器件的数量
        public string UpdateFTwxYQJsumbyYm(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);
            IList<NQ_TjFtwxCPTypeSuminfoView> Cptype = _INQ_TjFtwxCPTypeSuminfoDao.GetTjftinfobytjtypeandYM("2", Year, Mon);

            if (Cptype != null)
            {
                for (int i = 0, j = Cptype.Count(); i < j; i++)
                {
                    NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                    model = Cptype[i];
                    int sum = Convert.ToInt32(wcfxYQJsum(starttime, endtime, model.Cp_Id));
                    model.FT_SUM = sum;
                    model.Updatetime = DateTime.Now;
                    _INQ_TjFtwxCPTypeSuminfoDao.NUpdate(model);
                }
            }
            else
            {
                return "0";//不存在产品
            }
            return "1";

        }

        //查询元器件维修数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startime"></param>
        /// <param name="endtime"></param>
        /// <param name="YQJId"></param>
        /// <returns></returns>
        public string wcfxYQJsum(string startime, string endtime, string YQJId)
        {
            int sum;
            sum = _INQ_THinfoFXDao.GetYQJsumbyYQJId(startime, endtime, YQJId);
            return sum.ToString();
        }

        //维修的元器件数量统计显示（）
        public string GetFTYQJsuminfobyYmandtype(string Year, string Mon, string pxType)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_TjFtwxCPTypeSuminfoDao.GetTjinfobytypeandYm("2", Year, Mon, pxType));
            return json;
        }
    
        #endregion

        #region //获取各月维修的不良原因
        //获取各月维修的不量原因
        public string InsertorupdateTjblyyinfobytime(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);//获取该月的最后一天
            //查处当月维修的不良原因
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHwxfxgroupTH_BLXX(starttime, endtime));
            JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
            if (ja.Count() > 0)
            {
                for (int i = 0, j = ja.Count(); i < j; i++)
                {
                    if (!_INQ_TjFtwxCPTypeSuminfoDao.JcblyyIsczbyblyyIdandYM(ja[i][0].ToString(), Year, Mon))
                    {
                        NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                        model.Cp_Id = ja[i][0].ToString();//不良原因Id
                        model.CP_name = ja[i][1].ToString();//不良原因名称
                        model.FT_Year = Year;//年
                        model.FT_Mone = Mon;//月
                        model.Tj_Type = 1;//不良原因统计
                        _INQ_TjFtwxCPTypeSuminfoDao.Ninsert(model);
                    }
                }
            }
            else
            {
                return "0";//当月没有
            }
            return "1";
        }

        //更新不良原因维修数量
        public string UpdateFTwxblyysumbyYm(string Year, string Mon)
        {
            string starttime = Year + "-" + Mon + "-" + "1";
            string endtime = QRHelper.GetMonLastDAY(Year, Mon);
            IList<NQ_TjFtwxCPTypeSuminfoView> Cptype = _INQ_TjFtwxCPTypeSuminfoDao.GetTjftinfobytjtypeandYM("1",Year,Mon);
            if (Cptype != null)
            {
                for (int i = 0, j = Cptype.Count(); i < j; i++)
                {
                    NQ_TjFtwxCPTypeSuminfoView model = new NQ_TjFtwxCPTypeSuminfoView();
                    model = Cptype[i];
                    int sum = Convert.ToInt32(wcfxblyysum(starttime, endtime, model.Cp_Id));
                    model.FT_SUM = sum;
                    model.Updatetime = DateTime.Now;
                    _INQ_TjFtwxCPTypeSuminfoDao.NUpdate(model);
                }
            }
            else
            {
                return "0";//不存在产品
            }
            return "1";
        }

        //查询不良原因的返退产品的数量
        public string wcfxblyysum(string starttime, string endtime, string blyyId)
        {
            int sum;
            sum = _INQ_THinfoFXDao.GetBLyysumbyblyyId(starttime,endtime,blyyId);
            return sum.ToString();
        }

        //维修返退品的不良原因统计显示
        public string GetFtBlyysuminfobyYmandtype(string Year, string Mon, string pxType)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_TjFtwxCPTypeSuminfoDao.GetTjinfobytypeandYm("1", Year, Mon,pxType));
            return json;
        }
        #endregion
        #endregion

        #region //统计各个责任部门均摊的售后费用
        public ActionResult StatisticsgroupResdepartmentcostView()
        {
            return View();
        }
        //责任部门的均摊费用数据查询
        public string GetStatisticsgroupResdepartmentcost(string starttime, string endtime)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_SharealikeDao.GetStatisticsgroupResdepartment(starttime, endtime));
            return json;
        }

        public FileResult ExcelExportStatisticsgroupResdepartmentcost(string starttime, string endtime)
        {
            IList<Object> list = _INA_SharealikeDao.GetStatisticsgroupResdepartment(starttime, endtime);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("责任部门");
            row1.CreateCell(2).SetCellValue("售后费用");
            if (list != null)
            {
                int n = 0;
                foreach (var item in list)
                {
                    object[] dd = (object[])item;
                    string[] strs = new string[dd.Length];
                    for (int i = 0; i < dd.Length; i++)
                    {
                        strs[i] = dd[i].ToString();
                    }
                    n = n + 1;
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                    rowtemp.CreateCell(0).SetCellValue(n);//序号
                    rowtemp.CreateCell(1).SetCellValue(strs[0]);//责任部门
                    rowtemp.CreateCell(2).SetCellValue(strs[1]);//物料代码
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "责任部门售后费用统计.xls");
        }

        public ActionResult StatisticsgroupResdepartmentcostChartView(string json)
        {
            ViewBag.Name = json;
            return View();
        }
        #endregion

        #region //统计退货客户的售后费用
        public ActionResult StatisticsgroupThcusIdcostView()
        {
            return View();
        }
        //退货客户的售后费用数据查询
        public string GetStatisticsgroupThcusIdcost(string starttime, string endtime)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_SharealikeDao.GetStatisticsgroupThcusId(starttime,endtime));
            return json;
        }

        public FileResult ExcelExportStatisticsgroupThcusIdcost(string starttime, string endtime)
        {
            IList<Object> list = _INA_SharealikeDao.GetStatisticsgroupResdepartment(starttime, endtime);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("客户名称");
            row1.CreateCell(2).SetCellValue("售后费用");
            if (list != null)
            {
                int n = 0;
                foreach (var item in list)
                {
                    object[] dd = (object[])item;
                    string[] strs = new string[dd.Length];
                    for (int i = 0; i < dd.Length; i++)
                    {
                        strs[i] = dd[i].ToString();
                    }
                    n = n + 1;
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                    rowtemp.CreateCell(0).SetCellValue(n);//序号
                    rowtemp.CreateCell(1).SetCellValue(strs[1]);//客户名称
                    rowtemp.CreateCell(2).SetCellValue(strs[2]);//物料代码
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "责任部门售后费用统计.xls");
        }
        #endregion



        #endregion

        #endregion

        #region //返退单 新的编辑页面(New)

        #region //客服处理页面
        //编辑页面
        public ActionResult NewkefuchuliView(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            cpDropdown(NARmodel.R_pId);
            return View("NewkefuchuliView", NARmodel);
        }

        //客服编辑页面提交
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">返退货Id</param>
        /// <param name="cptyle">返退产品的类型</param>
        /// <param name="ftdatetime">返退时间</param>
        /// <param name="ftyy1">返退原因1</param>
        /// <param name="ftyy2">返退原因2</param>
        /// <param name="qtyy">其他原因</param>
        /// <param name="cljg">处理结果</param>
        /// <param name="fxjz">翻新减帐</param>
        /// <param name="qtyqcl">其他要求处理结果</param>
        /// <param name="yf">运费</param>
        /// <param name="sp">索赔</param>
        /// <param name="bcyq">补充要求</param>
        /// <returns></returns>
        public string NewkfEide(string Id, string cptyle, string ftdatetime, string ftyy1, string ftyy2, string qtyy, string cljg, string fxjz, string qtyqcl, string yf, string sp, string bcyq)
        {
            try {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //查询
                NAReturnListView Rmodel = _INAReturnListDao.NGetModelById(Id);
                if (Rmodel != null)
                {
                    //检测当前状态是否是 0 未处理 1待确定
                    if (Rmodel.L_type == 0 || Rmodel.L_type == 1)
                    {
                        Rmodel.R_pId = cptyle;//退货产品的类型 电控箱 温控器 其他
                        Rmodel.R_FTdate = Convert.ToDateTime(ftdatetime);//返退时间
                        Rmodel.R_FTyyp = ftyy1;//返退原因1
                        if (ftyy2 != null && ftyy2 != "")//返退原因2不为空保存
                        {
                            Rmodel.R_FTyy = ftyy2;//返退原因2
                        }
                        Rmodel.R_FTyysm = qtyy;//其他原因
                        Rmodel.R_CLjg = cljg;//处理结果
                        if (cljg == "1")//翻新减帐
                        {
                            if (fxjz != null && fxjz != "")
                            {
                                Rmodel.R_CLFY = Convert.ToDecimal(fxjz);
                            }
                            else
                            {
                                Rmodel.R_CLFY = 0;
                            }
                        }
                        if (cljg == "2")
                        {
                            Rmodel.R_CLQTSM = qtyqcl;//其他要求处理结果
                        }
                        if (yf != "" && yf != null)
                        {
                            Rmodel.R_Yf = Convert.ToDecimal(yf);//运费
                        }
                        else
                        {
                            Rmodel.R_Yf = 0;
                        }
                        if (sp != "" && sp != null)
                        {
                            Rmodel.R_sp = sp;//索赔
                        }
                        else
                        {
                            Rmodel.R_sp = "0";
                        }
                        Rmodel.Remark1 = bcyq;//补充要求
                        //查询客户名称
                        NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(Rmodel.C_Id);
                        //返退编号+客户名称
                        string str = Rmodel.fthbianhao;
                        if (Cusmodel != null)
                        {
                            str = Rmodel.fthbianhao + "(" + Cusmodel.Name + ")";
                        }
                        //操作管理员或者客服主管
                        if (user.RoleType == "1" || user.RoleType == "0")
                        {
                            if (Rmodel.Kfzy != null)//客服主管不为空
                            {
                                Rmodel.Kfzg = Rmodel.Kfzy;
                                Rmodel.ClDate = DateTime.Now;//客服专员处理日期
                            }
                            else
                            {
                                Rmodel.Kfzy = Convert.ToString(user.Id);//客服专员
                                Rmodel.ClDate = DateTime.Now;//客服专员处理日期
                            }
                            Rmodel.ClDate = Rmodel.ClDate;
                            Rmodel.L_type = 2;//待维修
                            Rmodel.Kfzg = Convert.ToString(user.Id);//客服主管
                            Rmodel.Kfzgqrdate = DateTime.Now;//客服主管确认日期
                            if (Rmodel.R_pId == "c9ab3dfcc90e4feb9bfb93b8efb73cec")//电控箱
                            {
                                MassManager.FMB_FTtypeupdateNotice("2",str, Rmodel.L_type.ToString());//给维修的发送
                            }
                            else if (Rmodel.R_pId == "7bb3c225a0564b49816c2a36144262af")//温控器
                            {
                                MassManager.FMB_FTtypeupdateNotice("3", str, Rmodel.L_type.ToString());//给维修的发送
                            }
                            else if (Rmodel.R_pId == "c962d65ae3df45b3ac4f898d3e2f4456")//其他
                            {
                                MassManager.FMB_FTtypeupdateNotice("4", str, Rmodel.L_type.ToString());//给维修的发送
                            }
                            else
                            {
                                MassManager.FMB_FTtypeupdateNotice("2,3,4", str, Rmodel.L_type.ToString());//给维修的发送
                            }
                        }
                        else
                        {
                            Rmodel.Kfzy = Convert.ToString(user.Id);//客服专员
                            Rmodel.ClDate = DateTime.Now;//客服专员处理日期
                            Rmodel.L_type = 1;//待确定
                            MassManager.FMB_FTtypeupdateNotice("1", str, Rmodel.L_type.ToString());
                        }
                        flay = _INAReturnListDao.NUpdate(Rmodel);
                        if (flay)
                            return "0";//提交成功
                        else
                            return "1";//提交失败
                    }
                    else
                    {
                        return "4"; //没有权限编辑
                    }
                }
                else
                {
                    return "2";//记录为空
                }

            }
            catch
            {
                return "3";//提交失败
            }
        }
        #endregion

        #region //车间维修处理页面
        //编辑页面
        public ActionResult NewchejianchuliView(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            cpDropdown(NARmodel.R_pId);
            NARmodel.R_YQJ = _INQ_THinfoFXDao.GetTotalcostbyRIdcosttype(id, "TH_yqjjg");
            NARmodel.R_RGF = _INQ_THinfoFXDao.GetTotalcostbyRIdcosttype(id, "TH_RGF");
            NARmodel.R_BCF= _INQ_THinfoFXDao.GetTotalcostbyRIdcosttype(id, "TH_BCF");
            return View("NewchejianchuliView", NARmodel);
        }

        //车间维修编辑提交（总）
        [HttpPost]
        public string NewwxEdit(NAReturnListView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //修改
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(model.Id);
                if (Rlmodel.L_type == 2 || Rlmodel.L_type == 3||Rlmodel.L_type==7)
                {
                    int fptype = Convert.ToInt32(Request.Form["SelectedftypeData"]);//返退类型 （0 维修 1 返退）
                    string Iswxwctype = Request.Form["isxwtype"];//是否维修完成 0完成 1还有配件待维修
                    Rlmodel.FTtype = fptype;//返退类型
                    if (fptype == 0)//判断返退类型是 维修
                    {
                        Rlmodel.R_isbxqn = Convert.ToInt32(Request.Form["SelectedsfzbData"]);//是否在保修期内
                        Rlmodel.R_Fxjlcon = null;//翻新记录为空
                        Rlmodel.R_Pzpd = null;//品质判定为空
                    }
                    else if (fptype == 1)
                    {//判断返退类型 翻新
                        Rlmodel.R_isbxqn = null;//是否在保为空
                        Rlmodel.R_Fxjlcon = model.R_Fxjlcon;//翻新记录
                        var pzpd = Convert.ToInt32(Request.Form["SelectedpzpdData"]);//品质判定
                        Rlmodel.R_Pzpd = pzpd;
                        if (pzpd == 3)
                        { //其他原因
                            Rlmodel.R_qtqksm = model.R_qtqksm;
                        }
                        else
                        {
                            Rlmodel.R_qtqksm = "";
                        }
                    }
                    Rlmodel.R_YQJ = model.R_YQJ;//元器件 
                    Rlmodel.R_RGF = model.R_RGF;//人工费
                    Rlmodel.R_BCF = model.R_BCF;//包材费
                    Rlmodel.R_XJ = hj(model.R_YQJ, model.R_RGF, model.R_BCF);//合计
                    //查询客户名称
                    NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(Rlmodel.C_Id);
                    //返退编号+客户名称
                    string str = Rlmodel.fthbianhao;
                    if (Cusmodel != null)
                    {
                        str = Rlmodel.fthbianhao + "(" + Cusmodel.Name + ")";
                    }
                    if (Iswxwctype == "0")//全部维修完成
                    {
                        if (_INQ_THinfoFXDao.JcTHFXweichulibyr_Id(Rlmodel.Id))
                        {
                            Rlmodel.L_type = 3;//待定责
                            string rse = Sharealikefreight(Rlmodel.Id,Rlmodel.R_Yf,Rlmodel.R_Number);//均摊订单运费
                            MassManager.FMB_FTtypeupdateNotice("5", str, Rlmodel.L_type.ToString());
                        }
                        else
                        {
                            return "11";
                        }
                    }
                    if (Iswxwctype == "1")
                    {
                        Rlmodel.L_type = 7;//维修未完成 配件待维修
                        MassManager.FMB_FTtypeupdateNotice("4", str, Rlmodel.L_type.ToString());
                    }
                    Rlmodel.R_CJFZR = Convert.ToString(user.Id);//电气车间负责人
                    Rlmodel.R_cjcldate = DateTime.Now;//维修时间
                    _INAReturnListDao.NUpdate(Rlmodel);
                    //查询分析记录数据
                    //IList<NQ_THinfoFXView> DXinfo = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Rlmodel.Id);
                    //if (DXinfo != null)
                    //{
                    //    for (int i = 0, j = DXinfo.Count(); i < j; i++)
                    //    {
                    //        NQ_THinfoFXView fxmodel = new NQ_THinfoFXView();
                    //        fxmodel = DXinfo[i];
                    //        fxmodel.wx_time = DateTime.Now;
                    //        _INQ_THinfoFXDao.NUpdate(fxmodel);
                    //    }
                    //}
                    return "2";
                }
                else
                {
                    return "3";
                }
            }
            catch
            {
                return "1";
            }
        }
        #region //费用明细均摊订单的费用
        /// <summary>
        /// 费用明细均摊订单的费用
        /// </summary>
        /// <param name="Rid">返退货</param>
        /// <param name="freight"></param>
        /// <param name="R_Number"></param>
        /// <returns></returns>
        public string Sharealikefreight(string Rid, decimal? freight, int? R_Number)
        {
            try {
                if (R_Number != 0)//运费不未0
                {
                    decimal? average = freight / R_Number;//平均运费
                    //查询分析记录数据
                    IList<NQ_THinfoFXView> DXinfo = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Rid);
                    if (DXinfo != null)
                    {
                        for (int i = 0, j = DXinfo.Count(); i < j; i++)
                        {
                            NQ_THinfoFXView fxmodel = new NQ_THinfoFXView();
                            fxmodel = DXinfo[i];
                            fxmodel.TH_YF = average;//均摊运费
                            fxmodel.TH_XJ = fxmodel.TH_RGF + fxmodel.TH_YF + fxmodel.TH_yqjjg + fxmodel.TH_BCF;//人工费，包材费,运费，元器件合计
                            _INQ_THinfoFXDao.NUpdate(fxmodel);
                        }
                    }
                    return "1";//
                }
                else { //运费未0
                    return "";
                }

            } catch {
                return "";
            }

        } 
        #endregion


        //批量编辑页面
        /// <summary>
        /// 批量编辑页面
        /// </summary>
        /// <param name="Id">明细Id</param>
        /// <param name="R_Id">返退货Id</param>
        /// <returns></returns>
        public ActionResult NewPLweixiuView(string Id, string R_Id)
        {
            ViewData["Id"] = Id;//维修分析记录Id 多个
            ViewData["R_Id"] = R_Id;//返退货单Id
            return View();
        }

        //批量编辑提交方法
        public string NewcjwxtjEide(FormContext form, HttpPostedFileBase image, HttpPostedFileBase image2)
        {
            string Id = Request.Form["Id"];
            Id = "'" + Id.Replace(",","','") + "'";
            IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.NGetList_id(Id);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (modellist != null)
            {
                string TH_imgurl1="";
                string TH_imgurl2="";
                if (image != null)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                    image.SaveAs(filePath);
                    TH_imgurl1 = "/Content/Img/" + fileName;
                }
                if (image2 != null)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(image2.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                    image2.SaveAs(filePath);
                    TH_imgurl2 = "/Content/Img/" + fileName;
                }
                string TH_SCdate = Request.Form["TH_SCdate"];//生产日期
                string TH_Ph = Request.Form["TH_Ph"];//批号
                string TH_YQJname = Request.Form["yjid"];//元器件Id
                string TH_BLXXX = Request.Form["Sel_blxx"];//不良现象
                string TH_BLXX = Request.Form["Sel_blyy"];//不良原因1
                string TH_BLYY = Request.Form["Sel_blyyc"];//不良原因2
                string TH_bz = Request.Form["TH_bz"];//处理方式
                string TH_bz2 = Request.Form["TH_bz2"];//备注
                string Analyst = Request.Form["Analyst"];//分析人
                NAReturnListView R_Model = new NAReturnListView();
                R_Model = _INAReturnListDao.NGetModelById(Request.Form["R_Id"]);
                int zbshj = Convert.ToInt32(DateDiff(DatePart.MM, Convert.ToDateTime(Request.Form["TH_SCdate"]), Convert.ToDateTime(R_Model.R_FTdate)));
                foreach (var a in modellist)
                {
                    NQ_THinfoFXView model = a;
                    model.Iscl = 1;//已经处理
                    model.wx_time = DateTime.Now;//最后填写处理时间
                    model.TH_imgurl1 = TH_imgurl1;//图片1
                    model.TH_imgurl2 = TH_imgurl2;//图片2
                    model.TH_SCdate = Convert.ToDateTime(TH_SCdate);//生产日期
                    model.TH_Ph = TH_Ph;//生产批号
                    model.TH_YQJname = TH_YQJname;//元器件Id
                    model.TH_BLXXX = TH_BLXXX;//不良现象
                    model.TH_BLXX = TH_BLXX;//不了原因1
                    model.TH_BLYY = TH_BLYY;//不良原图2
                    model.TH_zbshj = zbshj;//再保时间
                    model.TH_bz = TH_bz;//处理方式
                    model.TH_bz2 = TH_bz2;//备注
                    model.Isdz = 0;//未定责
                    model.Analyst = Analyst;//分析人
                    _INQ_THinfoFXDao.NUpdate(model);
                }
                return "2";
            }
            else
            {
                return "1";
            }
        }


        //车间维修——返退品明细编辑 分页页面
        public ActionResult Newcjwxmxlist(int? pageIndex,string Id)
        {

            PagerInfo<NQ_THinfoFXView> listmodel = new PagerInfo<NQ_THinfoFXView>();
            if (Id != null && Id != "")
            {
                Session["RId"] = Id;
                ViewData["RId"] = Id;
            }
            else
            {
                ViewData["RId"] = Session["RId"].ToString();
            }
            if (Session[SessionHelper.NSSerch] != null)
            {
                wxmxSearchPara ssearch = Session[SessionHelper.NSSerch] as wxmxSearchPara;
                ViewData["cpname"] = ssearch.cpname;
                if (pageIndex != null)
                {
                    ViewData["pageIndex"] = pageIndex;
                }
                else
                {
                    ViewData["pageIndex"] = ssearch.pageIndex;
                }
                ViewData["Iscl"] = ssearch.Iscl;
                listmodel = Getwxmxlistpage(pageIndex, ssearch.cpname, ssearch.Iscl);
            }
            else
            {
                Session[SessionHelper.NSSerch] = null;
                Session.Remove(SessionHelper.NSSerch);
                listmodel = Getwxmxlistpage(pageIndex, null, null);
            }
            return View(listmodel);
        }

        //车间维修——返退货明细查询
        public JsonResult wxmxSearchList(int? pageIndex)
        {
            Session[SessionHelper.NSSerch] = null;
            Session.Remove(SessionHelper.NSSerch);
            wxmxSearchPara view = new wxmxSearchPara();
            if (pageIndex == null)
                pageIndex = 1;
            view.pageIndex = Convert.ToString(pageIndex);
            view.cpname = Request.Form["cpname"];
            view.Iscl = Request.Form["Iscl"];
            Session[SessionHelper.NSSerch] = view;
            //int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_THinfoFXView> listmodel = Getwxmxlistpage(pageIndex, view.cpname, view.Iscl);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });

        }

        //维修返退品明细分页数据
        private PagerInfo<NQ_THinfoFXView> Getwxmxlistpage(int? pageIndex,string cpname,string Iscl)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_THinfoFXDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_THinfoFXDao.SetPagerPageSize(20);
            string RId = Session["RId"].ToString();
            PagerInfo<NQ_THinfoFXView> listmodel = _INQ_THinfoFXDao.Getftmxinfopage(RId,cpname,Iscl);
            return listmodel;
        }

        //
        //记录维修明细的查询条件
        public class wxmxSearchPara
        {
            /// <summary>
            /// 页面
            /// </summary>
            public virtual string pageIndex { get; set; }

            /// <summary>
            /// 产品名称
            /// </summary>
            public virtual string cpname { get; set; }

            /// <summary>
            /// 是否处理
            /// </summary>
            public virtual string Iscl { get; set; }
        }
        #endregion

        #region //品保定责页面(New)

        public ActionResult NewdingzeEideView(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if(!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("NewdingzeEideView",NARmodel);
        }

         #region //单个明细定责
		        //
        /// <summary>
        /// 明细定责编辑页面
        /// </summary>
        /// <param name="Id">明细记录Id</param>
        /// <param name="R_Id">返退货单的Id</param>
        /// <param name="clmethod">处理方式</param>
        /// <param name="zrbm">责任部门</param>
        /// <returns></returns>
        public ActionResult NewMxdingzeView(string Id, string R_Id, string clmethod,string zrbm, string beizhu)
        {
            ViewData["Id"] = Id;//维修分析记录Id
            ViewData["R_Id"] = R_Id;//返退货单Id
            ViewData["Clmethod"] = clmethod;
            ViewData["Zrbm"] = zrbm;
            ViewData["beizhu"] = beizhu;
            return View();
        }
                //明细定责编辑提交
        public string NewMxdingzeEide(string Id, string bm, string clmethod,string beizhu)
        {
           
              NQ_THinfoFXView model = _INQ_THinfoFXDao.NGetModelById(Id);
              SessionUser user = Session[SessionHelper.User] as SessionUser;
              if (model != null)
              {
                  model.TH_bz = clmethod;//处理方式修改
                  model.Isdz = 1;//已经定责
                  model.dzdatetime = DateTime.Now;//最终定责时间
                  model.dz_cusId = Convert.ToString(user.Id);//定责人
                  model.zrbm = bm;//责任部门
                  model.TH_bz2 = beizhu;
                if (_INQ_THinfoFXDao.NUpdate(model))
                {
                    bool flay = newlybuildsharealike(model);//责任部门均摊售后费用
                    return "0";
                }
                else { return "1"; }
                     
              }
              else
              {
                  return "2";
              }
                 
        }
        #endregion
        #region //根据责任部门产生售后费用记录
        public bool newlybuildsharealike(NQ_THinfoFXView model)
        {
            try {
                if (model == null)
                    return false;
                else {
                    bool check = _INA_SharealikeDao.Getlistbymxiddellist(model.Id);
                    if (check)
                    {
                        List<string> zrbmlist = NAHelper.GetDivisionstrlist(model.zrbm);
                        foreach (var item in zrbmlist)
                        {
                            NA_SharealikeView shmodel = new NA_SharealikeView();
                            shmodel.Resdepartment = item;//责任部门
                            shmodel.cost = model.TH_XJ / zrbmlist.Count;//均摊的费用
                            shmodel.Mx_id = model.Id;//明细Id
                            shmodel.Wxtime = model.wx_time;//维修时间
                            shmodel.c_time = DateTime.Now;//记录创建时间
                            shmodel.pdId = model.P_Id;
                            //查询产品信息
                            //NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(model.P_Id);
                            //if (cpmodel != null)
                            //{
                                shmodel.pdname = model.Cpname;
                                shmodel.pdmodel = model.Cpmode;
                            //}
                            //查询返退货订单
                            shmodel.RId = model.R_Id;
                            NAReturnListView rmodel = _INAReturnListDao.NGetModelById(model.R_Id);
                            if (rmodel != null)
                            {
                                shmodel.Rbianhao = rmodel.fthbianhao;
                                shmodel.ThcusId = rmodel.C_Id;
                                //查询客户信息
                                NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(rmodel.C_Id);
                                if (cusmodel != null)
                                {
                                    shmodel.Thcusname = cusmodel.Name;
                                }
                            }
                            bool flay = _INA_SharealikeDao.Ninsert(shmodel);

                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            } catch { return false; }
        }
        #endregion

        //批量定责编辑页面
        public ActionResult NewMxPLdingzeView(string Id,string R_Id)
        {
            ViewData["Id"] = Id;//维修分析记录Id 多个
            ViewData["R_Id"] = R_Id;//返退货单Id
            return View();
        }

        //批量定责编辑方法
        public string NewMxPldingzeEide(string Id, string bm)
        {
            Id = "'" + Id.Replace(",", "','") + "'";
            IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.NGetList_id(Id);
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (modellist != null)
            {
                foreach (var a in modellist)
                {
                    NQ_THinfoFXView model = a;
                    model.Isdz = 1;//已经定责
                    model.dzdatetime = DateTime.Now;//最终定责时间
                    model.dz_cusId = Convert.ToString(user.Id);//定责人
                    model.zrbm = bm;//责任部门
                    _INQ_THinfoFXDao.NUpdate(model);
                    bool flay=newlybuildsharealike(model);//责任部门均摊售后费用
                }
                return "0";
            }
            else
            {
                return "1";
            }
        }

        //定责明细分页列表
        public ActionResult Newdzmxlist(int? pageIndex, string Id)
        {
           
            PagerInfo<NQ_THinfoFXView> listmodel = new PagerInfo<NQ_THinfoFXView>();
            if (Id != null && Id != "")
            {
                Session["RId"] = Id;
                ViewData["RId"] = Id;
            }
            else
            {
                ViewData["RId"] = Session["RId"].ToString();
            }
            if (Session[SessionHelper.NSSerch] != null)
            {
                wxmxSearchPara ssearch = Session[SessionHelper.NSSerch] as wxmxSearchPara;
                ViewData["cpname"] = ssearch.cpname;
                if (pageIndex != null)
                {
                    ViewData["pageIndex"] = pageIndex;
                }
                else
                {
                    ViewData["pageIndex"] = ssearch.pageIndex;
                }
                ViewData["Isdz"] = ssearch.Iscl;
                listmodel = Getdzmxlistpage(pageIndex, ssearch.cpname, ssearch.Iscl);
            }
            else
            {
                Session[SessionHelper.NSSerch] = null;
                Session.Remove(SessionHelper.NSSerch);
                listmodel = Getdzmxlistpage(pageIndex, null, null);
            }
            return View(listmodel);
        }

        //屏保定责——明细查询
        public JsonResult dzmxSearchList(int? pageIndex)
        {
            Session[SessionHelper.NSSerch] = null;
            Session.Remove(SessionHelper.NSSerch);
            wxmxSearchPara view = new wxmxSearchPara();
            if (pageIndex == null)
                pageIndex = 1;
            view.pageIndex = Convert.ToString(pageIndex);
            view.cpname = Request.Form["cpname"];
            view.Iscl = Request.Form["Isdz"];
            Session[SessionHelper.NSSerch] = view;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_THinfoFXView> listmodel = Getdzmxlistpage(pageIndex, view.cpname, view.Iscl);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        //定责返退明细分页数据
        private PagerInfo<NQ_THinfoFXView> Getdzmxlistpage(int? pageIndex, string cpname, string Isdz)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_THinfoFXDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_THinfoFXDao.SetPagerPageSize(20);
            string RId = Session["RId"].ToString();
            PagerInfo<NQ_THinfoFXView> listmodel = _INQ_THinfoFXDao.Getftdzmxinfopage(RId, cpname, Isdz);
            return listmodel;
        }  

        //返退货单 定责
        public string NewFTHTongyidingze(string Id)
        {
            //检测是否存在未定责的
            if (_INQ_THinfoFXDao.JcTHFXweidingzebyr_Id(Id))//不存在未定责的
            {
                //部门名称
                string pbb = "";//品保部
               // string jsb = "";//技术部
                string dqjs="";//电气技术
                string dzjs="";//电子技术
                string wljs = "";//网络技术
                string dqcj="";//电气车间
                string dzcj="";//电子车间
                string wlgs="";//物流公司
                string gys="";//供应商
               // string zzb = "";//制造部
                string yxb = "";//营销部
               // string qtbm = "";//其他部门
                string khdw = "";//客户单位
                string ckdw = "";//仓库单位
                string zcwz = "";//正常无责
                IList<NQ_THinfoFXView> THFXmodellist = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Id);
                if (THFXmodellist != null)
                {
                    for (int i = 0, j = THFXmodellist.Count(); i < j; i++)
                    {
                        //s.IndexOf("l") != 0
                        string zrbmstr = THFXmodellist[i].zrbm;
                        if (zrbmstr.IndexOf("品保部") >-1)
                        {
                            pbb = "品保部";
                        }
                        //if (THFXmodellist[i].zrbm == "技术部")
                        //{
                        //    jsb = "技术部";
                        //}
                        if (zrbmstr.IndexOf("网络技术") > -1)
                        {
                            wljs = "网络技术";
                        }
                        if (zrbmstr.IndexOf("电气技术") > -1)
                        {
                            dqjs = "电气技术";
                        }
                        if (zrbmstr.IndexOf("电子技术") > -1)
                        {
                            dzjs = "电子技术";
                        }
                        if (zrbmstr.IndexOf("电气车间") > -1)
                        {
                            dqcj = "电气车间";
                        }
                        if (zrbmstr.IndexOf("电子车间") > -1)
                        {
                            dzcj = "电子车间";
                        }
                        //if (THFXmodellist[i].zrbm == "制造部")
                        //{
                        //    zzb = "制造部";
                        //}
                        if (zrbmstr.IndexOf("物流公司") > -1)
                        {
                            wlgs = "物流公司";
                        }
                        if (zrbmstr.IndexOf("供应商") > -1)
                        {
                            gys = "供应商";
                        }

                        if (zrbmstr.IndexOf("营销部") > -1)
                        {
                            yxb = "营销部";
                        }
                        //if (THFXmodellist[i].zrbm == "其他部门")
                        //{
                        //    qtbm = "其他部门";
                        //}
                        if (zrbmstr.IndexOf("客户单位") > -1)
                        {
                            khdw = "客户单位";
                        }
                        if (zrbmstr.IndexOf("仓库") > -1) {
                            ckdw = "仓库";
                        }
                        if (zrbmstr.IndexOf("正常无责") > -1)
                        {
                            zcwz = "正常无责";
                        }
                    }
                    NAReturnListView Rmodel = _INAReturnListDao.NGetModelById(Id);
                    if (Rmodel != null)
                    {
                        if (Rmodel.L_type != 6)
                        {
                            SessionUser user = Session[SessionHelper.User] as SessionUser;
                            string ZRBMSTR = "";
                            ZRBMSTR = pbb + "," + dqjs + "," + dzjs + "," + wljs + "," + dqcj + "," + dzcj + "," + wlgs + "," + gys + "," + yxb + "," + khdw + ","+ckdw+"," + zcwz;
                            Rmodel.R_bbzrbm = ZRBMSTR;//责任部门
                            Rmodel.R_bbgly = Convert.ToString(user.Id);//品保管理员
                            Rmodel.R_bbcldate = DateTime.Now;//定责时间
                            Rmodel.L_type = 4;//待处理
                            //查询客户名称
                            NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(Rmodel.C_Id);
                            //返退编号+客户名称
                            string str = Rmodel.fthbianhao;
                            if (Cusmodel != null)
                            {
                                str = Rmodel.fthbianhao + "(" + Cusmodel.Name + ")";
                            }
                            if (_INAReturnListDao.NUpdate(Rmodel))
                            {
                                MassManager.FMB_FTtypeupdateNotice("0,1", str, Rmodel.L_type.ToString());
                                return "1";//编辑成功
                            }
                            else
                            {
                                return "2";//编辑失败
                            }
                        }
                        else
                        {
                            return "5";//该单已经完成无需定责
                        }
                        //  MassManager.FMB_FTtypeupdateNotice("0,1", Rmodel.fthbianhao, Rmodel.L_type.ToString());
                    }
                    else
                    {
                        return "3";//不存在该返退货单
                    }
                }
                else
                {
                    return "4";//不存在返退明细
                }
            }
            else
            {
                return "0";//存在没有定责的
            }
        }

        //维修记录图片查看
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">维修记录Id</param>
        /// <returns></returns>
        public ActionResult CheckwxtupianView(string Id)
        {
            NQ_THinfoFXView model = new NQ_THinfoFXView();
            model = _INQ_THinfoFXDao.NGetModelById(Id);
            ViewData["img1"] = model.TH_imgurl1;
            ViewData["img2"] = model.TH_imgurl2;
            ViewData["img3"] = model.TH_imgurl3;
            return View();
        }
        #endregion

        #region //营销中心处理页面
        //营销中心页面
        public ActionResult NewyxzxView(string id)
        {
            NAReturnListView NARmodel = new NAReturnListView();
            if (!string.IsNullOrEmpty(id))
                NARmodel = _INAReturnListDao.NGetModelById(id);
            return View("NewyxzxView", NARmodel);
        }

        //营销中心提交
        public string NewxyzxEide(string Id,string clyj, string qyjl,string jhyf)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                NAReturnListView Rlmodel = _INAReturnListDao.NGetModelById(Id);
                if (Rlmodel != null)
                {

                    Rlmodel.R_xybclyj = clyj;//处理意见
                    Rlmodel.R_qyjl = qyjl;//区域经理
                    Rlmodel.R_yxzj = user.Id;
                    Rlmodel.R_yxcldate = DateTime.Now;
                    Rlmodel.R_Jhyf = Convert.ToDecimal(jhyf);
                    // Rlmodel.L_type = 5;//待审核(取消总经办审核 直接完成)
                    Rlmodel.L_type = 6;//完成
                    flay = _INAReturnListDao.NUpdate(Rlmodel);
                    if (flay) {
                        updateJHyf(Rlmodel.Id, Rlmodel.R_Jhyf, Rlmodel.R_Number);
                        return "0";//提交成功
                    }  
                    else {
                        return "1";//提交失败
                    }
                       
                }
                else
                {
                    return "2"; //不存在该返退单
                }
            }
            catch
            {
                return "3";//提交异常
            }
        }

        #region //存在寄回运费更新返退明细中的寄回运费数据已经更新定责数据
        public void updateJHyf(string Rid,decimal? jhyf, int? R_Number)
        {
            try
            {
                if (R_Number != 0)
                {
                    decimal? average = jhyf / R_Number;//平均运费
                                                       //查询分析记录数据
                    IList<NQ_THinfoFXView> DXinfo = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Rid);
                    if (DXinfo != null)
                    {
                        for (int i = 0, j = DXinfo.Count(); i < j; i++)
                        {
                            NQ_THinfoFXView fxmodel = new NQ_THinfoFXView();
                            fxmodel = DXinfo[i];
                            fxmodel.TH_JHYF = average;//均摊运费
                            fxmodel.TH_XJ = fxmodel.TH_RGF + fxmodel.TH_YF + fxmodel.TH_yqjjg + fxmodel.TH_BCF + fxmodel.TH_JHYF;//人工费，包材费,运费，元器件合计
                            _INQ_THinfoFXDao.NUpdate(fxmodel);
                            bool flay = newlybuildsharealike(fxmodel);//责任部门均摊售后费用
                        }
                    }
                }
            }
            catch
            {
                
            }
        }
        #endregion
        #endregion

        #endregion

        #region //维修明细查看页面
        public ActionResult Lookwxmxlist(int? pageIndex, string Id)
        {
            PagerInfo<NQ_THinfoFXView> listmodel = new PagerInfo<NQ_THinfoFXView>();
            if (Id != null && Id != "")
            {
                Session["RId"] = Id;
                ViewData["RId"] = Id;
            }
            else
            {
                ViewData["RId"] = Session["RId"].ToString();
            }
            if (Session[SessionHelper.NSSerch] != null)
            {
                wxmxSearchPara ssearch = Session[SessionHelper.NSSerch] as wxmxSearchPara;
                ViewData["cpname"] = ssearch.cpname;
                if (pageIndex != null)
                {
                    ViewData["pageIndex"] = pageIndex;
                }
                else
                {
                    ViewData["pageIndex"] = ssearch.pageIndex;
                }
                ViewData["Isdz"] = ssearch.Iscl;
                listmodel = Getdzmxlistpage(pageIndex, ssearch.cpname, ssearch.Iscl);
            }
            else
            {
                Session[SessionHelper.NSSerch] = null;
                Session.Remove(SessionHelper.NSSerch);
                listmodel = Getdzmxlistpage(pageIndex, null, null);
            }
            return View(listmodel);
        }

        //维修明细查看-查询
        public JsonResult LookwxmxSearchList(int? pageIndex)
        {
            Session[SessionHelper.NSSerch] = null;
            Session.Remove(SessionHelper.NSSerch);
            wxmxSearchPara view = new wxmxSearchPara();
            if (pageIndex == null)
                pageIndex = 1;
            view.pageIndex = Convert.ToString(pageIndex);
            view.cpname = Request.Form["cpname"];
            //view.Iscl = Request.Form["Isdz"];
            Session[SessionHelper.NSSerch] = view;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_THinfoFXView> listmodel = Getdzmxlistpage(pageIndex, view.cpname, view.Iscl);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //返回上级状态
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r_Id">返退单子Id</param>
        /// <param name="type">单子当前的状态</param>
        /// <param name="YHtype">用户类型 0 处理意见页面  1 车间维修页面 2 定责页面  3 营销处理页面</param>
        /// <returns></returns>
        public string Fanhuishangtype(string r_Id, string type, string YHtype)
        {
            NAReturnListView model = _INAReturnListDao.NGetModelById(r_Id);
            if (model != null)
            {
                if (YHtype == "0")//客服处理意见页面（只能返回1待确定 2待维修 状态的数据 返回到初始状态0）
                {
                    if (type == "1" || type == "2")//待确定和待维修状态返回成未处理
                    {
                        model.L_type = 0;
                        if (_INAReturnListDao.NUpdate(model))
                            return "0";//返回成功
                        else
                            return "1";//返回成功
                    }
                    else
                    {
                        return "2";//无权返回上级状态，请联系下级管理员
                    }
                }
                //维修页面（只能返回 3待定责  返回到 2待维修状态）
               else if (YHtype == "1")
                {
                    if (type == "3")//待定责状态返回到带维修状态
                    {
                        model.L_type = 2;
                        if (_INAReturnListDao.NUpdate(model))
                            return "0";//返回成功
                        else
                            return "1";//返回成功
                    }
                    else
                    {
                        return "2";//无权返回上级状态，请联系下级管理员
                    }
                }
                //定责页面 （返回 4待处理 状态的数据返回到 3 待定责状态）
              else  if (YHtype == "2")
                {
                    if (type == "4")
                    {
                        model.L_type = 3;
                        if (_INAReturnListDao.NUpdate(model))
                            return "0";//返回成功
                        else
                            return "1";//返回成功
                    }
                    else
                    {
                        return "2";//无权返回上级状态，请联系下级管理员
                    }
                }
                //营销中心（返回 6完成状态的数据 返回到 4 待处理状态）
                else if (YHtype == "3")
                {
                    if (type == "6")
                    {
                        model.L_type = 4;//完成状态
                        if (_INAReturnListDao.NUpdate(model))
                            return "0";//返回成功
                        else
                            return "1";//返回成功
                    }
                    else
                    {
                        return "2";//无权返回上级状态，请联系下级管理员
                    }
                }
                else
                {
                    return "4";//非法数据提交
                }
            }
            else
            {
                return "3";//不存在该数据
            }
        }

        #endregion

        #region //客户信息模糊查询
        public string cusInfolikename(string name)
        {
            string json;
            json = JsonConvert.SerializeObject(_INACustomerinfoDao.Getcusinfolikename(name));
            return json;
        }
        #endregion

        #region 

        #endregion

        #region //整理数据
  
        public JsonResult Arrangementdata()
        {
            try
            {
                IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.TemporaryGetcpnameisnulldata();
                if (modellist == null)
                    return Json(new { result = "error", explain = "没有需要整理的数据" });
                foreach (var item in modellist)
                {
                    NQ_THinfoFXView model = new NQ_THinfoFXView();
                    if (string.IsNullOrEmpty(item.Cpname))
                    {
                        model = item;
                        NQ_productinfoView cpmodel = new NQ_productinfoView();
                        cpmodel = _INQ_productinfoDao.NGetModelById(item.P_Id);
                        if (cpmodel != null)
                        {
                            model.Cpname = cpmodel.Pname;
                            model.Cpmode = cpmodel.P_xh;
                            model.Cpwlno = cpmodel.P_bianhao;
                        }
                        _INQ_THinfoFXDao.NUpdate(model);
                    }
                }
                return Json(new { result = "success", explain = "整理"+ modellist.Count+ "条，数据" });
            }
            catch
            {
                return Json(new { result = "error", explain = "操作异常" });
            }
        }
        #endregion



    }
}
