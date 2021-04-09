
function guanbi() {
    var index = parent.layer.getFrameIndex(window.name);
    //关闭弹出层
    parent.layer.close(index);
}

/*当前登录用户的信息*/
function AjaxPerson_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetPersonjson_Id",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

/*负责的工程师的信息*/
function AjaxGcsinfo(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/DKX_DDtypeinfo/AjaxGetgcsinfobyId",
        data: { "gcsId": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

function AjaxPuliceGcsinfo(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/publicAPI/AjaxGetgcsinfobyId",
        data: { "gcsId": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

//订单状态的返回// val4 电器排布图的上传审核状态  val5 电器原理图的上传审核状态 （原来其他图纸） val6 品审状态
function showddzt(val,val2,val3,val4,val5,val6)
{
    if (val == "-1")
    {
        return '<span style="color:red">未提交</span>';
    }
    
    if (val == "0")
    {
        return "已提交";
    }
    if (val == "1")
    {
        return '<span style="color:#00ff21"> 待制图</span>';
    }
    if (val == "2")
    {
        return '<span style="color:red">制图中</span>';
    }
    if (val == "-2") {
        if (val4 == "2")
        {
            return '<span style="color:red">待审核</span>';
        }
        if (val4 == "3") {
            return '<span style="color:red">资料异常</span>';
        }
        if (val5 == "2") {
            return '<span style="color:red">待审核</span>';
        }
        if (val5 == "3") {
            return '<span style="color:red">资料异常</span>';
        }
        return '<span style="color:red">待审核</span>';
       
    }
    if (val == "-3")
    {
        if (val6 == "0")
        {
            return '<span style="color:red">待品审</span>';
        }
        if (val6 == "1")
        {
            return '<span style="color:red">品审通过</span>';
        }
        if (val6 == "2") {
            return '<span style="color:red">品审异常</span>';
        }
        return '<span style="color:red">待品审</span>';
    }
    if (val == "3") {
        return "未发料";
    }
    if (val == "4") {
        return "可生产";
    }
    if (val == "5") {
        var xtstr = "未确定";
        var qtstr = "未确定";
        if (val2 == "1")
        {
            xtstr = "箱体缺";
        }
        if (val2 == "2")
        {
            xtstr = "箱体齐";
        }
        if (val3 == "1") {
            qtstr = "其他缺";
        }
        if (val3 == "2") {
            qtstr = "其他齐";
        }
        return '<span style="color:#FF9900">缺料(' + xtstr + '/' + qtstr + ')</span>';
    }
    if (val == "6") {
        return "生产中";
    }
    if (val == "7") {
        return '<span style="color:#FF9900">待发货</span>';
    }
    if (val == "8") {
        return "完成";
    }
    if (val == "9") {
        return "缺料";
    }
}

//物联网电控箱的类型
function showwlwtype(val)
{
    if (val == "0")
    {
        return "一体";
    }
    if (val == "1")
    {
        return "分体";
    }
}


//layui 数据列中时间字段的转换
function layui_dateToStr(date) {
    if (date) {

        var time = new Date(parseInt(date.replace("/Date(", "").replace(")/", ""), 10));
        var y = time.getFullYear();
        var M = time.getMonth() + 1;
        M = M < 10 ? ("0" + M) : M;
        var d = time.getDate();
        d = d < 10 ? ("0" + d) : d;
        var h = time.getHours();
        h = h < 10 ? ("0" + h) : h;
        var m = time.getMinutes();
        m = m < 10 ? ("0" + m) : m;
        var s = time.getSeconds();
        s = s < 10 ? ("0" + s) : s;
        var str = y + "-" + M + "-" + d + " " + h + ":" + m + ":" + s;
        return str;
    } else {
        return "";
    }
}