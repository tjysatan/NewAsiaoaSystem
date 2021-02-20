using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{

    public class ExpressinterfaceHelper
    {
        private const String host = "http://jisukdcx.market.alicloudapi.com";
        private const String path = "/express/query";
        private const String method = "GET";
        private const String appcode = "53b83047730d4ebb85562535ba7da818";
         
        private const String Newhost = "http://kdwlcxf.market.alicloudapi.com";
        private const String Newpath = "/kdwlcx";
        private const String Newmethod = "GET";
        private const String Newappcode = "53b83047730d4ebb85562535ba7da818";


        public static string Main(string kdno, string kdgongs)
        {
            String querys = "number="+kdno+"&type="+kdgongs;
            String bodys = "";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            //Console.WriteLine(httpResponse.StatusCode);
            //Console.WriteLine(httpResponse.Method);
            //Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            //XmlDocument xml = new XmlDocument();
            //xml.Load(st);
            //string outXmlString = xml.OuterXml;
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            string htmlData = reader.ReadToEnd();
            return htmlData;
        }

        public static string NewMain(string kdno, string kdgongs,string tel)
        {
            String querys;
            if (kdgongs =="SFEXPRESS")
            {
                querys = "no=" + kdno +":"+tel+ "&type=" + kdgongs;
            }
            else
            {
                querys = "no=" + kdno + "&type=" + kdgongs;
            }
            log4net.LogManager.GetLogger("参数" + querys);
            //String querys = "no=780098068058&type=zto";
            String bodys = "";
            String url = Newhost + Newpath;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = Newmethod;
            httpRequest.Headers.Add("Authorization", "APPCODE " + Newappcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            //Console.WriteLine(httpResponse.StatusCode);
            //Console.WriteLine(httpResponse.Method);
            //Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            //XmlDocument xml = new XmlDocument();
            //xml.Load(st);
            //string outXmlString = xml.OuterXml;
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            string htmlData = reader.ReadToEnd();
            log4net.LogManager.GetLogger("结果" + htmlData);
            return htmlData;
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}