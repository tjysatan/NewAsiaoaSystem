using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewAsiaOASystem.Web.example
{
    public partial class JsApiPayPage : System.Web.UI.Page
    {
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public static string wxJsO_Id { get; set; }//订单Id
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "page load");
            if (!IsPostBack)
            {
                string openid = Request.QueryString["openid"];
                string total_fee = Request.QueryString["total_fee"];
                string cpname = Request.QueryString["cpname"];
                string O_Id = Request.QueryString["O_Id"];
                //检测是否给当前页面传递了相关参数
                if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    submit.Visible = false;
                    return;
                }

                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.openid = openid;//用户 OPNEID
                jsApiPay.total_fee = int.Parse(total_fee);//商品金额 
                jsApiPay.cpms = cpname;
                //JSAPI支付预处理
                try
                {
                    NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(config);
                    wxJsApiParam = jsApiPay.GetJsApiParameters(config);//获取H5调起JS API参数 
                    lab_Id.InnerText = O_Id;
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                }
                catch (Exception ex)
                {
                    log4net.LogManager.GetLogger("ApplicationInfoLog").Error("支付错误：" + ex);
                    Log.Info("支付", "错误 : " + ex);
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败11，请返回重试" + ex+"</span>");
                    submit.Visible = false;
                }
            }
        }
    }
}