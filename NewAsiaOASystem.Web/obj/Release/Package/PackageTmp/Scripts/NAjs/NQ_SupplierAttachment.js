
function upload(val) {

    var newwin = window.open()

    newwin.location.href = val;
}

function SaveForm() {



    //表单验证
    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                layer.alert("保存成功！", { icon: 1 }, function () { parent.location.reload(); });
            }
            if ("error" == context.result) {
                layer.alert("提交失败，请重新操作！", { icon: 2 }, function () { location.reload(); });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }

    var status = $('#fstatus').val();

    if (status != 0) {
        $.messager.confirm("操作提示", '保存后需要重新审核', function (data) {
            if (data) {
                $('#form1').ajaxSubmit(options);
            }
        });
    }
    else {
        $('#form1').ajaxSubmit(options);
    }

    //$('#form1').ajaxSubmit(options);
    return false; //为了不刷新页面,返回false 
}





