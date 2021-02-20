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
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_YJinfoController : Controller
    {
        //元器件信息
        // GET: /NQ_YJinfo/
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
        INQ_GysInfoDao _INQ_GysInfoDao = ContextRegistry.GetContext().GetObject("NQ_GysInfoDao") as INQ_GysInfoDao;
        ICG_infoDao _ICG_infoDao = ContextRegistry.GetContext().GetObject("CG_infoDao") as ICG_infoDao;
        ICG_DetailinfoDao _ICG_DetailinfoDao = ContextRegistry.GetContext().GetObject("CG_DetailinfoDao") as ICG_DetailinfoDao;
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;

        INQ_OASupplierDao _INQ_OASupplierDao = ContextRegistry.GetContext().GetObject("NQ_OASupplierDao") as INQ_OASupplierDao;

        #region //数据列表页面

        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_YJinfoView> listmodel = GetListPager(pageIndex, null, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string yname = Request.Form["txtSearch_Yname"];
            string isbj = Request.Form["isbj"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_YJinfoView> listmodel = GetListPager(pageIndex, Name, isbj, yname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate }, "text/html");
            else
                return Json(new { result = "", PageN = PageNavigate }, "text/html");
        }
        #endregion
        public ActionResult SearchItemList()
        {
            string supplierId = Request.Form["supplierid"];
            string itemname = Request.Form["txtSearch_YName"];
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            }
            IList<NQ_YJinfoView> baglist = _INQ_YJinfoDao.GetItemsWithSupplier(supplierId) as IList<NQ_YJinfoView>;
            IList<NQ_YJinfoView> modellist = _INQ_YJinfoDao.GetItemsNotWithSupplier(supplierId) as IList<NQ_YJinfoView>;
            modellist = modellist.Where(w => w.Y_Name.Contains(itemname)).ToList();
            PagerInfo<NQ_YJinfoView> rtnlist = _INQ_YJinfoDao.setPagerInfo(modellist, pageIndex, 11);
            NQ_OASupplier suppliermodel = _INQ_OASupplierDao.GetSuById(supplierId);
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Suser.Id);
            IList<SysRole> RoleList = sysp.Role;
            ViewBag.isShow = 0;
            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null && Role.Name.Contains("品保"))
                    {
                        ViewBag.isShow = 1;
                    }
                }

            ViewBag.supplierid = supplierId;
            ViewBag.suppliername = suppliermodel.FName.ToString();
            ViewBag.itemsList = JsonConvert.SerializeObject(baglist);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(rtnlist.CurrentPageIndex, rtnlist.PageSize, rtnlist.RecordCount);
            if (rtnlist != null)
                return Json(new { result = rtnlist.GetPagingDataJson, PageN = PageNavigate }, "text/html");
            else
                return Json(new { result = "", PageN = PageNavigate }, "text/html");
        }

        #region //分页数据
        private PagerInfo<NQ_YJinfoView> GetListPager(int? pageIndex, string Name, string isbj, string Yname)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_YJinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_YJinfoDao.SetPagerPageSize(11);
            PagerInfo<NQ_YJinfoView> listmodel = _INQ_YJinfoDao.GetCinfoList(Name, Yname, isbj, Suser);
            return listmodel;
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_YJinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model.G_Dm = Request.Form["GYSListData"];
                if (model.G_Dm == null || model.G_Dm == "")
                    model.G_Dm = "0";
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Y_Dm = model.Y_Dm;//元器件代码
                    model.Y_Name = model.Y_Name;//元器件名称
                    model.Y_Ggxh = model.Y_Ggxh;//元器件规格

                    model.CreatePerson = Convert.ToString(user.UserName);
                    model.CreateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_YJinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Y_Dm = model.Y_Dm;//元器件代码
                    model.Y_Name = model.Y_Name;//元器件名称
                    model.Y_Ggxh = model.Y_Ggxh;//元器件规格
                    model.G_Dm = model.G_Dm;//供应商代码
                    model.UpdatePerson = model.UpdatePerson;
                    model.UpdateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_YJinfoDao.NUpdate(model);
                }

                if (flay)
                    return Json(new { result = "success" }, "text/html");
                else
                    return Json(new { result = "error" }, "text/html");
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }
        #endregion

        #region //增加跳转页面
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("Edit", new NQ_YJinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_YJinfoView sys = new NQ_YJinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_YJinfoDao.NGetModelById(id);
            AlbumDropdown(sys.G_Dm);
            return View("Edit", sys);
        }
        #endregion

        #region 初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedPID)
        {
            List<NQ_GysInfoView> ReasonlistView = _INQ_GysInfoDao.GetlistCust() as List<NQ_GysInfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (ReasonlistView != null)
            {
                foreach (var item in ReasonlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.G_Dm;
                    Reasonmodel.Name = item.G_Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //删除


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                //操作是否成功 
                string id = Request.QueryString["id"].ToString();
                List<NQ_YJinfoView> sys = _INQ_YJinfoDao.NGetList_id(id) as List<NQ_YJinfoView>;
                if (null != sys)
                {
                    if (_INQ_YJinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NQ_GysInfo/List");
        }
        #endregion

        #region //安全库存设置
        //安全库存设置页面
        public ActionResult AqkcView(string id)
        {
            NQ_YJinfoView sys = new NQ_YJinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_YJinfoDao.NGetModelById(id);
            return View("AqkcEidt", sys);
        }

        //安全库存设置保存
        [HttpPost]
        public JsonResult AqkcEidt(NQ_YJinfoView model, FrameController from)
        {

            bool flay = false;
            model.Y_aqkc = model.Y_aqkc;
            flay = _INQ_YJinfoDao.NUpdate(model);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }
        #endregion

        [HttpPost]
        public string Getyjinfobyname(string name)
        {
            string json=null;
            try
            {
                if (name!=""||name!=null) {
                json = JsonConvert.SerializeObject(_INQ_YJinfoDao.Getlistbyname(name));
                }
            }
            catch
            {
                
            }
            return json;
        }

        [HttpPost]
        public string Getyjinfo()
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_YJinfoDao.NGetList());
            return json;
        }

        [HttpPost]
        public string Getyjinfomodelbyid(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_YJinfoDao.NGetModelById(Id));
            return json;
        }

        [HttpPost]
        public JsonResult ajaxSelectedList(string supplierid, string txtSearch)
        {
            IList<NQ_YJinfoView> baglist = _INQ_YJinfoDao.GetItemsWithSupplier(supplierid) as IList<NQ_YJinfoView>;

            baglist = baglist.Where(w => w.Y_Name.Contains(txtSearch)).ToList();

            PagerInfo<NQ_YJinfoView> listmodel = _INQ_YJinfoDao.setPagerInfo(baglist, 1, 11);


            return Json(new { result = listmodel.GetPagingDataJson });
        }

        [HttpPost]
        public JsonResult ajaxAllList(string supplierid,string txtSearch)
        {
            IList<NQ_YJinfoView> modellist = _INQ_YJinfoDao.GetItemsNotWithSupplier(supplierid) as IList<NQ_YJinfoView>;

            modellist = modellist.Where(w => w.Y_Name.Contains(txtSearch)).ToList();

            PagerInfo<NQ_YJinfoView> listmodel = _INQ_YJinfoDao.setPagerInfo(modellist, 1, 11);

            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);

            return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
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

        /// <summary>
        /// 从System.Data.DataTable更新当前元器件库存数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void UpdateData(System.Data.DataTable dt)
        {
            string W_DM = "";//物料代码
            decimal kc = 0;//库存
            foreach (DataRow dr in dt.Rows)
            {
                W_DM = dr["物料长代码"].ToString().Trim();//获取元器件无聊代码
                if (dr["期末结存数量(常用)"].ToString().Trim() != "" && dr["期末结存数量(常用)"].ToString().Trim() != null)
                    kc = Convert.ToDecimal(dr["期末结存数量(常用)"].ToString().Trim());//获取当前库存
                else
                    kc = 0;
                NQ_YJinfoView model = new NQ_YJinfoView();
                model = _INQ_YJinfoDao.GetYqjModelbyW_dm(W_DM);
                if (model != null)
                {
                    model.Y_kc = kc;
                    _INQ_YJinfoDao.NUpdate(model);
                }
            }
        }

        //更新当前月的用量
        public void UpdateYLDate(System.Data.DataTable dt)
        {
            string w_dm = "";//物料代码
            decimal dqyl = 0;//当前用量
            foreach (DataRow dr in dt.Rows)
            {
                w_dm = dr["物料长代码"].ToString().Trim();//获取元器件无聊代码
                if (dr["数量(基本)"].ToString().Trim() != "" && dr["数量(基本)"].ToString().Trim() != null)
                    dqyl = Convert.ToDecimal(dr["数量(基本)"].ToString().Trim());
                else
                    dqyl = 0;
                NQ_YJinfoView model = new NQ_YJinfoView();
                model = _INQ_YJinfoDao.GetYqjModelbyW_dm(w_dm);
                if (model != null)
                {
                    model.Y_DQYL = dqyl;//本月当前用量
                    model.Y_DQDJZ = DateTime.Now.Day / 7 + ((DateTime.Now.Day % 7 == 0) ? 0 : 1);//本月第几周
                    model.Y_CYZ = model.Y_kc - model.Y_aqkc;//差异值
                    model.Y_iscg = JCIscg(model.Y_aqkc, model.Y_kc);//是否需要采购
                    model.Y_cgSL = JScgsl(model.Y_kc, dqyl, model.Y_DQDJZ);//采购
                    model.Y_jcdatetime = DateTime.Now;//库存检测时间
                    _INQ_YJinfoDao.NUpdate(model);
                }


            }
        }

        /// <summary>
        /// 检测是否需要采购
        /// </summary>
        /// <param name="aqkc">安全库存</param>
        /// <param name="dqkc">当前库存</param>
        /// <returns></returns>
        public int JCIscg(decimal aqkc, decimal dqkc)
        {
            int Iscg = 0;//是否采购
            if (aqkc != 0)
            {
                if (dqkc <= aqkc / 4)
                {
                    Iscg = 1;//当前库存小雨安全库存四分之一
                }
            }
            return Iscg;
        }

        /// <summary>
        /// 计算采购数量
        /// </summary>
        /// <param name="aqkc">安全库存</param>
        /// <param name="dqkc">当前库存</param>
        /// <param name="dqyl">本月当前用量</param>
        /// <param name="dqdjz">当前的几周</param>
        /// <returns></returns>
        public decimal JScgsl(decimal dqkc, decimal dqyl, int dqdjz)
        {
            decimal cgl = 0;//采购量
            decimal zjyl = 0;//当月周均用量
            int gjz = 0;//总过几周的用量
            zjyl = dqyl / dqdjz;
            gjz = 4 - dqdjz + 4;
            cgl = zjyl * gjz - dqkc;//当月周均用量乘以 采购几周用量 减去当前库存用量
            return cgl;
        }


        //导入库存数据Excel
        public ActionResult DRkcview()
        {
            return View();
        }

        #region //保存导入当前库存的方法
        [HttpPost]
        public JsonResult DRKCExcel(HttpPostedFileBase fileImport)
        {
            try
            {
                if (fileImport != null)
                {

                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                    fileImport.SaveAs(filePath);

                    string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                    System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                    UpdateData(dt);
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================以下为导出错误提示===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("123" + e);
            }
            return Json(new { result = "error1" });
        }
        #endregion

        [HttpPost]
        public JsonResult DRDQylExcel(HttpPostedFileBase filImg)
        {
            if (filImg != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(filImg.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                filImg.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                UpdateYLDate(dt);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        #region //采购订单 下单
        [HttpPost]
        public JsonResult Cgddxd(string Gysdm)
        {
            CG_infoView cginfomodel = new CG_infoView();
            NQ_GysInfoView gysmode = new NQ_GysInfoView();//供应商
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            gysmode = _INQ_GysInfoDao.Getmodelbydm(Gysdm);//根据供应商代码查找供应商代码
            string cgId = null;//采购单Id
            if (gysmode != null)
            {
                cginfomodel.GysId = gysmode.Id;//供应商代码
                cginfomodel.GysDm = gysmode.G_Dm;//供应商代码
                cginfomodel.Cg_xdtime = DateTime.Now;//下单时间
                cginfomodel.Cg_xdname = user.Id;//下单人
                cgId = _ICG_infoDao.InsertID(cginfomodel);
            }
            else
            {
                return Json(new { result = "error1" });//查不到供应商
            }
            IList<NQ_YJinfoView> yqjlist = new List<NQ_YJinfoView>();
            yqjlist = _INQ_YJinfoDao.Getlistbygysdm(Gysdm);//根据公因式
            if (yqjlist != null)
            {
                for (int i = 0, j = yqjlist.Count; i < j; i++)
                {
                    CG_DetailinfoView cgxqinfomodel = new CG_DetailinfoView();//采购单详情
                    cgxqinfomodel.cgId = cgId;//采购ID
                    cgxqinfomodel.Cgsl = yqjlist[i].Y_cgSL;//采购数量
                    cgxqinfomodel.YqjId = yqjlist[i].Id;//元器件ID
                    cgxqinfomodel.GysId = gysmode.Id;//供应商ID
                    _ICG_DetailinfoDao.Ninsert(cgxqinfomodel);
                    yqjlist[i].Y_cgzt = 1;//采购状态
                    _INQ_YJinfoDao.NUpdate(yqjlist[i]);//更新采购状态
                }
                return Json(new { result = "success" });//下单成功
            }
            else
            {
                return Json(new { result = "error1" });//查不到元器件
            }


        }
        #endregion


        //根据供应商
        [HttpPost]
        public string Getgysmodeljson(string Name)
        {
            string Json;
            Json = JsonConvert.SerializeObject(_INQ_GysInfoDao.Getmodelbygysname(Name));
            return Json;
        }



        #region //同步K3中元器件的数据
        public string UpdateK3DATAYJ()
        {
            //查询电子原料 电器原料 辅料
            IList<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.Getwuliaobytype("0,1,2");
            int addSUM = 0;//新增数量
            if (listmodel != null)
            {
                foreach (var item in listmodel)
                {
                    NQ_YJinfoView model = new NQ_YJinfoView();
                    model = _INQ_YJinfoDao.GetYqjModelbyW_dm(item.fnumber);
                    if (model == null)//不存在-插入
                    {
                        addSUM = addSUM + 1;
                        model = new NQ_YJinfoView();
                        model.Y_Name = item.fname;//元器件名称
                        model.Y_Ggxh = item.fmodel;//型号
                        model.Y_Dm = item.fnumber;//物料代码
                        model.Status = 1;
                        model.Sort = 0;
                        model.CreateTime = DateTime.Now;
                        _INQ_YJinfoDao.Ninsert(model);
                    }
                }

            }
            return addSUM.ToString();
        }
        #endregion

        public ActionResult supplierAndItemList(string supplierId)
        {
            int CurrentPageIndex = 0;
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            IList<NQ_YJinfoView> baglist = _INQ_YJinfoDao.GetItemsWithSupplier(supplierId) as IList<NQ_YJinfoView>;
            IList<NQ_YJinfoView> modellist = _INQ_YJinfoDao.GetItemsNotWithSupplier(supplierId) as IList<NQ_YJinfoView>;
            PagerInfo<NQ_YJinfoView> rtnlist = _INQ_YJinfoDao.setPagerInfo(modellist, CurrentPageIndex, 11);

            NQ_OASupplier suppliermodel = _INQ_OASupplierDao.GetSuById(supplierId);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Suser.Id);
            IList<SysRole> RoleList = sysp.Role;

            ViewBag.isShowTab = 0;

            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null && Role.Name.Contains("品保"))
                    {
                        ViewBag.isShowTab = 1;
                    }
                }

            ViewBag.supplierid = supplierId;
            ViewBag.suppliername = suppliermodel.FName.ToString();

            ViewBag.itemsList = JsonConvert.SerializeObject(baglist);

            return View("BaseitemlistOfSupplier", rtnlist);
        }

        public ActionResult supplierAndItemListCheck(string supplierId)
        {
            int CurrentPageIndex = 0;
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;

            IList<NQ_YJinfoView> baglist = _INQ_YJinfoDao.GetItemsWithSupplier(supplierId) as IList<NQ_YJinfoView>;
            //IList<NQ_YJinfoView> modellist = _INQ_YJinfoDao.GetItemsNotWithSupplier(supplierId) as IList<NQ_YJinfoView>;

            PagerInfo<NQ_YJinfoView> rtnlist = _INQ_YJinfoDao.setPagerInfo(baglist, CurrentPageIndex, 11);

            NQ_OASupplier suppliermodel = _INQ_OASupplierDao.GetSuById(supplierId);

            SessionUser Suser = Session[SessionHelper.User] as SessionUser;

            ViewBag.isShowTab = 0;

            ViewBag.supplierid = supplierId;
            ViewBag.suppliername = suppliermodel.FName.ToString();

            ViewBag.itemsList = JsonConvert.SerializeObject(baglist);

            return View("BaseitemlistOfSupplierCheck", rtnlist);
        }


        public ActionResult addItemToSupplier(string supplierid, string itemid, string txtSearch)
        {
            try
            {
                bool flay = false;

                SessionUser user = Session[SessionHelper.User] as SessionUser;
                flay = _INQ_OASupplierDao.saveOrUpdateSupplier(supplierid, itemid);
                NAHelper.Insertczjl(supplierid.ToString() + "," + itemid.ToString(), "增加关联元器件", user.Id);

                if (flay)
                {
                    //return View("../NQ_YJinfo/BaseitemlistOfSupplier");
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

        public ActionResult deleteItemFromSupplier(string supplierid, string itemid)
        {
            try
            {
                bool flay = false;

                SessionUser user = Session[SessionHelper.User] as SessionUser;

                flay = _INQ_OASupplierDao.deleteItemSupplier(supplierid, itemid);
                NAHelper.Insertczjl(supplierid.ToString() + "," +itemid.ToString(), "删除关联元器件", user.Id);

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

        public JsonResult notSelectedItems()
        {
            string Name = Request.Form["txtSearch_Name"];
            string yname = Request.Form["txtSearch_Yname"];
            string isbj = Request.Form["isbj"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_YJinfoView> listmodel = GetListPager(pageIndex, Name, isbj, yname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

    }
}
