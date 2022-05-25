using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    public class BJHelper
    {
        public static string url = "http://106.14.40.169:8081/BJBasics.asmx/";
        public static string k3key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";

        public static BJmodel GetPAY_CONTROL_INFObyzbno(string zbno)
        {
            string lurl;
            lurl = url + "Getbaojiadanlist?code=" + zbno+"&key="+ k3key;
            string result = HttpUtility11.PostUrl(lurl,"");
            BJmodel fhmodel = new BJmodel(); 
            //fhmodel = JsonConvert.DeserializeObject(ss) as K3FHmodel;
            fhmodel = JsonConvert.DeserializeObject<BJmodel>(result);
            return fhmodel;
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(result);
            //string sSupplier = doc.InnerText;
            //return sSupplier;
        }

        #region //查询报价单的报价数据
        public static string Getbjlist(string code)
        {
            string lurl = url + "Getbaojiadanlist?code=" + code + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }

        #endregion

        #region //查询报价单的报价数据(医药)
        public static string GetbjYYlist(string code)
        {
            string lurl = url + "GetbaojiadanYYlist?code=" + code + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }

        #endregion

        #region //查询报价单的
        public static string GetbjBom(string code)
        {
            string lurl = url + "GetBjBombyCONTROL_INFO_ID?code=" + code + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //查询报价单清单（费用）
        public static string Getbjqingdan(string code)
        {
            string lurl = url + "GetCONTROL_LISTbyCONTROL_INFO_ID?code=" + code + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion



    }
}