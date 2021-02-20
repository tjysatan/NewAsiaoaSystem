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
    public class PP_TeaminfoController : Controller
    {
        //
        // GET: /PP_Teaminfo/
        IPP_TeaminfoDao _IPP_TeaminfoDao = ContextRegistry.GetContext().GetObject("PP_TeaminfoDao") as IPP_TeaminfoDao;
        IPP_StaffinfoDao _IPP_StaffinfoDao = ContextRegistry.GetContext().GetObject("PP_StaffinfoDao") as IPP_StaffinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IPP_ProfitpointinfoDao _IPP_ProfitpointinfoDao = ContextRegistry.GetContext().GetObject("PP_ProfitpointinfoDao") as IPP_ProfitpointinfoDao;

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            AlbPerDropdown(null);
            PagerInfo<PP_TeaminfoView> listmodel = GetListPager(pageIndex, null,null,null);
            return View(listmodel);
        }
        #endregion



        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["Name"];//团队名称
            string zrname = Request.Form["zrname"];//责任人名称
            string Tel = Request.Form["tel"];//电话
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_TeaminfoView> listmodel = GetListPager(pageIndex, Name,zrname,Tel);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<PP_TeaminfoView> GetListPager(int? pageIndex, string Name,string zrname,string tel)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_TeaminfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_TeaminfoDao.SetPagerPageSize(11);
            PagerInfo<PP_TeaminfoView> listmodel = _IPP_TeaminfoDao.GetTeamList(Name,zrname,tel);
            return listmodel;
        }
        #endregion
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(PP_TeaminfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Team_No = model.Team_No;//团队编号
                    model.Team_Name = model.Team_Name;//团队名称
                    model.Team_zrname = model.Team_zrname;//责任人
                    model.Team_zrTel = model.Team_zrTel;//联系电话
                    model.Team_glyuser = Request.Form["ADListData"];
                    model.C_time = DateTime.Now;//创建时间
                    flay = _IPP_TeaminfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Team_No = model.Team_No;//团队编号
                    model.Team_Name = model.Team_Name;//团队名称
                    model.Team_zrname = model.Team_zrname;//责任人
                    model.Team_zrTel = model.Team_zrTel;//联系电话
                    model.Team_glyuser = Request.Form["ADListData"];
                    model.C_time = DateTime.Now;//创建时间
                    flay = _IPP_TeaminfoDao.NUpdate(model);
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
            AlbPerDropdown(null);
            return View("Edit", new PP_TeaminfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            PP_TeaminfoView sys = new PP_TeaminfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IPP_TeaminfoDao.NGetModelById(id);
            AlbPerDropdown(sys.Team_glyuser);
            return View("Edit", sys);
        }
        #endregion

        #region //员工数据导入
        public ActionResult ImportStaffinfoView()
        {
            AlbumDropdown(null);
            return View();
        }

        //
        //提交
        [HttpPost]
        public JsonResult DRyuangongExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            string teamId = Request.Form["teamName"];//团队Id
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                Inserthuiyimingdan(dt,teamId);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        public void Inserthuiyimingdan(System.Data.DataTable dt,string teamId)
        {
            string Name = "";//员工名称

            PP_StaffinfoView model = new PP_StaffinfoView();
            foreach (DataRow dr in dt.Rows)
            {
                Name = dr["Name"].ToString().Trim();//与会者名称
                if (Name != null && Name != "")
                {
                    model.Sat_Name = Name;
                    model.Sat_rztime = DateTime.Now;
                    model.Sat_Sex = 0;
                    model.Sat_teamId = teamId;
                    model.Sat_Tel = "";
                    model.C_time = DateTime.Now;
                    _IPP_StaffinfoDao.Ninsert(model);
                }
            }
        }

        //读取Excel数据返回datatable
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

        #region 初始化设置下拉框值
        /// <summary>
        /// 初始化设置下拉框值
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown(string SelectedPID)
        {
            List<PP_TeaminfoView> ReasonlistView = _IPP_TeaminfoDao.NGetList() as List<PP_TeaminfoView>;
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
                    Reasonmodel.Name = item.Team_Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //初始化设置管理员帐号的下拉框值

        public void AlbPerDropdown(string SelectedPID)
        {
            List<SysPersonView> sysPerlistView = _ISysPersonDao.NGetList() as List<SysPersonView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (sysPerlistView != null)
            {
                foreach (var item in sysPerlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.UserName;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["sysperonlist"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["sysperonlist"] = new SelectList(Reasonlist, "Id", "Name");


        }
        #endregion

        #region //收入和支出项数据导入
        public ActionResult ImportProfitinfoView()
        {
            AlbumDropdown(null);
            return View();
        }

        //提交
        [HttpPost]
        public JsonResult DRshouruzhuchuExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            string teamId = Request.Form["teamName"];//团队Id
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                Insershouruzhichu(dt, teamId);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        //插入方法
        public void Insershouruzhichu(System.Data.DataTable dt, string teamId)
        {
            string XMname = "";//项目名称
            string Jine = "";//金额
            string dw = "";//单位
            string type = "";//类型 收入 支出
            string sort = "";//排序
             SessionUser user = Session[SessionHelper.User] as SessionUser;
            PP_ProfitpointinfoView model = new PP_ProfitpointinfoView();
            foreach (DataRow dr in dt.Rows)
            {
                XMname = dr["srname"].ToString().Trim();//项目名称
                Jine = dr["jine"].ToString().Trim();//金额
                dw = dr["danwei"].ToString().Trim();//单位
                type = dr["type"].ToString().Trim();//类型 0 收入 1 支出
                sort = dr["sort"].ToString().Trim();//
                if (XMname != null && XMname != "")
                {
                    model.Rwname = XMname;//项目名称
                    model.Rwms = XMname;//描述
                    model.Rwfz = Convert.ToDecimal(Jine);
                    model.Rwdw = dw;
                    model.Rw_teamId = teamId;//团队
                    model.state = 1;
                    model.Sort = Convert.ToInt32(sort);
                    model.type = Convert.ToInt32(type);
                    model.C_name = user.Id;
                    model.C_time = DateTime.Now;
                    _IPP_ProfitpointinfoDao.Ninsert(model);
                }
            }
        }
        #endregion

    }
}
