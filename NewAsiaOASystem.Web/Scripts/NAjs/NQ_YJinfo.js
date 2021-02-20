
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加元器件信息");
    else
        $("#Ptitle").text("修改元器件信息");

})

 
//保存表单
function SavaForm()
{
    var Y_Dm = $("#Y_Dm").val();
    var Y_Name = $("#Y_Name").val();
    var Y_Ggxh = $("#Y_Ggxh").val();
    if (Y_Dm == "")
    {
        alert("物料代码不为空！");
        return false;
    }
    if (Y_Name == "")
    {
        alert("物料名称不为空！");
        return false;
    }
    if (Y_Ggxh == "")
    {
        alert("物料规格不为空");
    }

    return submitForm('/NQ_YJinfo/List');
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