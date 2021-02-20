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
    public class PP_ProfitpointinfoController : Controller
    {
        IPP_TeaminfoDao _IPP_TeaminfoDao = ContextRegistry.GetContext().GetObject("PP_TeaminfoDao") as IPP_TeaminfoDao;
        IPP_ProfitpointinfoDao _IPP_ProfitpointinfoDao = ContextRegistry.GetContext().GetObject("PP_ProfitpointinfoDao") as IPP_ProfitpointinfoDao;

        //分页列表页面(收入项)
        #region //列表以及查询页面

        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            AlbTeamnameDropdown(null);
            PagerInfo<PP_ProfitpointinfoView> listmodel = GetListPager(pageIndex, null, null,null);
            return View(listmodel);
        }
        #endregion



        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string rwName = Request.Form["rwName"];//团队名称
            string tdname = Request.Form["tdname"];//责任人名称
            string protype = Request.Form["protype"];//收入类型（个人收益，团队收益）
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_ProfitpointinfoView> listmodel = GetListPager(pageIndex, rwName, tdname,protype);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<PP_ProfitpointinfoView> GetListPager(int? pageIndex, string rwname, string tdname,string protype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_ProfitpointinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_ProfitpointinfoDao.SetPagerPageSize(10);
            PagerInfo<PP_ProfitpointinfoView> listmodel = _IPP_ProfitpointinfoDao.Getprofitpage(rwname, tdname,protype, Suser);
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
        public JsonResult Edit(PP_ProfitpointinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Rwname = model.Rwname;//业务名称
                    model.Rwms = model.Rwms;//页面描述
                    model.Rwbz = model.Rwbz;//任务备注
                    model.Rwfz = model.Rwfz;//单位盈利值
                    model.Rwdw = model.Rwdw;//单位
                    model.state = model.state;//是否启用
                    model.Sort = model.Sort;//排序
                    model.Rw_teamId = Request.Form["ADListData"];//所属团队
                    model.C_name = user.Id;//创建人
                    model.C_time = DateTime.Now;//创建时间
                    model.type = 0;//收入项
                    model.ProType = model.ProType;
                    flay = _IPP_ProfitpointinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Rwname = model.Rwname;//业务名称
                    model.Rwms = model.Rwms;//页面描述
                    model.Rwbz = model.Rwbz;//任务备注
                    model.Rwfz = model.Rwfz;//单位盈利值
                    model.Rwdw = model.Rwdw;//单位
                    model.Rw_teamId = Request.Form["ADListData"];//所属团队
                    model.state = model.state;//是否启用
                    model.Sort = model.Sort;//排序
                    model.Up_name = user.Id;//更新人
                    model.Up_time = DateTime.Now;//更新时间
                    model.type = 0;//收入项
                    model.ProType = model.ProType;
                    flay = _IPP_ProfitpointinfoDao.NUpdate(model);
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
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            PP_TeaminfoView model = _IPP_TeaminfoDao.Getpp_teambyguanliId(user.Id);
            if (model != null)
                AlbTeamnameDropdown(model.Id);
            else
                AlbTeamnameDropdown(null);
            return View("Edit", new PP_ProfitpointinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            PP_ProfitpointinfoView sys = new PP_ProfitpointinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IPP_ProfitpointinfoDao.NGetModelById(id);
            AlbTeamnameDropdown(sys.Rw_teamId);
            return View("Edit", sys);
        }
        #endregion

        //分页列表页面（支出项）
        #region //列表以及查询页面

        #region //分页列表页面
        public ActionResult zcList(int? pageIndex)
        {
            AlbTeamnameDropdown(null);
            PagerInfo<PP_ProfitpointinfoView> listmodel = zcGetListPager(pageIndex, null, null,null);
            return View(listmodel);
        }
        #endregion

        #region //支出项条件查询
        public JsonResult ZCSearchList()
        {
            string rwName = Request.Form["rwName"];//团队名称
            string tdname = Request.Form["tdname"];//责任人名称
            string protype = Request.Form["protype"];//收入类型（个人收益，团队收益）
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_ProfitpointinfoView> listmodel = zcGetListPager(pageIndex, rwName, tdname, protype);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<PP_ProfitpointinfoView> zcGetListPager(int? pageIndex, string rwname, string tdname,string protype)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_ProfitpointinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_ProfitpointinfoDao.SetPagerPageSize(10);
            PagerInfo<PP_ProfitpointinfoView> listmodel = _IPP_ProfitpointinfoDao.Getprrofitzhichupage(rwname, tdname,protype, Suser);
            return listmodel;
        }
        #endregion

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
            if(SelectedId!=null)
                ViewData["teamname"] = new SelectList(Reasonlist, "Id", "Name", SelectedId);
            else
                ViewData["teamname"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //根据团队Id查找团队的json数据
        public string ajaxgetteamjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_IPP_TeaminfoDao.Getpp_teambyId(Id));
            return json;
        }
        #endregion

        #region //支出保存文本的处理方法
                /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult zcEdit(PP_ProfitpointinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Rwname = model.Rwname;//业务名称
                    model.Rwms = model.Rwms;//页面描述
                    model.Rwbz = model.Rwbz;//任务备注
                    model.Rwfz = model.Rwfz;//单位盈利值
                    model.Rwdw = model.Rwdw;//单位
                    model.state = model.state;//是否启用
                    model.Sort = model.Sort;//排序
                    model.Rw_teamId = Request.Form["ADListData"];//所属团队
                    model.C_name = user.Id;//创建人
                    model.C_time = DateTime.Now;//创建时间
                    model.type = 1;//收入项
                    model.ProType = model.ProType;
                    flay = _IPP_ProfitpointinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Rwname = model.Rwname;//业务名称
                    model.Rwms = model.Rwms;//页面描述
                    model.Rwbz = model.Rwbz;//任务备注
                    model.Rwfz = model.Rwfz;//单位盈利值
                    model.Rwdw = model.Rwdw;//单位
                    model.Rw_teamId = Request.Form["ADListData"];//所属团队
                    model.state = model.state;//是否启用
                    model.Sort = model.Sort;//排序
                    model.Up_name = user.Id;//更新人
                    model.Up_time = DateTime.Now;//更新时间
                    model.type = 1;//收入项
                    model.ProType = model.ProType;
                    flay = _IPP_ProfitpointinfoDao.NUpdate(model);
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
        public ActionResult zcaddPage()
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            PP_TeaminfoView model = _IPP_TeaminfoDao.Getpp_teambyguanliId(user.Id);
            if (model != null)
                AlbTeamnameDropdown(model.Id);
            else
                AlbTeamnameDropdown(null);
            return View("zcEdit", new PP_ProfitpointinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        public ActionResult zcEditPage(string id)
        {
            PP_ProfitpointinfoView sys = new PP_ProfitpointinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IPP_ProfitpointinfoDao.NGetModelById(id);
            AlbTeamnameDropdown(sys.Rw_teamId);
            return View("zcEdit", sys);
        } 
        #endregion

    }
}
