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
using NewAsiaOASystem.Web.Controllers.NewAsiaOASystem;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WL_GcsinfoController : Controller
    {
        //
        // GET: /WL_Gcsinfo/
        IWL_GcsinfoDao _IWL_GcsinfoDao = ContextRegistry.GetContext().GetObject("WL_GcsinfoDao") as IWL_GcsinfoDao;

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<WL_GcsinfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }
        #endregion
        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["Qyname"];//物联网电控箱名称
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WL_GcsinfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<WL_GcsinfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWL_GcsinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWL_GcsinfoDao.SetPagerPageSize(11);
            PagerInfo<WL_GcsinfoView> listmodel = _IWL_GcsinfoDao.GetGcsinfoList(Name, Suser);
            return listmodel;
        }
        #endregion
        #endregion

        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(WL_GcsinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.DW_name = model.DW_name;//物联网电控箱名称
                    model.Gcs_no = model.Gcs_no;//编号（唯一识别号）
                    model.Lxr_name = model.Lxr_name;//联系人
                    model.Lxr_tel = model.Lxr_tel;//联系方式
                    model.Adress = model.Adress;//地址
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//是否启用
                    model.Beizhu = model.Beizhu;//备注
                    model.C_time = DateTime.Now;//创建时间
                    model.C_name = user.Id;//创建人
                    flay = _IWL_GcsinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.DW_name = model.DW_name;//物联网电控箱名称
                    model.Gcs_no = model.Gcs_no;//编号（唯一识别号）
                    model.Lxr_name = model.Lxr_name;//联系人
                    model.Lxr_tel = model.Lxr_tel;//联系方式
                    model.Adress = model.Adress;//地址
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//是否启用
                    model.Beizhu = model.Beizhu;//备注
                    flay = _IWL_GcsinfoDao.NUpdate(model);
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
            return View("Edit", new WL_GcsinfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WL_GcsinfoView sys = new WL_GcsinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWL_GcsinfoDao.NGetModelById(id);
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
                List<WL_GcsinfoView> sys = _IWL_GcsinfoDao.NGetList_id(id) as List<WL_GcsinfoView>;
                if (null != sys)
                {
                    if (_IWL_GcsinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../WL_CPInfo/List");
        }
        #endregion

        [HttpPost]
        public string GetGcsjsonbyId(string Id)
        {
            string json = JsonConvert.SerializeObject(_IWL_GcsinfoDao.NGetModelById(Id));
            return json;
        }

        //远程监控 供应商信息 接口
        /// <summary>
        /// 远程监控 供应商信息接口
        /// </summary>
        /// <param name="Name">工程商名称</param>
        /// <param name="Lxr">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="adress">地址</param>
        /// <param name="ycId">供应商Id(唯一识别号)</param>
        /// <param name="YcKey">密钥 4a2f262ac5ac77dbb339fbb1f93b5075</param>
        /// <returns></returns>
        [HttpPost]
        public string Editgys(string Name, string Lxr, string tel, string adress, string ycId, string YcKey)
        {
            if (YcKey == "4a2f262ac5ac77dbb339fbb1f93b5075")//接口验证是否 是远程监控的访问的
            {
                if (!_IWL_GcsinfoDao.JccfbyGcs_no(ycId))
                {
                    WL_GcsinfoView gcsmodel = new WL_GcsinfoView();//实例化工程商
                    gcsmodel.DW_name = Name;//单位名称
                    gcsmodel.Lxr_name = Lxr;//联系人
                    gcsmodel.Lxr_tel = tel;//联系方式
                    gcsmodel.Adress = adress;//地址
                    gcsmodel.Gcs_no = ycId;//工程商编号（唯一识别号 远程监控的数据记录的ID）
                    gcsmodel.C_time = DateTime.Now;//创建时间
                    if (_IWL_GcsinfoDao.Ninsert(gcsmodel))
                    {
                        return "{\"status\":\"true\"}";//添加成功
                    }
                    else
                    {
                        return "{\"status\":\"false\"}";//添加失败
                    }
                }
                else
                {
                    return "{\"status\":\"false1\"}";//重复添加
                }
            }
            else
            {
                return "{\"status\":\"false2\"}";//验证失败
            }
           
           

        }


     
    }
}
