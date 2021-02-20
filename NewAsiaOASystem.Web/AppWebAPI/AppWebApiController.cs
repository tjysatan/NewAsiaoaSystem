using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Text;

namespace NewAsiaOASystem.Web.WebAPI
{
    public class AppWebApiController : ApiController
    {
        //IAppSysPersonDao _IAppSysPersonDao = ContextRegistry.GetContext().GetObject("AppPersonDao") as IAppSysPersonDao;

        #region 用户管理
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UserLogin(UserLogin LoginInfo)
        {
            IAppSysPersonDao _IAppSysPersonDao = ContextRegistry.GetContext().GetObject("AppPersonDao") as IAppSysPersonDao;
            return _IAppSysPersonDao.login(LoginInfo.username, LoginInfo.userpass);

        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UserUpdatePassword(UserUpdatePassword LoginInfo)
        {
            IAppSysPersonDao _IAppSysPersonDao = ContextRegistry.GetContext().GetObject("AppPersonDao") as IAppSysPersonDao;
            return _IAppSysPersonDao.UpdatePasswod(LoginInfo.UserName, LoginInfo.OldPassword, LoginInfo.NewPassword);

        }

        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="view">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public string UpdatePersonInfo(SysPersonView view)
        {
            IAppSysPersonDao _IAppSysPersonDao = ContextRegistry.GetContext().GetObject("AppPersonDao") as IAppSysPersonDao;
            return _IAppSysPersonDao.UpdatePersonInfo(view);
        }

        /// <summary>
        /// 头像上传
        /// </summary>
        /// <returns></returns>

        public Task<HttpResponseMessage> UploadHeadPortraitOne()
        {
            try
            {
               
                // 检查是否是 multipart/form-data 
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    var taskErr = Request.Content.ReadAsStringAsync().ContinueWith<HttpResponseMessage>(t =>
                    {
                        return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"请检查enctype设置是否正确\",\"url\":\"\"}");
                    });
                    return taskErr;
                }
                    //throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                //文件保存目录路径 
                string SaveTempPath = "~/Content/Iot/HeadPortrait/";
                String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
                // 设置上传目录 
                var provider = new MultipartFormDataStreamProvider(dirTempPath);
                var task = Request.Content.ReadAsMultipartAsync(provider).
                    ContinueWith<HttpResponseMessage>(o =>
                    {
                        StringBuilder sb = new StringBuilder();
                        string LoginId = provider.FormData["LoginId"];
                        if (string.IsNullOrEmpty(LoginId))
                            return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"请输入用户ID\",\"url\":\"\"}");
                        if (provider.FileData.Count <= 0)
                            return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"请选择上传的头像\",\"url\":\"\"}");

                        var file = provider.FileData[0];
                        string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                        FileInfo fileinfo = new FileInfo(file.LocalFileName);

                        //最大文件大小 
                        int maxSize = 900000000;
                        if (fileinfo.Length <= 0)
                            return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"请选择上传的头像\",\"url\":\"\"}");
                        else if (fileinfo.Length > maxSize)
                            return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"上传头像大小超过限制\",\"url\":\"\"}");
                        else
                        {
                            string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                            //定义允许上传的文件扩展名 
                            String fileTypes = "gif,jpg,jpeg,png,bmp";
                            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                                return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"头像格式不正确\"}");
                            else
                            {
                                string FileName = Guid.NewGuid() + fileExt;//生产文件名
                                string SaveUrl = "/Content/Iot/HeadPortrait/" + FileName;//设置文件保存在数据库路径
                                fileinfo.CopyTo(Path.Combine(dirTempPath, FileName), true);//设置文件保存在本地路径
                                fileinfo.Delete();
                                IAppSysPersonDao _IAppSysPersonDao = ContextRegistry.GetContext().GetObject("AppPersonDao") as IAppSysPersonDao;
                                bool flay = _IAppSysPersonDao.UploadHeadPortrait(LoginId, SaveUrl);
                                if (flay)
                                    return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"true\",\"message\":\"上传成功\",\"url\":\"" + SaveUrl + "\"}");
                                else
                                    return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"上传失败\",\"url\":\"\"}");
                            }
                        }
                    });
                return task;
            }

            catch
            {
                var taskErr = Request.Content.ReadAsStringAsync().ContinueWith<HttpResponseMessage>(t =>
                {
                    return Request.CreateResponse<string>(HttpStatusCode.OK, "{\"status\":\"false\",\"message\":\"服务器异常请重试\",\"url\":\"\"}");
                });
                return taskErr;
                //throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
        }

        #endregion

        /// <summary>
        /// App下载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string AppUpload(string version)
        {
            IAppUpdateDao _IAppUpdateDao = ContextRegistry.GetContext().GetObject("AppUpdateDao") as IAppUpdateDao;
            return _IAppUpdateDao.AppUpload(version);

        }

        /// <summary>
        /// GIS坐标上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GisUpload(Point_infoView point)
        {
            IAppPoint_infoDao _IAppPoint_infoDao = ContextRegistry.GetContext().GetObject("AppPoint_infoDao") as IAppPoint_infoDao;
            return _IAppPoint_infoDao.GisUpload(point);

        }

    }
}
