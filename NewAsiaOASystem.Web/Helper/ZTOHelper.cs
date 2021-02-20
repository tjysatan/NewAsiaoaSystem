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

    public class ZTOHelper
    {
             private const String app_id ="ZTO531525753617737";
             private const String app_key = "LICYL75F";
             private const string shopkey = "NTQ3QTY1NjBEQ0UwODcyNjUxNUNFQ0RBQ0Y4OTBDODI=";
            // private const string shopkey = "T3BlbktER0pfMjAxNzUxNzEyMjAyODI=";
            // private const string url = " http://58.40.16.122:8080/exposeServicePushOrderService";//测试地址
              private const string url = "http://japi.zto.cn/exposeServicePushOrderService";


             public static INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
             public static ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
             public static INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
             /// <summary>
             //1.构建订单参数(需要将order参数)
             //2构建请求参数
             //3通过post请求结果 
             /// </summary>
             /// <param name="args"></param>
             public static string Main(EP_jlinfoView dyjlmodel)
             {
                 log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号3" + dyjlmodel.Id);
               //  string company_id = "ztoOrderTest";
                 string company_id = "53270c3e164640c4a4fed1889364179a";
               //  string key = "enRvMTIzc2lnbndoeA==";
                 string key = "be34373937cc";
                 string data = "";
                // string data = "{\"shopKey\":\"T3BlbktER0pfMjAxNzUxNzEyMjAyODI=\",\"unionCode\":\"536178917071\",\"send_province\":\"上海市\",\"send_city\":\"上海市\",\"send_district\":\"青浦区\",\"send_address\":\"华新镇华志路123号\",\"receive_province\":\"四川省\",\"receive_city\":\"成都市\",\"receive_district\":\"武侯区\",\"receive_address\":\"610号和兴润园二期1栋2单元1003室\"}";
                 ReqPushOrderJdpInfoReq ordermodel = new ReqPushOrderJdpInfoReq();
                 ordermodel.orderId = dyjlmodel.Id;
                 ordermodel.shopKey = shopkey;
                 ordermodel.orderType = "0";
                 if (dyjlmodel.SjaddId != "" && dyjlmodel.SjaddId != null)
                 {
                     NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);//获取公司数据
                     NA_AddresseeInfoView SJRmodel = _INA_AddresseeInfoDao.NGetModelById(dyjlmodel.SjaddId);
                     ordermodel.receiveMan = SJRmodel.Aname;//收件人
                     ordermodel.receivePhone = SJRmodel.Tel;//收件联系方式
                     ordermodel.receiveMobile = "";//固定电话
                     ordermodel.receiveZip = "";
                     ordermodel.receiveProvince = SJRmodel.qyo;//省
                     ordermodel.receiveCity = SJRmodel.qye;//市
                     ordermodel.receiveCounty = SJRmodel.qyt;//区县
                     ordermodel.receiveTown = "";
                   //ordermodel.receiveAddress = SJRmodel.Address + "/" + receivemodel.Name;//具体地址+公司
                ordermodel.receiveAddress = SJRmodel.Address ;//具体地址
                ordermodel.receiveCompany = receivemodel.Name;
                 }
                 else
                 {
                     NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);
                     ordermodel.receiveMan = receivemodel.LxrName;//收件人
                     ordermodel.receivePhone = receivemodel.Tel;//收件联系方式
                     ordermodel.receiveMobile = "";//固定电话
                     ordermodel.receiveZip = "";
                     ordermodel.receiveProvince = receivemodel.qyname;//省
                     ordermodel.receiveCity = receivemodel.qycname;//市
                     ordermodel.receiveCounty = receivemodel.qyqxname;//区县
                     ordermodel.receiveTown = "";
                //ordermodel.receiveAddress = receivemodel.Adress + "/" + receivemodel.Name;//具体地址+公司
                ordermodel.receiveAddress = receivemodel.Adress;//具体地址+公司
                ordermodel.receiveCompany = receivemodel.Name;
                 }
                 SysPersonView permodel = _ISysPersonDao.NGetModelById(dyjlmodel.JjId);
                 ordermodel.sendMan = permodel.UserName;
                 ordermodel.sendPhone = permodel.Tel;
                 ordermodel.sendMobile = "";
                 ordermodel.sendProvince = "江苏省";
                 ordermodel.sendCity = "苏州市";
                 ordermodel.sendCounty = "太仓市";
                 ordermodel.sendAddress = "花墙工业区新亚路2号";
                 ordermodel.sendCompany = "苏州新亚";
                 ordermodel.sendZip = "";
                 ordermodel.orderDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                 //ordermodel.payDate = "";
                 //ordermodel.payment = "";
                 ordermodel.sellerMessage = dyjlmodel.DHno;
                 ordermodel.buyerMessage = "";
                 List<goodInfo> goodlist = new List<goodInfo>();
                 goodInfo goodEntity = new goodInfo();
                 goodlist.Add(goodEntity);
                 ordermodel.goodInfo = new List<goodInfo>();
                 ordermodel.goodInfo.AddRange(goodlist);
                 data = JsonConvert.SerializeObject(ordermodel);
                 log4net.LogManager.GetLogger("ApplicationInfoLog").Error("data" + data);
                 var bodyNvc = new NameValueCollection();
                 bodyNvc.Add("company_id", company_id);
                 bodyNvc.Add("msg_type", "GETMARK");
                 bodyNvc.Add("data", data);
                 // 这里生成签名的body字符串的拼接顺序，要和http请求的参数顺序一致，也就是和上面bodyNvc的添加顺序一致
                 string body = "company_id=" + company_id + "&msg_type=GETMARK" + "&data=" + data;
                 var headersNvc = new NameValueCollection();
                 headersNvc.Add("x-companyId", company_id);
                 //得到签名
                 string dataDigest = EncryptMD5Base64(body + key);
                 headersNvc.Add("x-dataDigest", dataDigest);
                 string res = CreatePostSysHttpResponse(url, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
                 return res;
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
            catch(Exception e)
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

        #region 帮助方法
        /// <summary>
        /// 设置请求的参数信息
        /// </summary>
        /// <param name="request"></param>
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="data"></param>
        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }
        /// <summary>
        /// 编码方式
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static byte[] EncodePars(NameValueCollection Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }
        /// <summary>
        /// 参数转为字符串
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static String ParsToString(NameValueCollection Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }
        #endregion


         /// <summary>
        /// 开放平台签名[MD5 + BASE64]
        /// </summary>
        /// <param name="encryptStr">加密的字符串(请求数据[data] + 消息密钥[key])</param>
        /// <param name="charset">编码方式, 默认UTF-8</param>
        /// <returns></returns>
        public static string EncryptMD5Base64(string encryptStr, string charset = "UTF-8")
        {
            string rValue = "";
            var m5 = new MD5CryptoServiceProvider();

            byte[] inputBye;
            byte[] outputBye;
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encryptStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.UTF8.GetBytes(encryptStr);
            }
            outputBye = m5.ComputeHash(inputBye);
            rValue = Convert.ToBase64String(outputBye, 0, outputBye.Length);

            return rValue;
        }

        //public ReqPushOrderJdpInfoReq constructOrderInfoRequestParameter()
        //{
        //    ReqPushOrderJdpInfoReq jdpResponse = new ReqPushOrderJdpInfoReq();
        //    List<goodInfo> goodList = new List<goodInfo>();
        //    goodInfo goodEntity = new goodInfo();
        //    goodList.Add(goodEntity);
        //    jdpResponse.goodInfo = new List<goodInfo>();
        //    jdpResponse.goodInfo.AddRange(goodList);
        //    reqPushOrderInfoReq.jdpResponse = jdpResponse;
        //    string orderJson = JsonConvert.SerializeObject(reqPushOrderInfoReq);
        //    string sign = new Program().getSign(orderJson);
        //    return reqPushOrderInfoReq;
        //}


        /// <summary>
        /// 订单信息
        /// </summary>
        public class ReqPushOrderJdpInfoReq
        {
            /// <summary>
            /// 订单ID
            /// </summary>
            public string orderId { get; set; }

            /// <summary>
            /// 店铺key
            /// </summary>
            public string shopKey { get; set; }

            /// <summary>
            /// 订单类型(0代表普通订单1代表代收货款)
            /// </summary>
            public string orderType { get; set; }

            /// <summary>
            /// 如果orderType=0，不需要传；如果orderType=1，必传，大于0，小于等于10000，单位：元
            /// </summary>
            public string collectionMoney { get; set; }


           
            /// <summary>
            /// 收件人姓名
            /// </summary>
            public string receiveMan { get; set; }
            /// <summary>
            /// 收件人电话(电话和手机号码二选一)
            /// </summary>
            public string receivePhone { get; set; }
            /// <summary>
            /// 收件人手机(电话和手机号码二选一)
            /// </summary>
            public string receiveMobile { get; set; }

            /// <summary>
            /// 收件邮编
            /// </summary>
            public string receiveZip { get; set; }
            /// <summary>
            /// 收件人省
            /// </summary>
            public string receiveProvince { get; set; }


            /// <summary>
            /// 收件人市
            /// </summary>
            public string receiveCity { get; set; }
            /// <summary>
            /// 收件人区
            /// </summary>
            public string receiveCounty { get; set; }

            /// <summary>
            /// 收件人镇
            /// </summary>
            public string receiveTown { get; set; }
            /// <summary>
            /// 收件人详细地址
            /// </summary>
            public string receiveAddress { get; set; }

            /// <summary>
            /// 收件人单位
            /// </summary>
            public string receiveCompany { get; set; }
   


            /// <summary>
            /// 发件人姓名
            /// </summary>

            public string sendMan { get; set; }
            /// <summary>
            /// 发件人电话
            /// </summary>

            public string sendPhone { get; set; }
            /// <summary>
            /// 发件人手机号
            /// </summary>


            public string sendMobile { get; set; }
            /// <summary>
            /// 发件人省
            /// </summary>


            public string sendProvince { get; set; }
            /// <summary>
            /// 发件人市
            /// </summary>


            public string sendCity { get; set; }
            /// <summary>
            /// 发件人区
            /// </summary>


            public string sendCounty { get; set; }
            /// <summary>
            /// 发件人详细地址
            /// </summary>


            public string sendAddress { get; set; }

            /// <summary>
            /// 发件人单位
            /// </summary>
            public string sendCompany { get; set; }
            /// <summary>
            /// 发件邮编
            /// </summary>

            public string sendZip { get; set; }

            /// <summary>
            /// 下单时间
            /// </summary>
            public string orderDate { get; set; }

            /// <summary>
            /// 付款时间
            /// </summary>
            public string payDate { get; set; }

            /// <summary>
            /// 实付金额
            /// </summary>
            public string payment { get; set; }

            /// <summary>
            /// 卖家备注
            /// </summary>
            public string sellerMessage { get; set; }

            /// <summary>
            /// 买家备注
            /// </summary>
            public string buyerMessage { get; set; }
 
            /// <summary>
            /// 商品详情
            /// </summary>
            public List<goodInfo> goodInfo { get; set; }
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        public class goodInfo
        {
            /// <summary>
            /// 商品名称
            /// </summary>
            public string goodsTitle { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public string goodsNum { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// SKU的值。如：机身颜色:黑色;手机套餐:官方标配
            /// </summary>
            public string skuPropertiesName { get; set; }
           
            /// <summary>
            /// 商品的的图片URL
            /// </summary>
            public string goodsPath { get; set; }
            /// <summary>
            /// 单价
            /// </summary>
            public string unitPrice { get; set; }

        }
    }

            
    }
