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

namespace NewAsiaOASystem.Web.Controllers.NewAsiaOASystem
{
    public class NQ_CHdetailinfoController : Controller
    {
        //
        // GET: /NQ_CHdetailinfo/
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        INQ_CHdetailinfoDao _INQ_CHdetailinfoDao = ContextRegistry.GetContext().GetObject("NQ_CHdetailinfoDao") as INQ_CHdetailinfoDao;
        INQ_ThdetailinfoDao _INQ_ThdetailinfoDao = ContextRegistry.GetContext().GetObject("NQ_ThdetailinfoDao") as INQ_ThdetailinfoDao;
        INQ_THinfoFXDao _INQ_THinfoFXDao = ContextRegistry.GetContext().GetObject("NQ_THinfoFXDao") as INQ_THinfoFXDao;
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="Id"></param>
        /// <param name="type">0 是退货 1 是出货 2 分析 3销售</param>
        /// <returns></returns>
        public ActionResult list(int? pageIndex,string Id,string type)
        {
            if (Session[SessionHelper.SSerch] != null)
            {
                FTcanshumodel ssearch = Session[SessionHelper.SSerch] as FTcanshumodel;
                if (Id != null || Id != "")
                {
                    Session[SessionHelper.SSerch] = null;
                    Session.Remove(SessionHelper.SSerch);
                    FTcanshumodel model = new FTcanshumodel();
                    model.ft_Id = Id;
                    model.type = type;
                    Session[SessionHelper.SSerch] = model;
                    ViewData["Id"] = Id;
                    ViewData["type"] = type;
                }
                else
                {
                    ViewData["Id"] = ssearch.ft_Id;
                    ViewData["type"] = ssearch.type;
                }
            }
            else
            {
                Session[SessionHelper.SSerch] = null;
                Session.Remove(SessionHelper.SSerch);
                FTcanshumodel model = new FTcanshumodel();
                model.ft_Id = Id;
                model.type = type;
                Session[SessionHelper.SSerch] = model;
                ViewData["Id"] = Id;
                ViewData["type"] = type;
            }

            PagerInfo<NQ_productinfoView> listmodel = GetListPager(pageIndex,null,null,null);
            return View(listmodel);
        }

        #region // 按照条件查询
        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string P_xh = Request.Form["txtSearch_P_xh"];
            string bianhao = Request.Form["txtSearch_bianhao"];
            string ft_ID = Request.Form["ft_Id"];
            string thtype = Request.Form["thtype"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_productinfoView> listmodel = GetListPager(pageIndex, Name, P_xh, bianhao);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate});
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //返退货单Id,编辑类型 实体Session
        public class FTcanshumodel
        {
            public virtual string ft_Id { get; set; }
            public virtual string type { get; set; }
        }
        #endregion

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NQ_productinfoView> GetListPager(int? pageIndex, string Name, string P_xh, string bianhao)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_productinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_productinfoDao.SetPagerPageSize(6);
            PagerInfo<NQ_productinfoView> listmodel = _INQ_productinfoDao.GetCinfoList(Name,P_xh,bianhao,Suser);
            return listmodel;
        }
        #endregion

       #region //出货产品添加
       [HttpPost]
       public JsonResult Edit(string cpID,string SL,string R_Id,string bz)
        {
            try
            {
                bool flay = false;
                NQ_CHdetailinfoView model = new NQ_CHdetailinfoView();
                model.P_Id = cpID;//出货产品的Id
                model.shuliang = Convert.ToInt32(SL);//出货数量
                model.R_Id = R_Id;//返退流程ID
                model.Beizhu = bz;
                flay = _INQ_CHdetailinfoDao.Ninsert(model);
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

       #region //仓库返退货 明细保存|同时保存到客退品分析表中
       [HttpPost]
       public JsonResult ftEdit(string cpID, string SL, string R_Id,string beizhu,string Baddescribe)
       {
           try
           {
               bool flay = false;
               NQ_ThdetailinfoView model = new NQ_ThdetailinfoView();
               model.P_Id = cpID;//产品ID
               model.SL = Convert.ToInt32(SL);//返退货数量
               model.R_Id = R_Id;//返退流程Id
               model.C_time = DateTime.Now;//创建时间
               model.Beizhu = beizhu;
               model.Cust_Baddescribe = Baddescribe;//产品不良现象客户描述
                NQ_productinfoView cpmodel = _INQ_productinfoDao.NGetModelById(cpID);
               for (int i = 0; i <  Convert.ToInt32(SL); i++)
               {
                   NQ_THinfoFXView thmodel = new NQ_THinfoFXView();
                   thmodel.P_Id = cpID;//
                    if (cpmodel != null)
                    {
                        thmodel.Cpname = cpmodel.Pname;
                        thmodel.Cpmode = cpmodel.P_xh;
                        thmodel.Cpwlno = cpmodel.P_bianhao;
                    }
                   thmodel.R_Id = R_Id;//
                   thmodel.Sort = i + 1;//排序
                   thmodel.C_time = DateTime.Now;//创建时间
                   thmodel.TH_RGF = 0;//包材费
                   thmodel.TH_yqjjg = 0;//元器件价格
                   thmodel.TH_BCF = 0;//人工费
                   thmodel.TH_YF = 0;//
                   thmodel.TH_XJ = 0;//
                   thmodel.Isdz = 0;//未定责
                   thmodel.Cust_Baddescribe = Baddescribe;//产品不良现象客户描述
                    _INQ_THinfoFXDao.Ninsert(thmodel);
               }
               flay = _INQ_ThdetailinfoDao.Ninsert(model);
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

       #region //根据返退货流程ID 查找出货产品明细 json
     
       public string GetchinfobyRId(string R_Id)
       {
           string json;
           json = JsonConvert.SerializeObject(_INQ_CHdetailinfoDao.Getchinfoby_rid(R_Id));
           return json;
       } 
       #endregion

       #region //根据产品ID 查找出产品的详情信息
       [HttpPost]
       public string GetPreinfobyId(string ID)
       {
           string json;
           json = JsonConvert.SerializeObject(_INQ_productinfoDao.NGetModelById(ID));
           return json;
       } 
       #endregion

       #region //删除 出货单中产品
       [HttpPost]
       public JsonResult Delete(string Id)
       {
           NQ_CHdetailinfoView model = new NQ_CHdetailinfoView();
           if (!string.IsNullOrEmpty(Id))
               model = _INQ_CHdetailinfoDao.NGetModelById(Id);
           bool flay;
           flay = _INQ_CHdetailinfoDao.NDelete(model);
           if (flay)
               return Json(new { result = "success"});
           else
               return Json(new { result = "error" }); 
          
       } 
       #endregion
        //仓库 登记退货单
       #region //根据返退货流程ID 
       [HttpPost]
       public string GetthdetailinfobyIdjson(string R_Id)
       {
           string json;
           json = JsonConvert.SerializeObject(_INQ_ThdetailinfoDao.Gettfinfoby_rid(R_Id));
           return json;
       } 
       #endregion

       #region //删除退货明细
       [HttpPost]
       public JsonResult THDelete(string Id)
       {
           NQ_ThdetailinfoView model = new NQ_ThdetailinfoView();
           if (!string.IsNullOrEmpty(Id))
               model = _INQ_ThdetailinfoDao.NGetModelById(Id);//查询
           IList<NQ_THinfoFXView> listmodel = _INQ_THinfoFXDao.GetTHFXinfobyR_IdandCpId(model.R_Id,model.P_Id);
           //删除维修分写记录
           DeleteTHinfowxfx(listmodel);
           bool flay;
           flay = _INQ_ThdetailinfoDao.NDelete(model);
           if (flay)
               return Json(new { result = "success" });
           else
               return Json(new { result = "error" });
       }

        /// <summary>
        /// 删除要删除的维修明细
        /// </summary>
        /// <param name="listmodel"></param>
       public void DeleteTHinfowxfx(IList<NQ_THinfoFXView> listmodel)
       {
           if (listmodel != null)
           {
               List<NQ_THinfoFXView> sys = listmodel as List<NQ_THinfoFXView>;
               _INQ_THinfoFXDao.NDelete(sys);
           }
       }
       #endregion

       #region //产品维修分析打印
        //打印页面
         public ActionResult PrintFXView(string Id)
       {
           NAReturnListView model = _INAReturnListDao.NGetModelById(Id);
           //客户名称
           NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(model.C_Id);//查询客户信息
           string Cus_name =Cusmodel.Name;//客户名称
           ViewData["Cusname"] = Cus_name;
           string FTdate = Convert.ToDateTime(model.R_FTdate).ToString("yyyy-MM-dd");//返退时间
           ViewData["FTdate"] = FTdate;
           //维修人员
           SysPersonView sysPmodel=_ISysPersonDao.NGetModelById(model.R_CJFZR);
           string WXname = sysPmodel.UserName;//维修负责人
           ViewData["WXname"] = WXname;//维修人
           string wxdate = Convert.ToDateTime(model.R_cjcldate).ToString("yyyy-MM-dd");//维修时间
           ViewData["wxdate"] = wxdate;//维修时间
           ViewData["RId"] = Id;//返退单的Id
           return View();
       }

        //查找维修明细
         [HttpPost]
         public string AjaxWxmxjson(string RId)
         {
             string json;
             json = JsonConvert.SerializeObject(_INQ_THinfoFXDao.GetTHFCinfobyR_Id(RId));
              return json;
         }

        #endregion

       #region //出货单产品明细同步


         public string CHdetainfotb(string r_Id)
         {
             try
             {
                 for (int i = 0, j = 5; i < j; i++)
                 {
                     if(i==0)
                     {
                         CHdinfobc(r_Id,"报废");
                     }
                     if (i == 1)
                     {
                         CHdinfobc(r_Id, "补新");
                     }
                     if (i == 2)
                     {
                         CHdinfobc(r_Id, "正常");
                     }
                     if (i == 3)
                     {
                         CHdinfobc(r_Id, "折价补新");
                     }
                     if (i == 4)
                     {
                         CHdinfobc(r_Id, "修复");
                     }
                 }
                 return "1";
             }
             catch
             {
                 return "2";
             }
         }

        /// <summary>
        /// 根据返退货单Id
        /// </summary>
        /// <param name="clfs"></param>
        /// <param name="r_Id"></param>
        /// <returns></returns>
         public IList<NQ_THinfoFXView> thinfofxbyrIdclfs(string clfs, string r_Id)
         {
             IList<NQ_THinfoFXView> listmodel = _INQ_THinfoFXDao.GetNq_thinfofxbythbz(clfs,r_Id);
             if (listmodel != null)
             {
                 return listmodel;
             }
             else
             {
                 return null;
             }
         }

         public void CHdinfobc(string r_Id, string clfs)
         {
             IList<NQ_THinfoFXView> bxlistmodel = thinfofxbyrIdclfs(clfs, r_Id);
             if (bxlistmodel != null)
             {
                 for (int a = 0, z = bxlistmodel.Count(); a < z; a++)
                 {
                     if (_INQ_CHdetailinfoDao.JxCHcpbyridandcpidandclfs(r_Id, bxlistmodel[a].P_Id, clfs))//不存在
                     {
                         NQ_CHdetailinfoView model = new NQ_CHdetailinfoView();
                         model.R_Id = bxlistmodel[a].R_Id;//返退货Id
                         model.P_Id = bxlistmodel[a].P_Id;//产品Id
                         model.shuliang =1;//产品数量加1
                         model.clfs = bxlistmodel[a].TH_bz;//处理方式
                         _INQ_CHdetailinfoDao.Ninsert(model);
                     }
                     else//存在
                     {
                         NQ_CHdetailinfoView model = new NQ_CHdetailinfoView();
                         model = _INQ_CHdetailinfoDao.GetCHdetailinfobyr_IdcpIdclfs(r_Id, bxlistmodel[a].P_Id, clfs);
                         if (model != null)
                         {
                             model.shuliang = model.shuliang + 1;
                             _INQ_CHdetailinfoDao.NUpdate(model);
                         }
                     }
                 }
             }
         }

        #endregion

    }
}
