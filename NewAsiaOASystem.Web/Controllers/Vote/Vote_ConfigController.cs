using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.Dao;
using System.IO;


namespace NewAsiaOASystem.Web.Controllers
{
    public class Vote_ConfigController : Controller
    {
        //
        // GET: /Vote_Config/
        IVote_ConfigDao _IVote_ConfigDao = ContextRegistry.GetContext().GetObject("Vote_ConfigDao") as IVote_ConfigDao;
        public ActionResult Index()
        {
            Vote_ConfigView model = _IVote_ConfigDao.NGetone();
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View(new Vote_ConfigView());
            }
        }

        [HttpPost]
        public JsonResult Edit(Vote_ConfigView model)
        {
            try
            {
                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(model.C_Id))
                {
                    flay = _IVote_ConfigDao.Ninsert(model);
                }
                //修改
                else
                {
                    flay = _IVote_ConfigDao.NUpdate(model);
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

    }
}
