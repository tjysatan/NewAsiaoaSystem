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
    public class WL_JxqnumberController : Controller
    {
        //
        // GET: /WL_Jxqnumber/
        IWL_JxqnumberDao _IWL_JxqnumberDao = ContextRegistry.GetContext().GetObject("WL_JxqnumberDao") as IWL_JxqnumberDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;

        #region //获取数据列表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? pageIndex)
        {
            YearDropdown(null);
            PagerInfo<_20150509WL_JxqnumberView> listmodel = GetListPager(pageIndex, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string wljxs = Request.Form["txtSearch_Year"];//经销商类别
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<_20150509WL_JxqnumberView> listmodel = GetListPager(pageIndex, Name, wljxs);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion 
 
        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<_20150509WL_JxqnumberView> GetListPager(int? pageIndex, string Name, string Year)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(11);
            PagerInfo<_20150509WL_JxqnumberView> listmodel = _IWL_JxqnumberDao.GetCinfoList(Name, Year, Suser);
            return listmodel;
        }
        #endregion


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(_20150509WL_JxqnumberView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Jxqnumber = model.Jxqnumber;//年经销量
                    model.Cus_Id=Request.Form["SelectedCusId"];//经销商Id
                    model.Year=Request.Form["SelectedYear"];//年份
                    model.States = model.States;//是否启用 0 启用 1 禁止
                    model.C_time = DateTime.Now;//创建时间
                    if (checkrepeat(model.Year, model.Cus_Id))
                    {
                        flay = _IWL_JxqnumberDao.Ninsert(model);
                    }
                }
                //修改
                else
                {
                    model.Jxqnumber = model.Jxqnumber;//年经销量
                    model.Cus_Id = Request.Form["SelectedCusId"];//经销商Id
                    model.Year = Request.Form["SelectedYear"];//年份
                    model.States = model.States;//是否启用 0 启用 1 禁止
                    model.C_time = DateTime.Now;//创建时间
                    flay = _IWL_JxqnumberDao.NUpdate(model);
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
            YearDropdown(null);
            AlJxsDropdown(null);
            return View("Edit", new _20150509WL_JxqnumberView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id, int? Pageindex)
        {
            _20150509WL_JxqnumberView sys = new _20150509WL_JxqnumberView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWL_JxqnumberDao.NGetModelById(id);
            YearDropdown(sys.Year);
            AlJxsDropdown(sys.Cus_Id);
            ViewData["pageindex"] = Pageindex;
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
                List<_20150509WL_JxqnumberView> sys = _IWL_JxqnumberDao.NGetList_id(id) as List<_20150509WL_JxqnumberView>;
                foreach (var item in sys)
                {
                    item.States = 1;
                    _IWL_JxqnumberDao.NUpdate(item);
                }
                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("List");
            }
           
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

        #region //初始化年份下拉框的值（注当前年份往后5个年份）
        public void YearDropdown(string SelectedPId)
        {
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            string Year = DateTime.Now.Date.Year.ToString();
            int intYear = Convert.ToInt32(Year) + 5;
            int chaYear = intYear - Convert.ToInt32(Year);
            string[] Yearlist;
            Yearlist = new string[chaYear];
            for (int i = 0; i < chaYear; i++)
            {
                Yearlist[i] = (Convert.ToInt32(Year) + i).ToString();
            }
            foreach (var item in Yearlist)
            {
                Reasonmodel = new GetReasonlist();
                Reasonmodel.Id = item;
                Reasonmodel.Name = item;
                Reasonlist.Add(Reasonmodel);
            }
            if (SelectedPId != "0")
                ViewData["YearList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPId);
            else
                ViewData["YearList"] = new SelectList(Reasonlist, "Id", "Name");
        } 
        #endregion

        #region //检查该经销商在该年是否存在记录
        public bool checkrepeat(string Year, string Cu_Id)
        {
            bool flay;
            flay = _IWL_JxqnumberDao.checkrepeat(Year, Cu_Id);
            return flay;
        } 
        #endregion

    }
}
