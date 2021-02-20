using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;
using System.IO;
using Microsoft.SqlServer;

namespace NewAsiaOASystem.Web
{

    public class MenuContext
    {
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];
        private static DateTime GetAccessToken_Time;
        /// <summary>
        /// 过期时间为7200秒
        /// </summary>
        private static int Expires_Period = 7200;
        /// <summary>
        /// 
        /// </summary>
        private static string mAccessToken;
        /// <summary>
        /// 
        /// </summary>
        public static string AccessToken
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(mAccessToken) || HasExpired())
                {
                    //获取
                    mAccessToken = GetAccessToken(AppId, AppSecret);
                }

                return mAccessToken;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        private static string GetAccessToken(string appId, string appSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            string result = HttpUtility11.GetData(url);

            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement access_token = root.Element("access_token");
                if (access_token != null)
                {
                    GetAccessToken_Time = DateTime.Now;
                    if (root.Element("expires_in") != null)
                    {
                        Expires_Period = int.Parse(root.Element("expires_in").Value);
                    }
                    return access_token.Value;
                }
                else
                {
                    GetAccessToken_Time = DateTime.MinValue;
                }
            }
            return null;

         
        }


        //网页授权页面 关注的粉丝 回调openid
        public static string GetOAuthOpenID(string code)
        {
            string appId = AppId;
            string secret = AppSecret;
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, secret,code);
            string result = HttpUtility11.GetData(url);
            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement openid = root.Element("openid");
                if (openid != null)
                {
                    return openid.Value;
                }
            }
            return null;
        }



        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired()
        {
            if (GetAccessToken_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > GetAccessToken_Time.AddSeconds(Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }


        /// </summary>
     //   private static string mthumb_media_id;
        /// <summary>
        // public static string thumb_media_id(string path)
        // {

        //  mthumb_media_id = Getthumb_media_id(string path);


        // }

        /// <summary>
        /// 获取缩略图thumb_media_id
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        public static string Getthumb_media_id(string path)
        {
            string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type=thumb", AccessToken);
            string result = HttpUtility11.HttpUploadFile(url, path);
            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement thumb_media_id = root.Element("thumb_media_id");
                if (thumb_media_id != null)
                {
                    return thumb_media_id.Value;
                }
            }
            return null;
        }

        #region //获取全部关注的微信OpenId
        public static string GetAllOpenId()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid=", AccessToken);
            string result = HttpUtility11.GetData(url);
    
            Tojson jsonObj = JsonConvert.DeserializeObject<Tojson>(result);
            if (jsonObj != null)
            {
               List<string> list = jsonObj.data.openid;
                String [] st=new String [list.Count];
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    st[i] = list[i];
                }

                return string.Join("\",\"", st);
            }
            return null;
        }
        #endregion

        public class Tojson
        {
            public string total { get; set; }
            public string count { get; set; }
            public data data;
            public string next_openid { get; set; }
        }

        public class data
        {
            //public string openid;
            public List<string> openid { get; set; }
        }
    }
}