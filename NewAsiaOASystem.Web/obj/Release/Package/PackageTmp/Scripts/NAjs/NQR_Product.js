
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加产品");
    else
        $("#Ptitle").text("修改产品");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();
    var LxrName = $("#LxrName").val();
  
    if (Name == "")
    {
        alert("产品名称不为空！");
        return false;
    }
    return submitForm('/NQR_Product/List');
}


//产品返退
function SavaFormP()
{
    var Pname = $("#Pname").val();
    var tl = $("#p_type").val();
    if (Pname == "")
    {
        alert("产品名称不为空！");
        return false;
    }
    if (tl == "qxz") {
        alert("请选择产品类别")
        return false;
    }
    return submitForm('/NQ_productinfo/List');
}


function Showtl(val) {
    var value;//返回的类型
    if (val == "0") {
        value = "电控箱";
    } else if (val == "1") {
        value = "温控器";
    } else if (val == "2") {
        value = "其他";
    }
    return value;
}


/*出货开单*/
function chkdSavaForm(val) {
  
    var type = val;
    console.log('val', type)
    var arr = GetGridSelete();
    $("#cp").val(arr[0]);
    if (arr.length > 1) {
        alert("只能选择一个产品！");
        return false;
    }
    if (arr.length <= 0) {
        alert("请选择产品!");
        return false;
    }
    var sl = $("#sl").val();
    if (sl == "0")
    {
        alert("请填写数量！");
        return false;
    }
    var rlist_id = $("#ft_Id").val();
    var dj = $("#dj").val();
    var bz;
    if (type == "1"||type=="0") {
        bz = $("#bz").val();
    }
    if (type == "3") {
        if (dj == "0") {
            alert("请填写产品单价！");
            return false;
        }
    }
    var Baddescribe;
    if (type == "0") {
        Baddescribe = $("#txtBaddescribe").val();
    }
    if (type == "1") {
        $.ajax({
            type: "POST",
            url: "/NQ_CHdetailinfo/Edit",
            ContentType: "application/json;charset=utf-8;",
            data: { "cpID": arr[0], "SL": sl, "R_Id": rlist_id, "bz": bz },
            success: function (context) {
                if ("success" == context.result) {
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "../NAReturnList/chview?Id=" + rlist_id;
                    });
                }
            }
        })
    } else if (type == "0") {
        $.ajax({
            type: "POST",
            url: "/NQ_CHdetailinfo/ftEdit",
            ContentType: "application/json;charset=utf-8;",
            data: { "cpID": arr[0], "SL": sl, "R_Id": rlist_id, "beizhu": bz, "Baddescribe": Baddescribe},
            success: function (context) {
                if ("success" == context.result) {
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "../NAReturnList/CkkdView?Id=" + rlist_id;
                    });
                }
            }
        })
    } else if (type == "2") {
        $.ajax({
            type: "POST",
            url: "/NAReturnList/AddFxEdit",
            ContentType: "application/json;charset=utf-8;",
            data: { "cpID": arr[0], "R_Id": rlist_id, "SL": sl},
            success: function (context) {
                if ("success" == context.result) {
                    $.messager.alert("操作提示", '添加成功！', 'info', function () {
                        Closeiform();
                    });
                }
            }
        })
    } else if (type == "3") {
        $.ajax({
            type: "POST",
            url: "../NA_XSinfo/XsdetailEdit",
            ContentType: "application/json;charset=utf-8;",
            data: { "cpID": arr[0], "SL": sl, "Xs_Id": rlist_id, "dj": dj },
            success: function (context) {
                if ("success" == context.result) {
                    $.messager.alert("操作提示", '添加成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父级窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "../NA_XSinfo/XSkdView?Id=" + rlist_id;
                    });
                }
            }
        })
    }
}

//直接关闭所有弹出框
function Closeiform() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}