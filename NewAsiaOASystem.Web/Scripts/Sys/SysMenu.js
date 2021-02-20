function formatOper(value, row, index) {
    var s = '<a href="#" onclick="editUser(' + index + ')">设置按钮</a> ';
    return s;
}

function editUser(index) {
    //设置选中行
    $('#dg').datagrid('selectRow', index);
    var row = $('#dg').datagrid('getSelected');
    if (row) {
        $("#windowDia").dialog({
            title: '设置按钮',
            href: "/SysMenu/MenuButtonPage?menuId=" + row.Id,
            modal: true,
            closed: false,
            width: 600,
            height: 500,
        });
    }
}

//选中的样式
function CheckCss(obj) {
    $(obj).css("border", "2px solid #11BA41");
    $(obj).css("width", "76px");
    $(obj).css("height", "23px");
    $(obj).css("padding-top", "3px");
    $(obj).attr("name", "1");
}
//取消选中
function CancelCss(obj) {
    $(obj).css("border", "0px");
    $(obj).css("width", "80px");
    $(obj).css("height", "25px");
    $(obj).css("padding-top", "5px");
    $(obj).attr("name", "0");
}

$("#select").click(function () {
    if ($(this).val() == "0") {
        $(".main span").each(function () {
            CheckCss(this);
        })
        $(this).val("1");
    }

    else {
        $(".main span").each(function () {
            CancelCss(this);
        })
        $(this).val("0");
    }
})

/*
    ajax表单提交
    RetUrl 返回路径
    */
function submitButton() {
    var menuid = $("#menuId").val();
    if (menuid == null || menuid == "")
        return false;
    var arr = getCheckButton();
        $.ajax({
            type: "POST",
            url: '/SysMenu/SaveMenuButton', //提交给哪个执行   
            data: { 'menuId': menuid, 'ButtonId': arr.join(",") },
            dataType: "json",
            success: function (context) {
                if ("success" == context.result) {
                    $.messager.alert("操作提示", '操作成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        if (3 == ParentObj.length) {
                            window.parent[2].$('#windowDia').window('close');
                            window.parent[2].location = "/SysMenu";
                        }
                        else if (0 == ParentObj.length) {
                            window.parent.$('#windowDia').window('close');
                            window.parent.location = "/SysMenu";
                        }
                    });
                }
                else {
                    $.messager.alert("操作提示", '提交失败！', 'error');
                }
            },
            error: function () {
                $.messager.alert("操作提示", '提交失败！', 'error');
            }
        })
}

function getCheckButton() {
    var arr = new Array();
    $(".main span").each(function () {
        if ($(this).attr("name") == "1") {
            arr.push("'" + $(this).attr("id") + "'");
        }
    })
    return arr;
}


