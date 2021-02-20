//转换成用户名
function showPersonnaem(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var json;
        json = AjaxPerson(val);
      
        return json.Name;
    }
}

//转换成免疫点名称
function showicname(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var json;

        json = AjaxIc(val);
        if (json != null) {
            return json.Name;
        }
        else {
            return "疾控中心";
        }
    }
   
}


//ajax获取绑定用户的 信息
function AjaxPerson(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetPersonName_byId",
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

//ajax获取绑定用户免疫点 信息
function AjaxIc(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetIcNameby_Id",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
           json=null
        }
    })
    return json;
}
