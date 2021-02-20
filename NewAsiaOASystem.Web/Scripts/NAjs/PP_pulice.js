
var loadi;
function ddchen() {
    loadi = layer.load(1, { shade: [0.8, '#393D49'] })
}
//关闭等待效果
function disLoading() {
    layer.close(loadi);
}

$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加元器件信息");
    else
        $("#Ptitle").text("修改元器件信息");

})

//保存表单
function SavaForm()
{
    var Team_Name = $("#Team_Name").val();
    var Team_zrname = $("#Team_zrname").val();
    var Team_zrTel = $("#Team_zrTel").val();
    var ADListData = $("#ADListData")
    if (Team_Name == "")
    {
        alert("团队名称不为空！");
        return false;
    }
    if (Team_zrname == "")
    {
        alert("团队负责人不为空！");
        return false;
    }
    if (Team_zrTel == "")
    {
        alert("负责人的联系方式不为空！");
        return false;
    }
    if (ADListData == "")
    {
        alert("请选择管理员");
        return false;
    }
    return submitForm('/PP_Teaminfo/List');
}

//提交表单（盈利业务收入项）
function ProfitSavaForm()
{
    var Rwname = $("#Rwname").val();
    var Rwms = $("#Rwms").val();
    var Rwfz = $("#Rwfz").val();
    var Rwdw = $("#Rwdw").val();
    var ADListData = $("#ADListData").val();
    if (Rwname == "")
    {
        alert("盈利业务名称不为空！");
        return false;
    }
    if (Rwms == "")
    {
        alert("业务描述不为空！");
        return false;
    }
    if (Rwfz == "")
    {
        alert("请填写单位时间内的盈利！");
        return false;
    }
    if (isNaN(Rwfz))
    {
        alert("单位时间内的盈利,为纯数字！");
        return false;
    }
    if (Rwdw == "")
    {
        alert("请填写计数单位！");
        return false;
    }
    if (ADListData == "")
    {
        alert("请选择团队管理原因帐号！");
        return false;
    }
    return submitForm('/PP_Profitpointinfo/List');
}

//提交表单（支出项）
function ProfitzhichuSavaForm()
{
    var Rwname = $("#Rwname").val();
    var Rwms = $("#Rwms").val();
    var Rwfz = $("#Rwfz").val();
    var Rwdw = $("#Rwdw").val();
    var ADListData = $("#ADListData").val();
    if (Rwname == "") {
        alert("支出项目名称不为空！");
        return false;
    }
    if (Rwms == "") {
        alert("业务描述不为空！");
        return false;
    }
    if (Rwfz == "") {
        alert("请填写单位时间内的支出！");
        return false;
    }
    if (isNaN(Rwfz)) {
        alert("单位时间内的支出,为纯数字！");
        return false;
    }
    if (Rwdw == "") {
        alert("请填写计数单位！");
        return false;
    }
    if (ADListData == "") {
        alert("请选择团队管理原因帐号！");
        return false;
    }
    return submitForm('/PP_Profitpointinfo/zcList');
}

//提交表单（个人（员工））
function SatffSavaForm()
{
    var Sat_Name = $("#Sat_Name").val();
    var Sat_Tel = $("#Sat_Tel").val();
    alert(Sat_Tel);
    var ADListData = $("#ADListData").val();
    if (Sat_Name == "")
    {
        alert("员工姓名不为空！");
        return false;
    }
    if (Sat_Tel == "")
    {
        alert("联系方式不为空！");
        return false;
    }
    if (ADListData == "")
    {
        alert("请选择所属团队！");
        return false;
    }
    return submitForm('/PP_Staffinfo/List');
}

//ajax获取团队名称
function ajaxgetteamdata(val)
{
    var json;
    $.ajax({
        type: "POST",
        url: "/PP_Profitpointinfo/ajaxgetteamjson",
        data: { "Id": val },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval(reslut);
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

//获取该团队的收入项
function ajaxproffby(val)
{
    var json;
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/GetProfitinfojsonbystaffId",
        data:{"Id":val},
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval(result);
            $("#ButtonDg").datagrid({
                data: json,
                idField: "Id",
            });
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}

function ppbjshow(id) {
    var id = "'" + id + "'";
    var s = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="editProffAuth(' + id + ')">收入项编辑</a> ';
    var c = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="editProzhichuAuth(' + id + ' )" >支出项编辑</a> ';
    var z = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="editTTsrzcAuth(' + id + ' )" >团队收入支持编辑</a> ';
    //var t = '<a href="#" onclick="CheckTodayMx(' + id + ' )" class="btna1">支出收入明细</a>';
    return s+c+z;
}

//个人和团体收入支出项目的查看编辑跳转页面
function editTTsrzcAuth(id)
{
    //
    if (id != "")
    {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/gerenTTTProfutView?stafId=" + id + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '团体收入支出项',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//获取该团队的支出项
function ajaproffzhichubystaffId(val)
{
    var json;
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/GetProfitzhichuinfojsonbystaffId",
        data: { "Id": val },
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval(result);
            $("#ButtonDgzhichu").datagrid({
                data: json,
                idField: "Id",
            });
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}

//个人收入项编辑跳转页面
function editProffAuth(id) {
    //设置选中行
    if (id != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/ProfitManagePage?Id=" + id + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '个人收入项编辑',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//个人收入项编辑提交
function ProfitEide(val)
{
    var ProfId = GetGridSeleteID("ButtonDg").join(",");//收入业务Id
    var staffId = val;//员工Id
    $.ajax({
        url: "../PP_Staffinfo/ProfitStaffinfoEide",
        type: "POST",
        data: { StaffId: val, ProfitId: ProfId },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            if (response == "0") {
                disLoading();
                layer.alert("编辑失败！", { icon: 2 });
            }
            if (response == "1")
            {
                disLoading();
                layer.alert("编辑成功！", { icon: 1 }, function () {
                    var ParentObj = window.parent;//获取父窗口
                    window.parent.$('#windowDia').window('close');
                    window.parent.location = "/PP_Staffinfo/list";
                    return false;
                });
            }
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
    
}

//个人支出项编辑跳转页面
function editProzhichuAuth(id)
{
    //设置选中行
    if (id != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/ProfitzhichuMangePage?Id=" + id + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '个人支出项编辑',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//个人支出项编辑提交
function ProfitzhichuEide(val)
{
    var ProfId = GetGridSeleteID("ButtonDgzhichu").join(",");//收入业务Id
    var staffId = val;//员工Id
    $.ajax({
        url: "../PP_Staffinfo/ProfitStaffzhichuinfoEide",
        type: "POST",
        data: { StaffId: val, ProfitId: ProfId },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            if (response == "0") {
                disLoading();
                layer.alert("编辑失败！", { icon: 2 });
            }
            if (response == "1") {
                disLoading();
                layer.alert("编辑成功！", { icon: 1 }, function () {
                    var ParentObj = window.parent;//获取父窗口
                    window.parent.$('#windowDia').window('close');
                    window.parent.location = "/PP_Staffinfo/list";
                    return false;
                });
            }
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
}

/* 获取DataGrid选中的值(输入需要获取的DataGrid ID)*/
function GetGridSeleteId(id) {
    var arr = new Array();
    var checkedItems = $('#' + id).datagrid('getChecked'); //getSelected
    $.each(checkedItems, function (index, item) {
        arr.push("'" + item.id + "'");
    });
    return arr;
}

function GetGridSeleteID(id) {
    var arr = new Array();
    var checkedItems = $('#' + id).datagrid('getChecked'); //getSelected
    $.each(checkedItems, function (index, item) {
        arr.push("'" + item.Id + "'");
    });
    return arr;
}

//绑定选中的收入项
function GetSelectedAuth() {
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/StafttProjsonbystafttId",
        data: { staffId: $("#staffId").val() },
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            var data = eval(context);
            if (data.length > 0) {
                SeletedGrid(data)
            }
        },
        error: function () {
            $.messager.alert("操作提示", '获取权限失败！', 'error');
        }
    })
}

//绑定
function SeletedGrid(data) {
    for (var i = 0, j = data.length; i < j; i++) {
        if (data[i] != null && data[i] != undefined) {
            $('#ButtonDg').datagrid('selectRecord', data[i].ProfutId);
        }
    }
}

//绑定选中的支出项
function GetSelectedzhichu() {
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/StafttProjsonzhichubystaffId",
        data: { staffId: $("#staffId").val() },
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            var data = eval(context);
            if (data.length > 0) {
                SelectzhichuGrid(data)
            }
        },
        error: function () {
            $.messager.alert("操作提示", '获取权限失败！', 'error');
        }
    })
}

//绑定
function SelectzhichuGrid(data) {
    for (var i = 0, j = data.length; i < j; i++) {
        if (data[i] != null && data[i] != undefined) {
            $('#ButtonDgzhichu').datagrid('selectRecord', data[i].ProfutId);
        }
    }
}

//个人当天收入支出明细查看页面
function CheckTodayMx()
{
    var arr = GetGridSelete();
    if (arr.length > 1) {
        alert("只能选择一个员工！");
        return false;
    }
    if (arr.length <= 0) {
        alert("请选择一个员工再编辑!");
        return false;
    }
    var val=arr[0];
    if (val != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/TodayMxCheckView?stafId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '收入支出明细',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//查找当天的支出和收入明细数据
function AjaxshouruzhichujsonbystaffId(stafId,type)
{
    $.ajax({
        url: "../PP_Staffinfo/AjaxTodayMxjson",
        type: "POST",
        data: { stafId: stafId, type: type },
        dataType: "json",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            disLoading();
            var jsonstr = eval(response);
            if (type == "0")
                Gridshouruhtml(jsonstr);
            else
                Gridzhichuhtml(jsonstr);
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
}

//收入明细html拼接
function Gridshouruhtml(strval)
{
    var jsonStr = strval;
    $("#shouruButtonAuth").html("");
    if (jsonStr != null) {
        if (typeof (jsonStr) != "undefined") {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr>';
            html += '<td width="40%">名称</td>';
            html += '<td width="10%">数量</td>';
            html += '<td width="10%">收入</td>';
            html += '<td width="15%">收入时间</td>';
            html += '<td width="20%">操作</td>';
            html += '</tr>';

            for (var i = 0, j = jsonStr.length; i < j; i++) {
                if (i % 2 == 0)
                    html += '<tr class="h-table-content-r">';
                else
                    html += '<tr class="h-table-content-r1">';
      
                html += '<td width="40%">' + AJaxProfitbyID(jsonStr[i].ProfutId).Rwname + '</td>';
                html += '<td width="10%">' + jsonStr[i].Sum + '</td>';
                html += '<td width="10%">' + jsonStr[i].Total + '</td>';
                html += '<td width="15%">' + jsonStr[i].jl_time + '</td>';
                html += '<td width="20%">' + grczshow(jsonStr[i].Id, jsonStr[i].StafitId) + '</td>';
                html += '</tr>';
            }
            html += '</table>';
        }
    }
    else {
        var html = "";
        html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
        html += '<tr><td>当天没有任何收入项~</td></tr>';
        html += '</table>';
    }
    $("#shouruButtonAuth").html(html);
}

//支出明细html拼接
function Gridzhichuhtml(strval)
{
    var jsonStr = strval;
    $("#zhichuButtonAuth").html("");
    if (jsonStr != null) {
        if (typeof (jsonStr) != "undefined") {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr>';
            html += '<td width="60%">名称</td>';
            html += '<td width="10%">数量</td>';
            html += '<td width="10%">支出</td>';
            html += '<td width="15%">支出时间</td>';
            html += '</tr>';
            for (var i = 0, j = jsonStr.length; i < j; i++) {
                if (i % 2 == 0)
                    html += '<tr class="h-table-content-r">';
                else
                    html += '<tr class="h-table-content-r1">';
                html += '<td width="65%">' + AJaxProfitbyID(jsonStr[i].ProfutId).Rwname + '</td>';
                html += '<td width="10%">' + jsonStr[i].Sum + '</td>';
                html += '<td width="10%">' + jsonStr[i].Total + '</td>';
                html += '<td width="10%">' + jsonStr[i].jl_time + '</td>';
                html += '</tr>';
            }
            html += '</table>';
        }
    }
    else {
        var html = "";
        html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
        html += '<tr><td>当天没有任何支出项~</td></tr>';
        html += '</table>';
    }
    $("#zhichuButtonAuth").html(html);
}

//个人收入明细增加跳转
function AddshouruMxJson(val) {
    if (val != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/ShouruMXAddView?stafId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '收入明细增加',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//个人收入明细录入提交
function AddshouruEide(val)
{
    //勾选的项目
    var arr = GetGridSelete();
    if (arr.length > 1) {
        alert("只能选择一个项目！");
        return false;
    }
    if (arr.length <= 0) {
        alert("请选择项目!");
        return false;
    }
    var sl = $("#sl").val();//完成数量
    if (isNaN(sl))
    {
        alert("数量为纯数字!");
        return false;
    }
    var stafId = $("#stafId").val();
    var profId = arr[0];
    var wctime = $("#wctime").val();
    if (wctime == "")
    {
        alert("请选择完成时间!");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/shouruzhichuMXEide",
        data: { "stafId": stafId, "ProfitId": profId, "sl": sl, "wctime": wctime },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (context) {
            disLoading();
            if (context == "0") {
                if (val == "0") {//继续添加
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        window.location = "/PP_Staffinfo/ShouruMXAddView?stafId=" + stafId;
                    });
                } else {//关闭添加页面
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "/PP_Staffinfo/TodayMxCheckView?stafId=" + stafId;
                        return false;
                    });
                }
            }
            else  {
                $.messager.alert("操作提示", '提交失败！', 'error', function () {
                    window.location = "/PP_Staffinfo/ShouruMXAddView?stafId=" + stafId;
                });
            }
        }
    })
}

//个人收入支出明细操作显示
function grczshow(Id,satfId) {
    var id = "'" + Id + "'";
    var stafId = "'" + satfId + "'";
    var s = '<a href="#" onclick="geshouruzhichuMXdet(' + id + ',' + stafId + ')" class="btna1">删除</a> ';
    var t = '<a href="#" onclick="shouruzhuchuMxwctimeupdateAuth(' + id + ')" class="btna1">编辑</a> ';
    return s + t;
}

//个人收入支出明细删除
function geshouruzhichuMXdet(Id, satfId)
{
    $.ajax({
        url: "../PP_Staffinfo/shouruzhichuMXdel",
        type: "POST",
        data: { Id: Id },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            disLoading();
            if (response == "0") {
                $.messager.alert("操作提示", '删除成功！', 'info', function () {
                    window.location = "/PP_Staffinfo/TodayMxCheckView?stafId=" + satfId;
                    return false;
                });
            } else {
                layer.alert("操作失败！", { icon: 2 });
            }
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
}

//个人收入支出明细完成时间修改跳转页面
function shouruzhuchuMxwctimeupdateAuth(id) {
    if (id != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/shouruzhichuwctimeupdateView?Id=" + id + "  style='border:0px;' width='90%' height='350px'></iframe>");
        $("#windowDia").window({
            title: '收入支出明细完成时间修改',
            modal: true,
            closed: false,
            width: '80%',
            height: 350,
            top: 0,
        });
    }
}

//收入支出明细完成时间修改提交
function shouruzhichuwctimeupdateeide()
{
    var wctime = $("#wctime").val();
    var sum = $("#sum").val();
    var stafId = $("#stafId").val();
    var Id = $("#Id").val();
    if (wctime == "") {
        alert("完成时间不为空！");
        return false;
    }
    if (sum == "") {
        alert("数量不为空！");
        return false;
    }
    if (isNaN(sum)) {
        alert("数量请填写纯数字！");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/shouruzhichuwctimeupdateEide",
        data: { "Id": Id,"sum":sum,"wctime": wctime },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (context) {
            disLoading();
            if (context == "0") {

                $.messager.alert("操作提示", '编辑成功！', 'info', function () {
                    var ParentObj = window.parent;//获取父窗口
                    window.parent.$('#windowDia').window('close');
                    window.parent.location = "/PP_Staffinfo/TodayMxCheckView?stafId=" + stafId;
                    return false;
                });

            }
            else {
                $.messager.alert("操作提示", '提交失败！', 'error', function () {
                    window.location = "/PP_Staffinfo/shouruzhichuwctimeupdateView?Id=" + Id;
                });
            }
        },
    })
 
}

//团体收入项html拼接
function TTGridshouruhtml(strval)
{
    var jsonStr = strval;
    $("#TTshouruButtonAuth").html("");
    if (jsonStr != null) {
        if (typeof (jsonStr) != "undefined") {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr>';
            html += '<td width="45%">名称</td>';
            html += '<td width="10%">占比</td>';
            html += '<td width="10%">单位金额</td>';
            html += '<td width="20%">操作</td>';
            html += '</tr>';
            for (var i = 0, j = jsonStr.length; i < j; i++) {
                if (i % 2 == 0)
                    html += '<tr class="h-table-content-r">';
                else
                    html += '<tr class="h-table-content-r1">';
                var TTprofitjson = AJaxProfitbyID(jsonStr[i].ProfutId);
                html += '<td width="45%">' + TTprofitjson.Rwname + '</td>';
                html += '<td width="10%">' + jsonStr[i].Proportion + '%</td>';
                html += '<td width="10%">' + TTprofitjson.Rwfz + '</td>';
                html += '<td width="20%">' + TTcaozuoshow(jsonStr[i].StaffId,jsonStr[i].Id) + '</td>';
                html += '</tr>';
            }
            html += '</table>';
        }
    }
    else {
        var html = "";
        html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
        html += '<tr><td>该员工没有团体收入项~</td></tr>';
        html += '</table>';
    }
    $("#TTshouruButtonAuth").html(html);
}
//团体支出项html拼接
function TTGridzhichuhtml(strval)
{
    var jsonStr = strval;
    $("#TTzhichuButtonAuth").html("");
    if (jsonStr != null) {
        if (typeof (jsonStr) != "undefined") {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr>';
            html += '<td width="45%">名称</td>';
            html += '<td width="10%">占比</td>';
            html += '<td width="10%">单位金额</td>';
            html += '<td width="20%">操作</td>';
            html += '</tr>';
            for (var i = 0, j = jsonStr.length; i < j; i++) {
                if (i % 2 == 0)
                    html += '<tr class="h-table-content-r">';
                else
                    html += '<tr class="h-table-content-r1">';
                var TTprofitjson = AJaxProfitbyID(jsonStr[i].ProfutId);
                html += '<td width="45%">' + TTprofitjson.Rwname + '</td>';
                html += '<td width="10%">' + jsonStr[i].Proportion + '%</td>';
                html += '<td width="10%">' + TTprofitjson.Rwfz + '</td>';
                html += '<td width="20%">' + TTcaozuoshow(jsonStr[i].StaffId, jsonStr[i].Id) + '</td>';
                html += '</tr>';
            }
            html += '</table>';
        }
    }
    else {
        var html = "";
        html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
        html += '<tr><td>该员工没有团体支出项~</td></tr>';
        html += '</table>';
    }
    $("#TTzhichuButtonAuth").html(html);
}

//团体收入项增加跳转
function AddTTProshouru(val) {
    if (val != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/TTshourulist?stafId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '团体收入项目增加',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}
//团体支出项增加跳转
function AddTTProzhichu(val) {
    if (val != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/TTzhichulist?stafId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '团体支出项目增加',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }
}

//团体收入支出录入提交
function AddTTshouruzhichuEide(val) {
    //勾选的项目
    var arr = GetGridSelete();
    if (arr.length > 1) {
        alert("只能选择一个项目！");
        return false;
    }
    if (arr.length <= 0) {
        alert("请选择项目!");
        return false;
    }
    var zanbi = $("#zanb").val();
    if (zanbi == "")
    {
        alert("占比不为空！");
        return false;
    }
    if (isNaN(zanbi)) {
        alert("数量为纯数字!");
        return false;
    }
    var stafId = $("#stafId").val();
    var profId = arr[0];
    var type = $("#type").val();
    $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/gerenTTproEide",
        data: { "stafId": stafId, "ProfitId": profId, "zanbi": zanbi, "type": type },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (context) {
            disLoading();
            if (context == "0") {
                if (val == "0") {//继续添加
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        window.location = "/PP_Staffinfo/TTshourulist?stafId=" + stafId;
                    });
                } else {//关闭添加页面
                    $.messager.alert("操作提示", '保存成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "/PP_Staffinfo/gerenTTTProfutView?stafId=" + stafId;
                        return false;
                    });
                }
            }
            else {
                $.messager.alert("操作提示", '提交失败！', 'error', function () {
                    window.location = "/PP_Staffinfo/TTshourulist?stafId=" + stafId;
                });
            }
        },
    })
}

//团体收入支出的操作显示
function TTcaozuoshow(val, val1) {
    var id = "'" + val1 + "'";
    var stafId = "'" + val + "'";
    var s = '<a href="#" onclick="DelTTshouruzhichu(' + stafId + ',' + id + ')" class="btna1">删除</a> ';
    var t = '<a href="#" onclick="editTTProftstafzhanbiAuth(' + id + ')" class="btna1">编辑</a> ';
    return s+t;
}

//团体收入支出关系删除
///val stafId
//val Id
function DelTTshouruzhichu(val,val1)
{
    $.ajax({
        url: "../PP_Staffinfo/TTDetProfutostafId",
        type: "POST",
        data: { Id: val1 },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            disLoading();
            if (response == "0") {
                $.messager.alert("操作提示", '删除成功！', 'info', function () {
                    window.location = "/PP_Staffinfo/gerenTTTProfutView?stafId=" + val;
                    return false;
                });
            } else {
                layer.alert("操作失败！", { icon: 2 });
            }
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
}

//团体收入支持占比修改跳转页面
function editTTProftstafzhanbiAuth(id) {
    //设置选中行
    if (id != "") {
        $('#windowDia').html("<iframe src=../PP_Staffinfo/ZhanbiupdateView?Id=" + id + "  style='border:0px;' width='90%' height='350px'></iframe>");
        $("#windowDia").window({
            title: '占比修改',
            modal: true,
            closed: false,
            width: '80%',
            height: 350,
            top: 0,
        });
    }
}

//个人团体项目占比修改提交
function gerenTTzhanbiEide()
{
    var zhanbi = $("#zhanbi").val();
    var stafId = $("#stafId").val();
    var Id = $("#Id").val();
    //var zanbi = $("#zanb").val();
    if (zhanbi == "") {
        alert("占比不为空！");
        return false;
    }
    if (isNaN(zhanbi)) {
        alert("数量为纯数字!");
        return false;
    }
     $.ajax({
        type: "POST",
        url: "/PP_Staffinfo/zhanbiupdateEide",
        data: { "Id": Id, "zhanbi": zhanbi},
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (context) {
            disLoading();
            if (context == "0") {
                
                    $.messager.alert("操作提示", '编辑成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "/PP_Staffinfo/gerenTTTProfutView?stafId=" + stafId;
                        return false;
                    });
                
            }
            else {
                $.messager.alert("操作提示", '提交失败！', 'error', function () {
                    window.location = "/PP_Staffinfo/TTshourulist?stafId=" + stafId;
                });
            }
        },
    })

}


    //个人和团体收入项的数据
    function ajaxTTshourudata(val)
    {
        $.ajax({
            url: "../PP_Staffinfo/GetTTshourudataJson",
            type: "POST",
            data: { stafId: val },
            dataType: "json",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                var jsonstr = eval(response);
                TTGridshouruhtml(jsonstr);
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }
    //个人和团体支出项数据
    function ajaxTTzhichudata(val)
    {
        $.ajax({
            url: "../PP_Staffinfo/GetTTzhichudataJson",
            type: "POST",
            data: { stafId: val },
            dataType: "json",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                var jsonstr = eval(response);
                TTGridzhichuhtml(jsonstr);
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

    //根据项目的Id查找收入支出项目的数据
    function AJaxProfitbyID(val)
    {
        var str;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/shouruzhichubyId",
            data: { "Id": val },
            dataType: "json",
            async: false,
            success: function (context) {
                str = eval(context);
            },
            error: function (e) {
                alert("请求失败");
            }
        })
        return str;
    }

    //根据个人（员工）Id查找个人数据
    function AJaxstafiinfbyId(val)
    {
        var str;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/AjaxGetstafiinfo",
            data: { "Id": val },
            dataType: "json",
            async: false,
            success: function (context) {
                str = eval(context);
            },
            error: function (e) {
                alert("请求失败");
            }
        })
        return str;
    }


    //个人支出明细增加跳转
    function AddzhichuMxjson(val) {
        if (val != "") {
            $('#windowDia').html("<iframe src=../PP_Staffinfo/zhichuMxAddView?stafId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
            $("#windowDia").window({
                title: '支出明细增加',
                modal: true,
                closed: false,
                width: '90%',
                height: 500,
                top: 0,
            });
        }
    }

    //个人支出明细录入提交
    function AddzhichuEide(val) {
        //勾选的项目
        var arr = GetGridSelete();
        if (arr.length > 1) {
            alert("只能选择一个项目！");
            return false;
        }
        if (arr.length <= 0) {
            alert("请选择项目!");
            return false;
        }
        var sl = $("#sl").val();//完成数量
        if (isNaN(sl)) {
            alert("数量为纯数字!");
            return false;
        }
        var stafId = $("#stafId").val();
        var profId = arr[0];
        var wctime = $("#wctime").val();
        if (wctime == "") {
            alert("请选择完成时间!");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/shouruzhichuMXEide",
            data: { "stafId": stafId, "ProfitId": profId, "sl": sl, "wctime": wctime },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                if (context == "0") {
                    if (val == "0") {//继续添加
                        $.messager.alert("操作提示", '保存成功！', 'info', function () {
                            window.location = "/PP_Staffinfo/zhichuMxAddView?stafId=" + stafId;
                        });
                    } else {//关闭添加页面
                        $.messager.alert("操作提示", '保存成功！', 'info', function () {
                            var ParentObj = window.parent;//获取父窗口
                            window.parent.$('#windowDia').window('close');
                            window.parent.location = "/PP_Staffinfo/TodayMxCheckView?stafId=" + stafId;
                            return false;
                        });
                    }
                }

                else {
                    $.messager.alert("操作提示", '提交失败！', 'error', function () {
                        window.location = "/PP_Staffinfo/ShouruMXAddView?stafId=" + stafId;
                    });
                }
            }
        })
    }

    function YearMon() {
        //年
        function YYYYMMDDstart() {
            MonHead = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            //先给年下拉框赋内容   
            var y = new Date().getFullYear();
            for (var i = (y - 1) ; i < (y + 30) ; i++) //以今年为准，前30年，后30年   
                document.reg_testdate.YYYY.options.add(new Option(" " + i, i));

            //赋月份的下拉框   
            for (var i = 1; i < 13; i++)
                document.reg_testdate.MM.options.add(new Option(" " + i, i));

            document.reg_testdate.YYYY.value = y;
            document.reg_testdate.MM.value = new Date().getMonth() + 1;
            var n = MonHead[new Date().getMonth()];
            if (new Date().getMonth() == 1 && IsPinYear(YYYYvalue)) n++;
            //  writeDay(n); //赋日期下拉框Author:meizz   
            //  document.reg_testdate.DD.value = new Date().getDate();
        }
        if (document.attachEvent)
            window.attachEvent("onload", YYYYMMDDstart);
        else
            window.addEventListener('load', YYYYMMDDstart, false);

        function YYYYDD(str) //年发生变化时日期发生变化(主要是判断闰平年)   
        {
            var MMvalue = document.reg_testdate.MM.options[document.reg_testdate.MM.selectedIndex].value;
            if (MMvalue == "") { var e = document.reg_testdate.DD; optionsClear(e); return; }
            var n = MonHead[MMvalue - 1];
            if (MMvalue == 2 && IsPinYear(str)) n++;
            //writeDay(n)
        }

        //月
        function MMDD(str)   //月发生变化时日期联动   
        {
            var YYYYvalue = document.reg_testdate.YYYY.options[document.reg_testdate.YYYY.selectedIndex].value;
            if (YYYYvalue == "") { var e = document.reg_testdate.DD; optionsClear(e); return; }
            var n = MonHead[str - 1];
            if (str == 2 && IsPinYear(YYYYvalue)) n++;
            // writeDay(n)
        }

        function IsPinYear(year)//判断是否闰平年   
        {
            return (0 == year % 4 && (year % 100 != 0 || year % 400 == 0));
        }
    }

    //汇总个人盈利
    function HZProSumbyzhtime()
    {
        var hztype = $("#hztype").val();
        var hzYear = $("#YYYY").val();
        var hzMon = $("#MM").val();
        if (hzYear == "") {
            alert("请选择年份！");
            return false;
        }
        if (hztype == "0")//按月汇总
        {
            if (hzMon == "")
            {
                alert("按月汇总请再选择月份！");
                return false;
            }
        }
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/gerenProfitdataupdateEide",
            data: { "Year": hzYear, "Mon": hzMon, "type": hztype },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                if (context == "1") {
                    layer.alert("提交成功！", { icon: 1 });
                }
                if (context == "0") {
                    layer.alert("提交失败！", { icon: 1 });
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }


    //生产团队管理人员的管理费用汇总
    function scguanlifeiyongfenpei()
    {
        var wctime = $("#wctime").val();
        if (wctime == "") {
            alert("请选择时间！");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/SCguanlifeiyongupdate",
            data: { "wctime": wctime },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                if (context == "1") {
                    layer.alert("提交成功！", { icon: 1 });
                }
                if (context == "0") {
                    layer.alert("提交失败！", { icon: 1 });
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

    //收入类别显示
    function Showsrtype(val)
    {
       
        if (val == "1") {
            return '团队收入一';
        }
        else if (val == "0") {
            return '个人收入';
        } else {
            return "团队收入二";
        }
    }

    //支出类别显示
    function Showzctype(val) {
       
        if (val == "1") {
            return '团队支出';
        }
        else {
            return '个人支出';
        }
    }

//团体项目收支明细查看跳转
    function  CheckTTszToMonMX()
    {
        $('#windowDia').html("<iframe src='../PP_Staffinfo/TTshouruzhichucheakView'    style='border:0px;' width='100%' height='500px'></iframe>");
        $("#windowDia").window({
            title: '团体收支明细',
            modal: true,
            closed: false,
            width: '90%',
            height: 500,
            top: 0,
        });
    }

//团队当月团体项目收入html拼接
    function TTmxGridshouruhtml(strval)
    {
        var jsonstr = strval;
        $("#TTmxshouruButtonAuth").html("");
        if (jsonstr != null) {
            if (typeof (jsonstr) != "undefined") {
                var html = "";
                html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
                html += '<tr>';
                html += '<td width="40%">名称</td>';
                html += '<td width="10%">数量</td>';
                html += '<td width="10%">收入</td>';
                html += '<td width="15%">收入时间</td>';
                html += '<td width="20%">操作</td>';
                html += '</tr>';
                for (var i = 0, j = jsonstr.length; i < j; i++) {
                    if (i % 2 == 0)
                        html += '<tr class="h-table-content-r">';
                    else
                        html += '<tr class="h-table-content-r1">';
                    html += '<td width="40%">' + AJaxProfitbyID(jsonstr[i].ProfutId).Rwname + '</td>';
                    html += '<td width="10%">' + jsonstr[i].Sum + '</td>';
                    html += '<td width="10%">' + jsonstr[i].Total + '</td>';
                    html += '<td width="15%">' + jsonstr[i].jl_time + '</td>';
                    html += '<td width="20%">' + TTszmxczshow(jsonstr[i].Id) + '</td>';
                    html += '</tr>';
                }
                html += '</table>';
            }
        }
        else {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr><td>当月没有任何团体收入项~</td></tr>';
            html += '</table>';
        }
        $("#TTmxshouruButtonAuth").html(html);
    }

//团队当月团体项目支出html拼接
    function TTmxGridzhichuhtml(strval)
    {
        var jsonstr = strval;
        $("#TTmxzhichuButtonAuth").html("");
        if (jsonstr != null) {
            if (typeof (jsonstr) != "undefined") {
                var html = "";
                html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
                html += '<tr>';
                html += '<td width="40%">名称</td>';
                html += '<td width="10%">数量</td>';
                html += '<td width="10%">支出</td>';
                html += '<td width="15%">支出时间</td>';
                html += '<td width="20%">操作</td>';
                html += '</tr>';
                for (var i = 0, j = jsonstr.length; i < j; i++) {
                    if (i % 2 == 0)
                        html += '<tr class="h-table-content-r">';
                    else
                        html += '<tr class="h-table-content-r1">';
                    html += '<td width="40%">' + AJaxProfitbyID(jsonstr[i].ProfutId).Rwname + '</td>';
                    html += '<td width="10%">' + jsonstr[i].Sum + '</td>';
                    html += '<td width="10%">' + jsonstr[i].Total + '</td>';
                    html += '<td width="15%">' + jsonstr[i].jl_time + '</td>';
                    html += '<td width="20%">' + TTszmxczshow(jsonstr[i].Id)+ '</td>';
                    html += '</tr>';
                }
                html += '</table>';
            }
        }
        else {
            var html = "";
            html = '<table class="table_con" style="width: 100%; color: #000; font-size: 13px;">';
            html += '<tr><td>当月没有任何团体支出项~</td></tr>';
            html += '</table>';
        }
        $("#TTmxzhichuButtonAuth").html(html);
    }

//团队当月团体收支数据
    function ajaxTTszjsonbyteamId(teamId, type)
    {
        $.ajax({
            url: "../PP_Staffinfo/GetTTsztomonby",
            type: "POST",
            data: { teamId: teamId, type: type },
            dataType: "json",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                var jsonstr = eval(response);
                if (type == "0")
                    TTmxGridshouruhtml(jsonstr);
                else
                    TTmxGridzhichuhtml(jsonstr);
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

//团体项收入明细增加
    function AddTTshourumx(val) {
        if (val != "") {
            $('#windowDia').html("<iframe src=../PP_Staffinfo/AddTTmxshouchulist?teamId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
            $("#windowDia").window({
                title: '团体项收入明细',
                modal: true,
                closed: false,
                width: '90%',
                height: 500,
                top: 0,
            });
        }
    }
//团体支出项明细增
    function AddTTzhichumx(val)
    {
        if (val != "") {
            $('#windowDia').html("<iframe src=../PP_Staffinfo/AddTTmxzhichulist?teamId=" + val + "  style='border:0px;' width='100%' height='500px'></iframe>");
            $("#windowDia").window({
                title: '团体项支出明细',
                modal: true,
                closed: false,
                width: '90%',
                height: 500,
                top: 0,
            });
        }
    }
//团体项收支明细提交
    function TTAddshouruEide(val) {
        //勾选的项目
        var arr = GetGridSelete();
        if (arr.length > 1) {
            alert("只能选择一个项目！");
            return false;
        }
        if (arr.length <= 0) {
            alert("请选择项目!");
            return false;
        }
        var sl = $("#sl").val();//完成数量
        if (isNaN(sl)) {
            alert("数量为纯数字!");
            return false;
        }
        var teamId = $("#teamId").val();
        var profId = arr[0];
        var wctime = $("#wctime").val();
        if (wctime == "") {
            alert("请选择完成时间!");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/TTszmxaddEdit",
            data: { "teamId": teamId, "profuId": profId, "sl": sl, "wctime": wctime },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                if (context == "0") {
                    if (val == "0") {//继续添加
                        $.messager.alert("操作提示", '保存成功！', 'info', function () {
                            window.location = "/PP_Staffinfo/AddTTmxshouchulist?teamId=" + teamId;
                        });
                    } else {//关闭添加页面
                        $.messager.alert("操作提示", '保存成功！', 'info', function () {
                            var ParentObj = window.parent;//获取父窗口
                            window.parent.$('#windowDia').window('close');
                            window.parent.location = "/PP_Staffinfo/TTshouruzhichucheakView";
                            return false;
                        });
                    }
                }

                else {
                    $.messager.alert("操作提示", '提交失败！', 'error', function () {
                        window.location = "/PP_Staffinfo/AddTTmxshouchulist?teamId=" + teamId;
                    });
                }
            }
        })
    }

//团体项目收支操作明细操作显示
  function TTszmxczshow(Id) {
        var id = "'" + Id + "'";
        var s = '<a href="#" onclick="TTszmxdelbyId(' + id + ')" class="btna1">删除</a> ';
        var t = '<a href="#" onclick="TTszmxupdateAuth(' + id + ')" class="btna1">编辑</a>';
        return s+t;
    }

//团体收支删除
    function TTszmxdelbyId(Id)
    {
        $.ajax({
            url: "../PP_Staffinfo/TTszmxdel",
            type: "POST",
            data: { Id: Id },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                if (response == "0") {
                    $.messager.alert("操作提示", '删除成功！', 'info', function () {
                        window.location = "/PP_Staffinfo/TTshouruzhichucheakView";
                        return false;
                    });
                } else {
                    layer.alert("操作失败！", { icon: 2 });
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

//团体收支明细修改跳转页面
    function TTszmxupdateAuth(id)
    {
        if (id != "") {
            $('#windowDia').html("<iframe src=../PP_Staffinfo/TTszmxupdateView?Id=" + id + "  style='border:0px;' width='90%' height='350px'></iframe>");
            $("#windowDia").window({
                title: '收入支出明细完成时间修改',
                modal: true,
                closed: false,
                width: '80%',
                height: 350,
                top: 0,
            });
        }
    }

//团体收支明细修改提交
   function TTszmxupdateEide()
    {
        var wctime = $("#wctime").val();
        var sum = $("#sum").val();
        var Id = $("#Id").val();
        if (wctime == "") {
            alert("完成时间不为空！");
            return false;
        }
        if (sum == "") {
            alert("数量不为空！");
            return false;
        }
        if (isNaN(sum))
        {
            alert("数量请填写纯数字！");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/TTszmxupdateEide",
            data: { "Id": Id,"sl":sum,"wctime": wctime },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                if (context == "0") {

                    $.messager.alert("操作提示", '编辑成功！', 'info', function () {
                        var ParentObj = window.parent;//获取父窗口
                        window.parent.$('#windowDia').window('close');
                        window.parent.location = "/PP_Staffinfo/TTshouruzhichucheakView";
                        return false;
                    });

                }
                else {
                    $.messager.alert("操作提示", '提交失败！', 'error', function () {
                        window.location = "/PP_Staffinfo/shouruzhichuwctimeupdateView?Id=" + Id;
                    });
                }
            },
        })
    }


//后台个人盈利统计

//获取员工数据
    function AjaxGetygjson(startwctime, endwctime)
    {
        var json;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/yuangongjson",
            dataType: "json",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (context) {
                disLoading();
                json = eval(context);
                GRyinglishujupinjiehtml(json, startwctime, endwctime);
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        });
       
    }

//数据显示html拼接
    function GRyinglishujupinjiehtml(str,startwctime, endwctime)
    {
        var jsonStr = str;
        $("#htmlcon").html("");
        if (jsonStr != null) {
            if (typeof (jsonStr) != "undefined") {
                var ljzc = 0;
                var ljsr = 0;
                var dqsr = 0;
                var dqzc = 0;
                var html = "";
                html += '<table class="table_con" style="width:100%; color: #000; font-size: 13px; margin: auto">';
                for (var i = 0, j = jsonStr.length; i < j; i++) {
                    if (i % 2 == 0)
                        html += '<tr class="table_tr_bg1">';
                    else
                        html += '<tr>';
                   var a=Ajaxgerenleijishouru(jsonStr[i].Id);
                   ljsr = parseFloat(ljsr) + parseFloat(a);
                   var b = Ajaxgerenleijizhichu(jsonStr[i].Id);
                   ljzc = parseFloat(ljzc) + parseFloat(b);
                    var c = Ajaxsjdnshouzhijson(startwctime, endwctime, jsonStr[i].Id, "0");
                    dqsr = parseFloat(dqsr) + parseFloat(c);
                    var d = Ajaxsjdnshouzhijson(startwctime, endwctime, jsonStr[i].Id, "1");
                    dqzc = parseFloat(dqzc) + parseFloat(d);
                    html += '<td style="width:11%">' + jsonStr[i].Sat_Name + '</td>';
                    html += '<td style="width:16%">' + a + '</td>';
                    html += '<td style="width:15%">' + b + '</td>';
                    html += '<td style="width: 15%; color: red">' + c + '</td>';
                    html += '<td style="width:15%; color: red">' + d + '</td>';
                }
                html += '<tr class="table_tr_bg1">';
                html += '<td style="width:10%">合计</td>';
                html += '<td style="width:15%">' + ljsr + '</td>';
                html += '<td style="width:15%">' + ljzc + '</td>';
                html += '<td style="width:15%">' + dqsr + '</td>';
                html += '<td style="width:15%">' + dqzc + '</td>';
                html += '</tr>';
                $("#htmlcon").html(html);
            }
        }
    }

//个人累计收入
    function Ajaxgerenleijishouru(val)
    {
        var json;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/Ajaxgerenleijishouru",
            data:{"stafId":val},
            dataType: "html",
            async: false,
            success: function (context) {
                json = context;
            },
            error: function (e) {
                layer.alert("操作失败！", { icon: 2 });
            }
        })
        return json;
    }
//个人累计支出
    function Ajaxgerenleijizhichu(val)
    {
        var json;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/Ajaxgerenleijizhichu",
            data:{"stafId":val},
            dataType: "html",
            async: false,
            success: function (context) {
                json = context;
            },
            error: function (e) {
                layer.alert("操作失败！", { icon: 2 });
            }
        })
        return json;
    }

//时间段内的收支数据
    function Ajaxsjdnshouzhijson(startwctime, endwctime, stafId, type)
    {
        var json;
        $.ajax({
            type: "POST",
            url: "/PP_Staffinfo/gerenshouruzhichubywctimeandstafId",
            data: { "starttime": startwctime, "endtime": endwctime, "type": type, "stafId": stafId },
            dataType: "html",
            async: false,
            success: function (context) {
                json = context;
            },
            error: function (e) {
                layer.alert("操作失败！", { icon: 2 });
            }
        })
        return json;
    }

//个人盈利时间段查询
    function btnsever()
    {
        var starttime = $("#starttime").val();
        var endtime = $("#endtime").val();
        if (starttime == "")
        {
            alert("请选择开始时间！");
            return false;
        }
        if (endtime == "")
        {
            alert("请选择结束时间！");
            return false;
        }
        AjaxGetygjson(starttime, endtime);
    }


    var loadi;
    function ddchen() {
        loadi = layer.load(1, { shade: [0.8, '#393D49'] })
    }
    //关闭等待效果
    function disLoading() {
        layer.close(loadi);
    }

//团队不固定分配收入页面


    //打开父级iframe
    function iframeopen() {
        layer.open({
            type: 2,
            title: ['团队收入不固定分配', 'font-size:14px;'],
            area: ['1100px', '650px'],
            offset: '10px',
            fixed: false, //不固定
            maxmin: true,
            content: '../PP_Staffinfo/TTshourutwelistView',
            btn: ['关闭'],
            yes: function () {
                layer.closeAll();
            },
            end: function () {
                location.reload();
            }

        });
        return false;
    }






 





