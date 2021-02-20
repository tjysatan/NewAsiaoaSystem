
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加物联网电控箱产品");
    else
        $("#Ptitle").text("编辑电控箱产品信息");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();
    if (Name == "")
    {
        alert("产品名称不为空！");
        return false;
    }
     return submitForm('/WL_CPInfo/List');
}


//工程商 信息表单提交
function gcsSavaForm()
{
    return submitForm('/WL_Gcsinfo/List');
}



 

 