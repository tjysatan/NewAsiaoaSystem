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
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
        IDKX_RKZLDataInfoDao _IDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;
        IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        IDKX_CPInfoDao _IDKX_CPInfoDao = ContextRegistry.GetContext().GetObject("DKX_CPInfoDao") as IDKX_CPInfoDao;
        INA_MRPmainDao _INA_MRPmainDao = ContextRegistry.GetContext().GetObject("NA_MRPmainDao") as INA_MRPmainDao;
        INA_MRPdetailedDao _INA_MRPdetailedDao = ContextRegistry.GetContext().GetObject("NA_MRPdetailedDao") as INA_MRPdetailedDao;

        #region //生产标记工程资料异常
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
                if(ddmodel.DD_ZT!=3&&ddmodel.DD_ZT!=4&&ddmodel.DD_ZT!=6 && ddmodel.DD_ZT != 7)
                    return Json(new { result = "error", msg = "当前订单的状态不可以操作工程资料异常！" });
                ddmodel.gczl_Isyc = 1;
                ddmodel.gczl_yctime = DateTime.Now;
                ddmodel.gczl_ycyy = ycyy;
                if (_IDKX_DDinfoDao.NUpdate(ddmodel))
                {
                    NAHelper.Insertczjl(Id, "工程资料异常，不影响继续生产,原因："+ycyy, user.Id);
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
        public ActionResult gczlyclist()
        {
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
                    string RkRser = Newcprk(ddmodel);
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
                    cpmode.Lytype = 0;//默认是报价系统
                    if (model.Ps_BomNO != null)
                        cpmode.Ps_BomNO = model.Ps_BomNO;
                    if (model.Ps_sanduanno != null)
                        cpmode.Ps_sanduanno = model.Ps_sanduanno;
                    if (model.Ps_wlBomNO != null)
                        cpmode.Ps_wlBomNO = model.Ps_wlBomNO;
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

    }
}
