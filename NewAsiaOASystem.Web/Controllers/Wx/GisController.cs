using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class GisController : Controller
    {
        //
        // GET: /Gis/

        public ActionResult Index(string AID, string code)
        {
            ViewData["code"] = code;
            ViewData["AID"] = AID;
            //JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            //string json = GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
            return View();
        }

        //访问接口（通过code，请求以下链接获取access_token和openid）

        public object Getaccess_tokenbycodeinface(string code)
        {
          
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f57e0f1268190b&secret=bcbfe205a0e5fad5a4ab7f2ebb90656d&code={0}&grant_type=authorization_code ", code);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

    

        public string GetYHiNFOinface(string access_token, string openid)
        {
            //access_token = "0HrM7H8eHJyw03m8xv0UkCFrdz4AGGEc9eZCe_0odEyRnv_MnfgjhVnubOxJHpM2excWR2nkVlrUxgPrhT03l7GF9uREMqzyqayVsckwLng";
            //openid = "ol6V6t-M-QUOkjLqQ8_wgSZ9jYMs";
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token,openid);
            string json = HttpUtility11.GetData(url);
            return json;
        }  
    }
}
