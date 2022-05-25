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
using System.Runtime.Serialization.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WL_DkxInfoController : Controller
    {
        //
        // GET: /WL_DkxInfo/
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        IWL_GcsinfoDao _IWL_GcsinfoDao = ContextRegistry.GetContext().GetObject("WL_GcsinfoDao") as IWL_GcsinfoDao;
        IWL_ReturnVisitDao _IWL_ReturnVisitDao = ContextRegistry.GetContext().GetObject("WL_ReturnVisitDao") as IWL_ReturnVisitDao;
        INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
        IWL_NewgscinfoDao _IWL_NewgscinfoDao = ContextRegistry.GetContext().GetObject("WL_NewgscinfoDao") as IWL_NewgscinfoDao;
        IWL_dkxthjlinfoDao _IWL_dkxthjlinfoDao = ContextRegistry.GetContext().GetObject("WL_dkxthjlinfoDao") as IWL_dkxthjlinfoDao;
        INA_QyinfoDao _INA_QyinfoDao = ContextRegistry.GetContext().GetObject("NA_QyinfoDao") as INA_QyinfoDao;

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            AlJxsDropdown(null);
            AlJxsDropdown2(null);
            PagerInfo<WL_DkxInfoView> listmodel = GetListPager(pageIndex, null,null,null,null,null,null,null,null,null,null,null);
            return View(listmodel);
        }
        #endregion

        #region //技术部列表页面
        public ActionResult JsList(int? pageIndex)
        {
            AlJxsDropdown(null);
            AlJxsDropdown2(null);
            PagerInfo<WL_DkxInfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, null, null, null, null, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//经销商名称（Id）
            string Name2 = Request.Form["txtSearch_Name2"];//经销商名称（Id）
            string wl_zt = Request.Form["wl_zt"];//状态
            string Is_sx = Request.Form["Is_sx"];//是否上线
            string Is_xs = Request.Form["Is_xs"];//是否销售
            string wl_sid=Request.Form["wl_sid"];//sid码
            string Is_qf=Request.Form["Is_qf"];//是否欠费
            string startdate = Request.Form["txtSearch_startdate"];//销售开始时间
            string enddate = Request.Form["txtSearch_enddate"];//销售结束时间
            string sxstartdate = Request.Form["txtSearch_SXstartdate"];//上线时间开始
            string sxenddate = Request.Form["txtSearch_SXenddate"];//上线时间结束
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WL_DkxInfoView> listmodel = GetListPager(pageIndex, Name, Name2,wl_zt,Is_sx,Is_xs, wl_sid,Is_qf, startdate, enddate, sxstartdate, sxenddate);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<WL_DkxInfoView> GetListPager(int? pageIndex, string Name, string Name2, string wl_zt, string Is_sx, string Is_xs, string wl_sid, string Is_qf, string startdate, string enddate, string sxstartdate, string sxenddate)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWL_DkxInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWL_DkxInfoDao.SetPagerPageSize(11);
            PagerInfo<WL_DkxInfoView> listmodel = _IWL_DkxInfoDao.GetWLdkxinfoList(Name, Name2, Is_sx, Is_xs, wl_zt, wl_sid,Is_qf, startdate, enddate, sxstartdate, sxenddate, Suser);
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
        public JsonResult Edit(WL_DkxInfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                WL_DkxInfoView jcdkxmodel = new WL_DkxInfoView();
                jcdkxmodel = _IWL_DkxInfoDao.GetDkxinfo(Getsid(model.WL_SSD));
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    if (jcdkxmodel != null)
                    {
                        return Json(new { result = "error2" });
                    }
                    else
                    {
                        model.WL_SSD = Getsid(model.WL_SSD);//物联网电控箱SSD码
                        model.SC_time = model.SC_time;//生产时间
                        model.SC_PH = model.SC_PH;//生产批号
                        model.CS_time = model.CS_time;//测试时间
                        model.CS_zt = model.CS_zt;//测试状态 0 未测试 1 已测试
                        model.Xs_datetime = model.Xs_datetime;//销售时间
                        model.Xs_jxsId = model.Xs_jxsId;//经销商Id
                        model.Xs_gcsId = model.Xs_gcsId;//工程商Idw
                        model.Sx_time = model.Sx_time;//上线时间
                        model.WL_BXStarttime = model.WL_BXStarttime;//保修时间开始
                        model.WL_BXEndtime = model.WL_BXEndtime;//保修时间结束
                        model.WL_zt = model.WL_zt;//电控箱状态
                        model.Jf_zt = model.Jf_zt;//缴费状态
                        model.Jf_starttime = model.Jf_starttime;//上次缴费时间
                        model.Jf_endtime = model.Jf_endtime;//下次缴费时间
                        model.Jf_cs = model.Jf_cs;//缴费时间
                        model.C_time = DateTime.Now;//创建时间
                        model.C_name = user.Id;//创建人
                        model.Sort = model.Sort;//排序
                        model.States = model.States;//状态 0 启用 1禁用
                        model.Beizhu = model.Beizhu;//备注
                        flay = _IWL_DkxInfoDao.Ninsert(model);
                    }
                }
                //修改
                else
                {
                    model.WL_SSD = model.WL_SSD;//物联网电控箱SSD码
                    model.SC_time = model.SC_time;//生产时间
                    model.SC_PH = model.SC_PH;//生产批号
                    model.CS_time = model.CS_time;//测试时间
                    model.CS_zt = model.CS_zt;//测试状态 0 未测试 1 已测试
                    model.Xs_datetime = model.Xs_datetime;//销售时间
                    model.Xs_jxsId = model.Xs_jxsId;//经销商Id
                    model.Xs_gcsId = model.Xs_gcsId;//工程商Id
                    model.Sx_time = model.Sx_time;//上线时间
                    model.WL_BXStarttime = model.WL_BXStarttime;//保修时间开始
                    model.WL_BXEndtime = model.WL_BXEndtime;//保修时间结束
                    model.WL_zt = model.WL_zt;//电控箱状态
                    model.Jf_zt = model.Jf_zt;//缴费状态
                    model.Jf_starttime = model.Jf_starttime;//上次缴费时间
                    model.Jf_endtime = model.Jf_endtime;//下次缴费时间
                    model.Jf_cs = model.Jf_cs;//缴费时间
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//状态 0 启用 1禁用
                    model.Beizhu = model.Beizhu;//备注
                    flay = _IWL_DkxInfoDao.NUpdate(model);
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
            return View("Edit", new WL_DkxInfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WL_DkxInfoView sys = new WL_DkxInfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWL_DkxInfoDao.NGetModelById(id);
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
                List<WL_DkxInfoView> sys = _IWL_DkxInfoDao.NGetList_id(id) as List<WL_DkxInfoView>;
 
                foreach (var item in sys)
                {
                    item.States = 1;
                    _IWL_DkxInfoDao.NUpdate(item);
                }
                return RedirectToAction("List");
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }
        #endregion

        //生产录入跳转页面
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPageSc()
        {
            return View(new WL_DkxInfoView());
        }

        #region //物联网电控箱生产
        /// <summary>
        /// 物联网电控箱生产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditSc(WL_DkxInfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                WL_DkxInfoView modelnew = new WL_DkxInfoView();
                modelnew = _IWL_DkxInfoDao.GetDkxinfo(Getsid(model.WL_SSD));//通过sid 查找电控箱信息
                if (modelnew != null)
                { //判断是否存在该电控箱的信息
                    if (modelnew.CS_zt != 1)
                    {//是否已经生产
                        modelnew.SC_time = DateTime.Now;//生产时间
                        modelnew.CS_time = DateTime.Now;//测试时间
                        modelnew.CS_zt = 1;//测试状态 已测试
                        flay = _IWL_DkxInfoDao.NUpdate(modelnew);
                    }
                    else
                    {
                        return Json(new { result = "error2" });
                    }
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

        #region //扫码出货
        /// <summary>
        /// 扫码出货
        /// </summary>
        /// <param name="khId">客户Id</param>
        /// <param name="sid">SID码</param>
        /// <param name="mxid">明细Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Editch(string khId, string sid, string mxid)
        {
            try
            {
                  bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                WL_DkxInfoView modelnew = new WL_DkxInfoView();
                modelnew = _IWL_DkxInfoDao.GetDkxinfo(Getsid(sid));//通过sid 查找电控箱信息
             
                if (modelnew != null)
                {//检测该数据是否存在
                    if (modelnew.WL_zt == 0)
                    {
                        modelnew.Xs_jxsId = khId;//客户Id(经销商Id)
                        modelnew.Xs_datetime = DateTime.Now;//销售时间
                        modelnew.WL_zt = 1;//已销售
                        modelnew.OrdermxId = mxid;//订单明细Id
                        flay = _IWL_DkxInfoDao.NUpdate(modelnew);
                    }
                    else
                    {
                        return Json(new { result = "error2" });//已经销售
                    }
                }
                else
                {
                    return Json(new { result = "error3" });//不存在该 电控箱
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

        #region //扫码出货 保存
        /// <summary>
        /// 扫码出货
        /// </summary>
        /// <param name="khId">客户Id</param>
        /// <param name="sid">sid</param>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SmEditch(string khid, string sid, string orderId, string zsl)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                WL_DkxInfoView modelnew = new WL_DkxInfoView();
                int smcount = _IWL_DkxInfoDao.GetChcunotbyorderId(orderId);
                if (smcount < Convert.ToInt32(zsl) * 2)//后期去掉
                {//检测是否下单完成
                    modelnew = _IWL_DkxInfoDao.GetDkxinfo(Getsid(sid));//通过sid 查找电控箱信息
                    if (modelnew != null)
                    {//检测该数据是否存在
                        if (modelnew.WL_zt == 0)
                        {
                            modelnew.Xs_jxsId = khid;//客户Id(经销商Id)
                            modelnew.Xs_datetime = DateTime.Now;//销售时间
                            modelnew.WL_zt = 1;//已销售
                            modelnew.OrdermxId = orderId;//订单Id
                            flay = _IWL_DkxInfoDao.NUpdate(modelnew);
                            if (flay)
                                return Json(new { result = "success" });
                            else
                                return Json(new { result = "error" });
                        }
                        else
                        {
                            return Json(new { result = "yxs" });//已经销售
                        }
                    }
                    else
                    {
                        return Json(new { result = "bcz" });//不存在该 电控箱
                    }
                }
                else
                {
                    return Json(new { result = "fhwc" });//发货完成
                }
            }
            catch
            {
                return Json(new { result = "error" });
            }
        } 
        #endregion
        
        #region //截取sid
        public string Getsid(string a)
        {
            string s = a.Replace(" ", "").ToUpper();
            int i = s.IndexOf("D:")+2;
            int j = s.IndexOf(".KEY");
            string str = s.Substring(i, j - i);
            return str;
        } 
        #endregion

        #region 初始化经销商（物联网）设置下拉框值（取得经销权的经销商）
        /// <summary>
        /// 初始化经销商（物联网）设置下拉框值(取得经销权的)
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlJxsDropdown(string SelectedPID)
        {
            List<NACustomerinfoView> CustlistView = _INACustomerinfoDao.GetCustinfoisjxs("1") as List<NACustomerinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonlist.Add(Reasonmodel);
            if (CustlistView != null)
            {
                foreach (var item in CustlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["CustList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region 初始化经销商（物联网）设置下拉框值（只是参加买一送一活动的经销商）
        /// <summary>
        /// 初始化经销商（物联网）设置下拉框值(只是参加买一送一活动的经销商)
        /// </summary>
        /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlJxsDropdown2(string SelectedPID)
        {
            List<NACustomerinfoView> CustlistView = _INACustomerinfoDao.GetCustinfoisjxs("2") as List<NACustomerinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonlist.Add(Reasonmodel);
            if (CustlistView != null)
            {
                foreach (var item in CustlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["Cus2tList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["Cus2tList"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion

        #region //初始化工程商 设置下拉框
        public void ALGcsDropdown(string SelecteGcsId)
        {
            List<WL_GcsinfoView> GcslistView = _IWL_GcsinfoDao.NGetList() as List<WL_GcsinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonlist.Add(Reasonmodel);
            if (GcslistView != null)
            {
                foreach (var item in GcslistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.DW_name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelecteGcsId != "0")
                ViewData["GcsList"] = new SelectList(Reasonlist, "Id", "Name", SelecteGcsId);
            else
                ViewData["GcsList"] = new SelectList(Reasonlist, "Id", "Name");
        } 
        #endregion

        #region //根据Id 查询SID json
        public string GetSIDbyorderID(string orderId)
        {
            string json;
            json = JsonConvert.SerializeObject(_IWL_DkxInfoDao.GetSIDbyOrderId(orderId));
            return json;
        } 
        #endregion

        #region //初始化电控箱
        public string cshSID(string Id)
        {
            WL_DkxInfoView model = new WL_DkxInfoView();
            model = _IWL_DkxInfoDao.NGetModelById(Id);//根据Id 查询电控箱信息销缴费
            if (model.chtype != 2)
            { 
            NA_XSinfoView xsmodel = _INA_XSinfoDao.NGetModelById(model.OrdermxId);//关联的订单信息
            if (xsmodel != null)
            {
                xsmodel.SM_ZT = 0;
                _INA_XSinfoDao.NUpdate(xsmodel);
            }
            }
            model.Xs_datetime = null;//销售时间
            model.Xs_jxsId = null;//经销商
            model.Xs_gcsId = null;//工程商
            model.Sx_time = null;//上线时间
            model.WL_zt = 0;//状态
            model.WL_BXStarttime = null;//保修时间开始
            model.WL_BXEndtime = null;//保修时间结束
            model.Jf_starttime = null;//上次缴费时间
            model.Jf_endtime = null;//下次缴费时间
            model.Jf_cs = 0;//缴费次数
            model.OrdermxId = "";//销售单
            model.Is_xs = 0;//是否销售
            model.Is_sx = 0;//是否上线
            if (model.chtype == 2)
            {
                model.erp_jxscode = "";
                model.erp_jxsname = "";
                model.erp_JPorderno = "";
                model.chtype = 0;
            }
            bool flay = false;
            flay = _IWL_DkxInfoDao.NUpdate(model);
            if (flay)
                return "{\"status\":\"true\"}";//初始化成功
            else
                return "{\"status\":\"false\"}";//初始化失败
        }
        #endregion

        /// <summary>
        /// （电控箱）手动上线
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPagesdsx(string Id) {
            //ViewData["DKXId"] = Id;//电控箱Id
            WL_DkxInfoView model = _IWL_DkxInfoDao.NGetModelById(Id);
            if(model!=null)
              ALGcsDropdown(model.Xs_gcsId);
            return View("EditPagesdsx", model);
        }

        //手动上线方法
        [HttpPost]
        public JsonResult sdsxEdit(WL_DkxInfoView model, FrameController from)
        {
            bool flay = false;
            WL_DkxInfoView NewModel = new WL_DkxInfoView();
            NewModel = _IWL_DkxInfoDao.NGetModelById(model.Id);
            NewModel.Xs_gcsId = Request.Form["txtGcs"];//绑定工程上
            NewModel.Sx_time = model.Sx_time;//上线时间
            NewModel.WL_BXStarttime = model.Sx_time;//保修时间开始
            DateTime sxdatetime=Convert.ToDateTime(model.Sx_time);
            NewModel.WL_BXEndtime = Convert.ToDateTime(sxdatetime.AddMonths(16).ToShortDateString());//保修时间结束
            NewModel.Jf_endtime = Convert.ToDateTime(sxdatetime.AddMonths(12).ToShortDateString());//上次缴费时间
            NewModel.WL_zt = 2;//上线
            flay = _IWL_DkxInfoDao.NUpdate(NewModel);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        //判断出货的电控箱是否在一个月内上线
        #region //判断出货的电控箱是否在一个月内上线
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Xs_time">销售时间</param>
        /// <returns></returns>
        [HttpPost]
        public string checkIsnotOnline(string Xs_time)
        {
            string flay = "未上线（超出）";
            DateTime nowdate = DateTime.Now;//当前时间
            if (Xs_time != null && Xs_time != "")
            {
                DateTime saledate = Convert.ToDateTime(Xs_time);//销售时间
                if (Convert.ToDateTime(saledate.AddMonths(1).ToShortDateString()) > nowdate)//销售日期一个月后的日期 大于当前日期 改电控箱还未超过一个月
                {
                    flay = "未上线";
                }
            }
            else
            {
                flay = "未上线";
            }
            return "{\"status\":\"" + flay + "\"}";
        } 
        #endregion

        //是否缴费
        //通过缴费时间计算出当前是否欠费
        //根据上线时间
        #region //计算通过缴费时间计算天数
        public string JcIsqfbyjfdatatime(string sxdatetime)
        {
            DateTime nowdate = DateTime.Now;//当前时间
            DateTime sxdatetimenew = Convert.ToDateTime(sxdatetime);//上线时间
            int DAYSUM = DateDiff(nowdate,sxdatetimenew);
            //缴费剩余天数
            return DAYSUM.ToString();
        }

        #region //俩个时间之间的天数
         private static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        #endregion
        #endregion

        //各个区域的电控箱 销售上线量统计
        #region //各个区域的电控箱 销售上线量统计
        /// <summary>
        /// 统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TJdkxView()
        {
            return View();
        }

        /// <summary>
        /// 省份销售量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Getsfxsl(string sfId, string month,string Endtime, string type)
        {
            int json = 0;
            json = _IWL_DkxInfoDao.GetdkxxscountbySFIdandsj(sfId, month,Endtime, Convert.ToInt32(type));
            return "{\"status\":\"" + json + "\"}";
        }

        #region //根据省份ID 查找经销商
        [HttpPost]
        public string Getjxsbysfid(string sfId, string type)
        {
            string json;
            json = JsonConvert.SerializeObject(_INACustomerinfoDao.GetKhinfobysfIf(sfId, type));
            return json;
        }
        #endregion

        #region //根据经销商Id 销量和上线数量
        [HttpPost]
        public string GetHkIdxssl(string KhId, string month,string Endtime, string type)
        {
            int Count = 0;
            Count = _IWL_DkxInfoDao.GetdkxxscounybyKHIdandsj(KhId, month,Endtime, Convert.ToInt32(type));
            return "{\"status\":\"" + Count + "\"}";
        }
        #endregion
        
        #endregion


        //电控箱上线 工程商回访页面
        /// <summary>
        /// 工程商回访列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RVgcsList(int? pageIndex)
        {
            AlJxsDropdown(null);
            AlJxsDropdown2(null);
            PagerInfo<WL_DkxInfoView> listmodel = RVGetListPager(pageIndex, null, null,"0", "1", null,null,null);
            return View(listmodel);
        }

        /// <summary>
        /// 客户回访列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RVyhList(int? pageIndex)
        {
            AlJxsDropdown(null);
            AlJxsDropdown2(null);
            PagerInfo<WL_DkxInfoView> listmodel = RVGetListPager(pageIndex, null, null, "1", "1", null, null, null);
            return View(listmodel);
        }

        public JsonResult RvSearchList()
         {
             string Name = Request.Form["txtSearch_Name"];//经销商名称（Id）
             string Name2 = Request.Form["txtSearch_Name2"];//经销商名称（Id）
             string wl_zt = Request.Form["wl_zt"];//是否回访 0 全部 1 未回访 2已回访
             string wl_sid = Request.Form["wl_sid"];//sid码
             string startdate = Request.Form["txtSearch_startdate"];//销售开始时间
             string enddate = Request.Form["txtSearch_enddate"];//销售结束时间
             string Rvtype = Request.Form["Rvtype"];

             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<WL_DkxInfoView> listmodel = RVGetListPager(pageIndex, Name, Name2, Rvtype, wl_zt, wl_sid, startdate, enddate);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }

        private PagerInfo<WL_DkxInfoView> RVGetListPager(int? pageIndex, string Name, string Name2, string RVtype, string isrv, string SId, string startdate, string enddate)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IWL_DkxInfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IWL_DkxInfoDao.SetPagerPageSize(11);
             PagerInfo<WL_DkxInfoView> listmodel = _IWL_DkxInfoDao.GetWLdkxinfolistbyRVgcs(Name, Name2,RVtype,isrv,SId,startdate,enddate);
             return listmodel;
         }

        public ActionResult RvgcsView(string Sid, string Rvtype)
         {
             _20150510WL_ReturnVisitView model = _IWL_ReturnVisitDao.GetModelbySidrvtype(Sid,Rvtype);
             ViewBag.Sid = Sid;
             ViewBag.Rvtype = Rvtype;
             return View("RvgcsView",model);
         }

        [HttpPost]
        public JsonResult RvEdit(_20150510WL_ReturnVisitView model,FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.DKXId = Request.Form["DKXId"];//电控箱信息ID
                    model.RV_Usid = user.Id;//回访人
                    model.RVContent = model.RVContent;//回访记录
                    model.RVtype = Convert.ToInt32(Request.Form["RVtype"]);//回访对象
                    model.RVDateTime = model.RVDateTime;//回访时间
                    flay = _IWL_ReturnVisitDao.Ninsert(model);
                }
                else
                {
                    model.RVContent = model.RVContent;//回访记录
                    flay = _IWL_ReturnVisitDao.NUpdate(model);
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

        //手动批量录入电控箱SID码
        public ActionResult ManualbatchView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ManualbatchEdit()
        {
                WL_DkxInfoView model = new WL_DkxInfoView();
                model = _IWL_DkxInfoDao.GetDkxinfo(Request.Form["w_sid"]);
                SessionUser user = Session[SessionHelper.User] as SessionUser;
            bool flay=false;
            if(model==null)
            {
                int sl=Convert.ToInt32(Request.Form["sl"]);
                long sid=Convert.ToInt64(Request.Form["w_sid"])-1;
              for (int i = 0; i < sl; i++)
			  {
                 sid=sid+1;
			     model = new WL_DkxInfoView();
                  model.WL_SSD =Convert.ToString(sid);;//物联网电控箱SSD码
                     model.SC_time = model.SC_time;//生产时间
                        model.SC_PH = model.SC_PH;//生产批号
                        model.CS_time = model.CS_time;//测试时间
                        model.CS_zt = model.CS_zt;//测试状态 0 未测试 1 已测试
                        model.Xs_datetime = model.Xs_datetime;//销售时间
                        model.Xs_jxsId = model.Xs_jxsId;//经销商Id
                        model.Xs_gcsId = model.Xs_gcsId;//工程商Idw
                        model.Sx_time = model.Sx_time;//上线时间
                        model.WL_BXStarttime = model.WL_BXStarttime;//保修时间开始
                        model.WL_BXEndtime = model.WL_BXEndtime;//保修时间结束
                        model.WL_zt = model.WL_zt;//电控箱状态
                        model.Jf_zt = model.Jf_zt;//缴费状态
                        model.Jf_starttime = model.Jf_starttime;//上次缴费时间
                        model.Jf_endtime = model.Jf_endtime;//下次缴费时间
                        model.Jf_cs = model.Jf_cs;//缴费时间
                        model.C_time = DateTime.Now;//创建时间
                        model.C_name = user.Id;//创建人
                        model.Sort = model.Sort;//排序
                        model.States = model.States;//状态 0 启用 1禁用
                        model.Beizhu = model.Beizhu;//备注
                        flay = _IWL_DkxInfoDao.Ninsert(model);
                 
			  }
             if (flay)
                 return Json(new { result = "success" });
             else
                 return Json(new { result = "error" });
            }
            else
            {
             return Json(new { result = "error" });
            }
        }

        //整体检测上线检测上线
        [HttpPost]
        public string JcDxOnline()
        {
            IList<WL_DkxInfoView> modellist = _IWL_DkxInfoDao.Getscwsxinfo();
            if (modellist != null)//判断是否为空
            {
                string strSID = JsonConvert.SerializeObject(modellist);
                List<Jsonsid> model = getObjectByJson<Jsonsid>(strSID);//把查询出来的电控箱信息转换成只还有sid的list数据
                string jsonstr = JsonConvert.SerializeObject(model);//转成成JSON数据
                string url;
                //url = "http://www.sbycjk.net/ws/greeting/";
                url = "http://106.14.14.68:8088/ws/greeting/";
                string result = HttpUtility11.PostUrl(url, "sid="+jsonstr); 
                List<JsonClass> timemodel = getObjectByJson<JsonClass>(result);
                //string sxdatetime;
                foreach (var a in timemodel)
                {
                    WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                    dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(a.sid);
                    if (dkxmodel != null)
                    {
                        dkxmodel.Xs_gcsId = Request.Form["txtGcs"];//绑定工程上
                        dkxmodel.Sx_time = Convert.ToDateTime(a.time);//上线时间
                        dkxmodel.WL_BXStarttime = Convert.ToDateTime(a.time);//保修时间开始
                        DateTime sxdatetimenew = Convert.ToDateTime(a.time);
                        dkxmodel.WL_BXEndtime = Convert.ToDateTime(sxdatetimenew.AddMonths(16).ToShortDateString());//保修时间结束
                        dkxmodel.Jf_endtime = Convert.ToDateTime(sxdatetimenew.AddMonths(12).ToShortDateString());//下次缴费时间
                        //dkxmodel.WL_zt = 2;//上线
                        dkxmodel.Is_sx = 1;//上线
                        _IWL_DkxInfoDao.NUpdate(dkxmodel);
                    }
                }
                return "{\"status\":\"success\"}";//添加成功
            }
            else
            {
                return "{\"status\":\"error\"}";
            }
        }

        #region //新的检测上线
        //新的检测上线
        public ActionResult NewJcdkxView()
        {
            return View();
        }
        /// <summary>
        /// 新的检测上线
        /// </summary>
        /// <param name="startsum">开始数字</param>
        /// <param name="endsum">查询的条数</param>
        /// <returns></returns>
        public string NewJcDxonline(string startsum, string endsum)
        {
            IList<WL_DkxInfoView> modellist = _IWL_DkxInfoDao.Getwl_dkxbysum(startsum, endsum);
            if (modellist != null)//判定不为空
            {
                foreach (var a in modellist)
                {
                    string url;
                    //url = "http://www.sbycjk.net/ws/greeting2/" + a.WL_SSD;
                    url = "http://106.14.14.68:8088/ws/greeting2/" + a.WL_SSD;
                    string result = HttpUtility11.GetData(url);
                    if (result != "" && result != null)//已经上线
                    {
                        result = "[" + result + "]";
                        List<JsonClass> timemodel = getObjectByJson<JsonClass>(result);
                        foreach (var t in timemodel)
                        {
                            WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                            dkxmodel = a;
                            dkxmodel.Sx_time = Convert.ToDateTime(t.time);//上线时间
                            dkxmodel.WL_BXStarttime = Convert.ToDateTime(t.time);//保修时间开始
                            DateTime sxdatetimenew = Convert.ToDateTime(t.time);
                            dkxmodel.WL_BXEndtime = Convert.ToDateTime(sxdatetimenew.AddMonths(16).ToShortDateString());//保修时间结束
                            dkxmodel.Jf_endtime = Convert.ToDateTime(t.stoptime);//到期时间
                            dkxmodel.monitor_name = t.monitor_name;//站点名称
                            dkxmodel.Is_sx = 1;//上线
                            _IWL_DkxInfoDao.NUpdate(dkxmodel);
                        }
                    }
                }
                return modellist.Count().ToString();//检测成功 检测的条数
            }
            else
            {
                return "0";
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

        #region //单个上线检测
        //单个上线检测
        public string Jcsxtimebysid(string sid)
        {
            WL_DkxInfoView model = _IWL_DkxInfoDao.GetDkxinfobySID(sid);
            if (model != null)//判定是否为空
            {
                string strSID = JsonConvert.SerializeObject(model);
                string url;
                //url = "http://www.sbycjk.net/ws/greeting2/" + sid;
                url = "http://106.14.14.68:8088/ws/greeting2/" + sid;
                string result = HttpUtility11.GetData(url);
                if (result != "" && result != null)
                {
                    result = "[" + result + "]";
                    List<JsonClass> timemodel = getObjectByJson<JsonClass>(result);
                    foreach (var a in timemodel)
                    {
                        WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                        dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(a.sid);
                        if (dkxmodel != null)
                        {
                            dkxmodel.Sx_time = Convert.ToDateTime(a.time);//上线时间
                            dkxmodel.WL_BXStarttime = Convert.ToDateTime(a.time);//保修时间开始
                            DateTime sxdatetimenew = Convert.ToDateTime(a.time);
                            dkxmodel.WL_BXEndtime = Convert.ToDateTime(sxdatetimenew.AddMonths(16).ToShortDateString());//保修时间结束
                            dkxmodel.Jf_endtime = Convert.ToDateTime(a.stoptime);//到期时间
                            dkxmodel.monitor_name = a.monitor_name;//站点名称
                            dkxmodel.Is_sx = 1;//上线
                            _IWL_DkxInfoDao.NUpdate(dkxmodel);
                        }
                    }
                    return "1";//检测成功
                }
                else
                {
                    return "2";//尚未上线或者不是物联网电控箱
                }


            }
            else
            {
                return "0";//暂未上线
            }
        } 
        #endregion

        #region //更新原来上线的没有监控点的数据
        //更新页面
        public ActionResult updatejkdnameView()
        {
            return View();
        }

        public string Updatejkdname()
        {
            IList<WL_DkxInfoView> modellist = _IWL_DkxInfoDao.Getwldkxinfosxjkdnull();
            if (modellist != null)//判断不为空
            {
                foreach (var a in modellist)
                {
                    string url;
                    //url = "http://www.sbycjk.net/ws/greeting2/" + a.WL_SSD;
                    url = "http://106.14.14.68:8088/ws/greeting2/" + a.WL_SSD;
                    string result = HttpUtility11.GetData(url);
                    if (result != "" && result != null)
                    {
                        result = "[" + result + "]";
                        List<JsonClass> timemodel = getObjectByJson<JsonClass>(result);
                        foreach (var t in timemodel)
                        {
                            WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                            dkxmodel = a;
                            dkxmodel.Sx_time = Convert.ToDateTime(t.time);//上线时间
                            dkxmodel.WL_BXStarttime = Convert.ToDateTime(t.time);//保修时间开始
                            DateTime sxdatetimenew = Convert.ToDateTime(t.time);
                            dkxmodel.WL_BXEndtime = Convert.ToDateTime(sxdatetimenew.AddMonths(16).ToShortDateString());//保修时间结束
                            dkxmodel.Jf_endtime = Convert.ToDateTime(t.stoptime);//到期时间
                            dkxmodel.monitor_name = t.monitor_name;//站点名称
                            dkxmodel.Is_sx = 1;//上线
                            _IWL_DkxInfoDao.NUpdate(dkxmodel);
                        }
                    }
                }
                return modellist.Count().ToString();//检测成功 检测的条数
            }
            else
            {
                return "0";
            }
        }
        #endregion

        //返回json 转换实体
        public class JsonClass
        {
            public virtual string ID { get; set; }//Id
            public virtual string time { get; set; }//上线时间
            public virtual string stoptime { get; set; }//到期时间
            public virtual string lastStopTime { get; set; }//下次欠费时间
            public virtual string sid { get; set; }//返回的sid
            public virtual string monitor_name { get; set; }
        }

        //获取系统中售出未上线的SID实体
        public class Jsonsid
        {
            public virtual string WL_SSD { get; set; }
        }

        //各省电控箱销售上线情况数据
        public string testdkx(string month, string Endtime)
        {
            string Json = _IWL_DkxInfoDao.DkxsaleOnlineJson(month, Endtime);
            return Json;
        }

        //各省电控箱销售上线统计页面
        public ActionResult DkxSaleOnline_Statistics() 
        {
            return View();
        }

        /// <summary>
        /// 各经销商电控箱销售上线统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CusDkxSO_Statistics() 
        {
            return View();
        }

        /// <summary>
        /// 各经销商及一般经销商的销售上线数据
        /// </summary>
        /// <param name="month"></param>
        /// <param name="Endtime"></param>
        /// <returns></returns>
        public string CusDkxsaleOnlineJson(string month, string Endtime)
        {
            string json = _IWL_DkxInfoDao.CusDkxsaleOnlineJson(month,Endtime);
            return json;
        }

        #region //同步SID数据
        //调用接口
        public string TBsiddata()
        {
            WL_DkxInfoView dkmodel = _IWL_DkxInfoDao.Getwldkxinfonewdate();
            int t = 0;
            if (dkmodel != null) {
                if (dkmodel.sidxh != null) {
                    t =Convert.ToInt32(dkmodel.sidxh);
                }
            }
            string url;
           // url = "http://www.sbycjk.net/getsidsofshipment/getByid/" + t;
            url = "http://106.14.14.68:8088/getsidsofshipment/getByid/" + t;
            string result = HttpUtility11.GetData(url);
            List<tbsid> timemodel = getObjectByJson<tbsid>(result);
            foreach (var a in timemodel)
            {
                WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(a.sid);
                if (dkxmodel != null)//存在 -更新
                {
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.Beizhu = a.module_name;
                    _IWL_DkxInfoDao.NUpdate(dkxmodel);
                }
                else//不存在插入
                {
                    dkxmodel = new WL_DkxInfoView();
                    dkxmodel.WL_SSD = a.sid;
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.SC_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.Beizhu = a.module_name;
                    dkxmodel.Is_sx = 0;//未销售
                    dkxmodel.Is_xs = 0;//未上线
                    _IWL_DkxInfoDao.Ninsert(dkxmodel);
                }
            }
            if (timemodel == null)
            {
                return "0";
            }
            else
            {
                return timemodel.Count().ToString();//返回同步的条数
            }
        }

        //全部刷新更新sid对应的产品型号
        public string SXTBCPxhdata()
        {
            int t = 0;
            string url;
            //url = "http://www.sbycjk.net/getsidsofshipment/getByid/" + t;
            url = "http://106.14.14.68:8088/getsidsofshipment/getByid/" + t;
            string result = HttpUtility11.GetData(url);
            List<tbsid> timemodel = getObjectByJson<tbsid>(result);
            foreach (var a in timemodel)
            {
                WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(a.sid);
                if (dkxmodel != null)//存在 -更新
                {
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.Beizhu = a.module_name;
                    _IWL_DkxInfoDao.NUpdate(dkxmodel);
                }
                else//不存在插入
                {
                    dkxmodel = new WL_DkxInfoView();
                    dkxmodel.WL_SSD = a.sid;
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.SC_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.Beizhu = a.module_name;
                    dkxmodel.Is_sx = 0;//未销售
                    dkxmodel.Is_xs = 0;//未上线
                    _IWL_DkxInfoDao.Ninsert(dkxmodel);
                }
            }
            if (timemodel == null)
            {
                return "0";
            }
            else
            {
                return timemodel.Count().ToString();//返回同步的条数
            }
        }

        //同步sid数据页面
        public ActionResult TBsidjsonView()
        {
            return View();
            
        }

        //单独调用根据远程监控sidxh
        public string SIDTBbysidxh(string sidxh)
        {
            sidxh = "52833";
            string url;
            //url = "http://192.168.10.243:8081/newasia_remotexp/getsidsofshipment/getByid/" + sidxh;
            //url = "http://www.sbycjk.net/getsidsofshipment/getByid/" + sidxh;
            url = "http://106.14.14.68:8088/getsidsofshipment/getByid/" + sidxh;
            string result = HttpUtility11.GetData(url);
            return result;
        }

        //实体
        public class tbsid
        {
            public string id { get; set; }
            public string sid { get; set; }
            public string createTime { get; set; }
            public string module_name { get; set; }
        }
        #endregion

        #region //sid物联网电控箱详情查询 1.工程商信息查询 2.终端用户信息查看 3.sid上线情况 销售情况
        
         //信息查看页面
        public ActionResult SIDInfoCheckView(string sid)
        {
            ViewData["sid"] = sid;
            return View();
        }

        //通过sid查找工程商信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid">SID</param>
        /// <returns></returns>
        public string AJaxNewgcsinfobysid(string sid)
        {
            string json;
            json = JsonConvert.SerializeObject(_IWL_NewgscinfoDao.Getnewgscinfobysid(sid));
            return json;
        }

        #endregion

        #region //sid退货管理

        #region //退货分页列表页面
        public ActionResult THlist(int? pageIndex)
        {
            PagerInfo<WL_dkxthjlinfoView> listmodel = GetListPagerTH(pageIndex,null,null,null,null);
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult THSearchList()
        {
            string sid=Request.Form["txt_sid"];
            string jxsname=Request.Form["txt_jxsname"];//经销商名称
            string thdatetime=Request.Form["txt_thdatetime"];//
            string endthdatetime = Request.Form["txt_endthdatetime"];//
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WL_dkxthjlinfoView> listmodel = GetListPagerTH(pageIndex,sid,jxsname, thdatetime, endthdatetime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        //退货数据
        #region //退货分页数据
        private PagerInfo<WL_dkxthjlinfoView> GetListPagerTH(int? pageIndex,string SID, string jxsname, string startdatetime, string enddatetime)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWL_dkxthjlinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWL_dkxthjlinfoDao.SetPagerPageSize(10);
            PagerInfo<WL_dkxthjlinfoView> listmodel = _IWL_dkxthjlinfoDao.GetWL_dkxthlistPage(SID,jxsname, startdatetime, enddatetime);
            return listmodel;
        } 
        #endregion
        
        //通过订单的Id 返回经销商的数据
        public string GetCusinfobyorderId(string orderId)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(orderId);
            if (model != null)
            {
                NACustomerinfoView cusmodel = _INACustomerinfoDao.GetcusInfobyId(model.KhId);
                if (cusmodel != null)
                {
                    return cusmodel.Name;
                }
                else
                {
                    return "-";
                }
            }
            else
            {
                return "-";
            }
        }
        #endregion

        #region //经销商获取已购SID的数量

        public string GetjxsTGsidSUM(string jxsId)
        {
            int sum = 0;
            sum = _IWL_DkxInfoDao.jxqGetcountYGdkxsumbyjxsId(jxsId);
            return sum.ToString();
        }
        #endregion

        #region //经销商获取已经上线的数量
        public string GetjxsYSXsidSUM(string jxsId)
        {
            int sum = 0;
            sum = _IWL_DkxInfoDao.jxqGetcountSXdkxsumbyjxsId(jxsId);
            return sum.ToString();
        }
        #endregion

        #region //根据经销商Id查找退货的SID数量
        public string jxqGetcountTHsumbyjxsId(string jxsId)
        {
            int sum = 0;
            sum = _IWL_dkxthjlinfoDao.jxqGetTHsidbyjxsId(jxsId);
            return sum.ToString();
        }
        #endregion


        #region //已经扫描的SID 的数据 导出 显示 SID 仓库扫码时间  经销商  所在省份 

        public FileResult DCEP_JLDATA()
        {
            IList<WL_DkxInfoView> list = _IWL_DkxInfoDao.GetYSsidinfo();

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("SID");
            row1.CreateCell(1).SetCellValue("仓库扫码时间");
            row1.CreateCell(2).SetCellValue("经销商");
            row1.CreateCell(3).SetCellValue("省份");
            if (list != null)//数据不为空
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string sid = "";
                    string smtime = "";
                    string jxsname = "";
                    string qyname = "";
                    sid = list[i].WL_SSD;
                    smtime = list[i].Xs_datetime.ToString();
                    if (list[i].Xs_jxsId != "")
                    {
                         jxsname = "";
                         qyname = "";
                        try
                        {
                            NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(list[i].Xs_jxsId);
                            if (cusmodel != null)
                            {
                                jxsname = cusmodel.Name;
                            }
                            //查询省份
                            if (cusmodel.qyId != "")
                            {
                                NA_QyinfoView qymodel = _INA_QyinfoDao.NGetModelById(cusmodel.qyId);
                                if (qymodel != null)
                                {
                                    qyname = qymodel.Qyname;
                                }
                            }
                        }
                        catch
                        {
 
                        }
                    }
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(sid);
                    rowtemp.CreateCell(1).SetCellValue(smtime);
                    rowtemp.CreateCell(2).SetCellValue(jxsname);
                    rowtemp.CreateCell(3).SetCellValue(qyname);
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "已扫码出货的SID数据.xls");
        }
        #endregion
    }
}
