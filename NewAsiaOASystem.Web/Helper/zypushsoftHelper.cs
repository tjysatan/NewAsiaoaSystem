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

        #region //根据产品的物料代码查询没有停用BOM的明细
        public static string GetBom_QYbywlno(string wlno)
        {
            try
            {
                string lurl;
                lurl = url + "GetpushBOM_QY?codes=" + wlno + "&key=" + key;
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

        #region //查询普实里面新增的物料
        public static string GetMDItmbyOpDate(string OpDate)
        {
            try {
                string lurl;
                lurl = url + "GetMDItm_OpDate?codes=" + OpDate + "&key=" + key;
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

        #region //查询普实物料的及时库存
        public static string GetBCStkbyItmID(string ItmID)
        {
            try
            {
                string lurl;
                lurl = url + "GetBCStk_ItmID?codes=" + ItmID + "&key=" + key;
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

        #region //查询普实物料的未清拣配数量
        public static string Get_SATmpAsum_by_ItmID(string ItmID)
        {
            try
            {
                string lurl;
                lurl = url + "Get_SATmpAsum_by_ItmID?codes=" + ItmID + "&key=" + key;
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

        #region //查询普实的暂收送检的数据
        public static string GetPUTmp_DocEntry(string DocEntry)
        {
            try
            {
                string lurl;
                lurl = url + "GetPUTmp_DocEntry?codes=" + DocEntry + "&key=" + key;
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

        #region //根据暂收送检的单号单独查询暂收送简单数据
        public static string GetPUTmpmodel_DocEntry(string DocEntry)
        {
            try
            {
                string lurl;
                lurl = url + "GetPUTmpmodel_DocEntry?codes=" + DocEntry + "&key=" + key;
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

        #region //更新非标产品中责任工程师备注客户和软件硬件种类
        public static string updateMDMDItmZ_temName(string ItmID, string temName, string Cusname)
        {
            try
            {
                string lurl;
                lurl = url + "updateMDMDItmZ_temName?ItmID=" + ItmID+ "&temName="+ temName+ "&Cusname="+ Cusname+ "&key=" + key;
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

        #region //根据发货拣配单单号查询发货拣配单的主表数据
        public static string GetSATmp_DocEntry(string DocEntry)
        {
            try
            {
                string lurl;
                lurl = url + "GetSATmp_DocEntry?codes=" + DocEntry + "&key=" + key;
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

        #region ///根据发货拣配单单号查询单据的明细表数据
        public static string GetSATmpA_DocEntry(string DocEntry)
        {
            try
            {
                string lurl;
                lurl = url + "GetSATmpA_DocEntry?codes=" + DocEntry + "&key=" + key;
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

        #region //根据日期例如：202110（月份）
        public static string GetSASalA_AbsID(string code)
        {
            try
            {
                string lurl;
                lurl = url + "GetSASalA_AbsID?codes=" + code + "&key=" + key;
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

        #region //根据物料代码查询物料的信息
        /// <summary>
        /// 根据物料代码查询物料的信息
        /// </summary>
        /// <param name="code">物料代码</param>
        /// <returns></returns>
        public static string Get_MDItm_by_ItmID(string code)
        {
            try
            {
                string lurl;
                lurl = url + "Get_MDItm_by_ItmID?codes=" + code + "&key=" + key;
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

        #region //查询普实物料的及时库存/生产订单在定量/销售订单预约量/拣配单未清量
        public static string Get_ItmeZHinfo_ItmID(string ItmID)
        {
            try
            {
                string lurl;
                lurl = url + "Get_ItmeZHinfo_ItmID?codes=" + ItmID + "&key=" + key;
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

        #region //更新ERP中物料的工序工时数据
        /// <summary>
        /// 更新ERP中物料的工序工时数据
        /// </summary>
        /// <param name="ItmID"></param>
        /// <param name="Z_DQZZDBGS"></param>
        /// <param name="Z_DQZZKZXGS"></param>
        /// <param name="Z_DQZZZHLGS"></param>
        /// <param name="Z_DQZZMBZXGS"></param>
        /// <param name="Z_DQZZMBJXGS"></param>
        /// <returns></returns>
        public static string updateMDItm_GXGS(string ItmID,string Z_DQZZDBGS,string Z_DQZZKZXGS,string Z_DQZZZHLGS,string Z_DQZZMBZXGS,string Z_DQZZMBJXGS)
        {
            try
            {
                string lurl;
                lurl=url+ "updateMDItm_GXGS?ItmID="+ ItmID + "&Z_DQZZDBGS="+ Z_DQZZDBGS+ "&Z_DQZZKZXGS="+ Z_DQZZKZXGS+ "&Z_DQZZZHLGS="+ Z_DQZZZHLGS+ "&Z_DQZZMBZXGS="+ Z_DQZZMBZXGS+ "&Z_DQZZMBJXGS="+ Z_DQZZMBJXGS+ "&key=" + key;
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

        #region //更新ERP中的物料的内部售价
        public static string updateupdateMDItm_Z_NBPrice(string ItmID, string NBPrice)
        {
            try
            {
                string lurl;
                lurl = url + "updateMDItm_Z_NBPrice?ItmID=" + ItmID + "&NBPrice=" + NBPrice + "&key=" + key;
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

        #region //更新ERP中的物料的物料分类属性
        public static string updateMDItm_Z_WLSX(string ItmID, string Z_WLSX)
        {
            try
            {
                string lurl;
                lurl = url + "updateMDItm_Z_WLSX?ItmID=" + ItmID + "&Z_WLSX=" + Z_WLSX + "&key=" + key;
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

        #region //查询原材料的工序工时
        /// <summary>
        /// 查询原材料的工序工时
        /// </summary>
        /// <param name="ItmID"></param>
        /// <returns></returns>
        public static string GetMDItm_GXGS(string ItmID)
        {
            try
            {
                string lurl;
                lurl = url + "GetMDItm_GXGS?ItmID=" + ItmID + "&key=" + key;
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

        #region //通过物料的规格型号查询物料信息
        /// <summary>
        /// 通过物料的规格型号查询物料信息
        /// </summary>
        /// <param name="ItmSpec"></param>
        /// <returns></returns>
        public static string GetMDItm_ItmSpec(string ItmSpec)
        {
            try
            {
                string lurl;
                lurl = url + "GetMDItm_ItmSpec?ItmSpec=" + ItmSpec + "&key=" + key;
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

        #region //通过物料代理和近期 天数【一段时间】查询生产领料出库数据
        public static string GetWHDrw_ItmIDand_Days(string ItmID, string days)
        {
            try
            {
                string lurl;
                lurl = url + "GetWHDrw_ItmIDand_Days?ItmID=" + ItmID + "&days" + days+ "&key=" + key;
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
    }
}