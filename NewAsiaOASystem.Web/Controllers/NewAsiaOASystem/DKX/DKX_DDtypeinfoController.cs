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
    public class DKX_DDtypeinfoController : Controller
    {
        //
        // GET: /电控箱类型管理控制器/
        IDKX_DDtypeinfoDao _IDKX_DDtypeinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDtypeinfoDao") as IDKX_DDtypeinfoDao;
        IDKX_GCSinfoDao _IDKX_GCSinfoDao = ContextRegistry.GetContext().GetObject("DKX_GCSinfoDao") as IDKX_GCSinfoDao;
        IDKX_DKXtypeandgcsDao _IDKX_DKXtypeandgcsDao = ContextRegistry.GetContext().GetObject("DKX_DKXtypeandgcsDao") as IDKX_DKXtypeandgcsDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        IDKX_PAY_CONTROL_INFODao _IDKX_PAY_CONTROL_INFODao = ContextRegistry.GetContext().GetObject("DKX_PAY_CONTROL_INFODao") as IDKX_PAY_CONTROL_INFODao;
        IDKX_CONTROL_LISTDao _IDKX_CONTROL_LISTDao = ContextRegistry.GetContext().GetObject("DKX_CONTROL_LISTDao") as IDKX_CONTROL_LISTDao;
        IDKX_CONTROL_LIST_DETAILDao _IDKX_CONTROL_LIST_DETAILDao = ContextRegistry.GetContext().GetObject("DKX_CONTROL_LIST_DETAILDao") as IDKX_CONTROL_LIST_DETAILDao;
        IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        IDKX_CPInfoDao _IDKX_CPInfoDao = ContextRegistry.GetContext().GetObject("DKX_CPInfoDao") as IDKX_CPInfoDao;
        IDKX_RKZLDataInfoDao _IDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;
        IDKX_k3BominfoDao _IDKX_k3BominfoDao = ContextRegistry.GetContext().GetObject("DKX_k3BominfoDao") as IDKX_k3BominfoDao;
        IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
        IWx_FTUserbdopenIdinfoDao _IWx_FTUserbdopenIdinfoDao = ContextRegistry.GetContext().GetObject("Wx_FTUserbdopenIdinfoDao") as IWx_FTUserbdopenIdinfoDao;
        IDKX_PleasepurchaseinfoDao _IDKX_PleasepurchaseinfoDao = ContextRegistry.GetContext().GetObject("DKX_PleasepurchaseinfoDao") as IDKX_PleasepurchaseinfoDao;
        IDKX_DDCLyqNoteInfoDao _IDKX_DDCLyqNoteInfoDao = ContextRegistry.GetContext().GetObject("DKX_DDCLyqNoteInfoDao") as IDKX_DDCLyqNoteInfoDao;
        IDKX_pjgdbinfoDao _IDKX_pjgdbinfoDao = ContextRegistry.GetContext().GetObject("DKX_pjgdbinfoDao") as IDKX_pjgdbinfoDao;


        #region //电控箱类型管理

        #region //电控箱类型的列表
        public ActionResult DKXtypelist(int? pageIndex)
        {
            PagerInfo<DKX_DDtypeinfoView> listmodel = GetDKXlistpage(pageIndex, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//类型名称
            string start = Request.Form["start"];//是否禁用
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDtypeinfoView> listmodel = GetDKXlistpage(pageIndex, Name, start);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //获取电控箱的类型分页数据
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<DKX_DDtypeinfoView> GetDKXlistpage(int? pageIndex, string Name, string start)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDtypeinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDtypeinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDtypeinfoView> listmodel = _IDKX_DDtypeinfoDao.Getdkxtypelistpage(Name, start);
            return listmodel;
        }
        #endregion

        #region //类型编辑


        #region //增加跳转页面
        public ActionResult addPage()
        {
            return View("Edit", new DKX_DDtypeinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            DKX_DDtypeinfoView sys = new DKX_DDtypeinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IDKX_DDtypeinfoDao.NGetModelById(id);
            return View("Edit", sys);
        }
        #endregion

        #region //保存提交方法
        [HttpPost]
        public JsonResult Edit(DKX_DDtypeinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.C_name = user.Id;
                    model.C_time = DateTime.Now;
                    flay = _IDKX_DDtypeinfoDao.Ninsert(model);
                }
                else//修改
                {
                    model.Up_name = user.Id;
                    model.Up_time = DateTime.Now;
                    flay = _IDKX_DDtypeinfoDao.NUpdate(model);
                }
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return Json(new { result = "error" });//提交失败
            }
        }
        #endregion

        #endregion
        #endregion

        #region //电气工程师管理

        //数据列表
        #region //数据列表
        public ActionResult gcsList(int? pageIndex)
        {
            PagerInfo<DKX_GCSinfoView> listmodel = Getgcslistpage(pageIndex, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        //查询条件
        public JsonResult gcsSearchList()
        {
            string Name = Request.Form["txt_name"];//工程师名称
            string zhname = Request.Form["txt_zhname"];//关联帐号
            string tel = Request.Form["txt_tel"];//联系电话
            string start = Request.Form["txt_start"];//是否禁用
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_GCSinfoView> listmodel = Getgcslistpage(pageIndex, Name, zhname, tel, start);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //电气工程师的分页数据
        private PagerInfo<DKX_GCSinfoView> Getgcslistpage(int? pageIndex, string name, string zhname, string tel, string start)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_GCSinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_GCSinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_GCSinfoView> listmodel = _IDKX_GCSinfoDao.GetGCSlistpage(name, zhname, tel, start);
            return listmodel;
        }
        #endregion

        #region //电气工程师编辑

        //
        #region //增加跳转页面
        public ActionResult addPagegcs()
        {
            AllgcsDropdown(null);
            return View("gcsEdit", new DKX_GCSinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPagegcs(string id)
        {
            DKX_GCSinfoView sys = new DKX_GCSinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IDKX_GCSinfoDao.NGetModelById(id);
            AllgcsDropdown(sys.ZH_Id);
            return View("gcsEdit", sys);
        }
        #endregion

        #region //保存提交方法（工程师）
        public JsonResult gcsEdit(DKX_GCSinfoView model, FrameController from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //操作是否成功
                bool flay = false;
                model.ZH_Id = Request.Form["zhListData"];
                string dkxtypeId = Request.Form["SelectComm"];//获取选中的角色
                                                              //判断Id是否存在 插入
                if (string.IsNullOrEmpty(model.Id))
                {

                    model.C_name = user.Id;
                    model.C_time = DateTime.Now;
                    string gcsId = _IDKX_GCSinfoDao.InsertID(model);
                    flay = true;
                    InsertdkxtypeandgcsId(gcsId, dkxtypeId);

                }
                else
                {
                    model.Up_name = user.Id;
                    model.Up_time = DateTime.Now;
                    DeletedkxtypeandgcsId(model.Id);
                    InsertdkxtypeandgcsId(model.Id, dkxtypeId);
                    flay = _IDKX_GCSinfoDao.NUpdate(model);
                }
                if (flay)
                {
                    return Json(new { result = "success" }, "text/html");
                }

                else
                {
                    return Json(new { result = "error" }, "text/html");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }
        #endregion

        //插入工程师和电控箱种类的关系数据
        public void InsertdkxtypeandgcsId(string gcsId, string dkxtypeId)
        {
            string[] arrTemp = dkxtypeId.Split(',');
            if (arrTemp.Length > 0)
            {
                foreach (var item in arrTemp)
                {
                    DKX_DKXtypeandgcsView typegcsmodel = new DKX_DKXtypeandgcsView();
                    typegcsmodel.gcsId = gcsId;
                    string dkxtypeid = item;
                    dkxtypeid = dkxtypeid.Substring(0, dkxtypeid.Length - 1);
                    dkxtypeid = dkxtypeid.Substring(1);
                    typegcsmodel.DkxtypeId = dkxtypeid;
                    _IDKX_DKXtypeandgcsDao.Ninsert(typegcsmodel);
                }
            }
        }

        //删除工程师和电控箱种类的关系数据
        public void DeletedkxtypeandgcsId(string gcsId)
        {
            List<DKX_DKXtypeandgcsView> model = _IDKX_DKXtypeandgcsDao.GetdkxtypeandgcslistViewbygcsId(gcsId) as List<DKX_DKXtypeandgcsView>;
            if (model != null)
            {
                _IDKX_DKXtypeandgcsDao.NDelete(model);
            }
        }

        /// <summary>
        /// 设置电控箱类型值(编辑页面时)
        /// </summary>
        /// <param name="personId">需要选中的Value值</param>
        public JsonResult DKXAlbumDropdown(string gcsId)
        {
            string personId = Request.Form["personId"];
            string json = _IDKX_DDtypeinfoDao.DKXtypeAlbumDropdown(gcsId);
            return Json(new { result = json }, "text/html");
        }

        //查询所有电气工程师的帐号
        public void AllgcsDropdown(string SelectedID)
        {
            List<SysPersonView> UnitList = new List<SysPersonView>();
            if (UnitList != null)
            {
                SysPersonView model = new SysPersonView();
                model.Name = "全部";
                model.Id = "";
                UnitList.Add(model);
                List<SysPersonView> getUnitList = _ISysPersonDao.GetPersonbyRoletype("4,5") as List<SysPersonView>;//角色查询
                if (getUnitList != null)
                    UnitList.AddRange(getUnitList);
                if (SelectedID != null)
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name", SelectedID);
                else
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name");
            }

        }

        //根据帐号的Id查找帐号的信息
        public string Getzhanhaoinfojson(string Id)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_ISysPersonDao.NGetModelById(Id));
            return json;
        }
        #endregion
        #endregion

        #region //电控箱下单列表(客服)

        #region //电控箱生产单列表（客服下单列表）
        public ActionResult DKXDDlist(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            ALLKFdataDropdown(null);//客服的下拉数据
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            //PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex,null,null,null,null,null,null,null,null,null,null,"0",null,null,null,null,null,null,null,null,null);
            //return View(listmodel);
            return View();
        }
        #endregion

        #region //条件查询
        public JsonResult DKXddSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            string BJno = Request.Form["txt_BJno"];//报价单号
            string DD_Type = Request.Form["txtDD_Type"];//订单类型
            string KHname = Request.Form["txt_KHname"];//客户名称
            string Lxname = Request.Form["txt_Lxname"];//客户联系人
            string Tel = Request.Form["txt_Tel"];//联系电话
            string Gcs_nameId = Request.Form["txtGCS"];//工程师Id
            string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            string startctime = Request.Form["txt_startctime"];//创建时间
            string endctiome = Request.Form["txt_endctiome"];//创建时间
            string start = Request.Form["txt_start"];//是否启用
            string DHtype = Request.Form["txt_DHtype"];//订货型号
            string CPPH = Request.Form["txtCPPH"];
            string beizhu1 = Request.Form["txtbeizhu1"];
            string beizhu2 = Request.Form["txtbeizhu2"];
            string YQtype = Request.Form["txtYQtype"];
            string Isdqpb = Request.Form["Isdqpb"];
            string Isqtt = Request.Form["Isqtt"];
            string gnjsstr = Request.Form["gnjs"];
            string kfIs = Request.Form["txtKF"];
            string POWER = Request.Form["txtPOWER"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex, DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, Gcs_nameId, DD_ZT, startctime, endctiome, "0", DHtype, CPPH, beizhu1, beizhu2, YQtype, Isdqpb, Isqtt, gnjsstr, kfIs, POWER);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱生产单列表分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">报价单号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1 大系统 2 物联网电控箱</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系人</param>
        /// <param name="Tel">客户电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">当前状态 -1 未提交 0已提交 1 待制图 2制图中 3 待生产 4 生产中 5 验收入库 6 待发货 7 完成 9缺料</param>
        /// <param name="startctime">创建时间</param>
        /// <param name="endctiome">创建时间</param>
        /// <param name="start">是否启用 0启用 1禁用</param>
        /// <returns></returns>
        private PagerInfo<DKX_DDinfoView> GetDKXDDlistpage(int? pageIndex, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs, string kfId,string POWER)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getdkxtypelistpage(DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, Gcs_nameId, DD_ZT, startctime, endctiome, start, DHtype, cpph, beizhu1, beizhu2, YQtype, Isdqpb, Isqttz, gnjs, kfId, POWER, Suser);
            return listmodel;
        }
        #endregion

        #region //电控箱查询（客服）生产订单分页数据-20210412
        public ActionResult Getdkxorderlist_customerservice(int? page, int limit, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs, string kfId,string POWER)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(limit);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getdkxtypelistpage(DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, Gcs_nameId, DD_ZT, startctime, endctiome, start, DHtype, cpph, beizhu1, beizhu2, YQtype, Isdqpb, Isqttz, gnjs, kfId, POWER, Suser);
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

        #region //电控箱的下单页面
        public ActionResult DKXXdView(string Id)
        {
            //预存
            DKX_DDinfoView model = new DKX_DDinfoView();
            if (Id == null || Id == "")
            {
                if (Session["oId"] == null)
                {

                    model.Start = 1;//先禁用
                    string DD_Bianhao = Getorderbianhao();//订单编号
                    model.DD_Bianhao = DD_Bianhao;
                    model.C_time = DateTime.Now;
                    Id = _IDKX_DDinfoDao.InsertID(model);
                    Session["oId"] = Id;
                }
                else
                {
                    Id = Session["oId"].ToString();
                }
                //model.Start = 1;//先禁用
                //string DD_Bianhao = Getorderbianhao();//订单编号
                //model.DD_Bianhao = DD_Bianhao;
                //Id = _IDKX_DDinfoDao.InsertID(model);
            }
            ViewData["Id"] = Id;
            AldkxtypeDropdown(null);//电控箱类型
            model = new DKX_DDinfoView();
            model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }


        //产品型号选择列表
        public ActionResult CPxhxzlistView(int? pageIndex, string Id)
        {
            AlGCSdataDropdown(null);
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            PagerInfo<DKX_CPInfoView> listmodel = Getcplistpage(pageIndex, null, null, null, null, null, null, null);
            return View(listmodel);
        }

        #region //产品列表的条件查询
        public JsonResult CPSearchList()
        {
            string cpname = Request.Form["txtcpname"];//产品名称
            string gl = Request.Form["txtgl"];//功率
            string dw = Request.Form["txtdw"];//单位
            string Type = Request.Form["txtType"];//产品类型 0 小系统 1 大系统 2 物联网
            string ft = Request.Form["txtft"];//分体 0一体1 分体
            string Gcs_name = Request.Form["txtGcs_name"];//工程师
            string cpgnjs = Request.Form["txtcpgnjs"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_CPInfoView> listmodel = Getcplistpage(pageIndex, cpname, gl, dw, Type, ft, Gcs_name, cpgnjs);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //产品分页列表数据
        /// <summary>
        /// 产品分页列表数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网</param>
        /// <param name="ft">是否分体</param>
        /// <param name="Gcs_name">工程师</param>
        /// <returns></returns>
        private PagerInfo<DKX_CPInfoView> Getcplistpage(int? pageIndex, string cpname, string gl, string dw, string Type, string ft, string Gcs_name, string gnjs)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_CPInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_CPInfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_CPInfoView> listmodel = _IDKX_CPInfoDao.GetDKXcppagelist(cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            return listmodel;
        }
        #endregion

        #region //产品选择之后提交(保存该产品的相关资料)
        public string cpxztjEide(string cpId)
        {
            try
            {
                if (Session["oId"] == null)
                    return "0";//提交异常
                string Id = Session["oId"].ToString();//订单Id
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);//查询订单信息
                DKX_CPInfoView CPmodel = _IDKX_CPInfoDao.NGetModelById(cpId);//查询产品信息
                model.DD_DHType = CPmodel.Cpname;//订货类型（产品型号）
                model.POWER = CPmodel.Power.ToString();//功率
                model.dw = CPmodel.DW;//单位
                //DKX_DDtypeinfoView ddtypemodel = _IDKX_DDtypeinfoDao.Getdkxtypebytype(CPmodel.Type.ToString());
                //if (ddtypemodel.Issh == 1)
                //{
                //    model.Isdqsh = 1;//需要电气审核
                //    model.dqshres = 3;//未提交审核
                //}
                //else
                //{
                //    model.Isdqsh = 0;//无需电气审核
                //}
                model.DD_Type = CPmodel.Type;//产品类型
                model.DD_WLWtype = CPmodel.Isft;//是否分体
                model.Gcs_nameId = CPmodel.Gcs_name;//工程师
                model.Ps_BomNO = CPmodel.Ps_BomNO;
                model.Ps_wlBomNO = CPmodel.Ps_wlBomNO;
                model.Ps_sanduanno = CPmodel.Ps_sanduanno;
                model.Ps_Serialnumber = CPmodel.Ps_Serialnumber;
                model.Ps_DocEntry = CPmodel.Ps_DocEntry;
                model.YJcb = CPmodel.YJcb;//硬件成本
                model.Iszjsc = 1;//选择产品默认直接生产
                if (CPmodel.DXTDJ != null)
                {
                    model.DXTDJ = CPmodel.DXTDJ;//单价
                }
                else
                {
                    model.DXTDJ = 0;//单价
                }
                model.gnjsstr = CPmodel.cpgnjs;
                model.gnjsstr = CPmodel.cpgnjs;
                model.Bnote = "1";//箱体存在
                model.Bnote1 = "1";//电器原来图
                model.Bnote2 = "1";//电器排布图
                model.Bnote3 = "1";
                _IDKX_DDinfoDao.NUpdate(model);//保存以上信息
                //同步资料
                string tbjg = TBxdcpzl(Id, cpId);
                if (tbjg == "0")
                    return "0";
                if (tbjg == "1")
                    return "1";//资料库为空
                if (tbjg == "2")
                    return "2";//明细复制异常
                if (tbjg == "3")
                    return "3";// 同步成功
                return "3";
            }
            catch
            {
                return "0";
            }
        }

        #region //下单产品资料同步
        public string TBxdcpzl(string NewId, string cpId)
        {
            try
            {
                //先查询该订单是否有资料
                IList<DKX_ZLDataInfoView> oldzlmodellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(NewId);
                if (oldzlmodellist != null)
                {
                    //全部逻辑删除
                    foreach (var item in oldzlmodellist)
                    {
                        item.Start = 1;
                        _IDKX_ZLDataInfoDao.NUpdate(item);
                    }
                }

                IList<DKX_RKZLDataInfoView> RKzlmodellist = _IDKX_RKZLDataInfoDao.GetDKXCPZLdatalist(cpId);
                if (RKzlmodellist != null)
                {
                    foreach (var item in RKzlmodellist)
                    {
                        DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                        if (item.Zl_type == 0 && item.Isgl == 1)//需求是关联料单K
                        {
                            if (_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NOandId(item.Bjno, NewId) == null)//不存在
                            {
                                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(NewId);
                                ddmodel.BJno = item.Bjno;
                                _IDKX_DDinfoDao.NUpdate(ddmodel);
                                string tbRES = FZBxuqiuandliaodan(NewId, item.dd_Id, item.Bjno);
                                if (tbRES == "0")
                                    return "2";//同步异常
                            }
                        }
                        if (item.Zl_type == 1 && item.Isgl == 1)//料单b
                        {
                            if (_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NOandId(item.Bjno, NewId) == null)//不存在
                            {
                                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(NewId);
                                ddmodel.BJno = item.Bjno;
                                _IDKX_DDinfoDao.NUpdate(ddmodel);
                                string tbRES = FZBxuqiuandliaodan(NewId, item.dd_Id, item.Bjno);
                                if (tbRES == "0")
                                    return "2";//同步异常
                            }
                        }
                        if (item.Zl_type == 1 && item.Isgl == 2)//料单 k
                        {
                            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(NewId);
                            ddmodel.KBomNo = item.BomNo;
                            _IDKX_DDinfoDao.NUpdate(ddmodel);
                            if (_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(NewId, item.BomNo) == null)
                            {
                                string Idstr = "";
                                if (item.dd_Id != "" && item.dd_Id != null)
                                    Idstr = item.dd_Id;
                                else
                                    Idstr = item.cpId;

                                string tbRES = FZKliaodan(NewId, Idstr, item.BomNo);
                                if (tbRES == "0")
                                    return "2";//同步异常
                            }
                        }

                        if (item.Zl_type != 3 && item.Zl_type != 4)
                        {
                            zlmodel.dd_Id = NewId;
                            zlmodel.url = item.wjurl;
                            zlmodel.wjName = item.wjName;
                            zlmodel.Zl_type = item.Zl_type;
                            zlmodel.C_name = "";
                            zlmodel.C_Datetime = item.C_time;
                            zlmodel.Start = 0;
                            zlmodel.Isgl = item.Isgl;
                            if (item.Isgl == 1)
                            {
                                zlmodel.Bjno = item.Bjno;
                            }
                            if (item.Isgl == 2)
                            {
                                zlmodel.Bjno = item.BomNo;
                            }
                            _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
                        }

                    }
                    return "3";//确定
                }
                else
                {
                    return "1";//资料库为空
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }

        #region //复制报价系统的需求和料单
        public string FZBxuqiuandliaodan(string NewId, string oId, string BjNo)
        {
            try
            {
                //报价需求
                DKX_PAY_CONTROL_INFOView xqmodel = _IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NOandId(BjNo, oId);
                if (xqmodel != null)
                    xqmodel.dd_Id = NewId;
                _IDKX_PAY_CONTROL_INFODao.Ninsert(xqmodel);//插入
                //料单主表
                DKX_CONTROL_LISTView ldzmodel = _IDKX_CONTROL_LISTDao.GetliaodanbyxqnoandoId(xqmodel.CONTROL_INFO_NO, oId);
                if (ldzmodel != null)
                    ldzmodel.dd_Id = NewId;
                _IDKX_CONTROL_LISTDao.Ninsert(ldzmodel);//插入
                //料单明细
                IList<DKX_CONTROL_LIST_DETAILView> ldmxmodellist = _IDKX_CONTROL_LIST_DETAILDao.GetliaodanmxbyxqnoandoId(BjNo, oId);
                if (ldmxmodellist != null)
                {
                    foreach (var item in ldmxmodellist)
                    {
                        DKX_CONTROL_LIST_DETAILView ldmxmodel = new DKX_CONTROL_LIST_DETAILView();
                        ldmxmodel = item;
                        ldmxmodel.dd_Id = NewId;
                        _IDKX_CONTROL_LIST_DETAILDao.Ninsert(ldmxmodel);
                    }
                }
                return "1";//
            }
            catch
            {
                return "0";//异常
            }

        }
        #endregion

        #region //复制K3系统的料单
        public string FZKliaodan(string NewId, string oId, string BomNo)
        {
            try
            {
                //料单明细
                IList<DKX_k3BominfoView> ldmodelist = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(oId, BomNo);
                if (ldmodelist != null)
                {
                    foreach (var item in ldmodelist)
                    {
                        DKX_k3BominfoView ldmxmodel = new DKX_k3BominfoView();
                        ldmxmodel = item;
                        ldmxmodel.dd_Id = NewId;
                        _IDKX_k3BominfoDao.Ninsert(ldmxmodel);
                    }
                }
                return "1";
            }
            catch
            {
                return "0";//异常
            }
        }
        #endregion
        #endregion
        #endregion
        #endregion

        #region //下单提交
        public string DKXXDEide(FrameController from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                Session["oId"] = null;
                DKX_DDinfoView model = new DKX_DDinfoView();
                string Id = Request.Form["Id"];//订单Id
                model = _IDKX_DDinfoDao.NGetModelById(Id);
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
                string gnjsstr = Request.Form["gnjsstr"];//功能简述
                string zjsc = Request.Form["zjsc"];//是否直接生产 0 否 1是
                //string dxtdj = Request.Form["dj"];//大系统单价
                string yjjqtime = Request.Form["txt_startctime"];//预计交期
                string price = Request.Form["price"];
                string Ps_sanduanno = Request.Form["Ps_sanduanno"];//非标大类的编号
                string K3CODE = Request.Form["K3CODE"];
                //model.DD_Bianhao = DD_Bianhao;
                model.DD_Type = Convert.ToInt32(DD_Type);
                model.dragstart = 0;//待助力检查
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
                model.xtIsq = 0;
                model.qtIsq = 0;
                model.IsPdrefund = 0;//不是生产退单
                model.Ps_sanduanno = Ps_sanduanno;
                if (yjjqtime != "")
                {
                    model.YJJQtime = Convert.ToDateTime(yjjqtime);
                }
                if (zjsc == "0")
                {
                    model.DD_ZT = -1;//未提交
                    model.Bnote1 = "0";//不存在箱体
                    model.Ps_BomNO = "";
                    model.Ps_Serialnumber = "";
                    model.Ps_wlBomNO = "";
                    model.Ps_DocEntry = "";
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(Id, "下单成功", user.Id);
                        return "0";//保存成功
                    }
                    else
                    {
                        return "1";//保存失败
                    }
                }
                else //(废除直接下单生产改成到待制图状态 )
                {
                    model.DD_ZT = 3;//待生产
                    model.Bnote = "1";
                    model.xtIsq = 0;
                    model.qtIsq = 0;
                    model.Bnote = "1";
                    model.xtsctime = DateTime.Now;
                    model.Bnote1 = "1";
                    model.qtsctime = DateTime.Now;
                    model.Bnote2 = "1";
                    model.dqpbsctime = DateTime.Now;
                    model.Bnote3 = "1";
                    model.xtjtsctime = DateTime.Now;
                    if (ddtypemodel.Issh == 1)
                    {
                        model.dqshres = 1;
                        model.dqshmsg = "方案库直接生产";
                    }
                    // 检查需求
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "0") == null)
                    {
                        model.DD_ZT = -1;
                        model.Bnote = "0";
                        _IDKX_DDinfoDao.NUpdate(model);
                        NAHelper.Insertczjl(Id, "下单成功", user.Id);
                        return "3";//
                    }
                    // 检查料单
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                    {
                        model.DD_ZT = -1;
                        model.Bnote = "0";
                        _IDKX_DDinfoDao.NUpdate(model);
                        NAHelper.Insertczjl(Id, "下单成功", user.Id);
                        return "3";//
                    }
                    // 检查图纸
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "2") == null)
                    {
                        model.DD_ZT = -1;
                        model.Bnote = "0";
                        _IDKX_DDinfoDao.NUpdate(model);
                        NAHelper.Insertczjl(Id, "下单成功", user.Id);
                        return "3";//
                    }

                    _IDKX_DDinfoDao.NUpdate(model);
                    //同步工位机
                    string ftemplate_id = "";
                    if (model.DD_Type == 0 || model.DD_Type == 2)
                    {//小系统和物联网
                        ftemplate_id = "25";
                    }
                    else if (model.DD_Type == 1 || model.DD_Type == 3)
                    {//大系统
                        ftemplate_id = "29";
                    }
                    else
                    {//plc
                        ftemplate_id = "30";
                    }
                    //同步工位机平板的订单资料
                    gwjHelper.synchronizationorderandzl(model.Id, model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo, ftemplate_id, "40003");//同步工位机平板的订单资料
                    NAHelper.Insertczjl(Id, "下单成功，直接到未发料状态", user.Id);
                    return "4";
                }
            }
            catch
            {
                return "2";//保存异常
            }

        }
        #endregion

        #region //关联需求单或者料单

        //关联页面
        public ActionResult RelationView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //关联料单(报价系统)
        public string xuqiuguanlianEide(string Id, string Bjno)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //检查是否存在该报价单需求
                if (_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NOandId(Bjno, Id) == null)//不存在
                {
                    string xuqiu = xuqiuInterface(Bjno, Id);//需求
                    if (xuqiu == "1")
                    {
                        liaodanInterface(Bjno, Id);
                        liandanmxInterface(Bjno, Id);
                    }
                    else
                    {
                        return "3";//不存在该需求
                    }
                }
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.BJno = Bjno;//不关联
                    model.xqIsgl = 0;//需求不关联
                    model.ldIsgl = 0;//料单不管理
                    _IDKX_DDinfoDao.NUpdate(model);
                    DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                    zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1");
                    if (zlmodel == null)
                    {
                        zlmodel = new DKX_ZLDataInfoView();
                        zlmodel.dd_Id = Id;
                        zlmodel.Zl_type = 1;//料单
                        zlmodel.Bjno = Bjno;//报价系统单号
                        zlmodel.Isgl = 1;//关联的资料
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
                        zlmodel.Bjno = Bjno;//报价系统单号
                        zlmodel.Isgl = 1;//关联的资料
                        zlmodel.C_Datetime = DateTime.Now;
                        zlmodel.C_name = Suser.Id;
                        zlmodel.Start = 0;//启用
                        _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
                    }
                    NAHelper.Insertczjl(Id, "关联料单：" + Bjno, Suser.Id);
                    return "2";//成功

                }
                else
                {
                    return "1";//订单为空
                }
            }
            catch
            {
                return "0";//同步异常
            }
        }

        //关联需求（报价系统）
        public string BglEide(string Id, string Bjno)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //检查是否存在该报价单需求
                if (_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NOandId(Bjno, Id) == null)//不存在
                {
                    string xuqiu = xuqiuInterface(Bjno, Id);//需求
                    if (xuqiu == "1")
                    {
                        liaodanInterface(Bjno, Id);
                        liandanmxInterface(Bjno, Id);
                    }
                    else
                    {
                        return "3";//不存在该需求
                    }
                }
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.BJno = Bjno;//不关联
                    model.xqIsgl = 0;//需求不关联
                    model.ldIsgl = 0;//料单不管理
                    _IDKX_DDinfoDao.NUpdate(model);
                    DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                    zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "0");
                    if (zlmodel == null)
                    {
                        zlmodel = new DKX_ZLDataInfoView();
                        zlmodel.dd_Id = Id;
                        zlmodel.Zl_type = 0;//需求
                        zlmodel.Bjno = Bjno;//报价系统单号
                        zlmodel.Isgl = 1;//关联的资料
                        zlmodel.C_Datetime = DateTime.Now;
                        zlmodel.C_name = Suser.Id;
                        zlmodel.Start = 0;//启用
                        _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
                    }
                    else
                    {
                        //zlmodel.Bjno = Bjno;//报价系统单号
                        //zlmodel.C_Datetime = DateTime.Now;
                        //zlmodel.C_name = Suser.Id;
                        zlmodel.Start = 1;//禁用 （逻辑删除）
                        _IDKX_ZLDataInfoDao.NUpdate(zlmodel);
                        zlmodel = new DKX_ZLDataInfoView();
                        zlmodel.dd_Id = Id;
                        zlmodel.Zl_type = 0;//需求
                        zlmodel.Bjno = Bjno;//报价系统单号
                        zlmodel.Isgl = 1;//关联的资料
                        zlmodel.C_Datetime = DateTime.Now;
                        zlmodel.C_name = Suser.Id;
                        zlmodel.Start = 0;//启用
                        _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
                    }
                    NAHelper.Insertczjl(Id, "关联需求：" + Bjno, Suser.Id);
                    return "2";//成功
                }
                else
                {
                    return "1";//为空
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }

        //关联料单（K3）
        public string liaodanglk3(string Id, string FnumberBom)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //检查是否存在该料单
                if (_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, FnumberBom) == null)//不存在
                {
                    string k3liaodan = k3bomInterface(FnumberBom, Id);
                    if (k3liaodan == "0")
                        return "3";//不存在料单
                }
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.KBomNo = FnumberBom;
                    model.xqIsgl = 0;//需求不关联
                    model.ldIsgl = 0;//料单不管理
                    _IDKX_DDinfoDao.NUpdate(model);
                    DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                    zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1");
                    if (zlmodel == null)
                    {
                        zlmodel = new DKX_ZLDataInfoView();
                        zlmodel.dd_Id = Id;
                        zlmodel.Zl_type = 1;//料单
                        zlmodel.Bjno = FnumberBom;//K3系统的BOM编号
                        zlmodel.Isgl = 2;//关联的资 k3关联
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
                        zlmodel.Zl_type = 1;//需求
                        zlmodel.Bjno = FnumberBom;//K3系统的BOM编号
                        zlmodel.Isgl = 2;//关联的资 k3关联
                        zlmodel.C_Datetime = DateTime.Now;
                        zlmodel.C_name = Suser.Id;
                        zlmodel.Start = 0;//启用
                        _IDKX_ZLDataInfoDao.Ninsert(zlmodel);
                    }
                    NAHelper.Insertczjl(Id, "关联K3BOM：" + FnumberBom, Suser.Id);
                    return "2";//成功
                }
                else
                {
                    return "1";//为空
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }

        #endregion

        #region //客服提交订单（只有处于未提交状态,且已经关联号需求和料单的订单可以提交）
        /// <summary>
        /// 客服提交订单（只有处于未提交状态,且已经关联号需求和料单的订单可以提交）
        /// </summary>
        /// <param name="Id">订单的Id</param>
        /// <returns></returns>
        public string kfDDtjEide(string Id)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)//不为空
            {
                if (model.DD_ZT == -1 || model.DD_ZT == 1)//在未提交和待制图状态可以提交
                {
                    //检测需求是否存在
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "0") == null)
                        return "2";//需求缺失
                    //检测料单
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                        return "3";//料单缺失
                    model.DD_ZT = 1;//待制图
                    model.gcsfh = "";
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(model.Gcs_nameId);
                        //微信提醒工程师(客服下单提醒工程师)
                        MassManager.FMB_FBDKXNotice(gscmodel.ZH_Id, model.Id, "0");
                        return "4";//提交成功
                    }
                    else
                    {
                        return "5";//提交失败
                    }
                }
                else
                {
                    return "1";//不处于未提交状态
                }
            }
            else
            {
                return "0";//订单不存在
            }
        }
        #endregion

        #region //客服提交订单（只有处于未提交状态,且已经关联号需求和料单的订单可以提交同时检测图纸资料是否存在）
        public string kfzjtjscEide(string Id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == -1 || model.DD_ZT == 1)//在未提交和待制图状态可以提交
                {
                    model.DD_ZT = 3;//待生产
                    model.Bnote = "1";
                    model.xtIsq = 0;
                    model.qtIsq = 0;
                    //检测需求是否存在
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "0") == null)
                        return "2";//需求缺失
                    //检测料单
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                        return "3";//料单缺失
                    //检查图纸
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "2") == null)
                        return "4";//图纸缺失
                    _IDKX_DDinfoDao.NUpdate(model);
                    NAHelper.Insertczjl(Id, "下单成功，直接生产", user.Id);
                    return "5";

                }
                else
                {
                    return "1";//不处于未提交状态
                }
            }
            else
            {
                return "0";//订单不存在
            }
        }
        #endregion

        #region //料单查看页面
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddno">需求单订单号</param>
        /// <returns></returns>
        public ActionResult liaodanView(string ddno)
        {
            DKX_PAY_CONTROL_INFOView model = new DKX_PAY_CONTROL_INFOView();
            model = _IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NO(ddno);
            return View(model);
        }

        #region //查询料单
        public string GetliaodanDATA(string XQNO)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_CONTROL_LISTDao.Getliaodanbyxqno(XQNO));
            return json;
        }
        #endregion

        #region //查询非控制部分的料单明细
        public string GetliaodanFKZDATA(string XQNO)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_CONTROL_LIST_DETAILDao.GetliaodanFKZbyXQno(XQNO));
            return json;
        }
        #endregion

        #region //查询控制部分的料单的明细
        public string GetliaodanKZDATA(string XQNO)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_CONTROL_LIST_DETAILDao.GetliaodanKZbyXQNO(XQNO));
            return json;
        }
        #endregion
        #endregion

        #region //点击完成发货
        //完成发货同时录入方案库
        public string WcFHEide(string Id)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//不存在该订单
                //状态
                if (model.DD_ZT != 7)//只有在代发货的状态才能完成发货和入库
                    return "2";
                //检查需求
                if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "0") == null)
                    return "3";//缺少需求
                //检查料单
                if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                    return "4";//缺少料单
                //检查图纸
                if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "2") == null)
                    return "5";//缺少图纸
                //检查图片
                if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "3") == null)
                    return "6";//缺少照片
                ////检查验收单
                //if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "4") == null)
                //    return "7";//缺少验收单
                //产品入库
                if (model.gczl_Isyc != 1)
                {
                    string RkRser = Newcprk(model);
                    if (RkRser == "1")//产品库存在
                    {
                        model.DD_ZT = 8;
                        model.Fhdatetime = DateTime.Now;//发货时间
                        model.Rkdatetime = DateTime.Now;//入库时间
                        model.Isrk = 2;//方案库存在
                        _IDKX_DDinfoDao.NUpdate(model);
                        NAHelper.Insertczjl(Id, model.DD_DHType + "完成发货，方案库存在无需入库！", Suser.Id);
                        return "8";
                    }
                    if (RkRser == "2")//入库
                    {
                        model.DD_ZT = 8;
                        model.Fhdatetime = DateTime.Now;//发货时间
                        model.Rkdatetime = DateTime.Now;//入库时间
                        model.Isrk = 1;//第一次入方案库
                        _IDKX_DDinfoDao.NUpdate(model);
                        NAHelper.Insertczjl(Id, model.DD_DHType + "完成发货，资料齐全方案入库！", Suser.Id);
                        return "9";
                    }
                    else
                    {
                        return "0";//入库异常
                    }
                }
                else
                {
                    model.DD_ZT = 8;
                    model.Fhdatetime = DateTime.Now;//发货时间
                    _IDKX_DDinfoDao.NUpdate(model);
                    NAHelper.Insertczjl(Id, model.DD_DHType + "完成发货,资料异常无法入库！", Suser.Id);
                    return "8";
                }

            }
            catch
            {
                return "0";//异常
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
                    if(model.Ps_sanduanno!=null)
                        cpmode.Ps_sanduanno = model.Ps_sanduanno;
                    if(model.Ps_wlBomNO != null)
                        cpmode.Ps_wlBomNO = model.Ps_wlBomNO;
                    if(model.Ps_wlBomNO!=null)
                        cpmode.Ps_wlBomNO = model.Ps_wlBomNO;
                    if(model.Ps_Serialnumber!=null)
                        cpmode.Ps_Serialnumber = model.Ps_Serialnumber;
                    if(model.Ps_DocEntry!=null)
                        cpmode.Ps_DocEntry = model.Ps_DocEntry;
                    if (model.YJcb!=null)
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

        //根据工程师的Id查找工程师的帐号
        public string AjaxGetgcsinfobyId(string gcsId)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_IDKX_GCSinfoDao.NGetModelById(gcsId));
            return json;
        }

        //自动生产点单编号DKX20171030+01
        public string Getorderbianhao()
        {
            string Newdatestr = DateTime.Now.ToString("yyyyMMdd");
            string bianhao = "DKX" + Newdatestr + "-" + (_IDKX_DDinfoDao.GetDKXcount() + 1).ToString();
            return bianhao;
        }

        #region //订单基本信息修改（只有未提交和待制图的状态可以修改）

        public ActionResult DDjbinfoUPView(string Id)
        {
            DKX_DDinfoView model = new DKX_DDinfoView();
            model = _IDKX_DDinfoDao.NGetModelById(Id);
            AldkxtypeDropdown(model.DD_Type.ToString());//电控箱类型
            AlGCSdataDropdown(model.Gcs_nameId);
            return View(model);
        }

        public string DDJBINFOUPEide(string Id, string ddxh, string gl, string dw, string dkxtype, string DD_WLWtype, string sl, string khname, string beizhu, string gcsId, string crjytime, string price,string gnjs)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.DD_DHType = ddxh;
                    model.POWER = gl;
                    model.dw = dw;
                    model.DD_Type = Convert.ToInt32(dkxtype);
                    if (dkxtype == "2") {
                        model.DD_WLWtype = Convert.ToInt32(DD_WLWtype);
                    }
                    model.NUM = Convert.ToDecimal(sl);
                    model.KHname = khname;
                    model.REMARK = QRHelper.ToDBC(beizhu);
                    model.Gcs_nameId = gcsId;
                    model.price = Convert.ToDecimal(price);
                    model.gnjsstr = gnjs;
                    if (crjytime != "")
                    {
                        model.YJJQtime = Convert.ToDateTime(crjytime);
                    }
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(Id, "订单基本信息修改", user.Id);
                        return "0";//保存成功
                    }
                    else
                    {
                        return "3";//修改失败
                    }
                }
                else
                {
                    return "1";//订单不存在了
                }
            }
            catch
            {
                return "2";//提交异常
            }
        }
        #endregion

        #region //订单逻辑删除（只有未提交和待制图状态可以修改）
        public string DDdelEide(string Id)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (model.DD_ZT == -1 || model.DD_ZT == 1)
                    {
                        model.Start = 1;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(Id, "订单删除成功", user.Id);
                            return "0";//删除成功
                        }
                        else
                        {
                            return "4";//删除失败
                        }
                    }
                    else
                    {
                        return "3";//当前状态不可以删除
                    }
                }
                else
                {
                    return "2";//订单不存在
                }
            }
            catch
            {
                return "1";//删除异常
            }
        }
        #endregion
        #endregion

        #region //资料查看没有软件下载权限的页面
        public ActionResult zlcknorjxzView(string Id)
        {
            ViewData["Id"] = Id;
            //查询当前账号是存在电气工程师账号
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            if (Suser.RoleType == "5" || Suser.RoleType == "0") {
                ViewData["gsc_Id"] = true;
            } else {
                DKX_GCSinfoView model = _IDKX_GCSinfoDao.Getdkx_gscmodelbyuserId(Suser.Id);
                if (model != null)
                    ViewData["gsc_Id"] = model.Id;
                else
                    ViewData["gsc_Id"] = false;
            }
            return View();
        }
        #endregion

        #region //电控箱下单列表（电气工程师）

        #region //电气工程师列表
        public ActionResult DKXDDgcslist()
        {
            AlGCSdataDropdown(null);
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            ViewData["Uname"] = Suser.UserName;
            //判定是否有审核的权限
            ViewData["SHQX"] = Getzlshqx();
            //PagerInfo<DKX_DDinfoView> listmodel = Getdkxddgcslistpage(pageIndex, null, null, null, null, null, null,"1,2,3", null, null, "0",null,null,null,null,null);
            //return View(listmodel);
            return View();
        }
        #endregion

        #region //电气工程师列表（测试）
        public ActionResult TestDKXDDgcslist(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            ViewData["Uname"] = Suser.UserName;
            //判定是否有审核的权限
            ViewData["SHQX"] = Getzlshqx();
            //PagerInfo<DKX_DDinfoView> listmodel = Getdkxddgcslistpage(pageIndex, null, null, null, null, null, null, "1,2,3", null, null, "0", null, null, null, null, null);
            return View();
        }
        #endregion

        #region   //条件查询
        public JsonResult DKXddgcsSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            string KBomNo = Request.Form["txt_KBomNo"];//关联号
            string DD_Type = Request.Form["txtDD_Type"];//订单类型
            string KHname = Request.Form["txt_KHname"];//客户名称
            string Lxname = Request.Form["txt_Lxname"];//客户联系人
            string Tel = Request.Form["txt_Tel"];//联系电话
            //string Gcs_nameId = Request.Form["txtGcs_nameId"];//工程师Id
            string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            string startctime = Request.Form["txt_startctime"];//创建时间
            string endctiome = Request.Form["txt_endctiome"];//创建时间
            string start = Request.Form["txt_start"];//是否启用
            string GCSId = Request.Form["txtGCS"];
            string DHtype = Request.Form["txtDHtype"];
            string YQtype = Request.Form["txtYQtype"];
            string Isdqpb = Request.Form["Isdqpb"];
            string Isqtt = Request.Form["Isqtt"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = Getdkxddgcslistpage(pageIndex, DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, "0", GCSId, DHtype, YQtype, Isdqpb, Isqtt);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱（电气工程师）生产单列表分页数据
        /// <summary>
        /// 获取电控箱（电气工程师）生产单列表分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">报价单号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1 大系统 2 物联网电控箱</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系人</param>
        /// <param name="Tel">客户电话</param>
        /// <param name="DD_ZT">当前状态 -1 未提交 0已提交 1 待制图 2制图中 3 待生产 4 生产中 5 验收入库 6 待发货 7 完成 9缺料</param>
        /// <param name="startctime">创建时间</param>
        /// <param name="endctiome">创建时间</param>
        /// <param name="start">是否启用 0启用 1禁用</param>
        /// <returns></returns>
        private PagerInfo<DKX_DDinfoView> Getdkxddgcslistpage(int? pageIndex, string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel,
            string DD_ZT, string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdkxlistpageGCS(DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, "0", GCSId, DHtype, YQtype, Isdqpb, Isqttz, Suser);
            return listmodel;
        }
        #endregion

        #region //电控箱查询（电气工程师）生产订单分页数据
        public ActionResult Getdkxorderlist(int? page, int limit, string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel,
            string DD_ZT, string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(limit);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdkxlistpageGCS(DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, start, GCSId, DHtype, YQtype, Isdqpb, Isqttz, Suser);

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

        #region //订单数据导出
        public FileResult ExcelExportDD(string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<DKX_DDinfoView> modellist = _IDKX_DDinfoDao.GetExcelExportDDdata(DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, "0", GCSId, DHtype, YQtype, Isdqpb, Isqttz, Suser);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("订单编号");
            row1.CreateCell(2).SetCellValue("订单类型");
            row1.CreateCell(3).SetCellValue("产品型号");
            row1.CreateCell(4).SetCellValue("关联号");
            row1.CreateCell(5).SetCellValue("单位售价");
            row1.CreateCell(6).SetCellValue("数量");
            row1.CreateCell(7).SetCellValue("总价");
            row1.CreateCell(8).SetCellValue("下单人");
            row1.CreateCell(9).SetCellValue("下单时间");
            row1.CreateCell(10).SetCellValue("工程师");
            row1.CreateCell(11).SetCellValue("客户名称");
            if (modellist != null)
            {
                int n = 0;
                decimal? Totelsum = 0;
                decimal? totelprice = 0;
                for (int i = 0; i < modellist.Count; i++)
                {
                    n = n + 1;
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                    DKX_DDtypeinfoView ddtype = _IDKX_DDtypeinfoDao.Getdkxtypebytype(modellist[i].DD_Type.ToString());
                    SysPersonView xduser = _ISysPersonDao.NGetModelById(modellist[i].C_name);
                    DKX_GCSinfoView gcsname = _IDKX_GCSinfoDao.NGetModelById(modellist[i].Gcs_nameId);

                    Totelsum = Totelsum + modellist[i].NUM;
                    if (modellist[i].price != null) {
                        totelprice = totelprice + (Math.Round(Convert.ToDecimal(modellist[i].price * modellist[i].NUM), 2));
                    }
                    rowtemp.CreateCell(0).SetCellValue(n);//序号
                    rowtemp.CreateCell(1).SetCellValue(modellist[i].DD_Bianhao.ToString());//订单编号
                    rowtemp.CreateCell(2).SetCellValue(ddtype.Name);//订单类型
                    rowtemp.CreateCell(3).SetCellValue(modellist[i].DD_DHType + "(" + modellist[i].POWER + "/" + modellist[i].dw + ")");//产品型号
                    rowtemp.CreateCell(4).SetCellValue(modellist[i].KBomNo);//关联号
                    rowtemp.CreateCell(5).SetCellValue(modellist[i].price + "元/台");//单位售价
                    rowtemp.CreateCell(6).SetCellValue(modellist[i].NUM.ToString());//数量
                    rowtemp.CreateCell(7).SetCellValue(Math.Round(Convert.ToDecimal(modellist[i].price * modellist[i].NUM), 2).ToString());
                    rowtemp.CreateCell(8).SetCellValue(xduser.UserName);
                    rowtemp.CreateCell(9).SetCellValue(modellist[i].C_time.ToString());
                    rowtemp.CreateCell(10).SetCellValue(gcsname.Name);
                    rowtemp.CreateCell(11).SetCellValue(modellist[i].KHname);//客户名称
                }
                NPOI.SS.UserModel.IRow rowtemp1 = sheet1.CreateRow(n);
                rowtemp1.CreateCell(0).SetCellValue("合计：");//合计
                rowtemp1.CreateCell(6).SetCellValue(Totelsum.ToString());//数量合计
                rowtemp1.CreateCell(7).SetCellValue(totelprice.ToString());//总价合计
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "电控箱订单数据.xls");
        }
        #endregion

        #region //电气工程师接单
        /// <summary>
        /// 工程师接单,只能操作待制图的单子
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GCSjdEide(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == 1)//待制图状态
                {
                    model.DD_ZT = 2;//制图中
                    model.Gscjdtime = DateTime.Now;
                    model.UP_datetime = DateTime.Now;
                    model.UP_name = Suser.Id;
                    if (model.Bnote != "1") model.Bnote = "0";
                    if (model.Bnote1 != "1") model.Bnote1 = "0";
                    if (model.Bnote2 != "1") model.Bnote2 = "0";
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(Id, "工程师接单-制图中", Suser.Id);
                        //工程师接单-给客服发送通知
                        MassManager.FMB_FBDKXNotice(model.C_name, model.Id, "1-1");
                        return "3";//接单成功
                    }
                    else
                    {
                        return "2";//提交失败
                    }
                }
                else
                {
                    return "1";//不处于 待制图状态
                }
            }
            else
            {
                return "0";//不存在
            }
        }

        #endregion

        #region //资料编辑
        //资料编辑页面
        public ActionResult gcszlBJView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //工程师资料查看页面
        public ActionResult gcszlckView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //订单资料json
        public string AjaxorderbyId(string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_DDinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion

        #region //需求数据json
        public string AjaxxuqiubyorderNo(string Bjno)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NO(Bjno));
            return json;
        }
        #endregion

        #region //获取资料数据
        public string GetziliaojsonbyddIdandtype(string ddId, string type)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_ZLDataInfoDao.GetzljsonbyId(ddId, type));
            return json;
        }
        #endregion

        #region //根据订单Id获取全部类型的资料
        public string Getalltypeziliaojsonbyddid(string ddId)
        {
            try
            {
                string json = null;
                json = JsonConvert.SerializeObject(_IDKX_ZLDataInfoDao.GetAllzldatabyId(ddId));
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //需求上传
        [HttpPost]
        public JsonResult ziliaouploadEide(FormContext form, HttpPostedFileBase image)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            if (image != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/xuqiu"), fileName);
                image.SaveAs(filePath);
                model.url = "/Content/upload/xuqiu/" + fileName;
                model.wjName = Path.GetFileName(image.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 0;//需求
            model.dd_Id = Id;
            model.Start = 0;
            model.C_name = Suser.Id;
            model.C_Datetime = DateTime.Now;
            model.Isgl = 0;//附件
            flay = _IDKX_ZLDataInfoDao.Ninsert(model);
            //修改订单需求关联状态
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.xqIsgl = 1;
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            NAHelper.Insertczjl(Id, "上传需求资料", Suser.Id);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });

        }
        #endregion

        #region //料单上传
        [HttpPost]
        public JsonResult liaodanuploadEide(FormContext form, HttpPostedFileBase image2)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            if (image2 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image2.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/liaodan"), fileName);
                image2.SaveAs(filePath);
                model.url = "/Content/upload/liaodan/" + fileName;
                model.wjName = Path.GetFileName(image2.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 1;//料单
            model.dd_Id = Id;
            model.Start = 0;
            model.C_name = Suser.Id;
            model.C_Datetime = DateTime.Now;
            model.Isgl = 0;//附件
            flay = _IDKX_ZLDataInfoDao.Ninsert(model);
            //修改订单需求关联状态
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.ldIsgl = 1;
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            NAHelper.Insertczjl(Id, "上传料单资料", Suser.Id);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });

        }
        #endregion

        #region //图纸上传(箱体图)
        [HttpPost]
        public JsonResult tuziuploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image3)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image3)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/tuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/tuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 2;//图纸（箱体图）
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            //查询订单信息
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.Bnote = "1";//已经存在
            ddmodel.xtsctime = DateTime.Now;//箱体上传的最新时间
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            if (ddmodel.IsPdrefund != 1)//非生产退单
            {
                NAHelper.Insertczjl(Id, "上传图纸(箱体图)资料", Suser.Id);
            }
            else//有生产退单标记的
            {
                NAHelper.InsertIsPdrefundczjltew(Id, "上传图纸(箱体图)资料", Suser.Id);
            }

            if (flay)
            {
                //工程师上传了箱体图-生产 （通知生产（生产主管/箱体确认/其他物料确认））
                //查询生产绑定的帐号
                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                if (list != null)
                {
                    for (int i = 0, j = list.Count; i < j; i++)
                    {
                        MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "1");
                    }
                    //通知下单客服
                    MassManager.FMB_FBDKXNotice(ddmodel.C_name, Id, "1");
                }
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //其他图纸上传（电器原理图）
        [HttpPost]
        public JsonResult QTtuzhiuploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image4)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image4)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/QTtuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/QTtuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 5;//其他图纸
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            //DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            //if (image4 != null)
            //{
            //    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image4.FileName);
            //    string filePath = Path.Combine(Server.MapPath("~/Content/upload/QTtuzhi"), fileName);
            //    image4.SaveAs(filePath);
            //    model.url = "/Content/upload/QTtuzhi/" + fileName;
            //    model.wjName = Path.GetFileName(image4.FileName);
            //}
            //else
            //{
            //    return Json(new { result = "error1" });
            //}
            //model.Zl_type = 5;//其他图纸
            //model.dd_Id = Id;
            //model.Start = 0;
            //model.Isgl = 0;
            //model.C_name = Suser.Id;
            //model.C_Datetime = DateTime.Now;
            //flay = _IDKX_ZLDataInfoDao.Ninsert(model);
            //查询订单信息
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.Bnote1 = "1";//待审核
            ddmodel.qtsctime = DateTime.Now;
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            if (ddmodel.IsPdrefund != 1)//非生产退单
            {
                NAHelper.Insertczjl(Id, "上传图纸(其他图纸)资料", Suser.Id);
            }
            else
            {
                NAHelper.InsertIsPdrefundczjltew(Id, "上传图纸(箱体图)资料", Suser.Id);
            }
            if (flay)
            {
                ////工程师上传了电器排布图-（通知主管工程师审核）
                //IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("13");
                //if (list != null)
                //{

                //    for (int i = 0, j = list.Count; i < j; i++)
                //    {
                //        MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "9");
                //    }
                //}
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //电器排布图
        [HttpPost]
        public JsonResult DQPBtuzhiEide(FormContext form, IEnumerable<HttpPostedFileBase> image5)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image5)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/DQPBtuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/DQPBtuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 6;//电器排布图
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;//附件
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }

            }

            //查询订单信息
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.Bnote2 = "1";//待审核
            ddmodel.dqpbsctime = DateTime.Now;//电器分布图最新上传时间
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            if (ddmodel.IsPdrefund != 1)//非生产退单
            {
                NAHelper.Insertczjl(Id, "上传图纸(电器排布图)资料", Suser.Id);
            }
            else
            {
                NAHelper.InsertIsPdrefundczjltew(Id, "上传图纸(电器排布图)资料", Suser.Id);
            }

            if (flay)
            {
                ////工程师上传了电器排布图-（通知主管工程师审核）
                //IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("13");
                //if (list != null)
                //{
                //    for (int i = 0,j=list.Count; i < j; i++)
                //    {
                //        MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "8");
                //    }
                //}
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }

        }
        #endregion  

        #region //线号表（）
        [HttpPost]
        public JsonResult XTJTtuzhiEide(FormCollection form, IEnumerable<HttpPostedFileBase> image6)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image6)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/XTJTtuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/XTJTtuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 10;//线号表
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;//附件
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }

            //查询订单信息
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.Bnote3 = "1";
            ddmodel.xtjtsctime = DateTime.Now;//系统简图
            _IDKX_DDinfoDao.NUpdate(ddmodel);
            if (ddmodel.IsPdrefund != 1)//非生产退单
            {
                NAHelper.Insertczjl(Id, "上传图纸（线号表）资料", Suser.Id);
            }
            else
            {
                NAHelper.InsertIsPdrefundczjltew(Id, "上传图纸（线号表）资料", Suser.Id);
            }
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //软件程序上传（烧录软件）
        [HttpPost]
        public JsonResult RjcxuploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image7)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image7)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/rjcxtuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 8;//烧录软件
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;//附件
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            //DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            //if (image7 != null)
            //{
            //    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image7.FileName);
            //    string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
            //    image7.SaveAs(filePath);
            //    model.url = "/Content/upload/rjcxtuzhi/" + fileName;
            //    model.wjName = Path.GetFileName(image7.FileName);
            //}
            //else
            //{
            //    return Json(new { result = "error1" });
            //}
            //model.Zl_type = 8;//烧录软件
            //model.dd_Id = Id;
            //model.Start = 0;
            //model.Isgl = 0;//附件
            //model.C_name = Suser.Id;
            //model.C_Datetime = DateTime.Now;
            //flay = _IDKX_ZLDataInfoDao.Ninsert(model);

            NAHelper.Insertczjl(Id, "上传软件程序资料(烧录软件)", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //软件程序上传（源程序）
        [HttpPost]
        public JsonResult RjycxuploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image9)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image9)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/rjcxtuzhi/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 11;//软件程序-源程序（仅仅plc 项目有）
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;//附件
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            //DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            //if (image9 != null)
            //{
            //    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image9.FileName);
            //    string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
            //    image9.SaveAs(filePath);
            //    model.url = "/Content/upload/rjcxtuzhi/" + fileName;
            //    model.wjName = Path.GetFileName(image9.FileName);
            //}
            //else
            //{
            //    return Json(new { result = "error1" });
            //}
            //model.Zl_type = 11;//软件程序-源程序（仅仅plc 项目有）
            //model.dd_Id = Id;
            //model.Start = 0;
            //model.Isgl = 0;//附件
            //model.C_name = Suser.Id;
            //model.C_Datetime = DateTime.Now;
            //flay = _IDKX_ZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "上传软件程序资料(源程序)", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //操作手册
        [HttpPost]
        public JsonResult czscuploadEide(FormContext form, IEnumerable<HttpPostedFileBase> image8)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            foreach (var file in image8)
            {
                DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/czshouce"), fileName);
                    file.SaveAs(filePath);
                    model.url = "/Content/upload/czshouce/" + fileName;
                    model.wjName = Path.GetFileName(file.FileName);
                    model.Zl_type = 9;//操作手册
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.Isgl = 0;//附件
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);

                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }

            NAHelper.Insertczjl(Id, "上传操作手册资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //删除上传的资料（逻辑删除）
        public string ziliaodel(string Id)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_ZLDataInfoView model = _IDKX_ZLDataInfoDao.NGetModelById(Id);
                model.Start = 1;//
                model.D_datetime = DateTime.Now;
                model.D_name = Suser.Id;
                _IDKX_ZLDataInfoDao.NUpdate(model);
                string czco = "删除上传的资料:" + model.Zl_type.ToString();
                NAHelper.Insertczjl(model.dd_Id, czco, Suser.Id);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region //工程师完成制图提交-待生产(判断是否需要电气审核)
        public string GCSwczhituEide(string Id, string beizhu)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model.DD_ZT == 2 || model.DD_ZT == 3 || model.DD_ZT == -2)//制图中和待生产可以提交完成制图
                {
                    //检测料单
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                        return "3";//料单缺失
                    //检测图纸（箱体图）
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "2") == null)
                        return "2";//图纸缺失
                    //检测图纸（电器原理图）
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "5") == null)
                        return "6";//电器原理图缺失
                    //检测电器分布图
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "6") == null)
                        return "7";//电气分布图缺少
                    if (model.Bnote2 != "1")
                        return "8";//电器排布图尚未审核
                    if (model.Bnote1 != "1")
                        return "9";//其他图纸尚未审核
                    if (model.Isdqsh == 1)//需要提交电气审核
                    {
                        if (model.DD_ZT == 3)
                            return "10";//待生产状态不可以修改提交
                        model.dqshres = 0;//待电气资料审核
                        model.dqtjshtime = DateTime.Now;//提交审核的时间
                        if (model.xtIsq == 0 && model.qtIsq == 0)
                        { //俩个料单都没有确认
                            model.DD_ZT = 3;//待生产
                        }
                        else if (model.xtIsq == 2 && model.qtIsq == 2)
                        {//俩个物料都齐
                            model.DD_ZT = 4;//可生产
                        }
                        else
                        {
                            model.DD_ZT = 5;//缺料
                        }
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            //电气工程师完成制图之后-通知审核资料工程师
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("14");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "8");
                                }
                            }
                            return "0";//提交成功
                        }
                        else
                        {
                            return "1";//提交失败
                        }

                    }
                    else
                    {

                        model.REMARK2 = QRHelper.ToDBC(beizhu);//工程师备注
                        model.Gscwctime = DateTime.Now;
                        model.scfh = "";
                        if (model.xtIsq == 0 && model.qtIsq == 0) { //俩个料单都没有确认
                            model.DD_ZT = 3;//待生产
                        }
                        else if (model.xtIsq == 2 && model.qtIsq == 2)
                        {//俩个物料都齐
                            model.DD_ZT = 4;//待生产
                        }
                        else {
                            model.DD_ZT = 5;//缺料
                        }
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            //工程师完成制图通知生产 （通知生产主管/箱体/其他物料）
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            for (int i = 0, j = list.Count; i < j; i++)
                            {
                                MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "2");
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "2");
                            return "0";//提交成功
                        }
                        else
                        {
                            return "1";//提交失败
                        }
                    }
                }
                else
                {
                    return "5";//不要提交
                }
            }
            catch
            {
                return "4";//提交失败
            }

        }
        #endregion

        #region //工程师点击更新K3料单

        public string UPK3bomInterface(string FnumberBom, string ddId)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                string FnumberBomstr = FnumberBom.Replace("+", "%2B");
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/getBomBodyByFBomnumber?fbomnumber=" + FnumberBomstr;
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                List<KBOMjsonmodel> timemodel = getObjectByJson<KBOMjsonmodel>(sSupplier);
                //删除原来的
                List<DKX_k3BominfoView> YLModellist = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(ddId, FnumberBom) as List<DKX_k3BominfoView>;
                if (YLModellist != null)
                {
                    _IDKX_k3BominfoDao.NDelete(YLModellist);
                }
                foreach (var a in timemodel)
                {
                    DKX_k3BominfoView model = new DKX_k3BominfoView();
                    model.FnumberBom = FnumberBom;
                    model.dd_Id = ddId;
                    model.FNumber = a.FNumber;//物料代码
                    model.FItemName = a.FItemName;//物料名称
                    model.FModel = a.FModel;//型号
                    model.FBaseUnitID = a.FBaseUnitID;//单位
                    model.FAuxQty = Convert.ToDecimal(a.FAuxQty);//单位用量
                    model.C_time = DateTime.Now;
                    model.Beizhu = a.FNote;//备注
                    _IDKX_k3BominfoDao.Ninsert(model);
                }
                //查询订单信息
                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(ddId);
                if (ddmodel.IsPdrefund != 1)//非生产退单
                {
                    NAHelper.Insertczjl(ddId, "更新K3料单", Suser.Id);
                }
                else//有生产退单标记的
                {
                    NAHelper.InsertIsPdrefundczjltew(ddId, "更新K3料单", Suser.Id);
                }
                return "2";
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region //工程师提交-主管工程师进行资料审核（新改功能 工程师提交之后直接进入待生产）
        public string GCSzltjshEide(string Id, string beizhu, string gnjsstr)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model.DD_ZT == 2)//只有制图中的状态可以提交
                {
                    //检测料单
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1") == null)
                        return "3";//料单缺失
                    //检测图纸（箱体图）
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "2") == null)
                        return "2";//图纸缺失
                    //检测图纸（电器原理图）
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "5") == null)
                        return "6";//电器原理图缺失
                    //检测布局图
                    if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "6") == null)
                        return "7";//布局图缺少
                    //判断当前是否时plc项目 DD_Type大于3
                    if (model.DD_Type > 3) { //plc项目
                        ////检测烧录软件
                        //if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "8") == null)
                        //    return "8-1";//检测烧录软件缺少
                        ////检测源程序
                        //if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "11") == null)
                        //    return "8-2";//检测源程序缺少
                        //检测操作手册
                        if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "9") == null)
                            return "8-3";//检测操作手册缺少
                    }
                    if (model.Isdqsh == 1)//需要提交电气审核
                    {
                        if (model.DD_ZT == 3)
                            return "10";//待生产状态不可以修改提交
                        model.dqshres = 0;//待电气资料审核
                        model.dqtjshtime = DateTime.Now;//提交审核的时间
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            //电气工程师完成制图之后-通知审核资料工程师
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("14");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "8");
                                }
                            }
                            return "0-1";//提交成功
                        }
                        else
                        {
                            return "1";//提交失败
                        }
                    }
                    else
                    {

                        model.REMARK2 = QRHelper.ToDBC(beizhu);//工程师备注
                        model.Gstjshtime = DateTime.Now;
                        model.Gscwctime = DateTime.Now;
                        model.scfh = "";
                        model.dragstart = 1;//待助力检查
                        model.gnjsstr = gnjsstr;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            return "0";//提交成功
                        }
                        else
                        {
                            return "1";//提交失败
                        }
                    }
                }
                else
                {
                    return "5";//该状态下无法提交
                }
            }
            catch
            {
                return "4";//提交异常
            }
        }
        #endregion

        #region //退回上一步（制图中的返回待制图客服可以修改基本资料）
        public string GCSFanhuiEide(string Id)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (model.DD_ZT == 1)
                        return "3";//处于待制图状态不需要返回
                    if (model.DD_ZT == 2)//处于制图中
                    {
                        model.DD_ZT = 1;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(Id, "制图中返回-待制图", user.Id);
                            return "0";//操作成功
                        }
                        else
                        {
                            return "4";//操作失败
                        }
                    }
                    else
                    {
                        return "5";//当前状态不可以退回上一步
                    }
                }
                else
                {
                    return "2";//订单不存在
                }
            }
            catch
            {
                return "1";//操作异常
            }
        }
        #endregion

        #region //退回到-未提交的状态（客服可以修改需求单的）
        //退回到未提交状态
        public ActionResult gcsTHView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        //退回到未提交状态
        public string GcsfanhuiwtjEide(string Id, string thyy)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.DD_ZT == 1 || model.DD_ZT == 2)//在待制图和制图中的状态可以提交返回
                {
                    model.DD_ZT = -1;
                    model.gcsfh = thyy;
                    model.gcsfhdatetime = DateTime.Now;//工程师返回的时间
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(Id, "制图中返回-未提交状态-退回原因：" + thyy, user.Id);
                        //退回给对应的客服推送微信消息
                        MassManager.FMB_FBDKXNotice(model.C_name, model.Id, "12");
                        return "2";//操作成功
                    }
                    else
                    {
                        return "3";//退回失败
                    }
                }
                else
                {
                    return "4";//该状态下无法操作
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //主管电气工程师资料审核
        //资料审核页面
        public ActionResult zlshView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        //电气排布图审核提交 //Id 订单Id   type 0 通过  1不通过
        public string dqpbtuzhishEide(string Id, string type)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //查询订单
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.DD_ZT == 2 || model.DD_ZT == 3 || model.DD_ZT == -2)
                {
                    if (model.Bnote2 == "0")//判定电器排布图是否上传
                        return "3";//图纸尚未上传
                    if (type == "0")//通过
                    {
                        model.Bnote2 = "1";//通过
                        model.dqpbshuserId = Suser.Id;
                        model.xttshtime = DateTime.Now;
                        if (_IDKX_DDinfoDao.NUpdate(model))//提交成功
                        {
                            string wcstr = GCSwczhituEide(Id, "");
                            NAHelper.Insertczjl(model.Id, "电器排布图审核-通过", Suser.Id);
                            return "4";//提交成功
                        }
                        else
                        {
                            return "5";//提交不成功
                        }
                    }
                    else//不通过
                    {
                        model.Bnote2 = "3";//不通过
                        model.dqpbshuserId = Suser.Id;
                        model.xttshtime = DateTime.Now;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(model.Id, "电器排布图审核-不通过", Suser.Id);
                            DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(model.Gcs_nameId);
                            //微信提醒工程师(客服下单提醒工程师)
                            MassManager.FMB_FBDKXNotice(gscmodel.ZH_Id, model.Id, "10");
                            return "6";//审核成功
                        }
                        else
                        {
                            return "5";//提交失败
                        }
                    }
                }
                else
                {
                    return "2";//当前状态不可以审核图纸
                }
            }
            catch
            {
                return "0";//操作异常
            }
        }

        //其他图纸审核提交 //Id 订单Id  type 0 通过 1 不通过
        public string qttuzhishEide(string Id, string type)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //订单查询
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.DD_ZT == 2 || model.DD_ZT == 3 || model.DD_ZT == -2)
                {
                    if (model.Bnote1 == "0")//判定其他图纸是否上传
                        return "3";//图纸尚未上传
                    if (type == "0")//通过
                    {
                        model.Bnote1 = "1";//通过
                        model.qtshuserId = Suser.Id;
                        model.qttshtime = DateTime.Now;
                        if (_IDKX_DDinfoDao.NUpdate(model))//提交成功
                        {
                            string wcstr = GCSwczhituEide(Id, "");
                            NAHelper.Insertczjl(model.Id, "其他图纸审核-通过", Suser.Id);
                            // DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(model.Gcs_nameId);
                            //微信提醒工程师 
                            // MassManager.FMB_FBDKXNotice(gscmodel.ZH_Id, model.Id, "11");
                            return "4";//提交成功
                        }
                        else
                        {
                            return "5";//提交不成功
                        }
                    }
                    else
                    {
                        model.Bnote1 = "3";//通过
                        model.qtshuserId = Suser.Id;
                        model.qttshtime = DateTime.Now;
                        if (_IDKX_DDinfoDao.NUpdate(model))//提交成功
                        {
                            // string wcstr = GCSwczhituEide(Id, "");
                            NAHelper.Insertczjl(model.Id, "其他图纸审核-不通过", Suser.Id);
                            DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(model.Gcs_nameId);
                            //微信提醒工程师 
                            MassManager.FMB_FBDKXNotice(gscmodel.ZH_Id, model.Id, "11");
                            return "4";//提交成功
                        }
                        else
                        {
                            return "5";//提交不成功
                        }
                    }
                }
                else
                {
                    return "2";//当前状态不可以审核图纸
                }
            }
            catch
            {
                return "0";//操作异常
            }
        }

        #region //根据当前登录的帐号的角色判定是否可以操作审核按钮
        public string Getzlshqx()
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            string str = user.RoleType;
            if (str == "0" || str == "5")//超级管理员和工程师主管可以审核
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        #endregion


        #endregion

        #region //电气工程师重新编辑软件资料（订单状态未生产中/且是plc产品）
        public ActionResult gcsSupplementView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //电器工程师检查助力功能

        #region //查询需要检查助力的中数量
        public JsonResult Getdargcount(string dragstart)
        {

            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                int dragcount = 0;
                dragcount = _IDKX_DDinfoDao.Getdragsumbyzt(dragstart, Suser);
                return Json(new { result = "success", res = dragcount });

            }
            catch
            {
                return Json(new { result = "error", res = "0" });
            }
        }
        #endregion

        #region //订单助力检查的列表页面
        public ActionResult DKXDDDraglist(int? pageIndex)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            ViewData["Uname"] = Suser.UserName;
            PagerInfo<DKX_DDinfoView> listmodel = Getdraglistpage(pageIndex, null);
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult DKXdragSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单编号
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = Getdraglistpage(pageIndex, DD_Bianhao);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取需要助力检查的分页数据
        private PagerInfo<DKX_DDinfoView> Getdraglistpage(int? pageIndex, string DD_Bianhao)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdragddinfoPagerlist(DD_Bianhao, Suser);
            return listmodel;
        }
        #endregion

        #region //资料查看助力检查
        public ActionResult dragzlcheckupView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //资料检查提交
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">0 检查通过 1驳回</param>
        /// <param name="sm">驳回原因</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult dragdatacheckupEide(string Id, string type, string extra)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model.DD_ZT == 2)//只有制图中的状态可以提交
                {
                    model.draguserId = Suser.Id;
                    model.dragusername = Suser.RName;
                    if (type == "0")
                    {
                        NAHelper.Insertczjl(Id, "订单助力检查完成", Suser.Id);
                        model.dragstart = 2;
                        if (model.xtIsq == 0 && model.qtIsq == 0)
                        { //俩个料单都没有确认
                            model.DD_ZT = 3;//未发料
                            model.Flzt = 0;//物料待确认
                        }
                        else if (model.xtIsq == 2 && model.qtIsq == 2)
                        {//俩个物料都齐
                            model.DD_ZT = 3;//未发料
                            model.Flzt = 5;//待发料
                        }
                        else
                        {
                            model.DD_ZT = 3;//未发料
                            model.Flzt = 10;//待发料
                        }
                    }
                    else
                    {
                        NAHelper.Insertczjl(Id, "订单助力检查驳回", Suser.Id);
                        model.dragstart = 3;
                        model.dragsm = extra;
                        model.dragtime = DateTime.Now;
                    }
                    if (_IDKX_DDinfoDao.NUpdate(model)) {
                        if (type == "0")//通过检查之后上传提交
                        {
                            string ftemplate_id = "";
                            if (model.DD_Type == 0 || model.DD_Type == 2)
                            {//小系统和物联网
                                ftemplate_id = "25";
                            }
                            else if (model.DD_Type == 1 || model.DD_Type == 3)
                            {//大系统
                                ftemplate_id = "29";
                            }
                            else
                            {//plc
                                ftemplate_id = "30";
                            }
                            //同步工位机平板的订单资料
                            gwjHelper.synchronizationorderandzl(model.Id, model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo, ftemplate_id, "40003");//同步工位机平板的订单资料
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");//通知生产 仓库
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "2");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "2");
                        }

                        return Json(new { result = "success", res = "提交成功！" });
                    }
                    else {
                        return Json(new { result = "success", res = "保存失败，请重新操作！" });
                    }
                }
                else
                {
                    return Json(new { result = "error", res = "该订单状态已经改变，无法助力检查提交！" });
                }
            }
            catch
            {
                return Json(new { result = "error", res = "提交异常" });
            }
        }
        #endregion
        #endregion

        #endregion

        #region //电控箱下单列表（电气审核工程师）

        #region //电气审核列表
        public ActionResult DKXDDDQSHlist(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            PagerInfo<DKX_DDinfoView> listmodel = Getdkxdddqshlistpage(pageIndex, null, null, null, null, null, null, "0", null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult DKXdddqshSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            string DD_Type = Request.Form["txtDD_Type"];//订单类型
            string KHname = Request.Form["txt_KHname"];//客户名称
            string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            string startctime = Request.Form["txt_startctime"];//创建时间
            string endctiome = Request.Form["txt_endctiome"];//创建时间
            string start = Request.Form["txt_start"];//是否启用
            string GCSId = Request.Form["txtGCS"];
            string DHtype = Request.Form["txtDHtype"];
            string dqshjd = Request.Form["txtdqshjd"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = Getdkxdddqshlistpage(pageIndex, DD_Bianhao, DD_Type, KHname, startctime, endctiome, DD_ZT, "0", GCSId, DHtype, dqshjd);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱（电气审核）生产列表分页数据
        private PagerInfo<DKX_DDinfoView> Getdkxdddqshlistpage(int? pageIndex, string DD_Bianhao, string DD_Type, string KHname,
             string startctime, string endctiome, string DD_ZT, string start, string GCSId, string DHtype, string dqshjd)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getdkxlistpagedqsh(DD_Bianhao, DD_Type, KHname, startctime, endctiome, DD_ZT, start, GCSId, DHtype, dqshjd, Suser);
            return listmodel;
        }
        #endregion

        #region //电气资料审核页面
        //电气资料审核页面
        public ActionResult DQzlshView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //电气资料审核提交
        public string DQzlshtjEide(string Id, string type, string con)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //查询订单
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.DD_ZT == 2 || model.DD_ZT == 3)//在制图中和待生产（待发料）
                {
                    if (model.dqshres == 3)
                        return "8";//电气工程师尚未提交资料审核
                    if (!string.IsNullOrEmpty(type))
                    {
                        if (type == "0")//通过
                        {
                            model.DD_ZT = 3;
                            model.dqshres = 1;
                            model.dqshtime = DateTime.Now;
                            model.dqshuserId = Suser.Id;
                            model.dqshmsg = "通过";
                            if (_IDKX_DDinfoDao.NUpdate(model))
                            {
                                //资料审核完成 （通知生产主管/箱体/其他物料）
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "2");
                                }
                                //通知下单客服
                                MassManager.FMB_FBDKXNotice(model.C_name, Id, "2");
                                NAHelper.Insertczjl(model.Id, "电气资料审核-通过", Suser.Id);
                                return "3";//提交完成
                            }
                            else
                            {
                                return "4";//提交失败
                            }
                        }
                        else
                        {
                            model.DD_ZT = 2;
                            model.dqshres = 2;
                            model.dqshtime = DateTime.Now;
                            model.dqshuserId = Suser.Id;
                            model.dqshmsg = con;
                            if (_IDKX_DDinfoDao.NUpdate(model))
                            {
                                //通知电气工程师

                                NAHelper.Insertczjl(model.Id, "电气资料审核-不通过", Suser.Id);
                                DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(model.Gcs_nameId);
                                //微信提醒工程师(资料审核失败，被退回通知工程师)
                                MassManager.FMB_FBDKXNotice(gscmodel.ZH_Id, model.Id, "15");
                                return "3";//提交完成
                            }
                            else
                            {
                                return "4";
                            }
                        }
                    }
                    else
                    {
                        return "9";//请选择审核结果
                    }
                }
                else
                {
                    return "7";//该状态下无法进行审核提交
                }
            }
            catch
            {
                return "0";//操作异常
            }
        }
        #endregion
        #endregion

        #region //电控箱下单列表（生产）

        #region //生产部门的电控生产列表
        public ActionResult DKXDDsclist(int? pageIndex)
        {
            PagerInfo<DKX_DDinfoView> listmodel = new PagerInfo<DKX_DDinfoView>();
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            if (Session[SessionHelper.DKXSerch] != null)
            {
                DKXDDCXTJView ssearch = Session[SessionHelper.DKXSerch] as DKXDDCXTJView;
                ViewData["DD_Bianhao"] = ssearch.DD_Bianhao;
                ViewData["BJno"] = ssearch.BJno;
                ViewData["DD_Type"] = ssearch.DD_Type;
                ViewData["KHname"] = ssearch.KHname;
                ViewData["Lxname"] = ssearch.Lxname;
                ViewData["Tel"] = ssearch.Tel;
                ViewData["DD_ZT"] = ssearch.DD_ZT;
                ViewData["startctime"] = ssearch.startctime;
                ViewData["endctiome"] = ssearch.endctiome;
                ViewData["DHtype"] = ssearch.DHtype;
                //listmodel = Getdkxddsclistpage(pageIndex, ssearch.DD_Bianhao, ssearch.BJno, ssearch.DD_Type, ssearch.KHname, ssearch.Lxname, ssearch.Tel, ssearch.DD_ZT, ssearch.startctime, ssearch.endctiome, "0", ssearch.DHtype,null);
            }
            else
            {
                //listmodel = Getdkxddsclistpage(pageIndex, null, null, null, null, null, null, null, null, null, "0", null,null);
            }

            return View();
            //return View(listmodel);
        }
        #endregion

        #region //20210409生产订单分页数据
        public ActionResult GetdkxorderlistSC(int? page, int limit, string DD_Bianhao, string BJno, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel,
    string DD_ZT, string startctime, string endctiome, string start, string DHtype, string YQtype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(limit);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdkxlistpageSC(DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, "0", DHtype, YQtype, Suser);

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

        #region //条件查询
        public JsonResult DKXddscSearchList()
        {
            Session[SessionHelper.DKXSerch] = null;
            Session.Remove(SessionHelper.DKXSerch);
            DKXDDCXTJView view = new DKXDDCXTJView();
            view.DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            view.BJno = Request.Form["txt_BJno"];//报价单号
            view.DD_Type = Request.Form["txtDD_Type"];//订单类型
            view.KHname = Request.Form["txt_KHname"];//客户名称
            view.Lxname = Request.Form["txt_Lxname"];//客户联系人
            view.Tel = Request.Form["txt_Tel"];//联系电话
            view.DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            view.startctime = Request.Form["txt_startctime"];//创建时间
            view.endctiome = Request.Form["txt_endctiome"];//创建时间
            view.DHtype = Request.Form["txt_DHtype"];
            Session[SessionHelper.DKXSerch] = view;
            string YQtype = Request.Form["txtYQtype"];
            //string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            //string BJno = Request.Form["txt_BJno"];//报价单号
            //string DD_Type = Request.Form["txtDD_Type"];//订单类型
            //string KHname = Request.Form["txt_KHname"];//客户名称
            //string Lxname = Request.Form["txt_Lxname"];//客户联系人
            //string Tel = Request.Form["txt_Tel"];//联系电话
            ////string Gcs_nameId = Request.Form["txtGcs_nameId"];//工程师Id
            //string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            //string startctime = Request.Form["txt_startctime"];//创建时间
            //string endctiome = Request.Form["txt_endctiome"];//创建时间
            //string start = Request.Form["txt_start"];//是否启用
            //string DHtype = Request.Form["txt_DHtype"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = Getdkxddsclistpage(pageIndex, view.DD_Bianhao, view.BJno, view.DD_Type, view.KHname, view.Lxname, view.Tel, view.DD_ZT, view.startctime, view.endctiome, "0", view.DHtype, YQtype);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱（生产部门）生产单列表分页数据
        private PagerInfo<DKX_DDinfoView> Getdkxddsclistpage(int? pageIndex, string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string YQtype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdkxlistpageSC(DD_Bianhao, KBomNo, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, start, DHtype, YQtype, Suser);
            return listmodel;
        }
        #endregion

        #region //生产部门接单
        public string SCjdEide(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == 3)
                {
                    model.DD_ZT = 4;//生产中
                    model.UP_datetime = DateTime.Now;
                    model.UP_name = Suser.Id;
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(Id, "生产接单-生产中", Suser.Id);
                        return "3";//接单成功
                    }
                    else
                    {
                        return "2";//提交失败
                    }
                }
                else
                {
                    return "1";//不处于 待制图状态
                }
            }
            else
            {
                return "0";//不存在
            }
        }
        #endregion

        #region //生产接单或者缺料状态的提交
        /// <summary>
        /// 生产接单或者缺料状态的提交
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">操作类型 0 接单 1 缺料</param>
        /// <returns></returns>
        public string SCjdorqlEide(string Id, string type)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == 4)//在可生产状态下,可以进行接单生产
                {
                    if (type == "0")//接单生产中
                    {
                        model.DD_ZT = 6;//生产中
                        model.Scjdtime = DateTime.Now;
                        model.UP_datetime = DateTime.Now;
                        model.UP_name = Suser.Id;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(Id, "生产接单-生产中", Suser.Id);
                            //生产接单-生产中通知客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "5");

                            return "3";//提交成功
                        }
                        else
                        {
                            return "2";//提交失败
                        }
                    }
                    else //缺料
                    {
                        model.DD_ZT = 3;//未发料
                        model.Flzt = 10;//缺料
                        model.Scjdtime = DateTime.Now;//
                        model.UP_name = Suser.Id;
                        model.UP_datetime = DateTime.Now;
                        //model.DD_ZT = 5;//缺料
                        //model.Scjdtime = DateTime.Now;//
                        //model.UP_name = Suser.Id;
                        //model.UP_datetime = DateTime.Now;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(Id, "生产接单-缺料", Suser.Id);
                            //生产接单-缺料通知客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "5");
                            return "3";//提交成功
                        }
                        else
                        {
                            return "2";//提交失败
                        }
                    }
                }
                else
                {
                    return "4";//无须提交
                }
            }
            else
            {
                return "0";//不存在订单
            }
        }
        #endregion

        #region //生产返回上一级提交
        #region //生产退单页面
        public ActionResult SCFTView(string Id)
        {
            GetSCCBRDATADropdown(null, "1");
            ViewData["OID"] = Id;
            return View();
        }
        #endregion
        public string SCFTEide(string Id, string con, string CbrId, string bz)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == 3 || model.DD_ZT == 4 || model.DD_ZT == 5 || model.DD_ZT == 6)
                {
                    model.DD_ZT = 2;//制图中
                    model.scfh = QRHelper.ToDBC(con + bz);
                    model.scfhdatetime = DateTime.Now;
                    model.UP_datetime = DateTime.Now;
                    model.UP_name = Suser.Id;
                    model.dragstart = 0;
                    model.dqshuserId = "";
                    model.dragsm = "";
                    model.dragusername = "";
                    model.IsPdrefund = 1;//标记生产退单
                    if (model.Isdqsh == 1)
                    {
                        model.dqshres = 3;
                        model.dqshmsg = "";
                    }
                    if (_IDKX_DDinfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjltew(Id, "生产返退-" + con + bz, Suser.Id, model.DD_Bianhao, model.Gcs_nameId, CbrId, con, bz);
                        //生产退回通知工程师
                        MassManager.FMB_FBDKXNotice(model.Gcs_nameId, Id, "3");
                        return "3";//提交成功
                    }
                    else
                    {
                        return "2";//提交失败
                    }
                }
                else
                {
                    return "1";//无法退回到上级
                }
            }
            else
            {
                return "0";//不存在订单
            }
        }

        #region //获取生产对单的下拉数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectedPID">默认选中的数据</param>
        /// <param name="type">0包含全部 1不包含全部选项</param>
        public void GetSCCBRDATADropdown(string SelectedPID, string type)
        {
            IList<NewChargebackReasonView> modellist = NAHelper.GetSCCBRDATA();
            List<NewChargebackReasonView> listView = modellist as List<NewChargebackReasonView>;
            ViewBag.MyJson = JsonConvert.SerializeObject(modellist);
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            if (type == "0")
            {
                Reasonmodel.Name = "全部";
                Reasonmodel.Id = "";
                Reasonlist.Add(Reasonmodel);
            }
            if (listView != null)
            {
                foreach (var item in listView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id.ToString();
                    Reasonmodel.Name = item.Reason_name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["SCCBRTlist"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["SCCBRTlist"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion
        #endregion

        #region //生产部门查看资料页面
        public ActionResult SCzkckView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //生产状态编辑页面
        public ActionResult SCZTView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //生产部门_生成验收,只正对生产中的和待评审  的状态
        public ActionResult SCysView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        #region //验收图片上传
        [HttpPost]
        public JsonResult tupianuploadEide(FormContext form, List<HttpPostedFileWrapper> image)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            if (image != null)
            {
                //string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image.FileName);
                //string filePath = Path.Combine(Server.MapPath("~/Content/upload/zhaopian"), fileName);
                //image.SaveAs(filePath);
                //model.url = "/Content/upload/zhaopian/" + fileName;
                //model.wjName = Path.GetFileName(image.FileName);
                foreach (var img in image)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(img.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/zhaopian"), fileName);
                    img.SaveAs(filePath);
                    model.url = "/Content/upload/zhaopian/" + fileName;
                    model.wjName = Path.GetFileName(img.FileName);
                    model.Zl_type = 3;//需求
                    model.dd_Id = Id;
                    model.Start = 0;
                    model.C_name = Suser.Id;
                    model.C_Datetime = DateTime.Now;
                    model.Isgl = 0;//附件
                    flay = _IDKX_ZLDataInfoDao.Ninsert(model);
                }
            }
            else
            {
                return Json(new { result = "error1" });
            }
            NAHelper.Insertczjl(Id, "验收上传照片", Suser.Id);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }
        #endregion

        #region //验收单上传
        [HttpPost]
        public JsonResult yanshoudanuploadEide(FormContext form, HttpPostedFileBase image)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_ZLDataInfoView model = new DKX_ZLDataInfoView();
            if (image != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/yanshoudan"), fileName);
                image.SaveAs(filePath);
                model.url = "/Content/upload/yanshoudan/" + fileName;
                model.wjName = Path.GetFileName(image.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 4;//需求
            model.dd_Id = Id;
            model.Start = 0;
            model.C_name = Suser.Id;
            model.C_Datetime = DateTime.Now;
            model.Isgl = 0;//附件
            flay = _IDKX_ZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "验收单上传资料", Suser.Id);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }
        #endregion

        #region //生产完成验收入库-进入待品审
        public string SCwcyanshouEide(string Id, string czy, string dsy)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (model.DD_ZT == 6 || model.DD_ZT == -3)
                    {
                        //检查是否存在照片
                        if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "3") == null)//不存在照片
                            return "1";
                        if (model.DD_Type > 3)
                        {                          //检测烧录软件
                            if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "8") == null)
                                return "8-1";//检测烧录软件缺少
                                             //检测源程序
                            if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "11") == null)
                                return "8-2";//检测源程序缺少
                        }

                        //if (_IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "4") == null)//不存在验收单  （验收单不强制）
                        //    return "2";
                        model.DD_ZT = -3;//待品审
                        model.Ysdatetime = DateTime.Now;
                        model.UP_datetime = DateTime.Now;
                        model.pbshzt = 0;//品审状态
                        model.pbshbtgyy = "";
                        model.scczname = czy;
                        model.scDSname = dsy;
                        model.UP_name = Suser.Id;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(Id, "完成生产验收", Suser.Id);
                            //查询品保绑定的帐号
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("5");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "6");
                                }
                                //生产完成验收-通知客服-品保
                                MassManager.FMB_FBDKXNotice(model.C_name, Id, "6");
                            }
                            //插入K3 订单编号号工程师
                            Insterbianhaoandgcsname(model.DD_Bianhao, model.Gcs_nameId);

                            return "3";//提交完成
                        }
                        else
                        {
                            return "4";//提交失败
                        }
                    }
                    else
                    {
                        return "6";//不需要验收
                    }
                }
                else
                {
                    return "5";
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion
        #endregion


        #region //料单库存
        public ActionResult liaodanKCView(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            //DKX_PAY_CONTROL_INFOView model = new DKX_PAY_CONTROL_INFOView();
            //model = _IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NO(ddmodel.BJno);
            ViewData["sum"] = ddmodel.NUM;
            ViewData["Id"] = Id;
            DKX_ZLDataInfoView zlmodel = _IDKX_ZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1");//料单
            ViewData["type"] = 0;//不存在关联料单
            if (zlmodel != null)
            {
                if (zlmodel.Isgl == 1)
                {
                    ViewData["type"] = 1;//报价系统
                }
                if (zlmodel.Isgl == 2)
                {
                    ViewData["type"] = 2;//K3料单
                }
                ViewData["BjNo"] = zlmodel.Bjno;
            }
            return View();
        }

        #region //物料代理查看库存B
        public string CKKCBYXH(string xh)
        {
            try
            {
                string wl = Getwuliaodaimabyxh(xh);
                if (wl != "" && wl != null)
                {
                    DataTable dt = GetKcsum("'" + wl + "'");
                    string count = "0.00";
                    if (dt.Rows.Count > 0)
                    {
                        //P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                        count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
                    }
                    return count;
                }
                else
                {
                    return "-";
                }
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region //物料代理查看库存k
        public string CKkcbywldm(string wldm)
        {
            try
            {
                DataTable dt = GetKcsum("'" + wldm + "'");
                string count = "0.00";
                if (dt.Rows.Count > 0)
                {
                    //P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                    count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
                }
                return count;
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region //报价系统的料单明细查询
        public string BJliaodanmx(string xqno, string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_CONTROL_LIST_DETAILDao.GetliaodanmxbyxqnoandoId(xqno, Id));
            return json;
        }
        #endregion

        #region //K3料单明细
        public string K3liaodanmx(string Fnumber, string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, Fnumber));
            return json;
        }
        #endregion
        #endregion

        #region //电控箱的生产任务单打印
        /// <summary>
        /// 电控箱任务单的打印页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DKXSCPrintView(string Id)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }

        //订单数据查询
        public string GetDDDATAbyId(string Id)
        {
            string json = "";
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            json = JsonConvert.SerializeObject(model);
            return json;
        }
        #endregion

        #region //非标电控箱生产过程表打印页面
        public ActionResult DKXSCGCCPrintView(string Id, string type)
        {
            ViewData["type"] = type;//0 非标线  5 常规线
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion



        #region //物料确认
        #region //物料确认页面
        /// <summary>
        /// 物料确认页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult WLQRView(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            ViewData["Id"] = Id;
            ViewData["Uname"] = Suser.UserName;
            return View();
        }
        #endregion

        #region //物料确认提交
        /// <summary>
        /// 物料确认提交
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">物料类型 0 箱体 1其他物料</param>
        /// <param name="zt">当前状态 0 未确认 1 缺 2 齐</param>
        /// <returns></returns>
        public string WLQRTJEide(string Id, string type, string zt)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                //只有在未发料的状态下可以出来
                if (model.DD_ZT == 3||model.DD_ZT == 4|| model.DD_ZT == 6)
                {
                    //箱体处理
                    if (type == "0")
                    {
                        if (zt == "1")//缺料
                        {
                            model.xtIsq = 1;//箱体缺
                            model.xtqrtime = DateTime.Now;//相同库存确认时间
        
                            if (model.DD_ZT != 3)
                            {
                                return "1-1";
                            }
                            model.Flzt = 10;//缺料
                            NAHelper.Insertczjl(Id, "箱体库存-缺", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "4");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "4");
                            return "0";//提交成功
                        }
                        if (zt == "2")//料齐
                        {
                            model.xtIsq = 2;//箱体齐
                            model.xtdhtime = DateTime.Now;
                            if (model.qtIsq == 2)//如果其他料也齐 就变成 可生产
                            {
                                //model.DD_ZT = 4;//可生产
                                if (model.Flzt == 20 || model.Flzt == 15)
                                {
                                    model.Flzt = 15;
                                }
                                else
                                {
                                    model.Flzt = 5;//待发料状态
                                }
                                model.wlsqtime = DateTime.Now;//物料双齐的时间
                            }
                            else//如果其他物料未确认或者缺料怎么变成 缺料
                            {

                            }
                            NAHelper.Insertczjl(Id, "箱体库存-齐", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "7");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "7");
                            return "0";//提交成功
                        }
                        return "1";//提交异常
                    }
                    if (type == "1")//其他物料确认
                    {
                        if (zt == "1")//缺料
                        {
                            model.qtIsq = 1;//其他物料缺
                            model.qtqrtime = DateTime.Now;//确认时间
                                                          //model.DD_ZT = 3;//未发料状态
                            if (model.DD_ZT != 3)
                            {
                                return "1-1";
                            }
                            model.Flzt = 10;//缺料
                            NAHelper.Insertczjl(Id, "其他物料库存-缺", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "4");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "4");
                            return "0";//提交成功
                        }
                        if (zt == "2")
                        {
                            model.qtIsq = 2;//其他物料齐
                            model.qtdhtime = DateTime.Now;
                            if (model.xtIsq == 2)//如果相同不缺 就变成 可生产
                            {
                                //model.DD_ZT = 4;
                                if (model.Flzt == 20 || model.Flzt == 15)
                                {
                                    model.Flzt = 15;
                                }
                                else
                                {
                                    model.Flzt = 5;//待发料状态
                                }
                                model.wlsqtime = DateTime.Now;//物料双齐的时间
                            }
                            //else {
                            //    model.DD_ZT = 5;
                            //}
                            NAHelper.Insertczjl(Id, "其他物料库存-齐", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "7");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "7");
                            return "0";//提交成功
                        }
                        return "1";//提交异常
                    }
                    return "4";//处理类型参数缺少
                }
                else
                {
                    return "2";//当前状态不可以处理
                }
            }
            else
            {
                return "3";//订单为空
            }
        }
        #endregion

        #region //发料确认
        public JsonResult FLconfirmEide(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (model.DD_ZT == 3)//只有在未发料的状态下可以确认完成发料
                {
                    if (model.xtIsq!=0&&model.qtIsq!=0)
                    {
                        if(model.Flzt==5)
                        {//待发料的情况
                            model.Flzt = 15;//完成发料
                            NAHelper.Insertczjl(Id, "完成发料", Suser.Id);
                        }
                        if (model.Flzt == 10)
                        {//缺料的情况下
                            model.Flzt = 20;//部分发料
                            NAHelper.Insertczjl(Id, "部分发料", Suser.Id);
                        }
                        model.Flwxtime = DateTime.Now;//完成发料的时间-可以生产的时间
                        model.DD_ZT = 4;//可以生产
                       
                        _IDKX_DDinfoDao.NUpdate(model);
                        IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                        if (list != null)
                        {
                            for (int i = 0, j = list.Count; i < j; i++)
                            {
                                MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "16");
                            }
                        }
                        return Json(new { resule = "success", msg = "操作成功。" });
                    }
                    else
                    {
                        return Json(new { resule = "error", msg = "尚未确认物料是否齐全,无法去发料操作。" });
                    }

                }
                else
                {
                    return Json(new { resule = "error", msg = "当前状态下无法进行该操作。" });
                }
            }
            else
            {
                return Json(new { resule = "error", msg = "该订单不存在。" });
            }
        }
        #endregion
        #endregion

        #endregion

        #region //电控箱下单列表（品保）

        #region //品保部门的电控箱生产列表
        public ActionResult DKXDDPblist(int? pageIndex)
        {
            PagerInfo<DKX_DDinfoView> listmodel = new PagerInfo<DKX_DDinfoView>();
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            if (Session[SessionHelper.DKXSerch] != null)
            {
                DKXDDCXTJView ssearch = Session[SessionHelper.DKXSerch] as DKXDDCXTJView;
                ViewData["DD_Bianhao"] = ssearch.DD_Bianhao;
                ViewData["BJno"] = ssearch.BJno;
                ViewData["DD_Type"] = ssearch.DD_Type;
                ViewData["KHname"] = ssearch.KHname;
                ViewData["Lxname"] = ssearch.Lxname;
                ViewData["Tel"] = ssearch.Tel;
                ViewData["DD_ZT"] = ssearch.DD_ZT;
                ViewData["startctime"] = ssearch.startctime;
                ViewData["endctiome"] = ssearch.endctiome;
                ViewData["DHtype"] = ssearch.DHtype;
                listmodel = GetdkxddPBlistpage(pageIndex, ssearch.DD_Bianhao, ssearch.BJno, ssearch.DD_Type, ssearch.KHname, ssearch.Lxname, ssearch.Tel, ssearch.DD_ZT, ssearch.startctime, ssearch.endctiome, "0", ssearch.DHtype, null);
            }
            else
            {
                listmodel = GetdkxddPBlistpage(pageIndex, null, null, null, null, null, null, null, null, null, "0", null, null);
            }
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult DKXddPBSearchList()
        {
            Session[SessionHelper.DKXSerch] = null;
            Session.Remove(SessionHelper.DKXSerch);
            DKXDDCXTJView view = new DKXDDCXTJView();
            view.DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            view.BJno = Request.Form["txt_BJno"];//报价单号
            view.DD_Type = Request.Form["txtDD_Type"];//订单类型
            view.KHname = Request.Form["txt_KHname"];//客户名称
            view.Lxname = Request.Form["txt_Lxname"];//客户联系人
            view.Tel = Request.Form["txt_Tel"];//联系电话
            view.DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            view.startctime = Request.Form["txt_startctime"];//创建时间
            view.endctiome = Request.Form["txt_endctiome"];//创建时间
            view.DHtype = Request.Form["txt_DHtype"];
            Session[SessionHelper.DKXSerch] = view;
            string YQtype = Request.Form["txtYQtype"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = GetdkxddPBlistpage(pageIndex, view.DD_Bianhao, view.BJno, view.DD_Type, view.KHname, view.Lxname, view.Tel, view.DD_ZT, view.startctime, view.endctiome, "0", view.DHtype, YQtype);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱（品保）生产单列表分页数据
        private PagerInfo<DKX_DDinfoView> GetdkxddPBlistpage(int? pageIndex, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string YQtype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetdkxlistpagePb(DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, start, DHtype, YQtype, Suser);
            return listmodel;
        }
        #endregion

        #region //品保审核页面
        //品保审核页面
        public ActionResult PBshView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //品审提交 //Id 订单Id type 0 通过 1 不通过
        public string PbshEide(string Id, string type, string con)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //查询订单
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.DD_ZT == -3 || model.DD_ZT == 7)//在待品审 和 待发货的 状态下可以操作
                {
                    if (type == "0")//通过
                    {
                        model.pbshzt = 1;//品审通过
                        model.pbshtime = DateTime.Now;
                        model.pdshuserId = Suser.Id;
                        model.DD_ZT = 7;
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(model.Id, "品保审核-通过", Suser.Id);
                            //品保审核通过-通知客服- 发货
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "13");

                            return "3";//提交完成
                        }
                        else
                        {
                            return "4";//提交失败
                        }
                    }
                    else//不通过
                    {

                        model.pbshzt = 2;//品审未通过
                        model.DD_ZT = 6;//变为生产中
                        model.pbshtime = DateTime.Now;
                        model.pdshuserId = Suser.Id;
                        model.pbshbtgyy = QRHelper.ToDBC(con);//审核不通过的原因
                        if (_IDKX_DDinfoDao.NUpdate(model))
                        {
                            NAHelper.Insertczjl(model.Id, "品保审核-不通过", Suser.Id);
                            //品保审核不通过通知生产 （通知生产主管/箱体/其他物料）
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            for (int i = 0, j = list.Count; i < j; i++)
                            {
                                MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "14");
                            }
                            return "5";//提交完成
                        }
                        else
                        {
                            return "6";//提交失败
                        }
                    }
                }
                else
                {
                    return "7";//无法进行品审
                }
            }
            catch
            {
                return "0";//操作异常
            }
        }
        #endregion
        #endregion

        #region //电控箱下单列表（仓库）

        #region //仓库电控箱生产列表
        public ActionResult DKXDDcklist(int? pageIndex)
        {
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            PagerInfo<DKX_DDinfoView> listmodel = GetdkxddCKlistpage(pageIndex, null, null, null, null, null, null, null, null, null, "0", null, null);
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult DKXddckSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            string BJno = Request.Form["txt_BJno"];//报价单号
            string DD_Type = Request.Form["txtDD_Type"];//订单类型
            string KHname = Request.Form["txt_KHname"];//客户名称
            string Lxname = Request.Form["txt_Lxname"];//客户联系人
            string Tel = Request.Form["txt_Tel"];//联系电话
            //string Gcs_nameId = Request.Form["txtGcs_nameId"];//工程师Id
            string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            string startctime = Request.Form["txt_startctime"];//创建时间
            string endctiome = Request.Form["txt_endctiome"];//创建时间
            string start = Request.Form["txt_start"];//是否启用
            string DHtype = Request.Form["txt_DHtype"];
            string YQtype = Request.Form["txtYQtype"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = GetdkxddCKlistpage(pageIndex, DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, "0", DHtype, YQtype);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱（仓库）生产单列表分页数据
        private PagerInfo<DKX_DDinfoView> GetdkxddCKlistpage(int? pageIndex, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string YQtype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetDKXDDlistpageCK(DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, DD_ZT, startctime, endctiome, start, DHtype, YQtype, Suser);
            return listmodel;
        }
        #endregion
        #endregion

        #region //基础数据获取
        //通过客户名称查找客户信息
        public string GetCusinfobykhname(string khname)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_INACustomerinfoDao.GetCusinfobylikekhname(khname));
            return json;
        }

        //通过客户名称查询有kecode 的客户信息
        public string GetCusinfokecodebykhname(string khname)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_INACustomerinfoDao.GetCusinfok3codebylikekhname(khname));
            return json;
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

        #region //电控箱类型下拉数据设置下拉框值(全部数据)
        public void allDKXtypeDropdown(string SelectedPID)
        {
            List<DKX_DDtypeinfoView> CustlistView = _IDKX_DDtypeinfoDao.GetALLdkxtypejson() as List<DKX_DDtypeinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonmodel.Id = "";
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
                ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        //全部工程师的下拉框数据
        #region //全部工程师的下拉框数据
        public void AlGCSdataDropdown(string SelectedPID)
        {
            List<DKX_GCSinfoView> GcslistView = _IDKX_GCSinfoDao.GetALLgcsDATA() as List<DKX_GCSinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (GcslistView != null)
            {
                foreach (var item in GcslistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["GCSDATA"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["GCSDATA"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //全部客服的下拉数据量
        public void ALLKFdataDropdown(string SelectedPID)
        {
            List<SysPersonView> kflistView = _ISysPersonDao.GetPersonbyRolename("'客服专员','客服主管'") as List<SysPersonView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "1";
            Reasonmodel.Name = "请选择";

            Reasonlist.Add(Reasonmodel);
            if (kflistView != null)
            {
                foreach (var item in kflistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.UserName;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["KFDATA"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["KFDATA"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        //工程师的下拉框（通过电控箱类型的Id查找）
        public string GetgcsjsonbydkxtypeId(string type)
        {
            string json = null;
            DKX_DDtypeinfoView model = _IDKX_DDtypeinfoDao.Getdkxtypebytype(type);
            if (model != null)
            {
                IList<DKX_GCSinfoView> listmodel = _IDKX_GCSinfoDao.GetgcsinfobydkxtypeId(model.Id);
                json = JsonConvert.SerializeObject(listmodel);
            }
            return json;
        }
        #endregion

        #region //料单查看
        /// <summary>
        /// 料单查看
        /// </summary>
        /// <param name="oId">订单Id</param>
        /// <param name="glno">关联单号</param>
        /// <param name="type">关联类型 0 报价系统 1K3bom表</param>
        /// <returns></returns>
        public ActionResult NewliaodanView(string oId)
        {
            DKX_DDinfoView model = new DKX_DDinfoView();
            model = _IDKX_DDinfoDao.NGetModelById(oId);
            ViewBag.MyJson = JsonConvert.SerializeObject(_IDKX_pjgdbinfoDao.NGetList());
            return View(model);
        }

        #region //查询关联K3的料单数据
        public string CXk3data(string oId)
        {
            string json = "";
            //查询资料表(料单)
            IList<DKX_ZLDataInfoView> ZLmodellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(oId, "1");
            if (ZLmodellist != null)
            {
                DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                zlmodel = ZLmodellist[0];
                if (zlmodel.Isgl == 2)//关联K3的料单
                {
                    json = JsonConvert.SerializeObject(_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(oId, zlmodel.Bjno));
                }
                if (zlmodel.Isgl == 1)
                {
                    json = JsonConvert.SerializeObject(_IDKX_CONTROL_LIST_DETAILDao.GetliaodanmxbyxqnoandoId(zlmodel.Bjno, oId));
                }
            }
            return json;
        }
        #endregion

        #region //通过物料代理查询物料BOM

        //调用产品BOM以及库存
        public string GetBOMKCinfo(string wl_hm)
        {
            string json;
            json = GetBOM(wl_hm);
            return json;
        }

        //新的返回K3BOM以及库存
        public string GetBOM(string P_bianhao)
        {
            try
            {
                P_bianhao = P_bianhao.Replace("+", "%2B");
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/GetBom?codes=" + P_bianhao + "&key=00BE974F-C52D-434D-AB99-4D9E3796CD81";
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                return sSupplier;
            }
            catch
            {
                return null;
            }
        }

        //返回K3BOM以及库存
        //public DataTable GetBOM(string P_bianhao)
        //{
        //    try
        //    {
        //        Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
        //        string Wldm = P_bianhao;//物料代码
        //        string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
        //        DataTable dt = Dsmodel.GetBom(Wldm, Key);
        //        return dt;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //返回K3BOM以及库存
        //public DataTable GetKcsum(string P_bianhao)
        //{
        //    Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
        //    string Wldm = P_bianhao;//物料代码
        //    string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
        //    DataTable dt = Dsmodel.GetBom(Wldm, Key);
        //    return dt;
        //}
        #endregion
        #endregion

        #region //订单操作记录查看
        //记录页面
        public ActionResult LCjlckView(string oId)
        {
            ViewData["Id"] = oId;
            return View();
        }

        //操作记录
        public string LCCZJLdata(string oId)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_LCCZJLinfoDao.GetlcczjldatabyoId(oId));
            return json;
        }
        #endregion

        //检测产品是否存在库存并且返回库存数量及对应的K3 物料代码
        public DataTable GetKcsum(string P_bianhao)
        {
            try
            {
                Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
                string Wldm = P_bianhao;//物料代码
                string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
                DataTable dt = Dsmodel.GetKuCun(Wldm, Key);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        #region //报价系统接口

        //根据订单号同步需求数据
        public string xuqiuInterface(string ddno, string dd_Id)
        {
            try
            {
                string url;
                url = "http://106.14.40.169:8081/DKXPulice/GetXQDJsonbyORDER_NO?ddno=" + ddno;
                string result = HttpUtility11.GetData(url);
                if (result != "" && result != null)
                {
                    result = "[" + result + "]";
                    List<PAY_CONTROL_INFO> timemodel = getObjectByJson<PAY_CONTROL_INFO>(result);
                    foreach (var t in timemodel)
                    {
                        DKX_PAY_CONTROL_INFOView xuqiumodel = new DKX_PAY_CONTROL_INFOView();
                        xuqiumodel.dd_Id = dd_Id;
                        xuqiumodel.CONTROL_INFO_ID = t.CONTROL_INFO_ID;// 报价系统的自动编号
                        xuqiumodel.CONTROL_INFO_NO = t.CONTROL_INFO_NO;// 编号 
                        xuqiumodel.AREA_ID = t.AREA_ID; // 省份Id
                        xuqiumodel.CUST_NAME = t.CUST_NAME;// 客户名称
                        xuqiumodel.CUST_LEVEL = t.CUST_LEVEL;// 用户等级
                        xuqiumodel.COMPRESSOR_TYPE = t.COMPRESSOR_TYPE;// 机组类型 
                        xuqiumodel.B_TYPE = t.B_TYPE;// 箱体类型 0：横柜 1：竖柜
                        xuqiumodel.COMPRESSOR_BRAND = t.COMPRESSOR_BRAND;// 螺杆压缩机品牌
                        xuqiumodel.COMPRESSOR_MODEL = t.COMPRESSOR_MODEL;//压缩机型号
                        xuqiumodel.RT_TYPE = t.RT_TYPE;//容调关系
                        xuqiumodel.CONTROL_TYPE = t.CONTROL_TYPE;//控制方式
                        xuqiumodel.COM_START_MODE = t.COM_START_MODE;//压缩机启动方式 
                        xuqiumodel.COMPRESSOR_POWER = t.COMPRESSOR_POWER;//压缩机功率
                        xuqiumodel.COMPRESSOR_NUMBER = t.COMPRESSOR_NUMBER;//压缩机数量
                        xuqiumodel.UNIT_TYPE = t.UNIT_TYPE;//机组组合方式  0：双系统  1：独立系统
                        xuqiumodel.COMPRESSOR_ON_OFF = t.COMPRESSOR_ON_OFF;//压机起停方式
                        xuqiumodel.CONDEN_METHOD = t.CONDEN_METHOD;//冷凝方式 
                        xuqiumodel.CONDEN_DL = t.CONDEN_DL;//冷凝器单列
                        xuqiumodel.SP_POWER = t.SP_POWER;//若水冷，水泵功率
                        xuqiumodel.CONDEN_NUM = t.CONDEN_NUM;//数量
                        xuqiumodel.CONDEN_POWER = t.CONDEN_POWER;//功率
                        xuqiumodel.PUMP_NUM = t.PUMP_NUM;//水泵数量
                        xuqiumodel.PUMP_POWER = t.PUMP_POWER;//水泵功率
                        xuqiumodel.FAN_NUM = t.FAN_NUM;//冷凝风机数量
                        xuqiumodel.FAN_POWER = t.FAN_POWER;//冷凝风机功率
                        xuqiumodel.COOL_FAN_NUM = t.COOL_FAN_NUM;//冷却搭风机数量
                        xuqiumodel.COOL_FAN_POWER = t.COOL_FAN_POWER;//冷却搭风机功率 
                        xuqiumodel.ON_OFF = t.ON_OFF;//冷凝起停方式 0：温度 1：压力 2：手动  3：其他
                        xuqiumodel.HOUSE_NUMBER = t.HOUSE_NUMBER;//库房数量
                        xuqiumodel.HOUSE_FJ = t.HOUSE_FJ;//风机库数量
                        xuqiumodel.HOUSE_PG = t.HOUSE_PG;//排管库数量
                        xuqiumodel.EVAPORATION_METHOD = t.EVAPORATION_METHOD;// 蒸发方式
                        xuqiumodel.COOLER_NUM = t.COOLER_NUM;//冷风机数量
                        xuqiumodel.COOLER_POWER = t.COOLER_POWER;//冷风机功率
                        xuqiumodel.COOLER_GROUP = t.COOLER_GROUP;//冷风机分组数
                        xuqiumodel.DEFROS_METHOD = t.DEFROS_METHOD;//化霜方式
                        xuqiumodel.DEFROS_GROUP = t.DEFROS_GROUP;//化霜组数
                        xuqiumodel.COOLER_DEFROS = t.COOLER_DEFROS;//化霜功率   组/个
                        xuqiumodel.IS_AUTO = t.IS_AUTO;//热氟化霜 0:自动  1:手动  2:手自动
                        xuqiumodel.IS_PUMP = t.IS_PUMP;//水泵是否共用  0共用  1：不共用 
                        xuqiumodel.HOUSE_OPEN = t.HOUSE_OPEN;// 库房 0：同开   1：不同开
                        xuqiumodel.ELEC_CHOICE_A = t.ELEC_CHOICE_A;//交接品牌选择 0：正泰 1：施耐德  2：LG 3：默认
                        xuqiumodel.ELEC_CHOICE_C = t.ELEC_CHOICE_C;//空开选择 0：正泰 1：施耐德  2：LG  3：默认
                        xuqiumodel.TOUCH = t.TOUCH;//触摸屏  1：10寸   2：不需要  0:7寸
                        xuqiumodel.OIL_TYPE = t.OIL_TYPE;// 油泵方式  0：单相  1：三相  2：无
                        xuqiumodel.OIL_POWER = t.OIL_POWER;//油泵功率
                        xuqiumodel.HAS_ZKK = t.HAS_ZKK;//是否需要总空开  0：需要  1：不需要
                        xuqiumodel.HAS_GDY = t.HAS_GDY;//是否需要高低压 0：需要   1：不需要  2：捷迈  3：艾默生
                        xuqiumodel.YLQ_TYPE = t.YLQ_TYPE;//油冷却方式 2:板换  0:风冷  1:水冷 3：不需要
                        xuqiumodel.YLQ_NUM = t.YLQ_NUM;//数量
                        xuqiumodel.YLQ_POWER = t.YLQ_POWER;//功率
                        xuqiumodel.HEATING_OIL = t.HEATING_OIL;//油加热
                        xuqiumodel.IS_ALONE = t.IS_ALONE;//末端情况  0：做在一起   1：单独做
                        xuqiumodel.IS_YYC = t.IS_YYC;//电子油压差  0：四线制  1：六线制
                        xuqiumodel.IS_PYZH = t.IS_PYZH;//喷液增焓    0：无   1：有 
                        xuqiumodel.IS_PAIGUAN = t.IS_PAIGUAN;//是否含 针对电控箱即有排管又有风机  1:不含 0：含
                        xuqiumodel.PG_NUM = t.PG_NUM;//排管数量
                        xuqiumodel.IS_REMOTE = t.IS_REMOTE;//远程监控  0:需要   1:不需要 
                        xuqiumodel.IS_OTHER = t.IS_OTHER;//其他要求 0:无   1:分体型   2:冷暖型  3：新亚洲PLC  4：西门子PLC
                        xuqiumodel.FLAG_ORDER = t.FLAG_ORDER;//下单状态  0：未下单 1：已下单
                        xuqiumodel.REMARK = t.REMARK;//备注
                        xuqiumodel.CREATE_BY = t.CREATE_BY;// 创建者
                        xuqiumodel.CREATE_TIME = Convert.ToDateTime(t.CREATE_TIME);//创建时间
                        xuqiumodel.UPDATE_BY = t.UPDATE_BY;//更新者
                        xuqiumodel.UPDATE_TIME = Convert.ToDateTime(t.UPDATE_TIME);//更新时间
                        xuqiumodel.FLAG_XD = t.FLAG_XD;//报价  0：内部  1：电商平台
                        xuqiumodel.ORDER_NO = t.ORDER_NO;// 订单编号
                        xuqiumodel.REMARK2 = t.REMARK2;//备注2
                        _IDKX_PAY_CONTROL_INFODao.Ninsert(xuqiumodel);
                    }
                    return "1";//同步成功
                }
                else
                {
                    return "2";//为空
                }

            }
            catch
            {
                return "0";//同步异常
            }
        }

        //根据订单号同步料单数据
        public string liaodanInterface(string ddno, string ddId)
        {
            try
            {
                string url;
                url = "http://106.14.40.169:8081/DKXPulice/Getliaodanbyxqdno?xqdno=" + ddno;
                string result = HttpUtility11.GetData(url);
                if (result != "" && result != null)
                {
                    result = "[" + result + "]";
                    List<CONTROL_LIST> timemodel = getObjectByJson<CONTROL_LIST>(result);
                    foreach (var t in timemodel)
                    {
                        DKX_CONTROL_LISTView liaodanmodel = new DKX_CONTROL_LISTView();
                        liaodanmodel.dd_Id = ddId;
                        liaodanmodel.LIST_ID = t.LIST_ID;//
                        liaodanmodel.CONTROL_INFO_NO = t.CONTROL_INFO_NO;
                        liaodanmodel.C_DESC = t.C_DESC;
                        liaodanmodel.SUBTOTAL = t.SUBTOTAL;
                        liaodanmodel.SUB_PACK = t.SUB_PACK;//包装运费
                        liaodanmodel.SUB = t.SUB;//小计
                        liaodanmodel.TAXATION = t.TAXATION;//设计费
                        liaodanmodel.DQ = t.DQ;//电器含税费
                        liaodanmodel.KZ = t.KZ;//控制器部份
                        liaodanmodel.FL = t.FL;//
                        liaodanmodel.BZ = t.BZ;
                        liaodanmodel.YF = t.YF;
                        liaodanmodel.ZZF = t.ZZF;
                        liaodanmodel.GLF = t.GLF;
                        liaodanmodel.LR = t.LR;
                        liaodanmodel.SF = t.SF;
                        liaodanmodel.PRICE = t.PRICE;
                        liaodanmodel.CREATE_BY = t.CREATE_BY;
                        liaodanmodel.CREATE_TIME = Convert.ToDateTime(t.CREATE_TIME);
                        liaodanmodel.UPDATE_BY = t.UPDATE_BY;
                        liaodanmodel.UPDATE_TIME = Convert.ToDateTime(t.UPDATE_TIME);
                        liaodanmodel.DIS_PRICE = t.DIS_PRICE;
                        _IDKX_CONTROL_LISTDao.Ninsert(liaodanmodel);
                    }
                }
                return "1";//同步成功
            }
            catch
            {
                return "0";//同步异常
            }
        }

        //更具订单号同步料单明细数据
        public string liandanmxInterface(string ddno, string ddId)
        {
            try
            {
                string url;
                url = "http://106.14.40.169:8081/DKXPulice/Getliaodanmxbyxqdno?xqdno=" + ddno;
                string result = HttpUtility11.GetData(url);
                if (result != "" && result != null)
                {
                    List<CONTROL_LIST_DETAIL> timemodel = getObjectByJson<CONTROL_LIST_DETAIL>(result);
                    foreach (var t in timemodel)
                    {
                        DKX_CONTROL_LIST_DETAILView liaodanmxmodel = new DKX_CONTROL_LIST_DETAILView();
                        liaodanmxmodel.dd_Id = ddId;
                        liaodanmxmodel.DETAIL_ID = t.DETAIL_ID;
                        liaodanmxmodel.CONTROL_INFO_NO = t.CONTROL_INFO_NO;
                        liaodanmxmodel.PARTS_TYPE = t.PARTS_TYPE;
                        liaodanmxmodel.BRAND = t.BRAND;
                        liaodanmxmodel.PARTS_NAME = t.PARTS_NAME;
                        liaodanmxmodel.P_COUNT = t.P_COUNT;
                        liaodanmxmodel.UNIT_PRICE = t.UNIT_PRICE;
                        liaodanmxmodel.PRICE = t.PRICE;
                        liaodanmxmodel.REMARK = t.REMARK;
                        liaodanmxmodel.CREATE_BY = t.CREATE_BY;
                        liaodanmxmodel.CREATE_TIME = Convert.ToDateTime(t.CREATE_TIME);
                        liaodanmxmodel.UPDATE_BY = t.UPDATE_BY;
                        liaodanmxmodel.UPDATE_TIME = Convert.ToDateTime(t.UPDATE_TIME);
                        _IDKX_CONTROL_LIST_DETAILDao.Ninsert(liaodanmxmodel);
                    }
                }
                return "1";//同步成功
            }
            catch
            {
                return "0";//同步异常
            }
        }

        //根据型号获取物料代理
        public string Getwuliaodaimabyxh(string xh)
        {
            try
            {
                string url;
                url = "http://106.14.40.169:8081/DKXPulice/GetPARTS_INFOjsonbyxh?PARTS_NAME=" + xh;
                string result = HttpUtility11.GetData(url);
                return result;
            }
            catch
            {
                return "";
            }
        }

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

        //需求转换实体
        public class PAY_CONTROL_INFO
        {
            /// <summary>
            /// 报价系统的自动编号
            /// </summary>
            public virtual int? CONTROL_INFO_ID { get; set; }

            /// <summary>
            /// 编号
            /// </summary>
            public virtual string CONTROL_INFO_NO { get; set; }

            /// <summary>
            /// 省份Id
            /// </summary>
            public virtual decimal? AREA_ID { get; set; }

            /// <summary>
            /// 客户名称
            /// </summary>
            public virtual string CUST_NAME { get; set; }

            /// <summary>
            /// 用户等级
            /// </summary>
            public virtual decimal? CUST_LEVEL { get; set; }

            /// <summary>
            /// 机组类型 0：活塞并联机控制柜  1：螺杆机控制柜 2:螺杆冷水控制柜  3：末端箱   4:一库一机 5:一机多库 6:一库双机  7：一库多机  8:多库多机(9:两库两机 10：三库三机  11：四机两库)   12：冷水机
            /// 1和2  对应压缩机 螺杆压缩机品牌 其他是 并联机类型
            /// </summary>
            public virtual decimal? COMPRESSOR_TYPE { get; set; }

            /// <summary>
            /// 箱体类型 0：横柜 1：竖柜 
            /// </summary>
            public virtual decimal? B_TYPE { get; set; }

            /// <summary>
            /// 螺杆压缩机品牌  0：比泽尔  1：汉钟  2：复盛 3：谷轮  4：雪梅  5：企鹅 6：丹佛斯  7：来富康  8：富士豪   并联机类型：0：活塞  1：涡旋  
            /// </summary>
            public virtual decimal? COMPRESSOR_BRAND { get; set; }

            /// <summary>
            /// 压缩机型号
            /// </summary>
            public virtual decimal? COMPRESSOR_MODEL { get; set; }

            /// <summary>
            /// 容调关系  1：三段二容调     2：四段三容调  3：四段四容调  4：二段单容调 5：无容调
            /// </summary>
            public virtual decimal? RT_TYPE { get; set; }

            /// <summary>
            /// 控制方式：0：制冷  1：制冷/化霜 2：制冷/化霜/风机    风机是否随压机启停  0：是  1：否
            /// </summary>
            public virtual decimal? CONTROL_TYPE { get; set; }

            /// <summary>
            /// 压缩机启动方式    0：分线圈启动  1：Y-△启动  2：直接启动
            /// </summary>
            public virtual decimal? COM_START_MODE { get; set; }

            /// <summary>
            /// 压缩机功率
            /// </summary>
            public virtual decimal? COMPRESSOR_POWER { get; set; }

            /// <summary>
            /// 压缩机数量
            /// </summary>
            public virtual decimal? COMPRESSOR_NUMBER { get; set; }

            /// <summary>
            /// 机组组合方式  0：双系统  1：独立系统
            /// </summary>
            public virtual decimal? UNIT_TYPE { get; set; }

            /// <summary>
            /// 压机起停方式 0：压力1：温度  2：手动  3：其他
            /// </summary>
            public virtual decimal? COMPRESSOR_ON_OFF { get; set; }

            /// <summary>
            /// 冷凝方式  0：风冷  1：水冷  2：蒸发冷 
            /// </summary>
            public virtual decimal? CONDEN_METHOD { get; set; }

            /// <summary>
            /// 冷凝器单列  0:单列   1:不单列
            /// </summary>
            public virtual decimal? CONDEN_DL { get; set; }

            /// <summary>
            /// 若水冷，水泵功率
            /// </summary>
            public virtual decimal? SP_POWER { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public virtual decimal? CONDEN_NUM { get; set; }

            /// <summary>
            /// 功率
            /// </summary>
            public virtual decimal? CONDEN_POWER { get; set; }

            /// <summary>
            /// 水泵数量
            /// </summary>
            public virtual decimal? PUMP_NUM { get; set; }

            /// <summary>
            /// 水泵功率
            /// </summary>
            public virtual decimal? PUMP_POWER { get; set; }

            /// <summary>
            /// 冷凝风机数量
            /// </summary>
            public virtual decimal? FAN_NUM { get; set; }

            /// <summary>
            /// 冷凝风机功率
            /// </summary>
            public virtual decimal? FAN_POWER { get; set; }

            /// <summary>
            /// 冷却搭风机数量
            /// </summary>
            public virtual decimal? COOL_FAN_NUM { get; set; }

            /// <summary>
            /// 冷却搭风机功率 
            /// </summary>
            public virtual decimal? COOL_FAN_POWER { get; set; }

            /// <summary>
            /// 冷凝起停方式 0：温度 1：压力 2：手动  3：其他
            /// </summary>
            public virtual decimal? ON_OFF { get; set; }

            /// <summary>
            /// 库房数量
            /// </summary>
            public virtual decimal? HOUSE_NUMBER { get; set; }

            /// <summary>
            /// 风机库数量
            /// </summary>
            public virtual decimal? HOUSE_FJ { get; set; }

            /// <summary>
            /// 排管库数量
            /// </summary>
            public virtual decimal? HOUSE_PG { get; set; }

            /// <summary>
            /// 蒸发方式0：冷风机  1：排管  2:风机/排管
            /// </summary>
            public virtual decimal? EVAPORATION_METHOD { get; set; }


            /// <summary>
            /// 冷风机数量
            /// </summary>
            public virtual decimal? COOLER_NUM { get; set; }

            /// <summary>
            /// 冷风机功率
            /// </summary>
            public virtual decimal? COOLER_POWER { get; set; }

            /// <summary>
            /// 冷风机分组数
            /// </summary>
            public virtual decimal? COOLER_GROUP { get; set; }

            /// <summary>
            /// 化霜方式 0：电热化霜  1：水冲霜    2：热氟化霜  3：风机化霜  4：未选择
            /// </summary>
            public virtual decimal? DEFROS_METHOD { get; set; }

            /// <summary>
            /// 化霜组数
            /// </summary>
            public virtual decimal? DEFROS_GROUP { get; set; }

            /// <summary>
            /// 化霜功率   组/个
            /// </summary>
            public virtual decimal? COOLER_DEFROS { get; set; }

            /// <summary>
            /// 热氟化霜 0:自动  1:手动  2:手自动
            /// </summary>
            public virtual decimal? IS_AUTO { get; set; }

            /// <summary>
            /// 水泵是否共用  0共用  1：不共用 
            /// </summary>
            public virtual decimal? IS_PUMP { get; set; }

            /// <summary>
            /// 库房 0：同开   1：不同开
            /// </summary>
            public virtual decimal? HOUSE_OPEN { get; set; }

            /// <summary>
            /// 交接品牌选择 0：正泰 1：施耐德  2：LG 3：默认
            /// </summary>
            public virtual decimal? ELEC_CHOICE_A { get; set; }

            /// <summary>
            /// 空开选择 0：正泰 1：施耐德  2：LG  3：默认
            /// </summary>
            public virtual decimal? ELEC_CHOICE_C { get; set; }

            /// <summary>
            /// 触摸屏  1：10寸   2：不需要  0:7寸
            /// </summary>
            public virtual decimal? TOUCH { get; set; }

            /// <summary>
            /// 油泵方式  0：单相  1：三相  2：无
            /// </summary>
            public virtual decimal? OIL_TYPE { get; set; }

            /// <summary>
            /// 油泵功率
            /// </summary>
            public virtual decimal? OIL_POWER { get; set; }

            /// <summary>
            /// 是否需要总空开  0：需要  1：不需要
            /// </summary>
            public virtual decimal? HAS_ZKK { get; set; }

            /// <summary>
            /// 是否需要高低压 0：需要   1：不需要  2：捷迈  3：艾默生
            /// </summary>
            public virtual decimal? HAS_GDY { get; set; }

            /// <summary>
            /// 油冷却方式 2:板换  0:风冷  1:水冷 3：不需要
            /// </summary>
            public virtual decimal? YLQ_TYPE { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public virtual decimal? YLQ_NUM { get; set; }

            /// <summary>
            /// 功率
            /// </summary>
            public virtual decimal? YLQ_POWER { get; set; }

            /// <summary>
            /// 油加热    0:需要  1：不需要
            /// </summary>
            public virtual decimal? HEATING_OIL { get; set; }

            /// <summary>
            /// 末端情况  0：做在一起   1：单独做
            /// </summary>
            public virtual decimal? IS_ALONE { get; set; }

            /// <summary>
            /// 电子油压差  0：四线制  1：六线制
            /// </summary>
            public virtual decimal? IS_YYC { get; set; }

            /// <summary>
            /// 喷液增焓    0：无   1：有 
            /// </summary>
            public virtual decimal? IS_PYZH { get; set; }

            /// <summary>
            /// 是否含 针对电控箱即有排管又有风机  1:不含 0：含
            /// </summary>
            public virtual decimal? IS_PAIGUAN { get; set; }

            /// <summary>
            /// 排管数量
            /// </summary>
            public virtual decimal? PG_NUM { get; set; }

            /// <summary>
            /// 远程监控  0:需要   1:不需要 
            /// </summary>
            public virtual decimal? IS_REMOTE { get; set; }

            /// <summary>
            /// 其他要求 0:无   1:分体型   2:冷暖型  3：新亚洲PLC  4：西门子PLC
            /// </summary>
            public virtual decimal? IS_OTHER { get; set; }

            /// <summary>
            /// 下单状态  0：未下单 1：已下单
            /// </summary>
            public virtual decimal? FLAG_ORDER { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public virtual string REMARK { get; set; }

            /// <summary>
            /// 创建者
            /// </summary>
            public virtual decimal? CREATE_BY { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public virtual string CREATE_TIME { get; set; }

            /// <summary>
            /// 更新者
            /// </summary>
            public virtual decimal? UPDATE_BY { get; set; }

            /// <summary>
            /// 更新时间
            /// </summary>
            public virtual string UPDATE_TIME { get; set; }

            /// <summary>
            /// 报价  0：内部  1：电商平台
            /// </summary>
            public virtual decimal? FLAG_XD { get; set; }

            /// <summary>
            /// 订单编号
            /// </summary>
            public virtual string ORDER_NO { get; set; }

            /// <summary>
            /// 备注2
            /// </summary>
            public virtual string REMARK2 { get; set; }
        }

        //料单转换实体
        public class CONTROL_LIST
        {
            /// <summary>
            /// RECORD ID，自動編號
            /// </summary>
            public virtual string LIST_ID { get; set; }

            /// <summary>
            /// 需求NO
            /// </summary>
            public virtual string CONTROL_INFO_NO { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            public virtual string C_DESC { get; set; }

            /// <summary>
            /// 箱柜价格
            /// </summary>
            public virtual float? SUBTOTAL { get; set; }

            /// <summary>
            /// 包装及运费
            /// </summary>
            public virtual float? SUB_PACK { get; set; }

            /// <summary>
            /// 小计
            /// </summary>
            public virtual float? SUB { get; set; }

            /// <summary>
            /// 设计费
            /// </summary>
            public virtual float? TAXATION { get; set; }

            /// <summary>
            /// 电器含税费
            /// </summary>
            public virtual float? DQ { get; set; }

            /// <summary>
            /// 控制器部份
            /// </summary>
            public virtual float? KZ { get; set; }

            /// <summary>
            /// 辅料
            /// </summary>
            public virtual float? FL { get; set; }

            /// <summary>
            /// 包装费
            /// </summary>
            public virtual float? BZ { get; set; }

            /// <summary>
            /// 运费
            /// </summary>
            public virtual float? YF { get; set; }

            /// <summary>
            /// 组装调试费
            /// </summary>
            public virtual float? ZZF { get; set; }

            /// <summary>
            /// 现场管理费
            /// </summary>
            public virtual float? GLF { get; set; }

            /// <summary>
            /// 利润
            /// </summary>
            public virtual float? LR { get; set; }

            /// <summary>
            /// 税费
            /// </summary>
            public virtual float? SF { get; set; }

            /// <summary>
            /// 总价
            /// </summary>
            public virtual float? PRICE { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public virtual string REMARK { get; set; }

            /// <summary>
            /// 创建者
            /// </summary>
            public virtual string CREATE_BY { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public virtual string CREATE_TIME { get; set; }

            /// <summary>
            /// 更新者
            /// </summary>
            public virtual string UPDATE_BY { get; set; }

            /// <summary>
            /// 更新时间
            /// </summary>
            public virtual string UPDATE_TIME { get; set; }

            /// <summary>
            /// 优惠价
            /// </summary>
            public virtual float? DIS_PRICE { get; set; }
        }

        //料单明细转换实体
        public class CONTROL_LIST_DETAIL
        {
            /// <summary>
            /// RECORD ID，自動編號
            /// </summary>
            public virtual decimal? DETAIL_ID { get; set; }

            /// <summary>
            /// 电控箱NO 需求NO
            /// </summary>
            public virtual string CONTROL_INFO_NO { get; set; }

            /// <summary>
            /// 品名类型  0：箱体 1：接触器 2：断路器 3：电机保护器 4：指示灯 
            /// 5：蜂鸣器  6：急停按钮  7：触摸屏  8：接线端子 9：接线柱零线排 
            /// 10：中间继电器  11：转换开头 12：微动开头13：辅助触头 14：线槽 
            /// 15：风扇  16：常用控制器     
            /// 17：专用控制器  18：压力传感器   19：保险丝座  20：时间继电器   
            /// 21：相序保护器  22：PLC  23：温度传感器  24：开关电源    
            /// 25：通信模块 26：传感器线  27：辅助配件
            /// </summary>
            public virtual decimal? PARTS_TYPE { get; set; }

            /// <summary>
            /// 品牌  0：正泰 1：施耐德  2：LG 3：西门子 4：欣灵 5：艾默生 
            /// 6：昆仑通态  7：其他  8：新亚洲 9:亿维  10：九纯健  11：国达 
            /// 12：江阴长江  13：联捷 14：升亚 15：明伟  16:威纶通  17:捷迈
            /// </summary>
            public virtual decimal? BRAND { get; set; }

            /// <summary>
            /// 型号
            /// </summary>
            public virtual string PARTS_NAME { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public virtual decimal? P_COUNT { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public virtual decimal? UNIT_PRICE { get; set; }

            /// <summary>
            /// 总价
            /// </summary>
            public virtual decimal? PRICE { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public virtual string REMARK { get; set; }

            /// <summary>
            /// 创建者
            /// </summary>
            public virtual string CREATE_BY { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public virtual string CREATE_TIME { get; set; }

            /// <summary>
            /// 更新者
            /// </summary>
            public virtual string UPDATE_BY { get; set; }

            /// <summary>
            /// 更新时间
            /// </summary>
            public virtual string UPDATE_TIME { get; set; }
        }

        #endregion

        #region //K3系统料单接口
        public string k3bomInterface(string FnumberBom, string ddId)
        {
            try
            {
                string FnumberBomstr = FnumberBom.Replace("+", "%2B");
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/getBomBodyByFBomnumber?fbomnumber=" + FnumberBomstr;
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                List<KBOMjsonmodel> timemodel = getObjectByJson<KBOMjsonmodel>(sSupplier);
                foreach (var a in timemodel)
                {
                    DKX_k3BominfoView model = new DKX_k3BominfoView();
                    model.FnumberBom = FnumberBom;
                    model.dd_Id = ddId;
                    model.FNumber = a.FNumber;//物料代码
                    model.FItemName = a.FItemName;//物料名称
                    model.FModel = a.FModel;//型号
                    model.FBaseUnitID = a.FBaseUnitID;//单位
                    model.FAuxQty = Convert.ToDecimal(a.FAuxQty);//单位用量
                    model.C_time = DateTime.Now;
                    model.Beizhu = a.FNote;//备注
                    _IDKX_k3BominfoDao.Ninsert(model);
                }
                return "2";
            }
            catch
            {
                return "0";
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
        #endregion

        #region //元器件请购单

        //元器件采购页面
        public ActionResult YQJQGView(string YQJBM, string YQJName, string YQJxh, string shuliang)
        {
            ViewData["YQJBM"] = YQJBM;
            ViewData["YQJName"] = YQJName;
            ViewData["YQJxh"] = YQJxh;
            ViewData["shuliang"] = shuliang;
            return View();
        }
        //采购提交
        public string CGtjEide(string YQJBM, string YQJName, string YQJxh, string shuliang)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_PleasepurchaseinfoView model = new DKX_PleasepurchaseinfoView();
                model.Yqj_bianhao = YQJBM;
                model.Yqj_Name = YQJName;
                model.Yqj_Namexh = YQJxh;
                model.Cg_shuliang = Convert.ToDecimal(shuliang);
                model.C_time = DateTime.Now;
                model.C_Name = Suser.Id;
                model.Type = 0;
                bool faly = _IDKX_PleasepurchaseinfoDao.Ninsert(model);
                if (faly)
                    return "0";//提交成功
                else
                    return "1";//提交异常
            }
            catch
            {
                return "1";//提交异常
            }
        }
        //叠加采购的页面
        public ActionResult DJYQJQGView(string YQJBM, string YQJName, string YQJxh, string shuliang)
        {
            ViewData["YQJBM"] = YQJBM;
            ViewData["YQJName"] = YQJName;
            ViewData["YQJxh"] = YQJxh;
            ViewData["shuliang"] = shuliang;
            return View();
        }
        //叠加采购的页面
        public string DJCGtjEide(string YQJBM, string shuliang)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_PleasepurchaseinfoView model = new DKX_PleasepurchaseinfoView();
                model = _IDKX_PleasepurchaseinfoDao.GetYQJQgdanDATAbyyqjbm(YQJBM, "0");
                if (model != null)
                {
                    model.Cg_shuliang = model.Cg_shuliang + Convert.ToDecimal(shuliang);
                    bool faly = _IDKX_PleasepurchaseinfoDao.NUpdate(model);
                    if (faly)
                        return "0";//提交成功
                    else
                        return "1";//提交异常
                }
                else
                {
                    return "2";//不存在待采购的同种元器件
                }
            }
            catch
            {
                return "1";//提交异常
            }
        }
        //检测是否存在有未采购的订单根据元器件物料编码
        public string YQJcgjc(string YQJBM)
        {
            //先检测是否存在未采购订单
            if (_IDKX_PleasepurchaseinfoDao.GetYQJQgdanDATAbyyqjbm(YQJBM, "0") != null)
            {
                return "0";//需要叠加采购
            }
            //检测是否有采购中的订单
            if (_IDKX_PleasepurchaseinfoDao.GetYQJQgdanDATAbyyqjbm(YQJBM, "1") != null)
            {
                return "1";//不能采购
            }
            return "2";//可以新建采购单
        }

        #region //请购单列表（ ）

        #region //采购单列表（生产）
        public ActionResult SCQGDList(int? pageIndex)
        {
            PagerInfo<DKX_PleasepurchaseinfoView> listmodel = GetSCcgdPagelist(pageIndex, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //采购单列表（采购）
        public ActionResult CGQGDList(int? pageIndex)
        {
            PagerInfo<DKX_PleasepurchaseinfoView> listmodel = GetSCcgdPagelist(pageIndex, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //多条件查询
        public JsonResult CGDSearchList()
        {
            string yqjbm = Request.Form["txtyqjbm"];
            string yqjname = Request.Form["txtyqjname"];
            string yqjxh = Request.Form["txtyqjxh"];
            string type = Request.Form["txttype"];
            string starttime = Request.Form["txtstarttime"];
            string endtime = Request.Form["txtendtime"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_PleasepurchaseinfoView> listmodel = GetSCcgdPagelist(pageIndex, yqjbm, yqjname, yqjxh, type, starttime, endtime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //请购单分页数据
        /// <summary>
        /// 请购单分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="yqjbm">元器件编码</param>
        /// <param name="yqjname">元器件名称</param>
        /// <param name="yqjxh">元器件型号</param>
        /// <param name="type"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        private PagerInfo<DKX_PleasepurchaseinfoView> GetSCcgdPagelist(int? pageIndex, string yqjbm, string yqjname, string yqjxh, string type, string starttime, string endtime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_PleasepurchaseinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_PleasepurchaseinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_PleasepurchaseinfoView> listmodel = _IDKX_PleasepurchaseinfoDao.GetDKX_SCcgdpagelist(yqjbm, yqjname, yqjxh, type, starttime, endtime, Suser);
            return listmodel;
        }
        #endregion

        #region //采购接单
        /// <summary>
        /// 采购接单页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult CGJDView(string Id)
        {
            ViewData["Id"] = Id;
            ViewData["yjtime"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        //采购接单方法提交
        public string CGJDEide(string Id, string yjdatetime)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_PleasepurchaseinfoView model = new DKX_PleasepurchaseinfoView();
                model = _IDKX_PleasepurchaseinfoDao.NGetModelById(Id);
                model.Jqtime = Convert.ToDateTime(yjdatetime);
                model.Cg_name = Suser.Id;
                model.Type = 1;
                bool flay = false;
                flay = _IDKX_PleasepurchaseinfoDao.NUpdate(model);
                if (flay)
                    return "1";//提交正常
                else
                    return "2";//提交失败
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //采购完成采购
        /// <summary>
        /// 采购完成采购页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult CGWXCGView(string Id)
        {
            ViewData["Id"] = Id;
            ViewData["wctime"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        //采购完成提交方法
        public string CGWCEide(string Id, string wctime, string rkshuliang)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_PleasepurchaseinfoView model = new DKX_PleasepurchaseinfoView();
                model = _IDKX_PleasepurchaseinfoDao.NGetModelById(Id);
                model.Wc_datetime = Convert.ToDateTime(wctime);
                model.Cz_name = Suser.Id;
                model.Cz_time = DateTime.Now;
                model.Sjcgsl = Convert.ToDecimal(rkshuliang);
                model.Type = 2;
                bool flay = false;
                flay = _IDKX_PleasepurchaseinfoDao.NUpdate(model);
                if (flay)
                    return "1";//提交正常
                else
                    return "2";//提交失败
            }
            catch
            {
                return "0";
            }
        }

        #endregion
        #endregion
        #endregion

        #region //电控箱产品方案库

        #region //电控箱产品（工程师）

        #region //电控箱产品列表（）
        public ActionResult DKXcplistView(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            //判定是否有审核的权限
            ViewData["SHQX"] = Getzlshqx();
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, null, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //电控箱产品条件查询
        public JsonResult DkxcpSearchList()
        {
            string cpname = Request.Form["txtcpname"];//产品型号名称
            string gl = Request.Form["txtgl"];//功率
            string dw = Request.Form["txtdw"];//单位
            string Type = Request.Form["txtType"];//产品类型 0 小系统 1 大系统 2 物联网
            string ft = Request.Form["txtft"];//分体 0一体1 分体
            string Gcs_name = Request.Form["txtGcs_name"];//工程师
            string gnjs = Request.Form["txtgnjs"];//
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //电控箱产品分页数据
        private PagerInfo<DKX_CPInfoView> GetDkxcplistpage(int? pageIndex, string cpname, string gl, string dw, string Type, string ft, string Gcs_name, string gnjs)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_CPInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_CPInfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_CPInfoView> listmodel = _IDKX_CPInfoDao.GetALLDKXcppagelist(cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            return listmodel;
        }
        #endregion

        #region //工程师资料库资料编辑
        //资料编辑的页面
        public ActionResult GCSRKzlbjView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //主管工程师审核
        //入库资料审核页面
        public ActionResult FAKZLSHView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //主管工程师审核提交
        public string FAKZLSHEide(string Id, string type)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //查询电控箱产品
                DKX_CPInfoView model = _IDKX_CPInfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//订单不存在
                if (model.IsDis == 2)
                {
                    if (type == "0")//通过
                    {
                        model.IsDis = 0;
                        model.RK_time = DateTime.Now;
                        if (_IDKX_CPInfoDao.NUpdate(model))
                        {
                            return "4";//提交成功
                        }
                        else
                        {
                            return "5";//提交异常
                        }
                    }
                    else//审核不通过
                    {
                        model.IsDis = 1;//异常
                        if (_IDKX_CPInfoDao.NUpdate(model))
                        {
                            return "4";//提交成功
                        }
                        else
                        {
                            return "5";//提交异常
                        }
                    }
                }
                else
                {
                    return "2";//当前状态无需审核
                }
            }
            catch
            {
                return "0";//操作异常
            }
        }
        #endregion

        #endregion

        #region //电控箱方案库管理(品保)

        #region //电控箱产品
        public ActionResult PBDKXCPlist(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, null, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        public JsonResult PBDKXCPSearchList()
        {
            string cpname = Request.Form["txtcpname"];//产品名称
            string gl = Request.Form["txtgl"];//功率
            string dw = Request.Form["txtdw"];//单位
            string Type = Request.Form["txtType"];//产品类型 0 小系统 1 大系统 2 物联网
            string ft = Request.Form["txtft"];//分体 0一体1 分体
            string Gcs_name = Request.Form["txtGcs_name"];//工程师
            string gnjs = Request.Form["txtgnjs"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //方案库的数据列表
        private PagerInfo<DKX_CPInfoView> GetDKXCPInfolistpage(int? pageIndex, string cpname, string gl, string dw, string Type, string ft, string Gcs_name, string gnjs)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_CPInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_CPInfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_CPInfoView> listmodel = _IDKX_CPInfoDao.GetALLDKXcppagelist(cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            return listmodel;
        }
        #endregion

        #region //方案整改提交页面
        public ActionResult PBDKXCPZGView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //整改提交方法
        public string PBZGEide(string Id, string zgyy)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_CPInfoView model = _IDKX_CPInfoDao.NGetModelById(Id);
                if (model.IsDis == 0)//正常状态下可以提交整改
                {
                    model.IsDis = 1;//异常状态
                    model.zgyy = zgyy;
                    model.zgtime = DateTime.Now;
                    model.zgname = Suser.Id;
                    if (_IDKX_CPInfoDao.NUpdate(model))
                    {
                        return "0";//提交成功
                    }
                    else
                    {
                        return "1";//提交失败
                    }
                }
                else
                {
                    return "3";//产品当前状态不可以提交整改
                }
            }
            catch
            {
                return "4";//提交失败
            }
        }
        #endregion

        #endregion

        #region //电控箱方案库（公共）
        #region //列表页面
        public ActionResult FAKpubliclist(int? pageIndex)
        {
            AlGCSdataDropdown(null);
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, null, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //查询条件
        public JsonResult FAKpublicSearchList()
        {
            string cpname = Request.Form["txtcpname"];//产品名称
            string gl = Request.Form["txtgl"];//功率
            string dw = Request.Form["txtdw"];//单位
            string Type = Request.Form["txtType"];//产品类型 0 小系统 1 大系统 2 物联网
            string ft = Request.Form["txtft"];//分体 0一体1 分体
            string Gcs_name = Request.Form["txtGcs_name"];//工程师
            string gnjs = Request.Form["txtgnjs"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_CPInfoView> listmodel = GetDKXCPInfolistpage(pageIndex, cpname, gl, dw, Type, ft, Gcs_name, gnjs);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion
        #endregion

        #region //电控箱资料查看
        //查看页面
        public ActionResult FAKzlckView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        //获取入库资料数据
        public string GetRKziliaojsonbycpIdandtype(string cpId, string type)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(cpId, type));
            return json;
        }
        //根据产品Id 查询所有入库资料
        public string GetallRKziliaobycpid(string cpId)
        {
            try {
                string json = "";
                json = JsonConvert.SerializeObject(_IDKX_RKZLDataInfoDao.GetDKXCPZLdatalist(cpId));
                return json;
            } catch { return ""; }
        }
        #endregion

        #region //通过产品Id 查找产品数据
        public string RLCPjson(string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_CPInfoDao.NGetModelById(Id));
            return json;
        }
        #endregion

        #region //入库资料删除（逻辑删除）
        public string FAKziliaodel(string Id)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                DKX_RKZLDataInfoView model = _IDKX_RKZLDataInfoDao.NGetModelById(Id);
                model.Start = 1;
                model.D_datetime = DateTime.Now;//删除时间
                model.D_name = Suser.Id;//删除人
                _IDKX_RKZLDataInfoDao.NUpdate(model);
                string czcao = "方案库整改-删除上传资料:" + model.Zl_type.ToString();
                NAHelper.Insertczjl(Id, czcao, Suser.Id);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region //资料库 需求上传
        [HttpPost]
        public JsonResult FAK_xuqiuuploadEide(FormContext form, HttpPostedFileBase image1)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];//产品Id
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image1 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image1.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/xuqiu"), fileName);
                image1.SaveAs(filePath);
                model.wjurl = "/Content/upload/xuqiu/" + fileName;
                model.wjName = Path.GetFileName(image1.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 0;//需求
            model.dd_Id = "";
            model.Start = 0;//启用
            model.C_time = DateTime.Now;
            model.Isgl = 0;//附件
            model.cpId = Id;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传需求资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //关联K3 BOM
        public string FAKliaodanglk3(string Id, string FnumberBom)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //检查是否存在该料单
                if (_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, FnumberBom) == null)//不存在
                {
                    string k3liaodan = k3bomInterface(FnumberBom, Id);
                    if (k3liaodan == "0")
                        return "3";//不存在料单
                }
                DKX_CPInfoView cpmodel = _IDKX_CPInfoDao.NGetModelById(Id);
                if (cpmodel != null)
                {
                    DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
                    model = _IDKX_RKZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1");
                    if (model == null)
                    {
                        model = new DKX_RKZLDataInfoView();
                        model.BomNo = FnumberBom;
                        model.cpId = Id;
                        model.dd_Id = "";
                        model.Isgl = 2;
                        model.Zl_type = 1;//料单
                        model.C_time = DateTime.Now;
                        model.Start = 0;//启用
                        _IDKX_RKZLDataInfoDao.Ninsert(model);
                    }
                    else
                    {
                        model.Start = 1;
                        _IDKX_RKZLDataInfoDao.NUpdate(model);
                        model = new DKX_RKZLDataInfoView();
                        model.BomNo = FnumberBom;
                        model.cpId = Id;
                        model.dd_Id = "";
                        model.Isgl = 2;
                        model.Zl_type = 1;//料单
                        model.C_time = DateTime.Now;
                        model.Start = 0;//启用
                        _IDKX_RKZLDataInfoDao.Ninsert(model);
                    }
                    NAHelper.Insertczjl(Id, "关联K3BOM：" + FnumberBom, Suser.Id);
                    return "2";//成功
                }
                else
                {
                    return "1";//为空
                }

            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //资料库 图纸上传（箱体图）
        [HttpPost]
        public JsonResult FAK_XTTtuziuploadEide(FormContext form, HttpPostedFileBase image3)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image3 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image3.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/tuzhi"), fileName);
                image3.SaveAs(filePath);
                model.wjurl = "/Content/upload/tuzhi/" + fileName;
                model.wjName = Path.GetFileName(image3.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 2;//图纸（箱体图）
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(箱体图)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 图纸上传 （电器排布图）
        [HttpPost]
        public JsonResult FAK_DQPBtuzhiEide(FormContext form, HttpPostedFileBase image5)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image5 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image5.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/DQPBtuzhi"), fileName);
                image5.SaveAs(filePath);
                model.wjurl = "/Content/upload/DQPBtuzhi/" + fileName;
                model.wjName = Path.GetFileName(image5.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 6;//电器排布图
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(电器排布图)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 图纸上传（电器原理图）
        [HttpPost]
        public JsonResult FAK_DQYLtuzhiloadEide(FormContext form, HttpPostedFileBase image4)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image4 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image4.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/QTtuzhi"), fileName);
                image4.SaveAs(filePath);
                model.wjurl = "/Content/upload/QTtuzhi/" + fileName;
                model.wjName = Path.GetFileName(image4.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 5;//其他图纸(电器原理图)
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(电器原理图)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 图纸上传（线号表）
        [HttpPost]
        public JsonResult FAK_XTJtuzhiEide(FormCollection form, HttpPostedFileBase image6)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image6 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image6.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/XTJTtuzhi"), fileName);
                image6.SaveAs(filePath);
                model.wjurl = "/Content/upload/XTJTtuzhi/" + fileName;
                model.wjName = Path.GetFileName(image6.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 10;//线号表
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(系统简图)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 烧录程序上传
        [HttpPost]
        public JsonResult FAK_RJCXEide(FormCollection form, HttpPostedFileBase image7)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image7 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image7.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
                image7.SaveAs(filePath);
                model.wjurl = "/Content/upload/rjcxtuzhi/" + fileName;
                model.wjName = Path.GetFileName(image7.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 8;//软件程序
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(烧录程序)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 源程序序上传
        [HttpPost]
        public JsonResult FAK_RJYCXEide(FormCollection form, HttpPostedFileBase image9)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image9 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image9.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/rjcxtuzhi"), fileName);
                image9.SaveAs(filePath);
                model.wjurl = "/Content/upload/rjcxtuzhi/" + fileName;
                model.wjName = Path.GetFileName(image9.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 11;//软件程序-源程序（仅仅plc 项目有）
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(源程序)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //资料库 操作手册上传
        [HttpPost]
        public JsonResult FAK_czscuploadEide(FormCollection form, HttpPostedFileBase image8)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id = Request.Form["Id"];
            DKX_RKZLDataInfoView model = new DKX_RKZLDataInfoView();
            if (image8 != null)
            {
                string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image8.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Content/upload/czshouce"), fileName);
                image8.SaveAs(filePath);
                model.wjurl = "/Content/upload/czshouce/" + fileName;
                model.wjName = Path.GetFileName(image8.FileName);
            }
            else
            {
                return Json(new { result = "error1" });
            }
            model.Zl_type = 9;//操作手册
            model.dd_Id = "";
            model.cpId = Id;
            model.Start = 0;
            model.Isgl = 0;
            model.C_time = DateTime.Now;
            flay = _IDKX_RKZLDataInfoDao.Ninsert(model);
            NAHelper.Insertczjl(Id, "入库资料—上传图纸(操作手册)资料", Suser.Id);
            if (flay)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //工程师完成整改-提交待审核 （提交正常）
        public string FAK_WCZGtjshEide(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                DKX_CPInfoView model = _IDKX_CPInfoDao.NGetModelById(Id);
                if (model.IsDis == 1 || model.IsDis == 2 || model.IsDis == 3)//产品状态在 异常和待审核的状态下 可以提交
                {
                    //检测需求
                    if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "0") == null)
                        return "2-1";
                    //料单
                    if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "1") == null)
                        return "2-2";
                    //检测图纸（箱体图）
                    if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "2") == null)
                        return "2";//箱体图缺失
                    //检测图纸（其他图纸/电器原理图）
                    if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "5") == null)
                        return "6";////其他图纸缺失 电器原理图
                    //检测电器分布图
                    if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "6") == null)
                        return "7";//电器分布图缺失
                    if (model.Type > 3)
                    { //plc项目
                        //检测烧录软件
                        if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "8") == null)
                            return "8-1";//检测烧录软件缺少
                        //检测源程序
                        if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "11") == null)
                            return "8-2";//检测源程序缺少
                        //检测操作手册
                        if (_IDKX_RKZLDataInfoDao.GetRKzljsonbyId(Id, "11") == null)
                            return "8-3";//检测操作手册缺少

                    }
                    model.IsDis = 0;//启用 （工程师整该之后 直接提交 数据状态正常）
                    model.RK_time = DateTime.Now;
                    if (_IDKX_CPInfoDao.NUpdate(model))
                    {
                        return "0";//提交成功
                    }
                    else
                    {
                        return "1";//提交成功
                    }
                }
                else
                {
                    return "5";//不要提交
                }
            }
            catch
            {
                return "4";//提交异常
            }
        }
        #endregion

        #region //工程师添加现成方案
        //基础数据添加页面
        #region //基础数据编辑
        public ActionResult FAKaddxcfajcView()
        {
            AldkxtypeDropdown(null);//电控箱类型
            return View();
        }

        //基础数据提交
        public string FAKaddjcEide(string ddxh, string gl, string dw, string dkxtype, string wlwtyep, string gcscon, string gnjsstr)
        {
            try
            {
                DKX_CPInfoView model = new DKX_CPInfoView();
                model = _IDKX_CPInfoDao.Getcpdatabynameandpowanddw(ddxh, gl, dw, gnjsstr);
                if (model != null)
                    return "1";//存在相同的产品
                model = new DKX_CPInfoView();
                model.Cpname = ddxh;
                model.Power = gl;
                model.DW = dw;
                model.Type = Convert.ToInt32(dkxtype);
                if (wlwtyep == "")
                    wlwtyep = "0";
                model.Isft = Convert.ToInt32(wlwtyep);
                model.IsDis = 3;//未提交审核
                model.RK_time = DateTime.Now;//创建时间
                model.Lytype = 0;
                model.cplytype = 1;
                model.Gcs_name = gcscon;
                model.cpgnjs = gnjsstr;
                if (_IDKX_CPInfoDao.Ninsert(model))
                    return "2";//提交成功
                else
                    return "3";//提交失败
            }
            catch
            {
                return "0";//提交异常
            }
        }

        //基础数据修改页面
        public ActionResult FAKaddxcfajcupdateView(string Id)
        {
            ViewData["Id"] = Id;
            AldkxtypeDropdown(null);//电控箱类型
            DKX_CPInfoView model = new DKX_CPInfoView();
            model = _IDKX_CPInfoDao.NGetModelById(Id);
            return View(model);
        }
        //基础数据修改提交
        public string FAKaddjcupdateEide(string Id, string ddxh, string gl, string dw, string dkxtype, string wlwtyep, string gcscon, string gnjsstr)
        {
            try
            {
                DKX_CPInfoView model = new DKX_CPInfoView();
                model = _IDKX_CPInfoDao.NGetModelById(Id);
                if (model == null)
                    return "1";//不存在该订单
                model.Cpname = ddxh;
                model.Power = gl;
                model.DW = dw;
                model.Type = Convert.ToInt32(dkxtype);
                if (wlwtyep == "")
                    wlwtyep = "0";
                model.Isft = Convert.ToInt32(wlwtyep);
                model.IsDis = 3;//未提交审核
                model.RK_time = DateTime.Now;//创建时间
                model.Lytype = 0;
                model.cplytype = 1;
                model.Gcs_name = gcscon;
                model.cpgnjs = gnjsstr;
                if (_IDKX_CPInfoDao.NUpdate(model))
                    return "2";//提交成功
                else
                    return "3";//提交失败
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //资料编辑
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult FAKzlBJView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion
        #endregion

        #region //现成方案 中关联的BOM查看
        public ActionResult FAKliaodanView(string cpId)
        {
            DKX_CPInfoView model = new DKX_CPInfoView();
            model = _IDKX_CPInfoDao.NGetModelById(cpId);
            ViewBag.MyJson = JsonConvert.SerializeObject(_IDKX_pjgdbinfoDao.NGetList());
            return View(model);
        }

        //查询关联的K3料单数据
        public string FAKckk3data(string cpId, string ddId, string cplytype)
        {
            string json = "";
            //查询资料表（料单）
            IList<DKX_RKZLDataInfoView> rkzlmodellist = _IDKX_RKZLDataInfoDao.GetRKzljsonbyId(cpId, "1");
            if (rkzlmodellist != null)
            {
                DKX_RKZLDataInfoView zlmodel = new DKX_RKZLDataInfoView();
                zlmodel = rkzlmodellist[0];
                if (zlmodel.Isgl == 2)//关联K3的料单
                {
                    string dId = "";//最初的订单Id 或者产品Id
                    if (cplytype == "1")
                    {
                        dId = cpId;
                    }
                    else
                    {
                        dId = ddId;
                    }
                    json = JsonConvert.SerializeObject(_IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(dId, zlmodel.BomNo));
                }
            }
            return json;
        }
        #endregion

        #region //产品逻辑删除
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
                List<DKX_CPInfoView> sys = _IDKX_CPInfoDao.NGetList_id(id) as List<DKX_CPInfoView>;
                if (null != sys)
                {
                    foreach (var item in sys)
                    {
                        item.IsDis = 4;
                        _IDKX_CPInfoDao.NUpdate(item);
                    }
                    return RedirectToAction("PBDKXCPlist");
                    //if (_ISysPersonDao.NDelete(sys) == true)
                    //{
                    //    return RedirectToAction("List");
                    //}
                    //else
                    //{
                    //    return Content("<script>alert('删除失败');window.history.back();</script>");
                    //}
                }
            }
            catch
            {
                return Content("<script>alert('删除失败');window.history.back();</script>");
            }
            return RedirectToAction("PBDKXCPlist");
        }
        #endregion

        #region //方案库中批量更新产品BOM
        public JsonResult PLupdatecpBOMEide(string Id)
        {
            //操作是否成功 

            IList<DKX_CPInfoView> cpmodellist = _IDKX_CPInfoDao.NGetList_id(Id);
            if (cpmodellist == null)
                return Json(new { result = "error", res = "产品不存在" });
            int? gxsusNUMB = 0;
            int? gxfailNUM = 0;//料单
            int? wfgxnum = 0;//料单不是关联K3 的无法更新
            int? ldbcz = 0;//料单不存在
            foreach (var cpitme in cpmodellist)
            {
                DKX_CPInfoView cpmodel = cpitme;
                string ddId = "";
                string K3LDBH = "";

                //查询该产品的入库资料
                IList<DKX_RKZLDataInfoView> cpliaodanmodel = _IDKX_RKZLDataInfoDao.GetRKzljsonbyId(cpmodel.Id, "1");
                if (cpliaodanmodel != null)
                {
                    if (cpliaodanmodel[0].Isgl == 2)//为2的时候是关联K3 的
                    {
                        //判断产品是订单来的还是直接添加的
                        if (cpmodel.cplytype == 1)
                        {
                            ddId = cpmodel.Id;
                            K3LDBH = cpliaodanmodel[0].BomNo;
                        }
                        else
                        {
                            ddId = cpmodel.Dd_Id;
                            K3LDBH = cpliaodanmodel[0].BomNo;
                        }
                        string GXrse = UPK3bomInterface(K3LDBH, ddId);
                        if (GXrse == "2")//成功
                        {
                            gxsusNUMB = gxsusNUMB + 1;//更新成功的
                        }
                        else
                        {
                            gxfailNUM = gxfailNUM + 1;//更新失败的
                        }
                    }
                    else
                    {
                        wfgxnum = wfgxnum + 1;//料单不是关联K3 的
                    }
                }
                else
                {
                    ldbcz = ldbcz + 1;//料单不存在的
                }
            }
            return Json(new { result = "Success", res1 = gxsusNUMB, res2 = gxfailNUM, res3 = wfgxnum, res4 = ldbcz });


        }
        #endregion

        #region //工程师批量完成整改
        public JsonResult PLwxzgEide(string Id)
        {
            IList<DKX_CPInfoView> cpmodellist = _IDKX_CPInfoDao.NGetList_id(Id);
            if (cpmodellist == null)
                return Json(new { result = "error", res = "产品不存在" });
            int? wczgNUM = 0;//完成整改成功的数量
            int? wczgfailNUM = 0;//完成整改失败的数量
            int? bnzgtjNUM = 0;//不能整改提交的数量
            foreach (var cpitme in cpmodellist)
            {
                DKX_CPInfoView cpmodel = cpitme;
                string wczgres = FAK_WCZGtjshEide(cpmodel.Id);

                if (wczgres == "5")
                {
                    bnzgtjNUM = bnzgtjNUM + 1;
                }
                else if (wczgres == "0")
                {
                    wczgNUM = wczgNUM + 1;
                }
                else
                {
                    wczgfailNUM = wczgfailNUM + 1;
                }
            }
            return Json(new { result = "Success", res1 = wczgNUM, res2 = wczgfailNUM, res3 = bnzgtjNUM });
        }
        #endregion

        #region //方案库的BOM编号修改同时更新料单
        public ActionResult FAKupdatebomnoView(string Id)
        {
            DKX_CPInfoView model = _IDKX_CPInfoDao.NGetModelById(Id);
            ViewBag.MyJson = JsonConvert.SerializeObject(_IDKX_RKZLDataInfoDao.GetzldatamodelbyIdandtype(Id, "1"));
            return View(model);
        }

        //保存新的料单编号同时更新料单
        public JsonResult updatebomnoEide(string zlId, string newbomno, string cplytyle, string cpId, string dd_Id)
        {
            try
            {
                //更细入库料单资料的料单编号
                DKX_RKZLDataInfoView zlmodel = _IDKX_RKZLDataInfoDao.NGetModelById(zlId);
                string ddId = "";
                if (zlmodel != null)
                {
                    if (zlmodel.Isgl == 2)//关联K3 的可以修改更新
                    {
                        zlmodel.Bjno = newbomno;
                        zlmodel.BomNo = newbomno;
                        _IDKX_RKZLDataInfoDao.NUpdate(zlmodel);
                        if (cplytyle == "1")//直接添加的产品
                        {
                            ddId = cpId;
                        }
                        else//订单来的
                        {
                            ddId = dd_Id;
                        }
                        string GXrse = UPK3bomInterface(newbomno, ddId);
                        if (GXrse == "2")//成功
                        {
                            return Json(new { result = "success", res = "更新成功" });
                        }
                        else
                        {
                            return Json(new { result = "error", res = "同步K3料单异常" });
                        }
                    }
                    else
                    {
                        return Json(new { result = "error", res = "料单不是关联K3 无法修改更新" });
                    }
                }
                else
                {
                    return Json(new { result = "error", res = "提交异常" });
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "error", res = "提交异常" });
            }
        }
        #endregion

        #region //方案库中的数据导出功能

        #region //导出
        public FileResult dc_Faninfo(string Gcs_name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<DKX_CPInfoView> listmodel = _IDKX_CPInfoDao.GetalldkxcpbygcsId(Gcs_name);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("型号名称");
            row1.CreateCell(1).SetCellValue("功率");
            row1.CreateCell(2).SetCellValue("入库时间");
            row1.CreateCell(3).SetCellValue("状态");
            row1.CreateCell(4).SetCellValue("异常原因");
            row1.CreateCell(5).SetCellValue("功能简述");
            //row1.CreateCell(6).SetCellValue("工程师");
            if (listmodel != null)
            {
                for (int i = 0, j = listmodel.Count; i < j; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(listmodel[i].Cpname);
                    rowtemp.CreateCell(1).SetCellValue(listmodel[i].Power+"/"+listmodel[i].DW);
                    rowtemp.CreateCell(2).SetCellValue(listmodel[i].RK_time.ToString());
                    rowtemp.CreateCell(3).SetCellValue(listmodel[i].IsDis.ToString());
                    rowtemp.CreateCell(4).SetCellValue(listmodel[i].zgyy);
                    rowtemp.CreateCell(5).SetCellValue(listmodel[i].cpgnjs);
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "定制电控箱方案库数据.xls");
        }
        #endregion
        #endregion

        #endregion

        #region //逾期处理通知数据list
        #region //数据列表
        public ActionResult DDclyqList(int? pageIndex)
        {
            PagerInfo<DKX_DDCLyqNoteInfoView> listmodel = GetDDclyqNotelistpage(pageIndex, null, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //多条件查询
        public JsonResult DDclyqSearchList()
        {
            string dd_binahao = Request.Form["txt_DDBinahao"];//订单编号
            string Khname = Request.Form["txt_khname"];//客户名称
            string yqtype = Request.Form["txt_yqtype"];//逾期类型
            string jsname = Request.Form["txt_jsname"];//接收人
            string tztimetype = Request.Form["txt_tztimetype"];//发送的时间段 0 上午 1 下午
            string starttime = Request.Form["txt_starttime"];//发送时间
            string endtime = Request.Form["txt_endtime"];//发送时间
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDCLyqNoteInfoView> listmodel = GetDDclyqNotelistpage(pageIndex, dd_binahao, Khname, yqtype, jsname, tztimetype, starttime, endtime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //逾期处理通知分页数据
        /// <summary>
        /// 逾期处理通知分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="DD_Bianhao"></param>
        /// <param name="khname"></param>
        /// <param name="yqtype"></param>
        /// <param name="jsname"></param>
        /// <param name="tztimetype"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        private PagerInfo<DKX_DDCLyqNoteInfoView> GetDDclyqNotelistpage(int? pageIndex, string DD_Bianhao, string khname, string yqtype, string jsname,
            string tztimetype, string starttime, string endtime)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDCLyqNoteInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDCLyqNoteInfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDCLyqNoteInfoView> listmodel = _IDKX_DDCLyqNoteInfoDao.GetddclyqNotedataPagerlist(DD_Bianhao, khname, yqtype, jsname, tztimetype, starttime, endtime);
            return listmodel;
        }
        #endregion

        #region //根据Id查找订单数据
        public string GetDDinfobyId(string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_DDinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion
        #endregion

        #region //电控箱生产情况

        #region //生产情况统计列表
        public ActionResult dkxscinfoList(int? pageIndex)
        {
            PagerInfo<DKX_DDinfoView> listmodel = GetscdkinfoPagerlist(pageIndex, null, null, "8", null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //多条件查询
        public JsonResult dkxscinfoSearchList()
        {
            string dd_binahao = Request.Form["txt_DDBinahao"];//订单编号
            string Khname = Request.Form["txt_khname"];//客户名称
            string dd_zt = Request.Form["txtdd_zt"];//订单状态
            string Isyq = Request.Form["txtIsyq"];//是否逾期
            string starttime = Request.Form["txtstarttime"];//
            string endtime = Request.Form["txtendtime"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = GetscdkinfoPagerlist(pageIndex, dd_binahao, Khname, dd_zt, Isyq, starttime, endtime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="DD_Bianhao"></param>
        /// <param name="khname"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="Isyq"></param>
        /// <returns></returns>
        private PagerInfo<DKX_DDinfoView> GetscdkinfoPagerlist(int? pageIndex, string DD_Bianhao, string khname, string DD_ZT, string Isyq, string starttime, string endtime)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getscddinfopagerlist(DD_Bianhao, khname, DD_ZT, Isyq, starttime, endtime);
            return listmodel;
        }
        #endregion

        #region //返回俩个时间的天数
        public string GetTSbytime(string type, string starttime, string endtime)
        {
            try
            {
                string ts = "0";
                if (type == "0")//在途进行天数
                {
                    ts = QRHelper.Getdkxxdczts(starttime);
                }
                else
                {
                    ts = Convert.ToString(QRHelper.DateDiff(Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)));
                }
                return ts;
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region //逾期通知数据查询
        //页面
        public ActionResult YQZTinfoView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        //逾期-通知记录
        public string GetyqtzinfobyId(string Id)
        {
            try
            {
                string json = "";
                json = JsonConvert.SerializeObject(_IDKX_DDCLyqNoteInfoDao.GetyqtzinfobyddId(Id));
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //统计各类数据
        public string TJglsum(string starttime, string endtime)
        {
            try
            {
                tjmodel model = new tjmodel();
                decimal zlsum = _IDKX_DDinfoDao.Getzxdsum(starttime, endtime);
                decimal ZTsum = _IDKX_DDinfoDao.Getzaitusum(starttime, endtime);
                decimal wcsum = zlsum - ZTsum;
                decimal dztsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("1", starttime, endtime);//待制图
                decimal ztzsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("2", starttime, endtime);//制图中
                decimal dshsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("-2", starttime, endtime);//待审核
                decimal dscsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("3", starttime, endtime);//待生产
                decimal qlsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("5", starttime, endtime);//缺料
                decimal kscsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("4", starttime, endtime);//可生产
                decimal sczsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("6", starttime, endtime);//生产中
                decimal dfhsum = _IDKX_DDinfoDao.GetZTdsumbyddzt("7", starttime, endtime);//待发货
                decimal yqwcsum = _IDKX_DDinfoDao.Getyqwcandzcwcsum("0", starttime, endtime);//逾期完成
                decimal zcwcsum = _IDKX_DDinfoDao.Getyqwcandzcwcsum("1", starttime, endtime);//正常完成
                model.zlsum = zlsum.ToString();
                model.ZTsum = ZTsum.ToString();
                model.wcsum = wcsum.ToString();
                model.dztsum = dztsum.ToString();
                model.ztzsum = ztzsum.ToString();
                model.dshsum = dshsum.ToString();
                model.dscsum = dscsum.ToString();
                model.qlsum = qlsum.ToString();
                model.kscsum = kscsum.ToString();
                model.sczsum = sczsum.ToString();
                model.dfhsum = dfhsum.ToString();
                model.yqwcsum = yqwcsum.ToString();
                model.zcwcsum = zcwcsum.ToString();
                string json = "";
                json = JsonConvert.SerializeObject(model);
                return json;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region //统计数据实体
        public class tjmodel
        {
            /// <summary>
            /// 总量
            /// </summary>
            public virtual string zlsum { get; set; }

            /// <summary>
            /// 在途数量
            /// </summary>
            public virtual string ZTsum { get; set; }

            /// <summary>
            /// 完成数量
            /// </summary>
            public virtual string wcsum { get; set; }

            /// <summary>
            /// 待制图数量
            /// </summary>
            public virtual string dztsum { get; set; }

            /// <summary>
            /// 制图中数量
            /// </summary>
            public virtual string ztzsum { get; set; }

            /// <summary>
            /// 待审核数量
            /// </summary>
            public virtual string dshsum { get; set; }

            /// <summary>
            /// 待生产数量
            /// </summary>
            public virtual string dscsum { get; set; }

            /// <summary>
            /// 缺料数量
            /// </summary>
            public virtual string qlsum { get; set; }

            /// <summary>
            /// 可生产数量
            /// </summary>
            public virtual string kscsum { get; set; }

            /// <summary>
            /// 生产中的数量
            /// </summary>
            public virtual string sczsum { get; set; }

            /// <summary>
            /// 待发货数量
            /// </summary>
            public virtual string dfhsum { get; set; }

            /// <summary>
            /// 逾期完成
            /// </summary>
            public virtual string yqwcsum { get; set; }

            /// <summary>
            /// 正常完成
            /// </summary>
            public virtual string zcwcsum { get; set; }
        }
        #endregion

        #region //导出功能
        public FileResult DC_DDINFO(string DD_Bianhao, string khname, string DD_ZT, string Isyq, string starttime, string endtime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.GetddinfoExportJson(DD_Bianhao, khname, DD_ZT, Isyq, starttime, endtime);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("生产单号");
            row1.CreateCell(1).SetCellValue("客户名称");
            row1.CreateCell(2).SetCellValue("订单型号/功率");
            row1.CreateCell(3).SetCellValue("进度");
            row1.CreateCell(4).SetCellValue("下单时间");
            row1.CreateCell(5).SetCellValue("完成时间");
            row1.CreateCell(6).SetCellValue("进行天数");
            row1.CreateCell(7).SetCellValue("是否逾期");

            if (listmodel != null)//数据不为空
            {
                for (int i = 0, j = listmodel.Count; i < j; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(listmodel[i].DD_Bianhao);
                    rowtemp.CreateCell(1).SetCellValue(listmodel[i].KHname);
                    rowtemp.CreateCell(2).SetCellValue(listmodel[i].DD_DHType + "(" + listmodel[i].POWER + "/" + listmodel[i].dw + ")");
                    rowtemp.CreateCell(3).SetCellValue(FanhuiZT(listmodel[i].DD_ZT));
                    rowtemp.CreateCell(4).SetCellValue(listmodel[i].C_time.ToString());
                    rowtemp.CreateCell(5).SetCellValue(listmodel[i].Fhdatetime.ToString());
                    string zt = listmodel[i].DD_ZT.ToString();
                    string type = "0";
                    if (zt == "8")
                        type = "1";
                    string ts = GetTSbytime(type, listmodel[i].C_time.ToString(), listmodel[i].Fhdatetime.ToString());
                    rowtemp.CreateCell(6).SetCellValue(ts);
                    if (Convert.ToDecimal(ts) > 12)
                        rowtemp.CreateCell(7).SetCellValue("逾期");
                    else
                        rowtemp.CreateCell(7).SetCellValue("正常");

                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "电控箱生产情况.xls");
        }
        #endregion
        #region //返回状态
        public string FanhuiZT(int ZT)
        {
            if (ZT == 8) {
                return "完成";
            }
            if (ZT == 7)
            {
                return "待发货";
            }
            if (ZT == 6)
            {
                return "生产中";
            }
            if (ZT == 5)
            {
                return "缺料";
            }
            if (ZT == 4)
            {
                return "可生产";
            }
            if (ZT == 3)
            {
                return "待生产";
            }
            if (ZT == -2)
            {
                return "待审核";
            }
            if (ZT == 2)
            {
                return "制图中";
            }
            if (ZT == 1)
            {
                return "待制图";
            }
            if (ZT == -1)
            {
                return "未提交";
            }
            return "-";
        }
        #endregion
        #endregion

        #region //控制器查询产品订单数据功能
        public ActionResult kzqCcpshuliang()
        {
            return View();
        }

        #region //根据控制器型号或者物料代码查询产品
        public string Getcpinfobykzqxhorwldam(string str, string type, string starttime, string endtime)
        {
            try
            {
                string json = "";
                IList<DKX_CPInfoView> cplist = _IDKX_CPInfoDao.Getcpinfobyxhorwldm(str, type);
                List<cpscsummodel> newcpdatelist = new List<cpscsummodel>();
                foreach (var item in cplist)
                {
                    cpscsummodel model = new cpscsummodel();
                    model.cpname = item.Cpname;
                    model.gl = item.Power;
                    model.dw = item.DW;
                    model.scsum = _IDKX_DDinfoDao.GetSCCPSUM(item.Cpname, item.Power, item.DW, starttime, endtime).ToString();
                    if (model.scsum != "0")
                    {
                        newcpdatelist.Add(model);
                    }
                }
                json = JsonConvert.SerializeObject(newcpdatelist);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //显示层实体
        public class cpscsummodel
        {
            /// <summary>
            /// 产品名称
            /// </summary>
            public virtual string cpname { get; set; }

            /// <summary>
            /// 产品功率
            /// </summary>
            public virtual string gl { get; set; }

            /// <summary>
            /// 功率单位
            /// </summary>
            public virtual string dw { get; set; }

            /// <summary>
            /// 生产销售的数量
            /// </summary>
            public virtual string scsum { get; set; }
        }
        #endregion
        #endregion

        #region //查询全部的电控箱类型的数据
        public string getjsonalldkxtypedata()
        {
            try
            {
                IList<DKX_DDtypeinfoView> allmodell = _IDKX_DDtypeinfoDao.NGetList();
                string json = JsonConvert.SerializeObject(allmodell);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //拼接柜 底板数据关联

        #region //拼接柜 的底板列表
        public ActionResult DKXpjdblist(int? pageIndex)
        {
            PagerInfo<DKX_pjgdbinfoView> listmodel = Getdkx_pjinfo(pageIndex, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        public JsonResult PJDBSearchList()
        {
            string name = Request.Form["txtSearch_Name"];//类型名称
            string wlno = Request.Form["txtwlno"];//物料代码
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_pjgdbinfoView> listmodel = Getdkx_pjinfo(pageIndex, name, wlno);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });

        }

        #region //获取拼接柜 的分页数
        private PagerInfo<DKX_pjgdbinfoView> Getdkx_pjinfo(int? pageIndex, string Name, string wlno)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_pjgdbinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_pjgdbinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_pjgdbinfoView> listmodel = _IDKX_pjgdbinfoDao.GetDKXpjgdbinfopagelist(Name, wlno);
            return listmodel;
        }
        #endregion

        #region //编辑

        #region //增加跳转页面
        public ActionResult addPagepj()
        {
            return View("PJDBEide", new DKX_pjgdbinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        public ActionResult EditPagepj(string id)
        {
            DKX_pjgdbinfoView model = new DKX_pjgdbinfoView();
            if (!string.IsNullOrEmpty(id))
                model = _IDKX_pjgdbinfoDao.NGetModelById(id);
            return View("PJDBEide", model);
        }
        #endregion

        #region //保存提交方法
        [HttpPost]
        public JsonResult PJDBEide(DKX_pjgdbinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.c_time = DateTime.Now;//创建时间
                    flay = _IDKX_pjgdbinfoDao.Ninsert(model);
                }
                else//修改
                {
                    model.update_time = DateTime.Now;
                    flay = _IDKX_pjgdbinfoDao.NUpdate(model);
                }
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return Json(new { result = "error" });//提交失败
            }

        }
        #endregion
        #endregion

        #region //按照模板批量导入底板产品数据
        public ActionResult ImportpjdbinfoView()
        {
            return View();
        }
        //提交
        [HttpPost]
        public JsonResult DRpjdbinfoExcel(HttpPostedFileBase fileImport)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("插入异常:11111");
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                if (fileImport != null)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(fileImport.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);
                    fileImport.SaveAs(filePath);
                    string filurl = Server.MapPath("~") + "/Content/upload/" + fileName;
                    System.Data.DataTable dt = GetExcelDatatable(filurl, "mapTable");
                    Insertpjdbinfo(dt);
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "error1" });
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("插入异常1" + e);
                return Json(new { result = "error1" });
            }
        }

        //批量插入
        public void Insertpjdbinfo(System.Data.DataTable dt)
        {
            try
            {
                string cpname = "";//产品名称
                string wlno = "";//物料代码
                string ms = "";//描述
                DKX_pjgdbinfoView model = new DKX_pjgdbinfoView();
                foreach (DataRow dr in dt.Rows)
                {
                    cpname = dr["cpname"].ToString().Trim();//产品名称
                    wlno = dr["wlno"].ToString().Trim();//物料代码
                    ms = dr["ms"].ToString().Trim();//产品描述
                    if (wlno != null)
                    {
                        //检查物料是否存在
                        if (_IDKX_pjgdbinfoDao.Getpjdbinfobywlno(wlno) == null)
                        {
                            model = new DKX_pjgdbinfoView();
                            model.dbname = cpname;
                            model.dbwlno = wlno;
                            model.db_describe = ms;
                            model.c_time = DateTime.Now;
                            _IDKX_pjgdbinfoDao.Ninsert(model);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("插入异常2" + e);
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
        #endregion

        #region //统计生产退单的记录
        public ActionResult ProductionreturnRecordlist()
        {
            AlGCSdataDropdown(null);
            GetSCCBRDATADropdown(null, "0");
            ViewBag.MyJson = getgcsinfojson();
            return View();
        }

        #region //查询全部工程师数据
        public string getgcsinfojson() {
            try
            {
                IList<DKX_GCSinfoView> allmodell = _IDKX_GCSinfoDao.NGetList();
                string json = JsonConvert.SerializeObject(allmodell);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //按照条件查询当前操作记录
        public string ProductionreturnRecorddatajson(string gcsid, string ddbianhao, string c_timestart, string c_timeend, string CBRId)
        {
            try
            {
                IList<DKX_LCCZJLinfoView> listmodel = _IDKX_LCCZJLinfoDao.Getlcczjldatabycondition(null, gcsid, ddbianhao, c_timestart, c_timeend, CBRId);
                string json = JsonConvert.SerializeObject(listmodel);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //整理数据
        public string zlshuj()
        {
            try
            {
                IList<DKX_LCCZJLinfoView> list = _IDKX_LCCZJLinfoDao.Getzllistdata();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        DKX_LCCZJLinfoView jlmolde = item;
                        DKX_DDinfoView ormodel = _IDKX_DDinfoDao.NGetModelById(item.dd_Id);
                        if (ormodel != null)
                        {
                            jlmolde.dd_bianhao = ormodel.DD_Bianhao;
                            jlmolde.gcs_Id = ormodel.Gcs_nameId;
                            _IDKX_LCCZJLinfoDao.NUpdate(jlmolde);
                        }
                    }
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region //导出生产退单记录

        public FileResult DC_ProductionreturnRecordFilResult(string gcsid, string ddbianhao, string c_timestart, string c_timeend, string CBRId)
        {
            IList<DKX_LCCZJLinfoView> list = _IDKX_LCCZJLinfoDao.Getlcczjldatabycondition(null, gcsid, ddbianhao, c_timestart, c_timeend, CBRId); ;
            IList<DKX_GCSinfoView> allmodell = _IDKX_GCSinfoDao.NGetList();
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("生产批号");
            row1.CreateCell(1).SetCellValue("责任工程师");
            row1.CreateCell(2).SetCellValue("退单原因");
            row1.CreateCell(3).SetCellValue("退单时间");
            row1.CreateCell(4).SetCellValue("备注");
            if (list != null)//数据不为空
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string gscname = "";
                    if (allmodell != null)
                    {
                        foreach (var item in allmodell)
                        {
                            if (item.Id == list[i].gcs_Id)
                            {
                                gscname = item.Name;
                            }
                        }
                        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                        rowtemp.CreateCell(0).SetCellValue(list[i].dd_bianhao);
                        rowtemp.CreateCell(1).SetCellValue(gscname);
                        if (list[i].CBRId == "" || list[i].CBRId == null)
                        { rowtemp.CreateCell(2).SetCellValue(list[i].caozuo); }
                        else { rowtemp.CreateCell(2).SetCellValue(list[i].CBRName); }
                        rowtemp.CreateCell(3).SetCellValue(list[i].c_time.ToString());
                        rowtemp.CreateCell(4).SetCellValue(list[i].CBRRemarks);
                    }
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "生产退单操作记录明细.xls");
        }
        #endregion

        #region //生产退单的旧数据修改
        public ActionResult SCTDoldjlupdateView(string Id)
        {
            ViewData["OID"] = Id;
            GetSCCBRDATADropdown(null, "1");
            return View();
        }
        #region //提交保存的方法
        public string SCTDEide(string Id, string con, string CbrId, string bz)
        {
            try
            {
                DKX_LCCZJLinfoView model = _IDKX_LCCZJLinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.CBRId = CbrId;
                    model.CBRName = con;
                    model.CBRRemarks = bz;
                    model.caozuo = "生产返退-" + con + bz;
                }
                if (_IDKX_LCCZJLinfoDao.NUpdate(model))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion
        #endregion


        #endregion

        #region //插入K3 中订单编号和责任工程师
        public void Insterbianhaoandgcsname(string binahao, string gcsId)
        {
            try
            {
                //查询工程师名称
                DKX_GCSinfoView model = _IDKX_GCSinfoDao.NGetModelById(gcsId);
                K3Helper.InsertBianhaoandengineer(binahao, model.Name);
            }
            catch
            {
            }
        }
        #endregion

        #region //插入平板工位机的订单资料(非标)
        public string InsterGWJ(string Id)
        {
            try
            {
                //查询订单
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                string ftemplate_id = "";
                if (model.DD_Type == 0 || model.DD_Type == 2)
                {//小系统和物联网
                    ftemplate_id = "25";
                }
                else if (model.DD_Type == 1 || model.DD_Type == 3)
                {//大系统
                    ftemplate_id = "29";
                }
                else
                {//plc
                    ftemplate_id = "30";
                }
                string res = gwjHelper.synchronizationorderandzl(Id, model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo, ftemplate_id, "40003");
                return res;
            }
            catch
            {
                return "1";
            }
        }

        #region //可以选择不同的工艺路线插入模板
        public ActionResult TB_GWJ_gylxView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        //手工修改订单的工艺路线同步蓝河工位机
        public JsonResult ManualTB_GWJacyncInterface(string Id, string ftemplate_id)
        {
            try
            {
                //查询订单
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.NGetModelById(Id);
                string res = "";
                if (ftemplate_id == "27" || ftemplate_id == "28")//非标转常规线
                {
                     res = gwjHelper.ESOP_fbzhuancg(ftemplate_id,Id,model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo,"40002");
                }
                else
                {//非标线
                     res = gwjHelper.synchronizationorderandzl(Id, model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo, ftemplate_id, "40003");
                }
                model.gwj_gylxId = ftemplate_id;
                _IDKX_DDinfoDao.NUpdate(model);
                return Json(new { result = "success", msg = res });
            }
            catch(Exception e)
            {
                return Json(new { result = "error",msg=e });
            }
        }
        #endregion
        #endregion

        #region //批量插入订单编号和工程师数据
        public void plInsterbianhaoandgcsname()
        {
            IList<DKX_DDinfoView> list = _IDKX_DDinfoDao.GetorderdataToyear();
            if (list != null)
            {

                foreach (var item in list)
                {

                    Insterbianhaoandgcsname(item.DD_Bianhao, item.Gcs_nameId);
                }
            }


        }
        #endregion

        #region //根据订单号查询订单资料的页面
        public ActionResult selectorderbyddnoView()
        {
            return View();
        }

        //获取订单数据
        public string Ajaxorderbyno(string orderno)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_DDinfoDao.GetDDmodelbyorderno(orderno));
            return json;
        }

        //同步更新方案库的数据
        public string updateFANGKAN(string Id)
        {
            try
            {
                //查询订单
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return "0";
                DKX_CPInfoView cpmode = new DKX_CPInfoView();
                cpmode = _IDKX_CPInfoDao.Getcpdatabynameandpowanddw(model.DD_DHType, model.POWER, model.dw, model.gnjsstr);
                if (cpmode != null)
                {//存在更新
                    //查询原型的方案库的资料
                    IList<DKX_RKZLDataInfoView> Fzlmodellist = _IDKX_RKZLDataInfoDao.GetDKXCPZLdatalist(cpmode.Id);
                    if (Fzlmodellist != null)
                    {//删除资料
                        foreach (var item in Fzlmodellist)
                        {
                            _IDKX_RKZLDataInfoDao.NDelete(item);
                        }
                    }
                    //重新插入方案库资料
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
                            rkzlmodel.cpId = cpmode.Id;//产品Id
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
                else
                {
                    return "1";//不存在方案
                }
            }
            catch
            {
                return "3";//异常
            }
        }
        #endregion

        #region //同步K3 生成生成任务单
        public ActionResult TBK3SCworkView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }

        public JsonResult TBK3SCworkEide(string Id, string cpbom, string Sel_sj, string Sel_sctype, string starttime, string endtime)
        {
            try
            {
                //通过订单Id查询订单
                DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
                if (model == null)
                    return Json(new { result = "error", msg = "订单不存在！" });
                if (model.DD_ZT < 3)
                    return Json(new { result = "error", msg = "当前状态下不可以同步生产任务单！" });
                ICMOSysmodel TBmodel = new ICMOSysmodel();
                TBmodel.FBatchNo = model.DD_Bianhao;//批号
                TBmodel.FBOMNumber = model.KBomNo;//BOM编号
                TBmodel.FCustName = model.KHname;//客户名称
                TBmodel.FNumber = cpbom;//产品BOM编号
                TBmodel.FDeptNumber = Sel_sj;
                if (starttime == "")
                {//开工数量
                    TBmodel.FPlanCommitDate = DateTime.Now;
                    TBmodel.FPlanFinishDate = DateTime.Now;
                }
                else
                {//完成时间
                    TBmodel.FPlanCommitDate = Convert.ToDateTime(starttime);
                    TBmodel.FPlanFinishDate = Convert.ToDateTime(endtime);
                }
                TBmodel.FQty = Convert.ToInt32(model.NUM);//生产数量
                TBmodel.FWorktypeName = Sel_sctype;//生产类型
                //string ss = "";
                string ss = K3Helper.InsertICMOSys(TBmodel);
                if (ss == null)
                    return Json(new { result = "error", msg = "接口调用失败！" });
                else
                {
                    K3FHmodel fhmodel = new K3FHmodel();
                    //fhmodel = JsonConvert.DeserializeObject(ss) as K3FHmodel;
                    fhmodel=JsonConvert.DeserializeObject<K3FHmodel>(ss);
                    if (fhmodel.FStatus == "S")
                    {
                        model.Istbwork = 1;
                        model.tbworktime = DateTime.Now;
                        _IDKX_DDinfoDao.NUpdate(model);
                        return Json(new { result = "success", msg = fhmodel.FMessage });
                    }
                    else
                    {
                        return Json(new { result = "error", msg = fhmodel.FMessage });
                    }
                }
                
            }
            catch
            {
                return Json(new { result = "error", msg = "同步写入失败！" });
            }
        }

        //接口返回的参数
        public class K3FHmodel {

            /// <summary>
            /// 接口调用状态 S: 成功、F: 失败
            /// </summary>
            public string FStatus { get; set; }

            /// <summary>
            /// 处理消息
            /// </summary>
            public string FMessage { get; set; }
        }
        #endregion

        #region //同步插入普实的数据

        //同步普实产品同步的页面
        public ActionResult TBPsCPView(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(ddmodel);
        }

        public JsonResult Ps_Instercp(string Id,string Ps_sanduanno)
        {
            //查询非标订单
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_wlBomNO == "" || ddmodel.Ps_wlBomNO == null)
                {
                    var bomstr = ddmodel.KBomNo.Substring(0, 3);
                    if (bomstr == "BOM")
                        return Json(new { result = "error", msg = "常规产品无需同步" });
                    string sanduanno = "";
                    Ps_fbcpmodel pscpmodel = new Ps_fbcpmodel();
                    if (ddmodel.Ps_sanduanno == "" || ddmodel.Ps_sanduanno == null)
                        sanduanno = Ps_sanduanno;
                    else
                        sanduanno = ddmodel.Ps_sanduanno;
                    //流水号
                    //int count = _IDKX_DDinfoDao.Getdaleordercount(sanduanno);
                    //count = count + 1;
                    //string liushuihao = count.ToString().PadLeft(5, '0');
                    //随机5位数
                    string liushuihao= NAHelper.RandomNum(5);
                    pscpmodel.ItmID = sanduanno + ".A" + liushuihao;
                    if (!_IDKX_DDinfoDao.checkpswlno(pscpmodel.ItmID))
                        return Json(new { result = "error", msg = "自动生成物料号异常，请重新操作" });
                    pscpmodel.ItmSpec = ddmodel.KBomNo;
                    pscpmodel.Z_ItmID = Ps_sanduanno;
                    pscpmodel.Z_Price = ddmodel.YJcb.ToString();
                   string res =  pushsoftHelper.Instercpinfo(pscpmodel);
                    
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "同步接口返回空" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            ddmodel.Ps_wlBomNO= sanduanno + ".A" + liushuihao;
                            ddmodel.Ps_Serialnumber= ".A" + liushuihao;
                            ddmodel.Ps_sanduanno = Ps_sanduanno;
                            ddmodel.Ps_DocEntry= errmodel.Data.DocEntry;
                            _IDKX_DDinfoDao.NUpdate(ddmodel);
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

        //插入普实BOM
        public JsonResult Ps_InstarBom(string Id)
        {
            //查询非标订单
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_wlBomNO != "" && ddmodel.Ps_wlBomNO != null)
                {
                    //if(ddmodel.Ps_wlBomNO=="0")
                    //    return  Json(new { result = "error", msg = "常规产品无需同步" });
                    var bomstr = ddmodel.KBomNo.Substring(0, 3);
                    if(bomstr== "BOM")
                        return Json(new { result = "error", msg = "常规产品无需同步" });
                    if (ddmodel.Ps_BomNO == "" || ddmodel.Ps_BomNO == null)
                    {
                        Ps_Bommodel PsBomnodel = new Ps_Bommodel();
                        PsBomnodel.BomID = ddmodel.Ps_wlBomNO;
                        PsBomnodel.BomName = ddmodel.KBomNo;
                        PsBomnodel.ItmID= ddmodel.Ps_wlBomNO;
                        PsBomnodel.VerNum = "V 1.0";
                        PsBomnodel.RouID = DateTime.Now.ToString();
                        PsBomnodel.Procedures = Getgongxu();
                        PsBomnodel.Items = Getyongliao(ddmodel.Id);
                        string res = pushsoftHelper.InstaerBominfo(PsBomnodel);
                        if (res == "" || res == null) { return Json(new { result = "error", msg = "同步接口返回空" }); }
                        else
                        {
                            pushsoftErrmodel errmodel = new pushsoftErrmodel();
                            errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                            if (errmodel.ErrCode == "0")
                            {
                                ddmodel.Ps_BomNO= ddmodel.Ps_wlBomNO;
                                _IDKX_DDinfoDao.NUpdate(ddmodel);
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
                        return Json(new { result = "error", msg = "BOM表已经同步" });
                    }
                }
                else
                {
                    return Json(new { result = "error", msg = "非标产品尚未同步" });
                }
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }

        //工序
        public IList<Proceduresmodel> Getgongxu()
        {
            List<Proceduresmodel> gxmxlist = new List<Proceduresmodel>();
            Proceduresmodel gongx = new Proceduresmodel();
            gongx.LineNum = "10";
            gongx.PrcID = "DQ01";
            gongx.PrcName = "底板装配";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "0";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "20";
            gongx.PrcID = "DQ02";
            gongx.PrcName = "接控制线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "10";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "30";
            gongx.PrcID = "DQ03";
            gongx.PrcName = "接主回路线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "20";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "40";
            gongx.PrcID = "DQ05";
            gongx.PrcName = "面板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "30";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "50";
            gongx.PrcID = "DQ06";
            gongx.PrcName = "底板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "40";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "60";
            gongx.PrcID = "DQ071";
            gongx.PrcName = "面板接线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "50";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "70";
            gongx.PrcID = "DQ09";
            gongx.PrcName = "调试";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "60";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "80";
            gongx.PrcID = "DQ011";
            gongx.PrcName = "验收";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "70";
            gxmxlist.Add(gongx);
            return gxmxlist;
         
        }

        //用料
        public IList<Itemsmodel> Getyongliao(string Id)
        {
            //查询资料表(料单)
            IList<DKX_ZLDataInfoView> ZLmodellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1");
            if (ZLmodellist != null)
            {
                DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                zlmodel = ZLmodellist[0];
                List<Itemsmodel> wuliaolist = new List<Itemsmodel>();
                if (zlmodel.Isgl == 2)//关联K3的料单
                {
                    IList<DKX_k3BominfoView> kemodellist = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, zlmodel.Bjno);
                    foreach (var item in kemodellist)
                    {
                        Itemsmodel wlmodel = new Itemsmodel();
                        wlmodel.ItmID = item.FNumber;
                        wlmodel.NetQty = item.FAuxQty;
                        wlmodel.ScrapRate = "0";
                        wlmodel.LineType = "P";
                        wlmodel.OperationLine = "10";
                        wlmodel.Position = item.Beizhu;
                        wuliaolist.Add(wlmodel);
                    }
                }
 
                return wuliaolist;
            }
            return null;
        }

        #region //普实的非标销售订单插入
        public ActionResult ForderinstarView(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(ddmodel);
        }

        public ActionResult ForderinstarViewN(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel.Ps_orderno == ""||ddmodel.Ps_orderno==null)
            {
                ddmodel.Ps_orderno = DateTime.Now.ToString("yyyyddmm") + NAHelper.RandomNum(5);
                _IDKX_DDinfoDao.NUpdate(ddmodel);
            }
            return View(ddmodel);
        }

        //选择其他相同客户的没有销售订单的数据
        public ActionResult SelectForderView(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(ddmodel);
        }

        #region //销售订单明细编辑
        public ActionResult FordermxbianjiView(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            return View(ddmodel);
        }
        #endregion

        #region //销售订单删除（注：把销售订单编号设置为空）
        public JsonResult delForderbyId(string Id)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_orderDocEntry == "" || ddmodel.Ps_orderDocEntry == null)
                {
                    ddmodel.Ps_orderno = null;
                    if (_IDKX_DDinfoDao.NUpdate(ddmodel))
                        return Json(new { result = "success", msg = "删除成功" });
                    else
                        return Json(new { result = "error", msg = "删除失败" });
                }
                else
                {
                    return Json(new { result = "error", msg = "该明细已经同步普实销售订单,不能剔除。请联系管理员" });
                }
              
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }
        #endregion

        //确认添加其他非标订单的产品
        public JsonResult addqtFordercp(string Id,string Ps_orderno)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel.Ps_orderno==null)
            {
                ddmodel.Ps_orderno = Ps_orderno;
                if(_IDKX_DDinfoDao.NUpdate(ddmodel))
                    return Json(new { result = "success", msg = "操作成功" });
                else
                    return Json(new { result = "error", msg = "添加失败,请重新操作" });
            }
            else
            {
                return Json(new { result = "error", msg = "已经产生销售单号:"+ddmodel.Ps_orderno });
            }
        }

        #region //根据客户的编码查询没有生成销售订单的数据
        public ActionResult GetFNOordernobyk3code(string kecode,string Isxs)
        {
            IList<DKX_DDinfoView> modellist = _IDKX_DDinfoDao.Getqtmxbyk3code(kecode);
            if (Isxs == "1")
            {
                if (modellist != null) { 
                for (int j = modellist.Count() - 1; j >= 0; j--)
                {
                        if (modellist[j].KBomNo != null) {
                            if (modellist[j].KBomNo.CompareTo("BOM") >= 0)
                            {
                                modellist.Remove(modellist[j]);
                            }
                        }
                      
                }
                }
            }
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

        #region //相同销售订单号的数据 20210510
        public ActionResult GetallmxbyPs_orderno(string Ps_orderno)
        {
            IList<DKX_DDinfoView> modellist = _IDKX_DDinfoDao.GetAllmxbyPs_orderno(Ps_orderno);
            //string jsonstr = JsonConvert.SerializeObject(modellist);
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

        #endregion

        #region //修改订单K3code
        public JsonResult updateorderk3code(string Id, string k3code)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.khkecode = k3code;
            if (_IDKX_DDinfoDao.NUpdate(ddmodel))
            {
                return Json(new { result = "success", msg = "更新成功" });
            }
            else
            {
                return Json(new { result = "error", msg = "修改失败" });
            }
        }
        #endregion

        #region //修改非标常规的料号
        public JsonResult updateliaohao(string Id, string liaohao)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.Ps_wlBomNO = liaohao;
            if (_IDKX_DDinfoDao.NUpdate(ddmodel))
            {
                return Json(new { result = "success", msg = "更新成功" });
            }
            else
            {
                return Json(new { result = "error", msg = "修改失败" });
            }
        }
        #endregion

        #region //修改订单的合同单价
        public JsonResult updateprice(string Id, string price)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            ddmodel.price = Convert.ToInt32(price);
            if (_IDKX_DDinfoDao.NUpdate(ddmodel))
            {
                return Json(new { result = "success", msg = "更新成功" });
            }
            else
            {
                return Json(new { result = "error", msg = "修改失败" });
            }
        }
        #endregion

        public JsonResult Ps_InsterForder(string Id, string Z_JHQX, string Z_JHDD, string Z_YSFS)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel!=null)
            {
                if (ddmodel.Ps_wlBomNO != "" && ddmodel.Ps_wlBomNO != null)
                {
                    if (ddmodel.Ps_BomNO != "" && ddmodel.Ps_BomNO != null)
                    {
                        pushsoftorder Psordermodel = new pushsoftorder();
                        if (ddmodel.khkecode == "")
                            return Json(new { result = "error", msg = "客户没有维护编号" });
                        Psordermodel.CrdID = ddmodel.khkecode;
                        Psordermodel.NumAtCrd = ddmodel.DD_Bianhao;
                        Psordermodel.DocDate = Convert.ToDateTime(Convert.ToDateTime(ddmodel.C_time).ToString("yyyy-MMM-dd"));
                        Psordermodel.Z_JHQX = Z_JHQX;
                        Psordermodel.Z_JHDD = Z_JHDD;
                        Psordermodel.Z_YSFS = Z_YSFS;
                        Psordermodel.DocKind = 0;
                        List<pushsoftorderDetails> psmxlist = new List<pushsoftorderDetails>();
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID =ddmodel.Ps_wlBomNO;
                        Psmxmodel.Qty =Convert.ToInt32(ddmodel.NUM);
                        Psmxmodel.TaxAfPriceFC =ddmodel.price;
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                        Psordermodel.Details = psmxlist;
                        string res = pushsoftHelper.Insterorder(Psordermodel);
                        if (res == "" || res == null)
                        { return Json(new { result = "error", msg = "订单不存在" }); }
                        else
                        {
                            pushsoftErrmodel errmodel = new pushsoftErrmodel();
                            errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                            if (errmodel.ErrCode == "0")
                            {
                                ddmodel.Ps_DocEntry = errmodel.Data.DocEntry;
                                ddmodel.Ps_orderDocEntryNUM = errmodel.Data.DocNum;
                                _IDKX_DDinfoDao.NUpdate(ddmodel);
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
                        return Json(new { result = "error", msg = "非标产品BOM没有同步" });
                    }
                }
                else
                {
                    return Json(new { result = "error", msg = "非标产品没有同步" });
                }
                
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
           
            
        }

        public JsonResult Ps_InstercgForder(string Id, string Z_JHQX, string Z_JHDD, string Z_YSFS)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_wlBomNO != "" && ddmodel.Ps_wlBomNO != null)
                {
 
                        pushsoftorder Psordermodel = new pushsoftorder();
                        if (ddmodel.khkecode == "")
                            return Json(new { result = "error", msg = "客户没有维护编号" });
                        Psordermodel.CrdID = ddmodel.khkecode;
                        Psordermodel.NumAtCrd = ddmodel.DD_Bianhao;
                        Psordermodel.DocDate =Convert.ToDateTime(Convert.ToDateTime(ddmodel.C_time).ToString("yyyy-MMM-dd"));
                        Psordermodel.Z_JHQX = Z_JHQX;
                        Psordermodel.Z_JHDD = Z_JHDD;
                        Psordermodel.Z_YSFS = Z_YSFS;
                        Psordermodel.DocKind = 0;
                        List<pushsoftorderDetails> psmxlist = new List<pushsoftorderDetails>();
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = ddmodel.Ps_wlBomNO;
                        Psmxmodel.Qty = Convert.ToInt32(ddmodel.NUM);
                        Psmxmodel.TaxAfPriceFC = ddmodel.price;
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                        Psordermodel.Details = psmxlist;
                        string res = pushsoftHelper.Insterorder(Psordermodel);
                        if (res == "" || res == null)
                        { return Json(new { result = "error", msg = "订单不存在" }); }
                        else
                        {
                            pushsoftErrmodel errmodel = new pushsoftErrmodel();
                            errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                            if (errmodel.ErrCode == "0")
                            {
                                ddmodel.Ps_orderDocEntry = errmodel.Data.DocEntry;
                                _IDKX_DDinfoDao.NUpdate(ddmodel);
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
                    return Json(new { result = "error", msg = "非标产品没有同步" });
                }

            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }

        #region //非标插入普实的销售订单
        public JsonResult Ps_InsterForderNew(string Id, string Z_JHQX, string Z_JHDD, string Z_YSFS,string yfprice,string dsprice)
        {
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                if (ddmodel.Ps_orderDocEntry == "" || ddmodel.Ps_orderDocEntry == null)
                {
                    pushsoftorder Psordermodel = new pushsoftorder();
                    //客户编码k3code
                    if(ddmodel.khkecode==""||ddmodel.khkecode==null)
                        return Json(new { result = "error", msg = "客户没有维护编号" });
                    Psordermodel.CrdID = ddmodel.khkecode;
                    Psordermodel.NumAtCrd = ddmodel.Ps_orderno;
                    Psordermodel.DocDate = Convert.ToDateTime(Convert.ToDateTime(ddmodel.C_time).ToString("yyyy-MMM-dd"));
                    Psordermodel.Z_JHQX = Z_JHQX;
                    Psordermodel.Z_JHDD = Z_JHDD;
                    Psordermodel.Z_YSFS = Z_YSFS;
                    Psordermodel.DocKind = 0;
                    //查询订单明细
                    IList<DKX_DDinfoView> ddmmodellist = _IDKX_DDinfoDao.GetAllmxbyPs_orderno(ddmodel.Ps_orderno);
                    List<pushsoftorderDetails> psmxlist = new List<pushsoftorderDetails>();
                    foreach (var item in ddmmodellist)
                    {
                        //查询产品
                        //NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(item.cpId);
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        if (item.Ps_orderDocEntry != "" && item.Ps_orderDocEntry != null)
                            return Json(new { result = "error", msg = "该明细已经同步过销售订单：" + item.DD_Bianhao });
                        Psmxmodel.ItmID = item.Ps_wlBomNO;
                        if(item.Ps_wlBomNO==""|| item.Ps_wlBomNO==null)
                            return Json(new { result = "error", msg = "没有维护产品物料编号："+item.DD_Bianhao });
                        Psmxmodel.Qty = Convert.ToInt32(item.NUM);
                        if(item.price==0|| item.price==null)
                            return Json(new { result = "error", msg = "没有维护单价：" + item.DD_Bianhao });
                        Psmxmodel.TaxAfPriceFC = item.price;
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    if (yfprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.002";
                        Psmxmodel.Qty = 1;
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(yfprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    if (dsprice != "")
                    {
                        pushsoftorderDetails Psmxmodel = new pushsoftorderDetails();
                        Psmxmodel.ItmID = "08.005";
                        Psmxmodel.Qty = 1;
                        Psmxmodel.TaxAfPriceFC = Convert.ToDecimal(yfprice);
                        Psmxmodel.ReqDate = Convert.ToDateTime(Z_JHQX);
                        psmxlist.Add(Psmxmodel);
                    }
                    Psordermodel.Details = psmxlist;
                    string res = pushsoftHelper.Insterorder(Psordermodel);
                    if (res == "" || res == null) { return Json(new { result = "error", msg = "订单不存在" }); }
                    else
                    {
                        pushsoftErrmodel errmodel = new pushsoftErrmodel();
                        errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                        if (errmodel.ErrCode == "0")
                        {
                            foreach (var item in ddmmodellist)
                            {
                                 item.Ps_orderDocEntry = errmodel.Data.DocEntry;
                                item.Ps_orderDocEntryNUM = errmodel.Data.DocNum;
                                _IDKX_DDinfoDao.NUpdate(item);
                            }
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
                    return Json(new { result = "error", msg = "订单已经同步,不能再次同步" });
                }
            }
            else
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }
        #endregion
        #endregion

        #region //核算非标硬件成本
        public JsonResult Fyingjiancbsum(string Id)
        {
            try
            {
                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
                //查询bom
                string json = K3Helper.Getk3bombybomno(ddmodel.KBomNo);
                if (json != "[]")
                {
                    List<KBOMjsonmodel> timemodel = getObjectByJson<KBOMjsonmodel>(json);
                    decimal cbj = 0;
                    foreach (var a in timemodel)
                    {
                        if (Convert.ToDecimal(a.FOrderPrice) == 0)
                        {
                            var bcpcj = K3Helper.Getk3bompricebywlno(a.FNumber);
                            if (bcpcj != "[]")
                            {
                                 
                                List<cbdjmodel> bcpcb= getObjectByJson<cbdjmodel>(bcpcj);
                                if(bcpcb[0].Column1!=null)
                                { 
                                  cbj = cbj + Convert.ToDecimal(Convert.ToDouble(bcpcb[0].Column1));
                                }
                            }

                        }
                        else
                        {
                            cbj = cbj + Convert.ToDecimal(Convert.ToDouble(a.FOrderPrice)* Convert.ToDouble(a.FAuxQty));
                        }
                       
                    }
                    cbj = Convert.ToDecimal(Convert.ToDouble(cbj) * 1.15 * 1.1);
                    ddmodel.YJcb = cbj;
                    _IDKX_DDinfoDao.NUpdate(ddmodel);
                    return Json(new { result = "success", msg = cbj });
                }
                else
                {
                    return Json(new { result = "error", msg = "BOM不存在,或bom编号有问题" });
                }
                
            }
            catch
            {
                return Json(new { result = "error", msg = "订单不存在" });
            }
        }

        public class cbdjmodel
        {
            public virtual decimal? Column1 { get; set; }

            public virtual string FNumber { get; set; }
        }
        #endregion

        #region //根据BOMNO查询物料的编号
        public JsonResult GetFNumberbyFBOMNumber(string Id)
        {
            try
            {
                DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
                var bomstr = ddmodel.KBomNo.Substring(0, 3);
                if (bomstr == "BOM")
                {
                    string json = K3Helper.GetFNumberbyFBOMNumber(ddmodel.KBomNo);
                    if (json != "[]")
                    {
                        List<cbdjmodel> bcpcb = getObjectByJson<cbdjmodel>(json);
                        string wlmo = bcpcb[0].FNumber.ToString();
                        ddmodel.Ps_wlBomNO = wlmo;
                        if (_IDKX_DDinfoDao.NUpdate(ddmodel))
                            return Json(new { result = "success", msg = "操作成功" });
                        else
                            return Json(new { result = "error", msg = "操作失败" });
                    }
                    else
                    {
                        return Json(new { result = "error", msg = "查询不到该BOM号下面的产品物料号" });
                    }
                }
                else
                {
                    return Json(new { result = "error", msg = "非标的产品请同步普实产品物料" });
                }

            }
            catch
            {
                return Json(new { result = "error", msg = "BOM不存在,或bom编号有问题" });
            }
        }
        #endregion

        #region //更新普实BOM
        public JsonResult Ps_updateBom(string Id)
        {
            //查询非标订单
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if(ddmodel==null)
                return Json(new { result = "error", msg = "订单不存在" });
            if (ddmodel.Ps_BomNO==""|| ddmodel.Ps_BomNO == null)
                return Json(new { result = "error", msg = "BOM表尚未插入，无法更新" });
            //查询BOM主表单号
            string DocEntry = "";
            string strjson = zypushsoftHelper.GetBomDocEntryByItmID(ddmodel.Ps_BomNO);
            if(strjson=="")
                return Json(new { result = "error", msg = "BOM不存在" });
            IList<PSBOM> list = JsonConvert.DeserializeObject<IList<PSBOM>>(strjson);
            if (list != null)
                DocEntry = list[0].DocEntry;
            if(DocEntry=="")
                return Json(new { result = "error", msg = "BOM的单号不存在" });
            //更新
            //查询资料表(料单)
            IList<DKX_ZLDataInfoView> ZLmodellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1");
            if (ZLmodellist == null)
                return Json(new { result = "error", msg = "料单不存在" });
            DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
            zlmodel = ZLmodellist[0];
            IList<DKX_k3BominfoView> kemodellist = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, zlmodel.Bjno);
            string delres = zypushsoftHelper.DelBomA(DocEntry);//删除原先的BOM明细
            //循环插入明细表
            int iLineNum = 0;
            int IOrderNum = 10;
            foreach (var item in kemodellist)
            {
                iLineNum++;
                FInstaerMDBomA mxmodel = new FInstaerMDBomA();
                mxmodel.DocEntry = DocEntry;
                mxmodel.ItmID = item.FNumber;
                mxmodel.LineNum = iLineNum.ToString();
                mxmodel.NetQty = Convert.ToDecimal(item.FAuxQty);
                mxmodel.OrderNum = IOrderNum;
                IOrderNum = IOrderNum + 10;
                string datejson = JsonConvert.SerializeObject(mxmodel);
                datejson = "[" + datejson + "]";
                string INSRES = zypushsoftHelper.UpdateBomA(datejson);//插入BOM的明细
            }
            return Json(new { result = "success", msg = "操作成功" });
        }
        public class PSBOM
        {
            public virtual string DocEntry { get; set; }
        }
        #endregion


    }
}
