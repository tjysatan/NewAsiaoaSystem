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
using System.Xml;
using System.Runtime.Serialization.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class IQC_SopController : Controller
    {
        //
        // GET: /IQC_Sop/
        IPP_TeaminfoDao _IPP_TeaminfoDao = ContextRegistry.GetContext().GetObject("PP_TeaminfoDao") as IPP_TeaminfoDao;
        IIQC_SopInfoDao _IIQC_SopInfoDao = ContextRegistry.GetContext().GetObject("IQC_SopInfoDao") as IIQC_SopInfoDao;
        IIQC_JyconstrinfoDao _IIQC_JyconstrinfoDao = ContextRegistry.GetContext().GetObject("IQC_JyconstrinfoDao") as IIQC_JyconstrinfoDao;
        IIQC_JyffinfoDao _IIQC_JyffinfoDao = ContextRegistry.GetContext().GetObject("IQC_JyffinfoDao") as IIQC_JyffinfoDao;
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;
        IIQC_llNoticeordinfoDao _IIQC_llNoticeordinfoDao = ContextRegistry.GetContext().GetObject("IQC_llNoticeordinfoDao") as IIQC_llNoticeordinfoDao;
        IIQC_llNoticeMXordinfoDao _IIQC_llNoticeMXordinfoDao = ContextRegistry.GetContext().GetObject("IQC_llNoticeMXordinfoDao") as IIQC_llNoticeMXordinfoDao;
        IIQC_JYDDinfoDao _IIQC_JYDDinfoDao = ContextRegistry.GetContext().GetObject("IQC_JYDDinfoDao") as IIQC_JYDDinfoDao;
        IIQC_JYDjyconinfoDao _IIQC_JYDjyconinfoDao = ContextRegistry.GetContext().GetObject("IQC_JYDjyconinfoDao") as IIQC_JYDjyconinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        //进料检验作业标准管理

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<IQC_SopInfoView> listmodel = GetListPager(pageIndex, null, null, null,null,null,"0");
            return View(listmodel);
        }
        #endregion

        #region //审核分页列表
        public ActionResult shList(int? pageIndex)
        {
            PagerInfo<IQC_SopInfoView> listmodel = GetListPager(pageIndex,null,null,null,"0",null,"0");//查询待审核的数据
            return View(listmodel);
        }
        #endregion


        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string yqjname = Request.Form["yqjname"];//元器件名称
            string yqjxh = Request.Form["yqjxh"];//元器件型号
            string yqjdm = Request.Form["yqjdm"];//元器件物料代码
            string Issh = Request.Form["Issh"];//是否审核
            string fxdatetime = Request.Form["fxdatetime"];//发行时间
            string Iszf = Request.Form["Iszf"];//是否作废
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<IQC_SopInfoView> listmodel = GetListPager(pageIndex, yqjname, yqjxh, yqjdm, Issh, fxdatetime, Iszf);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<IQC_SopInfoView> GetListPager(int? pageIndex, string yqjname, string yqjxh, string yqjdm,string Issh,string fxdatetime,string Iszf)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IIQC_SopInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IIQC_SopInfoDao.SetPagerPageSize(10);
            PagerInfo<IQC_SopInfoView> listmodel = _IIQC_SopInfoDao.GetIQC_Soppagelist(yqjname,yqjxh,yqjdm,Issh,fxdatetime,Iszf);
            return listmodel;
        }
        #endregion
        #endregion

        //新建作业标准单

        #region 新建作业标准单
        //选择元器件
        public ActionResult SelyqjView()
        {
            return View("SelyqjView");
        }
        //保存选择的元器件，返回作业标准单Id
        public string GetsopId(string yqjId)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            NQ_YJinfoView yjmodel = _INQ_YJinfoDao.NGetModelById(yqjId);
            if (yjmodel != null)
            {
                //检测是否存在
                if (_IIQC_SopInfoDao.GetsopmodelbyyjId(yqjId) == null)
                {
                    IQC_SopInfoView model = new IQC_SopInfoView();
                    model.Ispz = 0;//未批准
                    model.Issh = -1;//未提交状态
                    model.Iszf = 0;//正常
                    model.YqjId = yjmodel.Id;//元器件Id
                    model.Yqjname = yjmodel.Y_Name;//名称
                    model.Yqjxh = yjmodel.Y_Ggxh;//型号
                    model.Yqjdm = yjmodel.Y_Dm;//代码
                    model.Iswc = 0;//未完成
                    model.C_name = Suser.Id;//创建人
                    model.C_datetime = DateTime.Now;//创建时间
                    string sopId = _IIQC_SopInfoDao.InsertID(model);
                    NAHelper.Insertczjl(sopId,"新建检验标准",Suser.Id);//操作记录
                    return sopId;
                }
                else
                {
                    return "0";//检验单已经存在
                }
            }
            else
            {
                return "1";//元器件不存在
            }
        }
        //查询的元器件数据ajax
        public string AJaxgetyjson(string paladat)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_INQ_YJinfoDao.GetyjlistbyNameorxhordm(paladat));
            return json;
        } 

        //作业标准的基本信息填写页面
        public ActionResult SavesopjbInfoView(string Id)
        {
            ViewData["Id"] = Id;
            IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
            return View("SavesopjbInfoView",model);
        }

        //基本信息提交
        [HttpPost]
        public JsonResult sopjbinfoEide(FormContext form, HttpPostedFileBase image, HttpPostedFileBase image2)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            bool flay = false;
            string Id=Request.Form["Id"];
            if (Request.Form["sopbianhao"] == "" || Request.Form["sopbanben"] == "" || Request.Form["fxdate"] == "" || Request.Form["soptype"] == "" )
            {
                return Json(new { result = "error2" });
            }
            IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
            if (model.Jsggsimgurl == "")
            {
                if (image == null)
                {
                    return Json(new { result = "error1" });
                }
            }
            if (model != null)
            {
                if (image != null)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/Sopjsggimg"), fileName);
                    image.SaveAs(filePath);
                    model.zlname1 = Path.GetFileName(image.FileName);
                    model.Jsggsimgurl = "/Content/upload/Sopjsggimg/" + fileName;
                }
                if (image2 != null)
                {
                    string fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "-" + Path.GetFileName(image2.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/upload/Sopjsggimg"), fileName);
                    image2.SaveAs(filePath);
                    model.zlname2 = Path.GetFileName(image2.FileName);
                    model.Jsggsimgurl2 = "/Content/upload/Sopjsggimg/" + fileName;
                }
                model.Sopbianhao = Request.Form["sopbianhao"];//文件编号
                model.Sopbanben = Request.Form["sopbanben"];//版本
                model.Sopfaxing = Convert.ToDateTime(Request.Form["fxdate"]);//发行编号
                model.Sopwztype = Request.Form["soptype"];
                flay = _IIQC_SopInfoDao.NUpdate(model);
                if (flay)
                {
                    NAHelper.Insertczjl(model.Id, "检验标准-基础数保存/修改", Suser.Id);//操作记录
                    return Json(new { result = "success" });
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }
            else
            {
                return Json(new { result = "error1" });
            }
        }

        #region //删除基本信息中的检验依据资料
        public string sopDeljyzl(string Id, string type)
        {
            try
            {
                IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
                if (type == "0")
                {
                    model.Jsggsimgurl = "";
                    model.zlname1 = "";
                }
                else
                {
                    model.Jsggsimgurl2 = "";
                    model.zlname2 = "";
                }
                if (_IIQC_SopInfoDao.NUpdate(model))
                    return "2";//删除成功
                else
                    return "1";//删除失败
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        //电气性能检验内容添加页面
        public ActionResult DQXNjyconAddView(string type,string sopId)
        {
            ViewData["type"] = type;//检验项目 0 电气性能 1 尺寸 2外观 3 可靠性 4 其他
            ViewData["sopId"] = sopId;
            return View();
        }
        //电气性能检验内容添加提交方法
        public string jyconaddEide(string type, string sopId, string jyconname, string jyyq,string QDDJ)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                IQC_JyconstrinfoView model = new IQC_JyconstrinfoView();
                model.SopId = sopId;//
                model.Jyxmname = Convert.ToInt32(type);//检验项目 0 电气性能 1尺寸 2外观 3可靠性  4其它
                model.Jyname = jyconname;//检验内容
                model.Jyyqname = jyyq;//检验仪器
                model.C_name = Suser.Id;
                model.C_time = DateTime.Now;
                model.State = 0;
                model.QDDJ = QDDJ;//缺点等级
                model.Ismj = 0;
                _IIQC_JyconstrinfoDao.Ninsert(model);
                NAHelper.Insertczjl(sopId, "检验标准-添加检验内容：" + type, Suser.Id);//操作记录
                return "0";//保存成功
            }
            catch
            {
                return "1";//保存失败
            }
        }

        //检验内容编辑页面
        public ActionResult JCconUpdateView(string Id)
        {
            IQC_JyconstrinfoView model = _IIQC_JyconstrinfoDao.NGetModelById(Id);
            return View(model);
        }
        //检验内容的编辑提交
        public string JCconupdateEide(string Id, string jyconname, string jyyq, string QDDJ)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IQC_JyconstrinfoView model = _IIQC_JyconstrinfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.Jyname = jyconname;
                model.Jyyqname = jyyq;
                model.QDDJ = QDDJ;
                _IIQC_JyconstrinfoDao.NUpdate(model);
                NAHelper.Insertczjl(model.SopId, "检验标准-修改检验内容", Suser.Id);//操作记录
                return "0";
            }
            return "1";
        }

        //检验内容数据
        public string ajaxjyconjson(string sopId, string type)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_IIQC_JyconstrinfoDao.GetjyconbysopIdxmtype(sopId,type));
            return json;
        }

        //检验方法添加编辑页面
        public ActionResult JyffAddView(string type, string sopId)
        {
            ViewData["type"] = type;//检验项目 0 电气性能 1 尺寸 2外观 3 可靠性 4 其他
            ViewData["sopId"] = sopId;
            IQC_JyffinfoView model = new IQC_JyffinfoView();
            model = _IIQC_JyffinfoDao.GetjyffmodebysopIdandxmtype(sopId, type);
            return View(model);
        }

        //删除检验内容
        public string jyconDel(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IQC_JyconstrinfoView model = _IIQC_JyconstrinfoDao.NGetModelById(Id);
            if (model != null)
            {
                bool flay = false;
                model.State = 1;
                flay=_IIQC_JyconstrinfoDao.NUpdate(model);
                if (flay)
                {
                    NAHelper.Insertczjl(model.SopId, "检验标准-删除检验内容", Suser.Id);//操作记录
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            return "1";
        }

        //设置免检活取消免检
        public string SETmjEide(string sopId, string type,string settype)
        {
            try
            {
                //查找要免检的数据
                IList<IQC_JyconstrinfoView> modellist = _IIQC_JyconstrinfoDao.GetjyconbysopIdxmtype(sopId,type);
                if (modellist != null)
                {
                    IQC_JyconstrinfoView model = new IQC_JyconstrinfoView();
                    foreach (var item in modellist)
                    {
                        model = item;
                        if (settype == "0")//不要免检
                        {
                            model.Ismj = 0;
                        }
                        if (settype == "1")
                        {
                            model.Ismj = 1;
                        }
                        _IIQC_JyconstrinfoDao.NUpdate(model);
                    }
                    return "2";//设置成功
                }
                else
                {
                    return "1";//请添加检验内容
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }

        ////检验方法提交
        //public string jyffaddEide(string type, string sopId, string jyff)
        //{
        //    try
        //    {
        //        SessionUser Suser = Session[SessionHelper.User] as SessionUser;
        //        IQC_JyffinfoView model = new IQC_JyffinfoView();
        //        jyff = Server.UrlDecode(jyff);
        //        model = _IIQC_JyffinfoDao.GetjyffmodebysopIdandxmtype(sopId, type);
        //        if (model != null)
        //        {
                     
        //            model.Jyffcont = jyff;
        //            _IIQC_JyffinfoDao.NUpdate(model);
        //            return "0";
        //        }
        //        else
        //        {
        //            model = new IQC_JyffinfoView();
        //            model.Jyffcont = jyff;
        //            model.Jyxmtype = Convert.ToInt32(type);
        //            model.sopId = sopId;
        //            model.C_name = Suser.Id;
        //            model.C_time = DateTime.Now;
        //            _IIQC_JyffinfoDao.Ninsert(model);
        //            return "1";
        //        }
        //    }
        //    catch
        //    {
        //        return "2";
        //    }
           
        //}

        //检验方法查询

        #region //检验方法提交
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult jyffaddEide( IQC_JyffinfoView model,FormContext Form)
        {
            try
            {
                string type = Request.Form["type"];
                string sopId = Request.Form["sopId"];
                //string jyff = Request.Form["LAY_demo2"].ToString();
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //IQC_JyffinfoView model = new IQC_JyffinfoView();
                ////jyff = Server.UrlDecode(jyff);
                //model = _IIQC_JyffinfoDao.GetjyffmodebysopIdandxmtype(sopId, type);
                if (!string.IsNullOrEmpty(model.Id))//更新
                {
                    IQC_JyffinfoView newmodel = _IIQC_JyffinfoDao.NGetModelById(model.Id);
                    newmodel.Jyffcont = model.Jyffcont;
                    //model.Jyffcont = jyff;
                    _IIQC_JyffinfoDao.NUpdate(newmodel);
                    NAHelper.Insertczjl(sopId, "检验标准-更新检验内容方法：" + type, Suser.Id);//操作记录
                    return Json(new { result = "0" });
                }
                else//新增
                {
                    //model = new IQC_JyffinfoView();
                    //model.Jyffcont = jyff;
                    model.Jyxmtype = Convert.ToInt32(type);
                    model.sopId = sopId;
                    model.C_name = Suser.Id;
                    model.C_time = DateTime.Now;
                    _IIQC_JyffinfoDao.Ninsert(model);
                    NAHelper.Insertczjl(sopId, "检验标准-新增检验内容方法："+type, Suser.Id);//操作记录
                    return Json(new { result = "1" });
                }
            }
            catch
            {
                return Json(new { result = "2" });
            }
        }
        #endregion

        public string jyffjson(string type, string sopId)
        {
            string json = null;
            IQC_JyffinfoView model = _IIQC_JyffinfoDao.GetjyffmodebysopIdandxmtype(sopId,type);
            json = JsonConvert.SerializeObject(model);
            return json;
        }
        #endregion

        #region //作业标准（提交）审核
        /// <summary>
        /// 只有状态 （-1） 未提交 （2） 未通过 可以提交审核
        /// </summary>
        /// <param name="Id">作业标准的唯一识别号</param>
        /// <returns></returns>
        public string SoptjshEide(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
                if (model.Issh == -1 || model.Issh == 2)
                {
                    model.Issh = 0;//待审核
                    model.Updatename = Suser.Id;
                    model.Updatetime = DateTime.Now;
                    if (_IIQC_SopInfoDao.NUpdate(model))
                    {
                        NAHelper.Insertczjl(model.Id, "提交审核", Suser.Id);//操作记录
                        return "3";//提交成功
                    }
                    else
                    {
                        return "2";//提交失败
                    }
                }
                else
                {
                    return "1";//该状态下无需提交审核
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //作业标准单审核
        //审核页面
        public ActionResult SopshView(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            ViewData["username"] = Suser.RName;//登录的真实姓名
            IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
            ViewData["jyname"] = model.Yqjname;
            ViewData["yjxh"] = model.Yqjxh;
            ViewData["Id"] = Id;
            return View();
        }

        //审核提交页面
        public string SopshEide(string Id, string Issh)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
            if (model != null)
            {
                if (Issh == "1")//通过
                {
                    model.Issh = 1;
                    model.Sopshname = Suser.Id;//审核人
                    model.Shdatetime = DateTime.Now;//审核时间
                    NAHelper.Insertczjl(model.Id, "审核通过", Suser.Id);//操作记录
                    _IIQC_SopInfoDao.NUpdate(model);
                }
                if (Issh == "2")//没有通过
                {
                    model.Issh = 2;//未通过
                    NAHelper.Insertczjl(model.Id, "审核不通过", Suser.Id);//操作记录
                    _IIQC_SopInfoDao.NUpdate(model);
                }
                return "0";//操作成功
            }
            else
            {
                return "1";//操作失败
            }
        }

        //sop信息查看页面
        public ActionResult CheaksopView(string Id)
        {
            ViewData["sopId"] = Id;
            IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
            return View("CheaksopView", model);
        }
        #endregion

        #region //作废提交
        public string IQC_SopzfEdie(string Id)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            try
            {
                IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
                if (model != null)
                {
                    bool flay = false;
                    model.Iszf = 1;
                    model.Zfdatetime = DateTime.Now;
                    flay = _IIQC_SopInfoDao.NUpdate(model);
                    if (flay)
                    {
                        NAHelper.Insertczjl(model.Id, "作废检验标准", Suser.Id);//操作记录
                        return "0";
                    }
                    else
                    {
                        return "1";
                    }
                }
                else
                {
                    return "1";
                }
            }
            catch
            {
                return "1";//提交失败
            }
        }
        #endregion

        #region //记录记录查看页面

        #endregion

        #region //来料单检验

        #region //同步K3来料检验通知单数据

        #region //数据列表
        public ActionResult llNoticelist(int? pageIndex)
        {
            PagerInfo<IQC_llNoticeMXordinfoView> listmodel = GetK3llNoticeMXListPager(pageIndex, null, null, null, null, null,"0");
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult llNoticeSearchList()
        {
            string ddno = Request.Form["txtddno"];
            string yqjwl = Request.Form["txtyqjwl"];
            string yqjname = Request.Form["txtyqjname"];
            string gyswl = Request.Form["txtgyswl"];
            string gyqname = Request.Form["txtgyqname"];
            string Isscjyd = Request.Form["txtIsscjyd"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<IQC_llNoticeMXordinfoView> listmodel = GetK3llNoticeMXListPager(pageIndex, ddno, yqjwl, yqjname, gyswl, gyqname, Isscjyd);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<IQC_llNoticeMXordinfoView> GetK3llNoticeMXListPager(int? pageIndex, string DDNO, string YJQWL, string YQJname, string gyswl, string gysname,string Isscjyd)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IIQC_llNoticeMXordinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IIQC_llNoticeMXordinfoDao.SetPagerPageSize(10);
            PagerInfo<IQC_llNoticeMXordinfoView> listmodel = _IIQC_llNoticeMXordinfoDao.GetllNoticelistPager(DDNO, YJQWL, YQJname, gyswl, gysname, Isscjyd);
            return listmodel;
        }
        #endregion

        #region //调用接口
        public string k3llNoticeInterface()
        {
            try
            {
                //查询当前最新的来料通知Id
                string lltzdId = "4870";
                IQC_llNoticeordinfoView model = _IIQC_llNoticeordinfoDao.GetllNoticeinfoorderbyddId();
                if (model != null)
                    lltzdId = model.ddId.ToString();
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/getUpdatedPoByid?fentryid=" + lltzdId;
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                List<K3llNoticemodel> timemodel = getObjectByJson<K3llNoticemodel>(sSupplier);
                if (timemodel == null)
                    return "1";//当前已经是最新的数据了
                foreach (var item in timemodel)
                {
                    //检查通知单是否存在
                    IQC_llNoticeordinfoView zmodel = _IIQC_llNoticeordinfoDao.GetllNoticeinfobyddno(item.FBillNo);
                    if (zmodel == null)
                    {
                        zmodel = new IQC_llNoticeordinfoView();
                        zmodel.ddId = Convert.ToInt32(item.FInterID);
                        zmodel.ddno = item.FBillNo;
                        zmodel.Isdis = "0";
                        zmodel.C_time = Convert.ToDateTime(item.FDate);
                        string pyddId = _IIQC_llNoticeordinfoDao.InsertID(zmodel);
                        IQC_llNoticeMXordinfoView mxmodel = new IQC_llNoticeMXordinfoView();
                        mxmodel = _IIQC_llNoticeMXordinfoDao.GetllnotceMXbysjdhandwldm(item.FBillNo, item.FItemNmber);
                        if (mxmodel == null)
                        {
                            mxmodel = new IQC_llNoticeMXordinfoView();
                            mxmodel.ddId = pyddId;//平台Id
                            mxmodel.llnoticId = Convert.ToInt32(item.FInterID);
                            mxmodel.DDNO = item.FBillNo;//送检单号
                            mxmodel.Yqjwldno = item.FItemNmber;//元器件物料
                            mxmodel.Yqjname = item.FItemName;//
                            mxmodel.Yqjxh = item.FModel;
                            mxmodel.gyswlno = item.FSupplierNumber;
                            mxmodel.gysname = item.FSupplierName;
                            mxmodel.Sum = Convert.ToDecimal(item.FAuxQty);
                            mxmodel.Dj = Convert.ToDecimal(item.FAuxPrice);
                            mxmodel.ZJ = Convert.ToDecimal(item.FAmount);
                            mxmodel.C_time = Convert.ToDateTime(item.FDate);
                            mxmodel.Isjy = 0;
                            _IIQC_llNoticeMXordinfoDao.Ninsert(mxmodel);
                        }
                        else
                        {
                            mxmodel.Sum = mxmodel.Sum + Convert.ToDecimal(item.FAuxQty);
                            mxmodel.ZJ =mxmodel.ZJ+ Convert.ToDecimal(item.FAmount);
                            mxmodel.Isjy = 0;
                            _IIQC_llNoticeMXordinfoDao.NUpdate(mxmodel);
                        }

                    }
                    else//存在
                    {
                        string pyddId = zmodel.Id;
                        IQC_llNoticeMXordinfoView mxmodel = new IQC_llNoticeMXordinfoView();
                        mxmodel.ddId = pyddId;//平台Id
                        mxmodel.llnoticId = Convert.ToInt32(item.FInterID);
                        mxmodel.DDNO = item.FBillNo;
                        mxmodel.Yqjwldno = item.FItemNmber;//元器件物料
                        mxmodel.Yqjname = item.FItemName;//
                        mxmodel.Yqjxh = item.FModel;
                        mxmodel.gyswlno = item.FSupplierNumber;
                        mxmodel.gysname = item.FSupplierName;
                        mxmodel.Sum = Convert.ToDecimal(item.FAuxQty);
                        mxmodel.Dj = Convert.ToDecimal(item.FAuxPrice);
                        mxmodel.ZJ = Convert.ToDecimal(item.FAmount);
                        mxmodel.C_time = Convert.ToDateTime(item.FDate);
                        mxmodel.Isjy = 0;
                        _IIQC_llNoticeMXordinfoDao.Ninsert(mxmodel);
                    }
                }
                return "2";//成功

            }
            catch
            {
                return "0";//调用异常
            }
        }

        #region //转换实体
        public class K3llNoticemodel
        {
            public virtual string FInterID { get; set; }//自增Id

            public virtual string FBillNo { get; set; }//单号

            public virtual string FEntryID { get; set; }

            public virtual string FItemID { get; set; }

            public virtual string FItemName { get; set; }//元器件名

            public virtual string FItemNmber { get; set; }//元器件物料


            public virtual string FModel { get; set; }//元器件型号

            public virtual string FSupplierid { get; set; }

            /// <summary>
            /// 供应商名称
            /// </summary>
            public virtual string FSupplierName { get; set; }

            /// <summary>
            /// 供应商物料代码
            /// </summary>
            public virtual string FSupplierNumber { get; set; }

            /// <summary>
            /// 元器件单位
            /// </summary>
            public virtual string FUnitName { get; set; }

            /// <summary>
            /// 采购数量
            /// </summary>
            public virtual string FAuxQty { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public virtual string FAuxPrice { get; set; }

            /// <summary>
            /// 总价
            /// </summary>
            public virtual string FAmount { get; set; }

            /// <summary>
            /// 时间
            /// </summary>
            public virtual string FDate { get; set; }

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
        #endregion

        #region //单个检验通知单刷新
        public string DGJYTZDshuaxin(string ddno)
        {
            try
            {
                IQC_llNoticeordinfoView zmodel = _IIQC_llNoticeordinfoDao.GetllNoticeinfobyddno(ddno);
                if (zmodel == null)
                    return "1";//该通知没有同步没有办法进行刷新
                string url;
                url = "http://222.92.203.58:83//Baseitem.asmx/getPoByFBillNo?fbillno=" + ddno;
                string result = HttpUtility11.GetData(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                List<K3llNoticemodel> timemodel = getObjectByJson<K3llNoticemodel>(sSupplier);
                if (timemodel == null)
                    return "2";//同步不到该数据
                //删除明细
                List<IQC_llNoticeMXordinfoView> mxmodellist = _IIQC_llNoticeMXordinfoDao.Getsjdlistmodelbyddno(ddno) as List<IQC_llNoticeMXordinfoView>;
                if (mxmodellist != null)
                {
                    _IIQC_llNoticeMXordinfoDao.NDelete(mxmodellist);
                }
                foreach (var item in timemodel)
                {
                    string pyddId = zmodel.Id;
                    IQC_llNoticeMXordinfoView mxmodel = new IQC_llNoticeMXordinfoView();
                    mxmodel = _IIQC_llNoticeMXordinfoDao.GetllnotceMXbysjdhandwldm(item.FBillNo, item.FItemNmber);
                    if (mxmodel == null)
                    {
                        mxmodel = new IQC_llNoticeMXordinfoView();
                        mxmodel.ddId = pyddId;//平台Id
                        mxmodel.llnoticId = Convert.ToInt32(item.FInterID);
                        mxmodel.DDNO = item.FBillNo;
                        mxmodel.Yqjwldno = item.FItemNmber;//元器件物料
                        mxmodel.Yqjname = item.FItemName;//
                        mxmodel.Yqjxh = item.FModel;
                        mxmodel.gyswlno = item.FSupplierNumber;
                        mxmodel.gysname = item.FSupplierName;
                        mxmodel.Sum = Convert.ToDecimal(item.FAuxQty);
                        mxmodel.Dj = Convert.ToDecimal(item.FAuxPrice);
                        mxmodel.ZJ = Convert.ToDecimal(item.FAmount);
                        mxmodel.C_time = Convert.ToDateTime(item.FDate);
                        mxmodel.Isjy = 0;
                        _IIQC_llNoticeMXordinfoDao.Ninsert(mxmodel);
                    }
                    else
                    {
                        mxmodel.Sum = mxmodel.Sum + Convert.ToDecimal(item.FAuxQty);
                        mxmodel.ZJ = mxmodel.ZJ + Convert.ToDecimal(item.FAmount);
                        mxmodel.Isjy = 0;
                        _IIQC_llNoticeMXordinfoDao.NUpdate(mxmodel);
                    }
                }
                return "3";
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //订单删除
        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="Id">检测订单明细Id</param>
        /// <returns></returns>
        public string llNoticedel(string Id)
        {
            try
            {
                IQC_llNoticeMXordinfoView model = _IIQC_llNoticeMXordinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (_IIQC_llNoticeMXordinfoDao.NDelete(model))
                        return "1";//删除成功
                    else
                        return "2";//删除失败
                }
                else
                {
                    return "3";//订单不存在
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //数量修改
        public ActionResult NumberUpdateView(string Id)
        {
            IQC_llNoticeMXordinfoView model = _IIQC_llNoticeMXordinfoDao.NGetModelById(Id);
            return View(model);
        }

        public string NumberUpdateEide(string Id,string sum)
        {
            try
            {
                IQC_llNoticeMXordinfoView model = _IIQC_llNoticeMXordinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (sum != null && sum != "")
                    {
                        model.Sum = Convert.ToDecimal(sum);
                        if (_IIQC_llNoticeMXordinfoDao.NUpdate(model))
                            return "1";//修改成功
                        else
                            return "2";//修改提交异常
                    }
                    else
                    {
                        return "3";//数量不为空
                    }
                }
                else
                {
                    return "4";//订单不存在
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #endregion

        #region //检验单生成

        public ActionResult JYDSCView(string Id)
        {
            IQC_JYDDinfoView model = new IQC_JYDDinfoView();
            model = _IIQC_JYDDinfoDao.GetJYDinfobymxId(Id);
            if (model == null)
            {
                CJjyddinfo(Id);
                model = _IIQC_JYDDinfoDao.GetJYDinfobymxId(Id);
                if (model != null)
                {
                    TBJYCON(model.JYBZId, model.Id);
                }
                else
                {
                    return View("JYDerrorView");
                }
            }
            return View(model);
        }


        //检验方法不存在的时候提示
        public ActionResult JYDerrorView()
        {
            return View();
        }
        #endregion

        #region //检验单的基础数据同步通知单明细
        public void CJjyddinfo(string mxId)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                IQC_llNoticeMXordinfoView model = _IIQC_llNoticeMXordinfoDao.NGetModelById(mxId);
                IQC_JYDDinfoView JYDDmodel = new IQC_JYDDinfoView();
                IQC_SopInfoView sopmodel = _IIQC_SopInfoDao.Getsopinfobyyqjwlno(model.Yqjwldno);
                if (sopmodel != null)//存在检验标准
                {
                    JYDDmodel.Jyddno = suijishengc();
                    JYDDmodel.MxId = mxId;
                    JYDDmodel.Yqjname = model.Yqjname;
                    JYDDmodel.Yqjxh = model.Yqjxh;
                    JYDDmodel.Yqjwl = model.Yqjwldno;
                    JYDDmodel.Gysname = model.gysname;
                    JYDDmodel.Gyswl = model.gyswlno;
                    JYDDmodel.Llsum = model.Sum;
                    JYDDmodel.Lltime = model.C_time;
                    JYDDmodel.Cjsum = Getjysum(model.Sum);
                    JYDDmodel.IsDis = 0;
                    JYDDmodel.Jydzt = 0;
                    JYDDmodel.Jydjg = 4;//未判定
                    JYDDmodel.JYBZId = sopmodel.Id;
                    JYDDmodel.C_time = DateTime.Now;
                    JYDDmodel.C_name = Suser.Id;
                    _IIQC_JYDDinfoDao.Ninsert(JYDDmodel);
                    model.Isjy = 1;
                    _IIQC_llNoticeMXordinfoDao.NUpdate(model);
                }
            }
            catch
            {
 
            }
        }
        #endregion

        #region //自动生产检验单号
        public string suijishengc()
        {
            try
            {
                string Ystr = DateTime.Now.ToString("yy");
                string Mstr = DateTime.Now.ToString("MM");
                string Dstr = DateTime.Now.ToString("dd");
                int suijishu = _IIQC_JYDDinfoDao.GetJYDcount() + 1;
                if (suijishu < 10)
                {
                    string JYSTR = Ystr + Mstr + Dstr + "0" + suijishu.ToString();
                    return JYSTR;
                }
                else
                {
                    string JYSTR = Ystr + Mstr + Dstr +suijishu.ToString();
                    return JYSTR;
                }
        
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region //根据来料数量，返回需要检验的数量
        public decimal Getjysum(decimal? llsum)
        {
            decimal jysum = 0;
            if (llsum < 2)
                jysum = 1;
            if (llsum >= 2 && llsum <= 8)
                jysum = 2;//A
            if (llsum >= 9 && llsum <= 15)
                jysum = 3;//B
            if (llsum >= 16 && llsum <= 25)
                jysum = 5;//C
            if (llsum >= 26 && llsum <= 50)
                jysum = 8;//D
            if (llsum >= 51 && llsum <= 90)
                jysum = 13;//e
            if (llsum >= 91 && llsum <= 150)
                jysum = 20;//f
            if (llsum >= 151 && llsum <= 280)
                jysum = 32;//g
            if (llsum >= 281 && llsum <= 500)
                jysum = 50;//h
            if (llsum >= 501 && llsum <= 1200)
                jysum = 80;//j
            if (llsum >= 1201 && llsum <= 3200)
                jysum = 125;//k
            if (llsum >= 3201 && llsum <= 10000)
                jysum = 200;//l
            if (llsum >= 10001 && llsum <= 35000)
                jysum = 315;//m
            if (llsum >= 35001 && llsum <= 150000)
                jysum = 500;//n
            if (llsum >= 150001 && llsum <= 500000)
                jysum = 800;//p
            if (llsum >= 500001)
                jysum = 1250;//Q
            return jysum;
        }
        #endregion

        #region //同步检验内容
        public void TBJYCON(string jybzId, string yjdId)
        {
            try
            {
                IList<IQC_JyconstrinfoView> modellist = _IIQC_JyconstrinfoDao.GetjyconbysopId(jybzId);
                if (modellist != null)
                {
                    List<IQC_JYDjyconinfoView> JYDmodellist = _IIQC_JYDjyconinfoDao.GetJYDjyconinfobyjydId(yjdId) as List<IQC_JYDjyconinfoView>;
                    if (JYDmodellist != null)
                    {
                        _IIQC_JYDjyconinfoDao.NDelete(JYDmodellist);
                    }
                    foreach (var item in modellist)
                    {
                        IQC_JYDjyconinfoView JYDmodel = new IQC_JYDjyconinfoView();
                        JYDmodel.jydId = yjdId;
                        JYDmodel.conff = item.Jyname;
                        JYDmodel.jygj = item.Jyyqname;
                        JYDmodel.type = item.Jyxmname;
                        JYDmodel.Ispd = 0;
                        JYDmodel.C_time = item.C_time;
                        JYDmodel.QDDJ = item.QDDJ;
                        JYDmodel.Ismj = item.Ismj;
                        if (item.Ismj == 1)//免检
                        {
                            JYDmodel.Ispd = 1;//ok
                        }
                        _IIQC_JYDjyconinfoDao.Ninsert(JYDmodel);
                    }
                }
            }
            catch
            {
                
            }
        }
        #endregion

        #region //查询检验测试内容数据查询查询
        public string ajaxJYDjyconinfojson(string jydId, string type)
        {
            try
            {
                string json = null;
                json = JsonConvert.SerializeObject(_IIQC_JYDjyconinfoDao.GetJYDjyconinfobyjydIdandtype(jydId,type));
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //检验测试内容判定
        //页面
        public ActionResult JYDcsconpdView(string Id)
        {
            IQC_JYDjyconinfoView model = _IIQC_JYDjyconinfoDao.NGetModelById(Id);
            return View(model);
        }
        //提交方法
        public string JYDcsconpdEide(string Id, string csjson1,string csjson2,string csjson3,string csjson4,string csjson5, string Ispd, string beizhu)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                IQC_JYDjyconinfoView model = _IIQC_JYDjyconinfoDao.NGetModelById(Id);
               // model.csdataconstr = csconjson;
                model.csdatastr1 = csjson1;
                model.csdatastr2 = csjson2;
                model.csdatastr3 = csjson3;
                model.csdatastr4 = csjson4;
                model.csdatastr5 = csjson5;
                model.Ispd = Convert.ToInt32(Ispd);
                model.beizhu = beizhu;
                model.txtime = DateTime.Now;
                model.txname = Suser.Id;
                if (_IIQC_JYDjyconinfoDao.NUpdate(model))
                    return "2";//成功
                else
                    return "1";//失败
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //检验测试结果提交
        public string JYDCSJGEide(string Id, string Bhgsum, string pdzt, string blsm, string beizhu, string LotNO)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                IQC_JYDDinfoView model = _IIQC_JYDDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (model.Jydzt == 0 || model.Jydzt == 2)
                    {
                        //检查是否存在未处理的测试项目
                      //  if (_IIQC_JYDjyconinfoDao.GetJYDjyconinfobyJYDIdwcl(Id) == null)
                      //  {
                            model.BHgesum = Convert.ToDecimal(Bhgsum);
                            model.Hgesum = model.Llsum - Convert.ToDecimal(Bhgsum);
                            model.Jydjg = Convert.ToInt32(pdzt);
                            model.Jydzt = 1;//待审核
                            model.TJtime = DateTime.Now;
                            model.Rdname = Suser.Id;
                            model.Rdtime = DateTime.Now;
                            model.beizhu = beizhu;
                            model.blmxsm = blsm;
                            model.LotNO = LotNO;
                            if (_IIQC_JYDDinfoDao.NUpdate(model))
                                return "2";
                            else
                                return "1";
                       // }
                      //  else
                       // {
                         //   return "4";//存在未处理的测试项目
                      //  }
                    }
                    else if (model.Jydzt == 1)
                    {
                        return "5";//待审核中
                    }
                    else
                    {
                        return "6";//审核完成
                    }
                }
                return "3";//空
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //检验测试工具查看
        public ActionResult JYCSGJView(string Id, string jybzId, string type)
        {
            IQC_JYDjyconinfoView model = _IIQC_JYDjyconinfoDao.NGetModelById(Id);
            ViewData["jybzId"] = jybzId;
            ViewData["type"] = type;
            return View(model);
        }
        #endregion

        #region //检验测试单管理

        #region //数据列表（显示全部的 按照状态和测试生产时间排序）
        public ActionResult JYDDlist(int? pageIndex)
        {
            PagerInfo<IQC_JYDDinfoView> listmodel = GetJYDDinfolistPager(pageIndex,null,null,null,null,null,null,null,null);
            return View(listmodel);
        }
        #endregion

        #region //检验测试单审核管理（默认待审核的显示 待审核/未通过/完成）
        public ActionResult JYDDshlist(int? pageIndex)
        {
            PagerInfo<IQC_JYDDinfoView> listmodel = GetJYDDinfolistPager(pageIndex, null, null, null, null, "1",null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //条件查询
        public JsonResult JYDDSearchList()
        {
            string jyddno = Request.Form["txtjyddno"];
            string gysname = Request.Form["txtgysname"];
            string yqjname = Request.Form["txtyqjname"];
            string yqjxh = Request.Form["txtyqjxh"];
            string clzt = Request.Form["txtclzt"];
            string cljg = Request.Form["txtcljg"];
            string shstarttime = Request.Form["txtshstarttime"];
            string shendtime = Request.Form["txtshendtime"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<IQC_JYDDinfoView> listmodel = GetJYDDinfolistPager(pageIndex, jyddno, gysname, yqjname, yqjxh, clzt, cljg,shstarttime,shendtime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }    
        #endregion

        #region //分页数据
        private PagerInfo<IQC_JYDDinfoView> GetJYDDinfolistPager(int? pageIndex, string jyddno, string gysname, string yqjname, string yqjxh, string clzt, string cljg,string shstarttime,string shendtime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IIQC_JYDDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IIQC_JYDDinfoDao.SetPagerPageSize(10);
            PagerInfo<IQC_JYDDinfoView> listmodel = _IIQC_JYDDinfoDao.GetJYDDlistPager(jyddno, gysname, yqjname, yqjxh, clzt, cljg,shstarttime,shendtime);
            return listmodel;
        }
        #endregion

        #region //测试单明细查看
        public ActionResult JYDxinqingView(string Id)
        {
            IQC_JYDDinfoView model = _IIQC_JYDDinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion

        #region //审核页面
        public ActionResult JYDshView(string Id)
        {
            IQC_JYDDinfoView model = new IQC_JYDDinfoView();
            model = _IIQC_JYDDinfoDao.NGetModelById(Id);
            return View(model);
        }

        //审核提交
        public string jydshEide(string Id,string Istg,string bgyycon)
        {
            try
            {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                IQC_JYDDinfoView model = new IQC_JYDDinfoView();
                model = _IIQC_JYDDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    if (model.Jydzt == 0)
                    {
                        return "4"; 
                    }
                    if (Istg == "0")//通过
                    {
                        model.Jydzt = 3;
                        model.shdatime = DateTime.Now;
                        model.shname = Suser.Id;
                        _IIQC_JYDDinfoDao.NUpdate(model);
                        return "2";
                    }
                    else
                    {
                        model.Jydzt = 2;
                        model.thyycon = bgyycon;
                        model.shdatime = DateTime.Now;
                        model.shname = Suser.Id;
                        _IIQC_JYDDinfoDao.NUpdate(model);
                        return "3";
                    }
                }
                else
                {
                    return "1";
                }
            }
            catch
            {
                return "0";//提交异常
            }
        }
        #endregion

        #region //检验单号修改
        public string JYDnoupdateEide(string Id, string dhno)
        {
            try
            {
                IQC_JYDDinfoView model = _IIQC_JYDDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.Jyddno = dhno;
                    if (_IIQC_JYDDinfoDao.NUpdate(model))
                    {
                        return "0";//提交成功
                    }
                    else
                    {
                        return "1";//提交失败
                    }
                }
                return "3";//提交失败
            }
            catch
            {
                return "2";//提交失败
            }
        }
        #endregion

        #region //检验记录打印页面
        public ActionResult jydMXPrintView(string strattime, string endtime)
        {
            ViewData["strattime"] = strattime;
            ViewData["endtime"] = endtime;
            return View();
        }
        #endregion

        #region //检验测试的逻辑删除
        /// <summary>
        /// 检验测试的逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string JYDDDELEide(string Id)
        {
            try
            {
                IQC_JYDDinfoView model = _IIQC_JYDDinfoDao.NGetModelById(Id);
                if (model != null)
                {
                    model.IsDis = 1;//逻辑删除
                    if (_IIQC_JYDDinfoDao.NUpdate(model))
                    {
                        return "3";//删除成功
                    }
                    else
                    {
                        return "2";//提交失败
                    }
                }
                else
                {
                    return "1";//不为空
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion
        #endregion

        #region //查询检验标准根据Id
        public string GetjybzinfobyId(string Id)
        {
            try
            {
                IQC_SopInfoView model = _IIQC_SopInfoDao.NGetModelById(Id);
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

        #region //审核通过的检验测试数据
        public string GetshwcdataDY(string strattime, string endtime)
        {
            IList<IQC_JYDDinfoView> list = _IIQC_JYDDinfoDao.GetSHTGDATAbyshtime(strattime, endtime);
            string json = "";
            json = JsonConvert.SerializeObject(list);
            return json;
        }
        
        #endregion

        #region //审核过的检验测试的导出功能

        public FileResult shwcdatcdataDC(string strattime, string endtime)
        {
            IList<IQC_JYDDinfoView> list = _IIQC_JYDDinfoDao.GetSHTGDATAbyshtime(strattime, endtime);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("检验日期");
            row1.CreateCell(2).SetCellValue("供应商名称");
            row1.CreateCell(3).SetCellValue("零件名称");
            row1.CreateCell(4).SetCellValue("零件规格");
            row1.CreateCell(5).SetCellValue("LOT NO");
            row1.CreateCell(6).SetCellValue("来料数量");
            row1.CreateCell(7).SetCellValue("抽样数量");
            row1.CreateCell(8).SetCellValue("不良数");
            row1.CreateCell(9).SetCellValue("不良明细");
            row1.CreateCell(10).SetCellValue("检验结果");
            row1.CreateCell(11).SetCellValue("IQC");
            row1.CreateCell(12).SetCellValue("进料批号");
            row1.CreateCell(13).SetCellValue("备注");
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    DateTime tjsj =Convert.ToDateTime(list[i].TJtime);
                    string tjshsj = tjsj.ToString("yyyy-MM-dd");

          

                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(i+1);
                    rowtemp.CreateCell(1).SetCellValue(tjshsj);//提交审核的时间
                    rowtemp.CreateCell(2).SetCellValue(list[i].Gysname.ToString());//供应商名称
                    rowtemp.CreateCell(3).SetCellValue(list[i].Yqjname.ToString());
                    rowtemp.CreateCell(4).SetCellValue(list[i].Yqjxh.ToString());
                    rowtemp.CreateCell(5).SetCellValue(list[i].LotNO);
                    rowtemp.CreateCell(6).SetCellValue(Getzs(list[i].Llsum.ToString()));
                    rowtemp.CreateCell(7).SetCellValue(Getzs(list[i].Cjsum.ToString()));
                    rowtemp.CreateCell(8).SetCellValue(Getzs(list[i].BHgesum.ToString()));
                    rowtemp.CreateCell(9).SetCellValue(list[i].blmxsm.ToString());
                    if (list[i].Jydjg == 0)
                    {
                        rowtemp.CreateCell(10).SetCellValue("合格");
                    }
                   else if (list[i].Jydjg == 1)
                    {
                        rowtemp.CreateCell(10).SetCellValue("退货");
                    }
                    else if (list[i].Jydjg == 2)
                    {
                        rowtemp.CreateCell(10).SetCellValue("特采");
                    }
                    else if (list[i].Jydjg == 3)
                    {
                        rowtemp.CreateCell(10).SetCellValue("试用");
                    }
                    else if (list[i].Jydjg == 4)
                    {
                        rowtemp.CreateCell(10).SetCellValue("未判断");
                    }
                    rowtemp.CreateCell(11).SetCellValue(GetsysnamebyId(list[i].Rdname));
                    rowtemp.CreateCell(12).SetCellValue(list[i].Jyddno.ToString());
                    rowtemp.CreateCell(13).SetCellValue(list[i].beizhu.ToString());
                }
            }
           // string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "进料检验汇总表.xls");
        }


        public string Getzs(string str)
        {
           string  ls_Old = str;//赋原始字符串值给变量
           int li_Index = ls_Old.LastIndexOf(".");//获得.的位置
           string ls_New = ls_Old.Substring(0, li_Index);//获得目标字符串
           return ls_New;
        }

        public string GetsysnamebyId(string Id)
        {
            SysPersonView model = _ISysPersonDao.NGetModelById(Id);
            return model.UserName;
        }
        #endregion

        #region //检验测试数据查看打印
        /// <summary>
        /// 检验测试数据查看打印
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult csdataPrintView(string Id)
        {
            IQC_JYDDinfoView model = new IQC_JYDDinfoView();
            model = _IIQC_JYDDinfoDao.NGetModelById(Id);
            return View(model);
        }
        #endregion
        #endregion

        #region //检验标准数据的导出

        //检验标准数据导出
        public FileResult ExcelExportIQC_Sop()
        {
            IList<IQC_SopInfoView> modellist = _IIQC_SopInfoDao.GetAllIQC_Soppagelist();
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("物料编码");
            row1.CreateCell(2).SetCellValue("物料名称");
            row1.CreateCell(3).SetCellValue("物料型号");
            row1.CreateCell(4).SetCellValue("是否检验");//0 没有检验项目   1 有免检 有非免检项目  2 都是免检项目  3 都是非免检项目
            if (modellist != null)
            {
                int n = 0;
                for (int i = 0; i < modellist.Count; i++)
                {
                    n = n + 1;
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(n);
                    rowtemp.CreateCell(0).SetCellValue(n);//序号
                    rowtemp.CreateCell(1).SetCellValue(modellist[i].Yqjdm);//物料编码
                    rowtemp.CreateCell(2).SetCellValue(modellist[i].Yqjname);//物料名称
                    rowtemp.CreateCell(3).SetCellValue(modellist[i].Yqjxh);//物料型号
                    rowtemp.CreateCell(4).SetCellValue(jcmjinfo(modellist[i].Id));//物料型号
                }
            }



            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "物料是否免检数据.xls");
        }


        #region ///查询检验内容
        public int jcmjinfo(string Id)
        {
            try
            {
                IList<IQC_JyconstrinfoView> modellist = _IIQC_JyconstrinfoDao.GetjyconbysopId(Id);
                if (modellist == null)
                    return 0;//没有检验内容

                var m = 0;
                var fm = 0;
                for (int i = 0; i < modellist.Count; i++)
                {
             
                    if (modellist[i].Ismj == 0)
                    {
                        fm = fm + 1;
                    }
                    if (modellist[i].Ismj == 1)
                    {
                        m = m + 1;
                    }
                }
                if (m == 0 && fm > 0) { return 3; }
                else if (m > 0 && fm == 0) { return 1; }
                else { return 2; }
            }
            catch {
                return -1;
            }
        }
        #endregion
        #endregion


    }
}
