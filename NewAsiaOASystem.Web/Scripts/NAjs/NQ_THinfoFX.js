//动态加载不良现象下拉框值
function GetFXblxxxlist() {
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetblxxJson",
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval('(' + context + ')');
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '"style="padding:1px;cursor:pointer;display:block;">' + data[i].Name + '</span>';
            }
            $("#DivContent_SelectedxxxData").html(html);
            $("#DivContent_SelectedxxxData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
                $('#SelectedxxxData').combo('setValue', v).combo('setText', s).combo('hidePanel');
            });
            //绑定下拉框值改变事件
            onChange();
        }, error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

//动态加载下拉框值
function GetFXxxList() {
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetBLyyJson",
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval('(' + context + ')');
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '"style="padding:1px;cursor:pointer;display:block;">' + data[i].Name + '</span>';
            }
            $("#DivContent_SelectedxxData").html(html);
            $("#DivContent_SelectedxxData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
                $('#SelectedxxData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                var value = $(this).attr("name");
                GetFXyyList(value, "0");
            });
            //绑定下拉框值改变事件
            onChange();
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }

    })
}

function GetFXyyList(Id, type) {
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetblyybyPid",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval('(' + context + ')');
            if (data != null) {
                for (var i = 0, j = data.length; i < j; i++) {
                    html = html + '<span  name="' + data[i].Id + '"  style="padding:1px;cursor:pointer;display:block;">' + data[i].Name + '</span>';
                }
                if (type == "0") {
                    $('#SelectedyyData').combo('showPanel');//有子集的弹出弹框
                }
                $("#DivContent_SelectedyyData").html(html);
                $("#DivContent_SelectedyyData  span").click(function () {
                    var v = $(this).attr("name");
                    var s = $(this).text();
                    $('#SelectedyyData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                });
            }
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

function onChange() {
    $('#SelectedxxData').combo({
        onChange: function (n, o) {
            //清除原来选中的值
            $('#SelectedyyData').combo('setValue', "").combo('setText', "");
            $("#DivContent_SelectedyyData").html("");
            //获取选中值
            var value = $('#SelectedxxData').combo('getValue');

        }
    });
}

//选中已编辑的下拉框
function SelectItemfx() {
    //不良原因1
    var TH_BLXX = $("#TH_BLXX").val();
    //不良原因2
    var TH_BLYY = $("#TH_BLYY").val();
    //不良现象
    var TH_BLXXX = $("#TH_BLXXX").val();

    selectedValfx("SelectedxxxData",TH_BLXXX,null, null);
    selectedValfx("SelectedxxData",null,TH_BLXX, null);
    selectedValfx("SelectedyyData",null,null, TH_BLYY);
}
/*
设置值
Obj 操作对象
SelectedCLData 返退原因1
SelectedZTData 返退原因2
*/
function selectedValfx(Obj,SelectedxxxData,SelectedxxData, SelectedyyData) {
    var val = -1;
    if (SelectedxxxData != null && SelectedxxxData != "")
        val = EachDoucmentfx(Obj, SelectedxxxData);
    if (val == 0)
        return;
    if (SelectedxxData != null && SelectedxxData != "")
        val = EachDoucmentfx(Obj, SelectedxxData);
    if (val == 0)
        return;
    if (SelectedyyData != null && SelectedyyData != "")
        val = EachDoucmentfx(Obj, SelectedyyData);
    if (val == 0)
        return;
}

function EachDoucmentfx(Obj, val) {
    var state = -1;
    $("#DivContent_" + Obj + " span").each(function () {
        if ($(this).attr("name") == val) {
            state = 0;
            var s = $(this).text();
            $('#'+ Obj).combo('setValue', val).combo('setText', s);
            return state;
        }
    })
    return state;
}


function fxSavaForm(type) {
    //表单验证
    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    //var ParentObj = window.parent;//获取父窗口
                    //  window.parent.$('#windowDia').window('close');
                    //  window.parent.location = "../NAReturnList/NewchejianchuliView?id=" + $("#R_Id").val();
                    Closeiform();
                });
            }
        },
      error: function (XMLHttpRequest, textStatus, errorThrown) {
        $.messager.alert("操作提示", '操作失败！', 'error');
    }
}
$('#form1').ajaxSubmit(options);
return false; //为了不刷新页面,返回false 
}


//直接关闭所有弹出框
function Closeiform() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}