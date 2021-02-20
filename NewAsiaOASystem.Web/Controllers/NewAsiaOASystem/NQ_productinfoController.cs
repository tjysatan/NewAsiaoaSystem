using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_productinfoController : Controller
    {
        //
        // GET: /NQ_productinfo/
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;

        #region //数据列表
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_productinfoView> listmodel = GetListPager(pageIndex, null,null,null);
            return View(listmodel);
        } 
        #endregion

        #region // 按照条件查询
        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string P_xh = Request.Form["txtSearch_P_xh"];
            string bianhao = Request.Form["txtSearch_bianhao"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_productinfoView> listmodel = GetListPager(pageIndex, Name, P_xh,bianhao);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        } 
        #endregion

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NQ_productinfoView> GetListPager(int? pageIndex, string Name, string P_xh,string bianhao)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_productinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_productinfoDao.SetPagerPageSize(6);
            PagerInfo<NQ_productinfoView> listmodel = _INQ_productinfoDao.GetCinfoList(Name,P_xh,bianhao, Suser);
            return listmodel;
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_productinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Pname = model.Pname;//产品名称
                    model.P_bianhao = model.P_bianhao;//产品编号
                    model.SMFH =model.SMFH;//需要扫码发货
                    model.p_type =Convert.ToInt32(Request.Form["p_type"]); //所属种类
                    model.CreatePerson = user.Id;//创建人
                    model.CreateTime = DateTime.Now;//创建时间
                    flay = _INQ_productinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Pname = model.Pname;//产品名称
                    model.P_bianhao = model.P_bianhao;//产品编号
                    model.SMFH = model.SMFH;//需要扫码发货
                    model.p_type = Convert.ToInt32(Request.Form["p_type"]);//所属种类
                    model.UpdatePerson = user.Id;//更新人
                    model.UpdateTime = DateTime.Now;//更新时间
                    flay = _INQ_productinfoDao.NUpdate(model);
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
            return View("Edit",new NQ_productinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_productinfoView sys = new NQ_productinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_productinfoDao.NGetModelById(id);
            return View("Edit", sys);
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
                List<NQ_productinfoView> sys = _INQ_productinfoDao.NGetList_id(id) as List<NQ_productinfoView>;
                if (null != sys)
                {
                    if (_INQ_productinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NQ_productinfo/List");
        }
        #endregion

        #region //导入数据，根据物料代理更新插入（产品名称，产品编码，产品型号，产品单位）

        //导入页面(产品数据)
        public ActionResult ImportCPDataView()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DRCPinfoExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                UpdateorInsertCPinfo(dt);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        //更新或者插入导入的数据
        public void UpdateorInsertCPinfo(System.Data.DataTable dt)
        {
            string CPbianhao = "";//产品编号
            string CPname = "";//产品名称
            string CPdw = "";//单位
            string CPxh = "";//产品型号
            NQ_productinfoView model = new NQ_productinfoView();
            foreach (DataRow dr in dt.Rows)
            {
                CPbianhao = dr["bianhao"].ToString().Trim();//产品编号
                CPname = dr["CPname"].ToString().Trim();//产品名称
                CPxh = dr["CPxh"].ToString().Trim();//产品型号
                CPdw = dr["CPdw"].ToString().Trim();//产品单位
                model = _INQ_productinfoDao.GetProinfobyname(CPbianhao);
                if (model != null)
                {
                    model.Pname = CPname;//产品名称
                    model.P_xh = CPxh;//产品型号
                    model.dw = CPdw;//2产品单位
                    _INQ_productinfoDao.NUpdate(model);
                }
                else
                {
                    model = new NQ_productinfoView();
                    model.Pname = CPname;//产品名称
                    model.P_xh = CPxh;//产品型号
                    model.dw = CPdw;//2产品单位
                    model.P_bianhao = CPbianhao;//产品编号
                    _INQ_productinfoDao.Ninsert(model);
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


        #region //对接基础数据表
        //更新产品资料表
        public ActionResult BasicsdataView()
        {
            return View();
        }

        //提交更新
        public string Updatecpdata()
        {
            IList<NQ_productinfoView> listmodel = _INQ_productinfoDao.Getproductbystaitisnull();
            //IList<NQ_productinfoView> listmodel = _INQ_productinfoDao.NGetList();
            if (listmodel != null)
            {
                foreach (var item in listmodel)
                {
                    K3_wuliaoinfoView jcdatamodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(item.P_bianhao);
                    if (jcdatamodel == null)//基础数据为空-只修改状态
                    {
                        item.stait = 1;
                        _INQ_productinfoDao.NUpdate(item);
                    }
                    else//存在基础数据-修改产品名称,型号,物料号,是否物联网产品,产品类别 （电控箱,温控器,其他）
                    {
                        item.Pname = jcdatamodel.fname;//产品名称
                        item.P_xh = jcdatamodel.fmodel;//产品型号
                        item.P_bianhao = jcdatamodel.fnumber;//物料编码
                        item.SMFH = JCIsiotcpbyfnumber(jcdatamodel.fnumber);//是否物联网产品
                        item.p_type = Getcptypebytype(jcdatamodel.Type.ToString());//产品类型
                        item.dw = jcdatamodel.unitname;
                        item.stait = 0;
                        _INQ_productinfoDao.NUpdate(item);
                    }
                }
                return listmodel.Count.ToString();
            }
            else
            {
                return "0";
            }
        }
        
        //同步K3基础数据
        public string Updatek3date()
        {
            IList<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.NGetList();
            int xinsum = 0;
            if (listmodel != null)
            {
                foreach (var item in listmodel)
                {
                    NQ_productinfoView cpmodel = _INQ_productinfoDao.Getproductbyfnumber(item.fnumber);
                    if (cpmodel == null)
                    {
                        xinsum = xinsum + 1;
                        cpmodel = new NQ_productinfoView();
                        cpmodel.Pname = item.fname;
                        cpmodel.P_xh = item.fmodel;//型号
                        cpmodel.P_bianhao = item.fnumber;//物料
                        cpmodel.dw = item.unitname;//单位
                        cpmodel.SMFH = JCIsiotcpbyfnumber(item.fnumber);//是否物联网产品
                        cpmodel.p_type = Getcptypebytype(item.Type.ToString());//产品类型
                        cpmodel.CreateTime = DateTime.Now;
                        cpmodel.Nptype = item.Type;
                        cpmodel.stait = 0;
                        _INQ_productinfoDao.Ninsert(cpmodel);
                    }
                }
                return xinsum.ToString();
            }
            else
            {
                return "0";
            }
        }

        //根据物料编码判断是否是物联网产品
        public int JCIsiotcpbyfnumber(string fnumber)
        {
            int index = fnumber.IndexOf("NAW");
            if (index > -1)
            {
                return 1;//物联网产品
            }
            else
            {
                return 0;//非物联网产品
            }
        }

        //返回产品种类
        public int Getcptypebytype(string type)
        {
            if (type == "0" || type == "1" || type == "2" || type == "6" || type == "7")
            {
                return 2;
            }
            if (type == "3" || type == "5")
            {
                return 1;
            }
            if (type == "4")
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        #endregion

    }
}
