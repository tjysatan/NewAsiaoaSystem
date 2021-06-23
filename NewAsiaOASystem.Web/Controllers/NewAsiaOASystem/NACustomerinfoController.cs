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
using System.Data;
using System.Data.OleDb;

namespace NewAsiaOASystem.Web.Controllers
{
    /// <summary>
    /// 客户信息管理
    /// tjy_satan
    /// </summary>
    public class NACustomerinfoController : Controller
    {
        //
        // GET: /NACustomerinfo/
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;

        #region //获取数据列表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? pageIndex)
        {
            if (Session["pageIndex"]==null)
            {
                if (pageIndex == null)
                {
                    Session["pageIndex"] = 0;
                }
                else
                {
                    Session["pageIndex"] = pageIndex;
                }
                
                pageIndex = Convert.ToInt32(Session["pageIndex"]);
            }
            if (Session["pageIndex"] != null)
            {
                if (Session["pageIndex"].ToString() != pageIndex.ToString())
                {
                    if (pageIndex == null)
                    {
                        pageIndex = Convert.ToInt32(Session["pageIndex"]);
                    }
                    else
                    {
                        Session["pageIndex"] = pageIndex;
                    }
                }
               // pageIndex = Convert.ToInt32(Session["pageIndex"]);
            }
            
            PagerInfo<NACustomerinfoView> listmodel = new PagerInfo<NACustomerinfoView>();
            if (Session[SessionHelper.NACusSerch] != null)
            {
                
                  //listmodel = GetListPager(pageIndex, null, null, null, "1", null, null);
                NACuscxView ssearch = Session[SessionHelper.NACusSerch] as NACuscxView;
                ViewData["name"] = ssearch.Name;
                ViewData["lxrname"] = ssearch.lxrname;
                ViewData["tel"] = ssearch.tel;
                ViewData["jxstype"] = ssearch.jxstype;
                ViewData["isly"] = ssearch.Isly;
                ViewData["isqy"] = ssearch.Isqy;

                listmodel = GetListPager(pageIndex, ssearch.Name, ssearch.lxrname, ssearch.jxstype, ssearch.Isqy, ssearch.tel, ssearch.Isly);
                //DKXDDCXTJView ssearch = Session[SessionHelper.DKXSerch] as DKXDDCXTJView;
                //ViewData["DD_Bianhao"] = ssearch.DD_Bianhao;
                //ViewData["BJno"] = ssearch.BJno;
                //ViewData["DD_Type"] = ssearch.DD_Type;
                //ViewData["KHname"] = ssearch.KHname;
                //ViewData["Lxname"] = ssearch.Lxname;
                //ViewData["Tel"] = ssearch.Tel;
                //ViewData["DD_ZT"] = ssearch.DD_ZT;
                //ViewData["startctime"] = ssearch.startctime;
                //ViewData["endctiome"] = ssearch.endctiome;
                //ViewData["DHtype"] = ssearch.DHtype;
            }
            else
            {
                listmodel = GetListPager(pageIndex, null, null, null, "1", null, null);
            }
            return View(listmodel);
        } 

          //条件查询
        public JsonResult SearchList(int? pageIndex) 
        {

            //Session[SessionHelper.DKXSerch] = null;
            //Session.Remove(SessionHelper.DKXSerch);
            //DKXDDCXTJView view = new DKXDDCXTJView();
            Session[SessionHelper.NACusSerch]=null;
            Session.Remove(SessionHelper.NACusSerch);
            NACuscxView view = new NACuscxView();

            //string Name = Request.Form["txtSearch_Name"];
            //string lxname = Request.Form["txtSearch_lxrName"];
            //string wljxs = Request.Form["wljxs"];//经销商类别
            //string Isjy = Request.Form["Isjy"];//是否禁用
            //string Tel=Request.Form["Tel"];//电话
            //string Isds=Request.Form["Isds"];//是否电商
            view.Name = Request.Form["txtSearch_Name"];
            view.lxrname = Request.Form["txtSearch_lxrName"];
            view.tel = Request.Form["Tel"];//电话
            view.Isly = Request.Form["Isds"];//是否电商
            view.jxstype = Request.Form["wljxs"];//经销商类别
            view.Isqy = Request.Form["Isds"];//是否电商
            if (Session["pageIndex"]!=null)
            {
                if (Session["pageIndex"].ToString() != pageIndex.ToString())
                {
                    Session["pageIndex"] = pageIndex;
                }
            }
           
            Session[SessionHelper.NACusSerch] = view;
            pageIndex = 0;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NACustomerinfoView> listmodel = GetListPager(pageIndex, view.Name, view.lxrname, view.jxstype, view.Isqy, view.tel, view.Isly);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
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
                List<NACustomerinfoView> sys = _INACustomerinfoDao.NGetList_id(id) as List<NACustomerinfoView>;
                if (null != sys)
                {
                    foreach (var item in sys)
                    {
                        item.Status = 0;
                        _INACustomerinfoDao.NUpdate(item);
                    }
                    return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NACustomerinfo/List");
        } 
        #endregion

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NACustomerinfoView> GetListPager(int? pageIndex, string Name, string lxrname, string wlisjx, string Isjy,string Tel,string Isds)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(11);
            PagerInfo<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetCinfoList(Name, lxrname, wlisjx,Isjy,Tel,Isds, Suser);
            return listmodel;
        } 
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NACustomerinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;
                    model.LxrName = model.LxrName;
                    model.Tel = model.Tel;
                    model.Adress = model.Adress;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    model.qyId = Request.Form["SelectedCLData"];//区域1
                    model.qyCId = Request.Form["SelectedZTData"];//区域2
                    model.isjxs = model.isjxs;//是否是经销商 0 不是 1 是
                    model.CreatePerson = Convert.ToString(user.UserName);
                    model.CreateTime = DateTime.Now;
                    model.Isds = 0;//不是电商
                    flay = _INACustomerinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;
                    model.LxrName = model.LxrName;
                    model.Tel = model.Tel;
                    model.Adress = model.Adress;
                    model.UpdatePerson = model.UpdatePerson;
                    model.UpdateTime = DateTime.Now;
                    model.qyId = Request.Form["SelectedCLData"];//区域1
                    model.qyCId = Request.Form["SelectedZTData"];//区域2
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INACustomerinfoDao.NUpdate(model);
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

        #region //增加跳转页面
        public ActionResult addPage()
        {
            return View("Edit", new NACustomerinfoView());
        } 
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NACustomerinfoView sys = new NACustomerinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INACustomerinfoDao.NGetModelById(id);
            return View("Edit", sys);
        } 
        #endregion

        #region //获取联系人信息
        //获取联系人
        public string GetaddressssInfobycusId(string cusId)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_AddresseeInfoDao.GetAddresseeinfobycustId(cusId));
            return json;
        }
        #endregion

        #region //取得物联网经销权的经销商列表

        public ActionResult jxqlist(int? pageIndex)
        {
            PagerInfo<NACustomerinfoView> listmodel = wljxsGetListPager(pageIndex,null,null);
            return View(listmodel);
        }
        
        //
        public JsonResult jxqSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string lxname = Request.Form["txtSearch_lxrName"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NACustomerinfoView> listmodel = wljxsGetListPager(pageIndex, Name, lxname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #region //获取数据列表
        private PagerInfo<NACustomerinfoView> wljxsGetListPager(int? pageIndex, string name, string lxrname)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(10);
            PagerInfo<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetJXQcinfoList(name,lxrname);
            return listmodel;
        }
        #endregion
        #endregion

        #region //全部客户信息导出功能
        public FileResult DCCusinfo()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetAllCusinfoordertime();
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("公司名称");
            row1.CreateCell(1).SetCellValue("联系人");
            row1.CreateCell(2).SetCellValue("地址");
            row1.CreateCell(3).SetCellValue("电话");

            if (listmodel != null)//数据不为空
            {
                for (int i = 0; i < listmodel.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(listmodel[i].Name);
                    rowtemp.CreateCell(1).SetCellValue(listmodel[i].LxrName);
                    rowtemp.CreateCell(2).SetCellValue(listmodel[i].Adress);
                    rowtemp.CreateCell(3).SetCellValue(listmodel[i].Tel);
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "客户信息.xls");
        }
        #endregion

        #region //导入匹配KECODE
        public ActionResult ImportCusk3codeView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Cusk3codeExcel(HttpPostedFileBase fileImport)
        {
           
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt =ExcelBase.GetExcelDatatable(filurl, "mapTable");
                Updateuserk3codeinfo(dt);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }

        public void Updateuserk3codeinfo(System.Data.DataTable dt)
        {
            string uid = "";//电商用户Id
            string k3code = "";//k3code
            NACustomerinfoView model = new NACustomerinfoView();
            foreach (DataRow dr in dt.Rows)
            {
                uid= dr["uid"].ToString().Trim();//电商用户Id
                k3code = dr["k3code"].ToString().Trim();//k3code
                if (k3code != "")
                {
                    model = _INACustomerinfoDao.GetCustomerbyUId(uid);
                    if (model != null) {
                        model.K3CODE = k3code;
                        _INACustomerinfoDao.NUpdate(model);
                    }
                        
                    
                }
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

        #region //更新修改K3code
        public ActionResult Updaetk3codeView(string Id)
        {
            NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(Id);
            return View(model);
        }

        //手动提交修改
        public JsonResult updatek3code(string Id, string k3code)
        {
            NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.K3CODE = k3code;
                if (_INACustomerinfoDao.NUpdate(model))
                {
                    return Json(new { result = "success", msg = "提交成功" });
                }
                else
                {
                    return Json(new { result = "error", msg = "修改异常" });
                }
            }
            else
            {
                return Json(new { result = "error", msg = "客户信息为空" });
            }
        }

        //同步电商的k3code
        public JsonResult updatek3codeTbds(string Id)
        {
            NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DsUid == "" || model.DsUid == null)
                    return Json(new { result = "error", msg = "不是电商同步的客户信息" });
                else
                {
                    Newasia.XYOffer dsway = new Newasia.XYOffer();
                    DataTable dt = dsway.GetUserInfoByUserId(Convert.ToInt32(model.DsUid) );
                    string K3CODE = dt.Rows[0]["K3CODE"].ToString();// 
                    model.K3CODE = K3CODE;
                    if (_INACustomerinfoDao.NUpdate(model))
                    {
                        return Json(new { result = "success", msg = "提交成功" });
                    }
                    else
                    {
                        return Json(new { result = "error", msg = "修改异常" });
                    }
                }
            }
            else
            {
                return Json(new { result = "error", msg = "客户信息为空" });
            }
        }
        #endregion




    }
}
