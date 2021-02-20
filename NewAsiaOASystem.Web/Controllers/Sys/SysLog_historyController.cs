using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using System.IO;
using System.Data;
using System.Text;

namespace NewAsiaOASystem.Web.Controllers
{
    public class SysLog_historyController : Controller
    {
        //
        // GET: /SysLog_history/ 
        ISysLog_historyDao _ISysLog_historyDao = ContextRegistry.GetContext().GetObject("SysLog_historyDao") as ISysLog_historyDao;
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public ActionResult List(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysLog_historyDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysLog_historyDao.SetPagerPageSize(5);
            PagerInfo<SysLog_historyView> listmodel = _ISysLog_historyDao.Search();
            return View(listmodel);
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Sys_loglist()
        {
            string dirPath = System.Web.HttpContext.Current.Server.MapPath("~/logs/");
            if (Directory.Exists(dirPath))
            {
                //获得目录信息
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                //获得目录文件列表
                FileInfo[] files = dir.GetFiles("*.*");
                string[] fileNames = new string[files.Length];
                //临时数据表
                DataTable dt = new DataTable();
                //文件名称
                dt.Columns.Add("logsName");
                //文件大小
                dt.Columns.Add("logsSize");
                //文件路径
                dt.Columns.Add("logsUrl");

                foreach (FileInfo fileInfo in files)
                {
                    DataRow dr = dt.NewRow();
                    dr["logsName"] = fileInfo.Name;
                    dr["logsSize"] = fileInfo.Length / 1024 + "K";
                    dr["logsUrl"] = dir + fileInfo.Name;
                    dt.Rows.Add(dr);
                }

                string Strjson = _ISysLog_historyDao.CreateJsonParameters(dt);
                ViewBag.Strjson = Strjson;
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult Sys_logView()
        {
            string logsName = Request.QueryString["Name"].ToString();
            string dirPath = System.Web.HttpContext.Current.Server.MapPath("~/logs/");
            var path = dirPath + logsName;
            string Content = "";
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(fs, System.Text.Encoding.Default);
                while (reader.Peek() > 0)
                {
                    Content += reader.ReadLine() + "";
                }
            }
            catch (Exception e)
            {

            }
            ViewBag.con = Content;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogTab()
        {
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string id = Request.QueryString["id"].ToString();
            List<SysLog_historyView> SysLog_historylist = _ISysLog_historyDao.NGetList_id(id) as List<SysLog_historyView>;
            if (null != SysLog_historylist)
            {
                if (_ISysLog_historyDao.NDelete(SysLog_historylist))
                {
                    return RedirectToAction("LogTab");
                }
                else
                {
                    return RedirectToAction("LogTab");
                }

            }
            return View();
        }



    }
}
