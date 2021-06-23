using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class pushsoftHelper
    {
        //销售订单的同步地址
        public static string urlorder = "http://49.75.59.149:81/SZ_LHWLJS/Api/Api.SaleOrder/Create?site=S01&token=70CA3EF4CB2DDD247B2735233F2FF2AA&version=v1.0";
        //非标产品同步地址
        public static string urlcp = "http://49.75.59.149:81/SZ_LHWLJS/Api/Api.Design/Create?site=M01&token=B2466E10663295A5C0BFCB16E8D60D14&version=v1.0";
        //非标bom同步地址
        public static string urlbom = "http://49.75.59.149:81/SZ_LHWLJS/Api/Api.Bom/Create?site=M02&token=A5B508A30D1420A25B2BDA086CCE609B&version=v1.0";

        #region //同步销售订单
        public static string Insterorder(pushsoftorder model)
        {
            try
            {

                string jsonstr = JsonConvert.SerializeObject(model);
                jsonstr= "{P0:" + jsonstr + "}";
                var headersNvc = new NameValueCollection();
                var bodyNvc = new NameValueCollection();
                bodyNvc.Add("data", jsonstr);
                string res = gwjHelper.CreatePostSysHttpResponse(urlorder, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
                return res;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region //同步非标产品
        public static string Instercpinfo(Ps_fbcpmodel model)
        {
            string psstr = "02384472-2337-444b-a3d0-dd129cbcbfcd";
            string jsonstr = JsonConvert.SerializeObject(model);
            jsonstr = "{P0:'" + psstr + "',"+"P1:"+ jsonstr + "}";
            var headersNvc = new NameValueCollection();
            var bodyNvc = new NameValueCollection();
            bodyNvc.Add("data", jsonstr);
            string res = gwjHelper.CreatePostSysHttpResponse(urlcp, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
            return res;
        }
        #endregion

        #region //同步非标BOM
        public static string InstaerBominfo(Ps_Bommodel model)
        {
            try
            {
                string jsonstr = JsonConvert.SerializeObject(model);
                jsonstr = "{P0:" + jsonstr + "}";
                var headersNvc = new NameValueCollection();
                var bodyNvc = new NameValueCollection();
                bodyNvc.Add("data", jsonstr);
                string res = gwjHelper.CreatePostSysHttpResponse(urlbom, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
                return res;
            }
            catch
            {
                return "";
            }
        }
        #endregion



    }
}