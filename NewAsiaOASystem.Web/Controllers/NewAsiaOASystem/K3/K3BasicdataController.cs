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
using System.Runtime.Serialization.Json;
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
    public class K3BasicdataController : Controller
    {
        //
        // GET: /K3Basicdata/
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
        INQ_BaseitemAttachmentDao _INQ_BaseitemAttachmentDao = ContextRegistry.GetContext().GetObject("NQ_BaseitemAttachmentDao") as INQ_BaseitemAttachmentDao;
        INQ_OASupplierDao _INQ_OASupplierDao = ContextRegistry.GetContext().GetObject("NQ_OASupplierDao") as INQ_OASupplierDao;
        INQ_SupplierAndBaseitemDao _INQ_SupplierAndBaseitemDao = ContextRegistry.GetContext().GetObject("NQ_SupplierAndBaseitemDao") as INQ_SupplierAndBaseitemDao;
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
        IIQC_SopInfoDao _IIQC_SopInfoDao = ContextRegistry.GetContext().GetObject("IQC_SopInfoDao") as IIQC_SopInfoDao;
        IFlow_RoutineStockinfoDao _IFlow_RoutineStockinfoDao = ContextRegistry.GetContext().GetObject("Flow_RoutineStockinfoDao") as IFlow_RoutineStockinfoDao;
        IFlow_NonSProductinfoDao _IFlow_NonSProductinfoDao = ContextRegistry.GetContext().GetObject("Flow_NonSProductinfoDao") as IFlow_NonSProductinfoDao;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult selectChanged(string itemid, string supplierid)
        {
            IList<NQ_BaseitemAttachmentView> selectedList = _INQ_BaseitemAttachmentDao.GetAttachByBaseitemAndSupplier(itemid, supplierid);
            NQ_SupplierAndBaseitemView sbi = _INQ_SupplierAndBaseitemDao.getBySupplierAndItem(supplierid, itemid);
            if (selectedList != null)
            {
                foreach (var s in selectedList)
                {
                    s.supplierAndBaseitemStatus = _INQ_BaseitemAttachmentDao.fileDateStatus(s);
                }
            }
            IList<NQ_SupplierAndBaseitemView> statuslist = new List<NQ_SupplierAndBaseitemView>();
            statuslist.Add(sbi);
            string jsonResult = JsonConvert.SerializeObject(selectedList);
            string statusResult = JsonConvert.SerializeObject(statuslist);

            return Json(new { result = jsonResult, status = statusResult });

            //return null;
        }


        public ActionResult editPage(string id)
        {
            K3_wuliaoinfoView baseitem = new K3_wuliaoinfoView();

            IList<NQ_SupplierAndBaseitemView> sidlist = _INQ_SupplierAndBaseitemDao.getSupplierByItemid(id);

            //已经有关系供应商列表
            IList<NQ_OASupplierView> listhadmodel = new List<NQ_OASupplierView>();
            //所有列表
            IList<NQ_OASupplierView> listnotmodel = _INQ_OASupplierDao.getSearchList(null, null, null);

            if (sidlist != null && sidlist.Count() > 0)
            {
                //设置已经添加附近的供应商
                foreach (var s in sidlist)
                {
                    listhadmodel.Add(_INQ_OASupplierDao.GetSupplierById(s.supplierid.ToString()));

                    listnotmodel = listnotmodel.Where(w => w.Id != s.supplierid).ToList();
                }
            }

            List<SelectListItem> notlist = new List<SelectListItem>()
            {
                    new SelectListItem(){Text="全部",Value="-1"},
            };

            foreach (var li in listnotmodel)
            {
                notlist.Add(new SelectListItem() { Text = li.FName, Value = li.Id.ToString() });
            }

            List<SelectListItem> hadlist = new List<SelectListItem>()
            {
                    new SelectListItem(){Text="全部",Value="-1"},
            };

            foreach (var li in listhadmodel)
            {
                hadlist.Add(new SelectListItem() { Text = li.FName, Value = li.Id.ToString() });
            }


            ViewBag.hadSupplier = hadlist;
            ViewBag.notSupplier = notlist;

            IList<NQ_BaseitemAttachmentView> attachments = new List<NQ_BaseitemAttachmentView>();
            if (!string.IsNullOrEmpty(id))
            {
                baseitem = _IK3_wuliaoinfoDao.GetBaseitemByItemId(id);

                baseitem.itemStatus = _IK3_wuliaoinfoDao.setSuStatus(baseitem);

                //attachments = _INQ_BaseitemAttachmentDao.GetAttachByBaseitemAndSupplier(id);

                ViewBag.CCCExist = 0;
                ViewBag.CQCExist = 0;
                ViewBag.SpecExist = 0;
                ViewBag.StdExist = 0;
                ViewBag.SampleExist = 0;
                ViewBag.QualityExist = 0;
                ViewBag.AppraisalsExist = 0;

            }

            return View("edit", baseitem);
        }

        public ActionResult CheckList(int? pageIndex)
        {
            IList<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.getSearchList(null, null, null, null);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            //PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

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

            PagerInfo<K3_wuliaoinfoView> listmodel1 = _IK3_wuliaoinfoDao.GetK3_wuliaoinfoList(null, null, null, null);


            return View(listmodel1);
        }


        public ActionResult CheckPage(string id)
        {
            K3_wuliaoinfoView supplier = new K3_wuliaoinfoView();
            IList<NQ_BaseitemAttachmentView> attachments = new List<NQ_BaseitemAttachmentView>();
            IList<NQ_SupplierAndBaseitemView> sidlist = _INQ_SupplierAndBaseitemDao.getSupplierByItemid(id);

            if (!string.IsNullOrEmpty(id))
            {
                supplier = _IK3_wuliaoinfoDao.GetBaseitemByItemId(id);
                //attachments = _INQ_BaseitemAttachmentDao(id)

                //已经有关系供应商列表
                IList<NQ_OASupplierView> listhadmodel = new List<NQ_OASupplierView>();
                //所有列表

                if (sidlist != null && sidlist.Count() > 0)
                {
                    //设置已经添加附近的供应商
                    foreach (var s in sidlist)
                    {
                        listhadmodel.Add(_INQ_OASupplierDao.GetSupplierById(s.supplierid.ToString()));
                    }
                }

                List<SelectListItem> hadlist = new List<SelectListItem>()
            {
                    new SelectListItem(){Text="全部",Value="-1"},
            };

                foreach (var li in listhadmodel)
                {
                    hadlist.Add(new SelectListItem() { Text = li.FName, Value = li.Id.ToString() });
                }

                List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="待审核",Value="0"},
                new SelectListItem(){Text="审核通过",Value="1"},
                new SelectListItem(){Text="未审核通过",Value="2"}
            };

                ViewBag.nameList = list;
                ViewBag.hadList = hadlist;


                IList<NQ_OASupplierView> listmodel = _INQ_OASupplierDao.getSearchList(null, null, null);
                IList<NQ_OASupplierView> supplierlist = _INQ_OASupplierDao.getSearchList(null, null, null);

                List<SelectListItem> list2 = new List<SelectListItem>()
            {
                    new SelectListItem(){Text="全部",Value="-1"},
            };

                foreach (var li in supplierlist)
                {
                    list2.Add(new SelectListItem() { Text = li.FName, Value = li.Id.ToString() });
                }

                ViewBag.searchSupplier = list2;


                ViewBag.FLicenceStatusName = "未设置";
                ViewBag.FTaxNumStatusName = "未设置";
                ViewBag.FISO9001StatusName = "未设置";
                ViewBag.FISO14000StatusName = "未设置";
                ViewBag.FPatentStatusName = "未设置";
                ViewBag.FOtherStatusName = "未设置";
                ViewBag.FQuestionnaireStatusName = "未设置";
                ViewBag.FAgentStatusName = "未设置";


            }
            return View("Check", supplier);
        }


        [HttpPost]
        public JsonResult checkBaseitem(FrameController from)
        {
            string itemid = Request.Form["Id"];

            string supplierid = Request.Form["hadList"];

            string checkstatus = Request.Form["checkList"];

            string rtnid = "";

            //K3_wuliaoinfoView smodel = new K3_wuliaoinfoView();
            NQ_SupplierAndBaseitemView smodel = new NQ_SupplierAndBaseitemView();

            try
            {
                bool flay = false;

                SessionUser user = Session[SessionHelper.User] as SessionUser;

                if ((int.Parse(itemid)) > 0)
                {
                    smodel = _INQ_SupplierAndBaseitemDao.getBySupplierAndItem(supplierid, itemid);

                    //smodel.ischecked = int.Parse(checkstatus);


                    flay = _INQ_SupplierAndBaseitemDao.NUpdate(smodel);

                    rtnid = smodel.itemid.ToString();
                }

                if (flay)
                {
                    return Json(new { result = "success", id = itemid });
                }
                else
                    return Json(new { result = "error", id = itemid });
            }
            catch (Exception e)
            {
                return Json(new { result = "error", id = itemid });
            }
        }


        public ActionResult List(int? pageIndex)
        {
            IList<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.getSearchList(null, null, null, null);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            //PagerInfo<NQ_OASupplierView> rtnlist = _INQ_OASupplierDao.setPagerInfo(listmodel, CurrentPageIndex, 11);

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

            PagerInfo<K3_wuliaoinfoView> listmodel1 = GetListPager(pageIndex, null, null, null, null);

            return View(listmodel1);
        }


        /// <summary>
        /// K3数据同步 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TBk3wuliaoView(int? pageIndex)
        {
            PagerInfo<K3_wuliaoinfoView> listmodel = GetListPager(pageIndex, null, null, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string fnumber = Request.Form["fnumber"];//物料代码
            string fname = Request.Form["fname"];//物料名称
            string fmodel = Request.Form["fmodel"];//物料型号
            string type = Request.Form["type"];//型号
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<K3_wuliaoinfoView> listmodel = GetListPager(pageIndex, fnumber, fname, fmodel, type);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //获取分页数据
        private PagerInfo<K3_wuliaoinfoView> GetListPager(int? pageIndex, string fnumber, string fname, string fmodel, string type)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IK3_wuliaoinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IK3_wuliaoinfoDao.SetPagerPageSize(10);
            PagerInfo<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.GetK3_wuliaoinfoList(fnumber, fname, fmodel, type);
            return listmodel;
        }
        #endregion

        #region //获取k3基础数据的方法
        public string Getk3ajaxjson()
        {
            //获取自增号最大的数据
            K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.GetwuliaoMaxfitem();
            int? t = 0;
            string url;
            if (model != null)
            {
                t = model.fitem;
            }
            url = "http://222.92.203.58:83//Baseitem.asmx/getUpdatedItemByid?fitemid=" + t;
            string result = HttpUtility11.GetData(url);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            List<jsonmolde> timemodel = getObjectByJson<jsonmolde>(sSupplier);
            foreach (var a in timemodel)
            {
                K3_wuliaoinfoView k3model = new K3_wuliaoinfoView();
                k3model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(a.fnumber);
                if (k3model == null)//不存在
                {
                    k3model = new K3_wuliaoinfoView();
                    k3model.fitem = Convert.ToInt32(a.fitem);
                    k3model.fnumber = a.fnumber;
                    k3model.fname = a.fname;
                    k3model.fmodel = a.fmodel;
                    k3model.forderprice = Convert.ToDecimal(a.forderprice);
                    k3model.fnetweight = Convert.ToDecimal(a.fnetweight);
                    k3model.Type = Convert.ToInt32(Getwuliaotypebyfnumber(a.fnumber));
                    k3model.str1 = Getwuliaotwobyfnumber(a.fnumber);
                    k3model.str2 = Getwuliaosanweibyfnumber(a.fnumber);
                    k3model.C_Time = DateTime.Now;
                    k3model.unitname = a.unitname;
                    _IK3_wuliaoinfoDao.Ninsert(k3model);
                }
                else
                {

                }
            }
            if (timemodel == null)
            {
                return "0";
            }
            else
            {
                return timemodel.Count().ToString();//返回同步的条数
            }
        }
        #endregion

        #region //根据物料编码获取K3的数据更新平台的数据
        /// <summary>
        /// 根据物料编码获取K3的数据更新平台的数据
        /// </summary>
        /// <param name="fnumber">物料编码</param>
        /// <returns></returns>
        public JsonResult updatek3databyfnumber(string fnumber)
        {
            //查询平台中的K3物料
            K3_wuliaoinfoView k3model = new K3_wuliaoinfoView();
            k3model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(fnumber);
            if (k3model != null)
            {
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/getUpdatedItemByfnumber?fnumber=" + fnumber;
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                List<jsonmolde> timemodel = getObjectByJson<jsonmolde>(sSupplier);
                if (timemodel != null)
                {
                    foreach (var a in timemodel)
                    {
                        k3model.fname = a.fname;
                        k3model.fmodel = a.fmodel;
                    }
                    _IK3_wuliaoinfoDao.NUpdate(k3model);//更新基础数据的名称
                    //查询平台的基础数据
                    NQ_productinfoView cpmodel = _INQ_productinfoDao.GetProinfobyname(fnumber);
                    if (cpmodel != null)
                    {
                        cpmodel.Pname = k3model.fname;//产品名称
                        cpmodel.P_xh = k3model.fmodel;//产品型号
                        _INQ_productinfoDao.NUpdate(cpmodel);
                    }
                    //查询元器件数据
                    NQ_YJinfoView yqjmodel = _INQ_YJinfoDao.GetYqjModelbyW_dm(fnumber);
                    if (yqjmodel != null)
                    {
                        yqjmodel.Y_Name= k3model.fname;//产品名称
                        yqjmodel.Y_Ggxh= k3model.fmodel;//产品型号
                        _INQ_YJinfoDao.NUpdate(yqjmodel);
                    }
                    //查询元器件检验标准
                    IQC_SopInfoView IQCmodel = _IIQC_SopInfoDao.Getsopinfobyyqjwlno(fnumber);
                    if (IQCmodel != null)
                    {
                        
                        IQCmodel.Yqjname= k3model.fname;//产品名称
                        IQCmodel.Yqjxh = k3model.fmodel;//产品型号
                        _IIQC_SopInfoDao.NUpdate(IQCmodel);
                    }
                    //查询常规电控温控产品
                    Flow_RoutineStockinfoView cgcpmodel = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(fnumber);
                    if (cgcpmodel != null)
                    {
                        cgcpmodel.P_Model= k3model.fmodel;//产品型号
                        cgcpmodel.P_Name= k3model.fname;//产品名称
                        _IFlow_RoutineStockinfoDao.NUpdate(cgcpmodel);
                    }
                    //查询非标电控温控产品
                    Flow_NonSProductinfoView fbcpmodel = _IFlow_NonSProductinfoDao.Getmodelbywldm(fnumber);
                    if (fbcpmodel != null)
                    {
                        fbcpmodel.Pname= k3model.fname;//产品名称
                        fbcpmodel.Pmodel= k3model.fmodel;//产品型号
                        _IFlow_NonSProductinfoDao.NUpdate(fbcpmodel);
                    }
                    return Json(new { result = "success",res="操作完成" });
                }
                else
                {
                    return Json(new { result = "error", res = "K3接口没有返回数据，请查正物料代码！" });
                }
            }
            else {
                return Json(new { result = "error", res = "平台中尚未同步该物料，请尝试先同步K3数据！" });
            }
        }
        #endregion

        #region //根据物料编码获取普实ERP中的数据更新平台的数据
        public JsonResult updatePsdatabyfnumber(string fnumber)
        {
            //查询平台中的K3物料
            K3_wuliaoinfoView k3model = new K3_wuliaoinfoView();
            k3model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(fnumber);
            if (k3model != null)
            {
                string resjson = zypushsoftHelper.Get_MDItm_by_ItmID(fnumber);
                if (resjson != null || resjson != null || resjson != "")
                {
                    List<Psjsonmodel> timemodel = getObjectByJson<Psjsonmodel>(resjson);
                    foreach (var a in timemodel)
                    {
                        k3model.fname = a.ItmName;
                        k3model.fmodel = a.ItmSpec;
                        k3model.unitname = a.UomName;
                        k3model.WhsName = a.WhsName;
                        k3model.SourceID = a.SourceID;
                        k3model.IsClose = a.IsClose;
                        k3model.Z_WLSX = a.Z_WLSX;
                        k3model.updatetime = DateTime.Now;
                    }
                    _IK3_wuliaoinfoDao.NUpdate(k3model);//更新基础数据的名称
                                                        //查询元器件数据
                    NQ_YJinfoView yqjmodel = _INQ_YJinfoDao.GetYqjModelbyW_dm(fnumber);
                    if (yqjmodel != null)
                    {
                        yqjmodel.Y_Name = k3model.fname;//产品名称
                        yqjmodel.Y_Ggxh = k3model.fmodel;//产品型号
                        _INQ_YJinfoDao.NUpdate(yqjmodel);
                    }
                    //查询元器件检验标准
                    IQC_SopInfoView IQCmodel = _IIQC_SopInfoDao.Getsopinfobyyqjwlno(fnumber);
                    if (IQCmodel != null)
                    {

                        IQCmodel.Yqjname = k3model.fname;//产品名称
                        IQCmodel.Yqjxh = k3model.fmodel;//产品型号
                        _IIQC_SopInfoDao.NUpdate(IQCmodel);
                    }
                    //查询常规电控温控产品
                    Flow_RoutineStockinfoView cgcpmodel = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(fnumber);
                    if (cgcpmodel != null)
                    {
                        cgcpmodel.P_Model = k3model.fmodel;//产品型号
                        cgcpmodel.P_Name = k3model.fname;//产品名称
                        _IFlow_RoutineStockinfoDao.NUpdate(cgcpmodel);
                    }
                    //查询非标电控温控产品
                    Flow_NonSProductinfoView fbcpmodel = _IFlow_NonSProductinfoDao.Getmodelbywldm(fnumber);
                    if (fbcpmodel != null)
                    {
                        fbcpmodel.Pname = k3model.fname;//产品名称
                        fbcpmodel.Pmodel = k3model.fmodel;//产品型号
                        _IFlow_NonSProductinfoDao.NUpdate(fbcpmodel);
                    }
                    return Json(new { result = "success", res = "操作完成" });
                }
                else
                {
                    return Json(new { result = "error", res = "ERP接口没有返回数据，请查正物料代码！" });
                }
            }
            else
            {
                return Json(new { result = "error", res = "平台中尚未同步该物料，请尝试先同步ERP数据！" });
            }
        }
        #endregion

        //返回json数据 转换方法
        private static List<T> getObjectByJson<T>(string jsonString)
        {
            // 实例化DataContractJsonSerializer对象，需要待序列化的对象类型  
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            //把Json传入内存流中保存  
            //jsonString = jsonString;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象  
            object ob = serializer.ReadObject(stream);
            List<T> ls = (List<T>)ob;
            return ls;
        }

        //返回json 转换实体
        public class jsonmolde
        {
            /// <summary>
            /// 自增列
            /// </summary>
            public virtual string fitem { get; set; }

            /// <summary>
            /// 产品名称
            /// </summary>
            public virtual string fname { get; set; }

            /// <summary>
            /// 物料代码
            /// </summary>
            public virtual string fnumber { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public virtual string forderprice { get; set; }

            /// <summary>
            /// 型号
            /// </summary>
            public virtual string fmodel { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public virtual string unitname { get; set; }

            /// <summary>
            /// 重量
            /// </summary>
            public virtual string fnetweight { get; set; }
        }

        public class Psjsonmodel 
        { 
            /// <summary>
            /// 物料号
            /// </summary>
           public virtual string ItmID { get; set; }

            /// <summary>
            /// 物料名称
            /// </summary>
            public virtual string ItmName { get; set; }

            /// <summary>
            /// 前俩段编码
            /// </summary>
            public virtual string ItmKindID { get; set; }

            public virtual string ItmKindName { get; set; }


            public virtual string ItmGrpID { get; set; }

            /// <summary>
            /// 规格型号
            /// </summary>
            public virtual string ItmSpec { get; set; }

            public virtual string IsClose { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public virtual string UomName { get; set; }

            public virtual string OpDate { get; set; }

            public virtual string WhsName { get; set; }
            public virtual string SourceID { get; set; }

            public virtual string Z_WLSX { get; set; }
 
        }

        //更加物料代码返回物料是属于那个仓的
        public string Getwuliaotypebyfnumber(string fnumber)
        {
            string str;
            str = fnumber.Substring(0, 2);
            switch (str)
            {
                case "01":
                    return "0";
                case "02":
                    return "1";
                case "03":
                    return "2";
                case "04":
                    return "3";
                case "05":
                    return "4";
                case "06":
                    return "5";
                case "07":
                    return "6";
                case "08":
                    return "7";
            }
            return "7";
        }

        //测试
        public void Test()
        {
            string fnumber = "02.014.0099";
            string str1 = Getwuliaotwobyfnumber(fnumber);
            string str2 = Getwuliaosanweibyfnumber(fnumber);
        }

        //数据整理
        public string datazhengli()
        {
            IList<K3_wuliaoinfoView> modellist = _IK3_wuliaoinfoDao.NGetList();
            if (modellist != null)
            {
                foreach (var item in modellist)
                {
                    K3_wuliaoinfoView model = new K3_wuliaoinfoView();
                    model = item;
                    model.str1 = Getwuliaotwobyfnumber(model.fnumber);
                    model.str2 = Getwuliaosanweibyfnumber(model.fnumber);
                    _IK3_wuliaoinfoDao.NUpdate(model);
                }
                return "0";
            }
            return "1";
        }

        //返回物料代码前俩位数
        public string Getwuliaotwobyfnumber(string fnumber)
        {
            string str;
            str = fnumber.Substring(0, 2);
            return str;
        }

        //返回物料中间三位数
        public string Getwuliaosanweibyfnumber(string fnumber)
        {
            try
            {
                string str;
                str = fnumber.Substring(3, 3);
                return str;
            }
            catch
            {
                return "";
            }
        }

        [HttpPost]
        public JsonResult updateOABaseitem(FrameController from)
        {
            string Id = Request.Form["Id"];
            string notList = Request.Form["notList"];
            string hadList = Request.Form["hadList"];

            //NQ_SupplierAndBaseitemView smodel = new NQ_SupplierAndBaseitemView();

            SessionUser user = Session[SessionHelper.User] as SessionUser;

            try
            {
                bool flay = false;
                if (int.Parse(notList) < 0 && int.Parse(hadList) > 0)
                {// update
                    flay = true;

                }
                //else if (int.Parse(notList) > 0 && int.Parse(hadList) < 0)
                //{//insert

                //    smodel.itemid = int.Parse(Id);
                //    smodel.supplierid = int.Parse(notList);
                //    smodel.updatedDate = DateTime.Now.Date;
                //    flay = _INQ_SupplierAndBaseitemDao.Ninsert(smodel);
                //}

                string rtnid = "";

                if (flay)
                {
                    return Json(new { result = "success", id = rtnid });
                }
                else
                    return Json(new { result = "error", id = Id });
            }
            catch (Exception e)
            {
                return Json(new { result = "error", id = Id });
            }
        }

        #region //平台批量修改产品型号根据物料编码
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BatcupdateprodectinfoxhbyfnumberView()
        {
            return View();
        }
        [HttpPost]
        public JsonResult BatcDRCPinfoExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                Batcupdatecpinfo(dt);
                return Json(new { result = "success" , res ="操作成功"});
            }
            else
            {
                return Json(new { result = "error", res = "操作失败" });
            }
        }

        //更新产品信息
        public void Batcupdatecpinfo(System.Data.DataTable dt)
        {
            string fnumber = "";//产品编号
            //string fxh = "";//产品型号
  
            foreach (DataRow dr in dt.Rows)
            {
                fnumber = dr["fnumber"].ToString().Trim();//产品编号
                updatek3databyfnumber(fnumber);
            }

        }

        //Excel数据导入Datable
        /// <summary>
        /// Excel数据导入Datable
        /// </summary>
        /// <param name="fileUrl">excel 文件路径</param>
        /// <param name="table"></param>
        /// <returns></returns>
        public System.Data.DataTable GetExcelDatatable(string fileUrl, string table)
        {
            //office2007之前 仅支持.xls
            //const string cmdText = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1';";
            //支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            const string cmdText = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            System.Data.DataTable dt = null;
            //建立连接
            OleDbConnection conn = new OleDbConnection(string.Format(cmdText, fileUrl));
            try
            {
                //打开连接
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //获取Excel的第一个Sheet名称
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                //查询sheet中的数据
                string strSql = "select * from [" + sheetName + "]";
                OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[0];
                return dt;

            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }
        #endregion

        #region //批量修改ERP中物料的数据[修改各个工序工时]
        public ActionResult BatcupdateERPMDItm_GXGSView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BatcDRERPMDItm_GXGSinfoExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                BatcupdateMDItm_GXGS(dt);
                return Json(new { result = "success", res = "操作成功" });
            }
            else
            {
                return Json(new { result = "error", res = "操作失败" });
            }
        }

        //更普实物料【各个工序的工时字段】
        public void BatcupdateMDItm_GXGS(System.Data.DataTable dt)
        {
            string fnumber = "";
            string Z_DQZZDBGS = "";
            string Z_DQZZKZXGS = "";
            string Z_DQZZZHLGS = "";
            string Z_DQZZMBZXGS = "";
            string Z_DQZZMBJXGS = "";
            foreach (DataRow dr in dt.Rows)
            {
                fnumber = dr["fnumber"].ToString().Trim();//产品编号
                Z_DQZZDBGS = dr["底板装配工时"].ToString().Trim();
                Z_DQZZKZXGS= dr["控制线工时"].ToString().Trim();
                Z_DQZZZHLGS= dr["主回路线工时"].ToString().Trim();
                Z_DQZZMBZXGS= dr["面板装箱工时"].ToString().Trim();
                Z_DQZZMBJXGS= dr["面板接线工时"].ToString().Trim();
                if (fnumber != "" && fnumber != null)
                {
                    if (Z_DQZZDBGS == "" || Z_DQZZDBGS == null)
                        Z_DQZZDBGS = "0";
                    if (Z_DQZZKZXGS == "" || Z_DQZZKZXGS == null)
                        Z_DQZZKZXGS = "0";
                    if (Z_DQZZZHLGS == "" || Z_DQZZZHLGS == null)
                        Z_DQZZZHLGS = "0";
                    if (Z_DQZZMBZXGS == "" || Z_DQZZMBZXGS == null)
                        Z_DQZZMBZXGS = "0";
                    if (Z_DQZZMBJXGS == "" || Z_DQZZMBJXGS == null)
                        Z_DQZZMBJXGS = "0";
                    zypushsoftHelper.updateMDItm_GXGS(fnumber, Z_DQZZDBGS, Z_DQZZKZXGS, Z_DQZZZHLGS, Z_DQZZMBZXGS, Z_DQZZMBJXGS);
                }
            }
        }
        #endregion

        #region //批量修改ERP中物料数据【修改内部售价字段】
        public ActionResult BatcupdateMDItm_NBPriceView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BatcDRMDItm_NBPriceExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                BatcMDItm_NBPrice(dt);
                return Json(new { result = "success", res = "操作成功" });
            }
            else
            {
                return Json(new { result = "error", res = "文件为空" });
            }
        }

        //更新普实物料【内部售价字段】
        public void BatcMDItm_NBPrice(System.Data.DataTable dt)
        {
            string fnumber = "";
            string NBPrice = "";
            foreach (DataRow dr in dt.Rows)
            {
                fnumber = dr["fnumber"].ToString().Trim();//产品编号
                NBPrice=dr["NBPrice"].ToString().Trim();
                if (fnumber != "" && fnumber != null)
                {
                    if (NBPrice == "" || NBPrice == null)
                        NBPrice = "0";
                    zypushsoftHelper.updateupdateMDItm_Z_NBPrice(fnumber, NBPrice);
                }
            }
        }
        #endregion

        #region //批量修改ERP中物料数据【修改物料分类属性字段】
        public ActionResult BatcupdateMDItm_Z_WLSXView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BatcDRMDItm_Z_WLSXExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                BatcMDItm_Z_WLSX(dt);
                return Json(new { result = "success", res = "操作成功" });
            }
            else
            {
                return Json(new { result = "error", res = "文件为空" });
            }
        }

        public void BatcMDItm_Z_WLSX(System.Data.DataTable dt)
        {
            string fnumber = "";
            string Z_WLSX = "";
            foreach (DataRow dr in dt.Rows)
            {
                fnumber = dr["fnumber"].ToString().Trim();//产品编号
                Z_WLSX = dr["Z_WLSX"].ToString().Trim();
                if (fnumber != "" && fnumber != null)
                {
                    zypushsoftHelper.updateMDItm_Z_WLSX(fnumber, Z_WLSX);
                }
            }
        }
        #endregion

        #region //同步普实物料
        public JsonResult TBwlpushi(string fnumber)
        {
            try
            {
                K3_wuliaoinfoView k3model = new K3_wuliaoinfoView();
                k3model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(fnumber);
                if (k3model != null)
                {
                    Ps_fbcpmodel pscpmodel = new Ps_fbcpmodel();
                    pscpmodel.ItmID = k3model.fnumber;
                    pscpmodel.ItmSpec = k3model.fname;
                    pscpmodel.Z_ItmID = k3model.fnumber;
                    pscpmodel.Z_Price = k3model.forderprice.ToString();
                    string res = pushsoftHelper.Instercpinfo(pscpmodel);
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "同步接口返回空" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            return Json(new { result = "success", msg = errmodel.ErrMsg });
                        }
                        else
                        {
                            return Json(new { result = "error", msg = errmodel.ErrMsg });
                        }
                    }
                }
                else
                {
                    return Json(new { result = "error", res = "数据不存在！" });
                }
            }
            catch
            {
                return Json(new { result = "error", res = "操作异常！" });
            }
        }
        #endregion

        #region //查询比对K3料单和普实料单
        public ActionResult BomcomparisonView(string Id)
        {
            K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.NGetModelById(Id);
            return View(model);  
        }


        #region //查询K3的BOM明细
        public string Getk3bombywlno(string wlno)
        {
            string strjson = K3Helper.GetBombywlno(wlno);
            List<k3bommode> timemodel = getObjectByJson<k3bommode>(strjson);
            return strjson;
        }
        #endregion

        #region //插入普实BOM
        //插入普实BOM
        public JsonResult Ps_InstarBom(string Id)
        {
            try
            {
                K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    Ps_Bommodel PsBomnodel = new Ps_Bommodel();
                    PsBomnodel.BomID = model.fnumber;
                    PsBomnodel.BomName = model.fname;
                    PsBomnodel.ItmID = model.fnumber;
                    PsBomnodel.VerNum = "V 1.0";
                    PsBomnodel.RouID = DateTime.Now.ToString();
                    if (model.Type == 3 || model.Type == 5)
                    {
                        PsBomnodel.Procedures = Getwxgongxu();
                    }
                    else if (model.Type == 4)
                    {
                        PsBomnodel.Procedures = Getgongxu();
                    }
                    else
                    {
                        return Json(new { result = "error", msg = "原材料不需要同步BOM" });
                    }
                
                    PsBomnodel.Items = Getyongliao(model.fnumber);
                    if (PsBomnodel.Items == null)
                    {
                        return Json(new { result = "error", msg = "没有获取到K3的BOM" });
                    }
                    string res = pushsoftHelper.InstaerBominfo(PsBomnodel);
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "同步接口返回空" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                           
                            return Json(new { result = "success", msg = errmodel.ErrMsg });
                        }
                        else
                        {
                            return Json(new { result = "error", msg = errmodel.ErrMsg });
                        }
                    }
                }
                else
                {
                    return Json(new { result = "error", msg = "物料不存在" });
                }
            }
            catch
            {
                return Json(new { result = "error", msg = "同步失败" });
            }
        }

        public class k3bommode
        {
            public string 子项物料代码 { get; set; }

            public string 子项物料名称 { get; set; }

            public string 子物料型号 { get; set; }

            public string 单位用量 { get; set; }


        }

        #region //用料
        public IList<Itemsmodel> Getyongliao(string wlno)
        {
            string strjson = K3Helper.GetBombywlno(wlno);
            if (strjson != "[]")
            {
                List<k3bommode> timemodel = getObjectByJson<k3bommode>(strjson);
                List<Itemsmodel> wuliaolist = new List<Itemsmodel>();
                if (timemodel != null)
                {

                    foreach (var item in timemodel)
                    {
                        //查询物料
                        K3_wuliaoinfoView jcwlmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(item.子项物料代码);
                        Itemsmodel wlmodel = new Itemsmodel();
                        wlmodel.ItmID = item.子项物料代码;
                        wlmodel.NetQty = Convert.ToDecimal(item.单位用量);
                        wlmodel.ScrapRate = "0";
                        if (jcwlmodel != null)
                        {
                            if (jcwlmodel.SourceID == "制造")
                            {
                                wlmodel.LineType = "M";
                            }
                            else
                            {
                                wlmodel.LineType = "P";
                            }
                        }
                        else
                        {
                            wlmodel.LineType = "P";
                        }
                        wlmodel.LineType = "P";
                        wlmodel.OperationLine = "10";
                        wlmodel.Position = "1";
                      
                        wuliaolist.Add(wlmodel);
                    }
                }
                return wuliaolist;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region //电控箱的工序
        //工序 常规电控的工序
        public IList<Proceduresmodel> Getgongxu()
        {
            List<Proceduresmodel> gxmxlist = new List<Proceduresmodel>();
            Proceduresmodel gongx = new Proceduresmodel();
            gongx.LineNum = "10";
            gongx.PrcID = "DQ01";
            gongx.PrcName = "底板装配";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "0";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "20";
            gongx.PrcID = "DQ02";
            gongx.PrcName = "接控制线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "10";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "30";
            gongx.PrcID = "DQ03";
            gongx.PrcName = "接主回路线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "20";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "40";
            gongx.PrcID = "DQ04";
            gongx.PrcName = "上温控线绕管";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "30";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "50";
            gongx.PrcID = "DQ05";
            gongx.PrcName = "面板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "40";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "60";
            gongx.PrcID = "DQ06";
            gongx.PrcName = "底板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "50";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "70";
            gongx.PrcID = "DQ07";
            gongx.PrcName = "接温控线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "60";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "80";
            gongx.PrcID = "DQ08";
            gongx.PrcName = "焊灯";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "70";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "90";
            gongx.PrcID = "DQ09";
            gongx.PrcName = "调试";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "80";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "100";
            gongx.PrcID = "DQ10";
            gongx.PrcName = "打包";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "90";
            gxmxlist.Add(gongx);
            return gxmxlist;
        }

        //工序 温控的工序
        public IList<Proceduresmodel> Getwxgongxu()
        {
            List<Proceduresmodel> gxmxlist = new List<Proceduresmodel>();
            Proceduresmodel gongx = new Proceduresmodel();
            gongx.LineNum = "10";
            gongx.PrcID = "DZ01";
            gongx.PrcName = "SMT";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "0";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "20";
            gongx.PrcID = "DZ02";
            gongx.PrcName = "插件";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "10";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "30";
            gongx.PrcID = "DZ03";
            gongx.PrcName = "焊接";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "20";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "40";
            gongx.PrcID = "DZ04";
            gongx.PrcName = "洗板";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "30";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "50";
            gongx.PrcID = "DZ05";
            gongx.PrcName = "看板";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "40";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "60";
            gongx.PrcID = "DZ06";
            gongx.PrcName = "烧录";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "50";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "70";
            gongx.PrcID = "DZ07";
            gongx.PrcName = "初检";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "60";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "80";
            gongx.PrcID = "DZ13";
            gongx.PrcName = "灌胶";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "70";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "90";
            gongx.PrcID = "DQ09";
            gongx.PrcName = "调试";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "80";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "100";
            gongx.PrcID = "DZ08";
            gongx.PrcName = "老化";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "90";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "110";
            gongx.PrcID = "DZ09";
            gongx.PrcName = "防潮";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "100";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "120";
            gongx.PrcID = "DZ10";
            gongx.PrcName = "装配";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "110";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "130";
            gongx.PrcID = "DZ11";
            gongx.PrcName = "总检";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "120";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "140";
            gongx.PrcID = "DZ12";
            gongx.PrcName = "包装";
            gongx.WcnID = "002";
            gongx.WcnName = "电子车间";
            gongx.PreLineNum = "130";
            gxmxlist.Add(gongx);
            return gxmxlist;
        }
        #endregion
        #endregion

        #region //查询普实的BOM明细
        public string Getpushbywlno(string wlno)
        {
            string strjson = zypushsoftHelper.GetBom_QYbywlno(wlno);
            return strjson;
        }
        #endregion

        #region //更新BOM明细
        public JsonResult updatePSbommx(string wlno, string DocEntry)
        {
            string strjson = K3Helper.GetBombywlno(wlno);//查询K3
            if (strjson != "[]")
            {
                List<k3bommode> timemodel = getObjectByJson<k3bommode>(strjson);
                //S删除普实BOM细表
                string delres = zypushsoftHelper.DelBomA(DocEntry);
                //循环插入明细表
                int iLineNum = 0;
                int IOrderNum = 10;
                foreach (var item in timemodel)
                {
                    //查询物料
                    K3_wuliaoinfoView jcwlmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(item.子项物料代码);
                    iLineNum++;
                    FInstaerMDBomA mxmodel = new FInstaerMDBomA();
                    mxmodel.DocEntry = DocEntry;
                    mxmodel.ItmID = item.子项物料代码;
                    mxmodel.LineNum = iLineNum.ToString();
                    mxmodel.NetQty = Convert.ToDecimal(item.单位用量);
                   
                    if (jcwlmodel != null)
                    {
                        if (jcwlmodel.SourceID == "制造")
                        {
                            mxmodel.LineType = "M";
                        }
                        else
                        {
                            mxmodel.LineType = "P";
                        }
                    }
                    else
                    {
                        mxmodel.LineType = "P";
                    }
                    mxmodel.OrderNum = IOrderNum;
                    IOrderNum = IOrderNum + 10;
                    string datejson = JsonConvert.SerializeObject(mxmodel);
                    datejson = "[" + datejson + "]";
                    string INSRES = zypushsoftHelper.UpdateBomA(datejson);

                }
                return Json(new { result = "success", msg = "操作成功" });
            }
            else
            {
                return Json(new { result = "error", msg ="K3bom接口异常" });
            }
        }
        #endregion
        #endregion

        #region //获取普实基础数据的方法
        public JsonResult GetBMitmejson()
        {
            //获取最近的时间
            K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.GetwuliaoMaxOpDate();
            string OpDate = "2021-04-01";
            if (model != null)
            {
                OpDate =Convert.ToDateTime(model.OpDate).ToString("yyyy-MM-dd");
            }
            string resjson = zypushsoftHelper.GetMDItmbyOpDate(OpDate);
            int xz = 0;
            int gz = 0;
            if (resjson != null || resjson != null)
            {
                List<Psjsonmodel> timemodel = getObjectByJson<Psjsonmodel>(resjson);
                foreach (var a in timemodel)
                {
                    K3_wuliaoinfoView k3model = new K3_wuliaoinfoView();
                    k3model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(a.ItmID);
                    if (k3model == null)//不存在
                    {
                        xz = xz + 1;
                        k3model = new K3_wuliaoinfoView();
                        k3model.fnumber = a.ItmID;
                        k3model.fname = a.ItmName;
                        k3model.fmodel = a.ItmSpec;
                        k3model.Type = Convert.ToInt32(Getwuliaotypebyfnumber(a.ItmID));
                        k3model.str1 = Getwuliaotwobyfnumber(a.ItmID);
                        k3model.str2 = Getwuliaosanweibyfnumber(a.ItmID);
                        k3model.unitname = a.UomName;
                        k3model.C_Time = DateTime.Now;
                        k3model.updatetime = DateTime.Now;
                        k3model.OpDate = Convert.ToDateTime(a.OpDate);
                        k3model.WhsName = a.WhsName;
                        k3model.SourceID = a.SourceID;
                        k3model.IsClose = a.IsClose;
                        k3model.Z_WLSX = a.Z_WLSX;
                        _IK3_wuliaoinfoDao.Ninsert(k3model);
                    }
                    else
                    {
                        gz = gz + 1;
                        k3model.OpDate = Convert.ToDateTime(a.OpDate);
                        k3model.WhsName = a.WhsName;
                        k3model.SourceID = a.SourceID;
                        k3model.IsClose = a.IsClose;
                        k3model.Z_WLSX = a.Z_WLSX;
                        k3model.updatetime = DateTime.Now;
                        _IK3_wuliaoinfoDao.NUpdate(k3model);
                    }
                }
                return Json(new { result = "success", msg ="新增数量："+xz +"条，更新："+gz+"条" });

            }
            else
            {
                return Json(new { result = "error", msg = "数据为空！"  });
            }


        }
        #endregion


        #region //产品标准工时计算
        #region //页面
        public ActionResult CalculationSWH()
        {
            return View();
        }

        #region //根据BOM和物料返回工序工时计算产品的标准工时
        public JsonResult GetchanpanSWH(string wlno,string xishu)
        {

            try
            {
                decimal? hz_Z_DQZZDBGS = 0;
                decimal? hz_Z_DQZZKZXGS = 0;
                decimal? hz_Z_DQZZZHLGS = 0;
                decimal? hz_Z_DQZZMBZXGS = 0;
                decimal? hz_Z_DQZZMBJXGS = 0;
                decimal? hz_zgs = 0;
                string strjson = zypushsoftHelper.GetBom_QYbywlno(wlno);
                if (strjson != null || strjson != "[]")
                {
                    List<bcmodel> timemodel = getObjectByJson<bcmodel>(strjson);
                    List<bcmodel> Newmodel = new List<bcmodel>(); 
                    foreach (var item in timemodel)
                    {
                        //查询物料工序工时
                        string wlgxgsjson = zypushsoftHelper.GetMDItm_GXGS(item.ItmID);
                        if (wlgxgsjson != null || wlgxgsjson != "[]")
                        {
                            List<Ps_wlGXGSModel> wlgsmodel = getObjectByJson<Ps_wlGXGSModel>(wlgxgsjson);
                            Ps_wlGXGSModel gxgsmodel = wlgsmodel[0];
                            if (gxgsmodel.Z_DQZZDBGS == null)
                                gxgsmodel.Z_DQZZDBGS = "0";
                            gxgsmodel.Z_DQZZDBGS = (Convert.ToDecimal(gxgsmodel.Z_DQZZDBGS) * Convert.ToDecimal(xishu)).ToString();
                            item.b_Z_DQZZDBGS =(item.NetQty *Convert.ToDecimal(gxgsmodel.Z_DQZZDBGS)).ToString();
                            hz_Z_DQZZDBGS = hz_Z_DQZZDBGS + (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZDBGS));
                            if (gxgsmodel.Z_DQZZKZXGS == null)
                                gxgsmodel.Z_DQZZKZXGS = "0";
                            gxgsmodel.Z_DQZZKZXGS = (Convert.ToDecimal(gxgsmodel.Z_DQZZKZXGS) * Convert.ToDecimal(xishu)).ToString();
                            item.b_Z_DQZZKZXGS = (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZKZXGS)).ToString();
                            hz_Z_DQZZKZXGS = hz_Z_DQZZKZXGS + (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZKZXGS));
                            if (gxgsmodel.Z_DQZZZHLGS == null)
                                gxgsmodel.Z_DQZZZHLGS = "0";
                            gxgsmodel.Z_DQZZZHLGS = (Convert.ToDecimal(gxgsmodel.Z_DQZZZHLGS) * Convert.ToDecimal(xishu)).ToString();
                            item.b_Z_DQZZZHLGS = (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZZHLGS)).ToString();
                            hz_Z_DQZZZHLGS = hz_Z_DQZZZHLGS + (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZZHLGS));
                            if (gxgsmodel.Z_DQZZMBZXGS == null)
                                gxgsmodel.Z_DQZZMBZXGS = "0";
                            gxgsmodel.Z_DQZZMBZXGS = (Convert.ToDecimal(gxgsmodel.Z_DQZZMBZXGS) * Convert.ToDecimal(xishu)).ToString();
                            item.b_Z_DQZZMBZXGS = (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZMBZXGS)).ToString();
                            hz_Z_DQZZMBZXGS = hz_Z_DQZZMBZXGS + (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZMBZXGS));
                            if (gxgsmodel.Z_DQZZMBJXGS == null)
                                gxgsmodel.Z_DQZZMBJXGS = "0";
                            gxgsmodel.Z_DQZZMBJXGS = (Convert.ToDecimal(gxgsmodel.Z_DQZZMBJXGS) * Convert.ToDecimal(xishu)).ToString();
                            item.b_Z_DQZZMBJXGS = (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZMBJXGS)).ToString();
                            hz_Z_DQZZMBJXGS = hz_Z_DQZZMBJXGS + (item.NetQty * Convert.ToDecimal(gxgsmodel.Z_DQZZMBJXGS));
                            item.b_zgs =Convert.ToString(Convert.ToDecimal(item.b_Z_DQZZDBGS) + Convert.ToDecimal(item.b_Z_DQZZKZXGS) + Convert.ToDecimal(item.b_Z_DQZZZHLGS) + Convert.ToDecimal(item.b_Z_DQZZMBZXGS) + Convert.ToDecimal(item.b_Z_DQZZMBJXGS));
                            hz_zgs = hz_zgs + Convert.ToDecimal(item.b_zgs);

                        }
                        Newmodel.Add(item);
                    }
                    string BOMJSON = JsonConvert.SerializeObject(Newmodel);
                    gxgshzmodel hzgxgs = new gxgshzmodel();
                    hzgxgs.Z_DQZZDBGS = hz_Z_DQZZDBGS.ToString();
                    hzgxgs.Z_DQZZKZXGS = hz_Z_DQZZKZXGS.ToString();
                    hzgxgs.Z_DQZZZHLGS = hz_Z_DQZZZHLGS.ToString();
                    hzgxgs.Z_DQZZMBZXGS = hz_Z_DQZZMBZXGS.ToString();
                    hzgxgs.Z_DQZZMBJXGS = hz_Z_DQZZMBJXGS.ToString();
                    hzgxgs.hz_gxgs = hz_zgs.ToString();
                    string hzjson = JsonConvert.SerializeObject(hzgxgs);
                    return Json(new { result= "success", BOMdata= BOMJSON ,HZGSdata= hzjson });


                }
                else
                {
                    return Json(new { result = "error", res = "产品物料不存在" });
                }
            }
            catch(Exception x)
            {
                return Json(new { result = "error", res = x });
            }
        }
        #endregion
        //继承BOM的实体，新增工序工时
        public class bcmodel:FInstaerMDBomA
        {
            /// <summary>
            /// 物料名称
            /// </summary>
            public virtual string ItmName { get; set; }

            /// <summary>
            /// 物料型号
            /// </summary>
            public virtual string ItmSpec { get; set; }
            public virtual string b_Z_DQZZDBGS { get; set; }

            public virtual string b_Z_DQZZKZXGS { get; set; }

            public virtual string b_Z_DQZZZHLGS { get; set; }

            public virtual string b_Z_DQZZMBZXGS { get; set; }

            public virtual string b_Z_DQZZMBJXGS { get; set; }

            public virtual string b_zgs { get; set; }
        }

        //各个工序汇总数据
        public class gxgshzmodel
        {
            public virtual string Z_DQZZDBGS { get; set; }

            public virtual string Z_DQZZKZXGS { get; set; }

            public virtual string Z_DQZZZHLGS { get; set; }

            public virtual string Z_DQZZMBZXGS { get; set; }

            public virtual string Z_DQZZMBJXGS { get; set; }

            public virtual string hz_gxgs { get; set; }
        }
        #endregion
        #endregion

        #region //批量更新 采购物料信息
        public JsonResult Batc_update_wuliaoinfo_bySourceID(string SourceID)
        {
            try
            {
                //查询物料平台
                IList<K3_wuliaoinfoView> list = _IK3_wuliaoinfoDao.Get_info_bySourceID(SourceID);
                var xz = 0;
                foreach (var item in list)
                {
                    string resjson = zypushsoftHelper.Get_MDItm_by_ItmID(item.fnumber);
                    if (resjson != null || resjson != null || resjson != "")
                    {
                        xz = xz + 1;
                        List<Psjsonmodel> timemodel = getObjectByJson<Psjsonmodel>(resjson);
                        foreach (var a in timemodel)
                        {
                            item.fname = a.ItmName;
                            item.fmodel = a.ItmSpec;
                            item.unitname = a.UomName;
                            item.WhsName = a.WhsName;
                            item.SourceID = a.SourceID;
                            item.IsClose = a.IsClose;
                            item.Z_WLSX = a.Z_WLSX;
                            item.updatetime = DateTime.Now;
                        }
                        _IK3_wuliaoinfoDao.NUpdate(item);//更新基础数据的名称

                        NQ_YJinfoView yqjmodel = _INQ_YJinfoDao.GetYqjModelbyW_dm(item.fnumber);
                        if (yqjmodel != null)
                        {
                            yqjmodel.Y_Name = item.fname;//产品名称
                            yqjmodel.Y_Ggxh = item.fmodel;//产品型号
                            _INQ_YJinfoDao.NUpdate(yqjmodel);
                        }
                        //查询元器件检验标准
                        IQC_SopInfoView IQCmodel = _IIQC_SopInfoDao.Getsopinfobyyqjwlno(item.fnumber);
                        if (IQCmodel != null)
                        {

                            IQCmodel.Yqjname = item.fname;//产品名称
                            IQCmodel.Yqjxh = item.fmodel;//产品型号
                            _IIQC_SopInfoDao.NUpdate(IQCmodel);
                        }
                        //查询常规电控温控产品
                        Flow_RoutineStockinfoView cgcpmodel = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(item.fnumber);
                        if (cgcpmodel != null)
                        {
                            cgcpmodel.P_Model = item.fmodel;//产品型号
                            cgcpmodel.P_Name = item.fname;//产品名称
                            _IFlow_RoutineStockinfoDao.NUpdate(cgcpmodel);
                        }
                        //查询非标电控温控产品
                        Flow_NonSProductinfoView fbcpmodel = _IFlow_NonSProductinfoDao.Getmodelbywldm(item.fnumber);
                        if (fbcpmodel != null)
                        {
                            fbcpmodel.Pname = item.fname;//产品名称
                            fbcpmodel.Pmodel = item.fmodel;//产品型号
                            _IFlow_NonSProductinfoDao.NUpdate(fbcpmodel);
                        }
                    }
                }
                return Json(new { result = "success", msg = "更新：" + xz + "条" });
            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion
    }

}
