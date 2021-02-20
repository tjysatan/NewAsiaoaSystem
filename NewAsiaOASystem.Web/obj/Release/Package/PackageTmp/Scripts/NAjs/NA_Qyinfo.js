
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加区域");
    else
        $("#Ptitle").text("编辑区域");

})

 
//保存表单
function SavaForm()
{
    var Qyname = $("#Qyname").val();
     if (Qyname == "")
    {
        alert("区域名称不为空！");
        return false;
    }
     return submitForm('/NA_Qyinfo/List');
}



 

 