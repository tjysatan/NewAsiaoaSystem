using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace NewAsiaOASystem.Web.Controllers
{
    public class ERPDATAController : Controller
    {
        //ERP数据汇总
        IERP_SASalAinfoDao _IERP_SASalAinfoDao = ContextRegistry.GetContext().GetObject("ERP_SASalAinfoDao") as IERP_SASalAinfoDao;
        IJs_xz_yfcostDao _IJs_xz_yfcostDao = ContextRegistry.GetContext().GetObject("Js_xz_yfcostDao") as IJs_xz_yfcostDao;
        IERP_SASalA_FBCP_BCBWKDao _IERP_SASalA_FBCP_BCBWKDao = ContextRegistry.GetContext().GetObject("ERP_SASalA_FBCP_BCBWKDao") as IERP_SASalA_FBCP_BCBWKDao;
        // GET: /ERPDATA/

        public ActionResult Index()
        {
            return View();
        }

        #region //销售出货的数据
        public ActionResult Xschdatelist()
        {
            return View();
        }

        #region //分页数据
        public ActionResult GetSASalAPagerInfo(int? page, int limit, string AbsID, string CrdName, string ItmID, string ItmName, string ItmSpec)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IERP_SASalAinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IERP_SASalAinfoDao.SetPagerPageSize(limit);
            PagerInfo<ERP_SASalAinfoView> listmodel = _IERP_SASalAinfoDao.GetSASalAPagerInfo(AbsID, CrdName, ItmID, ItmName, ItmSpec);
            var result = new
            {
                code = 0,
                msg = "",
                count = listmodel.RecordCount,
                data = listmodel.DataList,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //通过销售出货接口获取数据
        public JsonResult GetXSCHDATA(string DATEM)
        {

            try
            {
                //根据日期查询本地数据
                List<ERP_SASalAinfoView> oldlist = _IERP_SASalAinfoDao.GetalldatabyAbsID(DATEM) as List<ERP_SASalAinfoView>;
                if (oldlist != null)
                {//删除原先的同步数据
                    _IERP_SASalAinfoDao.NDelete(oldlist);
                }
                //重新从接口获取数据
                string resjson = zypushsoftHelper.GetSASalA_AbsID(DATEM);
                if (resjson != null && resjson != null && resjson != "[]")
                {
                    var n = 0;
                    var s = 0;
                    List<ERP_SASalAinfoView> timemodel = getObjectByJson<ERP_SASalAinfoView>(resjson);
                    foreach (var a in timemodel)
                    {
                        ERP_SASalAinfoView model = new ERP_SASalAinfoView();
                        model = a;
                        //model.LineSum = Convert.ToDecimal(a.LineSum);
                        //model.TaxAfPrice = Convert.ToDecimal(a.TaxAfPrice);
                        //model.Price = Convert.ToDecimal(a.Price);
                        model.BDocDate = Convert.ToDateTime(a.DocDate);
                        model.XSCostPrice = (a.BaseQty * a.TaxAfPrice);
                        model.C_time = DateTime.Now;
                        if (_IERP_SASalAinfoDao.Ninsert(model))
                        {
                            n = n + 1;
                        }
                        else
                        {
                            s = s + 1;
                        }
                    }
                    return Json(new { result = "success", msg = "成功获取"+n+"条数据。保存失败："+s+"条" });
                }
                else
                {
                    return Json(new { result = "error", msg = "接口数据为空。" });
                }
            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = x });
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
        #endregion

        #region //各个责任工程师的产品销售数据
        public ActionResult XschdatabyZ_EmpNameView(string AbsID)
        {
            ViewData["AbsID"] = AbsID;
            return View();
        }

        #region //获取工程师负责的产品销售数据（集合到每一个组）
        public JsonResult GetXschdatabyZ_EmpName(string AbsID)
        {
            try
            {
                decimal hzyjine = 0;//何健、林超、周旸一组
                decimal pwjine = 0;//彭菊、王韧一组
                decimal zzsjine = 0;//张骏、张文杰、孙微一组
                decimal yjsjine = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjine = 0;//吕正佳一组
                decimal llgjine = 0;//老李工 李凤进
                decimal wgjine = 0;//闻工 闻晓成

                decimal hzyjinew = 0;//何健、林超、周旸一组
                decimal pwjinew = 0;//彭菊、王韧一组
                decimal zzsjinew = 0;//张骏、张文杰、孙微一组
                decimal yjsjinew = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjinew = 0;//吕正佳一组
                decimal llgjinew = 0;//老李工 李凤进
                decimal wgjinew = 0;//闻工 闻晓成

                //销售成本
                decimal hzyjinecb = 0;//何健、林超、周旸一组
                decimal pwjinecb = 0;//彭菊、王韧一组
                decimal zzsjinecb = 0;//张骏、张文杰、孙微一组
                decimal yjsjinecb = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjinecb = 0;//吕正佳一组
                decimal llgjinecb = 0;//老李工 李凤进
                decimal wgjinecb = 0;//闻工 闻晓成
                //List<object> list = _IERP_SASalAinfoDao.GetsumjineGROUPBY(AbsID);
                string Jsonstr;
                Jsonstr = JsonConvert.SerializeObject(_IERP_SASalAinfoDao.GetsumjineGROUPBY(AbsID));
                JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
                if (ja != null)
                {

                    for (int i = 0, j = ja.Count(); i < j; i++)
                    {
                        if (ja[i][0].ToString() == "何健")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "林超")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "周旸")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "彭菊")
                        {
                            pwjine = pwjine + Convert.ToDecimal(ja[i][1].ToString());
                            pwjinew = pwjinew + Convert.ToDecimal(ja[i][2].ToString());
                            pwjinecb = pwjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "王韧")
                        {
                            pwjine = pwjine + Convert.ToDecimal(ja[i][1].ToString());
                            pwjinew = pwjinew + Convert.ToDecimal(ja[i][2].ToString());
                            pwjinecb = pwjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "张骏")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "张文杰")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "孙微")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "袁静铖")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "金志浩")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "孙中原")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "吕正佳")
                        {
                            lzjjine = lzjjine + Convert.ToDecimal(ja[i][1].ToString());
                            lzjjinew = lzjjinew + Convert.ToDecimal(ja[i][2].ToString());
                            lzjjinecb = lzjjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "李凤进")
                        {
                            llgjine = llgjine + Convert.ToDecimal(ja[i][1].ToString());
                            llgjinew = llgjinew + Convert.ToDecimal(ja[i][2].ToString());
                            llgjinecb = llgjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "闻晓成")
                        {
                            wgjine = wgjine + Convert.ToDecimal(ja[i][1].ToString());
                            wgjinew = wgjinew + Convert.ToDecimal(ja[i][2].ToString());
                            wgjinecb = wgjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                    }

                    List<object> datelist = new List<object>();

                    jsfzmodel model = new jsfzmodel();
                    model.jsfzname = "何健林超周旸组";
                    model.hzjine = hzyjine;
                    model.wsjine = hzyjinew;
                    model.xscbjine = hzyjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "彭菊王韧组";
                    model.hzjine = pwjine;
                    model.wsjine = pwjinew;
                    model.xscbjine = pwjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "张骏张文杰孙微组";
                    model.hzjine = zzsjine;
                    model.wsjine = zzsjinew;
                    model.xscbjine = zzsjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "袁静铖金志浩孙中原组";
                    model.hzjine = yjsjine;
                    model.wsjine = yjsjinew;
                    model.xscbjine = yjsjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "吕正佳组";
                    model.hzjine = lzjjine;
                    model.wsjine = lzjjinew;
                    model.xscbjine = lzjjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "李凤进组";
                    model.hzjine = llgjine;
                    model.wsjine = llgjinew;
                    model.xscbjine = llgjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "闻晓成组";
                    model.hzjine = wgjine;
                    model.wsjine = wgjinew;
                    model.xscbjine = wgjinecb;
                    datelist.Add(model);

                    //string datajson = JsonConvert.SerializeObject(datelist);


                    var result = new
                    {
                        code = 0,
                        msg = "",
                        count = 100,
                        data = datelist,
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var result = new
                    {
                        code = 0,
                        msg = "暂无数据",
                        count = 100,
                        data = "",
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception x)
            {
                var result = new
                {
                    code = 0,
                    msg = x,
                    count = 100,
                    data = "",
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public class jsfzmodel
        {
            /// <summary>
            /// 技术分组名称
            /// </summary>
            public virtual string jsfzname { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public virtual decimal? hzjine { get; set; }

            /// <summary>
            /// 未税金额
            /// </summary>
            public virtual decimal? wsjine { get; set; }

            /// <summary>
            /// 销售成本金额
            /// </summary>
            public virtual decimal? xscbjine { get; set; }
        }

        #region //各组利润显示的页面
        public ActionResult FRXschdatabyZ_EmpNameView(string AbsID, string hsjine, string wsjine)
        {

            return View();
        }
        #endregion
        #endregion
        #endregion

        #region //各技术小组的研发成本
        public ActionResult JsxzCostView(string name, string date)
        {
            //
            ViewData["name"] = name;
            ViewData["date"] = date;
            return View();
        }

        public JsonResult Getjsxzcostbynameanddate(string name, string date)
        {
            try
            {
                Js_xz_yfcostView model = _IJs_xz_yfcostDao.GetJx_xzcostbyxznameanddate(name,date);
                if (model != null)
                {
                    string json = JsonConvert.SerializeObject(model);
                    return Json(new { result = "success", data = json, msg = "成功" });
                }
                else
                {
                    return Json(new { result = "error", msg = "数据空" });
                }
            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }

        #region //提交小组的研发成本
        public JsonResult Saveorupdate_Js_xz_cbcost(string name, string date, string cost1, string cost2, string cost3)
        {
            try
            {
                //检查是否存在
                Js_xz_yfcostView model = _IJs_xz_yfcostDao.GetJx_xzcostbyxznameanddate(name, date);
                if (model != null)
                {
                    model.Fy_jine1 = Convert.ToDecimal(cost1);
                    model.Fy_jine2 = Convert.ToDecimal(cost2);
                    model.Fy_jine3 = Convert.ToDecimal(cost3);
                    model.c_time = DateTime.Now;
                    _IJs_xz_yfcostDao.NUpdate(model);
                    return Json(new { result = "success", msg = "设置成功！" });
                }
                else//新增
                {
                    model = new Js_xz_yfcostView();
                    model.Js_xz_name = name;
                    model.Fy_data = date;
                    model.Fy_jine1 = Convert.ToDecimal(cost1);
                    model.Fy_jine2 = Convert.ToDecimal(cost2);
                    model.Fy_jine3 = Convert.ToDecimal(cost3);
                    model.c_time = DateTime.Now;
                    _IJs_xz_yfcostDao.Ninsert(model);
                    return Json(new { result = "success", msg = "设置成功！" });
                }
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //各技术小组的月利润
        public ActionResult Js_KPI_View(string name, string date,string hsjine,string wsjine,string cbjine)
        {
            ViewData["name"] = name;
            ViewData["date"] = date;
            ViewData["hsjine"] = hsjine;
            ViewData["wsjine"] = wsjine;
            ViewData["cbjine"] = cbjine;
            return View();
        }
        #endregion

        #region //数据导出
        public FileResult dc_js_kpi_data(string name, string date, string hsjine, string wsjine, string cbjine)
        {
            double yfbl = 0.015;
            //运费 非标组的是 0.05 常规组的是0.015
            if (name == "张骏张文杰孙微组" || name == "袁静铖金志浩孙中原组" || name == "吕正佳组")
                yfbl = 0.05;
            //核算材料未税金额

            var clwsjine =(Convert.ToDouble(cbjine) * 0.85).ToString("0.00");
            //生产的工资福利
            var scgzjine =(Convert.ToDouble(cbjine) * 0.08).ToString("0.00");
            //智造费用
            var zzjine= (Convert.ToDouble(cbjine) * 0.05).ToString("0.00");
            //车间管理金额
            var cjgljine= (Convert.ToDouble(cbjine) * 0.02).ToString("0.00");

            //国税地税金额 （销售的未税金额-材料未税金额）*0.15
            var gsdsjine = ((Convert.ToDouble(wsjine) - Convert.ToDouble(clwsjine)) * 0.15).ToString("0.00");

            //运费 销售的未税金额*0.05
            var  yfjine = (Convert.ToDouble(wsjine) * yfbl).ToString("0.00");

            //市场费用
            var sjfyjine = (Convert.ToDouble(wsjine) * 0.04).ToString("0.00");
            //销售费用 销售的未税金额*0.024
            var scfyjine = (Convert.ToDouble(wsjine) * 0.024).ToString("0.00");
            //客服人员费用 销售的未税金额*0.006
            var kffyjine = (Convert.ToDouble(wsjine) * 0.006).ToString("0.00");
            //市场广告非 销售的未税金额*0.01
            var scggfjine = (Convert.ToDouble(wsjine) * 0.01).ToString("0.00");
            //保险费用
            var bxfjine = (Convert.ToDouble(wsjine) * 0.01).ToString("0.00");
            //平台费用
            var ptfjine = (Convert.ToDouble(wsjine) * 0.01).ToString("0.00");

            //产品的成本
            var cpcbjine = (Convert.ToDouble(clwsjine) + Convert.ToDouble(scgzjine) + Convert.ToDouble(zzjine) + Convert.ToDouble(cjgljine) + Convert.ToDouble(gsdsjine)
                + Convert.ToDouble(yfjine) + Convert.ToDouble(sjfyjine) + Convert.ToDouble(bxfjine) + Convert.ToDouble(ptfjine)).ToString("0.00");
          

            //查询研发成本
            Js_xz_yfcostView model = _IJs_xz_yfcostDao.GetJx_xzcostbyxznameanddate(name,date);
            var cost1 = "0";
            var cost2 = "0";
            var cost3 = "0";
            var zcost = "0";
            if (model != null)
            {
                cost1 =Convert.ToString(model.Fy_jine1);
                cost2 = Convert.ToString(model.Fy_jine2);
                cost3 = Convert.ToString(model.Fy_jine3);
                zcost = Convert.ToString(model.Fy_jine1 + model.Fy_jine2 + model.Fy_jine3);
            }
            //利润
            var lirunjine = (Convert.ToDouble(wsjine) - Convert.ToDouble(cpcbjine) - Convert.ToDouble(zcost)).ToString("0.00");
            var kflijine = ((Convert.ToDouble(wsjine) - Convert.ToDouble(cpcbjine) - Convert.ToDouble(zcost))*0.2).ToString("0.00");

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("项目");
            row1.CreateCell(2).SetCellValue("规则");
            row1.CreateCell(3).SetCellValue("总金额");
            //第一行
            NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(1);
            rowtemp.CreateCell(0).SetCellValue("一、");
            rowtemp.CreateCell(1).SetCellValue("报价（销售额未税）");
            rowtemp.CreateCell(2).SetCellValue("按照报价系统（未税价=含税价/1.13)");
            rowtemp.CreateCell(3).SetCellValue(wsjine);
            //2
            rowtemp = sheet1.CreateRow(2);
            rowtemp.CreateCell(0).SetCellValue("二、");
            rowtemp.CreateCell(1).SetCellValue("成本--产品");
            rowtemp.CreateCell(2).SetCellValue("1--9合计");
            rowtemp.CreateCell(3).SetCellValue(cpcbjine);

            //3
            rowtemp = sheet1.CreateRow(3);
            rowtemp.CreateCell(0).SetCellValue("1、");
            rowtemp.CreateCell(1).SetCellValue("材料（未税）");
            rowtemp.CreateCell(2).SetCellValue("全部含包材");
            rowtemp.CreateCell(3).SetCellValue(clwsjine.ToString());
            //4
            rowtemp = sheet1.CreateRow(4);
            rowtemp.CreateCell(0).SetCellValue("2、");
            rowtemp.CreateCell(1).SetCellValue("直接生产人员工资福利");
            rowtemp.CreateCell(2).SetCellValue("孙中原工序标准");
            rowtemp.CreateCell(3).SetCellValue(scgzjine.ToString());
            //5
            rowtemp = sheet1.CreateRow(5);
            rowtemp.CreateCell(0).SetCellValue("3、");
            rowtemp.CreateCell(1).SetCellValue("制造费用");
            rowtemp.CreateCell(2).SetCellValue("房租、折旧、水电、工具、办公费、福利、劳保、修理费等（占销售额比例3%）");
            rowtemp.CreateCell(3).SetCellValue(zzjine.ToString());

            //6
            rowtemp = sheet1.CreateRow(6);
            rowtemp.CreateCell(0).SetCellValue("4、");
            rowtemp.CreateCell(1).SetCellValue("车间管理人员工资福利");
            rowtemp.CreateCell(2).SetCellValue("车间干部仓管助理等（占销售比例1.6 %)");
            rowtemp.CreateCell(3).SetCellValue(cjgljine.ToString());

            //7
            rowtemp = sheet1.CreateRow(7);
            rowtemp.CreateCell(0).SetCellValue("5、");
            rowtemp.CreateCell(1).SetCellValue("国税增值税金及地税税金");
            rowtemp.CreateCell(2).SetCellValue("（销售额未税-材料未税金额）*15%");
            rowtemp.CreateCell(3).SetCellValue(gsdsjine.ToString());
            //8
            rowtemp = sheet1.CreateRow(8);
            rowtemp.CreateCell(0).SetCellValue("6、");
            rowtemp.CreateCell(1).SetCellValue("运费");
            rowtemp.CreateCell(2).SetCellValue("（重量、体积、里程（非标目前按照销售额5%）");
            rowtemp.CreateCell(3).SetCellValue(yfjine);
            //9
            rowtemp = sheet1.CreateRow(9);
            rowtemp.CreateCell(0).SetCellValue("7、");
            rowtemp.CreateCell(1).SetCellValue("市场费用");
            rowtemp.CreateCell(2).SetCellValue("7.1+7.2+7.3");
            rowtemp.CreateCell(3).SetCellValue(sjfyjine);
            //10
            rowtemp = sheet1.CreateRow(10);
            rowtemp.CreateCell(0).SetCellValue("7.1、");
            rowtemp.CreateCell(1).SetCellValue("销售人员费用");
            rowtemp.CreateCell(2).SetCellValue("依据占销售比例2.4%");
            rowtemp.CreateCell(3).SetCellValue(scfyjine);
            //11
            rowtemp = sheet1.CreateRow(11);
            rowtemp.CreateCell(0).SetCellValue("7.2、");
            rowtemp.CreateCell(1).SetCellValue("客服人员费用");
            rowtemp.CreateCell(2).SetCellValue("依据占销售比例0.6%");
            rowtemp.CreateCell(3).SetCellValue(kffyjine);
            //12
            rowtemp = sheet1.CreateRow(12);
            rowtemp.CreateCell(0).SetCellValue("7.3、");
            rowtemp.CreateCell(1).SetCellValue("市场广告费");
            rowtemp.CreateCell(2).SetCellValue("依据占销售比例1%");
            rowtemp.CreateCell(3).SetCellValue(scggfjine);
            //13
            rowtemp = sheet1.CreateRow(13);
            rowtemp.CreateCell(0).SetCellValue("8、");
            rowtemp.CreateCell(1).SetCellValue("保险费");
            rowtemp.CreateCell(2).SetCellValue("依据占销售比例1%");
            rowtemp.CreateCell(3).SetCellValue(bxfjine);
            //14
            rowtemp = sheet1.CreateRow(14);
            rowtemp.CreateCell(0).SetCellValue("9、");
            rowtemp.CreateCell(1).SetCellValue("平台费用");
            rowtemp.CreateCell(2).SetCellValue("依据占销售比例1%");
            rowtemp.CreateCell(3).SetCellValue(ptfjine);

            //15
            rowtemp = sheet1.CreateRow(15);
            rowtemp.CreateCell(0).SetCellValue("三、");
            rowtemp.CreateCell(1).SetCellValue("成本--研发人员");
            rowtemp.CreateCell(2).SetCellValue("1-3合计");
            rowtemp.CreateCell(3).SetCellValue(zcost);
            //16
            rowtemp = sheet1.CreateRow(16);
            rowtemp.CreateCell(0).SetCellValue("1、");
            rowtemp.CreateCell(1).SetCellValue("研发人员工资福利");
            rowtemp.CreateCell(2).SetCellValue("按照实际");
            rowtemp.CreateCell(3).SetCellValue(cost1);
            //17
            rowtemp = sheet1.CreateRow(17);
            rowtemp.CreateCell(0).SetCellValue("2、");
            rowtemp.CreateCell(1).SetCellValue("研发人员费用");
            rowtemp.CreateCell(2).SetCellValue("按照部门实际发生分摊");
            rowtemp.CreateCell(3).SetCellValue(cost2);
            //18
            rowtemp = sheet1.CreateRow(18);
            rowtemp.CreateCell(0).SetCellValue("3、");
            rowtemp.CreateCell(1).SetCellValue("售后费用");
            rowtemp.CreateCell(2).SetCellValue("差旅费、换货、赔偿等");
            rowtemp.CreateCell(3).SetCellValue(cost3);

            //19
            rowtemp = sheet1.CreateRow(19);
            rowtemp.CreateCell(0).SetCellValue("四、");
            rowtemp.CreateCell(1).SetCellValue("利润");
            rowtemp.CreateCell(2).SetCellValue("售价-成本产品-成本研发人员");
            rowtemp.CreateCell(3).SetCellValue(lirunjine);
            //20
            rowtemp = sheet1.CreateRow(20);
            rowtemp.CreateCell(0).SetCellValue("五、");
            rowtemp.CreateCell(1).SetCellValue("可分配利润");
            rowtemp.CreateCell(2).SetCellValue("1-5合计");
            rowtemp.CreateCell(3).SetCellValue(lirunjine);

            //21
            rowtemp = sheet1.CreateRow(21);
            rowtemp.CreateCell(0).SetCellValue("1、");
            rowtemp.CreateCell(1).SetCellValue("技术个人、小组");
            rowtemp.CreateCell(2).SetCellValue("利润20%");
            rowtemp.CreateCell(3).SetCellValue(kflijine);
            //22
            rowtemp = sheet1.CreateRow(22);
            rowtemp.CreateCell(0).SetCellValue("2、");
            rowtemp.CreateCell(1).SetCellValue("技术部门管理");
            rowtemp.CreateCell(2).SetCellValue("利润20%");
            rowtemp.CreateCell(3).SetCellValue(kflijine);
            //23
            rowtemp = sheet1.CreateRow(23);
            rowtemp.CreateCell(0).SetCellValue("3、");
            rowtemp.CreateCell(1).SetCellValue("公司发展基金");
            rowtemp.CreateCell(2).SetCellValue("利润20%");
            rowtemp.CreateCell(3).SetCellValue(kflijine);
            //24
            rowtemp = sheet1.CreateRow(24);
            rowtemp.CreateCell(0).SetCellValue("4、");
            rowtemp.CreateCell(1).SetCellValue("公司股东利益");
            rowtemp.CreateCell(2).SetCellValue("利润20%");
            rowtemp.CreateCell(3).SetCellValue(kflijine);
            //25
            rowtemp = sheet1.CreateRow(25);
            rowtemp.CreateCell(0).SetCellValue("5、");
            rowtemp.CreateCell(1).SetCellValue("公司管理团队");
            rowtemp.CreateCell(2).SetCellValue("利润20%");
            rowtemp.CreateCell(3).SetCellValue(kflijine);

            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", name+ date + "绩效核算.xls");
        }
        #endregion
        #endregion

        #region //非标产品出货中含半成品的数据
        public JsonResult GetFB_WKDATA_by_AbsID(string AbsID)
        {
            try
            {
                //根据日期查询本地的非标出货中温控半成品数据
                List<ERP_SASalA_FBCP_BCBWKView> oldlist = _IERP_SASalA_FBCP_BCBWKDao.GetFB_WKDATA_by_AbsID(AbsID) as List<ERP_SASalA_FBCP_BCBWKView>;
                if (oldlist != null)
                {//删除已经汇总的当前的半成品温控的数据
                    _IERP_SASalA_FBCP_BCBWKDao.NDelete(oldlist);
                }
                //重新根据日期查询本地的非标出货数据
                IList<ERP_SASalAinfoView> FBlist = _IERP_SASalAinfoDao.GetFB_DATA_BY_AbsID(AbsID);
                if (FBlist == null)
                    return Json(new { result = "error", msg = "当前没有非标产品销售出货" });
                foreach (var item in FBlist)
                {
                    //通过物料代码查询BOM（未停用的BOM）
                    string strjson = zypushsoftHelper.GetBom_QYbywlno(item.ItmID);
                    if (strjson != "" && strjson != null && strjson != "[]")
                    {
                        List<Bommodel> bomdata= getObjectByJson<Bommodel>(strjson);
                        foreach (var bomitem in bomdata)
                        {
                           string str = bomitem.ItmID.Substring(0, 3);
                            if (str == "06.")
                            {//是半成品温控
                             //查询ERP中的内部售价
                                decimal CostPrice = 0;
                                string wlgxgsjson = zypushsoftHelper.GetMDItm_GXGS(bomitem.ItmID);
                                if (wlgxgsjson != null || wlgxgsjson != "[]")
                                {
                                    List<Ps_wlGXGSModel> wlgsmodel = getObjectByJson<Ps_wlGXGSModel>(wlgxgsjson);
                                    Ps_wlGXGSModel gxgsmodel = wlgsmodel[0];
                                    if (gxgsmodel.Z_NBPrice == null)
                                        gxgsmodel.Z_NBPrice = "0";
                                    else
                                        CostPrice = Convert.ToDecimal(gxgsmodel.Z_NBPrice);
                                }
                                ERP_SASalA_FBCP_BCBWKView model = new ERP_SASalA_FBCP_BCBWKView();
                                model.F_Id = item.Id;
                                model.F_ItmID = item.ItmID;
                                model.F_ItmName = item.ItmName;
                                model.F_ItmSpec = item.ItmSpec;
                                model.F_Z_EmpName = item.Z_EmpName;
                                model.F_BaseQty = item.BaseQty;
                                model.F_CostPrice = item.CostPrice;
                                model.AbsID = AbsID;
                                model.ItmID = bomitem.ItmID;
                                model.ItmName = bomitem.ItmName;
                                model.ItmSpec = bomitem.ItmSpec;
                                model.CostPrice = CostPrice;
                                //model.Z_EmpName = "";
                                model.BaseQty = Convert.ToDecimal(bomitem.NetQty) * item.BaseQty;
                                _IERP_SASalA_FBCP_BCBWKDao.Ninsert(model);
                            }
                        }
                    }

                }
                return Json(new { result = "success", msg = "操作成功" });

            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //根据月份导出半成品数据
        public FileResult DC_FB_WKDATA_by_AbsID_Eexcl(string AbsID)
        {
            IList<ERP_SASalA_FBCP_BCBWKView> list = _IERP_SASalA_FBCP_BCBWKDao.GetFB_WKDATA_by_AbsID(AbsID);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("父级产品料号");
            row1.CreateCell(1).SetCellValue("父级产品");
            row1.CreateCell(2).SetCellValue("数量");
            row1.CreateCell(3).SetCellValue("责任工程师");
            row1.CreateCell(4).SetCellValue("温控产品料号");
            row1.CreateCell(5).SetCellValue("温控产品名称");
            row1.CreateCell(6).SetCellValue("温控产品规格");
            row1.CreateCell(7).SetCellValue("总用量");
            row1.CreateCell(8).SetCellValue("毛利单价（内部售价）");
            row1.CreateCell(9).SetCellValue("温控毛利金额");

            if (list != null)//数据不为空
            {
                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(list[i].F_ItmID);
                    rowtemp.CreateCell(1).SetCellValue(list[i].F_ItmSpec);
                    rowtemp.CreateCell(2).SetCellValue(list[i].F_BaseQty.ToString());
                    rowtemp.CreateCell(3).SetCellValue(list[i].F_Z_EmpName);
                    rowtemp.CreateCell(4).SetCellValue(list[i].ItmID);
                    rowtemp.CreateCell(5).SetCellValue(list[i].ItmName);
                    rowtemp.CreateCell(6).SetCellValue(list[i].ItmSpec);
                    rowtemp.CreateCell(7).SetCellValue(list[i].BaseQty.ToString());
                    rowtemp.CreateCell(8).SetCellValue(list[i].CostPrice.ToString());
                    rowtemp.CreateCell(9).SetCellValue((list[i].BaseQty* list[i].CostPrice).ToString());
                }
            }

            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", AbsID +"非标电控中用到的温控.xls");
        }
        #endregion


        #region //返回BOM的实体
        public class Bommodel
        {
            public virtual string ItmID { get; set; }

            public virtual string ItmName { get; set; }

            public virtual string ItmSpec { get; set; }

            public virtual string NetQty { get; set; }
        }
        #endregion

        #region 首页图标显示数据 ERP当月每天的销售金额
        public JsonResult Get_TheMonth_DailySales_Interface()
        {
            try
            {
                List<Object> obj = _IERP_SASalAinfoDao.Get_TheMonth_DailySales();
                return Json(new { result = "success", msg = "", data = obj }); ;
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

    }
}
