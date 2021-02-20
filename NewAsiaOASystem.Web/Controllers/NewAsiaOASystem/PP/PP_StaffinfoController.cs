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
    public class PP_StaffinfoController : Controller
    {
        //
        // GET: /PP_Staffinfo/
        IPP_TeaminfoDao _IPP_TeaminfoDao = ContextRegistry.GetContext().GetObject("PP_TeaminfoDao") as IPP_TeaminfoDao;
        IPP_StaffinfoDao _IPP_StaffinfoDao = ContextRegistry.GetContext().GetObject("PP_StaffinfoDao") as IPP_StaffinfoDao;
        IPP_ProfitpointinfoDao _IPP_ProfitpointinfoDao = ContextRegistry.GetContext().GetObject("PP_ProfitpointinfoDao") as IPP_ProfitpointinfoDao;
        IPP_ProfuttoStaffInfoDao _IPP_ProfuttoStaffInfoDao = ContextRegistry.GetContext().GetObject("PP_ProfuttoStaffInfoDao") as IPP_ProfuttoStaffInfoDao;
        IPP_ProfitSummaryinfoDao _IPP_ProfitSummaryinfoDao = ContextRegistry.GetContext().GetObject("PP_ProfitSummaryinfoDao") as IPP_ProfitSummaryinfoDao;
        IPP_ProfuttoStaffTDInfoDao _IPP_ProfuttoStaffTDInfoDao = ContextRegistry.GetContext().GetObject("PP_ProfuttoStaffTDInfoDao") as IPP_ProfuttoStaffTDInfoDao;
        IPP_TTShouruandzhichuDetailsinfoDao _IPP_TTShouruandzhichuDetailsinfoDao = ContextRegistry.GetContext().GetObject("PP_TTShouruandzhichuDetailsinfoDao") as IPP_TTShouruandzhichuDetailsinfoDao;
        IPP_ShouruandzhichuDetailsinfoDao _IPP_ShouruandzhichuDetailsinfoDao = ContextRegistry.GetContext().GetObject("PP_ShouruandzhichuDetailsinfoDao") as IPP_ShouruandzhichuDetailsinfoDao;

        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            AlbTeamnameDropdown(null);
            PagerInfo<PP_StaffinfoView> listmodel = GetListPager(pageIndex, null, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["Name"];//员工姓名
            string tel = Request.Form["tel"];//电话
            string bmname = Request.Form["tdname"];//团队
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_StaffinfoView> listmodel = GetListPager(pageIndex, Name,tel,bmname);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<PP_StaffinfoView> GetListPager(int? pageIndex, string name, string tel,string bmname)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_StaffinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_StaffinfoDao.SetPagerPageSize(10);
            PagerInfo<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.Getstaffinfopage(name, tel, bmname, Suser);
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
        public JsonResult Edit(PP_StaffinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Sat_Name = model.Sat_Name;//姓名
                    model.Sat_Tel = model.Sat_Tel;//电话
                    model.Sat_Sex = model.Sat_Sex;//性别
                    model.Sat_rztime = model.Sat_rztime;//入职时间
                    model.Sat_teamId = Request.Form["ADListData"];//所属团队
                    model.C_time = DateTime.Now;//创建时间
                    flay = _IPP_StaffinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Sat_Name = model.Sat_Name;//姓名
                    model.Sat_Tel = model.Sat_Tel;//电话
                    model.Sat_Sex = model.Sat_Sex;//性别
                    model.Sat_rztime = model.Sat_rztime;//入职时间
                    model.Sat_teamId = Request.Form["ADListData"];//所属团队
                   // model.C_time = DateTime.Now;//创建时间
                    flay = _IPP_StaffinfoDao.NUpdate(model);
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
            AlbTeamnameDropdown(null);
            return View("Edit", new PP_StaffinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            PP_StaffinfoView sys = new PP_StaffinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IPP_StaffinfoDao.NGetModelById(id);
            AlbTeamnameDropdown(sys.Sat_teamId);
            return View("Edit", sys);
        }
        #endregion

        #region //初始化设置团队下拉框
        public void AlbTeamnameDropdown(string SelectedId)
        {
            List<PP_TeaminfoView> pp_teamlistView = _IPP_TeaminfoDao.NGetList() as List<PP_TeaminfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (pp_teamlistView != null)
            {
                foreach (var item in pp_teamlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Team_Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedId != null)
                ViewData["teamname"] = new SelectList(Reasonlist, "Id", "Name", SelectedId);
            else
                ViewData["teamname"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //团队的主营业务根据个人查找团队的Id(个人收入项)
        public string GetProfitinfojsonbystaffId(string Id)
        {
            string json="";
            PP_StaffinfoView staffmodel = _IPP_StaffinfoDao.NGetModelById(Id);
            if (staffmodel != null)
            {
                if (staffmodel.Sat_teamId != null && staffmodel.Sat_teamId != "")
                {
                    json = JsonConvert.SerializeObject(_IPP_ProfitpointinfoDao.GetProfitinfobyteamId(staffmodel.Sat_teamId,"0")); 
                }
            }
            return json;
        }
        #endregion

        #region //团队的支出项数据根据个人的Id查找团队的Id
        public string GetProfitzhichuinfojsonbystaffId(string Id)
        {
            string json = "";
            PP_StaffinfoView staffmodel = _IPP_StaffinfoDao.NGetModelById(Id);
            if (staffmodel != null)
            {
                if (staffmodel.Sat_teamId != null && staffmodel.Sat_teamId != "")
                {
                    json = JsonConvert.SerializeObject(_IPP_ProfitpointinfoDao.GetProfitzhichuinfobyteamId(staffmodel.Sat_teamId,"0"));
                }
            }
            return json;
        }
        #endregion

        #region //个人收入项设置页面
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">个人（员工）Id</param>
        /// <returns></returns>
        public ActionResult ProfitManagePage(string Id)
        {
            ViewBag.RoleId = Id;//员工Id
            return View();
        }
        #endregion

        #region //个人收入业务提交
        public string ProfitStaffinfoEide(string StaffId, string ProfitId)
        {
            //先批量删除原来的收入项
            //查找当前的关系数据
            IList<PP_ProfuttoStaffInfoView> ptoslist = _IPP_ProfuttoStaffInfoDao.Getptoslistbystaffid(StaffId);
            if (ptoslist != null)//存在关系数据
            {
                //删除当前关系
                List<PP_ProfuttoStaffInfoView> ptslistnew = ptoslist as List<PP_ProfuttoStaffInfoView>;
                _IPP_ProfuttoStaffInfoDao.NDelete(ptslistnew);
            }
            //查找收入项信息
            IList<PP_ProfitpointinfoView> Prolist = _IPP_ProfitpointinfoDao.NGetList_id(ProfitId);
            if (Prolist != null)
            {
                foreach (var item in Prolist)
                {
                    PP_ProfuttoStaffInfoView model = new PP_ProfuttoStaffInfoView();
                    model.StaffId = StaffId;
                    model.ProfutId = item.Id;
                    model.type = 0;
                    _IPP_ProfuttoStaffInfoDao.Ninsert(model);
                }
                return "1";//编辑成功
            }
            else
            {
                return "0";//编辑失败
            }
        }
        #endregion

        #region //个人收入项数据

        public string StafttProjsonbystafttId(string staffId)
        {
            string json;
            json = JsonConvert.SerializeObject(_IPP_ProfuttoStaffInfoDao.Getptoslistbystaffid(staffId));
            return json;
        }
        
        #endregion

        #region //个人支出项设置页面
        public ActionResult ProfitzhichuMangePage(string Id)
        {
            ViewBag.staffId = Id;//员工Id
            return View();
        }
        #endregion

        #region //个人支出项提交
        public string ProfitStaffzhichuinfoEide(string StaffId, string ProfitId)
        {
            //先批量删除原来的支出项
            //查找当前的关系数据
            IList<PP_ProfuttoStaffInfoView> ptoslist = _IPP_ProfuttoStaffInfoDao.Getptoszhichulistbystaffid(StaffId);
            if (ptoslist != null)//存在关系数据
            {
                //删除当前关系
                List<PP_ProfuttoStaffInfoView> ptslistnew = ptoslist as List<PP_ProfuttoStaffInfoView>;
                _IPP_ProfuttoStaffInfoDao.NDelete(ptslistnew);
            }
            //查找收入项信息
            IList<PP_ProfitpointinfoView> Prolist = _IPP_ProfitpointinfoDao.NGetList_id(ProfitId);
            if (Prolist != null)
            {
                foreach (var item in Prolist)
                {
                    PP_ProfuttoStaffInfoView model = new PP_ProfuttoStaffInfoView();
                    model.StaffId = StaffId;
                    model.ProfutId = item.Id;
                    model.type = 1;
                    _IPP_ProfuttoStaffInfoDao.Ninsert(model);
                }
                return "1";//编辑成功
            }
            else
            {
                return "0";//编辑失败
            }

        }
        #endregion

        #region //个人支出项数据
        public string StafttProjsonzhichubystaffId(string staffId)
        {
            string json;
            json = JsonConvert.SerializeObject(_IPP_ProfuttoStaffInfoDao.Getptoszhichulistbystaffid(staffId));
            return json;
        }
        #endregion

        #region //个人收入支出明细删除
        public string shouruzhichuMXdel(string Id)
        {
            PP_ShouruandzhichuDetailsinfoView model = _IPP_ShouruandzhichuDetailsinfoDao.NGetModelById(Id);
            if (model != null)
            {
                _IPP_ShouruandzhichuDetailsinfoDao.NDelete(model);
                return "0";
            }
            else
            {
                return "1";
            }
        }
        #endregion

        #region //个人收入支出完成时间修改跳转页面
        public ActionResult shouruzhichuwctimeupdateView(string Id)
        {
            PP_ShouruandzhichuDetailsinfoView model = _IPP_ShouruandzhichuDetailsinfoDao.NGetModelById(Id);
            ViewData["Id"] = Id;
            ViewData["wctime"] = model.jl_time;
            ViewData["sl"] = model.Sum;
            ViewData["stafId"] = model.StafitId;
            return View();
        }
        //完成时间修改提交
        public string shouruzhichuwctimeupdateEide(string Id,string sum, string wctime)
        {
            PP_ShouruandzhichuDetailsinfoView model=_IPP_ShouruandzhichuDetailsinfoDao.NGetModelById(Id);
            if (model != null)
            {
                PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(model.ProfutId);
                if (promodel != null)
                {
                    DateTime a = Convert.ToDateTime(wctime);
                    wctime = a.ToString("yyyyMMdd");
                    model.Sum = Convert.ToDecimal(sum);
                    decimal dqtotle = Convert.ToDecimal(sum) * Convert.ToDecimal(promodel.Rwfz);
                    model.Total = dqtotle;
                    model.jl_time = wctime;
                    model.Up_time = DateTime.Now;
                    _IPP_ShouruandzhichuDetailsinfoDao.NUpdate(model);
                }
                return "0";
            }
            else
            {
                return "1";
            }
        }
        #endregion

        #region //收入支出明细
        //当日明细查看页面
        public ActionResult TodayMxCheckView(string stafId)
        {
            ViewData["stafId"] = stafId;//个人（员工）Id
            ViewData["shouru"] = shourubystafid(stafId);
            ViewData["zhichu"] = zhichubystafId(stafId);
            return View();
        }

        //个人每个月的总收入
        public decimal shourubystafid(string stafId)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string starttime = d1.ToString("yyyyMMdd");
            string endtime = d2.ToString("yyyyMMdd");
            decimal shouru = _IPP_ShouruandzhichuDetailsinfoDao.Getsumshourubystafid(starttime, endtime, stafId);
            return shouru;
        }
        //个人每个月的支出
        public decimal zhichubystafId(string stafId)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string starttime = d1.ToString("yyyyMMdd");
            string endtime = d2.ToString("yyyyMMdd");
            decimal zhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetsumzhichubystafId(starttime,endtime,stafId);
            return zhichu;
        }

        //支出货收入数据json
        public string AjaxTodayMxjson(string stafId, string type)
        {
            DateTime todaytime = DateTime.Now;
            string TDtime = todaytime.ToString("yyyyMMdd");
            string Json = JsonConvert.SerializeObject(_IPP_ShouruandzhichuDetailsinfoDao.GetshouruhichuMxbystaffId(stafId, type, TDtime));
            return Json;
        
        }

        //个人收入明细增加页面
        public ActionResult ShouruMXAddView(string stafId, int? pageIndex)
        {
            ViewData["stafId"] = stafId;
            //获取当前时间
            ViewData["TDtime"] = DateTime.Now.ToString("yyyy-MM-dd");
            PagerInfo<PP_ProfitpointinfoView> listmodel = GetListgenrenshouruzhichuPager(pageIndex,null,stafId,"0");
            return View(listmodel);
        }

        //个人收入明细增加页面条件查询
        public JsonResult shouruSearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//项目名称
            string stafId = Request.Form["stafId"];//个人（员工）Id
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_ProfitpointinfoView> listmodel = GetListgenrenshouruzhichuPager(pageIndex, Name,stafId,"0");
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });

        }

        #region //个人收入支出明细录入的分页数据
         private PagerInfo<PP_ProfitpointinfoView> GetListgenrenshouruzhichuPager(int? pageIndex, string rwname,string stafId,string type)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_ProfitpointinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_ProfitpointinfoDao.SetPagerPageSize(5);
            PagerInfo<PP_ProfitpointinfoView> listmodel = _IPP_ProfitpointinfoDao.GetgerenProfitpage(rwname, stafId, type);
            return listmodel;
        }
        #endregion

        //个人收入支出明细(新增)提交方法
         public string shouruzhichuMXEide(string stafId, string ProfitId, string sl, string wctime)
         {
             SessionUser user = Session[SessionHelper.User] as SessionUser;
             wctime = Convert.ToDateTime(wctime).ToString("yyyyMMdd");
             PP_ShouruandzhichuDetailsinfoView model = new PP_ShouruandzhichuDetailsinfoView();
             //检测当天是否已经存在
             if (_IPP_ShouruandzhichuDetailsinfoDao.JcshouruzhichuMxbystafidandproIdandwctime(stafId, ProfitId, wctime))//存在
             {
                  model = _IPP_ShouruandzhichuDetailsinfoDao.Getshouruzhichuinfobywctime(stafId, ProfitId, wctime);
                  if (model != null)
                  {
                      PP_ProfitpointinfoView Promodel = _IPP_ProfitpointinfoDao.NGetModelById(ProfitId);
                      if (Promodel != null)
                      {
                          model.StafitId = stafId;//个人Id
                          model.ProfutId = ProfitId;//收入项目
                          model.Sum = Convert.ToDecimal(sl) + model.Sum;//完成数量
                          model.jl_time = wctime;//完成时间
                          decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(Promodel.Rwfz);
                          model.Total = dqtotle + model.Total;//盈利
                          model.type = Promodel.type;//0 收入 1 支出
                          model.Up_Name = user.Id;
                          model.Up_time = DateTime.Now;
                          _IPP_ShouruandzhichuDetailsinfoDao.NUpdate(model);
                          return "0";//提交成功
                      }
                      else
                      {
                          return "1";//不存在项目
                      }
                  }
                  else
                  {
                      return "1";
                  }
             }
             else
             {
                 PP_ProfitpointinfoView Promodel = _IPP_ProfitpointinfoDao.NGetModelById(ProfitId);
                 if (Promodel != null)
                 {
                     model.StafitId = stafId;//个人Id
                     model.ProfutId = ProfitId;//收入项目
                     model.Sum = Convert.ToDecimal(sl);//完成数量
                     model.jl_time = wctime;//完成时间
                     model.Total = Convert.ToDecimal(sl) * Convert.ToDecimal(Promodel.Rwfz);//盈利
                     model.type = Promodel.type;//0 收入 1 支出
                     model.C_Name = user.Id;
                     model.C_time = DateTime.Now;
                     _IPP_ShouruandzhichuDetailsinfoDao.Ninsert(model);
                     return "0";//提交成功
                 }
                 else
                 {
                     return "2";//不存在
                 }
             }
         }

        //个人收入支出明细（）当天相同项目的提交方法
         public string shouruzhichuMXupdateEide(string stafId, string ProfitId, string sl, string wctime)
         {
             SessionUser user = Session[SessionHelper.User] as SessionUser;
             wctime = Convert.ToDateTime(wctime).ToString("yyyyMMdd");
             PP_ShouruandzhichuDetailsinfoView model = _IPP_ShouruandzhichuDetailsinfoDao.Getshouruzhichuinfobywctime(stafId,ProfitId,wctime);
             if (model != null)
             {
                 PP_ProfitpointinfoView Promodel = _IPP_ProfitpointinfoDao.NGetModelById(ProfitId);
                 if (Promodel != null)
                 {
                     model.StafitId = stafId;//个人Id
                     model.ProfutId = ProfitId;//收入项目
                     model.Sum = Convert.ToDecimal(sl) + model.Sum;//完成数量
                     model.jl_time = wctime;//完成时间
                     decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(Promodel.Rwfz);
                     model.Total = dqtotle + model.Total;//盈利
                     model.type = Promodel.type;//0 收入 1 支出
                     model.Up_Name = user.Id;
                     model.Up_time = DateTime.Now;
                     _IPP_ShouruandzhichuDetailsinfoDao.NUpdate(model);
                     return "0";//提交成功
                 }
                 else
                 {
                     return "1";//不存在项目
                 }
             }
             else
             {
                 return "2";//不存在明细
             }

         }

        //收入支出数据根据Id json
         public string shouruzhichubyId(string Id)
         {
             string json;
             json = JsonConvert.SerializeObject(_IPP_ProfitpointinfoDao.NGetModelById(Id));
             return json;
         }

        //个人支出明细增加页面
         public ActionResult zhichuMxAddView(string stafId, int? pageIndex)
         {
             ViewData["stafId"] = stafId;
             //获取当前时间
             ViewData["TDtime"] = DateTime.Now.ToString("yyyy-MM-dd");
             PagerInfo<PP_ProfitpointinfoView> listmodel = GetListgenrenshouruzhichuPager(pageIndex, null, stafId, "1");
             return View(listmodel);
         }

        //个人支出明细增加页面查询条件
         public JsonResult zhichuSearchList()
         {
             string Name = Request.Form["txtSearch_Name"];//项目名称
             string stafId = Request.Form["stafId"];//个人（员工）Id
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<PP_ProfitpointinfoView> listmodel = GetListgenrenshouruzhichuPager(pageIndex, Name, stafId, "1");
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }
        #endregion

        #region //根据Id查找个人名称等数据
         public string AjaxGetstafiinfo(string Id)
         {
             try
             {
                 string json;
                 json = JsonConvert.SerializeObject(_IPP_StaffinfoDao.NGetModelById(Id));
                 return json;
             }
             catch
             {
                 return null;
             }
         }
        #endregion

        #region //个人盈利汇总

        #region //数据更新页面
         //数据更新页面
         public ActionResult ProfitSumupdateView()
         {
             return View();
         }
        #endregion

        //盈利数据更新
         public string gerenProfitdataupdateEide(string Year,string Mon,string type)
         {
             string hztimeM = Year + Mon;//按照月份汇总
             //查找全部的员工
             IList<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.NGetList();
             if (listmodel != null)//不为on个
             {
                 string rse = "0";
                 if (type == "0")
                 {
                     rse = updateprofitdatabyM(Year, Mon);

                 }
                 else
                 {
                     rse = updateProfitdatabyY(Year);
                 }
                 return rse;
             }
             else
             {
                 return "0";//员工为空
             }
             
         }

        //按月更新数据
         public string updateprofitdatabyM(string Year,string Mon)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             //string hztimeM = Year + Mon;
             DateTime d1 = new DateTime(Convert.ToInt32(Year),Convert.ToInt32(Mon), 1);
             DateTime d2 = d1.AddMonths(1).AddDays(-1);
             string starttime = d1.ToString("yyyyMMdd");
             string endtime = d2.ToString("yyyyMMdd");
             string hztimeM = d1.ToString("yyyyMM");
             IList<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.NGetList();
             if (listmodel != null)//不为on个
             {
                 foreach (var item in listmodel)
                 {
                     PP_ProfitSummaryinfoView model = _IPP_ProfitSummaryinfoDao.GetprofitSinfobystafidanhztime(item.Id,hztimeM);
                     //检验是否已经存在该记录
                     if (model == null)
                     {
                         decimal shouru = _IPP_ShouruandzhichuDetailsinfoDao.Getsumshourubystafid(starttime, endtime, item.Id);//收入
                         decimal zhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetsumzhichubystafId(starttime, endtime, item.Id);//支出
                         decimal yingli = shouru - zhichu;
                         decimal ljshouru = _IPP_ShouruandzhichuDetailsinfoDao.GetleijishourubystafId(item.Id);//累计收入
                         decimal ljzhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetleijizhichubystafId(item.Id);//累计支出
                         decimal ljyingli = ljshouru - ljzhichu;//累计盈利
                         model = new PP_ProfitSummaryinfoView();
                         model.StafitId = item.Id;
                         model.Sat_teamId = item.Sat_teamId;
                         model.Shouru = shouru;
                         model.zhichu = zhichu;
                         model.yingli = yingli;
                         model.ljshouru = ljshouru;
                         model.ljzhichu = ljzhichu;
                         model.ljyingli = ljyingli;
                         model.Type = 0;//按月统计
                         model.MorY = hztimeM;//汇总时间
                         model.C_name = Suser.Id;
                         model.C_time = DateTime.Now;//创建人
                         _IPP_ProfitSummaryinfoDao.Ninsert(model);
                     }
                     else
                     {
                         decimal shouru = _IPP_ShouruandzhichuDetailsinfoDao.Getsumshourubystafid(starttime, endtime, item.Id);//收入
                         decimal zhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetsumzhichubystafId(starttime, endtime, item.Id);//支出
                         decimal yingli = shouru - zhichu;
                         decimal ljshouru = _IPP_ShouruandzhichuDetailsinfoDao.GetleijishourubystafId(item.Id);//累计收入
                         decimal ljzhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetleijizhichubystafId(item.Id);//累计支出
                         decimal ljyingli = ljshouru - ljzhichu;//累计盈利
                         model.StafitId = item.Id;
                         model.Sat_teamId = item.Sat_teamId;
                         model.Shouru = shouru;
                         model.zhichu = zhichu;
                         model.yingli = yingli;
                         model.ljshouru = ljshouru;
                         model.ljzhichu = ljzhichu;
                         model.ljyingli = ljyingli;
                         model.Type = 0;//按月统计
                         model.MorY = hztimeM;//汇总时间
                         model.C_name = Suser.Id;
                         model.Up_name = Suser.Id;
                         model.Up_time = DateTime.Now;
                         _IPP_ProfitSummaryinfoDao.NUpdate(model);
                     }
                 }
                 return "1";
             }
             else
             {
                 return "0";
             }
         }

        //按年更新数据
         public string updateProfitdatabyY(string Year)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             DateTime d1 = new DateTime(Convert.ToInt32(Year),1, 1);
             DateTime d2 = new DateTime(Convert.ToInt32(Year),12, 31);
             string starttime = d1.ToString("yyyyMMdd");
             string endtime = d2.ToString("yyyyMMdd");
             IList<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.NGetList();
             if (listmodel != null)
             {
                 foreach (var item in listmodel)
                 {
                     PP_ProfitSummaryinfoView model = _IPP_ProfitSummaryinfoDao.GetprofitSinfobystafidandhztimeY(item.Id, Year);
                     if (model == null)
                     {
                         decimal shouru = _IPP_ShouruandzhichuDetailsinfoDao.Getsumshourubystafid(starttime, endtime, item.Id);//收入
                         decimal zhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetsumzhichubystafId(starttime, endtime, item.Id);//支出
                         decimal yingli = shouru - zhichu;
                         decimal ljshouru = _IPP_ShouruandzhichuDetailsinfoDao.GetleijishourubystafId(item.Id);//累计收入
                         decimal ljzhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetleijizhichubystafId(item.Id);//累计支出
                         decimal ljyingli = ljshouru - ljzhichu;//累计盈利
                         model = new PP_ProfitSummaryinfoView();
                         model.StafitId = item.Id;
                         model.Sat_teamId = item.Sat_teamId;
                         model.Shouru = shouru;
                         model.zhichu = zhichu;
                         model.yingli = yingli;
                         model.ljshouru = ljshouru;
                         model.ljzhichu = ljzhichu;
                         model.ljyingli = ljyingli;
                         model.Type = 1;//按年统计
                         model.MorY = Year;//汇总时间
                         model.C_name = Suser.Id;
                         model.C_time = DateTime.Now;//创建人
                         _IPP_ProfitSummaryinfoDao.Ninsert(model);
                     }
                     else
                     {
                         decimal shouru = _IPP_ShouruandzhichuDetailsinfoDao.Getsumshourubystafid(starttime, endtime, item.Id);//收入
                         decimal zhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetsumzhichubystafId(starttime, endtime, item.Id);//支出
                         decimal yingli = shouru - zhichu;
                         decimal ljshouru = _IPP_ShouruandzhichuDetailsinfoDao.GetleijishourubystafId(item.Id);//累计收入
                         decimal ljzhichu = _IPP_ShouruandzhichuDetailsinfoDao.GetleijizhichubystafId(item.Id);//累计支出
                         decimal ljyingli = ljshouru - ljzhichu;//累计盈利
                         model.StafitId = item.Id;
                         model.Sat_teamId = item.Sat_teamId;
                         model.Shouru = shouru;
                         model.zhichu = zhichu;
                         model.yingli = yingli;
                         model.ljshouru = ljshouru;
                         model.ljzhichu = ljzhichu;
                         model.ljyingli = ljyingli;
                         model.Type = 1;//按年统计
                         model.MorY = Year;//汇总时间
                         model.C_name = Suser.Id;
                         model.Up_name = Suser.Id;
                         model.Up_time = DateTime.Now;
                         _IPP_ProfitSummaryinfoDao.NUpdate(model);
                     }
                 }
                 return "1";
             }
             else
             {
                 return "1";
             }
         }

         #region //生产团队管理人员的管理费用

         //生产管理费用（每天）更新页面
         public ActionResult SCguanlifeiyongView()
         {
             return View();
         }
         //生产管理人员管理费用统计录入
         public string SCguanlifeiyongupdate(string wctime)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             wctime = Convert.ToDateTime(wctime).ToString("yyyyMMdd");
             IList<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.GetguanliyuanInfo(Suser.Id);
             if (listmodel != null)
             {
                 foreach (var item in listmodel)
                 {
                     PP_ShouruandzhichuDetailsinfoView model = _IPP_ShouruandzhichuDetailsinfoDao.Getshouruguanlifeiyongbywctimeandstafid(wctime, item.Id);
                     if (model == null)//该管理人员当天不存在管理收入费用明细
                     {
                         model = new PP_ShouruandzhichuDetailsinfoView();
                         decimal scshouru = _IPP_ShouruandzhichuDetailsinfoDao.Getscteamshourusumbysj(wctime, Suser.Id);//生产团队当天收入汇总
                         model.StafitId = item.Id;//个人（员工）Id
                         model.Sum = scshouru;//数量（管理费用总和）
                         model.Total =Convert.ToDecimal(Convert.ToDouble(scshouru) * 2 * Convert.ToDouble(item.Proportion) / 1000);//总数
                         model.type = 2;//生产管理费用
                         model.jl_time = wctime;
                         model.C_Name = Suser.Id;
                         model.C_time = DateTime.Now;
                         _IPP_ShouruandzhichuDetailsinfoDao.Ninsert(model);
                     }
                     else//存在更新
                     {
                         decimal scshouru = _IPP_ShouruandzhichuDetailsinfoDao.Getscteamshourusumbysj(wctime, Suser.Id);//生产团队当天收入汇总
                         model.StafitId = item.Id;//个人（员工）Id
                         model.Sum = scshouru;//数量（管理费用综合）
                         model.Total = Convert.ToDecimal(Convert.ToDouble(scshouru) * 2 * Convert.ToDouble(item.Proportion) / 1000);//总数
                         model.type = 2;//生产管理费用
                         model.jl_time = wctime;
                         model.Up_Name = Suser.Id;
                         model.Up_time = DateTime.Now;
                         _IPP_ShouruandzhichuDetailsinfoDao.NUpdate(model);
                     }
                 }
                 return "1";
             }
             else
             {
                 return "0";
             }



         }

         #endregion

         #region //各个团队的员工的各个时间段的数据统计
           //盈利统计查看页面
         public ActionResult YLTJcheckView()
         {
             return View();
         }
           //查询员工的数据（管理员查看全部的  各个团队的管理员 查看自己团队的）
         public string yuangongjson()
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             string json = null;
             json = JsonConvert.SerializeObject(_IPP_StaffinfoDao.GetyuangongbySuer(Suser));
             return json;
         }

        //个人累计收入
         public string Ajaxgerenleijishouru(string stafId)
         {
             decimal ljsr = _IPP_ShouruandzhichuDetailsinfoDao.GetleijishourubystafId(stafId);
             return ljsr.ToString();
         }

        //个人累计支出
         public string Ajaxgerenleijizhichu(string stafId)
         {
             decimal ljzc = _IPP_ShouruandzhichuDetailsinfoDao.GetleijizhichubystafId(stafId);
             return ljzc.ToString();
         }

         //根据员工Id 时间段 收支类型 查询时间段内的收支
         public string gerenshouruzhichubywctimeandstafId(string starttime, string endtime, string type, string stafId)
         {
             decimal json;
             json = _IPP_ShouruandzhichuDetailsinfoDao.GethtgrszbystafId(starttime,endtime,stafId,type);
             return json.ToString();
         }
         #endregion
        #endregion

        #region //团体收入和支出项目和个人关系的设置
        //团体收入和支出项目个人查看页面
         public ActionResult gerenTTTProfutView(string stafId)
         {
             ViewData["stafId"] = stafId;
             return View();
         }
        //个人与团体收入的数据
         public string GetTTshourudataJson(string stafId)
         {
             string json;
             json = JsonConvert.SerializeObject(_IPP_ProfuttoStaffTDInfoDao.GetProfuttostafIdTDshouruinfobystafId(stafId));
             return json;
         }

        //个人与团体支出的数据
         public string GetTTzhichudataJson(string stafId)
         {
             string json;
             json = JsonConvert.SerializeObject(_IPP_ProfuttoStaffTDInfoDao.GetProfuttpstafIdTDzhichubystafId(stafId));
             return json;
         }

        //团体收入项目列表
         public ActionResult TTshourulist(string stafId, int? pageIndex)
         {
             ViewData["stafId"] = stafId;
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTGetlistProfitpage(pageIndex,null,"0");
             return View(listmodel);
         }

        //团队支出项目列表
         public ActionResult TTzhichulist(string stafId, int? pageIndex)
         {
             ViewData["stafId"] = stafId;
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTGetlistProfitpage(pageIndex, null, "1");
             return View(listmodel);
         }

        //团体收入支出项目的查询
         public JsonResult TTsrzcSearchList()
         {
             string Name = Request.Form["txtSearch_Name"];//项目名称
             string type = Request.Form["type"];//类型 团体收入/团体支出
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTGetlistProfitpage(pageIndex,Name,type);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }

        //属于该团队的团体收入项和支出项的分页数据
         private PagerInfo<PP_ProfitpointinfoView> TTGetlistProfitpage(int? pageIndex, string rwname, string type)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IPP_ProfitpointinfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IPP_ProfitpointinfoDao.SetPagerPageSize(5);
             PagerInfo<PP_ProfitpointinfoView> listmodel = _IPP_ProfitpointinfoDao.TTProfitshouruzhichupage(rwname,type,Suser);
             return listmodel;
         }

        //个人与团体项目关系提交方式
         public string gerenTTproEide(string stafId, string ProfitId, string zanbi,string type)
         {
             PP_ProfuttoStaffTDInfoView model = new PP_ProfuttoStaffTDInfoView();
             model = _IPP_ProfuttoStaffTDInfoDao.GetstafIdtoProfitinfobystafId(stafId,ProfitId,type);
             if (model != null)//已经存在
             {
                 model.Proportion = Convert.ToDecimal(zanbi);
                 _IPP_ProfuttoStaffTDInfoDao.NUpdate(model);
                 return "0";//提交成功
             }
             else
             {
                 model = new PP_ProfuttoStaffTDInfoView();
                 model.StaffId = stafId;
                 model.ProfutId = ProfitId;
                 model.Proportion = Convert.ToDecimal(zanbi);
                 model.Type = Convert.ToInt32(type);
                 _IPP_ProfuttoStaffTDInfoDao.Ninsert(model);
                 return "0"; //提交成功
             }
         }

        //个人与团体的关系的删除
         public string TTDetProfutostafId(string Id)
         {
             PP_ProfuttoStaffTDInfoView model = _IPP_ProfuttoStaffTDInfoDao.NGetModelById(Id);
             if (model != null)
             {
                 _IPP_ProfuttoStaffTDInfoDao.NDelete(model);
                 return "0";
             }
             else {
                 return "1";
             }
         }

        //个人与团体的关系占比修改页面
         public ActionResult ZhanbiupdateView(string Id)
         {
             PP_ProfuttoStaffTDInfoView model = _IPP_ProfuttoStaffTDInfoDao.NGetModelById(Id);
             ViewData["Id"] = Id;
             ViewData["zhanbi"] = model.Proportion;
             ViewData["stafid"] = model.StaffId;
             return View();
         }
        //占比修改提交
         public string zhanbiupdateEide(string Id, string zhanbi)
         {
             PP_ProfuttoStaffTDInfoView model = _IPP_ProfuttoStaffTDInfoDao.NGetModelById(Id);
             if (model != null)
             {
                 model.Proportion = Convert.ToDecimal(zhanbi);
                 _IPP_ProfuttoStaffTDInfoDao.NUpdate(model);
                 return "0";
             }
             else
             {
                 return "1";
             }
         }
        #endregion

        #region //团体收入支出明细查看添加
        //团体收入支出页面
         public ActionResult TTshouruzhichucheakView()
         {
             //当前登录的用户
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             PP_TeaminfoView model = _IPP_TeaminfoDao.Getpp_teambyguanliId(Suser.Id);
             if (model != null)
             {
                 ViewData["TeamId"] = model.Id;
                 ViewData["TeamName"] = model.Team_Name;
             }
             return View();
         }

        //团体当月收支明细数据
         public string GetTTsztomonby(string teamId,string type)
         {
             string json = "";
             json = JsonConvert.SerializeObject(_IPP_TTShouruandzhichuDetailsinfoDao.GetTTszMxtoMonbyteamId(teamId,type));
             return json;
         }

        //团体收入增加页面
         public ActionResult AddTTmxshouchulist(string teamId, int? pageIndex)
         {
             ViewData["teamId"] = teamId;
             ViewData["TDtime"] = DateTime.Now.ToString("yyyy-MM-dd");
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTGetlistProfitpage(pageIndex, null, "0");
             return View(listmodel);
         }

        //团体支出增加页面
         public ActionResult AddTTmxzhichulist(string teamId, int? pageIndex)
         {
             ViewData["teamId"] = teamId;
             ViewData["TDtime"] = DateTime.Now.ToString("yyyy-MM-dd");
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTGetlistProfitpage(pageIndex, null, "1");
             return View(listmodel);
         }

        //团体收支明细提交方法
        /// <summary>
         /// 团体收支明细提交方法
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="profuId"></param>
        /// <param name="sl"></param>
        /// <param name="wctime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
         public string TTszmxaddEdit(string teamId, string profuId, string sl, string wctime)
         {
             try
             {
                 PP_TTShouruandzhichuDetailsinfoView model = new PP_TTShouruandzhichuDetailsinfoView();
                 SessionUser user = Session[SessionHelper.User] as SessionUser;
                 wctime = Convert.ToDateTime(wctime).ToString("yyyyMMdd");
                 //检测是否存在相同时间相同项目的明细记录
                 model = _IPP_TTShouruandzhichuDetailsinfoDao.GetTTSZMXinfobyteamIdandprofuIdandwctime(teamId, profuId, wctime);
                 if (model != null)//存在(修改 团体项目的收入支出)
                 {
                     //查找收支项目
                     PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(profuId);
                     if (promodel != null)
                     {
                         model.TeamId = teamId;//团队Id
                         model.ProfutId = profuId;//收支项目Id
                         model.Sum = Convert.ToDecimal(sl) + model.Sum;//完成数量
                         model.jl_time = wctime;//完成时间
                         decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                         model.Total = dqtotle + model.Total;//总金额
                         model.type = promodel.type;//0 收入 1 支出
                         model.Up_Name = user.Id;
                         model.Up_time = DateTime.Now;
                         _IPP_TTShouruandzhichuDetailsinfoDao.NUpdate(model);
                         //删除先前分配
                         Delgerenfentanszdata(model.Id);
                         //重新分配
                         TTxmfbshouruzhimxadd(profuId, wctime, model.Id, Convert.ToString(model.Total));
                     }
                 }
                 else//不存在（插入 团体项目的收支明细）
                 {
                     model = new PP_TTShouruandzhichuDetailsinfoView();
                     //查找收支项目
                     PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(profuId);
                     if (promodel != null)
                     {
                         model.TeamId = teamId;//团队Id
                         model.ProfutId = profuId;//收支项目Id
                         model.Sum = Convert.ToDecimal(sl);//完成数量
                         model.jl_time = wctime;//完成时间
                         decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                         model.Total = dqtotle;//总金额
                         model.type = promodel.type;//0 收入 1 支出
                         model.C_Name = user.Id;
                         model.C_time = DateTime.Now;
                         string TTmxId = _IPP_TTShouruandzhichuDetailsinfoDao.InsertID(model);//保存返回Id
                         //分配
                         TTxmfbshouruzhimxadd(profuId, wctime, TTmxId, Convert.ToString(model.Total));
                     }
                 }
                 return "0";//提交成功
             }
             catch
             {
                 return "1";//提交失败
             }
         }

        //删除个人分摊的团体的收支数据根据团体收支的Id
         public void Delgerenfentanszdata(string TTmxId)
         {
             List<PP_ShouruandzhichuDetailsinfoView> listmodel = _IPP_ShouruandzhichuDetailsinfoDao.GetgerenTTszmxdatabyttmxId(TTmxId) as List<PP_ShouruandzhichuDetailsinfoView>;
             if (listmodel != null)
             {
                 _IPP_ShouruandzhichuDetailsinfoDao.NDelete(listmodel);
             }
         }

        //该团体项目分配
         public void TTxmfbshouruzhimxadd(string profuId, string wctime, string TTmxId,string zje)
         {
              SessionUser user = Session[SessionHelper.User] as SessionUser;
              IList<PP_ProfuttoStaffTDInfoView> listmodel = _IPP_ProfuttoStaffTDInfoDao.GetprofIdtostafIdinfobyprofuId(profuId);
             if (listmodel != null)
             {
                 foreach (var item in listmodel)
                 {
                     PP_ShouruandzhichuDetailsinfoView model = new PP_ShouruandzhichuDetailsinfoView();
                     model.StafitId = item.StaffId;//员工Id
                     model.ProfutId = profuId;//团体项目Id
                     model.type =Convert.ToInt32(item.Type);
                     model.Sum =Convert.ToDecimal(item.Proportion);//占比
                     model.Total =Convert.ToDecimal(Convert.ToDecimal(zje) * item.Proportion / 100);
                     model.TTmxId = TTmxId;
                     model.Lytype = 1;
                     model.C_time = DateTime.Now;
                     model.C_Name = user.Id;
                     model.jl_time = wctime;//完成时间
                     _IPP_ShouruandzhichuDetailsinfoDao.Ninsert(model);
                 }
             }
         }

        //团体项目收支删除
         public string TTszmxdel(string Id)
         {
             PP_TTShouruandzhichuDetailsinfoView ttmodel = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(Id);
             if (ttmodel != null)
             {
                 _IPP_TTShouruandzhichuDetailsinfoDao.NDelete(ttmodel);
                 Delgerenfentanszdata(Id);
                 return "0";
             }
             else
             {
                 return "1";
             }
         }

        //团体项目收支明细数量和完成时间修改页面
         public ActionResult TTszmxupdateView(string Id)
         {
             PP_TTShouruandzhichuDetailsinfoView ttmodel = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(Id);
             ViewData["Id"] = Id;
             ViewData["wctime"] = ttmodel.jl_time;
             ViewData["sl"] = ttmodel.Sum;//完成数量
             ViewData["ProfuId"] = ttmodel.ProfutId;//团体项Id
             return View();
         }

        //团体项目明细数量和完成时间的修改提交
         public string TTszmxupdateEide(string Id, string sl,string wctime)
         {
             PP_TTShouruandzhichuDetailsinfoView ttmodel = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(Id);
             if (ttmodel != null)
             {
                 //查找收支项目
                 PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(ttmodel.ProfutId);
                 if (promodel != null)
                 {
                     DateTime a = Convert.ToDateTime(wctime);
                     wctime = a.ToString("yyyyMMdd");
                     ttmodel.jl_time = wctime;
                     ttmodel.Up_time = DateTime.Now;
                     ttmodel.Sum = Convert.ToDecimal(sl);
                     decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                     ttmodel.Total = dqtotle;//总金额
                     _IPP_TTShouruandzhichuDetailsinfoDao.NUpdate(ttmodel);
                     Delgerenfentanszdata(Id);
                     //分配
                     TTxmfbshouruzhimxadd(ttmodel.ProfutId, wctime, Id, Convert.ToString(ttmodel.Total));
                 }
                 return "0";
             }
             else
             {
                 return "1";
             }
         }

        #endregion

        #region //团体收入明细（不固定分配的）
         //团体(不固定分配)明细列表页面
         public ActionResult TTshourutwelistView(int? pageIndex)
         {
             //当前登录的用户
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             PP_TeaminfoView model = _IPP_TeaminfoDao.Getpp_teambyguanliId(Suser.Id);
             if (model != null)
             {
                 ViewData["TeamId"] = model.Id;
                 ViewData["TeamName"] = model.Team_Name;
             }
             PagerInfo<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetListTT_shouruTwePager(pageIndex,null,null,"1");
             return View(listmodel);
         }

         #region //条件查询 团队收入（不固定分配）
         public JsonResult TTsrtweSearchList()
         {
             string srname = Request.Form["cpname"];//收入名称
             string jltime = Request.Form["jltime"];//收入时间
             string Iswcfp = Request.Form["Iswcfp"];//是否完成分配
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetListTT_shouruTwePager(pageIndex,srname,jltime,Iswcfp);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }
         #endregion

        #region //团队收入（不固定分配） 分页数据
         private PagerInfo<PP_TTShouruandzhichuDetailsinfoView> GetListTT_shouruTwePager(int? pageIndex, string srname, string jltime, string Iswcfp)
         {
             //当前登录的用户
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             PP_TeaminfoView model = _IPP_TeaminfoDao.Getpp_teambyguanliId(Suser.Id);
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IPP_TTShouruandzhichuDetailsinfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IPP_TTShouruandzhichuDetailsinfoDao.SetPagerPageSize(10);
             PagerInfo<PP_TTShouruandzhichuDetailsinfoView> listmodel = _IPP_TTShouruandzhichuDetailsinfoDao.GetTTshourutwelistpage(srname,jltime,Iswcfp,model.Id);
             return listmodel;
         }
        #endregion
        #endregion

        #region //团队固定收入支出项目（不固定分配）

        //团队收入项目列表（不固定分配）
         public ActionResult TTProfuttwelist(int? pageIndex)
         {
             ViewData["TDtime"] = DateTime.Now.ToString("yyyy-MM-dd");
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTtweGetlistProfitpage(pageIndex, null, "0");
             return View(listmodel);
         }

        //团队收支支出项目查询（不固定分配）
         public JsonResult TTsrzctweSearchList()
         {
             string Name = Request.Form["txtSearch_Name"];//项目名称
             string type = Request.Form["type"];//类型 团体收入/团体支出
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<PP_ProfitpointinfoView> listmodel = TTtweGetlistProfitpage(pageIndex, Name, type);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }

        #region //属于该团队的收入支出分页数据（不固定分配）
         private PagerInfo<PP_ProfitpointinfoView> TTtweGetlistProfitpage(int? pageIndex, string rwname, string type)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IPP_ProfitpointinfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IPP_ProfitpointinfoDao.SetPagerPageSize(8);
             PagerInfo<PP_ProfitpointinfoView> listmodel = _IPP_ProfitpointinfoDao.TTProfitsrzctwepagelist(rwname, type, Suser);
             return listmodel;
         }
        #endregion

        #region //团队收入明细增加提交（不固定分配）
        /// <summary>
         /// 团队收入明细增加提交（不固定分配）
        /// </summary>
        /// <param name="profuId">收支项目Id</param>
        /// <param name="sl">数量</param>
        /// <param name="wctime">完成时间</param>
        /// <returns></returns>
         public string TTsrmxaddtweEide(string profuId, string sl, string wctime)
         {
             try
             {
                 PP_TTShouruandzhichuDetailsinfoView model = new PP_TTShouruandzhichuDetailsinfoView();
                 //当前登录的用户
                 SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                 PP_TeaminfoView TTmodel = _IPP_TeaminfoDao.Getpp_teambyguanliId(Suser.Id);
                 wctime = Convert.ToDateTime(wctime).ToString("yyyyMMdd");//完成时间
                 if (TTmodel != null)//该管理员存在团队
                 {
                     //检测是否存在相同时间相同的项目的明细记录
                     model = _IPP_TTShouruandzhichuDetailsinfoDao.GetTTSZMXinfobyteamIdandprofuIdandwctime(TTmodel.Id,profuId,wctime);
                     if (model != null)//存在（修改 团体项目的收入 （不固定分配））
                     {
                         //如果已经完成分配了就不能修改
                         if (model.Iswcfp == 0)
                         {
                             return "1";//该时间内 该项目已经存在并且已经完成分配
                         }
                         else
                         {
                             //查找收支项目
                             PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(profuId);
                             if (promodel != null)
                             {
                                 model.TeamId = TTmodel.Id;//团队Id
                                 model.ProfutId = profuId;//收支Id
                                 model.Sum = Convert.ToDecimal(sl) + model.Sum;//完成数量
                                 decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                                 model.Total = dqtotle + model.Total;//总金额
                                 model.type = promodel.type;//0 收入 1 支出
                                 model.Up_Name = Suser.Id;
                                 model.Up_time = DateTime.Now;
                                 _IPP_TTShouruandzhichuDetailsinfoDao.NUpdate(model);
                             }
                         }
                     }
                     else//不存在 （插入 团体项目的收支明细 不固定分配）
                     {
                         model = new PP_TTShouruandzhichuDetailsinfoView();
                         //查找收支项目
                         PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(profuId);
                         if (promodel != null)
                         {
                             model.TeamId = TTmodel.Id;//团队Id
                             model.ProfutId = profuId;//收支项目Id
                             model.Sum = Convert.ToDecimal(sl);//完成数量
                             model.jl_time = wctime;//完成时间
                             decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                             model.Total = dqtotle;//总金额
                             model.type = promodel.type;//0 收入 1 支出
                             model.C_Name = Suser.Id;
                             model.C_time = DateTime.Now;
                             model.TT_type = 1;//不固定分配的 团队收入明细
                             model.Iswcfp = 1;//未分配
                             _IPP_TTShouruandzhichuDetailsinfoDao.Ninsert(model);//
                         }
                     }
                     return "2";//提交成功
                 }
                 else
                 {
                     return "0";//不是团队管理员
                 }


             }
             catch
             {
                 return "3";//提交失败
             }
         }
        #endregion
        #endregion

        #region //团队不固定收入项目 分配
        //分配明细页面
         public ActionResult TTsrtweFPmxView(string TTsrtweId)
         {
             PP_TTShouruandzhichuDetailsinfoView model = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(TTsrtweId);
             PP_ProfitpointinfoView Profitmodel = _IPP_ProfitpointinfoDao.NGetModelById(model.ProfutId);
             ViewData["xnname"] = Profitmodel.Rwname;
             ViewData["totle"] = model.Total;
             ViewData["TTsrtweId"] = TTsrtweId;
             string zb = TJfpsum(TTsrtweId);
             ViewData["zb"] = zb;
             return View();
         }

        //该项目已经分配多少的了
        public string TJfpsum(string TTsrmxId)
        {
            IList<PP_ShouruandzhichuDetailsinfoView> listmodel = _IPP_ShouruandzhichuDetailsinfoDao.GetgerenTTsrtwefpmxbyttmxId(TTsrmxId);
            decimal zb = 0;
            if (listmodel != null)
            {
                for (int i = 0; i < listmodel.Count(); i++)
                {
                    zb = zb + listmodel[i].Sum;
                }
            }
            return zb.ToString();
            
        }

        //分配明细数据
        /// <summary>
         /// 分配明细数据
        /// </summary>
         /// <param name="TTsrmxId">TTsrmxId</param>
        /// <returns></returns>
         public string AjaxTTsrtwefpmxjson(string TTsrmxId)
         {
             string json;
             json = JsonConvert.SerializeObject(_IPP_ShouruandzhichuDetailsinfoDao.GetgerenTTtwesrmxbyttmxId(TTsrmxId));
             return json;
         }

        //分配明细增加页面(员工分页列表)
         public ActionResult FPstafflist(int? pageIndex,string TTsrmxId)
         {
             if (TTsrmxId != null && TTsrmxId != "")
             {
                 Session["TTsrmxId"] = TTsrmxId;
                 ViewData["TTsrmxId"] = TTsrmxId;
             }
             else
             {
                 ViewData["TTsrmxId"] = Session["TTsrmxId"].ToString();
             }
             PagerInfo<PP_StaffinfoView> listmodel = GetlistfpAddstaffpaget(pageIndex, null, null);
             return View(listmodel);
         }
        //条件查询
         public JsonResult FPaddstaffSearchList()
         {
             string Name = Request.Form["Name"];//员工姓名
             string tel = Request.Form["tel"];//电话
             int? pageIndex = 1;
             if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                 pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
             PagerInfo<PP_StaffinfoView> listmodel = GetlistfpAddstaffpaget(pageIndex, Name, tel);
             string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
             if (listmodel != null)
                 return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
             else
                 return Json(new { result = "", PageN = PageNavigate });
         }

        //分配明细增加员工分页明细分页数据
         private PagerInfo<PP_StaffinfoView> GetlistfpAddstaffpaget(int? pageIndex, string name, string tel)
         {
             SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             int CurrentPageIndex = Convert.ToInt32(pageIndex);
             if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
             _IPP_StaffinfoDao.SetPagerPageIndex(CurrentPageIndex);
             _IPP_StaffinfoDao.SetPagerPageSize(8);
             PagerInfo<PP_StaffinfoView> listmodel = _IPP_StaffinfoDao.Getstaffinfopage(name, tel, null, Suser);
             return listmodel;
         }

        //分配明细提交
         public string FPmxaddEide(string TTsrmxId, string staffId, string zb)
         {
             PP_ShouruandzhichuDetailsinfoView model = new PP_ShouruandzhichuDetailsinfoView();
             if (TTsrmxId == "" || TTsrmxId == null)
             {
                 TTsrmxId = Session["TTsrmxId"].ToString();
             }
             PP_TTShouruandzhichuDetailsinfoView TTmodel = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(TTsrmxId);
              SessionUser Suser = Session[SessionHelper.User] as SessionUser;
             if (TTmodel != null)
             {
                 model = _IPP_ShouruandzhichuDetailsinfoDao.GetgerenttsrtwebyttsrmxIdandwctimeandstaffId(TTsrmxId,TTmodel.jl_time,staffId);
                 if (model != null)//存在 修改
                 {
                     model.StafitId = staffId;//个人Id
                     model.ProfutId = TTmodel.ProfutId;
                     model.type = Convert.ToInt32(TTmodel.type);//收入还是支出
                     model.TTmxId = TTmodel.Id;//收入明细Id
                     model.jl_time = TTmodel.jl_time;//完成时间
                     model.Lytype = 2;//团队收入 不固定分配
                     model.Total = Convert.ToDecimal(TTmodel.Total * Convert.ToDecimal(zb) / 100);//个人收入
                     model.Sum = Convert.ToDecimal(zb);
                     model.Up_Name = Suser.Id;
                     model.Up_time = DateTime.Now;
                     _IPP_ShouruandzhichuDetailsinfoDao.NUpdate(model);
                 }
                 else
                 {
                     model = new PP_ShouruandzhichuDetailsinfoView();
                     model.StafitId = staffId;//个人Id
                     model.ProfutId = TTmodel.ProfutId;
                     model.type = Convert.ToInt32(TTmodel.type);//收入还是支出
                     model.TTmxId = TTmodel.Id;//收入明细Id
                     model.jl_time = TTmodel.jl_time;//完成时间
                     model.Lytype = 2;//团队收入 不固定分配
                     model.Total = Convert.ToDecimal(TTmodel.Total * Convert.ToDecimal(zb) / 100);//个人收入
                     model.Sum = Convert.ToDecimal(zb);
                     model.C_Name = Suser.Id;
                     model.C_time = DateTime.Now;
                     _IPP_ShouruandzhichuDetailsinfoDao.Ninsert(model);
                 }
                 return "1";

             }
             else
             {
                 return "0";//异常
             }
         }

         //已经完成分配
         public string WCfpEide(string TTsrmxId)
         {
             string zb = TJfpsum(TTsrmxId);
             if (Convert.ToDecimal(zb) >= 100)
             {
                 PP_TTShouruandzhichuDetailsinfoView model = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(TTsrmxId);
                 if (model != null)
                 {
                     if (model.Iswcfp == 0)
                     {
                         return "2";//已经完成
                     }
                     else
                     {
                         model.Iswcfp = 0;
                         model.Wcfpdatetime = DateTime.Now;
                         _IPP_TTShouruandzhichuDetailsinfoDao.NUpdate(model);
                         return "3";//提交成功
                     }
                 }
                 else
                 {
                     return "1";
                 }
             }
             else
             {
                 return "0";
             }
         }

        //根据团队收入明细Id 删除 同时删除分配到个人 的明细
         public string DelTTsrmxtweEide(string TTsrmxId)
         {
             PP_TTShouruandzhichuDetailsinfoView model = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(TTsrmxId);
             if (model != null)
             {
                 List<PP_ShouruandzhichuDetailsinfoView> listmodel = _IPP_ShouruandzhichuDetailsinfoDao.GetgerenTTsrtwefpmxbyttmxId(TTsrmxId) as List<PP_ShouruandzhichuDetailsinfoView>;
                 if (listmodel != null)
                 {
                     _IPP_ShouruandzhichuDetailsinfoDao.NDelete(listmodel);
                 }
                 _IPP_TTShouruandzhichuDetailsinfoDao.NDelete(model);
                 return "0";//删除成功
             }
             else
             {
                 return "1";
             }
         }

        //编辑修改团队收入明细 页面
         public ActionResult UpDateTTtwemxView(string TTsrxmId)
         {
             PP_TTShouruandzhichuDetailsinfoView model = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(TTsrxmId);
             if (model != null)
             {
                 ViewData["Id"] = model.Id;//
                 ViewData["Sum"] = model.Sum;//
                 ViewData["wctime"] = model.jl_time;//完成时间
             }
             return View();
         }

        //团队收入明细修改提交（不固定分配）
         public string UPdatettmxtweEide(string Id, string wctime, string sl)
         {
             PP_TTShouruandzhichuDetailsinfoView model = _IPP_TTShouruandzhichuDetailsinfoDao.NGetModelById(Id);
             if (model != null)
             {
                 bool flay = false;
                 //查询收入项目
                 PP_ProfitpointinfoView promodel = _IPP_ProfitpointinfoDao.NGetModelById(model.ProfutId);
                 DateTime a = Convert.ToDateTime(wctime);
                 wctime = a.ToString("yyyyMMdd");
                 model.jl_time = wctime;
                 model.Up_time = DateTime.Now;
                 model.Sum = Convert.ToDecimal(sl);
                 decimal dqtotle = Convert.ToDecimal(sl) * Convert.ToDecimal(promodel.Rwfz);
                 model.Total = dqtotle;//总金额
                 model.Iswcfp = 1;//未完成分配
                 flay = _IPP_TTShouruandzhichuDetailsinfoDao.NUpdate(model);
                 if (flay)
                 {
                     List<PP_ShouruandzhichuDetailsinfoView> listmodel = _IPP_ShouruandzhichuDetailsinfoDao.GetgerenTTsrtwefpmxbyttmxId(model.Id) as List<PP_ShouruandzhichuDetailsinfoView>;
                     if (listmodel != null)
                     {
                         _IPP_ShouruandzhichuDetailsinfoDao.NDelete(listmodel);
                     }
                     return "0";//编辑成功
                 }
                 else
                 {
                     return "1";//编辑失败
                 }
             }
             else
             {
                 return "1";//编辑失败
             }
         }
        #endregion

    }
}
