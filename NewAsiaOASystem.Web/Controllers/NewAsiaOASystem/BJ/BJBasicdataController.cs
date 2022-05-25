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
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
  
    public class BJBasicdataController : Controller
    {
        IDKX_pjgdbinfoDao _IDKX_pjgdbinfoDao = ContextRegistry.GetContext().GetObject("DKX_pjgdbinfoDao") as IDKX_pjgdbinfoDao;
        IPAY_CONTROL_INFODao _IPAY_CONTROL_INFODao = ContextRegistry.GetContext().GetObject("PAY_CONTROL_INFODao") as IPAY_CONTROL_INFODao;
        //
        // GET: /BJBasicdata/

        public ActionResult Index()
        {
            return View();
        }

        #region //列表
        public ActionResult list()
        {
            return View();
        }
        #endregion

        #region //分页数据
        public ActionResult GetPager_service(int? page, int limit, string ORDER_NO, string CUST_NAME)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPAY_CONTROL_INFODao.SetPagerPageIndex(CurrentPageIndex);
            _IPAY_CONTROL_INFODao.SetPagerPageSize(limit);
            PagerInfo<PAY_CONTROL_INFOView> listmodel = _IPAY_CONTROL_INFODao.GetBJdlistPagerInfo(ORDER_NO, CUST_NAME);
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

        #region //同步报价系统的报价单
        public JsonResult Tbbjorder()
        {
            PAY_CONTROL_INFOView model1 = _IPAY_CONTROL_INFODao.GetMaxCONTROL_INFO_ID("0");
            PAY_CONTROL_INFOView model2 = _IPAY_CONTROL_INFODao.GetMaxCONTROL_INFO_ID("1");
            int ss;
            if (model1 == null)
                ss = 0;
            else
                ss = Convert.ToInt32(model1.CONTROL_INFO_ID);
            int ss2;
            if (model2 == null)
                ss2 = 0;
            else
                ss2 = Convert.ToInt32(model2.CONTROL_INFO_ID);
            string res = BJHelper.Getbjlist(ss.ToString());
            string res2 = BJHelper.GetbjYYlist(ss2.ToString());
            //if(res==""||res==null||res=="[]")
            //    return Json(new { result = "error", msg = "已经是最新的报价单数据了" });
            List<PAY_CONTROL_INFOView> list = JsonConvert.DeserializeObject<List<PAY_CONTROL_INFOView>>(res);
            List<PAY_CONTROL_INFOView> list2 = JsonConvert.DeserializeObject<List<PAY_CONTROL_INFOView>>(res2);
            int? shuliang = 0;
            if (list != null)
            {
                shuliang = shuliang + list.Count();
                foreach (var item in list)
                {
                    PAY_CONTROL_INFOView model = new PAY_CONTROL_INFOView(); 
                    model = item;
                    string bj = Getbjqingdan(item.CONTROL_INFO_NO);
                    model.bjPrice =Convert.ToDecimal(bj);
                    model.bjdtype = "0";
                    _IPAY_CONTROL_INFODao.Ninsert(model);
                }
            }
            if (list2 != null)
            {
                shuliang = shuliang + list2.Count();
                foreach (var item in list2)
                {
                    PAY_CONTROL_INFOView model22 = new PAY_CONTROL_INFOView();
                    model22 = item;
                    string bj = Getbjqingdan(item.CONTROL_INFO_NO);
                    model22.bjPrice = Convert.ToDecimal(bj);
                    model22.bjdtype = "1";
                    _IPAY_CONTROL_INFODao.Ninsert(model22);
                }
            }
            if (shuliang > 0)
            {
                return Json(new { result = "success", msg = "更新成功！一共" + list.Count() + "条。" });
            }
            return Json(new { result = "error", msg = "已经是最新的报价单数据了" });

        }
        #endregion

        #region //查询报价单的价格清单
        public string Getbjqingdan(string code)
        {
            string res = BJHelper.Getbjqingdan(code);
            if (res == "" || res == null)
                return "-1";
            List<BJ_QDModel> list=JsonConvert.DeserializeObject<List<BJ_QDModel>>(res);
            if (list != null)
            {
                if (list[0].DIS_PRICE != "" && list[0].DIS_PRICE != null)
                    return list[0].DIS_PRICE;
                else
                    return list[0].PRICE;
            }
            return "-1";
        }
        #endregion

        #region //重新获取报价单价格
        public JsonResult GetBJqingdanbj(string Id)
        {
            try
            {
                //查询报价单
                PAY_CONTROL_INFOView model = _IPAY_CONTROL_INFODao.NGetModelById(Id);
                //查询报价
                string bj = Getbjqingdan(model.CONTROL_INFO_NO);
                model.bjPrice = Convert.ToDecimal(bj);
                if (_IPAY_CONTROL_INFODao.NUpdate(model))
                    return Json(new { result = "success", msg = "操作成功" });
                else
                    return Json(new { result = "error", msg = "操作失败" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion

  

    }
}
