
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加原因");
    else
        $("#Ptitle").text("修改原因");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();
 
    if (Name == "")
    {
        alert("退货原因！");
        return false;
    }

    return submitForm('/NQR_Reason/List');
}



function AjaxReason_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetNAResasonjs",
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
        R_name = AjaxReason_Name(val).Name;
    }
    return R_name;
}