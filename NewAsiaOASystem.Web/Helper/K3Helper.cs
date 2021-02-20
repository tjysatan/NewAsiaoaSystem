using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    /// <summary>
    /// K3 接口
    /// </summary>
    public class K3Helper
    {
        public static string url = "http://222.92.203.58:83//Baseitem.asmx/";
        #region //插入生产订单号和订单的责任工程师
        public static string InsertBianhaoandengineer(string bianhao, string engineer)
        {
            bianhao = bianhao.Replace("DKX", "");
            //url = "http://222.92.203.58:83//Baseitem.asmx/INSERTbianhao?bianhao=" + bianhao;
            string lurl = url + "INSERTbianhao?bianhao=" + bianhao + "&gcsname=" + engineer;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion
    }
}