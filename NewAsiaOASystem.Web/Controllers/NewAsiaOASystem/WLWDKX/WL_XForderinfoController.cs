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
using System.Runtime.Serialization.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WL_XFOrderinfoController : Controller
    {
        //
        // GET: /WL_XForderinfo/
        IWL_XFOrderinfoDao _IWL_XForderinfoDao = ContextRegistry.GetContext().GetObject("WL_XFOrderinfoDao") as IWL_XFOrderinfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;

        public ActionResult Index()
        {
            return View();
        }
        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<WL_XFOrderinfoView> listmodel = new PagerInfo<WL_XFOrderinfoView>();
            listmodel = GetListPager(pageIndex, null, null, null);
            return View(listmodel);
        }
        #endregion

        #region //查询
        public JsonResult SearchList()
        {
            
            string sid=Request.Form["txt_sid"];
            string xdstartdatetime=Request.Form["txt_startdatetime"];
            string xdenddatetime=Request.Form["txt_enddatetime"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WL_XFOrderinfoView> listmodel = GetListPager(pageIndex,sid,xdstartdatetime,xdenddatetime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取分页数据
        private PagerInfo<WL_XFOrderinfoView> GetListPager(int? pageIndex, string sid, string startXDdatetime, string endXDdatetime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWL_XForderinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWL_XForderinfoDao.SetPagerPageSize(11);
            PagerInfo<WL_XFOrderinfoView> listmodel = _IWL_XForderinfoDao.GetWL_xforderinfoList(sid,startXDdatetime,endXDdatetime,Suser);
            return listmodel;
        }
        #endregion

        #region //同步订单数据

        //同步续费订单页面
        public ActionResult TBXFDATaView()
        {
            return View();
        }

        //同步数据
        public string TBXForderdata()
        {
            WL_XFOrderinfoView model = _IWL_XForderinfoDao.GetNewOderinfo();
            int t = 0;
            if (model != null)
            {
                if (model.Ids != null)
                {
                    t =Convert.ToInt32(model.Ids);
                }
            }
            string url;
           // url = "http://www.sbycjk.net/getsidsofshipment/getOrderByids/" + t;
            url = "http://106.14.14.68:8088/getsidsofshipment/getOrderByids/" + t;
            string result = HttpUtility11.GetData(url);
            List<JsonOrderClass> ordermodel = getObjectByJson<JsonOrderClass>(result);
            foreach (var a in ordermodel)
            {
                WL_XFOrderinfoView xfordermodel = new WL_XFOrderinfoView();
                xfordermodel=_IWL_XForderinfoDao.GetxforderinfobyIds(a.ids);
                if (xfordermodel != null)//存在-更新
                {
                    xfordermodel.Xcxfdatetime = Convert.ToDateTime(a.stop_time);//下次缴费时间
                    xfordermodel.xddatetime = Convert.ToDateTime(a.add_time);//下单时间
                    xfordermodel.price = Convert.ToDecimal(a.price);//订单金额
                    xfordermodel.Ids = Convert.ToInt32(a.ids);//远程Ids
                    xfordermodel.modele_name = a.module_name;//型号名称
                    xfordermodel.monitor_name = a.monitor_name;//站点名称
                    xfordermodel.Sid = a.sid;//sid
                    xfordermodel.Huoqudate = DateTime.Now;//同步时间
                    _IWL_XForderinfoDao.NUpdate(xfordermodel);
                    UpdateWL_Dkdjftime(a.sid, a.stop_time);
                }
                else
                {
                    xfordermodel = new WL_XFOrderinfoView();
                    xfordermodel.Xcxfdatetime = Convert.ToDateTime(a.stop_time);//下次缴费时间
                    xfordermodel.xddatetime = Convert.ToDateTime(a.add_time);//下单时间
                    xfordermodel.price = Convert.ToDecimal(a.price);//订单金额
                    xfordermodel.Ids = Convert.ToInt32(a.ids);//远程Ids
                    xfordermodel.modele_name = a.module_name;//型号名称
                    xfordermodel.monitor_name = a.monitor_name;//站点名称
                    xfordermodel.Sid = a.sid;//sid
                    xfordermodel.Huoqudate = DateTime.Now;//同步时间
                    //同步电商数据
                    int isxytb = Isfl(a.module_name, a.sid);
                    if (isxytb == 0)
                    {
                        xfordermodel.Isfr = 0;//分润
                    }
                    else
                    {
                        xfordermodel.Isfr = 1;//不分
                    }
                    _IWL_XForderinfoDao.Ninsert(xfordermodel);
                    UpdateWL_Dkdjftime(a.sid, a.stop_time);
                
                    if (isxytb == 0)
                    {
                        GetjsxUidbysid(a.sid);
                        //int Uid = GetjsxUidbysid(a.sid);//获取Uid
                        //if (Uid != -1)
                        //{
                        //    decimal jine = 50;
                        //    WL_JxsfrTBdata(Uid, jine);
                        //}
                    }
                }
            }
            if (ordermodel != null)
            {
                return ordermodel.Count().ToString();
            }
            else
            {
                return "0";
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

        //判断是否是特殊箱子第一次续费不返利
        public int Isfl(string xhname, string sid)
        {
            try
            {
                if (xhname == "NA9354S")//需要检查是否第一次续费
                {
                    if (_IWL_XForderinfoDao.GetxforderIsczbysid(sid))//存在需要续费
                    {
                        return 0;//需要续费
                    }
                    else
                    {
                        return 1;//不需要续费
                    }
                }
                return 0;//需要续费
            }
            catch
            {
                return 0;//需要续费
            }
        }

         //返回json 转换实体
        public class JsonOrderClass
        {
            /// <summary>
            /// 下次续费的时间
            /// </summary>
            public virtual string stop_time { get; set; }

            /// <summary>
            /// 订单续费的金额
            /// </summary>
            public virtual string price { get; set; }

            /// <summary>
            /// 远程Id
            /// </summary>
            public virtual string ids { get; set; }

            /// <summary>
            /// 型号名称
            /// </summary>
            public virtual string module_name { get; set; }
            /// <summary>
            /// 下单时间
            /// </summary>
            public virtual string add_time { get; set; }

   

            /// <summary>
            /// 监控点名称
            /// </summary>
            public virtual string monitor_name { get; set; }

         

            /// <summary>
            /// SID
            /// </summary>
            public virtual string sid { get; set; }
        }

        //更新下次缴费时间
        public void UpdateWL_Dkdjftime(string sid, string jfdatetime)
        {
            WL_DkxInfoView model = new WL_DkxInfoView();
            model = _IWL_DkxInfoDao.GetDkxinfobySID(sid);
            if (model != null)
            {
                model.Jf_endtime = Convert.ToDateTime(jfdatetime);
                _IWL_DkxInfoDao.NUpdate(model);
            }
        }

        //通过sid查找电控箱信息同步续费订单信息
        public int GetjsxUidbysid(string sid)
        {
            WL_DkxInfoView dkxmodel = _IWL_DkxInfoDao.GetDkxinfobySID(sid);
            decimal jine = 50;
            if (dkxmodel != null)//电控箱不为空
            {
                if (dkxmodel.chtype == 2)
                {
                    if (dkxmodel.erp_jxscode != "" && dkxmodel.erp_jxscode != null)
                    {
                        WL_JxsfrTBdatabycode(dkxmodel.erp_jxscode, jine);
                    }
                }
                if (dkxmodel.Xs_jxsId != "" && dkxmodel.Xs_jxsId != null)//经销商Id不为空
                {
                    NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(dkxmodel.Xs_jxsId);
                    if (cusmodel != null)//经销商信息不为空
                    {
                        if (cusmodel.DsUid != "" && cusmodel.DsUid != null)//电商uid
                        {
                           
                            int suid= Convert.ToInt32(cusmodel.DsUid);
                            WL_JxsfrTBdata(suid, jine);
                            return suid;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public bool WL_JxsfrTBdata(int Uid, decimal jine)
        {
            Newasia.XYOffer model = new Newasia.XYOffer();
           
                bool str = model.WlwCharge(Uid, jine);
           
            return str;
        }

        #region //通过客户的编码同步续费订单
        public bool WL_JxsfrTBdatabycode(string codeno, decimal jine)
        {
            Newasia.XYOffer model = new Newasia.XYOffer();

            bool str = model.WlwChargeByCode(codeno, jine);

            return str;
        }
        #endregion
        #endregion

        #region MyRegion

        #endregion

        #region //根据经销商UId查找续费订单数量

        public string jxqGetxfordersumbyuid(string jxsId)
        {
            int sum = 0;
            sum = _IWL_XForderinfoDao.jxqGetordersumbyuid(jxsId);
            return sum.ToString();
        }
        #endregion

    } 
}
