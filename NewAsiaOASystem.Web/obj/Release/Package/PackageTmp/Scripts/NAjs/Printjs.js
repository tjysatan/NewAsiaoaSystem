
 
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


/*根据ID查找客户信息*/
function AjaxCust_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetNACusjs",
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

function GetNewdatetimejson() {
    var sl;
    $.ajax({
        type: "POST",
        url: "../ExpressPrinting/Getdatenewjson",
        dataType: "json",
        async: false,
        success: function (reslut) {
            sl = reslut.status;
        },
        error: function (e) {
            alert(e.error);
            alert("失败！");
        }
    })
    return sl;
}

//根据收件人  查找收件人信息 （电商平台）
function AjaxAddresseeinfobyCustId(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "../NA_XSinfo/GetaddresseeInfobyCustId",
        data: { Id: Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval(reslut);
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}


 
 