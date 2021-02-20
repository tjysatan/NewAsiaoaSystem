
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加原因");
    else
        $("#Ptitle").text("修改原因");

})

 
//保存表单
function SavaForm()
{
    var G_Dm = $("#G_Dm").val();
    var G_Name = $("#G_Name").val();
    if (G_Dm == "")
    {
        alert("供应商代码不为空！");
        return false;
    }
    if (G_Name == "")
    {
        alert("供应商名称不为空！");
        return false;
    }

    return submitForm('/Gysinfo/List');
}



 

 