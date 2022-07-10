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
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Xml;
using System.Data.OleDb;

namespace NewAsiaOASystem.Web.Controllers
{
    public class DKX_DDController : Controller
    {
        //
        // GET: /DKX_DD/
        IDKX_DDtypeinfoDao _IDKX_DDtypeinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDtypeinfoDao") as IDKX_DDtypeinfoDao;
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
        IDKX_RKZLDataInfoDao _IDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;
        IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        IDKX_CPInfoDao _IDKX_CPInfoDao = ContextRegistry.GetContext().GetObject("DKX_CPInfoDao") as IDKX_CPInfoDao;
        INA_MRPmainDao _INA_MRPmainDao = ContextRegistry.GetContext().GetObject("NA_MRPmainDao") as INA_MRPmainDao;
        INA_MRPdetailedDao _INA_MRPdetailedDao = ContextRegistry.GetContext().GetObject("NA_MRPdetailedDao") as INA_MRPdetailedDao;
        IDKX_k3BominfoDao _IDKX_k3BominfoDao = ContextRegistry.GetContext().GetObject("DKX_k3BominfoDao") as IDKX_k3BominfoDao;
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
        IDKX_pjgdbinfoDao _IDKX_pjgdbinfoDao = ContextRegistry.GetContext().GetObject("DKX_pjgdbinfoDao") as IDKX_pjgdbinfoDao;
        INA_XSqitainfoDao _INA_XSqitainfoDao = ContextRegistry.GetContext().GetObject("NA_XSqitainfoDao") as INA_XSqitainfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        IDKX_GCSinfoDao _IDKX_GCSinfoDao = ContextRegistry.GetContext().GetObject("DKX_GCSinfoDao") as IDKX_GCSinfoDao;

        #region //平保标记工程资料异常
        public ActionResult GczlycView(string Id)
        {
            ViewData["OID"] = Id;
            return View();
        } 

        
        public JsonResult ModifyGczlycbyorderId(string Id,string ycyy)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
                if(ddmodel==null)
                    return Json(new { result = "error", msg = "订单不存在！" });
                if(ddmodel.DD_ZT==3||ddmodel.DD_ZT==4||ddmodel.DD_ZT==6 || ddmodel.DD_ZT == 7)
                    return Json(new { result = "error", msg = "当前订单的状态不可以操作工程资料异常！" });
                ddmodel.gczl_Isyc = 1;
                ddmodel.gczl_yctime = DateTime.Now;
                ddmodel.gczl_ycyy = ycyy;
                ddmodel.pbshzt = 1;//品审通过
                ddmodel.pbshtime = DateTime.Now;
                ddmodel.pdshuserId = user.Id;
                ddmodel.DD_ZT = 7;//进入待发货状态
                if (_IDKX_DDinfoDao.NUpdate(ddmodel))
                {
                    NAHelper.Insertczjltew(Id,"工程资料异常", user.Id,ddmodel.DD_Bianhao,ddmodel.Gcs_nameId,null,null, ycyy, "1");
                    return Json(new { result = "success", msg = "操作成功！" });
                }
                else
                {
                    return Json(new { result = "error", msg = "操作失败！" });
                }
            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //工程师工程资料异常处理

        #region //查询异常的的数量
        public JsonResult Getgczlyccount()
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                int gczlyccount = 0;
                gczlyccount = _IDKX_DDinfoDao.GetgczlycsumDate(Suser);
                return Json(new { result = "success", res = gczlyccount });
            }
            catch
            {
                return Json(new { result = "error", res = "0" });
            }
        }
        #endregion

        #region //异常整个的分页数据
        /// <summary>
        ///
        /// </summary>
        /// <param name="IsEdit">是否需要编辑按钮 false 就是需要 true或者空就是不需要</param>
        /// <returns></returns>
        public ActionResult gczlyclist(string IsEdit)
        {
            ViewData["IsEdit"] = IsEdit;//
            return View();
        }
        public ActionResult Getgczlyclist(int? page, int limit, string DD_Bianhao)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(limit);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetgczlycPagerlistdate(DD_Bianhao, Suser);

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

        #region //异常数据编辑页面
        public ActionResult gczlycBJView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        #region //异常整改提交
        public JsonResult Modifygczlyczg(string Id)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
                if (ddmodel == null)
                    return Json(new { result = "error", msg = "订单不存在！" });
                ddmodel.gczl_Isyc = 0;
                ddmodel.gczl_ycyy = "";
                ddmodel.gczl_yccltime = DateTime.Now;
                if (ddmodel.DD_ZT == 8)
                {//订单已经完成，资料可以入库
                    //string RkRser = Newcprk(ddmodel);
                    string RkRser = CP_PutIn(ddmodel);
                    if (RkRser == "1")//产品库存在
                    {
                        ddmodel.Rkdatetime = DateTime.Now;//入库时间
                        ddmodel.Isrk = 2;//方案库存在
                    }
                    if (RkRser == "2")//入库
                    {
                        ddmodel.Rkdatetime = DateTime.Now;//入库时间
                        ddmodel.Isrk = 1;//第一次入方案库
                    }
                   
                }
                if (_IDKX_DDinfoDao.NUpdate(ddmodel))
                {
                    NAHelper.Insertczjl(Id, "工程资料异常整改提交", user.Id);
                    return Json(new { result = "success", msg = "操作成功！" });
                }
                else
                {
                    return Json(new { result = "error", msg = "操作失败！" });
                }
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //新产品入库
        public string Newcprk(DKX_DDinfoView model)
        {
            try
            {
                if (model == null)
                    return "0";//订单走失
                DKX_CPInfoView cpmodel = _IDKX_CPInfoDao.Getcpdatabynameandpowanddw(model.DD_DHType, model.POWER, model.dw, model.gnjsstr);
                if (cpmodel != null)
                {
                    if (model.Ps_BomNO != null)
                        cpmodel.Ps_BomNO = model.Ps_BomNO;
                    if (model.Ps_sanduanno != null)
                        cpmodel.Ps_sanduanno = model.Ps_sanduanno;
                 
                    if (model.Ps_wlBomNO != null)
                        cpmodel.Ps_wlBomNO = model.Ps_wlBomNO;
                    if (model.Ps_Serialnumber != null)
                        cpmodel.Ps_Serialnumber = model.Ps_Serialnumber;
                    if (model.Ps_DocEntry != null)
                        cpmodel.Ps_DocEntry = model.Ps_DocEntry;
                    if (model.YJcb != null)
                        cpmodel.YJcb = model.YJcb;
                    _IDKX_CPInfoDao.NUpdate(cpmodel);
                    return "1";//已经存在改产品
                }
                else//如果不存在
                {
                    //保存产品
                    DKX_CPInfoView cpmode = new DKX_CPInfoView();
                    cpmode.Cpname = model.DD_DHType;//产品名称
                    cpmode.Power = model.POWER;//功率值
                    cpmode.DW = model.dw;//功率单位
                    cpmode.Gcs_name = model.Gcs_nameId;//工程师
                    cpmode.Type = model.DD_Type;//产品类型 0 小系统 1大系统 2物联网
                    cpmode.Isft = model.DD_WLWtype;//物联网的 0 一体 1分体
                    if (model.CONTROL_INFO_NO != "" && model.CONTROL_INFO_NO != null)
                    {
                        cpmode.COMPRESSOR_TYPE = model.COMPRESSOR_TYPE;
                        cpmode.CONTROL_INFO_NO = model.CONTROL_INFO_NO;
                    }

                    cpmode.Lytype = 0;//默认是报价系统
                    if (model.Ps_BomNO != null)
                        cpmode.Ps_BomNO = model.Ps_BomNO;
                    if (model.Ps_sanduanno != null)
                        cpmode.Ps_sanduanno = model.Ps_sanduanno;
                    if (model.Ps_wlBomNO != null)
                        cpmode.Ps_wlBomNO = model.Ps_wlBomNO;
                    if (model.Ps_Serialnumber != null)
                        cpmode.Ps_Serialnumber = model.Ps_Serialnumber;
                    if (model.Ps_DocEntry != null)
                        cpmode.Ps_DocEntry = model.Ps_DocEntry;
                    if (model.YJcb != null)
                        cpmode.YJcb = model.YJcb;
                    cpmode.Dd_Id = model.Id;//最初的订单
                    cpmode.RK_time = DateTime.Now;//产品名称
                    cpmode.IsDis = 0;
                    cpmode.cpgnjs = model.gnjsstr;//产品功能简述
                    cpmode.cplytype = 0;
                    if (model.DXTDJ != null)
                    {
                        cpmode.DXTDJ = model.DXTDJ;
                    }
                    else
                    {
                        cpmode.DXTDJ = 0;
                    }
                    string cpId = _IDKX_CPInfoDao.InsertID(cpmode);
                    //查询资料库
                    IList<DKX_ZLDataInfoView> zlmodellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(model.Id);
                    //循环插入资料
                    foreach (var item in zlmodellist)
                    {
                        //入库资料不要照片和验收单
                        if (item.Zl_type != 3 && item.Zl_type != 4)
                        {
                            DKX_RKZLDataInfoView rkzlmodel = new DKX_RKZLDataInfoView();
                            rkzlmodel.wjName = item.wjName;//附件名称
                            rkzlmodel.wjurl = item.url;//附件地址
                            rkzlmodel.Zl_type = item.Zl_type;//资料类型 0 需求 1 料单 2 图纸 3 照片 4 验收单
                            rkzlmodel.dd_Id = item.dd_Id;
                            rkzlmodel.cpId = cpId;//产品Id
                            rkzlmodel.Isgl = item.Isgl;//是否关联 0 附件 1关联报价系统 2 关联K3
                            rkzlmodel.Bjno = item.Bjno;
                            rkzlmodel.BomNo = item.Bjno;
                            rkzlmodel.C_time = DateTime.Now;
                            rkzlmodel.Start = 0;//启用
                            _IDKX_RKZLDataInfoDao.Ninsert(rkzlmodel);
                        }
                    }
                    return "2";
                }
            }
            catch
            {
                return "3";//异常
            }
        }
        #endregion

        #region //20220331 新产品信息入库或者更新BOM和图纸资料 （通过物料号，来判断是都是新产品）
        public string CP_PutIn(DKX_DDinfoView model)
        {
            try
            {
                if (model == null)
                    return "0";//订单走失
                DKX_CPInfoView cpmodel = _IDKX_CPInfoDao.Getcpdatebyps_wlno(model.Ps_wlBomNO);
                if (cpmodel != null)
                {
                    cpmodel.Cpname = model.DD_DHType;//产品名称
                    cpmodel.Power = model.POWER;//功率值
                    cpmodel.DW = model.dw;//功率单位
                    cpmodel.Gcs_name = model.Gcs_nameId;//工程师
                    cpmodel.Type = model.DD_Type;//产品类型 0 小系统 1大系统 2物联网
                    cpmodel.Isft = model.DD_WLWtype;//物联网的 0 一体 1分体
                    cpmodel.Dd_Id = model.Id;//最后的订单
                    cpmodel.RK_time = DateTime.Now;//最后更新入库的时间
                    //通过产品Id 查询 产品的资料明细并且删除
                    IList<DKX_RKZLDataInfoView> RKzllist = _IDKX_RKZLDataInfoDao.Get_ALLCPRKZL_BY_CpId(cpmodel.Id);
                    if (RKzllist != null)
                    {
                        //循环逻辑删除
                        foreach (var item in RKzllist)
                        {
                            item.Start = 1;
                            _IDKX_RKZLDataInfoDao.NUpdate(item);
                        }
                    }
                    //从新插入新的
                    //查询资料库
                    IList<DKX_ZLDataInfoView> zlmodellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(model.Id);
                    //循环插入资料
                    foreach (var item in zlmodellist)
                    {
                        //入库资料不要照片和验收单
                        if (item.Zl_type != 3 && item.Zl_type != 4)
                        {
                            DKX_RKZLDataInfoView rkzlmodel = new DKX_RKZLDataInfoView();
                            rkzlmodel.wjName = item.wjName;//附件名称
                            rkzlmodel.wjurl = item.url;//附件地址
                            rkzlmodel.Zl_type = item.Zl_type;//资料类型 0 需求 1 料单 2 图纸 3 照片 4 验收单
                            rkzlmodel.dd_Id = item.dd_Id;
                            rkzlmodel.cpId = cpmodel.Id;//产品Id
                            rkzlmodel.Isgl = item.Isgl;//是否关联 0 附件 1关联报价系统 2 关联K3
                            rkzlmodel.Bjno = item.Bjno;
                            rkzlmodel.BomNo = item.Bjno;
                            rkzlmodel.C_time = DateTime.Now;
                            rkzlmodel.Start = 0;//启用
                            _IDKX_RKZLDataInfoDao.Ninsert(rkzlmodel);
                        }
                    }
                    _IDKX_CPInfoDao.NUpdate(cpmodel);

                    return "1";
                }
                else//不存在
                {
                    DKX_CPInfoView cpmode = new DKX_CPInfoView();
                    cpmode.Cpname = model.DD_DHType;//产品名称
                    cpmode.Power = model.POWER;//功率值
                    cpmode.DW = model.dw;//功率单位
                    cpmode.Gcs_name = model.Gcs_nameId;//工程师
                    cpmode.Type = model.DD_Type;//产品类型 0 小系统 1大系统 2物联网
                    cpmode.Isft = model.DD_WLWtype;//物联网的 0 一体 1分体
                    if (model.CONTROL_INFO_NO != "" && model.CONTROL_INFO_NO != null)//是否关联报价单
                    {
                        cpmode.COMPRESSOR_TYPE = model.COMPRESSOR_TYPE;//报价单类型
                        cpmode.CONTROL_INFO_NO = model.CONTROL_INFO_NO;//报价单单号
                    }
                    cpmode.Lytype = 0;//默认是报价系统
                    if (model.Ps_BomNO != null)
                        cpmode.Ps_BomNO = model.Ps_BomNO;
                    if (model.Ps_sanduanno != null)
                        cpmode.Ps_sanduanno = model.Ps_sanduanno;
                    if (model.Ps_wlBomNO != null)
                        cpmode.Ps_wlBomNO = model.Ps_wlBomNO;
                    if (model.Ps_Serialnumber != null)
                        cpmode.Ps_Serialnumber = model.Ps_Serialnumber;
                    if (model.Ps_DocEntry != null)
                        cpmode.Ps_DocEntry = model.Ps_DocEntry;
                    if (model.YJcb != null)
                        cpmode.YJcb = model.YJcb;
                    cpmode.Dd_Id = model.Id;//最初的订单
                    cpmode.RK_time = DateTime.Now;//产品名称
                    cpmode.IsDis = 0;
                    cpmode.cpgnjs = model.gnjsstr;//产品功能简述
                    cpmode.cplytype = 0;
                    if (model.DXTDJ != null)
                    {
                        cpmode.DXTDJ = model.DXTDJ;
                    }
                    else
                    {
                        cpmode.DXTDJ = 0;
                    }
                    string cpId = _IDKX_CPInfoDao.InsertID(cpmode);//插入
                                                                   //查询资料库
                    IList<DKX_ZLDataInfoView> zlmodellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(model.Id);
                    //循环插入资料
                    foreach (var item in zlmodellist)
                    {
                        //入库资料不要照片和验收单
                        if (item.Zl_type != 3 && item.Zl_type != 4)
                        {
                            DKX_RKZLDataInfoView rkzlmodel = new DKX_RKZLDataInfoView();
                            rkzlmodel.wjName = item.wjName;//附件名称
                            rkzlmodel.wjurl = item.url;//附件地址
                            rkzlmodel.Zl_type = item.Zl_type;//资料类型 0 需求 1 料单 2 图纸 3 照片 4 验收单
                            rkzlmodel.dd_Id = item.dd_Id;
                            rkzlmodel.cpId = cpId;//产品Id
                            rkzlmodel.Isgl = item.Isgl;//是否关联 0 附件 1关联报价系统 2 关联K3
                            rkzlmodel.Bjno = item.Bjno;
                            rkzlmodel.BomNo = item.Bjno;
                            rkzlmodel.C_time = DateTime.Now;
                            rkzlmodel.Start = 0;//启用
                            _IDKX_RKZLDataInfoDao.Ninsert(rkzlmodel);
                        }
                    }
                    return "2";
                }
            }
            catch
            {
                return "3";//异常
            }
        }
        #endregion
        #endregion
        #endregion

        #region //非标电控箱MRP计算
        public JsonResult DKX_MRP_Count(string starttime, string endtime)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                if (starttime!=endtime)
                     return Json(new { result = "error", msg = "下单时间请选择一天！" });
                IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.Getorderlistbyxdtimeandddzt(starttime,endtime,"3");
                if(ddlist==null)
                    return Json(new { result = "error", msg = "当前下单时间没有需要计算的订单！" });
                int ordersum = ddlist.Count;
                NA_MRPmainView mainmodel = new NA_MRPmainView();
                mainmodel.state = 0;
                mainmodel.Ordertime =Convert.ToDateTime(Convert.ToDateTime(starttime).ToString("yyyy-MM-dd"));
                mainmodel.Ordersum = ordersum;
                mainmodel.c_userId = user.Id;
                mainmodel.C_username = user.RName;
                mainmodel.c_time = DateTime.Now;
                string Id = _INA_MRPmainDao.InsertID(mainmodel);//保存主表
                int sborder = 0;
                foreach (var item in ddlist)
                {
                    
                    string strjson = K3Helper.Getk3bombybomno(item.KBomNo);
                    if (strjson != "[]")
                    {
                        List<KBOMjsonmodel> timemodel = getObjectByJson<KBOMjsonmodel>(strjson);
                        foreach (var t in timemodel)
                        {
                            NA_MRPdetailedView mxmodel = new NA_MRPdetailedView();
                            mxmodel.dd_ddno = item.DD_Bianhao;
                            mxmodel.wlno = t.FNumber;
                            mxmodel.wlname = t.FItemName;
                            mxmodel.wlmodel = t.FModel;
                            mxmodel.MainId = Id;
                            mxmodel.qusum = item.NUM * Convert.ToDecimal(t.FAuxQty);
                            mxmodel.kcsum =Convert.ToDecimal(K3Helper.GetKcsum(t.FNumber));
                            _INA_MRPdetailedDao.Ninsert(mxmodel);
                        }
                    }
                    else
                    {
                        sborder = sborder + 1;
                    }
                }
                int cg = ordersum - sborder;
               return Json(new { result = "success", msg = "一共计算"+ cg+"条订单，失败："+sborder+"订单" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }


        #region //转换实体
        public class KBOMjsonmodel
        {
            public virtual string FEntryID { get; set; }

            public virtual string FInterID { get; set; }

            public virtual string FItemID { get; set; }

            public virtual string FNumber { get; set; }

            public virtual string FItemName { get; set; }

            public virtual string FModel { get; set; }

            public virtual string FBaseUnitID { get; set; }

            public virtual string FUnitID { get; set; }

            public virtual string FMaterielType { get; set; }

            public virtual string FMarshalType { get; set; }

            public virtual string FAuxQty { get; set; }

            public virtual string FUseState { get; set; }

            public virtual string FSPID { get; set; }

            public virtual string FStockID { get; set; }

            public virtual string FErpClsName { get; set; }

            public virtual string FNote { get; set; }

            public virtual decimal FOrderPrice { get; set; }
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

        #region //单据列表
        public ActionResult DKXMRPorderlist()
        {
            return View();
        }

        public ActionResult GetDKXMRPlist(int? page, int limit, string ordertime)
        {
          
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INA_MRPmainDao.SetPagerPageIndex(CurrentPageIndex);
            _INA_MRPmainDao.SetPagerPageSize(limit);
            PagerInfo<NA_MRPmainView> listmodel = _INA_MRPmainDao.GetNA_MRPorderlistpage(ordertime);

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

        #region //计算结果查看页面
        public ActionResult ShowMRPcountResult(string Id)
        {
            ViewData["MId"] = Id;
            return View();
        }

        #region //查询结果
        public JsonResult GetMRPResult(string MId)
        {
            try
            {
                IList<object> modellist = _INA_MRPdetailedDao.GetMRPresult(MId);
                string res = JsonConvert.SerializeObject(modellist);
                return Json(new { result = "success", msg = res });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //计算结果导出
        public FileResult ExcelExportDD(string MId)
        {
            IList<object> modellist = _INA_MRPdetailedDao.GetMRPresult(MId);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("物料");
            row1.CreateCell(2).SetCellValue("名称");
            row1.CreateCell(3).SetCellValue("规格型号");
            row1.CreateCell(4).SetCellValue("总用量");
            row1.CreateCell(5).SetCellValue("当前库存");
            row1.CreateCell(6).SetCellValue("缺少");
            int n = 0;
            for (int i = 0; i < modellist.Count; i++)
            {
                n = n + 1;
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                rowtemp.CreateCell(0).SetCellValue(n);//序号
                string fumber = ((object[])((object)modellist[i]))[0].ToString();
                string fname= ((object[])((object)modellist[i]))[1].ToString();
                string fmodel = ((object[])((object)modellist[i]))[2].ToString();
                string summ = ((object[])((object)modellist[i]))[3].ToString();
                string dqkc= ((object[])((object)modellist[i]))[4].ToString();
                var qssl = "0";
                qssl =Convert.ToString(Convert.ToDecimal(dqkc) - Convert.ToDecimal(summ));
                rowtemp.CreateCell(1).SetCellValue(fumber);//物料
                rowtemp.CreateCell(2).SetCellValue(fname);//名称
                rowtemp.CreateCell(3).SetCellValue(fmodel);//规格型号
                rowtemp.CreateCell(4).SetCellValue(summ);//总用量
                rowtemp.CreateCell(5).SetCellValue(dqkc);//当前库存
                rowtemp.CreateCell(6).SetCellValue(qssl);//缺少
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "MRP计算结果.xls");
        }
        #endregion
        #endregion

        #region //删除对应的订单
        public JsonResult Del_MRPorder(string Id)
        {
            try
            {
                //删除细表
                List<NA_MRPdetailedView> modellist = _INA_MRPdetailedDao.GetallMRPdetailedByMid(Id) as List<NA_MRPdetailedView>;
                if(modellist!=null)
                _INA_MRPdetailedDao.NDelete(modellist);
                //查询主表
                NA_MRPmainView model = _INA_MRPmainDao.NGetModelById(Id);
                if(_INA_MRPmainDao.NDelete(model))
                    return Json(new { result = "success", msg ="操作成功" });
                else
                    return Json(new { result = "error", msg = "删除失败" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion
        #endregion

        #region //报价系统的报价单下单
        public ActionResult DKXXXDbyBJView(string bjno,string bjorderno,string bjtype)
        {
            ViewData["bjno"] = bjno;
            ViewData["bjorderno"] = bjorderno;
            ViewData["bjtype"] = bjtype;
            AldkxtypeDropdown(null);//电控箱类型
            return View();
        }

        public JsonResult DKXXDEide(FrameController from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = new DKX_DDinfoView();
                string DD_Bianhao = Getorderbianhao();//订单编号
                string DD_Type = Request.Form["dkxtype"];//订单类型 0 大系统 1 小系统 2 物联网
                DateTime C_time = DateTime.Now;//创建时间
                string KHname = Request.Form["khname"];//客户名称
                string Lxname = Request.Form["lxrname"];//客户联系人
                string Tel = Request.Form["tel"];//联系电话
                string DD_DHType = Request.Form["ddxh"];//到货型号（自定义）
                string DD_WLWtype = Request.Form["DD_WLWtype"];//订单是 物联网的时候 选择物联网类型 0 一体 1分体
                string POWER = Request.Form["gl"];//功率
                string dw = Request.Form["dw"];//单位
                string cpph = Request.Form["cpph"];//产品批号
                decimal NUM = Convert.ToDecimal(Request.Form["sl"]);//下单数量
                string C_name = user.Id;//创建人
                string Gcs_nameId = Request.Form["gcs"];//工程师Id
                string REMARK = Request.Form["beizhu"];//备注
                string gnjsstr = Request.Form["gnjsstr"].Trim();//功能简述
             //   string zjsc = Request.Form["zjsc"];//是否直接生产 0 否 1是
                string yjjqtime = Request.Form["txt_startctime"];//预计交期
                string price = Request.Form["price"];
                string Ps_sanduanno = Request.Form["Ps_sanduanno"];//非标大类的编号
                string K3CODE = Request.Form["K3CODE"];
                string bjno = Request.Form["bjno"];//报价单号
                string bjorderno = Request.Form["bjorderno"];//报价单编号
                string bjtype = Request.Form["bjtype"];//报价单的机组型号
                model.DD_Type = Convert.ToInt32(DD_Type);
                model.dragstart = 0;//待助力检查
                model.C_time = C_time;
                DKX_DDtypeinfoView ddtypemodel = _IDKX_DDtypeinfoDao.Getdkxtypebytype(DD_Type);
                if (ddtypemodel.Issh == 1)
                {
                    model.Isdqsh = 1;//需要电气审核
                    model.dqshres = 3;//未提交审核
                }
                else
                {
                    model.Isdqsh = 0;//无需电气审核
                }
                //model.C_time = C_time;
                model.KHname = KHname;
                model.Lxname = Lxname;
                model.khkecode = K3CODE;
                if (price != "")
                {
                    model.price = Convert.ToDecimal(price);
                }
                model.Tel = Tel;
                model.DD_DHType = DD_DHType;
                model.DD_WLWtype = Convert.ToInt32(DD_WLWtype);
                model.POWER = POWER;
                model.NUM = NUM;
                model.C_name = C_name;
                model.Gcs_nameId = Gcs_nameId;
                model.cpph = cpph;//产品批号
                model.REMARK = QRHelper.ToDBC(REMARK);
                model.dw = dw;
                model.Isrk = 0;//未入方案库
                model.Start = 0;
                model.gcsfh = "";
                model.gnjsstr = gnjsstr;
                model.KBomNo = gnjsstr;//同步普实的时候作为产品的型号名称
                model.xtIsq = 0;
                model.qtIsq = 0;
                model.IsPdrefund = 0;//不是生产退单
                model.Ps_sanduanno = Ps_sanduanno;
                model.liushuitype = "B";
                if (yjjqtime != "")
                {
                    model.YJJQtime = Convert.ToDateTime(yjjqtime);
                }
               
                    model.DD_ZT =-1;//未提交
                    model.Bnote1 = "0";//不存在箱体
                    model.Ps_BomNO = "";
                    model.Ps_Serialnumber = "";
                    model.Ps_wlBomNO = "";
                    model.Ps_DocEntry = "";
                model.BJno = bjorderno;
                model.CONTROL_INFO_NO = bjno;
                model.DD_Bianhao = DD_Bianhao;
                model.COMPRESSOR_TYPE = bjtype;
                //检查物料规格名称ERP中是否有重复的
                bool jctm = NAHelper.check_IsYiJingcunzaiguige(gnjsstr);
                if(!jctm)
                    return Json(new { result = "error", res = "非标产品的规格型号（关联号）ERP中已经存在，不可以同名。" });
                //保存非标单的基本信息
                string Id = _IDKX_DDinfoDao.InsertID(model);
                //同步BOM
                BJbomInterface(bjno,Id,gnjsstr);
                //核算硬件成本
                string yjcb = Getbjyjcb(Id, gnjsstr);
                //
                bjxqandliaodanEide(Id, bjno, gnjsstr);
                //生产普实物料单号
                //同步普实产品
                Ps_Instercp(Id, Ps_sanduanno, yjcb,Gcs_nameId,KHname);
                return Json(new { result = "success", res = "下单成功"});

            }
            catch(Exception x)
            {
                return Json(new { result = "error", res = "新增异常:"+x });
            }
        }
        #endregion

        #region //保存非标订单的需求和料单明细
        public string bjxqandliaodanEide(string Id, string Bjno,string KBomNo)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
            //先BOM
            zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1");
            if (zlmodel == null)
            {
                zlmodel = new DKX_ZLDataInfoView();
                zlmodel.dd_Id = Id;
                zlmodel.Zl_type = 1;//料单
                zlmodel.Bjno = KBomNo;//报价系统单号
                zlmodel.Isgl = 2;//关联的资料
                zlmodel.C_Datetime = DateTime.Now;
                zlmodel.C_name = Suser.Id;
                zlmodel.Start = 0;//启用
                _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
            }
            else
            {
                zlmodel.Start = 1;
                _IDKX_ZLDataInfoDao.NUpdate(zlmodel);
                zlmodel = new DKX_ZLDataInfoView();
                zlmodel.dd_Id = Id;
                zlmodel.Zl_type = 1;//料单
                zlmodel.Bjno = KBomNo;//报价系统单号
                zlmodel.Isgl = 2;//关联的资料
                zlmodel.C_Datetime = DateTime.Now;
                zlmodel.C_name = Suser.Id;
                zlmodel.Start = 0;//启用
                _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
            }
            NAHelper.Insertczjl(Id, "根据报价单下单-关联料单：" + Bjno, Suser.Id);
            //再需求
            zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "0");
            if (zlmodel == null)
            {
                zlmodel = new DKX_ZLDataInfoView();
                zlmodel.dd_Id = Id;
                zlmodel.Zl_type = 0;//需求
                zlmodel.Bjno = Bjno;//报价系统单号
                zlmodel.Isgl = 2;//关联的资料
                zlmodel.C_Datetime = DateTime.Now;
                zlmodel.C_name = Suser.Id;
                zlmodel.Start = 0;//启用
                _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
            }
            else
            {
                zlmodel.Start = 1;
                _IDKX_ZLDataInfoDao.NUpdate(zlmodel);
                zlmodel = new DKX_ZLDataInfoView();
                zlmodel.dd_Id = Id;
                zlmodel.Zl_type = 0;//需求
                zlmodel.Bjno = Bjno;//报价系统单号
                zlmodel.Isgl = 2;//关联的资料
                zlmodel.C_Datetime = DateTime.Now;
                zlmodel.C_name = Suser.Id;
                zlmodel.Start = 0;//启用
                _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
            }
            NAHelper.Insertczjl(Id, "根据报价单下单-关联需求：" + Bjno, Suser.Id);
            return "2";//成功
        }
        #endregion

        #region //同步报价系统的Bom
        public string BJbomInterface(string bjorderno, string ddId,string Bomno)
        {
            //检查该订单是否存在
            if (_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(ddId, Bomno) == null)//不存在
            {
                string res = BJHelper.GetbjBom(bjorderno);
                if (res == "" || res == null)
                    return "1";
                List<BJ_BOMmodel> list = JsonConvert.DeserializeObject<List<BJ_BOMmodel>>(res);
                if (list != null)
                {
                    foreach (var a in list)
                    {
                        DKX_k3BominfoView model = new DKX_k3BominfoView();
                        //通过物料代码查询物料
                        K3_wuliaoinfoView wlmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(a.MATERIAL_NUM);
                        model.FnumberBom = Bomno;
                        model.dd_Id = ddId;
                        //  model.FNumber = a.MATERIAL_NUM;//物料代码
                        model.FNumber = wlmodel.fnumber;//
                        model.FItemName = wlmodel.fname;//物料名称
                        model.FModel = wlmodel.fmodel;//型号
                        model.FBaseUnitID = wlmodel.unitname;//单位
                        model.FAuxQty = Convert.ToDecimal(a.P_COUNT);//单位用量
                        model.C_time = DateTime.Now;
                        model.Beizhu = a.REMARK;//备注
                        model.yjcb = Convert.ToDecimal(a.UNIT_PRICE);
                        _IDKX_k3BominfoDao.Ninsert(model);
                    }
                   
                }
            }
            return "0";
        }

        //核算报价系统的硬件成本
        public string Getbjyjcb(string ddId,string Bomno)
        {
            IList<DKX_k3BominfoView> listmodel = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(ddId,Bomno);
            decimal? yjzj = 0;
            if (listmodel != null)
            {
               
                foreach (var item in listmodel)
                {
                    if (item.yjcb != null)
                    {
                        yjzj = yjzj + item.yjcb;
                    }
                }
               
            }
            return yjzj.ToString();
        }
        #endregion

        #region //生成普实的产品物料
        public string Getpswlno(string Ps_sanduanno)
        {
            Ps_fbcpmodel pscpmodel = new Ps_fbcpmodel();
            string sanduanno = "";
            sanduanno = Ps_sanduanno;
            string liushuihao = NAHelper.RandomNum(5);
            pscpmodel.ItmID = sanduanno + ".A" + liushuihao;
            string res = "";
            res= sanduanno + ".A" + liushuihao;
            if (!_IDKX_DDinfoDao.checkpswlno(pscpmodel.ItmID))
            {
                liushuihao = NAHelper.RandomNum(5);
                //pscpmodel.ItmID = sanduanno + ".A" + liushuihao;
                res = sanduanno + ".A" + liushuihao;
            }
            return res;
        }
        #endregion

        #region //同步普实物料产品
        public JsonResult Ps_Instercp(string Id, string Ps_sanduanno,string yjcb,string Gcs_nameId,string KHname)
        {
            //查询非标订单
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_wlBomNO == "" || ddmodel.Ps_wlBomNO == null)
                {
                    string sanduanno = "";
                    Ps_fbcpmodel pscpmodel = new Ps_fbcpmodel();
                    if (ddmodel.Ps_sanduanno == "" || ddmodel.Ps_sanduanno == null)
                        sanduanno = Ps_sanduanno;
                    else
                        sanduanno = ddmodel.Ps_sanduanno;
                    //流水号
                    //随机5位数
                    //string liushuihao = NAHelper.RandomNum(5);
                    string liushuihao = NAHelper.liushuihao();
                    pscpmodel.ItmID = sanduanno + ".B" + liushuihao;
                    if (!_IDKX_DDinfoDao.checkpswlno(pscpmodel.ItmID))
                    {
                        liushuihao = NAHelper.RandomNum(5);
                        pscpmodel.ItmID = sanduanno + ".B" + liushuihao;
                    }
                    pscpmodel.ItmSpec = ddmodel.KBomNo;
                    pscpmodel.Z_ItmID = Ps_sanduanno;
                    pscpmodel.Z_Price = yjcb.ToString();
                    string res = pushsoftHelper.Instercpinfo(pscpmodel);

                    if (res == "" || res == null) { return Json(new { result = "error", msg = "同步接口返回空" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            ddmodel.Ps_wlBomNO = sanduanno + ".B" + liushuihao;
                            ddmodel.Ps_Serialnumber = ".B" + liushuihao;
                            ddmodel.Ps_sanduanno = Ps_sanduanno;
                            ddmodel.Ps_DocEntry = errmodel.Data.DocEntry;
                            ddmodel.YJcb = Convert.ToDecimal(yjcb);
                            _IDKX_DDinfoDao.NUpdate(ddmodel);
                            //更新物料的工程师 公司名称
                            DKX_GCSinfoView gcsmodel = _IDKX_GCSinfoDao.NGetModelById(Gcs_nameId);
                            zypushsoftHelper.updateMDMDItmZ_temName(ddmodel.Ps_wlBomNO, gcsmodel.Name, KHname);
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
                    return Json(new { result = "error", msg = "产品已经同步,不能再次同步" });
                }

            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }
        #endregion

        //自动生产点单编号DKX20171030+01
        public string Getorderbianhao()
        {
            string Newdatestr = DateTime.Now.ToString("yyyyMMdd");
            string bianhao = "DKX" + Newdatestr + "-" + (_IDKX_DDinfoDao.GetDKXcount() + 1).ToString();
            return bianhao;
        }

        //电控箱类型下拉数据
        #region  //电控箱类型下拉数据设置下拉框值(启用的数据)
        public void AldkxtypeDropdown(string SelectedPID)
        {
            List<DKX_DDtypeinfoView> CustlistView = _IDKX_DDtypeinfoDao.GetALLQYdkxtypejson() as List<DKX_DDtypeinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (CustlistView != null)
            {
                foreach (var item in CustlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Type.ToString();
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["dkxtypeList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["dkxtypeList"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //非标巡检单
        public ActionResult FBXJView(string Id)
        {
            //查询订单
            DKX_DDinfoView ordermodel = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(ordermodel);
        }
        #endregion

        #region //工程师非标订单BOM页面可以删除编辑新增操作
        public ActionResult GCSbomView(string Id)
        {
            DKX_DDinfoView orderinfo = _IDKX_DDinfoDao.NGetModelById(Id);
            ViewBag.MyJson = JsonConvert.SerializeObject(_IDKX_pjgdbinfoDao.NGetList());
            return View(orderinfo);
        }
         
        #region //删除BOM明细记录
        public JsonResult BommxdelEide(string Id)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_k3BominfoView model = _IDKX_k3BominfoDao.NGetModelById(Id);
                if(_IDKX_k3BominfoDao.NDelete(model))
                    return Json(new { result = "success", msg = "操作成功！" });
                else
                    return Json(new { result = "error", msg = "操作失败！" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //新增BOM明细记录
        //新增页面
        [ValidateInput(false)]
        public ActionResult AddBmxView(string oId,string FnumberBom)
        {
            ViewData["oId"] = oId;
            ViewData["FnumberBom"] = FnumberBom;
            return View();
        }
        #endregion

        #region //物料的分页数据
        public ActionResult Getwuliaopage(int? page, int limit, string fnumber, string fname, string fmodel, string type)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IK3_wuliaoinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IK3_wuliaoinfoDao.SetPagerPageSize(limit);
            PagerInfo<K3_wuliaoinfoView> listmodel = _IK3_wuliaoinfoDao.GetK3_wuliaoinfoList(fnumber, fname, fmodel, type);
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

        #region //物料新增提交
        [ValidateInput(false)]
        public JsonResult AddBommxEide(string oId, string FnumberBom, string wlId, string sum, string beizhu)
        {
            try
            {
                //查询物料
                DKX_DDinfoView omodel = _IDKX_DDinfoDao.NGetModelById(oId);
                K3_wuliaoinfoView model = _IK3_wuliaoinfoDao.NGetModelById(wlId);
                DKX_k3BominfoView Bommodel = new DKX_k3BominfoView();
                Bommodel.FnumberBom = omodel.KBomNo;
                Bommodel.dd_Id = oId;
                Bommodel.FNumber = model.fnumber;
                Bommodel.FModel = model.fmodel;
                Bommodel.FItemName = model.fname;
                Bommodel.FAuxQty = Convert.ToDecimal(sum);
                Bommodel.FBaseUnitID = model.unitname;
                Bommodel.C_time = DateTime.Now;
                Bommodel.Beizhu = beizhu;
                if(_IDKX_k3BominfoDao.Ninsert(Bommodel))
                    return Json(new { result = "success", msg = "操作成功！" });
                else
                    return Json(new { result = "error", msg = "操作失败！" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //Bom表中修改物料的用量和备注页面
        public ActionResult Update_WL_NumberView(string Id)
        {
            DKX_k3BominfoView model = _IDKX_k3BominfoDao.NGetModelById(Id);
            return View(model);
        }

        public JsonResult Update_WL_NumberEide(string Id, string Number, string beizhu)
        {
            try
            {
                DKX_k3BominfoView model = _IDKX_k3BominfoDao.NGetModelById(Id);
                model.FAuxQty = Convert.ToDecimal(Number);
                model.Beizhu = beizhu;
                if(_IDKX_k3BominfoDao.NUpdate(model))
                    return Json(new { result = "success", msg = "修改成功" });
                else
                    return Json(new { result = "error", msg = "提交保存失败" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion
        #endregion

        #region //销售客服资料查看页面
        public ActionResult kfzlckView(string Id)
        {
            ViewData["Id"] = Id;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion
         
        #region //非标合同
        public ActionResult FBhetongView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        public ActionResult FBHTZWView(string Id)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }

        #region //合同的基础信息保存
        public JsonResult HTJCXXEide(string oId, string Dsjhqx, string Dsyfcd, string Dsjhdd, string DsJSFS)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //查询合同的其他信息
                NA_XSqitainfoView model = _INA_XSqitainfoDao.GetModelByDsOrder_Id(oId);
                if (model != null)
                {
                    model.Dsjhqx = Dsjhqx;
                    model.Dsyfcd = Dsyfcd;
                    model.Dsjhdd = Dsjhdd;
                    model.DsJSFS = DsJSFS;
                    if (_INA_XSqitainfoDao.NUpdate(model))
                        return Json(new { result = "success", msg = "操作成功！" });
                    else
                        return Json(new { result = "error", msg = "操作失败！" });
                }
                else
                {
                    model = new NA_XSqitainfoView();
                    model.Dsjhqx = Dsjhqx;
                    model.Dsyfcd = Dsyfcd;
                    model.Dsjhdd = Dsjhdd;
                    model.DsJSFS = DsJSFS;
                    model.DsOrder_Id = oId;
                    if (_INA_XSqitainfoDao.Ninsert(model))
                        return Json(new { result = "success", msg = "操作成功！" });
                    else
                        return Json(new { result = "error", msg = "操作失败！" });
                }
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //非标合同多单合并打印
        public ActionResult FBHTZWMOREView(string Id)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #region //根据同步普实生产的订单
        public string Getorderlistbypsorderno(string psorderno)
        {
            IList<DKX_DDinfoView> modellist = _IDKX_DDinfoDao.GetAllmxbyPs_orderno(psorderno);
            string jsonstr = JsonConvert.SerializeObject(modellist);
            return jsonstr;
            
        }
        #endregion
        #endregion

        #region //查询客户的信息通过编码
        public string Getcusinfobycode(string code)
        {
            NACustomerinfoView model = _INACustomerinfoDao.Getcusinfobycode(code);
            if (model != null)
                return JsonConvert.SerializeObject(model);
            else
                return null;
        }
        #endregion

        #region //查询客户信息通过Id
        public string GetcusinfobyId(string Id)
        {
            NACustomerinfoView model = _INACustomerinfoDao.NGetModelById(Id);
            if (model != null)
                return JsonConvert.SerializeObject(model);
            else
                return null;
        }
        #endregion

        #region //刷新报价系统的BOM数据
        public string linshishuaxinBOM()
        {
            
            string res = BJHelper.GetbjBom("fc34773e-385f-483b-a9d0-07c5a5416068");
            if (res == "" || res == null)
                return "1";
            List<BJ_BOMmodel> list = JsonConvert.DeserializeObject<List<BJ_BOMmodel>>(res);
            if (list != null)
            {
                foreach (var a in list)
                {
                    DKX_k3BominfoView model = new DKX_k3BominfoView();
                    //通过物料代码查询物料
                    K3_wuliaoinfoView wlmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(a.MATERIAL_NUM);
                    model.FnumberBom = "210824   汉钟螺杆冷水单机，压机RC2-180B-Z(P)，蒸发冷，NAS133PLC2.0";
                    model.dd_Id = "7102a0cc75fb4dcabf95b61ce3b48655";
                    model.FNumber = a.MATERIAL_NUM;//物料代码
                    model.FItemName = wlmodel.fname;//物料名称
                    model.FModel = wlmodel.fmodel;//型号
                    model.FBaseUnitID = wlmodel.unitname;//单位
                    model.FAuxQty = Convert.ToDecimal(a.P_COUNT);//单位用量
                    model.C_time = DateTime.Now;
                    model.Beizhu = a.REMARK;//备注
                    model.yjcb = Convert.ToDecimal(a.UNIT_PRICE);
                    _IDKX_k3BominfoDao.Ninsert(model);
                }

            }
            return "0";
        }
        #endregion

        #region //更新非标产品物料(工程师、客户名称、软硬件类型)
        public ActionResult updateFBitmeView()
        {
            return View();
        }

        #region //根据时间查询非标生产订单更新ERP中的数据
        public JsonResult updateERP_Mitme(string starttime, string endtime)
        {
            try
            {
                IList<DKX_DDinfoView> list = _IDKX_DDinfoDao.Getorderlistbyxdtimeandddzt(starttime, endtime, null);
                if(list.Count<=0)
                    return Json(new { result = "error", res = "这段时间不存在订单" });
                var sucsum = 0;//更新成功
                var errsum = 0;//更新失败
                var onwlnosum = 0;//没有物料号
                var fbsum = 0;//非标的
                var cgsum = 0;//常规
                foreach (var item in list)
                {
                    if (item.Ps_sanduanno == "0")
                    {//常规的
                        cgsum = cgsum + 1;
                    }
                    else
                    {//非标的
                        fbsum = fbsum + 1;
                        if (item.Ps_wlBomNO == "" || item.Ps_wlBomNO == null)
                        {//没有物料号的
                            onwlnosum = onwlnosum + 1;
                        }
                        else
                        {
                            //查询订单的工程师名称
                            DKX_GCSinfoView gcsmodel = _IDKX_GCSinfoDao.NGetModelById(item.Gcs_nameId);
                            string res = zypushsoftHelper.updateMDMDItmZ_temName(item.Ps_wlBomNO, gcsmodel.Name, item.KHname);
                            if (res == "" || res == null || res == "[]")
                                errsum = errsum + 1;
                            else
                                sucsum = sucsum + 1;
                        }
                    }
                }
                return Json(new { result = "success", res = "一共："+list.Count+"条,常规的："+ cgsum +"条,非标："+ fbsum+"条，更新成功："+ sucsum +"条,更新失败："+ errsum+"条" });
            }
            catch(Exception x)
            {
                return Json(new { result = "error", res = x});
            }
        }
        #endregion

        #endregion

        #region //

        #region //非标定制下单

        #endregion

        #region //常规定制下单

        #endregion

        #region //产品分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">当期页码</param>
        /// <param name="limit">一页的数量</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">产品类型</param>
        /// <param name="ft">是否分摊</param>
        /// <param name="Gcs_name">工程师</param>
        /// <param name="gnjs">功能简述</param>
        /// <param name="Iscg">是否常规 0 是常规定制 其他的是非标定制</param>
        /// <returns></returns>
        //public ActionResult GetCPPagerData_service(int? page, int limit, string wlno, string cpname, string gl, string dw, string Type, string ft, string Gcs_name, string gnjs, string Iscg)
        //{
        //    SessionUser Suser = Session[SessionHelper.User] as SessionUser;
        //    int CurrentPageIndex = Convert.ToInt32(page);
        //    if (CurrentPageIndex == 0)
        //        CurrentPageIndex = 1;
        //    _IDKX_CPInfoDao.SetPagerPageIndex(CurrentPageIndex);
        //    _IDKX_CPInfoDao.SetPagerPageSize(limit);

        //}
        #endregion

        #endregion

        #region //首页订单数据
        public JsonResult DKX_DD_Statistics_Interface()
        {
            try
            {
                //查询全年的
                int YY_Count = _IDKX_DDinfoDao.Get_OrderNumber_YORM("YY");
                //查询当月的
                int MM_Count = _IDKX_DDinfoDao.Get_OrderNumber_YORM("MM");
                DDDataMode model = new DDDataMode();
                model.TotalSum = YY_Count.ToString();
                model.TotaSameMonthSum = MM_Count.ToString();
                string json = JsonConvert.SerializeObject(model);
                return Json(new { result = "success", msg = "",data= model }); ;
            }
            catch(Exception x)
            {
                return Json(new { result = "error", msg = x }); ;
            }
        }
        #endregion
        #region //首页方案库数据统计
        public JsonResult DKX_CP_Statistics_Interface()
        {
            try
            {
                //查询全部
                int YY_Count = _IDKX_CPInfoDao.Get_CPNumber_YORM("YY");
                //查询当月的
                int MM_Count = _IDKX_CPInfoDao.Get_CPNumber_YORM("MM");
                DDDataMode model = new DDDataMode();
                model.TotalSum = YY_Count.ToString();
                model.TotaSameMonthSum = MM_Count.ToString();
                string json = JsonConvert.SerializeObject(model);
                return Json(new { result = "success", msg = "", data = model });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

        #region //订单生产进度查看
        public ActionResult Order_SC_JinduView(string orderno)
        {
            ViewData["orderno"] = orderno;
            return View();
        }

        public JsonResult GetOrder_SC_Jindujson(string orderno)
        {
            try
            {
                string res = gwjHelper.Order_SC_jindu(orderno);
                return Json(new { result = "success", msg = "", data = res }); ;
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x }); ;
            }
        }
        #endregion




    }
}
 