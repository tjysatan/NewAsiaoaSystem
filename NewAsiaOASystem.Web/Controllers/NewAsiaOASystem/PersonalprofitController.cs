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
    public class PersonalprofitController : Controller
    {
        //
        // GET: /个人利润/
        IPP_TeaminfoDao _IPP_TeaminfoDao = ContextRegistry.GetContext().GetObject("PP_TeaminfoDao") as IPP_TeaminfoDao;
        IPP_StaffinfoDao _IPP_StaffinfoDao = ContextRegistry.GetContext().GetObject("PP_StaffinfoDao") as IPP_StaffinfoDao;
        IPP_ProfitSummaryinfoDao _IPP_ProfitSummaryinfoDao = ContextRegistry.GetContext().GetObject("PP_ProfitSummaryinfoDao") as IPP_ProfitSummaryinfoDao;


        public ActionResult Index()
        {
            return View();
        }

        //统计页面
        public ActionResult PerproView()
        {
            return View();
        }

        //查询全部员工
        public string Getyuangonginfo()
        {
            string json;
            json = JsonConvert.SerializeObject(_IPP_StaffinfoDao.Getallyuangonginfo());
            return json;
        }

        //查询团队
        public string GettuanduiinfobyId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_IPP_TeaminfoDao.NGetModelById(Id));
            return json;
        }

        //查询当月汇总的数据
        public string AJaxhzdata()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            string hztime = d1.ToString("yyyyMM");
            string json;
            json = JsonConvert.SerializeObject(_IPP_ProfitSummaryinfoDao.GetprofitSuminfobyhztimeM(hztime));
            return json;
        }

    }
}
