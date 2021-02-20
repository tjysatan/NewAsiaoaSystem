
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加返退货单");
    else
        $("#Ptitle").text("编辑返退货单");

})

 
//保存表单
function SavaForm()
{
    var Name = $("#Name").val();//型号
    var CustListData = $("#CustListData").val();//客户
    var R_Number = $("#R_Number").val();//数量
   
    if (Name == "")
    {
        alert("请填写型号！");
        return false;
    }
    if (CustListData == "")
    {
        alert("请选择客户！");
        return false;
    }
    if (R_Number == "")
    {
        alert("请填写数量！");
        return false;
    }
 
    return submitForm('/NAReturnList/List');
}

//保存表单
function kfSavaForm() {
    var PerListData = $("#PerListData").val();//产品类型
    var R_FTdate = $("#R_FTdate").val();//返退时间
    var SelectedCLData = $("input[name='SelectedCLData']").val();//返退原因1
    var SelectedyqclData = $("input[name='SelectedyqclData']").val();//要求处理结果
    if (PerListData == "") {
        alert("请选择返退产品！");
        return false;
    }
    if (R_FTdate == "") {
        alert("请选择返退时间！");
        return false;
    }
    if (SelectedCLData == "") {
        alert("请选择返退原因！");
        return false;
    }
    if (SelectedyqclData == "") {
        alert("请选择要求处理结果！");
        return false;
    }
    return submitForm('/NAReturnList/kflist');
}


function AjaxCust_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetNACusjs",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

/*根据ID 返回客户名称*/
function ShowCustName(val) {
    var R_name;
    if (AjaxCust_Name(val) != "" && AjaxCust_Name(val) != null)
        R_name = AjaxCust_Name(val).Name;
    else
        R_name = "-";
    return R_name;
}

/*根据ID 返回客户联系人名称*/
function ShowCustlxrName(val) {
    var R_name;
    if (AjaxCust_Name(val))
        R_name = AjaxCust_Name(val).LxrName;
    else
        R_name = "-";
    return R_name;
}

/*显示 返退货单的处理状态*/

function Showl_zt(val) {
    var zt_name;
    if (val == "0") {
        zt_name = "未处理";
    } else if (val == "1") {
        zt_name = "待确定";
    } else if (val == "2") {
        zt_name = "待维修";
    } else if (val == "3") {
        zt_name = "待定责";
    } else if (val == "4") {
        zt_name = "待处理";
    } else if (val == "5") {
        zt_name = "待审核";
    } else if (val == "6") {
        zt_name = "已完成";
    } else if (val == "7") {
        zt_name = "配件待修";
    }
    return zt_name;
}

function ckupdate(OpenUrl, Id, Type) {
 
    if (Type == "0") {
        location.href = OpenUrl + "?id=" + Id;
    }
    
    else {
        alert('相关部门已受理受理当前返退货单，不能修改！');
    }
}

function kfupdate(OpenUrl, Id, Type) {
    var roletype = $("#roletype").val();
    if (Type == "0") {
        location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type == "1")
    {
       location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type == "2") {
        if (roletype == "0" || roletype == "1") {
            location.href = OpenUrl + "?id=" + Id;
        }
        else {
            alert("相关部门已受理受理当前返退货单，您无权限编辑。");
        }
    }
    else {
        alert('相关部门已受理受理当前返退货单，您无权限编辑！');
    }
}

/*
  流程跟踪 跳转
*/
function lcgz(Id, Type) {
    if (Type != "0") {
        var url = "/NAReturnList/LCGZ?Id=" + Id;
        location.href = url;

    } else {
        alert('该单尚未处理！暂没流程跟踪单。');
    }

}


function AjaxPerson_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetPersonjson_Id",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}


//动态加载下拉框值
function GetproList() {
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/Getpreasonjson",
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval('(' + context + ')');
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '" style="padding:1px;cursor:pointer;display:block;">' + data[i].Name + '</span>';
            }
            $("#DivContent_SelectedCLData").html(html);
            $("#DivContent_SelectedCLData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
                $('#SelectedCLData').combo('setValue', v).combo('setText', s).combo('hidePanel');
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


function GetproxhList(Id,type) {
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetNAReasonby_pidjson",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (context) {
            var html = "";
            var data = eval('(' + context + ')');
            for (var i = 0, j = data.length; i < j; i++) {
                html = html + '<span  name="' + data[i].Id + '"  style="padding:1px;cursor:pointer;display:block;">' + data[i].Name + '</span>';
            }
            if (type == "0") {
                $('#SelectedZTData').combo('showPanel');//有子集的弹出弹框
            }
            $("#DivContent_SelectedZTData").html(html);
            $("#DivContent_SelectedZTData  span").click(function () {
                var v = $(this).attr("name");
                var s = $(this).text();
               
                $('#SelectedZTData').combo('setValue', v).combo('setText', s).combo('hidePanel');
               
            });
        },
        error: function () {
            layer.alert('初始化数据错误！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }

    })
}

/*给要求处理的下拉框赋值*/
function Getcljg() {
    var html = "";
    html+='<span  name="0"  style="padding:1px;cursor:pointer;display:block;">修复寄回</span>';
    html += '<span  name="1"  style="padding:1px;cursor:pointer;display:block;">翻新入库后减帐</span>';
    html += '<span  name="3"  style="padding:1px;cursor:pointer;display:block;">正常流程处理</span>';
    html += '<span  name="2"  style="padding:1px;cursor:pointer;display:block;">其他:</span>';
  
    $("#DivContent_SelectedyqclData").html(html);
    $("#DivContent_SelectedyqclData  span").click(function () {
        var v = $(this).attr("name");
        var s = $(this).text();

        $('#SelectedyqclData').combo('setValue', v).combo('setText', s).combo('hidePanel');
        if (v == "1") {
            $("#clfxjz").css('display', 'block');
            $("#clqtsm").css('display','none');
        }
        else if (v == "2") {
            $("#clfxjz").css('display', 'none');
            $("#clqtsm").css('display', 'block');
        } else {
            $("#clfxjz").css('display', 'none');
            $("#clqtsm").css('display', 'none');
        }

    });
}

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

//选中已编辑的下拉框
function SelectItem() {
    //返退货原因1
    var R_FTyyp = $("#R_FTyyp").val();
    //返退货原因2
    var R_FTyy = $("#R_FTyy").val();
    //处理结果
    var cljg = $("#R_CLjg").val();
    //返退类型
    var FTtype = $("#FTtype").val();
    //是否在保
    var R_isbxqn = $("#R_isbxqn").val();
    //品质判定
    var R_Pzpd = $("#R_Pzpd").val();
    selectedVal("SelectedCLData",R_FTyyp,R_FTyy,null);
    //selectedVal("SelectedQYData", DisFeedback, DisArea, DisDealWithState);
    selectedVal("SelectedZTData", null, R_FTyy, null);
    selectedVal("SelectedyqclData", null, null, cljg);
}
function SelectItemwx() {
    //返退类型
    var FTtype = $("#FTtype").val();
    //是否在保
    var R_isbxqn = $("#R_isbxqn").val();
    //品质判定
    var R_Pzpd = $("#R_Pzpd").val();
    selectedVal("SelectedftypeData", FTtype, null, null);
    selectedVal("SelectedsfzbData", null, R_isbxqn, null);
    selectedVal("SelectedpzpdData", null, null, R_Pzpd);
}
function SelectItemdz() {
    var R_ZRMB=$("#R_bbzrbm").val();
    EachDoucment("SelectezrbmData", R_ZRMB)
}

/*
设置值
Obj 操作对象
SelectedCLData 返退原因1
SelectedZTData 返退原因2
*/
function selectedVal(Obj, SelectedCLData, SelectedZTData, SelectedyqclData) {
      var val = -1;
    if (SelectedCLData != null && SelectedCLData != "")
        val = EachDoucment(Obj, SelectedCLData);
    if (val == 0)
        return;
  
    if (SelectedZTData != null && SelectedZTData != "")
        val = EachDoucment(Obj, SelectedZTData);
    if (val == 0)
        return;

    if (SelectedyqclData != null && SelectedyqclData != "")
        val = EachDoucment(Obj, SelectedyqclData);
    if (val == 0)
        return;
}


function EachDoucment(Obj, val) {
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
根据ID 查找返退货流程跟踪单
*/

function AjaxReturnlist(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetReturnlistjson",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

/*
根据Id 查找返退货产品累类型信息
*/

function AjaxR_Product(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetR_Productjson",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

/*
根据Id 查找返退货原因
*/
function AjaxResasonjson(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/NAWebApi/GetNAResasonjs",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}


/*显示要求处理结果*/
function Showcljg(val) {
    var cljg;
    if (val != "") {
        if (val == "0")
        {
            cljg = "修复寄回";
        } else if (val == "1") {
            cljg = "翻新入库后减帐";
        } else if (val == "2") {
            cljg = "其他：";
        } else if(val=="3"){
            cljg = "正常处理流程";
        }
    }
    return cljg;
}

/*
 车间返退货维修跳转
*/
function wxupdate(OpenUrl, Id, Type) {
    var roletype = $("#roletype").val();
    if (Type == "2" || Type == "3"||Type=="7") {
        location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type != "2") {
        if (roletype == "0") {
            location.href = OpenUrl + "?id=" + Id;
        }
        else {
            alert("相关部门已受理受理当前返退货单，您无权限编辑。");
        }
    }
    else {
        alert('相关部门已受理受理当前返退货单，您无权限编辑！');
    }
}

/*返退货类型 0 维修 1 翻新*/
function Getfttype() {
    var html = "";
    html += '<span  name="0"  style="padding:1px;cursor:pointer;display:block;">维修</span>';
    html += '<span  name="1"  style="padding:1px;cursor:pointer;display:block;">翻新</span>';
    $("#DivContent_SelectedftypeData").html(html);
    $("#DivContent_SelectedftypeData  span").click(function () {
        var v = $(this).attr("name");
        var s = $(this).text();
        $('#SelectedftypeData').combo('setValue', v).combo('setText', s).combo('hidePanel');
        if (v == "0") {// 维修
            $("#sfzb").css('display', 'block');
            $("#fxjl").css('display', 'block');
            $("#bzpd").css('display', 'none');
            $("#qtqk").css('display', 'none');
            $("#fxjljs").css('display','none');
            Getiszb("0");
        } else if (v == "1") {//翻新
            $("#sfzb").css('display', 'none');
            $("#fxjl").css('display', 'none');
            $("#bzpd").css('display', 'block');
            $("#fxjljs").css('display','block');
            Getpzpd("0");
        }
    });
}

/*是否在保期内*/
function Getiszb(type) {
    var html = "";
    html += '<span name="0"  style="padding:1px;cursor:pointer;display:block;">保内</span>';
    html += '<span name="1"  style="padding:1px;cursor:pointer;display:block;">保外</span>';
    $("#DivContent_SelectedsfzbData").html(html);
    if(type=="0")
        $('#SelectedsfzbData').combo('showPanel');

    $("#DivContent_SelectedsfzbData  span").click(function () {
        var v = $(this).attr("name");
        var s = $(this).text();
        $('#SelectedsfzbData').combo('setValue', v).combo('setText', s).combo('hidePanel'); 
    });
}

/*品质判定*/
function Getpzpd(type) {
    var html = "";
    html += '<span name="0" style="padding:1px;cursor:pointer;display:block;">保修期限外,不予入库</span>';
    html += '<span name="1" style="padding:1px;cursor:pointer;display:block;">客户使用不当,不予入库</span>';
    html += '<span name="2" style="padding:1px;cursor:pointer;display:block;">翻新入库，可减帐</span>';
    html += '<span name="3" style="padding:1px;cursor:pointer;display:block;">其他情况说明：</span>';
    $("#DivContent_SelectedpzpdData").html(html);
    if(type=="0")
        $('#SelectedpzpdData').combo('showPanel');
    $("#DivContent_SelectedpzpdData  span").click(function () {
        var v = $(this).attr("name");
        var s = $(this).text();
        if (v == "3") {
            $("#qtqk").css('display', 'block');
        } else {
            $("#qtqk").css('display', 'none');
        }
        $('#SelectedpzpdData').combo('setValue', v).combo('setText', s).combo('hidePanel');
    });
}

//车间维修记录保存表单
function wxSavaForm() {
    var SelectedftypeData = $("input[name='SelectedftypeData']").val();//返退类型
    var SelectedsfzbData = $("input[name='SelectedsfzbData']").val();//是否在保
    var SelectedpzpdData = $("input[name='SelectedpzpdData']").val();//品质判定
    var R_qtqksm = $("#R_qtqksm").val();//其他情况说明
    var R_Fxjlcon = $("#R_Fxjlcon").val();//翻新记录
    var R_YQJ = $("#R_YQJ").val();//元器件
    var R_RGF = $("#R_RGF").val();//人工费
    var R_BCF = $("#R_BCF").val();//包材费
   //var SelectedyqclData = $("input[name='SelectedyqclData']").val();//要求处理结果
    if (SelectedftypeData == "") {
        alert("请选择返退类型！");
        return false;
    }
    if (SelectedftypeData == "0")//维修
    {
        if (SelectedsfzbData == "")
        {
            alert("选择是否在保！");
            return false;
        }
    }
    else if (SelectedftypeData == "1")//翻新
    {
        if (SelectedpzpdData == "")
        {
            alert("请选择品质判定！");
            return false;
        }
        if (R_Fxjlcon == "")
        {
            alert("请填写翻新记录");
            return false;
        }
    }
    if (R_YQJ == "")
    {
        alert("请填写元器件费用！如果没有请填0！");
        return false;
    }
    if (R_RGF == "")
    {
        alert("请填写人工费用！如果没有请填0！");
        return false;
    }
    if (R_BCF == "")
    {
        alert("请填写包材费用！如果没有请填0！");
        return false;
    }
    return submitForm('/NAReturnList/wxlist');
}

/*品保返退货定责*/
function dzupdate(OpenUrl, Id, Type) {
    var roletype = $("#roletype").val();
    if (Type == "0") {
        location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type != "0") {
        if (roletype == "0" || Type == "3" || Type == "4") {
            location.href = OpenUrl + "?id=" + Id;
        }
        else {
            alert("相关部门已受理当前返退货单，您无权限编辑。");
        }
    }
    else {
        alert('相关部门已受理当前返退货单，您无权限编辑！');
    }
}

/*品质判定*/
function pzpd(val) {
    var value;
    if (val == "0") {
        value = "保修期限外,不予入库";
    } else if (val == "1") {
        value = "客户使用不当,不予入库";
    } else if (val == "2") {
        value = "翻新入库，可减帐";
    } else if (val == "3") {
        value = "其他情况说明：";
    }
    return value;
}

/*定责表单*/
function dzSavaForm() {
    var zrbm = $("#zrbm").val();//
    if (zrbm == "xz") {
        alert("请选择责任部门！");
        return false;
    }
    return submitForm('/NAReturnList/dzlist');
}

//新定责

/*显示责任部门*/

function showzrbm(val) {
    var value;//返回责任部门
    if (val == "0"){
        value = "品保部";
    } else if (val == "1") {
        value = "技术部";
    } else if (val == "2") {
        value = "制造部";
    } else if (val == "3") {
        value = "营销部";
    } else if (val == "4") {
        value == "其他部门";
    } else if (val == "5") {
        value == "客户单位";
    }
    return value;
}

/*营销部返退货处理*/
function clupdate(OpenUrl, Id, Type) {
    var roletype = $("#roletype").val();
    if (Type == "0") {
        location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type != "0") {
        if (roletype == "0" || Type == "4") {
            location.href = OpenUrl + "?id=" + Id;
        }
        else {
            alert("相关部门已受理当前返退货单，您无权限编辑。");
        }
    }
    else {
        alert('相关部门已受理当前返退货单，您无权限编辑！');
    }
}

function clSavaForm() {
    var R_xybclyj = $("#R_xybclyj").val();//
    if (R_xybclyj == "") {
        alert("处理意见不为空！");
        return false;
    }
    return submitForm('/NAReturnList/cllist');
}
 

/*经理办公司审核意见*/
function shupdate(OpenUrl, Id, Type) {
    var roletype = $("#roletype").val();
    if (Type == "0") {
        location.href = OpenUrl + "?id=" + Id;
    }
    else if (Type != "0") {
        if (roletype == "0" || Type == "5") {
            location.href = OpenUrl + "?id=" + Id;
        }
        else {
            alert("相关部门理受理当前返退货单，您无权限编辑。");
        }
    }
    else {
        alert('相关部门已受理当前返退货单，您无权限编辑！');
    }
}

/*打印流程跟踪*/
function PrintNaReturn(Id,L_type)
{
    if (L_type != "6")
    {
        alert('该单没有完成，无法打印！');
        return false;
    }
    if (Id != "") {
        var url = "/NAReturnList/NAReturnPrintView?Id=" + Id;
        location.href = url;

    }
}

//返回打印操作
function Printsc(val, val2)
{
    var Id = "'" + val + "'";
    var l_type = "'" + val2 + "'";//
    var f = '<a href="#" onclick="PrintNaReturn(' + Id + "," + l_type + ')">打印</a>&nbsp;&nbsp;'
    return f;
}

function shSavaForm() {
    var R_xybclyj = $("#R_JLSHYJ").val();//
    if (R_xybclyj == "") {
        alert("处理意见不为空！");
        return false;
    }
    return submitForm('/NAReturnList/shlist');
}

/*出货单状态 显示*/
function showCHtype(val) {
    var value;//返回的状态
    if (val == "0") {
        value = "待开单";
    } else if (val == "1") {
        value = "待确认";
    } else if (val == "2") {
        value = "待出货";
    } else if (val == "3") {
        value = "已出货";
    }
    return value;
}


/*出货单单开跳转*/
function chview(OpenUrl, Id, Type) {
    if (Type == "0" || Type == "1"||Type=="2") {
        location.href = OpenUrl + "?id=" + Id;
    } else {
        alert("出货单已开好，相关部门已经受理无法修改！");
    }
}
/*出货确认跳转*/
function chqrview(OpenUrl, Id, Type) {
    if (Type == "1" || Type == "2") {
        location.href = OpenUrl + "?id=" + Id;
    } else {
        alert("出货单已确认，相关部门已经受理无法修改！");
    }
}

/*仓库发货跳转*/
function ckchview(OpenUrl,Id, Type) {
    if (Type == "2" || Type == "3") {
        location.href = OpenUrl + "?id=" + Id;
    } 
}

function chaddp(Id) {
    editchmx(Id);
}


function editchmx(id) {
    //设置选中行
    if (id != "") {
        $('#windowDia').html("<iframe src=../NQ_CHdetailinfo/list?id=" + id + "&&type=1 style='border:0px;' width='600px' height='500px'></iframe>");
        $("#windowDia").window({
            title: '出货产品',
            modal: true,
            closed: false,
            width:680,
            height:500,
        });
    }
}

function kdSavaForm() {
    return submitForm('/NAReturnList/chlist');
}

//品保出货确认
function chqrSavaForm() {
    return submitForm('/NAReturnList/chqrlist');
}

//仓库 出货确认
function ckchSavaForm() {
    return submitForm('/NAReturnList/ckchlist');
}

//展开or收起 仓库退货明细内容
function ckdetail() {
    if ($("#thdetail").css('display') == "none") {
        $("#ckck").html("收起");
        $("#thdetail").css('display', 'block');
    }
    else {
        $("#ckck").html("展开");
        $("#thdetail").css('display', 'none');
    }
}
//展开or收起 基础信息内容
function jcinfock() {
    if ($("#jcinfo").css('display') == "none") {
        $("#jcck").html("收起");
        $("#jcinfo").css('display', 'block');
    }
    else {
        $("#jcck").html("展开");
        $("#jcinfo").css('display', 'none');
    }
}

//展开or 收起 车间信息内容
function cjinfock() {
    if ($("#dqcjinfo").css('display') == "none") {
        $("#dqcjck").html("收起");
        $("#dqcjinfo").css('display', 'block');
    }
    else {
        $("#dqcjck").html("展开");
        $("#dqcjinfo").css('display', 'none');
    }
}

//展开or收起 品保部信息内容
function pbbinfock()
{
    if ($("#pbbinfo").css('display') == "none") {
        $("#pbbck").html("收起");
        $("#pbbinfo").css('display', 'block');
    }
    else {
        $("#pbbck").html("展开");
        $("#pbbinfo").css('display', 'none');
    }
}
//展开or收起 营销中心处理信息内容
function pyxzxclck() {
    if ($("#yxzxclinfo").css('display') == "none") {
        $("#yxzxcl").html("收起");
        $("#yxzxclinfo").css('display', 'block');
    }
    else {
        $("#yxzxcl").html("展开");
        $("#yxzxclinfo").css('display', 'none');
    }
}

//AJax 方法


//查询返退货流程信息
function ajaxReturnjson(r_id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NAReturnList/GetReturnmodeljson",
        data: { Id: r_id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("请求失败!");
        }
    })
    return json;
}
//根据返退货流程ID 查找退货明细
function ajaxthdetailjson(R_Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_CHdetailinfo/GetthdetailinfobyIdjson",
        data: { R_Id: R_Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

/*根据产品Id 查找产品信息*/
function GetPerinfobyId(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_CHdetailinfo/GetPreinfobyId",
        data: { ID: Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
    return json;
}

//根据返退货流程ID 查找退货明细(新的)
function ajaxNewthFXjson(R_Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NAReturnList/GetAJxaTHfxcpbyr_id",
        data: { R_Id: R_Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = reslut;

        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}

//根据元器件ID 查找元器件信息(新的)
function ajaxNewyjmodelbyidjson(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_YJinfo/Getyjinfomodelbyid",
        data: { Id: Id },
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
    return json;
}

//根据供应商代码查找供应商信息(新的)
function ajaxNewgysinfobydm(dm) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_GysInfo/Getgysinfobydm",
        data: { dm: dm },
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
    return json;
}

//根据不良现象ID 查找不良现象信息(新的)
function ajaxNewblxxmodeljson(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_Blxxinfo/GetblXXmodelbyId",
        data: { Id: Id },
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
    return json;
}

//根据不良原因ID 查找不良原因信息(新的)
function ajaxNewblyymodelbyidjson(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/NQ_Blinfo/GetblyymodelbyId",
        data: { Id: Id },
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
    return json;
}

function showNewISZB(val) {
    var str;
    if (Number(val) < 18) {
        str = "18个月内";
        return str;
    }
    if (18 <= Number(val) && Number(val) <= 36) {
        str = "18个月至3年";
        return str;
    }
    if (Number(val) > 36) {
        str = "3年外";
        return str;
    }
    return;
}


/*根据明细Id 删除*/
function DeletethinfonbyId(Id) {
    $.ajax({
        type: "POST",
        url: "/NQ_CHdetailinfo/THDelete",
        data: {
            Id: Id
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            dataGrid();
        },
        error: function (e) {
            alert("请求失败！");
        }
    })
}

function fxcktz(id) {
        //设置选中行
        if (id != "") {
            $('#windowDia').html("<iframe src=../NAReturnList/ThFXlistck?Id=" + id +   "&&R_Id="+'@Model.Id' +"style='border:0px;' width='600px' height='500px'></iframe>");
            $("#windowDia").window({
                title: '返退品分析查看',
                modal: true,
                closed: false,
                width: 800,
                height: 500,
            });
        }
}

//返退单返回上一级状态
function Fthfhtype(Id, type, hytype)
{
    $.messager.confirm('提示框', '你确定要确认吗?', function (data) {
        $.ajax({
            type: "POST",
            url: "/NAReturnList/Fanhuishangtype",
            data: { r_Id: Id, type: type, YHtype: hytype },
            dataType: "html",
            success: function (response) {
                if ("0" == response) {
                    $.messager.alert("操作提示", "操作成功！", "info");
                }
                if ("1" == response) {
                    $.messager.alert("操作提示", "操作失败！", "info");
                }
                if ("2" == response) {
                    $.messager.alert("操作提示", "无权返回上级状态，请联系下级管理员！", "info");
                }
                if ("3" == response) {
                    $.messager.alert("操作提示", "不存在该数据！", "info");
                }
                if ("4" == response) {
                    $.messager.alert("操作提示", "非法提交！", "info");
                }
            },
            error: function (e) {
                $.messager.alert("操作提示", "网络异常,请重试！", "warning");
            }
        })
    })   
}

 