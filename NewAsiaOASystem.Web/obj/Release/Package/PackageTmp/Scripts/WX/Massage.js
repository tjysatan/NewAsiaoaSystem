//保存表单
function SavaForm() {
    var A_KeyWord = $("#A_KeyWord").val();
    //    var passwd1 = $("#txt_CheckPassword").val();
    var A_Content = $("#A_Content").val();
    // var UserName = $("#UserName").val();
    if (A_KeyWord == "") {
        alert("关键字不为空！");
        return false;
    }

    if (A_Content == "") {
        alert("回复内容不为空！");
        return false;
    }

    return submitForm('/FirstBack/Index');
}