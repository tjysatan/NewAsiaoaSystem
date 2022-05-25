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
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class Flow_RoutineStockinfoController : Controller
    {
        //
        // GET: /Flow_RoutineStockinfo/
        //常规温控器接口
        IFlow_RoutineStockinfoDao _IFlow_RoutineStockinfoDao = ContextRegistry.GetContext().GetObject("Flow_RoutineStockinfoDao") as IFlow_RoutineStockinfoDao;
        //生产计划接口
        IFlow_PlanProductioninfoDao _IFlow_PlanProductioninfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanProductioninfoDao") as IFlow_PlanProductioninfoDao;
        //料单采购通知
        IFlow_ProductionNoticeinfoDao _IFlow_ProductionNoticeinfoDao = ContextRegistry.GetContext().GetObject("Flow_ProductionNoticeinfoDao") as IFlow_ProductionNoticeinfoDao;
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
        IDKX_RKZLDataInfoDao _iIDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;


        public ActionResult Index()
        {
            return View();
        }

        //常规销售的管理页面
        public ActionResult list()
        {
            return View();
        }
        //常规工程
        public ActionResult gclist()
        {
            return View();
        }


        #region //常规温控 管理ESOP
        public ActionResult wkcplist()
        {
            return View();
        }

        #region //温控图纸资料
        public ActionResult wkgxtzView(string Id)
        {
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Flow_RoutineStockinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    Flow_RoutineStockinfoView newmodel = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(model.P_Bianhao);
                    if (newmodel != null)//存在
                    {
                        newmodel.state = 0;
                        newmodel.Isscing = 0;
                        newmodel.type = 0;
                        flay = _IFlow_RoutineStockinfoDao.NUpdate(newmodel);
                    }
                    else
                    {
                        model.Isscing = 0;
                        model.state = 0;//是否启用 0 启用 1禁用
                        model.type = 0;
                        flay = _IFlow_RoutineStockinfoDao.Ninsert(model);
                    }
                }
                //修改
                else
                {
                    model.state = 0;//是否启用 0 启用 1禁用
                    model.type = 0;
                    flay = _IFlow_RoutineStockinfoDao.NUpdate(model);
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

        #region //增加跳转页面(常规销售产品)
        public ActionResult addPage()
        {
            return View("Edit", new Flow_RoutineStockinfoView());
        }
        #endregion

        #region //增加跳转页面（常规工程）
        public ActionResult addPagegc()
        {
            return View("addPagegc", new Flow_RoutineStockinfoView());
        }
        #endregion


        //查询常规产品的Json数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sort">排序</param>
        /// <param name="Category">产品种类 成品 半成品</param>
        /// <returns></returns>
        public string GetCgkcJson(string Sort,string Category,string cpname,string wlsort)
        {
            string Jsonstr;
            Jsonstr = JsonConvert.SerializeObject(_IFlow_RoutineStockinfoDao.GetxsinfobyOrderCode(Sort, Category,cpname, wlsort));
            return Jsonstr;
        }

        #region //常规销售温控器数据
        public ActionResult New_GetCgkcJson(string Sort, string Category, string cpname, string P_Model, string wlsort)
        {
            IList<Flow_RoutineStockinfoView> modellist = _IFlow_RoutineStockinfoDao.New_GetxsinfobyOrderCode(Sort, Category, cpname, P_Model, wlsort);
            var result = new
            {
                code = 0,
                msg = "",
                count = 100,
                data = modellist,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //更新库存信息
        public string Updateinventory(string type)
        {
            IList<Flow_RoutineStockinfoView> listmdeol = _IFlow_RoutineStockinfoDao.GetCpkcInfo("0");
            string P_Bianhaojson = "";
            for (int i = 0, j = listmdeol.Count; i < j; i++)
            {
                P_Bianhaojson = P_Bianhaojson + "'" + listmdeol[i].P_Bianhao + "',";
            }
            P_Bianhaojson = P_Bianhaojson.Substring(0, P_Bianhaojson.Length - 1);
            DataTable dt = GetKcsum(P_Bianhaojson);
            for (int a = 0, b = dt.Rows.Count; a < b; a++)
            {
                string P_Bianhao = dt.Rows[a]["code"].ToString();//物料代码
                decimal count = Convert.ToDecimal(dt.Rows[a]["count"]);//库存
                Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(P_Bianhao);
                model.N_Stock = count;//当天的及时库存
                model.A_Sum = count - model.W_Consumption;//报警库存
                //model.SC_Sum=
                if (model.A_Sum <= 0)
                {
                    model.Isbj = 1;//库存异常
                    model.SC_Sum = model.M_Consumption - model.A_Sum - count;//报警库存为负数（用月用量加上报警数的绝对值建现有库存）
                }
                else
                {
                    model.Isbj = 0;//库存正常
                    model.SC_Sum = 0;//库存未报警 需要生产0
                }
                _IFlow_RoutineStockinfoDao.NUpdate(model);
            }
            return "";
        }

        //检测产品是否存在库存并且返回库存数量及对应的K3 物料代码
        public DataTable GetKcsum(string P_bianhao)
        {
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            string Wldm =  P_bianhao;//物料代码
            string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
            DataTable dt = Dsmodel.GetKuCun(Wldm, Key);
            return dt;
        }

        public DataTable GetK3BOM(string P_bianhao)
        {
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            string Wldm = P_bianhao;//物料代码
            string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
            DataTable dt = Dsmodel.GetBom(Wldm, Key);
            return dt;
        }

        #endregion

        #region //20210804库存更新

        public string PsUpdateinventory(string type)
        {
            IList<Flow_RoutineStockinfoView> listmdeol = _IFlow_RoutineStockinfoDao.GetCpkcInfo("");//查询全部的温控常规产品
            foreach (var item in listmdeol)
            {
                Flow_RoutineStockinfoView model = item;
                string kcjson = GetPSKC(model.P_Bianhao);
                string wqjpjson = Get_PS_wqjpsum(model.P_Bianhao);
                decimal count = Convert.ToDecimal(kcjson);//库存
                model.N_Stock = count;//当天的及时库存
                model.A_Sum = count - model.W_Consumption;//报警库存
                if (model.A_Sum <= 0)
                {
                    model.Isbj = 1;//库存异常
                    model.SC_Sum = model.M_Consumption - model.A_Sum - count;//报警库存为负数（用月用量加上报警数的绝对值建现有库存）
                }
                else
                {
                    model.Isbj = 0;//库存正常
                    model.SC_Sum = 0;//库存未报警 需要生产0
                }
                _IFlow_RoutineStockinfoDao.NUpdate(model);
            }
            return "";
        }

        public JsonResult N_PsUpdateinventory(string type)
        {
            try
            {
                IList<Flow_RoutineStockinfoView> listmodel = _IFlow_RoutineStockinfoDao.GetCpkcInfo("");//查询全部的温控常规产品
                if (listmodel == null)
                    return Json(new { result = "error", res = "当前产品都在生产中" });
                foreach (var item in listmodel)
                {
                    Flow_RoutineStockinfoView model = item;
                    decimal kcjson = 0;
                    decimal wqjpsum = 0;
                    decimal OnOrder = 0;
                    psItmAzhinfomodel AA = Get_PS_psItmAzhinfo(model.P_Bianhao);
                    if (AA != null)
                    {
                        kcjson = Convert.ToDecimal(AA.OnHand);
                        OnOrder = Convert.ToDecimal(AA.OnOrder);
                        if (AA.QtySUM == null || AA.QtySUM == "null")
                            wqjpsum = 0;
                        else
                            wqjpsum = Convert.ToDecimal(AA.QtySUM);
                    }
                    decimal count = Convert.ToDecimal(kcjson);//库存
                    model.N_Stock = count;//当天的及时库存
                    model.A_Sum = count - model.W_Consumption + Convert.ToDecimal(wqjpsum);//报警库存
                    model.wjjpsum = Convert.ToDecimal(wqjpsum);//未清拣配单数量
                    model.sczdsum = Convert.ToDecimal(OnOrder);//生产预定量
                    if (model.A_Sum <= 0)
                    {
                        model.Isbj = 1;//库存异常
                        model.SC_Sum = model.M_Consumption - model.A_Sum - count;//报警库存为负数（用月用量加上报警数的绝对值建现有库存）
                    }
                    else
                    {
                        model.Isbj = 0;//库存正常
                        model.SC_Sum = 0;//库存未报警 需要生产0
                    }
                    _IFlow_RoutineStockinfoDao.NUpdate(model);
                }
                return Json(new { result = "success", res = "操作成功" });

            }
            catch (Exception x)
            {
                return Json(new { result = "error", res = x });
            }
        }

        //查询普实erp里面及时库存
        public string GetPSKC(string wlno)
        {
            string resjson = zypushsoftHelper.GetBCStkbyItmID(wlno);
            List<pskcmodel> timemodel = getObjectByJson<pskcmodel>(resjson);
            if (timemodel.Count() > 0)
            {
                return timemodel[0].OnHand;
            }
            else
            {
                return "0";
            }

        }

        //查询普实erp中未清拣配数量
        public string Get_PS_wqjpsum(string wlno)
        {
            string resjson = zypushsoftHelper.Get_SATmpAsum_by_ItmID(wlno);
            List<psjpsummodel> timemodel = getObjectByJson<psjpsummodel>(resjson);
            if (timemodel.Count() > 0)
            {
                if (timemodel[0].Qtysum == "null" || timemodel[0].Qtysum == null)
                    return "0";
                else
                    return timemodel[0].Qtysum;
            }
            else
            {
                return "0";
            }
        }

        #region //获取及时库存/生产在的/销售预约/拣配未清
        public psItmAzhinfomodel Get_PS_psItmAzhinfo(string wlno)
        {
            string resjson = zypushsoftHelper.Get_ItmeZHinfo_ItmID(wlno);
            if (resjson == "[]")
                return null;
            List<psItmAzhinfomodel> timemodel = getObjectByJson<psItmAzhinfomodel>(resjson);
            if (timemodel.Count() > 0)
            {
                return timemodel[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        public class pskcmodel
        {
            public virtual string ItmID { get; set; }

            /// <summary>
            /// 及时库存
            /// </summary>
            public virtual string OnHand { get; set; }
        }

        #region //拣配单数量
        public class psjpsummodel
        {
            public virtual string Qtysum { get; set; }
        }
        #endregion

        #region //及时库存/生产在的/销售预约/拣配未清
        public class psItmAzhinfomodel
        {
            public virtual string OnHand { get; set; }//及时库存

            public virtual string OnOrder { get; set; }//生产在定量

            public virtual string IsCommited { get; set; }//销售预约量

            public virtual string QtySUM { get; set; }//拣配未清量
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

        //用量修改
        public ActionResult UpdateConsumption(string Id)
        {
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);//根据ID查询实体信息
            return View(model);
        }

        #region //生产计划制定模块

        //生产计划页面
        public ActionResult PlanproductionView(string Id, string type)
        {
            ViewData["Id"] = Id;
            ViewData["type"] = type;
            return View();
        } 

        //生产计划单保存
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CpId">产品Id</param>
        /// <param name="Cpname">产品名称</param>
        /// <param name="Cp_bianhao">产品编号</param>
        /// <param name="scsl">生产数量</param>
        /// <returns></returns>
        public string InsertPlanproduction(string CpId, string Cpname, string Cp_bianhao, string scsl)
        {
           SessionUser user = Session[SessionHelper.User] as SessionUser;
            Flow_RoutineStockinfoView modelcp = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(Cp_bianhao);
            Flow_PlanProductioninfoView model = new Flow_PlanProductioninfoView();
            //查询物料信息
            model.P_CPId = CpId;//需要生产的产品的Id
            model.P_CPname = Cpname;//需要生产的产品名称
            model.P_Pianhao = Cp_bianhao;//需要生产的产品物料代码
            model.P_Model = modelcp.P_Model;
            model.P_SCnumber = Convert.ToDecimal(scsl);//计划生产的数量
            model.P_Issc = 2;//待生产中  0生产中  1缺料中 2 待生产 3 完成
            model.C_time = DateTime.Now;//记录创建时间
            model.Nc_time = DateTime.Now;
            model.C_name = user.Id;//
            model.Status = 0;//记录状态
            model.P_type = 0;//常规
            model.scbianhao = Getwkorderbianhao();
            model.orderbianhao = model.scbianhao;
            if (_IFlow_PlanProductioninfoDao.Ninsert(model))
            {
                locCp(CpId, 0, scsl);//锁定产品（保存实际生产的数量）
                return "0";//插入成功
            }
            else
            {
                return "1";//插入失败
            }
        }
        #endregion

        #region //生产温控的生产批号
        public string Getwkorderbianhao()
        {
            string Newdatestr = DateTime.Now.ToString("yyyyMMdd");
            string str;
            int sum = _IFlow_PlanProductioninfoDao.Getwkcount() + 1;
            if (sum < 10)
            {
                str = "0" + sum.ToString();
            }
            else
            {
                str = sum.ToString();
            }
            string bianhao = Newdatestr + str;
            return bianhao;
        }
        #endregion

        #region //公共调用模块
        //获取常规产品的信息json
        public string GetRoutineStockinfoJson(string Id)
        {
            string json = null;
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);
            json = JsonConvert.SerializeObject(model);
            return json;
        }
        #endregion

        //锁定和解锁产品的
        /// <summary>
        /// 锁定和解锁产品的
        /// </summary>
        /// <param name="CPId">产品名称</param>
        /// <param name="type">锁定 0和解锁 1</param>
        public void locCp(string CPId, int type, string scsl)
        {
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(CPId);
            if (type == 0) {//锁定产品
                model.Isscing = 1;
                model.Sjsc_Sum = Convert.ToDecimal(scsl);
                _IFlow_RoutineStockinfoDao.NUpdate(model);
            }
            else if (type == 1) {//解锁产品
                model.Isscing = 0;
                model.Sjsc_Sum = 0;
                _IFlow_RoutineStockinfoDao.NUpdate(model);
            }
        }

        //完成生产订单
        public string CompleteSc(string Id)
        {
            Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
            if (model != null)
            {

                model.P_Issc = 3;
                model.wcdatetime = DateTime.Now;
                _IFlow_PlanProductioninfoDao.NUpdate(model);
                if (model.P_type != 0)
                {
                    MassManager.FMb_ProductionOrderNotice(Id);
                }
                if (model.P_type == 0)//(常规的通知单)非标生产通知
                {
                    locCp(model.P_CPId, 1, null);
                }
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //常规生产产品的删除（禁用）
        public string DelcgscEide(string Id)
        {
            //Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.state = 1;
                bool flay = false;
                flay = _IFlow_RoutineStockinfoDao.NUpdate(model);
                if (flay)
                    return "0";//禁用成功
                else
                    return "2";//禁用失败
            }
            return "1";//禁用失败
        }

        #region //通过物料代理 查询K3基础数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bianma">物料编码</param>
        /// <returns></returns>
        public string GetK3infobybianma(string bianma)
        {
            K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(bianma);
            string json = null;
            json = JsonConvert.SerializeObject(model);
            return json;
        }
        #endregion


        #region //常规电控箱

        #region //常规电控箱列表 工程管理ESOP
        public ActionResult gcblist()
        {
            return View();
        }

        #region //工程查询常规电控箱(温控的)的分页数据（2021-05-14）
        public ActionResult Getcgdkxpage(int? page, int limit, string cpname, string wlno, string type,string category)
        {
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IFlow_RoutineStockinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IFlow_RoutineStockinfoDao.SetPagerPageSize(limit);
            PagerInfo<Flow_RoutineStockinfoView> listmodel = _IFlow_RoutineStockinfoDao.Getcgdiankongpagerlist(cpname, wlno,type, category);
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

        #region //工序图纸编辑页面(NAW)
        public ActionResult gxtzView(string Id,string Iswlw)
        {
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);
            ViewData["Iswlw"] = Iswlw;
            return View(model);
        }
        #endregion

 

        #region //工序图纸传
        [HttpPost]
        public JsonResult ziliaouploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["wlno"];
            string zl_type = Request.Form["gx"];
            foreach (var file in image)
            {
                DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/ESOP"), fileName);
                    file.SaveAs(filePath);
                    model.wjurl = "/Content/upload/ESOP/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = Convert.ToInt32(zl_type);
                    model.cpId = Id;
                    model.Start = 0;
                    model.Isgl = 0;
                    flay = _iIDKX_RKZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }
        #endregion
        #endregion

        //管理页面
        public ActionResult dkxlist()
        {
            return View();
        }

        #region //常规电控箱管理页面（客服）
        public ActionResult KFdkxlist()
        {
            return View();
        }
        #endregion

        //查询常规电控箱的json数据
        /// <summary>
        /// 查询常规电控箱的json数据
        /// </summary>
        /// <param name="Sort">告警数量排序</param>
        /// <param name="Category">0成品 1半成品</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="wlsort">物料代理排序</param>
        /// <returns></returns>
        public string DKXGetDATAJson(string Sort, string Category, string cpname, string wlsort)
        {
            string jsonstr = "";
            jsonstr = JsonConvert.SerializeObject(_IFlow_RoutineStockinfoDao.DKGetcgDATAinfo(Sort,Category,cpname,wlsort));
            return jsonstr;
        }


        #region //常规电控箱数据
        public ActionResult DKXGetADATAnopage(string Sort, string Category, string cpname, string wlsort)
        {
            IList<Flow_RoutineStockinfoView> modellist = _IFlow_RoutineStockinfoDao.DKGetcgDATAinfo(Sort, Category, cpname, wlsort);
            var result = new
            {
                code = 0,
                msg = "",
                count = 100,
                data = modellist,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //增加跳转页面（常规电控箱）
        /// <summary>
        /// 增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DKXAddPage()
        {
            return View(new Flow_RoutineStockinfoView());
        }
        [HttpPost]
        public JsonResult DKXEide(Flow_RoutineStockinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //新增
                if (string.IsNullOrEmpty(model.Id))
                {
                    Flow_RoutineStockinfoView newmodel = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(model.P_Bianhao);
                    if (newmodel != null)
                    {
                        newmodel.state = 0;//启用
                        newmodel.Isscing = 0;//正常
                        newmodel.type = 1;//电控箱
                        newmodel.Category = 0;
                        flay = _IFlow_RoutineStockinfoDao.NUpdate(newmodel);
                    }
                    else//不存在
                    {
                        model.Isscing = 0;
                        model.state = 0;//是否启用 0 启用 1禁用
                        model.type = 1;
                        model.Category = 0;
                        flay = _IFlow_RoutineStockinfoDao.Ninsert(model);
                    }
                }
                else
                {
                    model.state = 0;//是否启用 0 启用 1禁用
                    model.type = 1;
                    model.Category = 0;
                    flay = _IFlow_RoutineStockinfoDao.NUpdate(model);
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

        #region //用量修改(常规电控箱)
        /// <summary>
        /// 用量修改(常规电控箱)
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        public ActionResult DKXupdateConsumption(string Id)
        {
            //根据Id查询实体信息
            Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #region 常规电控箱 更新K3库存信息
        //常规电控箱 更新K3库存信息
        public string DKXUpdateInventory(string type)
        {
            IList<Flow_RoutineStockinfoView> listmodel = _IFlow_RoutineStockinfoDao.DKXGetCpkcInfo("0");
            string P_Bianhaojson = "";
            for (int i = 0, j = listmodel.Count; i < j; i++)
            {
                P_Bianhaojson = P_Bianhaojson + "'" + listmodel[i].P_Bianhao + "',";
            }
            P_Bianhaojson = P_Bianhaojson.Substring(0, P_Bianhaojson.Length - 1);
            DataTable dt = GetKcsum(P_Bianhaojson);
            for (int a = 0, b = dt.Rows.Count; a < b; a++)
            {
                string P_Bianhao = dt.Rows[a]["code"].ToString();//物料代码
                decimal count = Convert.ToDecimal(dt.Rows[a]["count"]);//库存
                Flow_RoutineStockinfoView model = _IFlow_RoutineStockinfoDao.Getmodelinfobyp_bianhao(P_Bianhao);
                model.N_Stock = count;//当天的及时库存
                model.A_Sum = count - model.W_Consumption;//报警库存
                if (model.A_Sum <= 0)
                {
                    model.Isbj = 1;//库存异常
                    model.SC_Sum = model.M_Consumption - model.A_Sum - count;//报警库存为负数（用月用量加上报警数的绝对值建现有库存）
                }
                else
                {
                    model.Isbj = 0;//库存正常
                    model.SC_Sum = 0;//库存未报警 需要生产0
                }
                _IFlow_RoutineStockinfoDao.NUpdate(model);
            }
            return "";
        }

        #endregion

        #region //常规电控箱 更新普实的库存信息
        public JsonResult PsDKXUpdateInventory(string type)
        {
            try
            {
                IList<Flow_RoutineStockinfoView> listmodel = _IFlow_RoutineStockinfoDao.DKXGetCpkcInfo("");
                if (listmodel == null)
                    return Json(new { result = "error", res = "当前产品都在生产中"});
                foreach (var item in listmodel)
                {
                    Flow_RoutineStockinfoView model = item;
                    //string kcjson = GetPSKC(model.P_Bianhao);
                    //string wqjpsum = Get_PS_wqjpsum(model.P_Bianhao);
                    decimal kcjson = 0;
                    decimal wqjpsum = 0;
                    decimal OnOrder = 0;
                    psItmAzhinfomodel AA = Get_PS_psItmAzhinfo(model.P_Bianhao);
                    if (AA != null)
                    {
                        kcjson = Convert.ToDecimal(AA.OnHand);
                        OnOrder = Convert.ToDecimal(AA.OnOrder);
                        if (AA.QtySUM == null || AA.QtySUM == "null")
                            wqjpsum = 0;
                        else
                            wqjpsum = Convert.ToDecimal(AA.QtySUM);
                    }
                    decimal count = Convert.ToDecimal(kcjson);//库存
                    model.N_Stock = count;//当天的及时库存
                    model.A_Sum = count - model.W_Consumption + Convert.ToDecimal(wqjpsum);//报警库存
                    model.wjjpsum = Convert.ToDecimal(wqjpsum);//未清拣配单数量
                    model.sczdsum = Convert.ToDecimal(OnOrder);//生产预定量
                    if (model.A_Sum <= 0)
                    {
                        model.Isbj = 1;//库存异常
                        model.SC_Sum = model.M_Consumption - model.A_Sum - count;//报警库存为负数（用月用量加上报警数的绝对值建现有库存）
                    }
                    else
                    {
                        model.Isbj = 0;//库存正常
                        model.SC_Sum = 0;//库存未报警 需要生产0
                    }
                    _IFlow_RoutineStockinfoDao.NUpdate(model);
                }
                return Json(new { result = "success", res = "操作成功" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", res = x });
            }
        }
        #endregion

        #region //常规电控的数据整理
        public JsonResult DKXdatezl()
        {
            try {
                IList<Flow_RoutineStockinfoView> listmodel = _IFlow_RoutineStockinfoDao.NGetList();
                foreach (var item in listmodel)
                {
                    K3_wuliaoinfoView cpmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(item.P_Bianhao);
                    if(cpmodel!=null)
                    { 
                    item.Iswlw = cpmodel.str2;
                    }
                    item.Bomno = k3bomnobywl(item.P_Bianhao);
                    _IFlow_RoutineStockinfoDao.NUpdate(item);
                }
                return Json(new { result = "success" });
            }
            catch
            {
                return Json(new { result = "error" });
            }

        }
        #endregion

        #region //根据物料代理查询单个产品的库存
        public string k3kuncunbywl(string wl)
        {
            try
            {
                string kc = "-";
                wl = "'" + wl + "'";
                DataTable dt = GetKcsum(wl);
                if (dt != null)
                {
                    decimal count = Convert.ToDecimal(dt.Rows[0]["count"]);//库存
                    kc = count.ToString();
                }
                return kc;
            }
            catch
            {
                return "";
            }

        }
        #endregion

        #region //根据物料号查询产品的BOM号
        public string k3bomnobywl(string wl)
        {
            try
            {

                DataTable dt = GetK3BOM(wl);
                if (dt != null)
                {
                    string BOMNO = dt.Rows[0]["BOM编号"].ToString();//库存
                    return BOMNO;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region //电控箱生产计划制定模块


        /// <summary>
        ///  //电控生产计划页面
        /// </summary>
        /// <param name="Id">产品数据的Id</param>
        /// <param name="type">当前产的类型</param>
        /// <returns></returns>
        public ActionResult DKXPlanproductionView(string Id, string type)
        {
            ViewData["Id"] = Id;
            ViewData["type"] = type;
            return View();
        }

        public string DKXInsertPlanproduction(string CPId, string Cpname, string Cp_bianhao,string p_model,string scsl)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            Flow_PlanProductioninfoView model = new Flow_PlanProductioninfoView();
            model.P_CPId = CPId;//产品Id
            model.P_CPname = Cpname;//产品名称
            model.P_Pianhao = Cp_bianhao;//产品的物料代码
            model.P_Model = p_model;//产品型号
            model.P_SCnumber = Convert.ToDecimal(scsl);//计划生产的数量
            model.P_Issc = 2;//待生产
            model.C_time = DateTime.Now;
            model.C_name = user.Id;
            model.Status = 0;
            model.P_type = 4;//常规电控
            model.scbianhao = Getorderbianhao();
            if (_IFlow_PlanProductioninfoDao.Ninsert(model))
            {
                locCp(CPId, 0, scsl);//锁定产品（保存实际生产的数量）
                return "0";//插入成功
            }
            else
            {
                return "1";
            }
        }

        //返回常规电控箱下单的编号
        //自动生产点单编号DKX20171030+01
        public string Getorderbianhao()
        {
            string Newdatestr = DateTime.Now.ToString("yyyyMMdd");
            string bianhao = "CGDKX" + Newdatestr + "-" + (_IFlow_PlanProductioninfoDao.GetDKXcount() + 1).ToString();
            return bianhao;
        }
        #endregion

        #region //常规电控箱完成生产订单
        public string DKXCompleteSc(string Id)
        {
            Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.P_Issc = 3;
                model.wcdatetime = DateTime.Now;
                _IFlow_PlanProductioninfoDao.NUpdate(model);
                if (model.P_type ==4)//(常规的通知单)非标生产通知
                {
                    locCp(model.P_CPId, 1, null);
                }
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //完成制图提交
        public string DKXwcscEide(string Id, string gw1, string gw2, string gw3, string gw4, string gw5, string gw6, string gw7, string gw8, string gw9, string gw10, string wctime)
        {
            try
            {
                Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.gwy = gw1;
                    model.gwe = gw2;
                    model.gws = gw3;
                    model.gwsi = gw4;
                    model.gww = gw5;
                    model.gwl = gw6;
                    model.gwq = gw7;
                    model.gwb = gw8;
                    model.gwj = gw9;
                    model.gwshi = gw10;
                    model.P_Issc = 3;
                    model.wcdatetime = Convert.ToDateTime(wctime);
                    //下单时间
                    DateTime xdtime = Convert.ToDateTime(model.C_time);
                    string xddata = xdtime.ToString("yyyy-MM-dd");
                    xdtime = Convert.ToDateTime(xddata);
                    if (xdtime>Convert.ToDateTime(wctime))
                        return "3";//完成时间不可以小于下单时间
                    _IFlow_PlanProductioninfoDao.NUpdate(model);
                    if (model.P_type == 4)//(常规的通知单)非标生产通知
                    {
                        locCp(model.P_CPId, 1, null);
                    }
                    return "2";

                }
                else
                {
                    return "1";//生产计划不存在
                }
            }
            catch
            {
                return "0";//
            }
        }
        #endregion

        #endregion

    }
}
