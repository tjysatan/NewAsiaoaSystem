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
    /// <summary>
    /// K3 接口
    /// </summary>
    public class K3Helper
    {
        public static string url = "http://222.92.203.58:83//Baseitem.asmx/";
        public static string k3key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
        //K3生产任务单
        public static string urlscrw = "http://222.92.203.58:8081/XY/ICMOSys/JsonData";
        public static string ss = "http://192.168.10.3:8081/XY/ICMOSys/JsonData";
        #region //插入生产订单号和订单的责任工程师
        public static string InsertBianhaoandengineer(string bianhao, string engineer)
        {
            //bianhao = bianhao.Replace("DKX", "");
            //url = "http://222.92.203.58:83//Baseitem.asmx/INSERTbianhao?bianhao=" + bianhao;
            string lurl = url + "INSERTbianhao?bianhao=" + bianhao + "&gcsname=" + engineer;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //提交生产任务单
        public static string InsertICMOSys(ICMOSysmodel model)
        {
            string jsonstr = JsonConvert.SerializeObject(model);
            var request = (HttpWebRequest)WebRequest.Create(urlscrw);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteData = Encoding.UTF8.GetBytes(jsonstr);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            return responseString.ToString();

            //var headersNvc = new NameValueCollection();
            //headersNvc.Add("ContentType", "application/json");
            //var bodyNvc = new NameValueCollection();
            //bodyNvc.Add("FCustName", model.FCustName);
            //bodyNvc.Add("FNumber", model.FNumber);
            //bodyNvc.Add("FQty", model.FQty.ToString());
            //bodyNvc.Add("FBatchNo", model.FBatchNo);
            //bodyNvc.Add("FBOMNumber", model.FBOMNumber);
            //bodyNvc.Add("FDeptNumber", model.FDeptNumber);
            //bodyNvc.Add("FPlanCommitDate", model.FPlanCommitDate.ToString());
            //bodyNvc.Add("FPlanFinishDate", model.FPlanFinishDate.ToString());
            //string res =gwjHelper.CreatePostSysHttpResponse(ss, headersNvc, bodyNvc, 5000, Encoding.UTF8, null);
            //return res;

        }
        #endregion


        #region //通过产品的BOM 编号查询bom表
        public static string Getk3bombybomno(string bomno)
        {
            //string lurl = url + "GetBombybomno?bomno=" + bomno + "&key=" + k3key;
            //string result = HttpUtility11.GetData(lurl);
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(result);
            //string sSupplier = doc.InnerText;
            //return sSupplier;
            string FnumberBomstr = bomno.Replace("+", "%2B");
            string lurl;
            lurl = url+"getBomBodyByFBomnumber?fbomnumber=" + FnumberBomstr;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //通过物料代码（半成品）查询硬件成本
        public static string Getk3bompricebywlno(string wlno)
        {
            string lurl;
            lurl = url + "GetBomPricebywlno?codes=" + wlno+"&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //通过BOM编号查询产品的物料代码
        /// <summary>
        /// 通过BOM编号查询产品的物料代码
        /// </summary>
        /// <param name="bomno">物料编号</param>
        /// <returns></returns>
        public static string GetFNumberbyFBOMNumber(string bomno)
        {
            string lurl;
            lurl = url + "GetFNumberbyFBOMNumber?codes=" + bomno + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //通过物料编码查询BOM明细
        public static string GetBombywlno(string wlno)
        {
            string lurl;
            lurl = url + "GetBom?codes=" + wlno + "&key=" + k3key;
            string result = HttpUtility11.GetData(lurl);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string sSupplier = doc.InnerText;
            return sSupplier;
        }
        #endregion

        #region //通过物料代码查询及时库存
        public static string GetKcsum(string P_bianhao)
        {
            try
            {
                Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
                string Wldm ="'"+P_bianhao+"'";//物料代码
                string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
                DataTable dt = Dsmodel.GetKuCun(Wldm, Key);
                string count = "0";
                if (dt.Rows.Count > 0)
                {
                    //P_Bianhao = dt.Rows[0]["code"].ToString();//物料代码
                    count = Convert.ToDecimal(dt.Rows[0]["count"]).ToString("0.00");//库存
                }
                return count;
              
            }
            catch
            {
                return null;
            }
        }
        #endregion


    }
}