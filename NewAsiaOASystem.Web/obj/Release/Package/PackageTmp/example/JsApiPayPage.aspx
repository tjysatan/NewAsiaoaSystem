<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsApiPayPage.aspx.cs" Inherits="NewAsiaOASystem.Web.example.JsApiPayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js"></script>
    <script type="text/javascript">

        //调用微信JS api 支付
        function jsApiCall() {
            WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            <%=wxJsApiParam%>,//josn串
                 function (res) {
                     if(res.err_msg == "get_brand_wcpay_request:ok" ) { 
                         var O_Id=$("#lab_Id").html();
                         location.href="../activity/PaySuccView?Id="+O_Id;
                     }  

                     else{   
                         location.href="/default.aspx?n=payment&action=error";
                     }  
                   // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                 }
             );
          }
          function callpay() {
              if (typeof WeixinJSBridge == "undefined") {
                  if (document.addEventListener) {
                      document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                  } else if (document.attachEvent) {
                      document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                      document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                  }
              }
              else {
                  jsApiCall();
              }
          }
        callpay();
    </script>
</head>
<body>
     <form id="form1" runat="server">
        <br/>
       <label runat="server" id="lab_Id" style="display:none" ></label>
	    <div align="center">
		    <br/><br/><br/>
            <asp:Button ID="submit" runat="server" Text="立即支付" OnClientClick="callpay()" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;display:none" />
	    </div>
    </form>
</body>
</html>
