//保存表单
function SavaForm() {
    var Title = $("#Title").val();
//    var passwd1 = $("#txt_CheckPassword").val();
    var Description = $("#Description").val();
   // var UserName = $("#UserName").val();
    if (Title == "") {
        alert("标题不能为空！");
        return false;
    }

    if (Description == "") {
        alert("简介不能为空！");
        return false;
    }
    if (sort == "") {
        alert("排序不能为空！");
        return false;
    }

    return submitForm('/FirstBack/Newslist');
}