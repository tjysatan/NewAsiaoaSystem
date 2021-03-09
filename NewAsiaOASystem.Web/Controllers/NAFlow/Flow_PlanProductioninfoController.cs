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
    public class Flow_PlanProductioninfoController : Controller
    {
          IFlow_PlanProductioninfoDao _IFlow_PlanProductioninfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanProductioninfoDao") as IFlow_PlanProductioninfoDao;
          IFlow_PleasepurchaseinfoDao _IFlow_PleasepurchaseinfoDao = ContextRegistry.GetContext().GetObject("Flow_PleasepurchaseinfoDao") as IFlow_PleasepurchaseinfoDao;
          IFlow_PlanPPrintinfoDao _IFlow_PlanPPrintinfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanPPrintinfoDao") as IFlow_PlanPPrintinfoDao;
          IFlow_DKXPlanPPrintinfoDao _IFlow_DKXPlanPPrintinfoDao = ContextRegistry.GetContext().GetObject("Flow_DKXPlanPPrintinfoDao") as IFlow_DKXPlanPPrintinfoDao;
        //name:生产计划controller
        //author:tjy_satan
        // GET: /Flow_PlanProductioninfo/

          //分页列表页面
          #region //列表以及查询页面
          #region //分页列表页面
          public ActionResult List(int? pageIndex)
          {
              PagerInfo<Flow_PlanProductioninfoView> listmodel = GetListPager(pageIndex,null,null,"2",null,null,"0,1,2,3");
              return View(listmodel);
          }
          #endregion
          //条件查询
          #region //条件查询
          public JsonResult SearchList()
          {
              string Name = Request.Form["CPname"];//产品名称
              string wlbianhao=Request.Form["wl_bm"];//物料编号
              string Isscing=Request.Form["Isscing"];//状态
              string starttime = Request.Form["starttime"];//下单开始时间
              string endtime = Request.Form["endtime"];//下单结束时间
              string p_type = Request.Form["p_type"];
              int? pageIndex = 1;
              if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                  pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
              PagerInfo<Flow_PlanProductioninfoView> listmodel = GetListPager(pageIndex, Name, wlbianhao, Isscing, starttime, endtime, p_type);
              string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
              if (listmodel != null)
                  return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
              else
                  return Json(new { result = "", PageN = PageNavigate });
          }
          #endregion
          #region //分页数据
          private PagerInfo<Flow_PlanProductioninfoView> GetListPager(int? pageIndex, string Name, string wl_bm, string Isscing, string starttime, string endtime,string p_type)
          {
              SessionUser Suser = Session[SessionHelper.User] as SessionUser;
              int CurrentPageIndex = Convert.ToInt32(pageIndex);
              if (CurrentPageIndex == 0)
                  CurrentPageIndex = 1;
              _IFlow_PlanProductioninfoDao.SetPagerPageIndex(CurrentPageIndex);
              _IFlow_PlanProductioninfoDao.SetPagerPageSize(11);
              PagerInfo<Flow_PlanProductioninfoView> listmodel = _IFlow_PlanProductioninfoDao.GetCinfoList(Name,wl_bm,Isscing,starttime,endtime,p_type);
              return listmodel;
          }
          #endregion
          #endregion
          #region //查看BOM料单以及库存信息

          //调用产品BOM以及库存
          public string GetBOMKCinfo(string wl_hm)
          {
              string json;
              json = JsonConvert.SerializeObject(GetKcsum(wl_hm));
              return json;
          }


          //返回K3BOM以及库存
          public DataTable GetKcsum(string P_bianhao)
          {
              Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
              string Wldm = P_bianhao;//物料代码
              string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
              DataTable dt = Dsmodel.GetBom(Wldm, Key);
              return dt;
          }


          //物料查看页面
        /// <summary>
          /// 物料查看页面
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <param name="scNumber">生产数量</param>
        /// <returns></returns>
          public ActionResult BOMView(string Id,string scNumber,string wl_dm)
          {
              ViewData["cpId"] = Id;
              ViewData["scSL"] = scNumber;
              ViewData["wl_dm"] = wl_dm;
              return View();
          } 
          #endregion

          #region //采购单模块
          //元器件采购数量设置页面
          public ActionResult YQJ_gcSLView(string P_Id, string YQJ_BM, string YQJ_name, string sl)
          {
              ViewData["P_Id"] = P_Id;//生产计划单Id
              ViewData["YQJ_BM"] = YQJ_BM;//元器件的物料代码
              ViewData["YQJ_name"] = YQJ_name;//元器件名称
              ViewData["sl"] = sl;//缺少的数量
              return View();
          }

          //叠加采购数量设置页面
          public ActionResult DjYQJ_gcSLView(string P_Id, string YQJ_BM, string YQJ_name, string sl)
          {
              ViewData["P_Id"] = P_Id;//生产计划单Id
              ViewData["YQJ_BM"] = YQJ_BM;//元器件的物料代码
              ViewData["YQJ_name"] = YQJ_name;//元器件名称
              ViewData["sl"] = sl;//缺少的数量
              return View();
          }

          //下元器件请购单
          public string Yqj_REQ(string P_Id, string YQJ_BM, string YQJ_name, string sl,string cgy)
          {
              Flow_PleasepurchaseinfoView model = new Flow_PleasepurchaseinfoView();
              model.P_Id = P_Id;//采购计划Id
              model.Yqj_bianhao = YQJ_BM;
              model.Yqj_Name = YQJ_name;
              model.Cg_shuliang = Convert.ToDecimal(sl);
              model.C_time = DateTime.Now;//创建时间
              model.cgy = Convert.ToInt32(cgy);
              if (_IFlow_PleasepurchaseinfoDao.Ninsert(model))
              {
                  return "0";
              }
              else
              {
                  return "1";
              }
          }

          //检测请购单
          public string JCISCZYqj_REQ(string P_Id, string YQJ_BM)
          {
              if (_IFlow_PleasepurchaseinfoDao.Jcqgd(P_Id, YQJ_BM) == false)
              {
                  return "0";//可以下单
              }
              else
              {
                  return "1";//重复下单
              }
          }

          //检测是否存在未处理的相同的采购单
          public string checkqgd(string YQJ_BM)
          {
              if (_IFlow_PleasepurchaseinfoDao.checkqgdbywlbm(YQJ_BM) == false)
              {
                  return "0";//可以下单
              }
              else
              {
                  return "1";//重复下单
              }
          } 

          //根据物料代码查询未处理采购单
          public string QgdJson(string YQJ_BM)
          {
              string Json = null;
              Json = JsonConvert.SerializeObject(_IFlow_PleasepurchaseinfoDao.Getqgdmodelbynm(YQJ_BM));
              return Json;
          }

          //叠加采购
          public string Yqj_Insertdj(string Id, string zcsl)
          {
              Flow_PleasepurchaseinfoView model =_IFlow_PleasepurchaseinfoDao.NGetModelById(Id);
              model.Cg_shuliang = model.Cg_shuliang + Convert.ToDecimal(zcsl);
              if (_IFlow_PleasepurchaseinfoDao.NUpdate(model))
              {
                  return "0";
              }
              else
              {
                  return "1";
              }
          }
          
          #endregion

         
          //打印数据设置页面
          public ActionResult PrintPlanView(string Id)
          {
              ViewData["Newdatetime"] = DateTime.Now.ToString("yyyMMdd");
              ViewData["PlId"] = Id;
              Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
              ViewData["Yhsl"] = model.P_SCnumber;
              ViewData["Cpname"] = model.P_CPname;
              return View();
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="scph1">生产批号年月日</param>
          /// <param name="scph2">生产批号后</param>
          /// <param name="yhsl">要货数量</param>
          /// <param name="Cusname">客户名称</param>
          /// <param name="yhdatetime">要货时间</param>
          /// <param name="cpname">产品名称</param>
          /// <param name="DYdy">电源电源</param>
          /// <param name="yhxz">要货性质 0库存储备 1订单生产</param>
          /// <param name="kfqr">客服确认</param>
          /// <param name="PL">配料</param>
          /// <param name="GY">生产工艺</param>
          /// <returns></returns>
          public string PrintPlanDATa(string Plan_Id, string scph1, string scph2, string yhsl, string Cusname, string yhdatetime, string cpname, string DYdy, string yhxz, string kfqr, string PL, string GY,string jsqr)
          {
              try
              {
                  bool flay = false;
                  Flow_PlanPPrintinfoView model = new Flow_PlanPPrintinfoView();
                  model.Plan_Id = Plan_Id;
                  scph2 = GetcountPproduction();
                  model.Scph = scph1 + scph2;//生产批号
                  model.Scsl = yhsl;//要货数量
                  model.Cusname = Cusname;//客户名称
                  model.Yhdate = Convert.ToDateTime(yhdatetime);//要货时间
                  model.CPname = cpname;//产品名称
                  model.DYDY = DYdy;//电源型号
                  model.Yhxz = Convert.ToInt32(yhxz);
                  model.Kfname = kfqr;
                  model.Kfdatetime = DateTime.Now;
                  //检测要货时间不能小于当前时间
                  if (model.Yhdate <= DateTime.Now)
                  {
                      return "3";//要货时间小于当前时间
                  }
                  model.PLname = PL;//配料
                  model.Scgy = GY;//生产工艺
                  model.Jsname = jsqr;//技术确认
                  model.Jsdatetime = DateTime.Now;
                  flay = _IFlow_PlanPPrintinfoDao.Ninsert(model);
                  //Flow_PlanProductioninfoView Planmodel = _IFlow_PlanProductioninfoDao.NGetModelById(Plan_Id);
                  //Planmodel.Isprint = 1;
                  //_IFlow_PlanProductioninfoDao.NUpdate(Planmodel);
                  return "1";
              }
              catch
              {
                  return "0";
              }
          }
          
          //打印数据显示页面（）
          /// <summary>
          /// 
          /// </summary>
          /// <param name="Id">生产计划Id</param>
         
          /// <returns></returns>
          public ActionResult PlanPrint(string Id)
          {
           
            Flow_PlanProductioninfoView Planmodel = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
            if (Planmodel.Isprint != 1)
            {
                Planmodel.Isprint = 1;
                _IFlow_PlanProductioninfoDao.NUpdate(Planmodel);
            }

            Flow_PlanPPrintinfoView model = _IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);
             
              ViewData["scpj"] = model.Scph;//生产批号
              ViewData["yhsl"] = model.Scsl;//要货数量
              ViewData["Cusname"] = model.Cusname;//客户名称
              ViewData["yhdate"] = DateTime.Parse(model.Yhdate.ToString()).ToString("yyyyMMdd");//要货日期
              ViewData["cpname"] = model.CPname;//产品名称
              ViewData["Dydy"] = model.DYDY;//电源电压
              ViewData["Newdatetime"] = DateTime.Now.ToString("yyy年MM月dd日");
              if (model.Yhxz == 0)
              {
                  ViewData["yhxz"] = "库存储备";//要货性质
              }
              else
              {
                  ViewData["yhxz"] = "订单生产";//要货性质
              }
              ViewData["kfqr"] = model.Kfname;//客服query
              ViewData["kfdate"] = DateTime.Parse(model.Kfdatetime.ToString()).ToString("yyyyMMdd");//客户确认时间
              ViewData["Pl"] = model.PLname;//配料
              ViewData["GYname"] = model.Scgy;//生产工艺
              ViewData["JSQR"] = model.Jsname;//技术确认
              ViewData["jsdate"] = DateTime.Parse(model.Jsdatetime.ToString()).ToString("yyyyMMdd");//技术确认时间
              ViewData["kfBeizhu"] = model.kfBeizhu;
              ViewData["JsBeizhu"] = model.JsBeizhu;
              return View();
          }
          
          //打印数据
          public void PlandateJson(string plan_Id)
          {
             
              //string json;
              //json =JsonConvert.SerializeObject(_IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(plan_Id));
              //return json;
          }

          //客服生产计划通知单列表
          public ActionResult kfflistView(int? pageIndex)
          {
              PagerInfo<Flow_PlanProductioninfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, "1");
              return View(listmodel);
          }

          //技术生产通知单列表
          public ActionResult JsPplanlistView(int? pageIndex)
          {
              PagerInfo<Flow_PlanProductioninfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, "1,2,3");
              return View(listmodel);
          }

          //工程生产计划通知单列表
          public ActionResult GcPplanlistView(int? pageIndex)
          {
              PagerInfo<Flow_PlanProductioninfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, "2");
              return View(listmodel);
          }

         //技术编辑页面

          #region //技术编辑模块
          /// <summary>
          /// 技术部 生产通知单编辑页面
          /// </summary>
          /// <param name="Id">生产计划单Id</param>
          /// <returns></returns>
          public ActionResult PplanJsEdit(string Id)
          {
              //ViewData["PId"] = Id;//生产计划单
              Flow_PlanPPrintinfoView model = _IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);
              return View(model);
          }

          /// <summary>
          /// 技术编辑提交页面
          /// </summary>
          /// <param name="PPId">生产几乎Id</param>
          /// <param name="PNId"></param>
          /// <param name="PL"></param>
          /// <param name="GY"></param>
          /// <returns></returns>
          public string JsPPlanEdit(string PPId, string PNId, string PL, string GY, string jsbeizhu)
          {
              SessionUser user = Session[SessionHelper.User] as SessionUser;
              Flow_PlanProductioninfoView Pmodel = _IFlow_PlanProductioninfoDao.NGetModelById(PPId);
              Pmodel.P_Issc = 2;//待生产中
              _IFlow_PlanProductioninfoDao.NUpdate(Pmodel);
              Flow_PlanPPrintinfoView Nmodel = _IFlow_PlanPPrintinfoDao.NGetModelById(PNId);
              Nmodel.PLname = PL;//配料
              Nmodel.Scgy = GY;//生产工艺
              Nmodel.Jsname = user.RName;//技术人
              Nmodel.Jsdatetime = DateTime.Now;//技术确认时间
              Nmodel.JsBeizhu = jsbeizhu;
              bool flay = false;
              flay = _IFlow_PlanPPrintinfoDao.NUpdate(Nmodel);
              MassManager.FMb_ProductionOrderNotice(PPId);
              if (flay)
                  return "1";
              else
                  return "2";
          } 
          #endregion


          //生产管理编辑页面 （处理生产订单的状态,回复交期）

          public ActionResult PplanSCEdit(string Id)
          {
              ViewData["Id"] = Id;//生产订单的Id
              return View();
          }
          
        /// <summary>
        /// 订单状态（生产管理员）编辑
        /// </summary>
        /// <param name="Id">生产订单Id</param>
        /// <param name="p_type">状态</param>
        /// <param name="yjdatetime">预计生产完成时间</param>
        /// <returns></returns>
        public string PplanSCEditUPdate(string Id, string p_type, string yjdatetime)
          {
              Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
            if (model.P_Issc == 3)
            {
                return "3";
            }
            else { 
              model.P_Issc = Convert.ToInt32(p_type);
              if (yjdatetime != null&&yjdatetime!="")
              {
                  model.YJdatetime = Convert.ToDateTime(yjdatetime);
              }
              bool flay = false;
              flay = _IFlow_PlanProductioninfoDao.NUpdate(model);
              MassManager.FMb_ProductionOrderNotice(Id);
              if (flay)
                  return "1";
              else
                  return "2";
            }
        }

        /// <summary>
        /// 返回 当天生产批号的条数
        /// </summary>
        /// <returns></returns>
         public string  GetcountPproduction()
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

        #region //生产计划删除（工程和客服）

         public string DeletePplanGS(string Id)
         {
             Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);//查询生产计划
             if (model != null)//该生产计划存在
             {
                 Flow_PlanPPrintinfoView Primodel = _IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);//查询打印信息
                 if (Primodel != null)
                 {
                     _IFlow_PlanPPrintinfoDao.NDelete(Primodel);//删除生产计划打印信息
                 }
                 _IFlow_PlanProductioninfoDao.NDelete(model);//删除生产计划
                 return "0";//删除成功
             }
             else
             {
                 return "1";//删除失败
             }
         }
        #endregion

        #region //生产计划单变更（工程和客服）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">生产计划Id</param>
        /// <returns></returns>
         public ActionResult UpdatePplanGCView(string Id)
         {
             Flow_PlanPPrintinfoView model = _IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);//查询生产通知单
             return View(model);
         }

         public string UpdatePplanEide(string yhsl, string khname, string yhdate, string dydy, string yhxz, string bz,string Id,string ppid)
         {
             Flow_PlanProductioninfoView Pmodel = _IFlow_PlanProductioninfoDao.NGetModelById(ppid);//查询生产计划信息
             if (Pmodel != null)//存在生产计划
             {
                 if (Pmodel.P_Issc == 4)
                 { //当前是 待技术审核状态
                     Pmodel.P_SCnumber = Convert.ToDecimal(yhsl);//修改计划单中的生产数量
                     _IFlow_PlanProductioninfoDao.NUpdate(Pmodel);
                     Flow_PlanPPrintinfoView prinmodel = _IFlow_PlanPPrintinfoDao.NGetModelById(Id);
                     prinmodel.Scsl = yhsl;//要货数量
                     prinmodel.Kfname = khname;//客户名称
                     prinmodel.Yhdate = Convert.ToDateTime(yhdate);//要货时间
                     prinmodel.DYDY = dydy;//电源电压
                     prinmodel.Yhxz = Convert.ToInt32(yhxz);//要货性质
                     prinmodel.kfBeizhu = bz;//备注
                     _IFlow_PlanPPrintinfoDao.NUpdate(prinmodel);
                     return "1";//修改成功
                 }
                 else
                 {
                     return "0";//没有权限修改
                 }
             }
             else
             {
                 return "1";
             }
         }
        #endregion


        #region //生成计划单的详情页面查看（客服、工程、技术）
        public ActionResult SCPPView(string Id)
        {
            Flow_PlanPPrintinfoView model = _IFlow_PlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);
            return View(model);
        }
        #endregion


        #region //常规电控箱

        #region //常规电控箱的生产
        public ActionResult DKXList(int? pageIndex)
         {
             PagerInfo<Flow_PlanProductioninfoView> listmodel = DKXGetlistPager(pageIndex,null,null,"2",null,null,null,null);
             return View(listmodel);
         }
        #endregion



        #region //条件查询
         public JsonResult DKXSearchList()
         {
             string Name = Request.Form["CPname"];//产品名称
             string wlbianhao = Request.Form["wl_bm"];//物料编号
             string Isscing = Request.Form["Isscing"];//状态
             string starttime = Request.Form["starttime"];//下单开始时间
             string endtime = Request.Form["endtime"];//下单结束时间
             string p_type = Request.Form["p_type"];
             string scbianhao = Request.Form["scbianhao"];//生产编号
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<Flow_PlanProductioninfoView> listmodel = DKXGetlistPager(pageIndex, Name, wlbianhao, Isscing, starttime, endtime, p_type, scbianhao);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }
         #endregion

        #region //常规电控箱的分页数据
        /// <summary>
         /// 常规电控箱的分页数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="Name">产品名称</param>
        /// <param name="wl_bm">物料代码</param>
        /// <param name="Isscing">当前状态</param>
        /// <param name="starttime">下单时间</param>
        /// <param name="endtime"></param>
        /// <param name="p_type">类型 4 常规电控箱</param>
        /// <returns></returns>
         private PagerInfo<Flow_PlanProductioninfoView> DKXGetlistPager(int? pageIndex, string Name, string wl_bm, string Isscing, string starttime, string endtime, string p_type,string scbianhao)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IFlow_PlanProductioninfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IFlow_PlanProductioninfoDao.SetPagerPageSize(11);
             PagerInfo<Flow_PlanProductioninfoView> listmodel = _IFlow_PlanProductioninfoDao.DKXGetPDATAPagerlist(Name, wl_bm, Isscing, starttime, endtime, p_type,scbianhao);
             return listmodel;
         }
        #endregion

        #region //常规电控箱的生产任务单的打印
        //打印数据设置页面
         public ActionResult DKXPrintPlanView(string Id)
         {
             ViewData["PId"] = Id;//生产计划单
             Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
             ViewData["yhsl"]=model.P_SCnumber;//生产数量
             ViewData["cpname"]=model.P_CPname;//产品名称
             ViewData["cpmodel"] = model.P_Model;//产品型号 功率
             ViewData["DHtime"] = model.C_time;//订货时间
             return View();
         }

        //常规电控箱的任务单数据
         public string DKXPrintPlanDATa(string plan_Id, string scsl, string khname, string cpname, string cpxh, string xdname, string jdname, string khyaoqiu,string DHRQ)
         {
             try
             {
                 bool flay = false;
                 Flow_DKXPlanPPrintinfoView model = new Flow_DKXPlanPPrintinfoView();
                 model.Plan_Id = plan_Id;//生产计划Id
                 model.Scph = "";
                 model.Scsl = Convert.ToDecimal(scsl);//生产数量
                 model.Cusname = khname;//客户名称
                 model.CPname = cpname;//产品名称
                 model.Dw = cpxh;//产品型号
                 model.Xdname = xdname;//下单人
                 model.Jdname = jdname;//接单人
                 model.Beizhu = khyaoqiu;
                 model.C_datetime = Convert.ToDateTime(DHRQ);
                 flay = _IFlow_DKXPlanPPrintinfoDao.Ninsert(model);
                 Flow_PlanProductioninfoView Planmodel = _IFlow_PlanProductioninfoDao.NGetModelById(plan_Id);
                 Planmodel.Isprint = 1;
                 _IFlow_PlanProductioninfoDao.NUpdate(Planmodel);
                 return "1";
             }
             catch
             {
                 return "0";
             }
         }

        //打印数据直接显示页面
         public ActionResult DKXPlanPeint(string Id)
         {
             Flow_DKXPlanPPrintinfoView model = _IFlow_DKXPlanPPrintinfoDao.GetFlow_PlanprinmodelbypId(Id);
             ViewData["DHRQ"] = model.C_datetime;
             ViewData["GL"] = model.Dw;
             ViewData["CUSNAME"] = model.Cusname;
             ViewData["CPNAME"] = model.CPname;
             ViewData["SL"] = model.Scsl;
             ViewData["BEIZHU"] = model.Beizhu;
             ViewData["XDNAME"] = model.Xdname;
             ViewData["JDNAME"] = model.Jdname;
             return View();

         }
        #endregion

        #region //常规电控箱生产关联编辑页面（生产订单的状态和回复交期）
        //编辑页面
         public ActionResult DKXPlanSCEideView(string Id)
         {
             ViewData["Id"] = Id;//生产订单Id
             return View();
         }
        //编辑提交
         public string DKXPlanSCEide(string Id, string p_type, string yjdatetime)
         {
             Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(Id);
             model.P_Issc = Convert.ToInt32(p_type);
             if (yjdatetime != null && yjdatetime != "")
             {
                 model.YJdatetime = Convert.ToDateTime(yjdatetime);
             }
             bool flay = false;
             flay = _IFlow_PlanProductioninfoDao.NUpdate(model);
             if (flay)
                 return "1";
             else
                 return "2";
         }
        #endregion

        #region //常规电控箱生产完成页面 填写各个工位的人名单
        //完成制图的页面
         public ActionResult DKXwcscView(string Id)
         {
             ViewData["Id"]=Id;
             return View();
         }
        //完成提交方法 在 Flow_RoutineStockinfo 控制器中

         //获取当亲服务器的时间
         public string GetNewdatetimejson()
         {
             DateTime Time = new DateTime();
             Time = DateTime.Now;
             string flay = Time.ToShortDateString();
             return flay;
         }
        #endregion

        #region //电控箱各个工位生产人员查看
         public ActionResult DKXSCscyyckView(string Id)
         {
             ViewData["Id"] = Id;
             return View();
         }

        //查询生产计划的数据
         public string DKXscjhInfojson(string Id)
         {
             try
             {
                 string json = null;
                 json = JsonConvert.SerializeObject(_IFlow_PlanProductioninfoDao.NGetModelById(Id));
                 return json;
             }
             catch
             {
                 return null;
             }
         }
        
        #endregion
        #endregion

    }
}
