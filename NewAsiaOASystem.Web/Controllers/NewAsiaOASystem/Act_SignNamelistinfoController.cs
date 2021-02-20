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
    public class Act_SignNamelistinfoController : Controller
    {
        IAct_SignNamelistinfoDao _IAct_SignNamelistinfoDao = ContextRegistry.GetContext().GetObject("Act_SignNamelistinfoDao") as IAct_SignNamelistinfoDao;
        //
        // GET: /Act_SignNamelistinfo/
        /// <summary>
        /// 会议签到控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region //导入与会者名单
        //导入名单
        public ActionResult ImporthuiyiinfoView()
        {
            return View();
        }
        //提交
        [HttpPost]
        public JsonResult DRhuiyimingdanExcel(HttpPostedFileBase fileImport)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            if (fileImport != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                fileImport.SaveAs(filePath);
                string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                Inserthuiyimingdan(dt);
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        //插入数据
        public void Inserthuiyimingdan(System.Data.DataTable dt)
        {
            string Name = "";//与会者名称
            string Tel = "";//与会者联系方式
            string Company = "";//公司名称
            string Dm = "";//编码
            Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
            foreach (DataRow dr in dt.Rows)
            {
                Name = dr["Name"].ToString().Trim();//与会者名称
                Tel = dr["Tel"].ToString().Trim();//与会者联系方式
                Company = dr["Company"].ToString().Trim();//公司名称
                Dm = dr["Dm"].ToString().Trim();//编码
                if (Name != null && Name != "")
                {
                    model = _IAct_SignNamelistinfoDao.Gethuiyimingdanmodelbycompany(Company);
                    if (model == null)//插入
                    {
                        model = new Act_SignNamelistinfoView();
                        model.Name = Name;//与会者名单
                        model.Tel = Tel;//与会者电话
                        model.Company = Company;//与会者公司名称
                        model.Dm = Dm;//编号
                        model.Issq = 0;//还未签到
                        model.C_time = DateTime.Now;//创建时间
                        model.Ispg = 0;//签到的时候已经飘过
                        model.Isyd = 0;//原定与会名单
                        _IAct_SignNamelistinfoDao.Ninsert(model);
                    }
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

        #region //客服签到操作
        //页面
        public ActionResult KefuqiandaoView()
        {
            return View();
        }

        //提交
        [HttpPost]
        public JsonResult KefuqiandaoEide(FormCollection form)
        {
            string Name = form["Name"];//与会者名称/公司名称/编码
            string type=form["type"];//0与会者名称 1公司名称  2编码
            Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
            if (type == "0")//根据与会者名称查询
            {
                model = _IAct_SignNamelistinfoDao.GethuiyimingdanbyName(Name);
                if (model != null)
                {
                    if (model.Issq != 1)
                    {
                        //model.Issq = 1;//签到
                        //model.Qddatetime = DateTime.Now;//签到时间
                        //if (_IAct_SignNamelistinfoDao.Ninsert(model))
                        //{
                        //    return Json(new { result = "success" });
                        //}
                        //else
                        //{
                        //    return Json(new { result = "error" });
                        //}
                        string rul = model.Company + "-" + model.Name + "-" + "编号";
                        return Json(new { result = rul });
                    }
                    else
                    {
                        return Json(new { result = "error5" });//已经签到
                    }
                }
                else
                {
                    return Json(new { result = "error1" });//根据与会者名称没有查询到该名单
                }
            }
            if (type == "1")//根据公司名称
            {
                model = _IAct_SignNamelistinfoDao.Gethuiyimingdanmodelbycompany(Name);
                if (model != null)
                {
                    if (model.Issq != 1)
                    {
                        //model.Issq = 1;//签到
                        //model.Qddatetime = DateTime.Now;//签到时间
                        //if (_IAct_SignNamelistinfoDao.Ninsert(model))
                        //{
                        //    return Json(new { result = "success" });
                        //}
                        //else
                        //{
                        //    return Json(new { result = "error" });
                        //}
                        string rul = model.Company + "-" + model.Name + "-" + "编号";
                        return Json(new { result = rul });
                    }
                    else
                    {
                        return Json(new { result = "error5" });//已经签到
                    }
                }
                else
                {
                    return Json(new { result = "error2" });//根据公司名单名称没有查询到该名单
                }
            }
            if (type == "2")
            {
                model = _IAct_SignNamelistinfoDao.Gethuiyimingdanbydm(Name);
                if (model != null)
                {
                    if (model.Issq != 1)
                    {
                        //model.Issq = 1;//签到
                        //model.Qddatetime = DateTime.Now;//签到时间
                        //if (_IAct_SignNamelistinfoDao.Ninsert(model))
                        //{
                        //    return Json(new { result = "success" });
                        //}
                        //else
                        //{
                        //    return Json(new { result = "error" });
                        //}
                        string rul = model.Company + "-" + model.Name + "-" + "编号："+model.Dm;
                        return Json(new { result = rul });
                    }
                    else
                    {
                        return Json(new { result = "error5" });//已经签到
                    }
                }
                else
                {
                    return Json(new { result = "error3" });//根据公司编码没有查询到该名单
                }
            }
            return Json(new { result = "error4" });//查无此人
        }

        //修改状态
        public string Kefuqianupdate(string name, string type)
        {
            if (type == "0")//根据与会者名称查询
            {
                Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
                model = _IAct_SignNamelistinfoDao.GethuiyimingdanbyName(name);
                if (model != null)
                {
                    model.Issq = 1;//签到
                    model.Qddatetime = DateTime.Now;//签到时间
                    _IAct_SignNamelistinfoDao.NUpdate(model);
                }
            }
            if (type == "1")//根据公司名称
            {
                Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
                model = _IAct_SignNamelistinfoDao.Gethuiyimingdanmodelbycompany(name);
                if (model != null)
                {
                    model.Issq = 1;//签到
                    model.Qddatetime = DateTime.Now;//签到时间
                    _IAct_SignNamelistinfoDao.NUpdate(model);
                }
            }
            if (type == "2")//根据公司名称
            {
                Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
                model = _IAct_SignNamelistinfoDao.Gethuiyimingdanbydm(name);
                if (model != null)
                {
                    model.Issq = 1;//签到
                    model.Qddatetime = DateTime.Now;//签到时间
                    _IAct_SignNamelistinfoDao.NUpdate(model);
                }
            }
            return "0";
        }
        #endregion

        #region //新增来宾
        //页面
        public ActionResult XinzenglaibinView()
        {
            return View();
        }

        //提交
        [HttpPost]
        public JsonResult xinzengEide(FormCollection form)
        {
            string Name = form["Name"];//与会者名称
            string company = form["company"];//公司名称
            Act_SignNamelistinfoView model = new Act_SignNamelistinfoView();
            if (_IAct_SignNamelistinfoDao.Gethuiyimingdanmodelbycompany(company) == null)
            {
                model.Name = Name;
                model.Company = company;
                model.Ispg = 0;
                model.Isyd = 1;
                model.Issq = 1;
                model.C_time = DateTime.Now;
                model.Qddatetime = DateTime.Now;
                if(_IAct_SignNamelistinfoDao.Ninsert(model))
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error1" });//新增失败
            }
            else
            {
                return Json(new { result = "error" });//存在相同公司名
            }
        }
        #endregion

        #region //屏幕显示
        /// <summary>
        /// 屏幕显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ScreendisplayView()
        {
            return View();
        }

        //查找要在头部显示的数据
        public string GetzaitoubuxianshiData()
        {
            string json;
            Act_SignNamelistinfoView model = _IAct_SignNamelistinfoDao.Getyijingqiandaomeiyoupiaoguo();
            if (model != null)
            {
                json = JsonConvert.SerializeObject(_IAct_SignNamelistinfoDao.Getyijingqiandaomeiyoupiaoguo());
                model.Ispg = 1;
                _IAct_SignNamelistinfoDao.NUpdate(model);
                return json;
            }
            else
            {
                return null;
            }

        }

        //查找签到过的在头部显示过的数据
        public string GetqiandaoguopiaoguodeData()
        {
            string json;
            json = JsonConvert.SerializeObject(_IAct_SignNamelistinfoDao.Getyijingqiandaoyijingpiaoguo());
            return json;
        }
        #endregion


    }
}
