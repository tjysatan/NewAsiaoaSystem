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
        INA_XSqitainfoDao _INA_XSqitainfoDao = ContextRegistry.GetContext().GetObject("NA_XSqitainfoDao") as INA_XSqitainfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        IFlow_ProductionNoticeinfoDao _IFlow_ProductionNoticeinfoDao = ContextRegistry.GetContext().GetObject("Flow_ProductionNoticeinfoDao") as IFlow_ProductionNoticeinfoDao;
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;

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
            //刷新订单
            updateDsorder(Id);
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

        #region //同步插入普实的数据


        #region //打开同步普实页面
        public ActionResult TBPsorderView(string Id)
        {
            //刷新订单的其他信息
            updatexs_qtinfo(Id);
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #region //查询销售订单的其他信息
        public string Getxsorderqtinfo(string dsorderId)
        {
            try
            {
                //查询
                NA_XSqitainfoView qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(dsorderId);
                if (qtmodel == null)
                {
                    //刷新
                    Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
                    DataTable dt = Dsmodel.GetOrderShipInfoByID(Convert.ToInt32(dsorderId));
                    string OrderId = dt.Rows[0]["OrderId"].ToString();//订单Id
                    string JHQX = dt.Rows[0]["JHQX"].ToString();//交货期限
                    string YFCD = dt.Rows[0]["YFCD"].ToString();//运费承担
                    string JSFS = dt.Rows[0]["JSFS"].ToString();//付款方式
                    string JHDD = dt.Rows[0]["JHDD"].ToString();//交货地点
                    string AddTime = dt.Rows[0]["AddTime"].ToString();//添加时间
                    string ISK3 = dt.Rows[0]["ISK3"].ToString();//
                    string YWRY = dt.Rows[0]["YWRY"].ToString();
                    string IsSend = dt.Rows[0]["IsSend"].ToString();
                    string cell = dt.Rows[0]["cell"].ToString();
                    string ordercode = dt.Rows[0]["ordercode"].ToString();
                    string Freight = dt.Rows[0]["Freight"].ToString();//运费
                    qtmodel = new NA_XSqitainfoView();
                    qtmodel.DsOrder_Id = OrderId;
                    qtmodel.Dsjhqx = JHQX;
                    qtmodel.Dsjhdd = JHDD;
                    qtmodel.Dsyfcd = YFCD;
                    qtmodel.DsJSFS = JSFS;
                    qtmodel.Dsaddtime = AddTime;
                    qtmodel.DsIsK3 = ISK3;
                    qtmodel.Dsywry = YWRY;
                    qtmodel.DsIsseed = IsSend;
                    qtmodel.Dscell = cell;
                    qtmodel.Dsordercode = ordercode;
                    qtmodel.Dsyf = Convert.ToDecimal(Freight);
                    qtmodel.C_Time = DateTime.Now;
                    _INA_XSqitainfoDao.Ninsert(qtmodel);
                }
                string json = JsonConvert.SerializeObject(qtmodel);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //选择其他相同客户的没有销售订单的数据的非标生产单
        public ActionResult TBPsSelectForderView(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            NACustomerinfoView khmodel = _INACustomerinfoDao.NGetModelById(model.KhId);
            ViewData["K3CODE"] = khmodel.K3CODE;
            //if (khmodel.K3CODE == "")
            //    return Json(new { result = "error", msg = "客户没有维护编号" });
            return View(model);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="Z_JHQX">交货期限</param>
        /// <param name="Z_JHDD">交货地点</param>
        /// <param name="Z_YSFS">运输方式</param>
        /// <returns></returns>
        public JsonResult Ps_Insterorder(string Id,string Z_JHQX,string Z_JHDD,string Z_YSFS,string yfprice,string dsprice)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.Ps_DocEntry == ""|| model.Ps_DocEntry==null)
                {
                    pushsoftorder Psordermodel = new pushsoftorder();
                    //客户编码k3code
                    NACustomerinfoView khmodel = _INACustomerinfoDao.NGetModelById(model.KhId);
                    if(khmodel.K3CODE=="")
                        return Json(new { result = "error", msg ="客户没有维护编号" });
                    Psordermodel.CrdID = khmodel.K3CODE;
                    Psordermodel.NumAtCrd = model.Sc_Id;
                    Psordermodel.DocDate =Convert.ToDateTime(Convert.ToDateTime(model.Xs_datetime).ToString("yyyy-MMM-dd"));
                    Psordermodel.Z_JHQX = Z_JHQX;
                    Psordermodel.Z_JHDD = Z_JHDD;
                    Psordermodel.Z_YSFS = Z_YSFS;
                    Psordermodel.DocKind = 0;
                    //查询订单明细
                    IList<NA_XSdetailsinfoView> mxmodellist = _INA_XSdetailsinfoDao.GetxsdetaillistbyxsId(model.Id);
                    List<pushsoftorderDetails> psmxlist = new List<pushsoftorderDetails>();
                    
                    foreach (var item in mxmodellist)
                    {
                        //查询产品
                        NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(item.cpId);
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = cpmodel.P_bianhao;
                        Psmxmodel.Qty = item.SL;
                        if (model.PaymentTypeName == "支付宝" || model.PaymentTypeName == "微信支付")
                        {
                            item.Je =Convert.ToDecimal(Convert.ToDouble(item.Je) * (1 - 0.006));
                        }
                        Psmxmodel.TaxAfPriceFC = item.Je / item.SL;
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    if (yfprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.002";
                        Psmxmodel.Qty = 1;
                        if (model.PaymentTypeName == "支付宝" || model.PaymentTypeName == "微信支付")
                        {
                            yfprice = (Convert.ToDouble(yfprice) * (1 - 0.006)).ToString();
                        }
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(yfprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    if (dsprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.005";
                        Psmxmodel.Qty = 1;
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(yfprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    Psordermodel.Details = psmxlist;

                    string res = pushsoftHelper.Insterorder(Psordermodel);
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "订单不存在" }); }

                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            model.Ps_DocEntry = errmodel.Data.DocEntry;
                            model.Ps_DocNu = errmodel.Data.DocNum;
                            _INA_XSinfoDao.NUpdate(model);
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
                    return Json(new { result = "error", msg = "订单已经同步,不能再次同步" });
                }

            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }

        #region //电商同步普实销售订单（可以选择管理非标生产的产品订单）
        public JsonResult Ps_InsterorderNew(string Id, string Z_JHQX, string Z_JHDD, string Z_YSFS, string yfprice, string dsprice)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.Ps_DocEntry == "" || model.Ps_DocEntry == null)
                {
                    pushsoftorder Psordermodel = new pushsoftorder();
                    //客户编码k3code
                    NACustomerinfoView khmodel = _INACustomerinfoDao.NGetModelById(model.KhId);
                    if (khmodel.K3CODE == "" || khmodel.K3CODE == null)
                    {
                        Newasia.XYOffer dsway = new Newasia.XYOffer();
                        DataTable dt = dsway.GetUserInfoByUserId(Convert.ToInt32(khmodel.DsUid));
                        string K3CODE = dt.Rows[0]["K3CODE"].ToString();// 
                        if (K3CODE != null)
                        {
                            khmodel.K3CODE = K3CODE;
                            _INACustomerinfoDao.NUpdate(khmodel);
                        }
                        else
                        {
                            return Json(new { result = "error", msg = "客户没有维护编号" });
                        }
                    }
                       
                    Psordermodel.CrdID = khmodel.K3CODE;
                    Psordermodel.NumAtCrd = model.Sc_Id;
                    //Psordermodel.DocDate = Convert.ToDateTime(Convert.ToDateTime(model.Xs_datetime).ToString("yyyy-MMM-dd"));
                    Psordermodel.DocDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MMM-dd"));//凭证日期
                    Psordermodel.Z_JHQX = Z_JHQX;
                    Psordermodel.Z_JHDD = Z_JHDD;
                    Psordermodel.Z_YSFS = Z_YSFS;
                    if (model.Xs_je == 0)
                    {
                        Psordermodel.DocKind = 1;
                    }
                    else
                    { 
                        Psordermodel.DocKind = 0;
                    }
                    if (Suser.ERP_YGNO != "" || Suser.ERP_YGNO != null)
                    {//制单人
                        Psordermodel.Z_OpUserSign = Suser.ERP_YGNO.ToString();
                    }
                    //主表备注
                    var ZBbeizhu = "";
                    //查询订单明细
                    IList<NA_XSdetailsinfoView> mxmodellist = _INA_XSdetailsinfoDao.GetxsdetaillistbyxsId(model.Id);
                    List<pushsoftorderDetails> psmxlist = new List<pushsoftorderDetails>();
                    bool Isfb = false;
                    foreach (var item in mxmodellist)
                    {
                        //查询产品
                        NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(item.cpId);
                        if (cpmodel.P_bianhao == "05.013.0001" || cpmodel.P_bianhao == "05.013.0002" || cpmodel.P_bianhao == "05.013.0003" || cpmodel.P_bianhao == "05.013.0004" || cpmodel.P_bianhao == "05.013.0005"
                            || cpmodel.P_bianhao == "05.013.0006" || cpmodel.P_bianhao == "05.013.0007" || cpmodel.P_bianhao == "05.013.0009" || cpmodel.P_bianhao == "05.013.NAW0008")
                        {
                            Isfb = true;//含有非标产品
                        }
                        else
                        {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = cpmodel.P_bianhao;
                        Psmxmodel.Qty = item.SL;
                        Psmxmodel.TaxAfPriceFC = item.Je / item.SL;
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        Psmxmodel.FreeTxt = item.beizhu;
                        psmxlist.Add(Psmxmodel);
                            if (item.beizhu != "" && item.beizhu != null)
                            {
                                ZBbeizhu = ZBbeizhu + item.beizhu;
                            }
                        }
                    }
                    Psordermodel.Notes = ZBbeizhu;//主表的备注
                    //查询非标关联明细
                    //查询订单明细
                    IList<DKX_DDinfoView> ddmmodellist = _IDKX_DDinfoDao.GetAllmxbyPs_orderno(model.Sc_Id);
                    if (ddmmodellist != null)
                    {//存在
                        if (Isfb == false)
                        {//销售订单中没有
                            return Json(new { result = "error", msg = "电商销售订单中不存在非标产品，请删除该订单关联的非标生产订单" });
                        }
                        else
                        {
                            foreach (var item in ddmmodellist)
                            {
                                pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                                if (item.Ps_orderDocEntry != "" && item.Ps_orderDocEntry != null)
                                    return Json(new { result = "error", msg = "该明细已经同步过销售订单：" + item.DD_Bianhao });
                                Psmxmodel.ItmID = item.Ps_wlBomNO;
                                if (item.Ps_wlBomNO == "" || item.Ps_wlBomNO == null)
                                    return Json(new { result = "error", msg = "没有维护产品物料编号：" + item.DD_Bianhao });
                                Psmxmodel.Qty = Convert.ToInt32(item.NUM);
                                if (item.price == 0 || item.price == null)
                                    return Json(new { result = "error", msg = "没有维护单价：" + item.DD_Bianhao });
                                Psmxmodel.TaxAfPriceFC = item.price;
                                Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                                psmxlist.Add(Psmxmodel);
                            }
                        }
                    }
                    else
                    {//没有查询到非标明细
                        if (Isfb == true)
                        {//电商订单中存在非标
                            return Json(new { result = "error", msg = "电商销售订单中存在非标产品，请选择的非标生产订单" });
                        }
                    }
                    if (yfprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.002";
                        Psmxmodel.Qty = 1;
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(yfprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    if (dsprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.005";
                        Psmxmodel.Qty = 1;
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(dsprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    Psordermodel.Details = psmxlist;
                    string res = pushsoftHelper.Insterorder(Psordermodel);
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "订单不存在" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            model.Ps_DocEntry = errmodel.Data.DocEntry;
                            model.Ps_DocNu = errmodel.Data.DocNum;
                            _INA_XSinfoDao.NUpdate(model);
                            if (ddmmodellist != null) { 
                                foreach (var item in ddmmodellist)
                            {
                                item.Ps_orderDocEntry = errmodel.Data.DocEntry;
                                    item.Ps_orderDocEntryNUM = errmodel.Data.DocNum;
                                _IDKX_DDinfoDao.NUpdate(item);
                            }
                            }
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
                    return Json(new { result = "error", msg = "订单已经同步,不能再次同步" });
                }
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }
        #endregion
        #endregion

        #region //刷新电商销售订单的其他信息
        public JsonResult updatexs_qtinfo(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if(model==null)
                return Json(new { result = "error", msg = "订单不存在" });
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            DataTable dt = Dsmodel.GetOrderShipInfoByID(model.Sort);
            string OrderId= dt.Rows[0]["OrderId"].ToString();//订单Id
            string JHQX = dt.Rows[0]["JHQX"].ToString();//交货期限
            string YFCD = dt.Rows[0]["YFCD"].ToString();//运费承担
            string JSFS = dt.Rows[0]["JSFS"].ToString();//付款方式
            string JHDD = dt.Rows[0]["JHDD"].ToString();//交货地点
            string AddTime = dt.Rows[0]["AddTime"].ToString();//添加时间
            string ISK3 = dt.Rows[0]["ISK3"].ToString();//
            string YWRY = dt.Rows[0]["YWRY"].ToString();
            string IsSend = dt.Rows[0]["IsSend"].ToString();
            string cell = dt.Rows[0]["cell"].ToString();
            string ordercode = dt.Rows[0]["ordercode"].ToString();
            string Freight = dt.Rows[0]["Freight"].ToString();//运费
            //查询
            NA_XSqitainfoView qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(model.Sort.ToString());
            if (qtmodel == null)
            {
                qtmodel = new NA_XSqitainfoView();
                qtmodel.DsOrder_Id = OrderId;
                qtmodel.Dsjhqx = JHQX;
                qtmodel.Dsjhdd = JHDD;
                qtmodel.Dsyfcd = YFCD;
                qtmodel.DsJSFS = JSFS;
                qtmodel.Dsaddtime = AddTime;
                qtmodel.DsIsK3 = ISK3;
                qtmodel.Dsywry = YWRY;
                qtmodel.DsIsseed = IsSend;
                qtmodel.Dscell = cell;
                qtmodel.Dsordercode = ordercode;
                qtmodel.Dsyf = Convert.ToDecimal(Freight);
                qtmodel.C_Time = DateTime.Now;
                _INA_XSqitainfoDao.Ninsert(qtmodel);
            }
            else
            {
                qtmodel.DsOrder_Id = OrderId;
                qtmodel.Dsjhqx = JHQX;
                qtmodel.Dsjhdd = JHDD;
                qtmodel.Dsyfcd = YFCD;
                qtmodel.DsJSFS = JSFS;
                qtmodel.Dsaddtime = AddTime;
                qtmodel.DsIsK3 = ISK3;
                qtmodel.Dsywry = YWRY;
                qtmodel.DsIsseed = IsSend;
                qtmodel.Dscell = cell;
                qtmodel.Dsordercode = ordercode;
                qtmodel.Dsyf = Convert.ToDecimal(Freight);
                qtmodel.C_Time = DateTime.Now;
                _INA_XSqitainfoDao.NUpdate(qtmodel);
            }

            return Json(new { result = "success", msg = "操作成功" });
        }
        #endregion

        #region //刷新电商销售订单
        public JsonResult updateDsorder(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                decimal? zje = 0;
                string fkfs = "";
                //删除订单明细
                List<NA_XSdetailsinfoView> oldmalist = _INA_XSdetailsinfoDao.GetxsdetaillistbyxsId(model.Id) as List<NA_XSdetailsinfoView>;
                _INA_XSdetailsinfoDao.NDelete(oldmalist);
                Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
                //string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
                DataTable dt = Dsmodel.GetOrderByOrderCode(model.Sc_Id);
                for (int a = 0, b = dt.Rows.Count; a < b; a++)
                {
                    string OrderCode = dt.Rows[a]["OrderCode"].ToString();//订单编号（唯一识别号）
                    string CreatedDate = dt.Rows[a]["CreatedDate"].ToString();//订单下单时间
                    string userid = dt.Rows[a]["userid"].ToString();//电商客户Id
                    string userName = dt.Rows[a]["userName"].ToString();//电商用户登录名称（客户登录名称）
                    string company = dt.Rows[a]["company"].ToString();//电商平台用户公司名称（客户公司名称）
                    string BuyerCellPhone = dt.Rows[a]["BuyerCellPhone"].ToString();//用户联系电话(客户电话)
                    string ShipRegion = dt.Rows[a]["ShipRegion"].ToString();//客户所属区域
                    string ShipAddress = dt.Rows[a]["ShipAddress"].ToString();//客户具体地址
                    string ShipName = dt.Rows[a]["ShipName"].ToString();//收货人名称
                    string ExpressCompanyName = dt.Rows[a]["ExpressCompanyName"].ToString();//公司名称
                    string PaymentTypeName = dt.Rows[a]["PaymentTypeName"].ToString();//支付方式
                    string SPName = dt.Rows[a]["Name"].ToString();//商品名称
                    string Description = dt.Rows[a]["Description"].ToString();//商品描述
                    string Quantity = dt.Rows[a]["Quantity"].ToString();//数量
                    string AdjustedPrice = dt.Rows[a]["AdjustedPrice"].ToString();//产品价格
                  
                    string OrderStatus = dt.Rows[a]["OrderStatus"].ToString();//订单状态
                    string ShippingStatus = dt.Rows[a]["ShippingStatus"].ToString();//货运状态
                    string PaymentStatus = dt.Rows[a]["PaymentStatus"].ToString();//支付状态
                    string Sort = dt.Rows[a]["OrderId"].ToString();//订单
                    string sku = dt.Rows[a]["SKU"].ToString();//产品物料码 （产品的唯一识别）
                    string ShipCellPhone = dt.Rows[a]["ShipCellPhone"].ToString();//收件人电话
                    string Haswlw = dt.Rows[a]["Haswlw"].ToString();//客户是否取得物联网电控箱的经销权
                    string Province = dt.Rows[a]["Province"].ToString();//省
                    string City = dt.Rows[a]["City"].ToString();//地级市
                    string Area = dt.Rows[a]["Area"].ToString();//区县
                    string beizhu = dt.Rows[a]["OrderRemark"].ToString();//备注
                    string ItemId = dt.Rows[a]["ItemId"].ToString();//电商明细排序字段
                    zje =zje + (Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice));
                    fkfs = PaymentTypeName;
                    NA_XSdetailsinfoView Ordermxmodel = new NA_XSdetailsinfoView();
                    Ordermxmodel.xsId = model.Id;//订单Id
                    Ordermxmodel.Je = Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice);//明细价格
                    Ordermxmodel.SL = Convert.ToInt32(Quantity);//产品数量
                    Ordermxmodel.cpbianmao = sku;
                    Ordermxmodel.beizhu = beizhu;
                    Ordermxmodel.ItemId = Convert.ToInt32(ItemId);
                    NQ_productinfoView SPmodel = new NQ_productinfoView();
                    SPmodel = _INQ_productinfoDao.GetProinfobyname(sku);//根据产品名称查询产品信息
                    if (SPmodel != null)
                    {
                        Ordermxmodel.cpId = SPmodel.Id;//产品
                    }
                    else
                    {
                        SPmodel = new NQ_productinfoView();
                        SPmodel.Pname = SPName;
                        SPmodel.P_bianhao = sku;
                        SPmodel.CreateTime = DateTime.Now;
                        string PRiD = _INQ_productinfoDao.InsertID(SPmodel);
                        Ordermxmodel.cpId = PRiD;

                    }
                    _INA_XSdetailsinfoDao.Ninsert(Ordermxmodel);

                }
                model.Xs_je = zje;
                model.PaymentTypeName = fkfs;
                _INA_XSinfoDao.NUpdate(model);
               return Json(new { result = "success", msg = "提交成功" });
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }
        #endregion

        #region //销售订单打印合同
        #region //销售订单合同
        public ActionResult XShetongView(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #region //查询合同的基础信息（或者订单的其他信息）
        public string GethetongBasicsinfo(string Id, string dsorderId)
        {
            try
            {
                //通过销售订单的ID查询合同的基础信息
                //查询
                NA_XSqitainfoView qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(Id);
                if (qtmodel == null)
                {
                    qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(dsorderId);
                }
                string json = JsonConvert.SerializeObject(qtmodel);
                return json;
            }
            catch
            {
                return null;
            }
        }
        //public 
        #endregion

        #region //合同基础条款设置
        public ActionResult HTBasicsinfoSetView(string Id)
        {
            NA_XSqitainfoView qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(Id);
            ViewData["orderId"] = Id;
            if (qtmodel == null)
                qtmodel = new NA_XSqitainfoView();
            return View(qtmodel);
        }

        #region //提交合同的基础条款的数据保存
        public JsonResult HTBasicsinfoSetEide(string orderId, string Dsjhqx, string Dsyfcd, string Dsjhdd, string DsJSFS)
        {
            try
            {
                NA_XSqitainfoView qtmodel = _INA_XSqitainfoDao.GetModelByDsOrder_Id(orderId);
                if (qtmodel == null)
                {
                    qtmodel = new NA_XSqitainfoView();
                    qtmodel.DsOrder_Id = orderId;
                }
                qtmodel.Dsjhqx = Dsjhqx;
                qtmodel.Dsyfcd = Dsyfcd;
                qtmodel.Dsjhdd = Dsjhdd;
                qtmodel.DsJSFS = DsJSFS;
                qtmodel.C_Time = DateTime.Now;
                bool flay = false;
                if (qtmodel.Id != "" || qtmodel.Id != null)
                    flay= _INA_XSqitainfoDao.NUpdate(qtmodel);
                else
                    flay=_INA_XSqitainfoDao.Ninsert(qtmodel);
                if(flay)
                    return Json(new { result = "success", msg = "保存成功" });
                else
                    return Json(new { result = "error", msg = "提交失败" });

            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = "提交失败" });
            }
        }
        #endregion
        #endregion
        #endregion

        #region //首页订单数据集
        public JsonResult DKX_DSOrder_Statistics_Interface()
        {
            try
            {
                //查询全年的
                int YY_Count = _INA_XSinfoDao.Get_OrderNumber_YORM("YY");
                //查询当月的
                int MM_Count = _INA_XSinfoDao.Get_OrderNumber_YORM("MM");
                DDDataMode model = new DDDataMode();
                model.TotalSum = YY_Count.ToString();
                model.TotaSameMonthSum = MM_Count.ToString();
                string json = JsonConvert.SerializeObject(model);
                return Json(new { result = "success", msg = "", data = model }); ;
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x }); ;
            }
        }
        #endregion
    }
}
 