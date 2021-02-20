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
using System.Xml.Linq;
using System.Net;
using System.Data.OleDb;
using System.Data;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NA_XSinfoController : Controller
    {
        //
        // GET: /NA_XSinfo/
        INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
        INA_XSdetailsinfoDao _INA_XSdetailsinfoDao = ContextRegistry.GetContext().GetObject("NA_XSdetailsinfoDao") as INA_XSdetailsinfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        IFlow_ProductionNoticeinfoDao _IFlow_ProductionNoticeinfoDao = ContextRegistry.GetContext().GetObject("Flow_ProductionNoticeinfoDao") as IFlow_ProductionNoticeinfoDao;


        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NA_XSinfoView> listmodel = GetListPager(pageIndex, null,null,null);
            return View(listmodel);
        }
        #endregion

        #region //查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string Issm = Request.Form["Issm"];
            string sm_zt = Request.Form["sm_zt"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NA_XSinfoView> listmodel = GetListPager(pageIndex, Name, Issm, sm_zt);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取分页数据
        private PagerInfo<NA_XSinfoView> GetListPager(int? pageIndex, string Name,string Issm,string sm_zt)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INA_XSinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INA_XSinfoDao.SetPagerPageSize(11);
            PagerInfo<NA_XSinfoView> listmodel = _INA_XSinfoDao.GetXSinfoList(Name,Issm,sm_zt, Suser);
            return listmodel;
        }
        #endregion

        #region //销售开单 根据客户开单的客户列表
        public ActionResult Xskdlist(int? pageIndex)
        {
            PagerInfo<NACustomerinfoView> listmodel = GetCustinfo(pageIndex, null, null);
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
        private PagerInfo<NACustomerinfoView> GetCustinfo(int? pageIndex, string Name, string lxrname)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(11);
            PagerInfo<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetCinfoList(Name, lxrname,null,null,null,null, Suser);
            return listmodel;
        }
        #endregion

        //客户信息 条件查询
        public JsonResult ckSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//单位名称
            string lxrName = Request.Form["txtSearch_LXname"];//联系人姓名
           
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NACustomerinfoView> listmodel = GetCustinfo(pageIndex, Name, lxrName);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        //销售开单 保存销售订单并且跳转到 编辑销售产品明细页面
        public ActionResult XSkdEit(string C_Id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            NA_XSinfoView model = new NA_XSinfoView();
            model.KhId = C_Id;//客户Id
            model.Khname = C_Id;//客户Id
            model.Xs_datetime = DateTime.Now;//销售时间
            model.Xs_type = 0;//传统销售
            model.Ddzt = 0;//未付款
            model.C_datetime = DateTime.Now;//创建时间
            model.C_Name = user.Id;//创建人
            model.States = 0;//启用
            model.Sort = 0;//排序
            string XS_Id = _INA_XSinfoDao.InsertID(model);
            ViewData["xs_Id"] = XS_Id;
            ViewData["C_Id"] = C_Id;
            return View("XSkdView", ViewData["xs_Id"]);
        }

        // 销售明细页面
        public ActionResult XSkdView(string Id)
        {
            ViewData["XS_Id"] = Id;
            return View("XSkdView");
        }

        #region //根据销售Id 查找销售订单的详情
        /// <summary>
        /// 查询销售订单 json
        /// </summary>
        /// <param name="Id">销售订单ID</param>
        /// <returns></returns>
        public string GetXsinfojson(string Id)
        {
            string json = "{\"status\":\"true\"}";
            NA_XSinfoView model = new NA_XSinfoView();
            model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                json = JsonConvert.SerializeObject(_INA_XSinfoDao.NGetModelById(Id));
            }
            return json;
        } 
        #endregion

        #region //根据销售Id查找销售明细信息
        public string GetXsdetailjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_XSdetailsinfoDao.GetxsdetaillistbyxsId(Id));
            return json;
        } 
        #endregion

        #region //根据销售明细Id  查找该明细信息
        public string GetXsdetailbyIdjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_XSdetailsinfoDao.NGetModelById(Id));
            return json;
        } 
        #endregion

        #region //根据客户ID 查找收件人信息
        public string GetaddresseeInfobyCustId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_AddresseeInfoDao.NGetModelById(Id));
            return json;
        } 
        #endregion

        [HttpPost]
        public JsonResult XsdetailEdit(string cpID, string SL, string Xs_Id,string dj)
        {
            try
            {
                bool flay = false;
                NA_XSdetailsinfoView model = new NA_XSdetailsinfoView();
                model.cpId = cpID;//产品Id
                model.SL = Convert.ToInt32(SL);//数量
                model.Je = Convert.ToInt32(SL) * Convert.ToDecimal(dj);//金额
                model.xsId = Xs_Id;//销售订单Id
                model.C_datetime = DateTime.Now;//创建时间
                NA_XSinfoView xsmodel = _INA_XSinfoDao.NGetModelById(Xs_Id);
                xsmodel.Xs_je =Convert.ToDecimal(xsmodel.Xs_je) + Convert.ToDecimal(model.Je);//订单总金额
                _INA_XSinfoDao.NUpdate(xsmodel);
                flay = _INA_XSdetailsinfoDao.Ninsert(model);
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

        #region //删除 销售产品明细
        [HttpPost]
        public JsonResult Delete(string Id)
        {
            NA_XSdetailsinfoView model = new NA_XSdetailsinfoView();
            if (!string.IsNullOrEmpty(Id))
                model = _INA_XSdetailsinfoDao.NGetModelById(Id);

            bool flay;
            flay = _INA_XSdetailsinfoDao.NDelete(model);
            NA_XSinfoView xsmodel = _INA_XSinfoDao.NGetModelById(model.xsId);
            xsmodel.Xs_je = xsmodel.Xs_je - model.Je;//订单总金额
            _INA_XSinfoDao.NUpdate(xsmodel);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });

        }
        #endregion

        #region //销售订单删除
        public ActionResult xsDelete(string Id)
        {
            try
            {
                List<NA_XSinfoView> sys = _INA_XSinfoDao.NGetList_id(Id) as List<NA_XSinfoView>;
                foreach (var item in sys)
                {
                    item.States = 1;
                    _INA_XSinfoDao.NUpdate(item);
                }
                return RedirectToAction("List");
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        } 
        #endregion

        #region //订单明细查看
        public ActionResult OrdermxView(string Id)
        {
            ViewData["OrderId"] = Id;//保存订单Id
            return View();
        } 
        #endregion

        #region //扫码出货 扫描页面
        public ActionResult ChSmSID(string Id, string khId,string mxId)
        {
            ViewData["OrderId"] = Id;//保存订单Id
            ViewData["khId"] = khId;//客户Id
            ViewData["mxId"] = mxId;//明细ID
            return View();
        } 
        #endregion

        #region //
        [HttpPost]
        public string GetsmCount(string ordermxId)
        {
            int smcount = _IWL_DkxInfoDao.GetChcunotbyorderId(ordermxId);
            return "{\"status\":\""+smcount +"\"}";//添加成功
        } 
        #endregion

        #region //扫码 更加订单Id 改变订单扫码状态
        [HttpPost]
        public string Updateordersmzt(string orderId)
        {
            NA_XSinfoView xsmodel = new NA_XSinfoView();
            xsmodel = _INA_XSinfoDao.NGetModelById(orderId);
            xsmodel.SM_ZT = 1;//已经扫码
            if (_INA_XSinfoDao.NUpdate(xsmodel))
            {
                return "{\"status\":\"true\"}";//添加成功
            }
            else
            {
                return "{\"status\":\"fales\"}";//添加成功
            }
        } 
        #endregion

        #region //
        public ViewResult EwmOrderch(string Id)
        {
            ViewData["EwmOrderId"] = Id;//保存订单Id
            return View();
        } 
        #endregion

        #region //快递打印页面
        /// <summary>
        /// 中通快递单打印
        /// </summary>
        /// <returns></returns>
        public ActionResult ztPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = id;//客户信息ID
            return View();
        }

        /// <summary>
        /// 德邦快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult dbPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;
            return View();
        }

        /// <summary>
        /// 顺丰快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult sfPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 顺丰快递（新版）打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult sfxPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 盛辉快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult shPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 天地华宇
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult tdhyPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        } 
        #endregion

        #region //扫描查询订单
        public ViewResult SmSelectOrder()
        {
            return View();
        } 
        #endregion

        #region //扫码发货订单详情
        public ViewResult SmOrderinfoView(string Id)
        {
            ViewData["SMOrderId"] = Id;//保存订单Id
            return View("SmOrderinfoView");
        } 
        #endregion

        #region //查看订单的出货的SID码
        public ViewResult SOrderSID(string Id)
        {
            ViewData["OrderId"] = Id;//保存订单
            return View("SOrderSID");
        } 
        #endregion


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

        #region //无线扫描数据 出货
        public void ADDData(System.Data.DataTable dt)
        {
            string datastr = "";//扫码
            NA_XSinfoView xsmodel = new NA_XSinfoView();//销售订单实例化
            WL_DkxInfoView modelnew = new WL_DkxInfoView();
            foreach (DataRow dr in dt.Rows)
            {
                datastr = dr["扫码"].ToString().Trim();//码值
                if (!Issid(datastr))
                { //不是SID
                    if (datastr != "0" && datastr != "1" && datastr != "2")
                    { //不是订单状态    0 未出货  1 完成  2 未完成
                        xsmodel = new NA_XSinfoView();
                        xsmodel = _INA_XSinfoDao.NGetModelById(datastr);
                    }
                    else
                    {
                        xsmodel.SM_ZT = Convert.ToInt32(datastr);
                        _INA_XSinfoDao.NUpdate(xsmodel);
                    }
                }
                else
                {//是sid
                    modelnew = new WL_DkxInfoView();
                    modelnew = _IWL_DkxInfoDao.GetDkxinfo(Getsid(datastr));//通过sid 查找电控箱信息
                    if (modelnew != null)
                    {//检测该数据是否存在
                        if (modelnew.WL_zt == 0)
                        {
                            modelnew.Xs_jxsId = xsmodel.KhId;//客户Id(经销商Id)
                            modelnew.Xs_datetime = DateTime.Now;//销售时间
                            modelnew.WL_zt = 1;//已销售
                            modelnew.OrdermxId = xsmodel.Id;//订单Id
                            _IWL_DkxInfoDao.NUpdate(modelnew);
                        }
                    }
                }
            }
        } 
        #endregion

        [HttpPost]
        public JsonResult DRSMDATAExcel(HttpPostedFileBase filImg)
        {
            if (filImg != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(filImg.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                filImg.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                ADDData(dt);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        //判定是否是sid码
        public bool Issid(string str) 
        {
            if (str != null && str != ""&&str.Length>4)
            {
                str = str.Substring(0,4);
                if (str != "SID:")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //导入无线扫码数据 页面
        public ActionResult DRSMDATAview()
        {
            return View();
        }

        #region //截取sid
        public string Getsid(string a)
        {
            string s = a.Replace(" ", "").ToUpper();
            int i = s.IndexOf("D:") + 2;
            int j = s.IndexOf(".KEY");
            string str = s.Substring(i, j - i);
            return str;
        }
        #endregion

        //检测产品是否有库存,没有产生生产通知单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Product_Id">产品Id</param>
        /// <param name="Xsorder_Id">订单Id</param>
        /// <returns></returns>
        public string testingstock(string Product_Id, string Xsorder_Id) 
        {
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            //查询产品信息
            NQ_productinfoView Pmodel = _INQ_productinfoDao.NGetModelById(Product_Id);
            string Wldm ="'"+ Pmodel.P_bianhao+"'";//物料代码
            string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
            DataTable dt = Dsmodel.GetKuCun(Wldm,Key);
            Flow_ProductionNoticeinfoView PNModel = new Flow_ProductionNoticeinfoView();
            PNModel.Product_Id = Product_Id;//产品Id
            PNModel.Xsorder_Id = Xsorder_Id;//订单Id
            PNModel.Number = 2;//数量
            PNModel.Status = 0;//记录状态
            PNModel.Type = 0;//处理状态
            PNModel.Createtime = DateTime.Now;//创建时间
            _IFlow_ProductionNoticeinfoDao.Ninsert(PNModel);
            IList<Flow_ProductionNoticeinfoView> PNlist = _IFlow_ProductionNoticeinfoDao.NGetList();
            return "";
        }

        #region //修改订单的扫码数量
        public ActionResult UpdateordersmsumView(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            return View(model);
        }

        //扫码数量修改提交
        public string EideUpdateordersmsun(string Id, string sum)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.S_Number = Convert.ToDecimal(sum);
                bool flay = false;
               flay= _INA_XSinfoDao.NUpdate(model);
               if (flay)
                   return "0";//修改成功
               else
                   return "1";//修改失败

            }
            else
            {
                return "2";//为空
            }
        }
        #endregion

        #region //收件人区域编辑
        public ActionResult resinfoView(string addId)
        {
            NA_AddresseeInfoView model = _INA_AddresseeInfoDao.NGetModelById(addId);
            return View(model);
        }

        //编辑区域信息
        [HttpPost]
        public JsonResult resinfoEide(string Id, string qyo, string qye, string qyt)
        {
            try
            {
                NA_AddresseeInfoView model = _INA_AddresseeInfoDao.NGetModelById(Id);
                model.qyo = qyo;
                model.qye = qye;
                model.qyt = qyt;
                if (_INA_AddresseeInfoDao.NUpdate(model))
                {
                    return Json(new { result = "success", res = "保存成功" });
                }
                else
                {
                    return Json(new { result = "error", res = "提交失败" });
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "error", res = "提交异常" });
            }
        }
        #endregion
    }
}
