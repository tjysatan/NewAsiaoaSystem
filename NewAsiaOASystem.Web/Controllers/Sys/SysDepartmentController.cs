using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;


namespace NewAsiaOASystem.Web.Controllers
{

    public class SysDepartmentController : Controller
    {
        //
        // GET: /SysDepartment/
        ISysDepartmentDao _ISysDepartmentDao = ContextRegistry.GetContext().GetObject("SysDepartmentDao") as ISysDepartmentDao;

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {  
            string json = _ISysDepartmentDao.GetTreeDepartment();
            ViewBag.result = json;
            return View();
        } 

        /// <summary>
        /// 编辑处理的方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public JsonResult Edit(SysDepartmentView model, FormCollection from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                string ID = Request.Form["ID"];
                //操作是否成功
                bool flay = false;
                if (string.IsNullOrEmpty(ID))
                {
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = user.UserName;
                    flay = _ISysDepartmentDao.Ninsert(model);
                }
                else
                {
                    model.Id = model.Id;
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = user.UserName;
                    flay = _ISysDepartmentDao.NUpdate(model);
                } 

                if (flay)
                {
                    return Json(new { result = "success" }, "text/html");
                }

                else
                {
                    return Json(new { result = "error" }, "text/html");
                }

            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }


        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        //[UserAuthorize(Roles = "admin")]
        public ActionResult Add()
        { 
            return View("Edit",new SysDepartmentView());
        }


      /// <summary>
      /// 修改跳转页面
      /// </summary>
      /// <returns></returns>
        public ActionResult Update()
        {
            ViewBag.action = "EditPage";
            string id = Request.QueryString["id"].ToString();
            SysDepartmentView sys = _ISysDepartmentDao.NGetModelById(id);
            ViewBag.result = sys;
            return View("Edit",sys);
        }
     

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
                List<SysDepartmentView> sys = _ISysDepartmentDao.NGetList_id(id) as List<SysDepartmentView>;
               // PagerInfo<SysDepartmentView> lisemodel =_ISysDepartmentDao.Search();
                if (null != sys)
                {
                    if (_ISysDepartmentDao.NDelete(sys) == true)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return RedirectToAction("List");
                    }
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../SysDepartment/List");
           
        }
    }
}
