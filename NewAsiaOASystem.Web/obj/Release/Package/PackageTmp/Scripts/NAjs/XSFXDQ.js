
 

 
//保存表单
function SavaForm()
{
    var dqname = $("#dqname").val();
    var XSYear = $("#XSYear").val();
    var XSL = $("#XSL").val();
    var zrr = $("#zrr").val();
    if (dqname == "")
    {
        alert("区域名称不为空！");
        return false;
    }
    if (XSYear == "")
    {
        alert("销售目标年份不为空！");
        return false;
    }
    if (XSL == "")
    {
        alert("年销售目标不为空！");
        return false;
    }
    if (zrr == "")
    {
        alert("销售区域责任人不为空！");
        return false;
    }

    return submitForm('/XSFXdqinfo/List');
}





 