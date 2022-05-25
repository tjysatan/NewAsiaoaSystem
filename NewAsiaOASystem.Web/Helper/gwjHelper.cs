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
        public static IDKX_RKZLDataInfoDao _IDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;
        public static string url = "http://erp.sbycjk.net:9501/admin_api/mo/create_by_external"; //正式
        public static string keyAuthorization = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImlzcyI6IkFza2EifQ.eyJpc3MiOiJBc2thIiwiYXVkIjoic29tZWJvZHkiLCJqdGkiOiI2MjMxN2I0ZDAyOTZkIiwiaWF0IjoxNjQ3NDA5OTk3LjAxMDYsIm5iZiI6MTY0NzQwOTk5Ny4wMTA2LCJleHAiOjE2Nzg5NDU5OTcuMDEwNiwidXNlcl9pZCI6MSwicm9sZV9pZCI6MSwidXNlcm5hbWUiOiJhZG1pbiIsInRva2VuX3R5cGUiOiIyIn0.V-64EXUmncfPRgNla3cb6h0midjyMEhGi9NvPhUbkcVhTSS5J_b9_1dbTiNvM-ueHWfMKlRuo5SrM7nsAK-kiQrZ0K1Pub6wQoVK8hXOTThZ0Z6GdmPKRQEEtTIMU6oDsC7tWP1wBKnG9FkHfRVvnv5pDGB2-E5-2ui0hyRtlW06uxE-66W7v252mYR2tbcEZL0fl665HU-Iqk6BUNNGggMPFiL6laHC2IUs2N45WU7ApiUHZwPyqxBUfrXTOmRVLObjlfXQa8zGCTCtcqPdlXC2eMec4MeNVFlCuMos93MsReZQotlmwoyoPsLbPzHxSz8syoFQ6529FuJON4A8eg";
        //public static string url = "http://erp.sbycjk.net:9601/admin_api/mo/create_by_external";
        //public static string url = "http://192.168.10.122:9501/admin_api/mo/create_by_external";//测试

        //插入订单和订单的资料数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orderno"></param>
        /// <param name="ordercustname"></param>
        /// <param name="number"></param>
        /// <param name="bomno"></param>
        /// <param name="fdelivery_date">订单交期</param>
        /// <param name="fproduction_state">订单状态</param>
        /// <returns></returns>
        public static string synchronizationorderandzl(string Id, string orderno,string ordercustname,string number,string bomno,string fdelivery_date,string fproduction_state,string ftemplate_id,string fproduction_line_id)
        {
            try
            {
                moldedata model = new moldedata();
                model.fexternal_mo_bill_no = orderno;
                model.fcustomer = ordercustname;
                model.fqty = number;
                model.fbom_number = bomno;
                model.fdelivery_date = fdelivery_date;
                model.fproduction_state = fproduction_state;
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
                            jxkz.Add(url);
                            mbjx.Add(url);
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
                    headersNvc.Add("Authorization", keyAuthorization);
                    var bodyNvc = new NameValueCollection();
                    bodyNvc.Add("fexternal_mo_bill_no", orderno);
                    bodyNvc.Add("fcustomer", ordercustname);
                    bodyNvc.Add("fqty", number);
                    bodyNvc.Add("fdelivery_date", fdelivery_date);
                    bodyNvc.Add("fproduction_state", fproduction_state);
                    bodyNvc.Add("ftemplate_id", ftemplate_id);
                    bodyNvc.Add("fproduction_line_id ", fproduction_line_id);
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

        #region //常规电控NAWORNAK
        public static string ESOP_CGDKX(string type, string orderno, string ordercustname, string number, string bomno, string fdelivery_date, string fproduction_state, string wlbno,string fproduction_line_id)
        {
            try
            {
                string jsonstr = "";
                string ftemplate_id = "";
                //查询资料
                IList<DKX_RKZLDataInfoView> modellist = _IDKX_RKZLDataInfoDao.GetDKXCPZLdatalist(wlbno);
                List<string> tu2 = new List<string>();
                List<string> tu3 = new List<string>();
                List<string> tu4 = new List<string>();
                List<string> tu5 = new List<string>();
                List<string> tu6 = new List<string>();
                List<string> tu7 = new List<string>();
                List<string> tu8 = new List<string>();
                List<string> tu9 = new List<string>();
                List<string> tu10 = new List<string>();
                List<string> tu11 = new List<string>();
                if (modellist!=null)
                {
                   
                    foreach (var item in modellist)
                    {

                        string url = "http://wx.chinanewasia.com/" + item.wjurl;
                        if (item.Zl_type == 2)
                        {
                            tu2.Add(url);
                        }
                        if (item.Zl_type == 3)
                        {
                            tu3.Add(url);
                        }
                        if (item.Zl_type == 4)
                        {
                            tu4.Add(url);
                        }
                        if (item.Zl_type == 5)
                        {
                            tu5.Add(url);
                        }
                        if (item.Zl_type == 6)
                        {
                            tu6.Add(url);
                        }
                        if (item.Zl_type == 7)
                        {
                            tu7.Add(url);
                        }
                        if (item.Zl_type == 8)
                        {
                            tu8.Add(url);
                        }
                        if (item.Zl_type == 9)
                        {
                            tu9.Add(url);
                        }
                        if (item.Zl_type == 10)
                        {
                            tu10.Add(url);
                        }
                        if (item.Zl_type == 11)
                        {
                            tu11.Add(url);
                        }
                    }
                   
                }
                if (type == "0")
                {//物联网
                    fwork_NAWinstruction NAWmodel = new fwork_NAWinstruction();
                    NAWmodel.底板装配一 = tu2;
                    NAWmodel.底板装配二 = tu3;
                    NAWmodel.接控制线一 = tu4;
                    NAWmodel.接控制线二 = tu5;
                    NAWmodel.接主回路线 = tu6;
                    NAWmodel.上温控线绕管 = tu7;
                    NAWmodel.面板装箱 = tu8;
                    NAWmodel.底板装箱 = tu9;
                    NAWmodel.调试 = tu10;
                    NAWmodel.包装 = tu11;
                    jsonstr = JsonConvert.SerializeObject(NAWmodel);
                    ftemplate_id = "28";
                }
                else
                {
                    fwork_NAKinstruction NAKmodel = new fwork_NAKinstruction();
                    NAKmodel.底板装配一 = tu2;
                    NAKmodel.接控制线一 = tu3;
                    NAKmodel.接主回路线 = tu4;
                    NAKmodel.上温控线绕管 = tu5;
                    NAKmodel.面板装箱 = tu6;
                    NAKmodel.底板装箱 = tu7;
                    NAKmodel.接温控线 = tu8;
                    NAKmodel.焊灯 = tu9;
                    NAKmodel.调试 = tu10;
                    NAKmodel.包装 = tu11;
                    jsonstr = JsonConvert.SerializeObject(NAKmodel);
                    ftemplate_id = "27";
                }
                string res=pulicgwESOP(orderno, ordercustname, number, bomno, fdelivery_date, fproduction_state, ftemplate_id, fproduction_line_id,jsonstr);
                return res;
            }
            catch
            {
                return "1";
            }
        }
        #endregion

        #region //非标转常规订单
        public static string ESOP_fbzhuancg(string ftemplate_id, string Id, string orderno, string ordercustname, string number, string bomno, string fdelivery_date, string fproduction_state,string fproduction_line_id)
        {
            try
            {
                //查询资料
                IList<DKX_ZLDataInfoView> modellist = _IDKX_ZLDataInfoDao.GetAllzldatabyId(Id);
                if (modellist!= null)
                {
                    List<string> tu2 = new List<string>();
                    List<string> tu3 = new List<string>();
                    List<string> tu4 = new List<string>();
                    List<string> tu5 = new List<string>();
                    List<string> tu6 = new List<string>();
                    List<string> tu7 = new List<string>();
                    List<string> tu8 = new List<string>();
                    List<string> tu9 = new List<string>();
                    List<string> tu10 = new List<string>();
                    List<string> tu11 = new List<string>();
                    foreach (var item in modellist)
                    {
                        string url = "http://wx.chinanewasia.com/" + item.url;
                        if (ftemplate_id == "27")//常规NAK
                        {
                            if (item.Zl_type == 2)//箱体图
                            {
                                tu6.Add(url);//面板装箱
                            }
                            else if (item.Zl_type == 6)//电气排布图
                            {
                                tu2.Add(url);
                                tu3.Add(url);
                            }
                            else if (item.Zl_type == 5)
                            {
                                tu4.Add(url);
                                tu5.Add(url);
                                tu7.Add(url);
                                tu8.Add(url);
                                tu9.Add(url);
                                tu10.Add(url);
                                tu11.Add(url);
                            }
                        }
                        if (ftemplate_id == "28")//常规NAW
                        {
                            if (item.Zl_type == 2)//箱体图
                            {
                                tu8.Add(url);//面板装箱
                            }
                            else if (item.Zl_type == 6)//电气排布图
                            {
                                tu2.Add(url);
                                tu3.Add(url);
                            }
                            else if(item.Zl_type == 5)
                            {
                                tu4.Add(url);
                                tu5.Add(url);
                                tu7.Add(url);
                                tu6.Add(url);
                                tu9.Add(url);
                                tu10.Add(url);
                                tu11.Add(url);
                            }
                        }
                    }
                    string jsonstr = "";
                    //string ftemplate_id = "";
                    if (ftemplate_id == "28")
                    {//物联网
                        fwork_NAWinstruction NAWmodel = new fwork_NAWinstruction();
                        NAWmodel.底板装配一 = tu2;
                        NAWmodel.底板装配二 = tu3;
                        NAWmodel.接控制线一 = tu4;
                        NAWmodel.接控制线二 = tu5;
                        NAWmodel.接主回路线 = tu6;
                        NAWmodel.上温控线绕管 = tu7;
                        NAWmodel.面板装箱 = tu8;
                        NAWmodel.底板装箱 = tu9;
                        NAWmodel.调试 = tu10;
                        NAWmodel.包装 = tu11;
                        jsonstr = JsonConvert.SerializeObject(NAWmodel);
                      
                    }
                    else
                    {
                        fwork_NAKinstruction NAKmodel = new fwork_NAKinstruction();
                        NAKmodel.底板装配一 = tu2;
                        NAKmodel.接控制线一 = tu3;
                        NAKmodel.接主回路线 = tu4;
                        NAKmodel.上温控线绕管 = tu5;
                        NAKmodel.面板装箱 = tu6;
                        NAKmodel.底板装箱 = tu7;
                        NAKmodel.接温控线 = tu8;
                        NAKmodel.焊灯 = tu9;
                        NAKmodel.调试 = tu10;
                        NAKmodel.包装 = tu11;
                        jsonstr = JsonConvert.SerializeObject(NAKmodel);
                       
                    }
                    string res = pulicgwESOP(orderno, ordercustname, number, bomno, fdelivery_date, fproduction_state, ftemplate_id, fproduction_line_id,jsonstr);
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
        #endregion

        #region //温控同步工位机
        public static string ESOP_WK(string orderno, string ordercustname, string number, string bomno, string fdelivery_date, string fproduction_state, string wlbno, string fproduction_line_id)
        {
            try
            {
                //查询资料
                IList<DKX_RKZLDataInfoView> modellist= _IDKX_RKZLDataInfoDao.GetDKXCPZLdatalist(wlbno);
                List<string> tu2 = new List<string>();
                List<string> tu3 = new List<string>();
                List<string> tu4 = new List<string>();
                List<string> tu5 = new List<string>();
                List<string> tu6 = new List<string>();
                List<string> tu7 = new List<string>();
                List<string> tu8 = new List<string>();
                //List<string> tu9 = new List<string>();
                List<string> tu10 = new List<string>();
                List<string> tu11 = new List<string>();
                List<string> tu12 = new List<string>();
                List<string> tu13 = new List<string>();
                List<string> tu14 = new List<string>();
                string jsonstr = "";
                string ftemplate_id = "";
                if (modellist != null)
                {

                    foreach (var item in modellist)
                    {
                        string url = "http://wx.chinanewasia.com/" + item.wjurl;
                        if (item.Zl_type == 2)
                        {
                            tu2.Add(url);
                        }
                        if (item.Zl_type == 3)
                        {
                            tu3.Add(url);
                        }
                        if (item.Zl_type == 4)
                        {
                            tu4.Add(url);
                        }
                        if (item.Zl_type == 5)
                        {
                            tu5.Add(url);
                        }
                        if (item.Zl_type == 6)
                        {
                            tu6.Add(url);
                        }
                        if (item.Zl_type == 7)
                        {
                            tu7.Add(url);
                        }
                        if (item.Zl_type == 8)
                        {
                            tu8.Add(url);
                        }
                        //if (item.Zl_type == 9)
                        //{
                        //    tu9.Add(url);
                        //}
                        if (item.Zl_type == 10)
                        {
                            tu10.Add(url);
                        }
                        if (item.Zl_type == 11)
                        {
                            tu11.Add(url);
                        }
                        if (item.Zl_type == 12)
                        {
                            tu12.Add(url);
                        }
                        if (item.Zl_type == 13)
                        {
                            tu13.Add(url);
                        }
                        if (item.Zl_type == 14)
                        {
                            tu14.Add(url);
                        }
                    }
                   
                }
                fwork_WKinstruction wkmodel = new fwork_WKinstruction();
                wkmodel.SMT = tu2;
                wkmodel.插件 = tu3;
                wkmodel.焊接 = tu4;
                wkmodel.洗板 = tu5;
                wkmodel.看板 = tu6;
                wkmodel.烧录 = tu7;
                wkmodel.初检 = tu8;
                wkmodel.老化 = tu10;
                wkmodel.防潮 = tu11;
                wkmodel.装配 = tu12;
                wkmodel.总检 = tu13;
                wkmodel.包装 = tu14;
                jsonstr = JsonConvert.SerializeObject(wkmodel);
                ftemplate_id = "30";
                string res = pulicgwESOP(orderno, ordercustname, number, wlbno, fdelivery_date, fproduction_state, ftemplate_id, fproduction_line_id, jsonstr);
                return res;
                //else
                //{
                //    return "0-1";
                //}
            }
            catch
            {
                return "1";
            }
        }
        #endregion

        #region //插入工位机生成订单和图纸
        public static string pulicgwESOP(string orderno, string ordercustname, string number, string bomno, string fdelivery_date, string fproduction_state, string ftemplate_id,string fproduction_line_id, string jsonstr)
        {
            try
            {

                var headersNvc = new NameValueCollection();
                headersNvc.Add("Authorization", keyAuthorization);
                var bodyNvc = new NameValueCollection();
                bodyNvc.Add("fexternal_mo_bill_no", orderno);
                bodyNvc.Add("fcustomer", ordercustname);
                bodyNvc.Add("fqty", number);
                bodyNvc.Add("fdelivery_date", fdelivery_date);
                bodyNvc.Add("fproduction_state", fproduction_state);
                bodyNvc.Add("ftemplate_id", ftemplate_id);
                bodyNvc.Add("fproduction_line_id", fproduction_line_id);
                bodyNvc.Add("fbom_number", TO_Base_Encode(Encoding.UTF8, bomno));
                bodyNvc.Add("fwork_instruction", jsonstr);
                log4net.LogManager.GetLogger("2021-01" + bodyNvc.ToString());
                string res = CreatePostSysHttpResponse(url, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
                return res;
            }
            catch
            {
                return "1";
            }
        }
        #endregion
         
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
        /// 订单交货日期
        /// </summary>
        public string fdelivery_date { get; set; }

        /// <summary>
        /// 订单当前的状态
        /// </summary>
        public string fproduction_state { get; set; }

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

    #region //常规物联网电控箱

    public class Nawmoldedata
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string fexternal_mo_bill_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>

        public string fcustomer { get; set; }

        /// <summary>
        /// 关联的BOM编号
        /// </summary>
        public string fbom_number { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string fqty { get; set; }

        public fwork_NAWinstruction fwork_instruction { get; set; }
    }

    public class fwork_NAWinstruction { 
       public List<string> 底板装配一 { get; set; }

       public List<string> 底板装配二 { get; set; }

        public List<string> 接控制线一 { get; set; }

        public List<string> 接控制线二 { get; set; }

        public List<string> 接主回路线 { get; set; }

        public List<string> 上温控线绕管 { get; set; }

        public List<string> 面板装箱 { get; set; }

        public List<string> 底板装箱 { get; set; }

        public List<string> 调试 { get; set; }

        public List<string> 包装 { get; set; }
    }
    #endregion

    #region ///常规NAK电控箱
    public class Nakmoldedata
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string fexternal_mo_bill_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>

        public string fcustomer { get; set; }

        /// <summary>
        /// 关联的BOM编号
        /// </summary>
        public string fbom_number { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string fqty { get; set; }

        public fwork_NAKinstruction fwork_instruction { get; set; }
    }


    public class fwork_NAKinstruction
    {
        public List<string> 底板装配一 { get; set; }

        public List<string> 接控制线一 { get; set; }

        public List<string> 接主回路线 { get; set; }

        public List<string> 上温控线绕管 { get; set; }

        public List<string> 面板装箱 { get; set; }

        public List<string> 底板装箱 { get; set; }

        public List<string> 接温控线 { get; set; }

        public List<string> 焊灯 { get; set; }

        public List<string> 调试 { get; set; }

        public List<string> 包装 { get; set; }
    }
    #endregion

    #region //温控
    public class WKmodeldata
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string fexternal_mo_bill_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>

        public string fcustomer { get; set; }

        /// <summary>
        /// 关联的BOM编号
        /// </summary>
        public string fbom_number { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string fqty { get; set; }

        public fwork_WKinstruction fwork_instruction { get; set; }
    }

    public class fwork_WKinstruction
    {
        public List<string> SMT { get; set; }

        public List<string> 插件 { get; set; }

        public List<string> 焊接 { get; set; }

        public List<string> 洗板 { get; set; }

        public List<string> 看板 { get; set; }

        public List<string> 烧录 { get; set; }

        public List<string> 初检 { get; set; }

        public List<string> 老化 { get; set; }

        public List<string> 防潮 { get; set; }

        public List<string> 装配 { get; set; }

        public List<string> 总检 { get; set; }

        public List<string> 包装 { get; set; }


    }
    #endregion






}