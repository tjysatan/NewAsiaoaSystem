using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    /// <summary>
    /// 谭建云写的普实接口
    /// </summary>
    public class zypushsoftHelper
    {
        public static string url = "http://49.75.59.149:82//Baseitem.asmx/";
        public static string key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";

        #region //根据产品的物料代码查询BOM的明细
        /// <summary>
        /// 根据产品的物料代码查询BOM的明细
        /// </summary>
        /// <param name="wlno">物料代码</param>
        /// <returns></returns>
        public static string GetBombywlno(string wlno)
        {
            try
            {
                string lurl;
                lurl = url + "GetpushBom?codes=" + wlno + "&key=" + key;
                string result = HttpUtility11.GetData(lurl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                return sSupplier;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region //普实BOM细表更新（重新插入明细）
        public static string UpdateBomA(string datejson)
        {
            try
            {
                string lurl;
                lurl = url + "InstaerMDBomA?datajson=" + datejson + "&key=" + key;
                string result = HttpUtility11.GetData(lurl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                return sSupplier;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region //普实通过产品的物料代码查询BOM主表的单号
        public static string GetBomDocEntryByItmID(string ItmID)
        {
            try
            {
                string lurl;
                lurl = url + "GetMDBom_DocEntry?codes=" + ItmID + "&key=" + key;
                string result = HttpUtility11.GetData(lurl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                return sSupplier;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //删除Bom细表
        public static string DelBomA(string codes)
        {
            try
            {
                string lurl;
                lurl = url + "DelMDBomA?codes=" + codes + "&key=" + key;
                string result = HttpUtility11.GetData(lurl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string sSupplier = doc.InnerText;
                return sSupplier;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}