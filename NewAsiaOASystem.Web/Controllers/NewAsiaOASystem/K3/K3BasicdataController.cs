﻿using System;
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
            string str;
            str = fnumber.Substring(3, 3);
            return str;
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



    }

}