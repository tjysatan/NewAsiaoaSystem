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

namespace NewAsiaOASystem.Web.Controllers
{
    public class Flow_NonSProductinfoController : Controller
    {
        //
        // GET: /Flow_NonSProductinfo/
        //IFlow_PlanProductioninfoDao _IFlow_PlanProductioninfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanProductioninfoDao") as IFlow_PlanProductioninfoDao;
        IFlow_NonSProductinfoDao _IFlow_NonSProductinfoDao = ContextRegistry.GetContext().GetObject("Flow_NonSProductinfoDao") as IFlow_NonSProductinfoDao;
        IFlow_PlanProductioninfoDao _IFlow_PlanProductioninfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanProductioninfoDao") as IFlow_PlanProductioninfoDao;
        IFlow_PleasepurchaseinfoDao _IFlow_PleasepurchaseinfoDao = ContextRegistry.GetContext().GetObject("Flow_PleasepurchaseinfoDao") as IFlow_PleasepurchaseinfoDao;
        IFlow_PlanPPrintinfoDao _IFlow_PlanPPrintinfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanPPrintinfoDao") as IFlow_PlanPPrintinfoDao;
        IFlow_RoutineStockinfoDao _IFlow_RoutineStockinfoDao = ContextRegistry.GetContext().GetObject("Flow_RoutineStockinfoDao") as IFlow_RoutineStockinfoDao;
        IFlow_AjaxtxdateDao _IFlow_AjaxtxdateDao = ContextRegistry.GetContext().GetObject("Flow_AjaxtxdateDao") as IFlow_AjaxtxdateDao;

        public ActionResult Index()
        {
            return View();
        }

        //非标销售页面管理页面
        public ActionResult list()
        {
            return View();
        }

        #region //保存
        [HttpPost]
        public JsonResult Edit(Flow_NonSProductinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    Flow_NonSProductinfoView newmodel = _IFlow_NonSProductinfoDao.Getmodelbywldm(model.Pbianma);
                    if (newmodel != null)
                    {
                        flay = _IFlow_NonSProductinfoDao.NUpdate(newmodel);
                    }
                    else
                    {
                        model.C_time = DateTime.Now;//记录创建时间
                        flay = _IFlow_NonSProductinfoDao.Ninsert(model);
                    }
                }
                else
                {
                    flay = _IFlow_NonSProductinfoDao.NUpdate(model);
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
            return View("Edit", new Flow_NonSProductinfoView());
        }
        #endregion

        //查找非标产品信息
        public string AJxaGetNonsdataJson(string Sort, string Category, string wldm, string Pname)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_IFlow_NonSProductinfoDao.GetNonsdata(Sort,Category,wldm,Pname));
            return json;
        }

        //库存查看页面
        public ActionResult KcckView(string wldm)
        {
            DataTable dt = GetKcsum("'"+wldm+"'");
            string P_Bianhao = wldm;
            string count = "0.00";
            if (dt.Rows.Count > 0)
            {
                P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
            }
            Flow_NonSProductinfoView model = _IFlow_NonSProductinfoDao.Getmodelbywldm(wldm);
            ViewData["wldm"] = P_Bianhao;
            ViewData["kcsl"] = count;
            ViewData["cpname"] = model.Pname;
            ViewData["pmodel"] = model.Pmodel;
            return View();
        }

        //检测产品是否存在库存并且返回库存数量及对应的K3 物料代码
        public DataTable GetKcsum(string P_bianhao)
        {
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            string Wldm = P_bianhao;//物料代码
            string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
            DataTable dt = Dsmodel.GetKuCun(Wldm, Key);
            return dt;
        }

        //非标生产模块
        /// <summary>
        /// 非标产品生产通知单 页面（生产计划单）
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <returns></returns>
        public ActionResult InsertSCPlan(string Id)
        {
            ViewData["Id"] = Id;//产品Id
            Flow_NonSProductinfoView model = _IFlow_NonSProductinfoDao.NGetModelById(Id);
            ViewData["pname"] = model.Pname;
            ViewData["pmodel"] = model.Pmodel;
            ViewData["Newdatetime"] = DateTime.Now.ToString("yyyMMdd");
            ViewData["p_type"] = model.Category;
            DataTable dt = GetKcsum("'" + model.Pbianma + "'");
            if (dt.Rows.Count > 0)
            {
                string P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                string count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
                ViewData["kcsl"] = count;
            }
            else
            {
                ViewData["kcsl"] = "0.00";
            }
            return View();
        }

        //保存生产计划
        /// <summary>
        /// 保存并且返回生产计划单的Id
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <param name="scsum">生产数量</param>
        /// <returns></returns>
        public string ReturnPId(string Id,string scsum,string p_type)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            Flow_NonSProductinfoView model = _IFlow_NonSProductinfoDao.NGetModelById(Id);
            Flow_PlanProductioninfoView planmodel = new Flow_PlanProductioninfoView();
            planmodel.C_name = user.Id;//创建人
            planmodel.C_time = DateTime.Now;//创建时间
            planmodel.Isprint =0;//默认没有打印
            planmodel.P_CPId = model.Id;//产品Id
            planmodel.P_CPname = model.Pname;//产品名称
            planmodel.P_Issc = 4;//待技术确认
            planmodel.P_Pianhao = model.Pbianma;//产品编号
            planmodel.P_SCnumber = Convert.ToDecimal(scsum);//要货数量
            planmodel.P_type = Convert.ToInt32(p_type);//生产计划单的种类 0 常规  1非标（销售） 2非标（工程） 3 试产
            planmodel.Status = 0;
            string pId = _IFlow_PlanProductioninfoDao.InsertID(planmodel);
            MassManager.FMb_ProductionOrderNotice(pId);
            string tzdtype;
            if (p_type == "1")
            {
                tzdtype = "0";
            }
            else
            {
                tzdtype = "1";
            }
            InsertAJAXtxdate("0",tzdtype);
            return pId;
        }

        //保存生产通知单
        /// <summary>
        /// 
        /// </summary>
        ///  <param name="scph1">生产批号年月日</param>
        /// <param name="scph1">生产批号年月日</param>
        /// <param name="scph2">生产批号后</param>
        /// <param name="yhsl">要货数量</param>
        /// <param name="Cusname">客户名称</param>
        /// <param name="yhdatetime">要货时间</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="DYdy">电源电源</param>
        /// <param name="yhxz">要货性质 0库存储备 1订单生产</param>
        /// <returns></returns>
        public string PrintPlanDATa(string pId, string scph1, string scph2, string yhsl, string Cusname, string yhdatetime, string cpname, string DYdy, string yhxz, string p_type, string kfbeizhu)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                bool flay = false;
                Flow_PlanPPrintinfoView model = new Flow_PlanPPrintinfoView();
                model.Yhdate = Convert.ToDateTime(yhdatetime);//要货时间
                //检测要货时间不能小于当前时间
                if (model.Yhdate <= DateTime.Now)
                {
                    return "3";//要货时间小于当前时间
                }
                string Plan_Id = ReturnPId(pId, yhsl, p_type);
                model.Plan_Id = Plan_Id;
                scph2 = GetcountPproduction();
                model.Scph = scph1 + scph2;//生产批号
                model.Scsl = yhsl;//要货数量
                model.Cusname = Cusname;//客户名称
             
                model.CPname = cpname;//产品名称
                model.DYDY = DYdy;//电源型号
                model.Yhxz = Convert.ToInt32(yhxz);
                model.Kfname = user.RName;
                model.Kfdatetime= DateTime.Now;
                model.kfBeizhu = kfbeizhu;
                flay = _IFlow_PlanPPrintinfoDao.Ninsert(model);
                if (flay)
                    return "1";//保存成功
                else
                    return "2";//保存失败
            }
            catch
            {
                return "0";//请求有问题
            }
        }

        /// <summary>
        /// 返回 当天生产批号的条数
        /// </summary>
        /// <returns></returns>
        public string GetcountPproduction()
        {
            int ts = _IFlow_PlanProductioninfoDao.GetPPcount();
            string ph;
            if (ts < 10)
            {
                ph = "0" + (ts + 1).ToString();
            }
            else
            {
                ph = (ts + 1).ToString();
            }
            return ph;
        }

        //保存生产通知单提醒信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">生产通知单状态</param>
        /// <param name="tzdtype">通知单类型</param>
        public void InsertAJAXtxdate(string type, string tzdtype)
        {
            Flow_AjaxtxdateView model = new Flow_AjaxtxdateView();
            model.Type = Convert.ToInt32(type);//当前的状态 0 待技术确认 1 待生产 2 已生产 3 缺料 4 完成
            model.Istz = 0;//提示是否 提示过 0 未提示 1已提示
            model.tzdtype = Convert.ToInt32(tzdtype);//生产通知单的类型 0 非标销售 1非标工程
            _IFlow_AjaxtxdateDao.Ninsert(model);//保存
        }

        //ajax调用 
        public string AJAXtxdatejson(string Type, string tzdtype)
        {
            string json;
            json = JsonConvert.SerializeObject(_IFlow_AjaxtxdateDao.GetWTZajaxtxdate(Type,tzdtype));
            return json;
        }

        #region //小批量试产生产通知单

        #region //小批量市场产品列表页面
        /// <summary>
        /// 试产产品列表管
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchProductionView()
        {
            return View();
        }
        /// <summary>
        /// 试产产品编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BPEideView()
        {
            return View("BPEideView", new Flow_NonSProductinfoView());
        }

        /// <summary>
        /// 小批量试产下单
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <returns></returns>
        public ActionResult BPSCPlanView(string Id)
        {
            ViewData["Id"] = Id;//产品Id
            Flow_NonSProductinfoView model = _IFlow_NonSProductinfoDao.NGetModelById(Id);
            ViewData["pname"] = model.Pname;
            ViewData["pmodel"] = model.Pmodel;
            ViewData["Newdatetime"] = DateTime.Now.ToString("yyyMMdd");
            ViewData["p_type"] = model.Category;
            DataTable dt = GetKcsum("'" + model.Pbianma + "'");
            if (dt.Rows.Count > 0)
            {
                string P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                string count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
                ViewData["kcsl"] = count;
            }
            else
            {
                ViewData["kcsl"] = "0.00";
            }
            return View();
        }
        #endregion

        #endregion


    }
}
