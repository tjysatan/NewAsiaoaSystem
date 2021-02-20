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
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
    public class AftersaleController : Controller
    {
        //
        // GET: /客户端售后/
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        INQ_THinfoFXDao _INQ_THinfoFXDao = ContextRegistry.GetContext().GetObject("NQ_THinfoFXDao") as INQ_THinfoFXDao;
        INQ_BlxxinfoDao _INQ_BlxxinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlxxinfoDao") as INQ_BlxxinfoDao;
        INQ_BlinfoDao _INQ_BlinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlinfoDao") as INQ_BlinfoDao;
        public ActionResult Index()
        {
            return View();
        }

        #region //客户端售后分析页面
        public ActionResult AftersalesanalysisView(string Id)
        {
            NAReturnListView model = _INAReturnListDao.NGetModelById(Id);
            return View(model);
        }

        //获取分析明细数据
        public string GetNQ_THinfoFXdatajson(string Rid)
        {
            try
            {
                string json = null;
                IList<NQ_THinfoFXView> modellist = _INQ_THinfoFXDao.GetTHFCinfobyR_Id(Rid);
                json = JsonConvert.SerializeObject(modellist);
                return json;
            }
            catch
            {
                return null;
            }
        }

        #region //根据ID 查找不良原因
        [HttpPost]
        public string GetblyymodelbyId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion

        #region //根据ID 查找不良现象
        [HttpPost]
        public string GetblXXmodelbyId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlxxinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion
        #endregion

    }
}
