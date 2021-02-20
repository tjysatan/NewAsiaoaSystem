<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfoPay.aspx.cs" Inherits="NewAsiaOASystem.Web.example.OrderInfoPay" %>

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>订单支付页面</title>
    <link href="http://libs.baidu.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet">
    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js"></script>
    <script src="http://libs.baidu.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

    <script type="text/javascript">

        //获取共享地址
        function editAddress() {
            WeixinJSBridge.invoke(
                'editAddress',
                 <%=wxEditAddrParam%>,//josn串
                  function (res) {
                      var addr1 = res.proviceFirstStageName;
                      var addr2 = res.addressCitySecondStageName;
                      var addr3 = res.addressCountiesThirdStageName;
                      var addr4 = res.addressDetailInfo;
                      var tel = res.telNumber;
                      var addr = addr1 + addr2 + addr3 + addr4;
                      ajaxUpdateadd(addr,tel);
                      alert(addr + ":" + tel);
                  }
              );
        }

        $(function () {
            if (typeof WeixinJSBridge == "undefined") {
                if (document.addEventListener) {
                    document.addEventListener('WeixinJSBridgeReady', editAddress, false);
                }
                else if (document.attachEvent) {
                    document.attachEvent('WeixinJSBridgeReady', editAddress);
                    document.attachEvent('onWeixinJSBridgeReady', editAddress);
                }
            }
            else {
                editAddress();
            }
        })


        function ajaxUpdateadd(addr,tel){
            var Id=$("#O_Id").html()
                $.ajax({
                    type: "POST",
                    url: "/activity/UpdateADDR",
                    data: {Id: Id,addr:addr,Tel:tel },
                    dataType: "html",
                    async: false,
                    success: function (reslut) {
                        json =reslut;
                    },
                    error: function (e) {
                        alert("请求失败");
                    }
                })
            }
    
     
	</script>
</head>
<body>
     <form id="Form1" runat="server">
    <div style="padding-top:20px">
        <label runat="server" id="O_Id" style="display:none"></label>
     </div>
    <div class="container">
        <table class="table table-bordered">
            <tbody>
                <tr>
                    <td style="width:30%">商品：</td>
                    <td><label runat="server" id="cpname"></label></td>
                </tr>
                <tr>
                    <td style="width:30%">总价：</td>
                    <td><label runat="server" id="zj"></label></td>
                </tr>
            </tbody>
        </table>

      
        <asp:Button ID="Button" runat="server" Text="立即购买" class="btn btn-success btn-lg" OnClick="Button_Click" />
    </div>
</form>
  

</body>
</html>
