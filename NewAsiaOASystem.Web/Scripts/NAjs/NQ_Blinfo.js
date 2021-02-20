
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加不良原因");
    else
        $("#Ptitle").text("修改不良原因");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();
 
    if (Name == "")
    {
        alert("不良原因不为空！");
        return false;
    }
    return submitForm('/NQ_Blinfo/List');
}

//保存表单
function SavaFormblxx() {
    var Name = $("#Name").val();

    if (Name == "") {
        alert("不良现象不为空！");
        return false;
    }

    return submitForm('/NQ_Blxxinfo/List');
}



function AjaxReason_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetblyyfjbyId",
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

/*根据ID 返回返退货原因的名称*/
function ShowReasonName(val) {
    var R_name;
    if (val == null || val == "0") {
        R_name = "一级";
    } else {
        R_name = AjaxReason_Name(val)[0].Name;
    }
    return R_name;
}