$(function () {
    $('#SelectedCLData').combo({ editable: false, height: 25, panelHeight: 170 });
    $('#Div_SelectedCLData').appendTo($('#SelectedCLData').combo('panel'));
    GetDisFeedBackList('', 0);

    $('#SelectedQYData').combo({ editable: false, height: 25, panelHeight: 150 });
    $('#Div_SelectedQYData').appendTo($('#SelectedQYData').combo('panel'));

    $('#SelectedZTData').combo({ editable: false, height: 25, panelHeight: 150 });
    $('#Div_SelectedZTData').appendTo($('#SelectedZTData').combo('panel'));
    onChange();//绑定值改变事件

    var Id = $("#Id").val();

    //编辑时绑定下拉框值
    if (Id != null && Id != "") {
        SelectItem();//选中第一个下拉框值
        var SelectedCLData = $('#SelectedCLData').combo('getValue');
        //编辑时根据选中的第一个框的值，加载第二个下拉框
        if (SelectedCLData != null && SelectedCLData != "") {
            GetDisFeedBackList(SelectedCLData, 2);
            SelectItem();
            var SelectedZTDataItems = $("#DivContent_SelectedZTData span").size();
            if (SelectedZTDataItems > 0)
                $('#SelectedZTData').combo('hidePanel');

            var SelectedQYDataItems = $("#DivContent_SelectedQYData span").size();
            if (SelectedQYDataItems > 0)
                $('#SelectedQYData').combo('hidePanel');

        }

        var SelectedQYData = $('#SelectedQYData').combo('getValue');
        //根据选中的第二个下拉框值，加载第三个下拉框
        if (SelectedQYData != null && SelectedQYData != "") {
            GetDisFeedBackList(SelectedQYData, 3);
            SelectItem();
            var SelectedQYDataItems = $("#DivContent_SelectedZTData span").size();
            if (SelectedQYDataItems > 0)
                $('#SelectedZTData').combo('hidePanel');
        }
    }
})

function onChange() {
    $('#SelectedCLData').combo({
        onChange: function (n, o) {
            //清除原来选中的值
            $('#SelectedQYData').combo('setValue', "").combo('setText', "");
            $('#SelectedZTData').combo('setValue', "").combo('setText', "");
            $("#DivContent_SelectedQYData").html("");
            $("#DivContent_SelectedZTData").html("");
            SelectClick(2);
            //获取选中值
            var value = $('#SelectedCLData').combo('getValue');
            if (value == "3" || value == "4") {
                //反馈状态
                var SelectedZTDataItems = $("#DivContent_SelectedZTData span").size();
                if (SelectedZTDataItems > 0) {
                    $('#SelectedZTData').combo('showPanel');
                }
            }
            else {
                //反馈类型
                var SelectedQYDataItems = $("#DivContent_SelectedQYData span").size();
                if (SelectedQYDataItems > 0)
                    $('#SelectedQYData').combo('showPanel');
            }
        }
    });

    $('#SelectedQYData').combo({
        onChange: function (n, o) {
            //清除原来选中的值
            $('#SelectedZTData').combo('setValue', "").combo('setText', "");
            $("#DivContent_SelectedZTData").html("");
            SelectClick(3);
            //反馈状态
            var SelectedZTDataItems = $("#DivContent_SelectedZTData span").size();
            if (SelectedZTDataItems > 0) {
                $('#SelectedZTData').combo('showPanel');
            }
        }
    });
}

//值改变事件
function SelectClick(type) {
    var value = "";
    if (type == 2)
        value = $('#SelectedCLData').combo('getValue');
    else if (type == 3)
        value = $('#SelectedQYData').combo('getValue');
    GetDisFeedBackList(value, type);
}

function Save(pageindex) {
    var Id = $("#Id").val();
    var SelectedCLData = $("input[name='SelectedCLData']").val();
    var SelectedQYData = $("input[name='SelectedQYData']").val();
    var SelectedZTData = $("input[name='SelectedZTData']").val();
    if ($("#DivContent_SelectedQYData").text() != null && $("#DivContent_SelectedQYData").text() !="")
    {
        if (SelectedQYData == "" || SelectedQYData == null)
        {
            layer.alert('免疫状态不能为空！', { icon: 2, title: '操作提示' });
            return false;
        }
    }

    if ($("#DivContent_SelectedZTData").text() != null&&$("#DivContent_SelectedZTData").text() !="") {
        if (SelectedZTData == "" || SelectedZTData == null) {
            layer.alert('处理结果不为空！', { icon: 2, title: '操作提示' });
            return false;
        }
    }
 
    if (SelectedCLData == "" || SelectedCLData == null) {
        layer.alert('免疫状态不能为空！', { icon: 2, title: '操作提示' });
        return false;
    }
    SubmiNoChildForm(Id, SelectedCLData, SelectedQYData, SelectedZTData, pageindex);
}

//保存编辑结果
function SubmiNoChildForm(Id, SelectedCLData, SelectedQYData, SelectedZTData, pageindex) {
    $.ajax({
        type: "POST",
        url: "/DisNoFloatingChild/SavaDisNoFloatingChild",
        data: {
            Id: Id, SelectedCLData: SelectedCLData,
            SelectedQYData: SelectedQYData, SelectedZTData: SelectedZTData
        },
        dataType: "json",
        success: function (context) {
            if (context.result == "success") {
                layer.alert('保存成功', { icon: 1, title: '操作提示' }, function (index) {
                    var url_fh = "/DisNoFloatingChild/List" + "?pageIndex=" + pageindex;
                    //  window.parent.addTab("未入市平台流动儿童", "/DisNoFloatingChild/List");
                    window.parent.addTab("未入市平台流动儿童", url_fh);
                    CloseTab();
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.alert('保存失败请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

//动态加载下拉框值
function GetDisFeedBackList(Id, type) {
    $.ajax({
        type: "POST",
        url: "/DisNoFloatingChild/GetDisFeedBackList",
        data: { Id: Id },
        async: false,
        success: function (context) {
            var html = "";
            var data = eval(context.result);


            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '" style="padding:1px;cursor:pointer;display:block;">' + data[i].DisName + '</span>';
            }

            if (Id == null || Id == "") {
                $("#DivContent_SelectedCLData").html(html);
                floatingAltrow("DivContent_SelectedCLData");
                $('#DivContent_SelectedCLData span').click(function () {
                    var v = $(this).attr("name");
                    var s = $(this).text();
                    $('#SelectedCLData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                });
            }


            else if ((type == 2 && Id == "3") || (type == 2 && Id == "4")) {
                $("#DivContent_SelectedZTData").html(html);
                floatingAltrow("DivContent_SelectedZTData");
                $('#DivContent_SelectedZTData span').click(function () {
                    var v = $(this).attr("name");
                    var s = $(this).text();
                    $('#SelectedZTData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                });
            }

            else if (Id != "" && type == 2 && Id != "3" && Id != "4") {
                $("#DivContent_SelectedQYData").html(html);
                floatingAltrow("DivContent_SelectedQYData");
                $('#DivContent_SelectedQYData span').click(function () {
                    var v = $(this).attr("name");
                    var s = $(this).text();
                    $('#SelectedQYData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                });
            }

            else if (Id != "" && type == 3) {
                $("#DivContent_SelectedZTData").html(html);
                floatingAltrow("DivContent_SelectedZTData");
                $('#DivContent_SelectedZTData span').click(function () {
                    var v = $(this).attr("name");
                    var s = $(this).text();
                    $('#SelectedZTData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                });
            }

            //绑定下拉框值改变事件
            //onChange();
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }

    })
}

//选中已编辑的下拉框
function SelectItem() {
    //处理结果
    var DisFeedback = $("#DisFeedback").val();
    //反馈类型
    var DisArea = $("#DisArea").val();
    //反馈状态
    var DisDealWithState = $("#DisDealWithState").val();
    selectedVal("SelectedCLData", DisFeedback, DisArea, DisDealWithState);
    selectedVal("SelectedQYData", DisFeedback, DisArea, DisDealWithState);
    selectedVal("SelectedZTData", DisFeedback, DisArea, DisDealWithState);
}

/*
设置值
Obj 操作对象
SelectedCLData 处理结果
SelectedQYData 反馈类型
SelectedZTData 反馈状态

*/
function selectedVal(Obj, SelectedCLData, SelectedQYData, SelectedZTData) {
    var val = -1;
    if (SelectedCLData != null && SelectedCLData != "")
        val = EachDoucment(Obj, SelectedCLData);
    if (val == 0)
        return;

    if (SelectedQYData != null && SelectedQYData != "")
        val = EachDoucment(Obj, SelectedQYData);
    if (val == 0)
        return;

    if (SelectedZTData != null && SelectedZTData != "")
        val = EachDoucment(Obj, SelectedZTData);
    if (val == 0)
        return;
}


function EachDoucment(Obj, val) {
    var state = -1;
    $("#DivContent_" + Obj + " span").each(function () {
        if ($(this).attr("name") == val) {
            state = 0;
            var s = $(this).text();
            $('#' + Obj).combo('setValue', val).combo('setText', s);
            return state;
        }
    })
    return state;
}

/* 
    鼠标移动到表格行样式
 *@param tabID 表格名称 
 *@param oveClass 鼠标进入时的样式 
 *@param outClass 鼠标离开时的样式(为样式名称) 
 */
function floatingAltrow(tabID) {
    var rowObj = $("#" + tabID).find("span");
    $.each(rowObj, function (i, obj) {
        var tr = $(obj);
        if ("title" != tr.attr("name")) {
            tr.mousemove(function () {
                tr.addClass("TempBg");
            });
            tr.mouseout(function () {
                tr.removeClass("TempBg");
            });
        }
    });
}