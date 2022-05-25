using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    /// <summary>
    /// 顺丰快递接口
    /// </summary>
    public class SFEpressinterHelper
    {
       // private const String host = "https://sfapi-sbox.sf-express.com/std/service";//测试地址（）
        private const String host = "https://sfapi.sf-express.com/std/service";//正式地址(下单地址)
       // private const String Tokenhost = "https://sfapi-sbox.sf-express.com/oauth2/accessToken";//测试地址
         private const String Tokenhost = "https://sfapi.sf-express.com/oauth2/accessToken";//正式地址


        private const String APPKEY = "uCiA59RpIUxgMehlJ3GTqLUUqoqsqEZX";
        private const String CusKey = "XYKJ1s";

        public static INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        public static ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        public static INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;


        public static string testMain(EP_jlinfoView dyjlmodel)
        {
            //获取Get_accessToken
            string Tokenjson = Get_accessToken();
            if (Tokenjson == null)
                return "-1";//Token获取异常
            ReqPushOrderJdpInfoReq ordermodel = new ReqPushOrderJdpInfoReq();
            ordermodel.language = "zh_CN";
            ordermodel.orderId= dyjlmodel.Id;
            ordermodel.monthlyCard = "5124038993";
            ordermodel.payMethod = dyjlmodel.payType;
            ordermodel.expressTypeId = dyjlmodel.deliveryType;
            List<CargoDetail> alist = new List<CargoDetail>();
            CargoDetail amodel = new CargoDetail();
            amodel.name= dyjlmodel.cargoName;
            amodel.count = "1";
            amodel.unit = "个";
            alist.Add(amodel);
            ordermodel.cargoDetails = alist;
            List<sender> blist = new List<sender>();
            sender bmodel = new sender();
            SysPersonView permodel = _ISysPersonDao.NGetModelById(dyjlmodel.JjId);
            //寄件方
            bmodel.province = "江苏省";
            bmodel.city = "苏州市";
            bmodel.county = "太仓市";
            bmodel.address = "花墙工业区新亚路2号";
            bmodel.contactType = "1";
            bmodel.contact = permodel.UserName;
            bmodel.tel= permodel.Tel;
            bmodel.country = "CN";
            bmodel.company = "苏州新亚";
            blist.Add(bmodel);
            bmodel = new sender();
            //收方
            bmodel.contactType = "2";
            bmodel.country = "CN";
            if (dyjlmodel.SjaddId != "" && dyjlmodel.SjaddId != null)
            {
                NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);//获取公司数据
                NA_AddresseeInfoView SJRmodel = _INA_AddresseeInfoDao.NGetModelById(dyjlmodel.SjaddId);
                bmodel.contact = SJRmodel.Aname;//收件人
                bmodel.tel = SJRmodel.Tel;//收件联系方式
                bmodel.province = SJRmodel.qyo;//省
                bmodel.city = SJRmodel.qye;//市
                bmodel.county = SJRmodel.qyt;//区县
                bmodel.address = SJRmodel.Address;//具体地址
                bmodel.company = receivemodel.Name;
            }
            else
            {
                NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);
                bmodel.contact = receivemodel.LxrName;//收件人
                bmodel.tel = receivemodel.Tel;//收件联系方式
                bmodel.province = receivemodel.qyname;//省
                bmodel.city = receivemodel.qycname;//市
                bmodel.county = receivemodel.qyqxname;//区县
                bmodel.address = receivemodel.Adress;//具体地址
                bmodel.company = receivemodel.Name;
            }
            blist.Add(bmodel);
            ordermodel.contactInfoList = blist;
            string timestamp = ConvertDateTimeToInt(DateTime.Now).ToString();
            string data = "";
            data = JsonConvert.SerializeObject(ordermodel);
           
            var bodyNvc = new NameValueCollection();
            bodyNvc.Add("serviceCode", "EXP_RECE_CREATE_ORDER");
            bodyNvc.Add("partnerID", CusKey);
            bodyNvc.Add("requestID", dyjlmodel.Id);
            bodyNvc.Add("timestamp", timestamp);
            bodyNvc.Add("accessToken", Tokenjson);
            bodyNvc.Add("msgData", data);
            string res = CreatePostSysHttpResponse(host, null, bodyNvc, 5000, Encoding.UTF8, null);
            return res;
        }

        private static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }


        public class ReqPushOrderJdpInfoReq
        {
           public string language { get; set; }

           public string orderId { get; set; }

          public string payMethod { get; set; }

          public string expressTypeId { get; set; }

          public string monthlyCard { get; set; }

           public List<CargoDetail> cargoDetails { get; set; }

           public List<sender> contactInfoList { get; set; }
        }

        /// <summary>
        /// 收/发货人
        /// </summary>
        public class sender
        {
            /// <summary>
            /// 地址 N
            /// </summary>
            public string address { get; set; }

            /// <summary>
            /// 公司名称
            /// </summary>
            public string company { get; set; }

            /// <summary>
            /// 联系人 N
            /// </summary>
            public string contact { get; set; }

            /// <summary>
            /// 地址类型：1，寄件方信息 2，到件方信息
            /// </summary>
            public string contactType { get; set; }

            /// <summary>
            /// 国家或地区 2位代码 参照附录国家代码附件
            /// </summary>
            public string country { get; set; }

            /// <summary>
            /// 邮编，跨境件必填（中国内地，港澳台互寄除外）
            /// </summary>
            public string postCode { get; set; }

            /// <summary>
            /// 联系电话
            /// </summary>
            public string tel { get; set; }

            /// <summary>
            /// 省份
            /// </summary>
            public string province { get; set; }

            /// <summary>
            /// 发货人城市 y
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 发货人区县
            /// </summary>
            public string county { get; set; }

          
        }

        public class CargoDetail
        {
            /// <summary>
            /// 货物名称y
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 总件数（包裹数） y
            /// </summary>
            public string count { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public string unit { get; set; }

            /// <summary>
            /// 订单货物单位重量，包含子母件，单位千克，精确到小数点后3位跨境件报关需要填写
            /// </summary>
            public string weight { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public string amount { get; set; }

            /// <summary>
            /// 货物单价的币别：参照附录币别代码附件
            /// </summary>
            public string currency { get; set; }

            /// <summary>
            /// 原产地国别， 跨境件报关需要填写
            /// </summary>
            public string sourceArea { get; set; }

        }

        /// <summary>
        /// 开放平台签名[MD5 + BASE64]
        /// </summary>
        /// <param name="encryptStr">加密的字符串(请求数据[data] + 消息密钥[key])</param>
        /// <param name="charset">编码方式, 默认UTF-8</param>
        /// <returns></returns>
        public static string EncryptMD5Base64(string data, string charset = "UTF-8")
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            
            byte[] dataHash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in dataHash)
            {
                sb.Append(b.ToString("X2").ToLower());
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
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
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号4" + url);
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
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, requestEncoding);
                string ret = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                returns = ret;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号777" + returns);
                return returns;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号5" + e);

                return null;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        #endregion

        public static string UrlDecode(string str)
        {
            char[] cr = str.ToCharArray();

            byte[] byStr = new byte[cr.Length / 2];

            int j = 0;
            for (int i = 0; i < byStr.Length; i++)
            {
                byStr[i] = byte.Parse(cr[j] + "" + cr[j + 1], System.Globalization.NumberStyles.HexNumber);
                j = j + 2;
            }
            str = System.Text.Encoding.UTF8.GetString(byStr);

            return (str);
        }

        #region //获取accessToken 的接口
        public static string Get_accessToken()
        {
            string lurl;
            lurl = Tokenhost + "?partnerID=" + CusKey + "&secret=" + APPKEY+ "&grantType=password";
            string resjson = CreatePostSysHttpResponse(lurl, null, null, 5000, Encoding.UTF8, null);
            //List<Tokenmodel> timemodel = getObjectByJson<Tokenmodel>(resjson);
            Tokenmodel timemodel = JsonConvert.DeserializeObject<Tokenmodel>(resjson);
            if (timemodel.apiResultCode == "A1000")
                return timemodel.accessToken;
            else
                return null;
        
        }

        #region //Token返回实体
        public class Tokenmodel
        {
            public virtual string apiResultCode { get; set; }//A1000 获取成功

            public virtual string apiErrorMsg { get; set; } //描述

            public virtual string apiResponseID { get; set; }//

            public virtual string accessToken { get; set; }//Token

            public virtual string expiresIn { get; set; }
        }
        #endregion

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
        #endregion

        #region //取消订单
        public static string revokeMain(EP_jlinfoView dyjlmodel)
        {
            //获取Get_accessToken
            string Tokenjson = Get_accessToken();
            if (Tokenjson == null)
                return "-1";//Token获取异常
            rovokemodel orderrovokemodel = new rovokemodel();
            orderrovokemodel.dealType = "2";// 1 确认 2 取消
            orderrovokemodel.orderId = dyjlmodel.Id;
            orderrovokemodel.language = "zh_CN";
            List<waybillNoInfomodel> alist = new List<waybillNoInfomodel>();
            waybillNoInfomodel mxmodel = new waybillNoInfomodel();
            mxmodel.waybillNo = dyjlmodel.Kd_no;
            mxmodel.waybillType = dyjlmodel.waybillType;
            alist.Add(mxmodel);
            orderrovokemodel.waybillNoInfoList = alist;

            string timestamp = ConvertDateTimeToInt(DateTime.Now).ToString();
            string data = "";
            data = JsonConvert.SerializeObject(orderrovokemodel);
            var bodyNvc = new NameValueCollection();
            bodyNvc.Add("serviceCode", "EXP_RECE_UPDATE_ORDER");
            bodyNvc.Add("partnerID", CusKey);
            bodyNvc.Add("requestID", dyjlmodel.Id);
            bodyNvc.Add("timestamp", timestamp);
            bodyNvc.Add("accessToken", Tokenjson);
            bodyNvc.Add("msgData", data);
            string res = CreatePostSysHttpResponse(host, null, bodyNvc, 5000, Encoding.UTF8, null);
            return res;
        }

        #region //取消订单的实体
        public class rovokemodel
        {
            public virtual string dealType { get; set; }

            public virtual string orderId { get; set; }

            public virtual string language { get; set; }
            public virtual IList<waybillNoInfomodel> waybillNoInfoList { get; set; }
        }

        public class waybillNoInfomodel
        {
            public virtual string waybillNo { get; set; }

            public virtual string waybillType { get; set; }
        }
        #endregion
        #endregion
    }

}