using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;

namespace NewAsiaOASystem.Web.example
{
    public partial class OrderInfoPay : System.Web.UI.Page
    {
        /// <summary>
        /// 调用js获取收货地址时需要传入的参数
        /// 格式：json串
        /// 包含以下字段：
        ///     appid：公众号id
        ///     scope: 填写“jsapi_address”，获得编辑地址权限
        ///     signType:签名方式，目前仅支持SHA1
        ///     addrSign: 签名，由appid、url、timestamp、noncestr、accesstoken参与签名
        ///     timeStamp：时间戳
        ///     nonceStr: 随机字符串
        /// </summary>
        public static string wxEditAddrParam { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "page load");
            string Id = Session["o_Id"].ToString();//订单Id
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================以下为错误提示===================================");
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(Id);
            if (!IsPostBack)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                try
                {
                    //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                    jsApiPay.GetOpenidAndAccessToken();
                    //////获取收货地址js函数入口参数
                    wxEditAddrParam = jsApiPay.GetEditAddressParameters();

                    ViewState["openid"] = jsApiPay.openid;

                    ViewState["o_Id"] = Id;
                    O_Id.InnerText = Id;

                    IWeChat_OrderInfoDao _IWeChat_OrderInfoDao = ContextRegistry.GetContext().GetObject("WeChat_OrderInfoDao") as IWeChat_OrderInfoDao;
                    WeChat_OrderInfoView model = _IWeChat_OrderInfoDao.NGetModelById(Id);
                    cpname.InnerText = model.Cpname;
                    zj.InnerText = model.Jiage.ToString();

                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                }
            }
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            string total_fee = zj.InnerText;
            string name = cpname.InnerText;
            if (ViewState["openid"] != null)
            {
                string openid = ViewState["openid"].ToString();
                string O_Id = ViewState["o_Id"].ToString();
                string url = "http://wx.chinanewasia.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee + "&cpname=" + name + "&O_Id=" + O_Id;
                Response.Redirect(url);
            }
            else
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");

            }
        }

        //protected void Button_Click(object sender, EventArgs e)
        //{
        //    string total_fee = zj.InnerText;
        //    string name = cpname.InnerText;
        //    if (ViewState["openid"] != null)
        //    {
        //        string openid = ViewState["openid"].ToString();
        //        string url = "http://wx.chinanewasia.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee+"&cpname="+name;
        //        Response.Redirect(url);
        //    }
        //    else
        //    {
        //        Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
               
        //    }
        //}

      
    }
}