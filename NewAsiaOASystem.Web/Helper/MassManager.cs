using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using NewAsiaOASystem.DBModel;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;

namespace NewAsiaOASystem.Web
{
    public class MassManager
    {
        public static IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
        public static IWX_MessageDao _IWX_MessageDao = ContextRegistry.GetContext().GetObject("WX_MessageDao") as IWX_MessageDao;
        public static IWX_OpenIDDao _IWX_OpenIDDao = ContextRegistry.GetContext().GetObject("WX_OpenIDDao") as IWX_OpenIDDao;
        public static ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        public static IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;
        public static IWx_FTUserbdopenIdinfoDao _IWx_FTUserbdopenIdinfoDao = ContextRegistry.GetContext().GetObject("Wx_FTUserbdopenIdinfoDao") as IWx_FTUserbdopenIdinfoDao;
        //生产计划
        public static IFlow_PlanProductioninfoDao _IFlow_PlanProductioninfoDao = ContextRegistry.GetContext().GetObject("Flow_PlanProductioninfoDao") as IFlow_PlanProductioninfoDao;
        //非标电控箱生产
        public static IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        public static IDKX_DDCLyqNoteInfoDao _IDKX_DDCLyqNoteInfoDao = ContextRegistry.GetContext().GetObject("DKX_DDCLyqNoteInfoDao") as IDKX_DDCLyqNoteInfoDao;
        public static IDKX_GCSinfoDao _IDKX_GCSinfoDao = ContextRegistry.GetContext().GetObject("DKX_GCSinfoDao") as IDKX_GCSinfoDao;
        public static IDKX_DDtypeinfoDao _IDKX_DDtypeinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDtypeinfoDao") as IDKX_DDtypeinfoDao;

        public static IDisImmuneCenterDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("DisImmuneCenterDao") as IDisImmuneCenterDao;
        public static readonly string jxmbts = WebConfigurationManager.AppSettings["jxmbts"];
        public static readonly string jxmbfs = WebConfigurationManager.AppSettings["jxmbfs"];//发货通知模版
        public static readonly string Nambscd = WebConfigurationManager.AppSettings["Nambscd"];//生产单状态提醒
        public static readonly string Nambtfh = WebConfigurationManager.AppSettings["Nambtfh"];//返退货单子状态提醒

        public static IWx_openIdinfoDao _IWx_openIdinfoDao = ContextRegistry.GetContext().GetObject("Wx_openIdinfoDao") as IWx_openIdinfoDao;

        public static IYCnoticeinfoDao _IYCnoticeinfoDao = ContextRegistry.GetContext().GetObject("YCnoticeinfoDao") as IYCnoticeinfoDao;


        /// <summary>
        /// 获取缩略图的ID
        /// </summary>
        /// <param name="Id">图文的Id</param>
        /// <param name="filpath">程序存放的物理路径</param>
        /// <returns></returns>
        
        #region //获取图文缩略图Id
        public static string GetDataForJs(string Id, string filpath)
        {
            StringBuilder builder = new StringBuilder();
            WX_Message Mmodel = new WX_Message();
            List<WX_Message_News> list = new List<WX_Message_News>();
            Mmodel = _IWX_MessageDao.WX_GetModelById(Id);
            list = Mmodel.wx_Message_News.ToList();
            list = list.FindAll(x => x.PicUrl != null);
            if (list != null && list.Count != 0)
            {
                builder.Append("{\"articles\":[");
                foreach (WX_Message_News messagenews in list)
                {
                    int num = 1;
                    string path = filpath + messagenews.PicUrl;
                    builder.Append("{\"thumb_media_id\":\"" + MenuContext.Getthumb_media_id(path) + "\",");
                    builder.Append("\"author\":\"" + "苏州新亚" + "\",");
                    builder.Append("\"title\":\"" + messagenews.Title + "\",");
                    builder.Append("\"content_source_url\":\"" + messagenews.Url + "\",");
                    builder.Append("\"content\":\"" + messagenews.MnContent + "\",");
                    builder.Append("\"digest\":\"" + messagenews.Description + "\",");
                    builder.Append("\"show_cover_pic\":\"" + "0" + "\"");
                    if (num < list.Count)
                    {
                        builder.Append("},");
                        num++;
                    }
                    else
                    {
                        builder.Append("}");
                    }
                }
                builder.Append("]}");
                return builder.ToString();
            }
            else
            {
                return null;
            }
        } 
        #endregion
       
        /// <summary>
        /// 获取图文信息的Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="filpath"></param>
        /// <returns></returns>
        
        #region //获取群发图文消息的Id +Getmedia_id(string Id, string filpath)
        public static string Getmedia_id(string Id, string filpath)
        {
            string datajs = GetDataForJs(Id, filpath);//获取缩略图的Id
            if (datajs != null)
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", MenuContext.AccessToken);
                //string menu = FileUtility.Read(Menu_Data_Path);
                String result = HttpUtility11.PostUrl(url, datajs);
                XDocument doc = XmlUtility.ParseJson(result, "root");
                XElement root = doc.Root;
                if (root != null)
                {
                    XElement media_id = root.Element("media_id");
                    if (media_id != null)
                    {
                        return media_id.Value;
                    }
                }
            }
            return null;
        }  
        #endregion
       

        //public static string S_dataxml(string Id)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("{");
        //    builder.Append("\"filter\":{");
        //    builder.Append("\"is_to_all\":\"" + "false" + "\",");
        //    builder.Append("\"group_id\":\"" + "0" + "\"},");
        //    builder.Append("\"mpnews\":{");
        //   // builder.Append("\"text\":{");
        //    builder.Append("\"media_id\":\"" + Getmedia_id(Id) + "\"},");
        //    builder.Append("\"content\":\"" + "测试" + "\"},");
        //   // builder.Append("\"msgtype\":\"" + "mpnews" + "\"");
        //    builder.Append("\"msgtype\":\"" + "text" + "\"");
        //    builder.Append("}");
        //    return builder.ToString();
        //}

        #region //根据Openid 群发文本消息
        public static string S_datatextxml_byOpenid(string Id, string type)
        {
            string opendID;
            if (type == "1")
            {
                opendID = GetWXopenid();
            }
            else
            {
                opendID = GetBDopenid();
            }
            WX_Message Mmodel = new WX_Message();
            Mmodel = _IWX_MessageDao.WX_GetModelById(Id);
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":[");
            builder.Append("\"" + opendID + "\"");
            builder.Append("],");
            builder.Append("\"msgtype\":\"" + "text" + "\",");
            builder.Append("\"text\":{");
            builder.Append("\"content\":\"" + Mmodel.A_Content + "\"}");
            builder.Append("}");
            return builder.ToString();
        } 
        #endregion

        #region //发送图文消息 +S_datanewsxml_byOpenid(string Id,string filpath)
        public static string S_datanewsxml_byOpenid(string Id,string filpath,string type)
        {
            string opendID;
            if (type == "1")
            {
                opendID = GetWXopenid();
            }
            else
            {
                opendID = GetBDopenid();
            }
            string media_id = Getmedia_id(Id, filpath);
            if (media_id != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                builder.Append("\"touser\":[");
                builder.Append("\"" + opendID + "\"");
                builder.Append("],");
                builder.Append("\"mpnews\":{");
                builder.Append("\"media_id\":\"" + media_id + "\"},");
                builder.Append("\"msgtype\":\"" + "mpnews" + "\"");
                builder.Append("}");
                return builder.ToString();
            }
            else
            {
                return null;
            }
        } 
        #endregion

        #region //绑定的微信群发
        public static string S_bddatanewsxml(string Id,string filpath)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":[");
            builder.Append("\"" + GetBDopenid() + "\"");
            builder.Append("],");
            builder.Append("\"mpnews\":{");
            builder.Append("\"media_id\":\"" + Getmedia_id(Id, filpath) + "\"},");
            builder.Append("\"msgtype\":\"" + "mpnews" + "\"");
            builder.Append("}");
            return builder.ToString();
        }
        #endregion

        #region //绑定的微信群发根据OpenId群发消息
        public static string S_MassbyOpendid(string Id,string filpath)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", MenuContext.AccessToken);
            string data = S_bddatanewsxml(Id,filpath);
            String result = HttpUtility11.PostUrl(url, data);
            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement errcode = root.Element("errcode");
                if (errcode != null)
                {
                    return errcode.Value;
                }
            }
            return null;
        } 
        #endregion


        #region //所有关注的微信群发
        public static string S_Mass(string Id, string filpath, string type,string fstype)
        {
            string data;
            if (fstype == "text")
            {
                data = S_datatextxml_byOpenid(Id, type);
            }
            else
            {
                data = S_datanewsxml_byOpenid(Id, filpath, type);
            }
            string I="1";
            if (data != null)
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", MenuContext.AccessToken);
                // string data = S_datanewsxml_byOpenid(Id,filpath);
                String result = HttpUtility11.PostUrl(url, data);
                XDocument doc = XmlUtility.ParseJson(result, "root");
                XElement root = doc.Root;
                if (root != null)
                {
                    XElement errcode = root.Element("errcode");
                    if (errcode != null)
                    {
                        if (errcode.Value=="0")
                        {
                          I="0"; 
                        }
                        return I;
                    }
                }
            }
            return I;
        } 
        #endregion


        #region //返回所有的关注微信的OPENID
        public static string GetWXopenid()
        {
            string allOpenid = MenuContext.GetAllOpenId();
            return allOpenid;
        } 
        #endregion

        #region //返回所有绑定的关注微信openid string格式的
        public static string GetBDopenid()
        {
            string bdopenid = _IWX_OpenIDDao.GetbdOpenId();
            return bdopenid;
        } 
        #endregion

        #region //返回所有绑定微信List +GetNdopenidlist()
        public static IList<WX_OpenIDView> GetNdopenidlist()
        {
            IList<WX_OpenIDView> list = _IWX_OpenIDDao.NGetList();
            return list;
        } 
        #endregion

        #region //获取模版Id JSON
        public static string Mb_Gettemplate_idjson()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"template_id_short\":\"" + "TM00015" + "\"");
            builder.Append("}");
            return builder.ToString();
        } 
        #endregion


        #region //返回模版Id
        public static string Mb_Gettemplate_id()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}", MenuContext.AccessToken);
            string data = Mb_Gettemplate_idjson();
            String result = HttpUtility11.PostUrl(url, data);
            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement template_id = root.Element("template_id");
                if (template_id != null)
                {
                    return template_id.Value;
                }
            }
            return null;
        } 
        #endregion

        #region //拼接模版绩效推送的json
        public static string Mb_S_massjson(string openid,string Id)
        {
            WX_Message_NewsView model = new  WX_Message_NewsView();
          //  list = _IWX_Message_NewsDao.GetWX_Message_newby_Nid(Id).ToList();
            model = _IWX_Message_NewsDao.GetWX_Message_newby_type(1);
           // list = list.FindAll(x => x.Url != null);
            if (model!=null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                builder.Append("\"touser\":\"" + openid + "\",");
                //builder.Append("\"template_id\":\"" + Mb_Gettemplate_id() + "\",");ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY
                builder.Append("\"template_id\":\"" + jxmbts + "\",");
                builder.Append("\"url\":\"" + model.Url + "\",");
                builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
                builder.Append("\"data\":{");
                builder.Append("\"first\":{");
                builder.Append("\"value\":\"" + model.Title + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword1\":{");
                builder.Append("\"value\":\"" +"疾控中心"+ "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword2\":{");
                builder.Append("\"value\":\"" + DateTime.Now + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"remark\":{");
                builder.Append("\"value\":\"" + model.Description + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("}");
                builder.Append("}");
                builder.Append("}");
                return builder.ToString();
            }
            return null;
        } 
        #endregion



        #region //模板绩效考核发送
        public static string Mb_S_mass(string Id)
        {
            IList<WX_OpenIDView> list = GetNdopenidlist();
            string I="1";
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string openid = list[i].OpenId;
                    string data = Mb_S_massjson(openid, Id);
                    if (data != null)
                    {
                        string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                        String result = HttpUtility11.PostUrl(url, data);
                        XDocument doc = XmlUtility.ParseJson(result, "root");
                        XElement root = doc.Root;
                        if (root != null)
                        {
                            XElement errcode = root.Element("errcode");
                            if (errcode != null)
                            {
                                if (errcode.Value == "0")
                                {
                                    I = "0";
                                }
                                else
                                {
                                    I = "1";
                                }
                            }
                        }
                    }
                }
            }
            return I;
     
        } 
        #endregion

        /// <summary>
        /// 拼接各个免疫点未处理儿童数量
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public static string Mb_S_massIcjson(string openid,string Pid)
        {
            SysPerson pmodel = _ISysPersonDao.GetDModelbyId(Pid);//通过用户ID查找用户信息
            List<SysRole> listR = pmodel.Role.ToList();
        
            string icname="疾控中心";//免疫点名称
            string count = "0人";//未处理的人数
             listR = listR.FindAll(x => x.RoleType != 0);
                if (listR.Count !=0)
                {
                    DisImmuneCenterView icmodel = _IDisImmuneCenterDao.NGetModelById(pmodel.DisImmuneCenter);//通过免疫点ID查找免疫点信息
                   // IList<DisNoFloatingChild> NFmodel = _IDisNoFloatingChildDao.GetNof_byIcid(pmodel.DisImmuneCenter);//查找该点全部未处理儿童
                    //count = Convert.ToString(_IDisNoFloatingChildDao.Getcountnofby_Icid(pmodel.DisImmuneCenter)) + "人";
                    if (icmodel != null)
                    {
                        icname = icmodel.Name;
                    }
                }
                else
                {
                   // IList<DisNoFloatingChild> AllNfmodel = _IDisNoFloatingChildDao.GetAllNof();//查找全部未处理儿童
                    //count = Convert.ToString(_IDisNoFloatingChildDao.GetcountallNof()) + "人";
                }

            if (pmodel != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                builder.Append("\"touser\":\"" + openid + "\",");
                builder.Append("\"template_id\":\"" + jxmbfs + "\",");
                builder.Append("\"url\":\"" + "" + "\",");
                builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
                builder.Append("\"data\":{");
                builder.Append("\"first\":{");
                builder.Append("\"value\":\"" + "当前未处理人数统计" + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword1\":{");
                builder.Append("\"value\":\"" + icname + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword2\":{");
                builder.Append("\"value\":\"" + count + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword3\":{");
                builder.Append("\"value\":\"" + DateTime.Now + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"remark\":{");
                builder.Append("\"value\":\"" + "请登录系统尽快处理 ！" + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("}");
                builder.Append("}");
                builder.Append("}");
                return builder.ToString();
            }
            return null;

        }


        #region //模板各个免疫点未免疫儿童处理数量发送
        public static string Mb_S_Icmass()
        {
            IList<WX_OpenIDView> list = GetNdopenidlist();
            string I = "发送失败！";
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string openid = list[i].OpenId;
                    string pId = list[i].Person_Id;
                    string data = Mb_S_massIcjson(openid, pId);
                    if (openid != null)
                    {
                        if (data != null)
                        {
                           string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                            // string data = Mb_S_massjson(openid, Id);
                            String result = HttpUtility11.PostUrl(url, data);
                            XDocument doc = XmlUtility.ParseJson(result, "root");
                            XElement root = doc.Root;
                            if (root != null)
                            {
                                XElement errmsg = root.Element("errmsg");
                                if (errmsg != null)
                                {
                                    if (errmsg.Value == "ok")
                                    {
                                        I = "发送成功！";
                                    }
                                    else
                                    {
                                        I = "发送失败！";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return I;

        }
        #endregion


        /// <summary>
        /// 拼接发货通知模板
        /// </summary>
        /// <param name="CusId">客户Id</param>
        /// <returns></returns>
        public static string Mb_DeliveryNotice(string openid,string Ep_JlId)
        {
            EP_jlinfoView EP_jlmodel = _IEP_jlinfoDao.NGetModelById(Ep_JlId);
            string l_Company;//物流公司
            string O_Numbers;//单号
            string Number;//数量
            string F_Date;//发货时间

            if (EP_jlmodel != null)
            {
                if (EP_jlmodel.Kdgs == "zt")
                {
                    l_Company = "中通快递";
                }
                else if (EP_jlmodel.Kdgs == "tdhy")
                {
                    l_Company = "天地华宇";
                }
                else if (EP_jlmodel.Kdgs == "sf")
                {
                    l_Company = "顺丰快递";
                }
                else if (EP_jlmodel.Kdgs == "db")
                {
                    l_Company = "德邦快递";
                }
                else if (EP_jlmodel.Kdgs == "dbwl")
                {
                    l_Company = "德邦物流";
                }
                else if (EP_jlmodel.Kdgs == "sh")
                {
                    l_Company = "盛辉物流";
                }
                else if (EP_jlmodel.Kdgs == "ycky")
                {
                    l_Company = "远成快运";
                }
                else if (EP_jlmodel.Kdgs == "JJ")
                {
                    l_Company = "佳吉物流";
                }
                else if (EP_jlmodel.Kdgs == "ztky")
                {
                    l_Company = "中通快运";
                }
                else
                {
                    l_Company = "";
                }
                O_Numbers = EP_jlmodel.Kd_no;//快递单号
                Number = EP_jlmodel.DHno;//发货数量
                F_Date = EP_jlmodel.Jjdatetime.ToString("yyyy年MM月dd日");
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                builder.Append("\"touser\":\"" + openid + "\",");
                builder.Append("\"template_id\":\"" + jxmbfs + "\",");
                builder.Append("\"url\":\"" + "" + "\",");
                builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
                builder.Append("\"data\":{");
                builder.Append("\"first\":{");
                builder.Append("\"value\":\"" + "您好，您的货物已经发出！" + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword1\":{");
                builder.Append("\"value\":\"" + l_Company + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword2\":{");
                builder.Append("\"value\":\"" + O_Numbers + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword3\":{");
                builder.Append("\"value\":\"" + Number + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword4\":{");
                builder.Append("\"value\":\"" + F_Date + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"remark\":{");
                builder.Append("\"value\":\"" + "购买电控箱，请登录新亚洲电商平台 ！" + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("}");
                builder.Append("}");
                builder.Append("}");
                return builder.ToString();
            }
            return null;
        }

        #region //发货通知
        public static string FMb_DeliveryNotice(string CusId, string Ep_JlId)
        {
            IList<WX_OpenIDView> OpenIdlist = _IWX_OpenIDDao.GetCount_byP_Id(CusId,"0");
            string openId;
            string I = "发送失败！";
            if (OpenIdlist != null && OpenIdlist.Count != 0)
            {
                openId = OpenIdlist[0].OpenId;
                string data = Mb_DeliveryNotice(openId, Ep_JlId);
                if (data != null)
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                    String result = HttpUtility11.PostUrl(url, data);
                    XDocument doc = XmlUtility.ParseJson(result, "root");
                    XElement root = doc.Root;
                    if (root != null)
                    {
                        XElement errmsg = root.Element("errmsg");
                        if (errmsg != null)
                        {
                            if (errmsg.Value == "ok")
                            {
                                I = "发送成功！";
                            }
                            else
                            {
                                I = "发送失败！";
                            }
                        }
                    }
                }

            }
            return I;
        } 
        #endregion

        /// <summary>
        /// 拼接生产单状态提醒模版
        /// </summary>
        /// <param name="opendid"></param>
        /// <param name="PPId"></param>
        /// <returns></returns>
        public static string Mb_ProductionOrderNotice(string openid, string PPId)
        {
            Flow_PlanProductioninfoView model = _IFlow_PlanProductioninfoDao.NGetModelById(PPId);
            string Cpname;//产品名称
            string xd_sum;//下单数量
            string xd_type;// 0 常规  1非标销售  2非标工程
            //string xd_name;//下单人
            string dq_type;//当前状态 0 生产中 1缺料 2 待生产 3 完成  4 技术待确认 
            string dateNew;//当前时间
            Cpname = model.P_CPname;
            xd_sum = model.P_SCnumber.ToString();
            if (model.P_type == 0) {
                xd_type = "常规产品";
            }
            else if (model.P_type == 1) {
                xd_type = "非标销售";
            }
            else if(model.P_type==2){
                xd_type = "非标工程";
            }
            else{
                xd_type = "试产";
            }
            //xd_name = _ISysPersonDao.GetDModelbyId(model.C_name).UserName;
            if (model.P_Issc == 0)
            {
                dq_type = "生产中";
            }
            else if(model.P_Issc==1)
            {
                dq_type = "缺料";
            }
            else if (model.P_Issc == 2)
            {
                dq_type = "待生产";
            }
            else if (model.P_Issc == 3)
            {
                dq_type = "完成";
            }
            else {

                dq_type = "待技术完成";
            }
            dateNew =DateTime.Now.ToString("yyyy年MM月dd日");
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":\"" + openid + "\",");
            builder.Append("\"template_id\":\"" + Nambscd + "\",");
            builder.Append("\"url\":\"" + "" + "\",");
            builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
            builder.Append("\"data\":{");
            builder.Append("\"first\":{");
            builder.Append("\"value\":\"" + "你好，生产通知单状态已经改变!" + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword1\":{");
            builder.Append("\"value\":\"" + Cpname + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword2\":{");
            builder.Append("\"value\":\"" + xd_sum + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword3\":{");
            builder.Append("\"value\":\"" + xd_type + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword4\":{");
            builder.Append("\"value\":\"" + dq_type + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword5\":{");
            builder.Append("\"value\":\"" + dateNew + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"remark\":{");
            builder.Append("\"value\":\"" + "感谢，关注新亚洲控制微信公众号！" + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("}");
            builder.Append("}");
            builder.Append("}");
            return builder.ToString();
        }

        public static string FMb_ProductionOrderNotice(string PPId) {
            IList<WX_OpenIDView> OpenIdlist = _IWX_OpenIDDao.GetNBopenid();
            string openId;
            string I = "发送失败！";
            if (OpenIdlist != null && OpenIdlist.Count != 0)
            {
                //openId = OpenIdlist[0].OpenId;
                for (int i = 0,j=OpenIdlist.Count; i < j; i++)
                {
                 openId = OpenIdlist[i].OpenId;
                string data = Mb_ProductionOrderNotice(openId, PPId);
                if (data != null)
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                    String result = HttpUtility11.PostUrl(url, data);
                    XDocument doc = XmlUtility.ParseJson(result, "root");
                    XElement root = doc.Root;
                    if (root != null)
                    {
                        XElement errmsg = root.Element("errmsg");
                        if (errmsg != null)
                        {
                            if (errmsg.Value == "ok")
                            {
                                I = "发送成功！";
                            }
                            else
                            {
                                I = "发送失败！";
                            }
                        }
                    }
                }

                }
            }
            return I;
        }
        
        public static void LoginSession()
        {
            SessionUser user = HttpContext.Current.Session[SessionHelper.User] as SessionUser;
            if (user != null)
            {
                _ISysPersonDao.loginsession(user);
            }
        }

        #region //返退货提醒 微信通知

        //退货单处理状态提醒发送
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmtype">发送的部分处理人员</param>
        /// <param name="R_Id"></param>
        /// <returns></returns>
        public static string FMB_FTtypeupdateNotice(string bmtype, string FT_order, string L_type)
        {
           //查询需要发送的人
            IList<Wx_FTUserbdopenIdinfoView> modelopenId = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype(bmtype);
            string I = "发送失败！";
            string openId;
            string data="";
            if (modelopenId != null)
            {
                for (int i = 0, j = modelopenId.Count(); i < j; i++)
                {
                    string zt = "1";
                    if (L_type == "0")
                    {
                        zt = "未处理";
                    }
                    if (L_type == "1")
                    {
                        zt = "待确定";
                    }
                    if (L_type == "2")
                    {
                        zt = "待维修";
                    }
                    if (L_type == "7")
                    {
                        zt = "配件待修";
                    }
                    if (L_type == "3")
                    {
                        zt = "待定责";
                    }
                    if (L_type == "4")
                    {
                        zt = "待处理";
                    }
                    if (L_type == "5")
                    {
                        zt = "待审核";
                    }
                    if (L_type == "6")
                    {
                        zt = "已完成";
                    }
                    openId = modelopenId[i].OpenId;
                    data = Mb_FTtypeupdateNotice(openId, FT_order, zt);
                    if (data != null)
                    {
                        string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                        String result = HttpUtility11.PostUrl(url, data);
                        XDocument doc = XmlUtility.ParseJson(result, "root");
                        XElement root = doc.Root;
                        if (root != null)
                        {
                            XElement errmsg = root.Element("errmsg");
                            if (errmsg != null)
                            {
                                if (errmsg.Value == "ok")
                                {
                                    I = "发送成功！";
                                }
                                else
                                {
                                    I = "发送失败！";
                                }
                            }
                        }
                    }
                }
                
            }
            return I;
        }

        //发送的模板
        public static string Mb_FTtypeupdateNotice(string openid, string FT_order, string L_type)
        {
            string dateNew;//当前时间
            dateNew = DateTime.Now.ToString("yyyy年MM月dd日");
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":\"" + openid + "\",");
            builder.Append("\"template_id\":\"" + Nambtfh + "\",");//模版Id
            builder.Append("\"url\":\"" + "" + "\",");
            builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
            builder.Append("\"data\":{");
            builder.Append("\"first\":{");
            builder.Append("\"value\":\"" + "你好,返退货状态已变更!" + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword1\":{");
            builder.Append("\"value\":\"" + FT_order + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword2\":{");
            builder.Append("\"value\":\"" + L_type + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword3\":{");
            builder.Append("\"value\":\"" + dateNew + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"remark\":{");
            builder.Append("\"value\":\"" + "感谢，关注新亚洲控制微信公众号！" + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("}");
            builder.Append("}");
            builder.Append("}");
            return builder.ToString();
        }
        #endregion

        #region //非标电控箱生产提醒 微信通知
        #region //发送
        public static string FMB_FBDKXNotice(string FSId, string ddId, string type)
        {
            //查询需要发送的人
            IList<Wx_FTUserbdopenIdinfoView> modelopenId = _IWx_FTUserbdopenIdinfoDao.GetwxopenIdbybdzhuserId(FSId);
            string I = "发送失败！";
            string openId;
            string data = "";
            if (modelopenId != null)
            {
                for (int i = 0,j=modelopenId.Count(); i < j; i++)
                {
                    openId = modelopenId[i].OpenId;
                    data = Mb_FBDKXTXNotice(openId,ddId,type);
                    if (data != null)
                    {
                        string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                        String result = HttpUtility11.PostUrl(url, data);
                        XDocument doc = XmlUtility.ParseJson(result, "root");
                        XElement root = doc.Root;
                        if (root != null)
                        {
                            XElement errmsg = root.Element("errmsg");
                            if (errmsg != null)
                            {
                                if (errmsg.Value == "ok")
                                {
                                    I = "发送成功！";
                                }
                                else
                                {
                                    I = "发送失败！";
                                }
                            }
                        }
                    }
                }
            }
            return I;
        }
        #endregion

        #region //通知单数据拼接
        /// <summary>
        /// 非标电控柜生产的微信通知模版
        /// </summary>
        /// <param name="openid">微信的OPENID</param>
        /// <param name="Id">订单Id</param>
        /// <param name="type">发送的类型 0 客服下单 1-1 工程师接单  1 工程师上传箱体图 2 工程师完成制图 3 生产退回工程 4 生产接单缺料 5生产接单 生产中 6 完成验收 7 料齐可生产  8 资料审核 </param>
        /// <returns></returns>
        public static string Mb_FBDKXTXNotice(string openid, string Id, string type)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            string cpname;//产品名称
            string gl;//功率
            string dw;//功率单位
            string xd_sum;//下单数量
            string xd_type="";
            string dd_zt="";//当前状态
            string dateNew;
            string isft="";//是否分体
            string bicon = "";//标题内容
            xd_sum = model.NUM.ToString();
            if (type == "0")//客服下单通知工程师
            {
                bicon = "工程师您好！,有新的订单需要制图,请尽快处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "1-1")
            {
                bicon = "您好,工程师已经接单！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "1")
            {
                bicon = "工程师已经上传了箱体图纸！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "2")
            {
                bicon = "工程师已经完成图纸绘制！订单号:" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "3")
            {
                bicon = "图纸被退回！原因：" + model.scfh + "。 订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "4")//缺料
            {
                string xtqr = "箱体未确认";
                string qtre = "其他未确认";
                if (model.xtIsq == 1)
                {
                    xtqr = "箱体缺";
                }
                if (model.xtIsq == 2)
                {
                    xtqr = "箱体齐";
                }
                if (model.qtIsq == 1)
                {
                    qtre = "其他物料缺";
                }
                if (model.qtIsq == 2)
                {
                    qtre = "其他物料齐";
                }
                bicon = "生产已查库存！-订单缺料(" + xtqr + "/" + qtre + ")。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "5")
            {
                bicon = "生产已经接单！-订单生产中。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "6")
            {
                bicon = "完成验收-待品审。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "7")
            {
                bicon = "物料已齐可以生产。订单号："+model.DD_Bianhao+";客户："+model.KHname;
            }
            if (type == "8")
            {
                bicon = "工程师已经上传资料,需要审核。请尽快处理。订单号："+model.DD_Bianhao+";客户："+model.KHname;
            }
            if (type == "9")
            {
                bicon = "工程师已经上传其他图纸,需要审核。请尽快处理。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "10")
            {
                bicon = "主管工程师已审核电器排布图。请尽快查看,进行下步操作。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "11")
            {
                bicon = "主管工程师已审核其他图纸。请尽快查看,进行下步操作。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "12")
            {
                bicon = "客服你好！工程师把订单号："+model.DD_Bianhao+"的订单退回。原因："+model.gcsfh;
            }
            if (type == "13")
            {
                bicon = "完成品审-可以发货。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "14")
            {
                bicon = "品保审核不通过-请重新生产重新验收。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "15")
            {
                bicon = "资料被审核退回(" + model.dqshmsg + ")。请尽快查看,进行下步操作。订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            gl = model.POWER;
            dw = model.dw;
            cpname = model.DD_DHType + "/" + gl + "/" + dw;
             //类型返回
    
            DKX_DDtypeinfoView tylemodel = _IDKX_DDtypeinfoDao.Getdkxtypebytype(model.DD_Type.ToString());
            if (tylemodel != null)
            {
                xd_type = tylemodel.Name;
            }
            if (model.DD_Type == 2)
            {
                if (model.DD_WLWtype ==0)
                {
                    xd_type = xd_type + "/一体";
                }
                else
                {
                    xd_type = xd_type + "/分体";
                }
            }
            
            //if (model.DD_Type == 0)
            //    xd_type = "小系统";
            //if (model.DD_Type == 1)
            //    xd_type = "大系统";
            //if (model.DD_Type == 2)
            //{
            //    if (model.DD_WLWtype == 0)
            //    {
            //        xd_type = "物联网" + "/" + "一体";
            //    }
            //    if (model.DD_WLWtype == 1)
            //    {
            //        xd_type = "物联网" + "/" + "分体";
            //    }
            //}

            if (model.DD_ZT == -2)
                dd_zt = "待审核";
            if (model.DD_ZT == 1)
                dd_zt = "待制图";
            if (model.DD_ZT == 2)
                dd_zt = "制图中";
            if (model.DD_ZT == 3)
                dd_zt = "待发料";
            if (model.DD_ZT == 4)
                dd_zt = "可生产";
            if (model.DD_ZT == 5)
                dd_zt = "缺料";
            if (model.DD_ZT == 6)
                dd_zt = "生产中";
            if (model.DD_ZT == 7)
                dd_zt = "待发货";
            if(model.DD_ZT==8)
                dd_zt = "完成";
            dateNew = DateTime.Now.ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":\"" + openid + "\",");
            builder.Append("\"template_id\":\"" + Nambscd + "\",");
            builder.Append("\"url\":\"" + "" + "\",");
            builder.Append("\"topcolor\":\"" + "#FF8888" + "\",");
            builder.Append("\"data\":{");
            builder.Append("\"first\":{");
            builder.Append("\"value\":\"" + bicon + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword1\":{");
            builder.Append("\"value\":\"" + cpname + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword2\":{");
            builder.Append("\"value\":\"" + xd_sum + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword3\":{");
            builder.Append("\"value\":\"" + xd_type + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword4\":{");
            builder.Append("\"value\":\"" + dd_zt + "\",");
            builder.Append("\"color\":\"" + "#ff8888" + "\"");
            builder.Append("},");
            builder.Append("\"keyword5\":{");
            builder.Append("\"value\":\"" + dateNew + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"remark\":{");
            builder.Append("\"value\":\"" + "感谢，关注新亚洲控制微信公众号！" + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("}");
            builder.Append("}");
            builder.Append("}");
            return builder.ToString();
        }
        #endregion
        #endregion

        #region //非标电控箱处理逾期提醒 微信通知
        #region 逾期通知发送
        public static string FMB_FBDKXCLYQfs(string FSId, string ddId, string type,string fstype,string fsdxtype)
        {
            //查询需要发送的人
            IList<Wx_FTUserbdopenIdinfoView> modelopenId = _IWx_FTUserbdopenIdinfoDao.GetwxopenIdbybdzhuserId(FSId);
            string I = "发送失败！";
            string openId;
            string data = "";
            if (modelopenId != null)
            {
                for (int i = 0, j = modelopenId.Count(); i < j; i++)
                {
                    openId = modelopenId[i].OpenId;
                    data = Mb_FBDKXCLYQNotice(openId, ddId, type);
                   
                    if (data != null)
                    {
                        //保存发送记录
                        DKX_DDCLyqNoteInfoView YQmodel = new DKX_DDCLyqNoteInfoView();
                        YQmodel.DD_Id = ddId;
                        YQmodel.Type = Convert.ToInt32(type);
                        YQmodel.Xml = data;
                        YQmodel.Jsname = FSId;
                        YQmodel.C_time = DateTime.Now;
                        YQmodel.fstype = Convert.ToInt32(fstype);
                        YQmodel.fsdxtype = Convert.ToInt32(fsdxtype);
                        _IDKX_DDCLyqNoteInfoDao.Ninsert(YQmodel);
                        string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                        String result = HttpUtility11.PostUrl(url, data);
                        XDocument doc = XmlUtility.ParseJson(result, "root");
                        XElement root = doc.Root;
                        if (root != null)
                        {
                            XElement errmsg = root.Element("errmsg");
                            if (errmsg != null)
                            {
                                if (errmsg.Value == "ok")
                                {
                                    I = "发送成功！";
                                }
                                else
                                {
                                    I = "发送失败！";
                                }
                            }
                        }
                    }
                }
            }
            return I;
        }
        #endregion

        #region //逾期通知单拼接
        /// <summary>
        /// 非标电控柜生产单处理逾期微信通知模版
        /// </summary>
        /// <param name="openid">OPENID</param>
        /// <param name="Id">订单Id</param>
        /// <param name="type">发送类型 0 工程师接单逾期 1 制图逾期  2 箱体库存确认逾期 3 其他物料确认逾期 4 生产接单逾期 5 生产逾期 6 发货逾期</param>
        /// <param name="fsdxtype">0主对象 1抄送 </param>
        /// <returns></returns>
        public static string Mb_FBDKXCLYQNotice(string openid, string Id, string type)
        {
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            string cpname;//产品名称
            string gl;//功率
            string dw;//功率单位
            string xd_sum;//下单数量
            string xd_type = "";
            string dd_zt = "";//当前状态
            string dateNew;
            string isft = "";//是否分体
            string bicon = "";//标题内容
            xd_sum = model.NUM.ToString();
            if (type == "0")//制图接单逾期-提醒工程师
            {
                bicon = "制图接单逾期！请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "1")//制图逾期-提醒工程师
            {
                bicon = "制图逾期！请尽快到平台上次图纸资料！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "2")//箱体库存确认逾期-提醒老陶（箱体确认）
            {
                bicon = "箱体确认逾期！请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "3")//其他物料库存确认逾期-提醒季英（其他物料确认）
            {
                bicon = "其它物料确认逾期！请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "4")//接单逾期-提醒生产
            {
                bicon = "生产接单逾期！物料已齐请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "5")//生产逾期-提醒生产
            {
                bicon = "生产逾期！请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "6")
            {
                bicon = "发货逾期！请尽快到平台处理！订单号：" + model.DD_Bianhao + ";客户：" + model.KHname;
            }
            if (type == "7")
            {
                bicon = "资料审核逾期！请尽快到平台处理。订单号："+model.DD_Bianhao+";客户："+model.KHname;
            }
            gl = model.POWER;
            dw = model.dw;
            cpname = model.DD_DHType + "/" + gl + "/" + dw;
            if (model.DD_Type == 0)
                xd_type = "小系统";
            if (model.DD_Type == 1)
                xd_type = "大系统";
            if (model.DD_Type == 2)
            {
                if (model.DD_WLWtype == 0)
                {
                    xd_type = "物联网" + "/" + "一体";
                }
                if (model.DD_WLWtype == 1)
                {
                    xd_type = "物联网" + "/" + "分体";
                }
            }

            if (model.DD_ZT == 1)
                dd_zt = "待制图";
            if (model.DD_ZT == 2)
                dd_zt = "制图中";
            if (model.DD_ZT == 3)
                dd_zt = "待生产";
            if (model.DD_ZT == 4)
                dd_zt = "可生产";
            if (model.DD_ZT == 5)
                dd_zt = "缺料";
            if (model.DD_ZT == 6)
                dd_zt = "生产中";
            if (model.DD_ZT == 7)
                dd_zt = "待发货";
            if (model.DD_ZT == 8)
                dd_zt = "完成";
            dateNew = DateTime.Now.ToString("yyyy年MM月dd日");
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":\"" + openid + "\",");
            builder.Append("\"template_id\":\"" + Nambscd + "\",");
            builder.Append("\"url\":\"" + "" + "\",");
            builder.Append("\"topcolor\":\"" + "#FF8888" + "\",");
            builder.Append("\"data\":{");
            builder.Append("\"first\":{");
            builder.Append("\"value\":\"" + bicon + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword1\":{");
            builder.Append("\"value\":\"" + cpname + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword2\":{");
            builder.Append("\"value\":\"" + xd_sum + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword3\":{");
            builder.Append("\"value\":\"" + xd_type + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"keyword4\":{");
            builder.Append("\"value\":\"" + dd_zt + "\",");
            builder.Append("\"color\":\"" + "#ff8888" + "\"");
            builder.Append("},");
            builder.Append("\"keyword5\":{");
            builder.Append("\"value\":\"" + dateNew + "\",");
            builder.Append("\"color\":\"" + "#173177" + "\"");
            builder.Append("},");
            builder.Append("\"remark\":{");
            builder.Append("\"value\":\"" + "如果已经操作,请忽略该条消息！" + "\",");
            builder.Append("\"color\":\"" + "#FF0000" + "\"");
            builder.Append("}");
            builder.Append("}");
            builder.Append("}");
            return builder.ToString();
        }
        #endregion
        #endregion


        #region //逾期提醒定时发送
        //工程师接单逾期提醒
        public static void gcsjdyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("0", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("0");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给工程师
                                DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(ddlist[i].Gcs_nameId);
                                MassManager.FMB_FBDKXCLYQfs(gscmodel.ZH_Id, ddlist[i].Id, "0", "0","0");
                                //发给客服
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name,ddlist[i].Id,"0","0","1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("0", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("0");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给工程师
                                DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(ddlist[i].Gcs_nameId);
                                MassManager.FMB_FBDKXCLYQfs(gscmodel.ZH_Id, ddlist[i].Id, "0", "1","0");
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "0", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
                
            }
        }

        //工程师制图逾期
        public static void gcsztyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("1", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("1");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给工程师
                                //发给工程师
                                DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(ddlist[i].Gcs_nameId);
                                MassManager.FMB_FBDKXCLYQfs(gscmodel.ZH_Id, ddlist[i].Id, "1", "0","0");
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "1", "0", "1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("1", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("1");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给工程师
                                //发给工程师
                                DKX_GCSinfoView gscmodel = _IDKX_GCSinfoDao.NGetModelById(ddlist[i].Gcs_nameId);
                                MassManager.FMB_FBDKXCLYQfs(gscmodel.ZH_Id, ddlist[i].Id, "1", "1","0");
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "1", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        //资料审核逾期
        public static void zlshyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                      //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("7", "0"))
                    {
                        //查询工程师资料审核数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("7");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给主管工程师
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("13");
                                if (list != null)
                                {
                                    for (int a = 0,b=list.Count;a < b; a++)
                                    {
                                         MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "7", "0","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "7", "0", "1");
                            }
                        }
                    }
                }
                if(Isfssjd=="1")
                {
                     //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("7", "1"))
                    {
                        //查询工程师资料审核数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("7");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给主管工程师
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("13");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "7", "1", "0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "7", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        //箱体库存确认逾期
        public static void xtkcqryq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("2", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("2");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "2", "0","0");
                                    
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "2", "0", "1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("2", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("2");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "2", "1","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "2", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        //其他物料确认逾期
        public static void qtwlqryq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("3", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("3");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "3", "0","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "3", "0", "1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("3", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("3");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "3", "1","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "3", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        //生产接单逾期
        public static void scjdyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("4", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("4");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "4", "0","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "4", "0", "1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("4", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("4");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "4", "1","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "4", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        //生产逾期
        public static void scyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("5", "0"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("5");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "5", "0","0");
                                    }
                                    MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "5", "0", "1");
                                }

                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("5", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("5");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "5", "1","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "5", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            { 
            }
        }

        //发货逾期
        public static void fhyq()
        {
            try
            {
                //判定时间段
                string Isfssjd = pdtime();
                if (Isfssjd == "0")//上午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("6", "0"))
                    {
                        //查询发货数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("6");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("12");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "6", "0","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "6", "0", "1");
                            }
                        }
                    }
                }
                if (Isfssjd == "1")//下午发送
                {
                    //检测是否发送过
                    if (_IDKX_DDCLyqNoteInfoDao.JCFSshujubytypeandfstypw("6", "1"))
                    {
                        //查询工程师接单逾期的数据
                        IList<DKX_DDinfoView> ddlist = _IDKX_DDinfoDao.CZYQDATAList("6");
                        if (ddlist != null)
                        {
                            for (int i = 0, j = ddlist.Count; i < j; i++)
                            {
                                //发给箱体确认生产
                                IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("12");
                                if (list != null)
                                {
                                    for (int a = 0, b = list.Count; a < b; a++)
                                    {
                                        MassManager.FMB_FBDKXCLYQfs(list[a].UserId, ddlist[i].Id, "6", "1","0");
                                    }
                                }
                                MassManager.FMB_FBDKXCLYQfs(ddlist[i].C_name, ddlist[i].Id, "6", "1", "1");
                            }
                        }
                    }
                }
            }
            catch
            { 
            }
        }

        //判定时间
        public static string pdtime()
        {
            //判定时间段
            string Todaytime = DateTime.Now.ToString("HH");
            int H = Convert.ToInt32(Todaytime);
            if (H >= 8 && H < 9)
            {
                return "0";//上午可发送
            }
            if (H >= 16 && H < 17)
            {
                return "1";//下午可发送
            }
            return "2";//不是发送时间
        }
        #endregion

        #region //远程平台告警通知模版消息

        #region //发送xml拼接
        public static string YCTZXMLpj(string openId, string Username, string consrt, string jkdname)
        {
            try
            {
                string YCTZmbId = "ejogp4Iyc9481HNQNulyoR9zoMrgzuWB72zmvIbpjpo";
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("参数3:" + openId + "/" + Username + "/" + consrt + "/" + jkdname);
                string F_Date = DateTime.Now.ToString();
                StringBuilder builder = new StringBuilder();
                string titlestr = "尊敬的客户,您的" + Username + "账户下监控点告警/通知！";
                string fsdate = DateTime.Now.ToString();
                builder.Append("{");
                builder.Append("\"touser\":\"" + openId + "\",");
                builder.Append("\"template_id\":\"" + YCTZmbId + "\",");//模版Id
                builder.Append("\"url\":\"" + "" + "\",");
                builder.Append("\"topcolor\":\"" + "#FF0000" + "\",");
                builder.Append("\"data\":{");
                builder.Append("\"first\":{");
                builder.Append("\"value\":\"" + titlestr + "\",");
                builder.Append("\"color\":\"" + "#FFC0CB" + "\"");
                builder.Append("},");
                builder.Append("\"keyword1\":{");
                builder.Append("\"value\":\"" + jkdname + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword2\":{");
                builder.Append("\"value\":\"" + Username + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword3\":{");
                builder.Append("\"value\":\"" + consrt + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"keyword4\":{");
                builder.Append("\"value\":\"" + fsdate + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("},");
                builder.Append("\"remark\":{");
                builder.Append("\"value\":\"" + "如果您已经处理,请忽略该告警/通知,感谢关注新亚洲远程监控！" + "\",");
                builder.Append("\"color\":\"" + "#173177" + "\"");
                builder.Append("}");
                builder.Append("}");
                builder.Append("}");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("拼接DATE:" + builder.ToString());
                return builder.ToString();
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("拼接错误:" + e);
                return null;
            }
        }
        #endregion

        #region //调用发送接口
        public static string YCNotice_MB(string openId, string Username, string consrt, string jkdname, string SID, string type)
        {
            string I = "1";
            try
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("参数2:" + openId + "/" + Username + "/" + consrt + "/" + jkdname + "/" + SID);
                string data = YCTZXMLpj(openId, Username, consrt, jkdname);
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("发送数据:" + data);
                if (data != null)
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", MenuContext.AccessToken);
                    String result = HttpUtility11.PostUrl(url, data);
                    XDocument doc = XmlUtility.ParseJson(result, "root");
                    XElement root = doc.Root;
                    if (root != null)
                    {
                        XElement errmsg = root.Element("errmsg");
                        if (errmsg != null)
                        {
                            if (errmsg.Value == "ok")
                            {
                                InsertYCTZDATA(openId, Username, consrt, jkdname, "0", data, SID, type);
                                I = "0";
                            }
                            else
                            {
                                InsertYCTZDATA(openId, Username, consrt, jkdname, "1", data, SID, type);
                                I = "1";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("发送错误:" + e);
            }
            return I;
        }
        #endregion

        #region //发送数据保存
        public static void InsertYCTZDATA(string openId, string Username, string consrt, string jkdname, string Isfs, string data, string SID, string type)
        {
            YCnoticeinfoView model = new YCnoticeinfoView();
            try
            {
                model.openId = openId;
                model.username = Username;
                model.Tzcon = Decode(consrt);
                model.Xml = data;
                model.SID = SID;
                model.JKDname = jkdname;
                model.Isfs = Convert.ToInt32(Isfs);
                model.C_time = DateTime.Now;
                model.type = Convert.ToInt32(type);//0 告警 1 通知
                _IYCnoticeinfoDao.Ninsert(model);
            }
            catch
            {

            }
        }

        static Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

        public static string Decode(string s)
        {
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }
        #endregion
        #endregion
    }
}