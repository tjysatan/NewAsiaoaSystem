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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    public class DBEpressinterHelper
    {
        private const String host = "http://gwapi.deppon.com/dop-interface-async/standard-order/createOrderNotify.action";
       // private const String host = "http://dpsanbox.deppon.com/sandbox-web/dop-standard-ewborder/createOrderNotify.action";
        private const String revhost = "http://gwapi.deppon.com/dop-interface-async/dop-standard-ewborder/cancelEwaybillOrder.action";
        private const String companyCode = "EWBSZXYKJYXGS";
        private const String APPKEY = "26abbf50c5000635a59a61e917b29404";
        private const String SIGN = "LUKF";
        private const String customerCode = "400030358";
       // private const String customerCode = "219401 ";
        public static INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        public static ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        public static INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;


        public static string Main(EP_jlinfoView dyjlmodel)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号3" + dyjlmodel.Id);

            ReqPushOrderJdpInfoReq ordermodel = new ReqPushOrderJdpInfoReq();
            ordermodel.logisticID = SIGN + dyjlmodel.Id;
            ordermodel.custOrderNo = dyjlmodel.Id;
            ordermodel.needTraceInfo = "1";
            ordermodel.companyCode = companyCode;
            ordermodel.orderType = "2";
            ordermodel.transportType = dyjlmodel.transportType;//注外传
            ordermodel.customerCode = customerCode;
            SysPersonView permodel = _ISysPersonDao.NGetModelById(dyjlmodel.JjId);
            sender sendermodel = new sender();
            sendermodel.companyName = "苏州新亚";
            sendermodel.name = permodel.UserName;
            sendermodel.mobile = permodel.Tel;
            sendermodel.province = "江苏省";
            sendermodel.city = "苏州市";
            sendermodel.county = "太仓市";
            sendermodel.address = "花墙工业区新亚路2号";

            ordermodel.sender = sendermodel;
            receiver receivermodel = new receiver();
            if (dyjlmodel.SjaddId != "" && dyjlmodel.SjaddId != null)
            {
                NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);//获取公司数据
                NA_AddresseeInfoView SJRmodel = _INA_AddresseeInfoDao.NGetModelById(dyjlmodel.SjaddId);

                receivermodel.name = SJRmodel.Aname;//收件人
                receivermodel.phone = SJRmodel.Tel;//收件联系方式
                receivermodel.province = SJRmodel.qyo;//省
                receivermodel.city = SJRmodel.qye;//市
                receivermodel.county = SJRmodel.qyt;//区县
                receivermodel.address = SJRmodel.Address;//具体地址
                receivermodel.companyName = receivemodel.Name;
               
            }
            else {
                NACustomerinfoView receivemodel = _INACustomerinfoDao.NGetModelById(dyjlmodel.SjId);
                receivermodel.name = receivemodel.LxrName;//收件人
                receivermodel.phone = receivemodel.Tel;//收件联系方式
                receivermodel.province = receivemodel.qyname;//省
                receivermodel.city = receivemodel.qycname;//市
                receivermodel.county = receivemodel.qyqxname;//区县
                receivermodel.address = receivemodel.Adress;//具体地址
                receivermodel.companyName = receivemodel.Name;
            }
            ordermodel.receiver = receivermodel;
            packageInfo packageInfomodel = new packageInfo();
            packageInfomodel.cargoName = dyjlmodel.cargoName;
            packageInfomodel.totalNumber ="1";
            packageInfomodel.totalWeight = dyjlmodel.totalWeight;
            packageInfomodel.deliveryType = dyjlmodel.deliveryType;
            ordermodel.packageInfo = packageInfomodel;
            ordermodel.gmtCommit= DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            ordermodel.payType = dyjlmodel.payType;
            ordermodel.isOut = "N";
            string data = "";
            data = JsonConvert.SerializeObject(ordermodel);
            string timestamp = ConvertDateTimeToInt(DateTime.Now).ToString();
            String plainText = data + APPKEY + timestamp;
            //string digest = EncryptMD5Base64(plainText);
            string digest = Convert.ToBase64String(Encoding.UTF8.GetBytes(EncryptMD5Base64(plainText)));
            var bodyNvc = new NameValueCollection();
            bodyNvc.Add("companyCode", companyCode);
            bodyNvc.Add("digest", digest);
            bodyNvc.Add("timestamp", timestamp);
            bodyNvc.Add("params", data);
             string res = CreatePostSysHttpResponse(host, null, bodyNvc, 5000, Encoding.UTF8, null);
            return res;
        }

        public static string revokeMain(EP_jlinfoView dyjlmodel) {
            revokemodel revmode = new revokemodel();
            revmode.logisticID = dyjlmodel.logisticID;
            revmode.logisticCompanyID = "DEPPON";
            revmode.mailNo = dyjlmodel.Kd_no;
            revmode.cancelTime = DateTime.Now.ToString();
            revmode.remark = "撤销订单";
            string data = "";
            data = JsonConvert.SerializeObject(revmode);
            string timestamp = ConvertDateTimeToInt(DateTime.Now).ToString();
            String plainText = data + APPKEY + timestamp;
            //string digest = EncryptMD5Base64(plainText);
            string digest = Convert.ToBase64String(Encoding.UTF8.GetBytes(EncryptMD5Base64(plainText)));
            var bodyNvc = new NameValueCollection();
            bodyNvc.Add("companyCode", companyCode);
            bodyNvc.Add("digest", digest);
            bodyNvc.Add("timestamp", timestamp);
            bodyNvc.Add("params", data);
            string res = CreatePostSysHttpResponse(revhost, null, bodyNvc, 5000, Encoding.UTF8, null);
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

        private static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
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
                sb.Append(b.ToString("x2").ToLower());
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //byte[] dataHash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            //StringBuilder sb = new StringBuilder();
            //foreach (byte b in dataHash)
            //{
            //    sb.Append(b.ToString("x2").ToLower());
            //}
            ////Console.WriteLine(sb.ToString());
            ////return sb.ToString();
            //string rValue = "";
            ////var m5 = new MD5CryptoServiceProvider();

            //byte[] inputBye;
            //byte[] outputBye;
            //try
            //{
            //    inputBye = Encoding.GetEncoding(charset).GetBytes(sb.ToString());
            //}
            //catch (Exception)
            //{
            //    inputBye = Encoding.UTF8.GetBytes(sb.ToString());
            //}
            //outputBye = md5.ComputeHash(inputBye);
            //rValue = Convert.ToBase64String(outputBye, 0, outputBye.Length);

            //return rValue;
        }
 

        public class ReqPushOrderJdpInfoReq {
            /// <summary>
            /// 渠道单号 由第三方接入商产生的订单号（生成规则为sign+数字，sign值由双方约定）
            /// </summary>
            public string logisticID { get; set; }

            /// <summary>
            /// 客户订单号/商户订单号
            /// </summary>
            public string custOrderNo { get; set; }

            /// <summary>
            /// 预埋单号时传运单号，不传时会返回运单号给客户
            /// </summary>
            //public string mailNo { get; set; }

            //
            ///

            /// <summary>
            /// 是否需要订阅轨迹 1：是（为是时要对接轨迹推送接口） 2：否 默认否
            /// </summary> 
            public string needTraceInfo { get; set; }

            /// <summary>
            /// 第三方接入商的公司编码
            /// </summary>
            public string companyCode { get; set; }

            /// <summary>
            /// 下单模式 1、	散客模式（单量较小，平台类，异地调货，退换货等发货地址不固定-需要通知快递员或者司机上门取件打单;整车订单也选此模式）； 2、	大客户模式（仓库发货，固定点出货，单量较大客户自行打印标签，快递员直接盲扫走货）3、同步筛单下单（只支持大客户模式快递下单）
            /// </summary>
            public string orderType { get; set; }

            /// <summary>
            ///  （具体传值请与月结合同签订约定的为准，否则可能影响计费） 快递运输方式 : RCP：大件快递360； NZBRH：重包入户； ZBTH：重包特惠； WXJTH：微小件特惠； JJDJ：经济大件； PACKAGE：标准快递； DEAP：特准快件；EPEP ：电商尊享；CITYPIECE：同城件 零担运输方式： JZKY：精准空运（仅散客模式支持该运输方式）; JZQY_LONG：精准汽运； JZKH：精准卡航； AGENT_VEHICLE：汽运偏线； DTD：精准大票-经济件； YTY：精准大票-标准件； PCP：精准包裹; 整车运输方式 1.精准整车 JZZC 2.整车配送 ZCPS 3.精准专车 JZZHC 4.精准拼车JZPC
            /// </summary>
            public string transportType { get; set; }

            /// <summary>
            /// 客户编码/月结账号
            /// </summary>
            public string customerCode { get; set; }

            /// <summary>
            /// 发货人信息
            /// </summary>
            public sender sender { get; set; }

            /// <summary>
            /// 收件人信息
            /// </summary>
            public receiver receiver { get; set; }

            /// <summary>
            /// 包裹信息
            /// </summary>
            public packageInfo packageInfo { get; set; }

            /// <summary>
            /// 订单提交时间
            /// </summary>
            public string gmtCommit { get; set; }

            /// <summary>
            /// 0:发货人付款（现付） 1:收货人付款（到付） 2：发货人付款（月结） （电子运单客户不支持寄付）
            /// </summary>
            public string payType { get; set; }

            /// <summary>
            /// 是否外发 N
            /// </summary>
            public string isOut { get; set; }
        }

        /// <summary>
        /// 发货人
        /// </summary>
        public class sender {
            /// <summary>
            /// 发货人公司 N
            /// </summary>
            public string companyName { get; set; }

            /// <summary>
            /// 发货部门编码 N
            /// </summary>
            public string businessNetworkNo { get; set; }

            /// <summary>
            /// 发货人名称 y
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 发货人手机 y
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 发货人电话 N
            /// </summary>
            public string phone { get; set; }

            /// <summary>
            /// 发货人省份 y
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

            /// <summary>
            /// 发货人乡镇街道 N
            /// </summary>
            public string town { get; set; }

            /// <summary>
            /// 发货人详细地址 y
            /// </summary>
            public string address { get; set; }
        }

        /// <summary>
        /// 收件人
        /// </summary>
        public class receiver
        {
            /// <summary>
            /// 到达部门编码 N
            /// </summary>
            public string toNetworkNo { get; set; }

            /// <summary>
            /// 收货人名称 y
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 收货人电话 n
            /// </summary>
            public string phone { get; set; }

            /// <summary>
            /// 收货人手机 y
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 收货人省份y
            /// </summary>
            public string province { get; set; }

            /// <summary>
            /// 收货人城市 y
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 收货人区县 y
            /// </summary>
            public string county { get; set; }

            /// <summary>
            /// 收货人镇街道 n
            /// </summary>
            public string town { get; set; }

            /// <summary>
            /// 收货人详细地址 y
            /// </summary>
            public string address { get; set; }

            /// <summary>
            /// 收货人公司 N
            /// </summary>
            public string companyName { get; set; }
        }

        public class packageInfo
        {
            /// <summary>
            /// 货物名称y
            /// </summary>
            public string cargoName { get; set; }

            /// <summary>
            /// 总件数（包裹数） y
            /// </summary>
            public string totalNumber { get; set; }

            /// <summary>
            /// 总重量 y kg
            /// </summary>
            public string totalWeight { get; set; }

            /// <summary>
            /// 总体积 y m³
            /// </summary>
            public string totalVolume { get; set; }

            /// <summary>
            /// 包装 N
            /// </summary>
            public string packageService { get; set; }

            /// <summary>
            /// 送货方式 1、自提； 2、送货进仓； 3、送货（不含上楼）； 4、送货上楼； 5、大件上楼
            /// </summary>
            public string deliveryType { get; set; }

        }

        /// <summary>
        /// 撤销实体
        /// </summary>
        public class revokemodel { 
            public string logisticCompanyID { get; set; }

            public string logisticID { get; set; }

            public string mailNo { get; set; }

            public string cancelTime { get; set; }

            public string remark { get; set; }
        }


    }
}