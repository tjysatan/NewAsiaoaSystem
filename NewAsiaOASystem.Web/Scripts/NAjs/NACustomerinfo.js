
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加用户");
    else
        $("#Ptitle").text("修改用户");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();
    var LxrName = $("#LxrName").val();
    var Tel = $("#Tel").val();
    var Adress = $("#Adress").val();
    if (Name == "")
    {
        alert("客户名称不为空！");
        return false;
    }

    if (LxrName == "") {
        alert("客户联系人不为空！");
        return false;
    }

    if (Tel == "")
    {
        alert("联系方式不为空！");
        return false;
    }

    if (Adress=="") {
        alert("客户地址不为空！");
        return false;
    }
    return submitForm('/NACustomerinfo/List');
}


//动态加载下拉框值
function GetproList() {
    $.ajax({
        type: "POST",
        url: "/NA_Qyinfo/GetPqyinfojson",
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval(context);
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '" style="padding:1px;cursor:pointer;display:block;">' + data[i].Qyname + '</span>';
            }
            $("#DivContent_SelectedCLData").html(html);
            $("#DivContent_SelectedCLData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
                $('#SelectedCLData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                $('#qyname').val(s);
                var value = $(this).attr("name");
                GetproxhList(value,"0");
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

//动态下拉数组
function GetproxhList(Id,type) {
    $.ajax({
        type: "POST",
        url: "/NA_Qyinfo/GetallCqybyPid",
        data: { Pid: Id },
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval(context);
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '"   style="padding:1px;cursor:pointer;display:block;">' + data[i].Qyname + '</span>';
            }
            if (type == "0") {
                $('#SelectedZTData').combo('showPanel');//有子集的弹出弹框
            }
            $("#DivContent_SelectedZTData").html(html);
            $("#DivContent_SelectedZTData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
                $('#SelectedZTData').combo('setValue', v).combo('setText', s).combo('hidePanel');
                $('#qycname').val(s);
                qxGetproxhList(s,v,"0")
            });
            //绑定下拉框值改变事件
            onChangeQX();
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

//区县动态加载
function qxGetproxhList(name,Id,type) {
    var sfname = $('#qyname').val();//省份
    var djsname = $('#qycname').val();//地级市
    $.ajax({
        type: "POST",
        url: "/NA_Qyinfo/GetallCqybyPid",
        data: { Pid: Id },
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval(context);
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Qyname + '"   style="padding:1px;cursor:pointer;display:block;">' + data[i].Qyname + '</span>';
            }
               if (type == "0") {
            $('#SelectedQXData').combo('showPanel');//有子集的弹出弹框
    }
    $("#DivContent_SelectedQXData").html(html);
    $("#DivContent_SelectedQXData  span").click(function () {
        var v = $(this).attr("name");
        var s = $(this).text();
        $('#SelectedQXData').combo('setValue', v).combo('setText', v).combo('hidePanel');
        $("#qyqxname").val(s)
       
    });
            //绑定下拉框值改变事件
            //onChangeQX();
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
    //var odata;
    //var tdata;
    //var sdata = [];
    //var strjson = provice;
    //for(var i in strjson)
    //{
    //    if (sfname.substring(0, 2) == strjson[i].name.substring(0, 2))
    //    {
    //        odata = strjson[i].city;
    //    }
    //}
    //if (odata)
    //{
    //    for (var i in odata)
    //    {
    //        if (djsname.substring(0, 2) == odata[i].name.substring(0, 2))
    //        {
    //            sdata = odata[i].districtAndCounty;
    //        }
    //    }
    //}
    //var html = "";
    //var data =eval(sdata);
    //for (var i = 0, j = sdata.length; i < j; i++) {
    //    html = html + '<span  name="' + sdata[i] + '"   style="padding:1px;cursor:pointer;display:block;">' + sdata[i] + '</span>';
    //}

    //绑定下拉框值改变事件

}



//地级市
function onChange() {
    $('#SelectedCLData').combo({
        onChange: function (n, o) {
            //清除原来选中的值
            $('#SelectedZTData').combo('setValue', "").combo('setText', "");
            $("#DivContent_SelectedZTData").html("");
            //获取选中值
            var value = $('#SelectedCLData').combo('getValue');

        }
    });
}
//区县
function onChangeQX() {
    $('#SelectedZTData').combo({
        onChange: function (n, o) {
            //清除原来选中的值
            $('#SelectedQXData').combo('setValue', "").combo('setText', "");
            $("#DivContent_SelectedQXData").html("");
            //获取选中值
            var value = $('#SelectedZTData').combo('getValue');

        }
    });
}



//选中已编辑的下拉框
function SelectItem() {
    var qyId = $("#qyId").val();//区域名称
    var qycid = $("#qyCId").val();
    var qxname = $("#qyqxname").val();
    selectedVal("SelectedCLData", qyId, null,null);
    selectedVal("SelectedZTData", null, qycid, null);
    selectedVal("SelectedQXData", null, null, qxname);
}

/*
设置值
Obj 操作对象
SelectedCLData 区域省1
SelectedZTData 区域市2
*/
function selectedVal(Obj, SelectedCLData, SelectedZTData, SelectedQXData) {
    var val = -1;
    if (SelectedCLData != null && SelectedCLData != "")
        val = EachDoucment(Obj, SelectedCLData,"0");
    if (val == 0)
        return;

    if (SelectedZTData != null && SelectedZTData != "")
        val = EachDoucment(Obj, SelectedZTData,"1");
    if (val == 0)
        return;

    if (SelectedQXData != null && SelectedQXData != "")
        val = EachDoucment(Obj, SelectedQXData,"2");
    if (val == 0)
        return;
 
}


function EachDoucment(Obj, val,type) {
    $("#DivContent_" + Obj + " span").each(function () {
        if ($(this).attr("name") == val) {
            state = 0;
            var s = $(this).text();
            if (type == "0")
                $("#qyname").val(s);
            if (type == "1")
                $("#qycname").val(s);
            if (type == '2')
                $("#qyqxname").val(s);
            $('#' + Obj).combo('setValue', val).combo('setText', s);
            return state;
        }
    })
    return state;
}