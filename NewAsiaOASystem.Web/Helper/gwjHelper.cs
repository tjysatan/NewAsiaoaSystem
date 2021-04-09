using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Spring.Context.Support;
using System.Collections.Specialized;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace NewAsiaOASystem.Web
{


    //产生工位机平台的数据对接
    public class gwjHelper
    {

        public static IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        public static string url = "https://erp.sbycjk.net/admin_api/mo/create_by_external";
       // public static string url = "http://192.168.10.217:9501/admin_api/mo/create_by_external";

        //插入订单和订单的资料数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orderno"></param>
        /// <param name="ordercustname"></param>
        /// <param name="number"></param>
        /// <param name="bomno"></param>
        /// <returns></returns>
        public static string synchronizationorderandzl(string Id, string orderno,string ordercustname,string number,string bomno)
        {
            
            try
            {
                moldedata model = new moldedata();
                model.fexternal_mo_bill_no = orderno;
                model.fcustomer = ordercustname;
                model.fqty = number;
                model.fbom_number = bomno;
                //查询资料
                IList<DKX_ZLDataInfoView> modellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(Id);
                if (modellist.Count>0)
                {
                    fwork_instruction lmodel = new fwork_instruction();
                    List<string> zx = new List<string>();
                    List<string> db = new List<string>();
                    List<string> jcx = new List<string>();
                    List<string> jxkz = new List<string>();
                    List<string> mbjx = new List<string>();
                    List<string> ds = new List<string>();
                    foreach (var item in modellist)
                    {

                        string url = "http://wx.chinanewasia.com/" + item.url;
                       // string url =  item.url; 
                        if (item.Zl_type == 2)//箱体图
                        {
                           
                            //lmodel.装箱.Add(url);
                            zx.Add(url);
                            //lmodel.装箱 = zx;
                        }
                        if (item.Zl_type == 6)//电气排布图
                        {
                          
                            db.Add(url);
                            //lmodel.底板装配.Add(url);
                        }
                        if (item.Zl_type == 5)//电气原理图
                        {
                            jcx.Add(url);
                            ds.Add(url);
                            //lmodel.接粗线.Add(url);
                            //lmodel.调试.Add(url);
                        }
                        if (item.Zl_type == 10)//线号表
                        {
                            db.Add(url);
                            jxkz.Add(url);
                            mbjx.Add(url);
                            //lmodel.底板装配.Add(url);
                            //lmodel.接控制线.Add(url);
                            //lmodel.面板接线.Add(url);
                        }
                        if (item.Zl_type == 0)//需求
                        {
                            ds.Add(url);
                            //lmodel.调试.Add(url);
                        }
                    }
                    lmodel.底板装配 = db;
                    lmodel.接控制线 = jxkz;
                    lmodel.接粗线 = jcx;
                    lmodel.装箱 = zx;
                    lmodel.面板接线 = mbjx;
                    lmodel.调试 = ds;
                    model.fwork_instruction = lmodel;
                    string jsonstr = JsonConvert.SerializeObject(lmodel);
                    var headersNvc = new NameValueCollection();
                    headersNvc.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImlzcyI6IkFza2EifQ.eyJpc3MiOiJBc2thIiwiYXVkIjoic29tZWJvZHkiLCJqdGkiOiI2MDRlZDNlYjc3ZjA0IiwiaWF0IjoiMTYxNTc3ODc5NS40OTEwNTUiLCJuYmYiOiIxNjE1Nzc4Nzk1LjQ5MTA1NSIsImV4cCI6IjE2NDczMTQ3OTUuNDkxMDU1IiwidXNlcl9pZCI6MSwicm9sZV9pZCI6MSwidXNlcm5hbWUiOiJhZG1pbiIsInRva2VuX3R5cGUiOiIyIn0.hNXGqVPNFA0jjcigg7gtRu0e0UyDAKxKTgo1-4jXgx8575e4ZfB4G9GFnb9H6XtCuIhN28VjN7aydYHXCFEdi6Y0k3f380qmsiL2kkgmcdo8UBTe-lxqyx2AsLz0OvzOa9MBlT8xnJgYZpiyCVdDP6PJ9SBiQCvHavPUEBHuEVk6lpGmD2gRhgP1GldJGSXtM2f-GSL8ERwO41VcfkDQAoRpqh3v7517PBcr_C5f7ch69O7CXyDRWrbvz-3fe14b40IfARAG0Tcnf3_2RqppiBmmXbi4oXx_rj0UTODTlKePY5sCXaS41ky_jw4fvNRKJlblucZf4XcXr7VzITu0WA");
                    var bodyNvc = new NameValueCollection();
                    bodyNvc.Add("fexternal_mo_bill_no", orderno);
                    bodyNvc.Add("fcustomer", ordercustname);
                    bodyNvc.Add("fqty", number);
                    bodyNvc.Add("fbom_number", TO_Base_Encode(Encoding.UTF8,bomno));
                    bodyNvc.Add("fwork_instruction", jsonstr);
                    log4net.LogManager.GetLogger("2021-01"+ bodyNvc.ToString());
                    string res = CreatePostSysHttpResponse(url, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
                    return res;

                } 
                else
                {
                    return "0-1";
                }
            }
            catch
            {
                return "1";
            }
        }

        #region 创建POST方式的HTTP请求
        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="Hederparameters"></param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <param name="contentType">请求的发送编码方式</param>
        /// <param name="method">请求的方式GET、POST</param>
        /// <returns></returns>
        public static string CreatePostSysHttpResponse(string url, NameValueCollection Hederparameters, NameValueCollection parameters, int? timeout, Encoding requestEncoding, CookieCollection cookies, string contentType = "application/x-www-form-urlencoded", string method = "POST")
        {
            try
            {
                string returns = "";
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException("url");
                }
                if (requestEncoding == null)
                {
                    throw new ArgumentNullException("requestEncoding");
                }
                HttpWebRequest request = null;
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = method;
                request.ContentType = contentType;
                if (Hederparameters != null)
                {
                    request.Headers.Add(Hederparameters);
                }
                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value;
                }
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                //如果需要POST数据  
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, HttpUtility.UrlEncode(parameters[key], Encoding.UTF8));
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, HttpUtility.UrlEncode(parameters[key], Encoding.UTF8));
                        }
                        i++;
                    }
                    byte[] data = requestEncoding.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                //if (!(parameters == null || parameters == ""))
                //{
                //    byte[] data = requestEncoding.GetBytes(parameters);
                //    using (Stream stream = request.GetRequestStream())
                //    {
                //        stream.Write(data, 0, data.Length);
                //    }
                //}
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, requestEncoding);
                string ret = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                returns = ret;
                return returns;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }


        #endregion

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string TO_Base_Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
    }

    public class moldedata
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public  string fexternal_mo_bill_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>

        public  string fcustomer { get; set; }

        /// <summary>
        /// 关联的BOM编号
        /// </summary>
        public string fbom_number { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string fqty { get; set; }

        public fwork_instruction fwork_instruction { get; set; }
    }

    public class fwork_instruction {
        public List<string> 底板装配 { get; set; }

        public List<string> 接控制线 { get; set; }

        public List<string> 接粗线 { get; set; }

        public List<string> 装箱 { get; set; }

        public List<string > 面板接线 { get; set; }

        public List<string> 接温控线 { get; set; }

        public List<string> 调试 { get; set; }
    }



 
 
 
}