$(function () {
    RespontTitle();
    GetDisWorkInfo("","","","");
    GetAllYear();
    //GetSumrs("","","","");
})

function GetAllYear() {
    $.ajax({
        type: 'POST',
        url: "/DisPerformanceAppraisal/GetAllYear",
        dataType: "json",
        ContentType: 'application/json;charset=utf-8',
        success: function (context) {
            var data = eval(context.result); 
            $("#Year").append("<option value=''>请选择</option>");
            for (var i = 0, j = data.length; i < j; i++) {
                $("#Year").append("<option value='" + data[i].Year + "'>" + data[i].Year + "</optiion>");
            }
        },
        error: function (e) {
            layer.alert('查询失败,请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

function GetDisWorkInfo(LStartDate, LEndDate, StartDate, EndDate) {

    $.ajax({
        type: 'POST',
        url: "/DisPerformanceAppraisal/DisWorkInfo",
        data: { LStartDate: LStartDate, LEndDate: LEndDate, StartDate: StartDate, EndDate: EndDate },
        dataType: "json",
        ContentType: 'application/json;charset=utf-8',
        success: function (context) {
            var rs = GetSumrs(LStartDate, LEndDate, StartDate, EndDate)
  
            RespontHtml(eval(context.result),rs);
            altrow("Content");
        },
        error: function (e) {
            layer.alert('查询失败,请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

function GetSumrs(LStartDate, LEndDate, StartDate, EndDate)
{
    var sumrs;
    $.ajax({
        type: 'POST',
        url: "/DisPerformanceAppraisal/DisSUMzrs",
        data: { LStartDate: LStartDate, LEndDate: LEndDate, StartDate: StartDate, EndDate: EndDate },
        dataType: "json",
        ContentType: 'application/json;charset=utf-8',
        async:false,
        success: function (context) {
 
            sumrs = context.result;
            altrow("Content");
        },
        error: function (e) {
            sumrs = 0;
            layer.alert('查询失败,请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
    return sumrs;
}

function Btn_Search() {

    var LStartDate = $("#LStartDate").val();//流入时间开始
    var LEndDate = $("#LEndDate").val();//流入时间结束
    var StartDate = $("#StartDate").val();//入库时间开始
    var EndDate = $("#EndDate").val();//入库时间结束
    GetDisWorkInfo(LStartDate, LEndDate, StartDate,EndDate)
}

function WorkExportExcel() {
    var LStartDate = $("#LStartDate").val();//流入时间开始
    var LEndDate = $("#LEndDate").val();//流入时间结束
    var StartDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();
    location.href = "/DisPerformanceAppraisal/DisWorkInfoExcel?LStartDate=" + LStartDate +
        "&LEndDate=" + LEndDate + "&StartDate=" + StartDate + "&EndDate=" + EndDate;
}



function RespontHtml(data, rs) {
 
    var tempHtml = "";
   $("#Content").empty();
    if (data != null && data != undefined && data.length > 0) {
        var Yes_Bd_ImmuneDtJson = data[0].Yes_Bd_ImmuneDtJson;  //本地已免疫json
        var Yes_Wd_ImmuneDtJson = data[0].Yes_Wd_ImmuneDtJson;//外地已免疫json
        var No_Bd_ImmuneDtJson = data[0].No_Bd_ImmuneDtJson;//本地未免疫json
        var No_Wd_ImmuneDtJson = data[0].No_Wd_ImmuneDtJson;//外地未免疫json
        var No_HandleDtJson = data[0].No_HandleDtJson;//未处理json
       // var No_WlxDtJson = data[0].No_WlxDtJson;//未联系上数据
        var No_YLCDtJson = data[0].No_YLCDtJson;//已流出数据
 
        var arr = new Array();
        for (var i = 0, j = Yes_Bd_ImmuneDtJson.length; i < j; i++) {
            if (i % 2 == 0)
                tempHtml = tempHtml + '<tr name="title">';
            else
                tempHtml = tempHtml + '<tr class="trBack" name="title">';
            //  =======本地已免疫=====
            tempHtml = tempHtml + '<td class="imm_ass_th imm_ass_user1">' + Yes_Bd_ImmuneDtJson[i].YesImmune_UnitName + '</td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_zq">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Zq + '</td></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Jkybz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Jjz + '</div></td>';
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Ybhz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_lxsjjjz + '</div></td>';

            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Jkyyy + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Jjz + '</div></td>';
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Ybhz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_lxsjjjz + '</div></td>';

            //  =======外地已免疫=====
            tempHtml = tempHtml + '<td class="imm_ass_width_zq" style="width:3.2%">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Zq + '</td></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Jkybz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + +Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Jjz + '</div></td>';
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Ybhz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_lxsjjjz + '</div></td>';

            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Jkyyy + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Jjz + '</div></td>';
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Ybhz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_lxsjjjz + '</div></td>';

            //  =======本地未免疫=====
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson[i].NoImmune_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson[i].NoImmune_Jjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson[i].NoImmune_Jkzbjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson[i].NoImmune_Lxsjjjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson[i].NoImmune_ybhz + '</div></td>';

            //  =======外地未免疫=====
            //tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson[i].NoImmune_Wyywl + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson[i].NoImmune_Jjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson[i].NoImmune_Jkzbjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson[i].NoImmune_Lxsjjjz + '</div></td>';
            tempHtml = tempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson[i].NoImmune_ybhz + '</div></td>';

            //  =======未联系上=====
           // tempHtml = tempHtml + '<td class="imm_ass_width_w5"><div class="imm_ass_width_cont" style="width: 100%; text-align: center;">' + No_WlxDtJson[i].NoHandle_Wmy + '</div></td>';
            
            //  =======已流出=====
            tempHtml = tempHtml + '<td class="imm_ass_width_w5"><div class="imm_ass_width_cont" style="width: 100%; text-align: center;">' + No_YLCDtJson[i].NoHandle_Wmy + '</div></td>';

            //=====未处理=====
            tempHtml = tempHtml + '<td class="imm_ass_width_w6 imm_b"><div class="imm_ass_width_cont">' + No_HandleDtJson[i].NoHandle_Wmy + '</div></td>';
            tempHtml = tempHtml + '</tr>';
        }

        tempHtml = tempHtml + SumCount(Yes_Bd_ImmuneDtJson, Yes_Wd_ImmuneDtJson, No_Bd_ImmuneDtJson, No_Wd_ImmuneDtJson, No_HandleDtJson, No_YLCDtJson,rs);
    }
    $("#Content").append(tempHtml);
    layer.msg('数据加载成功', {
        icon: 1,
        time: 700 //700毫秒关闭（如果不配置，默认是3秒）
    });
}

//列汇总及列百分比
function SumCount(Yes_Bd_ImmuneDtJson, Yes_Wd_ImmuneDtJson, No_Bd_ImmuneDtJson, No_Wd_ImmuneDtJson, No_HandleDtJson, No_YLCDtJson,rs) {
    var Yes_Bd_ImmuneDtJson_YesImmune_Zq = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jkybz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jjz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Lz_Wyywl = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Lz_Ybhz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Lz_lxsjjjz = 0;

    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jkyyy = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jjz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_Wyywl = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz = 0;
    var Yes_Bd_ImmuneDtJson_YesImmune_Dz_lxsjjjz = 0;

    var Yes_Wd_ImmuneDtJson_YesImmune_Zq = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jkybz = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jjz = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Lz_Wyywl = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Lz_Ybhz = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Lz_lxsjjjz = 0;

    var Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jkyyy = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jjz = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Dz_Wyywl = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Dz_Ybhz = 0;
    var Yes_Wd_ImmuneDtJson_YesImmune_Dz_lxsjjjz = 0;



  

    //本地未免
    var No_Bd_ImmuneDtJson_NoImmune_Wyywl = 0;
    var No_Bd_ImmuneDtJson_NoImmune_Jjz = 0;
    var No_Bd_ImmuneDtJson_NoImmune_Jkzbjz =0;
    var No_Bd_ImmuneDtJson_NoImmune_Lxsjjjz =0;
    var No_Bd_ImmuneDtJson_NoImmune_ybhz = 0;

    //外地未免
    var No_Wd_ImmuneDtJson_NoImmune_Wyywl = 0;
    var No_Wd_ImmuneDtJson_NoImmune_Jjz = 0;
    var No_Wd_ImmuneDtJson_NoImmune_Jkzbjz = 0;
    var No_Wd_ImmuneDtJson_NoImmune_Lxsjjjz =0;
    var No_Wd_ImmuneDtJson_NoImmune_ybhz =0;

    //未联系上
   // var No_WlxDtJson_NoHandle_Wmy = 0;

    //已经流出
    var No_YLCDtJson_NoHandle_Wmy = 0;

    //未处理
    var No_HandleDtJson_NoHandle_Wmy = 0;

    //已免疫人数
    var yes_myzrs = 0;
    //未免疫人数
    var no_myzrs = 0;

    for (var i = 0, k = Yes_Bd_ImmuneDtJson.length; i < k; i++) {
        //  =======本地已免疫=====
        Yes_Bd_ImmuneDtJson_YesImmune_Zq = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Zq)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Zq);
        Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jkybz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jkybz)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Jkybz);
        Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jjz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jjz)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Jjz);
        Yes_Bd_ImmuneDtJson_YesImmune_Lz_Wyywl = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Wyywl)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Wyywl);
        Yes_Bd_ImmuneDtJson_YesImmune_Lz_Ybhz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Ybhz)
            + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_Ybhz);
        Yes_Bd_ImmuneDtJson_YesImmune_Lz_lxsjjjz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_lxsjjjz)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Lz_lxsjjjz);


        Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jkyyy = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jkyyy)
            + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Jkyyy);
        Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jjz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jjz)
            + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Jjz);
        Yes_Bd_ImmuneDtJson_YesImmune_Dz_Wyywl = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Wyywl)
            + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Wyywl);
        Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz)
           + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_Ybhz);
        Yes_Bd_ImmuneDtJson_YesImmune_Dz_lxsjjjz = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_lxsjjjz)
          + parseInt(Yes_Bd_ImmuneDtJson[i].YesImmune_Dz_lxsjjjz);

        //  =======外地已免疫=====
        Yes_Wd_ImmuneDtJson_YesImmune_Zq = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Zq)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Zq);
        Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jkybz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jkybz)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Jkybz);
        Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jjz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jjz)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Jjz);
        Yes_Wd_ImmuneDtJson_YesImmune_Lz_Wyywl = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Wyywl)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Wyywl);
        Yes_Wd_ImmuneDtJson_YesImmune_Lz_Ybhz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Ybhz)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_Ybhz);
        Yes_Wd_ImmuneDtJson_YesImmune_Lz_lxsjjjz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_lxsjjjz)
           + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Lz_lxsjjjz);


        Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jkyyy = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jkyyy)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Jkyyy);
        Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jjz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jjz)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Jjz);
        Yes_Wd_ImmuneDtJson_YesImmune_Dz_Wyywl = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Wyywl)
            + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Wyywl);
        Yes_Wd_ImmuneDtJson_YesImmune_Dz_Ybhz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Ybhz)
           + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_Ybhz);
        Yes_Wd_ImmuneDtJson_YesImmune_Dz_lxsjjjz = parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_lxsjjjz)
          + parseInt(Yes_Wd_ImmuneDtJson[i].YesImmune_Dz_lxsjjjz);

        //本地未免
        No_Bd_ImmuneDtJson_NoImmune_Wyywl = parseInt(No_Bd_ImmuneDtJson_NoImmune_Wyywl)
        + parseInt(No_Bd_ImmuneDtJson[i].NoImmune_Wyywl);
        No_Bd_ImmuneDtJson_NoImmune_Jjz = parseInt(No_Bd_ImmuneDtJson_NoImmune_Jjz)
            + parseInt(No_Bd_ImmuneDtJson[i].NoImmune_Jjz);
        No_Bd_ImmuneDtJson_NoImmune_Jkzbjz = parseInt(No_Bd_ImmuneDtJson_NoImmune_Jkzbjz)
            + parseInt(No_Bd_ImmuneDtJson[i].NoImmune_Jkzbjz);
        No_Bd_ImmuneDtJson_NoImmune_Lxsjjjz = parseInt(No_Bd_ImmuneDtJson_NoImmune_Lxsjjjz)
           + parseInt(No_Bd_ImmuneDtJson[i].NoImmune_Lxsjjjz);
        No_Bd_ImmuneDtJson_NoImmune_ybhz = parseInt(No_Bd_ImmuneDtJson_NoImmune_ybhz)
          + parseInt(No_Bd_ImmuneDtJson[i].NoImmune_ybhz);

        //外地未免
        No_Wd_ImmuneDtJson_NoImmune_Wyywl = parseInt(No_Wd_ImmuneDtJson_NoImmune_Wyywl)
        + parseInt(No_Wd_ImmuneDtJson[i].NoImmune_Wyywl);
        No_Wd_ImmuneDtJson_NoImmune_Jjz = parseInt(No_Wd_ImmuneDtJson_NoImmune_Jjz)
            + parseInt(No_Wd_ImmuneDtJson[i].NoImmune_Jjz);
        No_Wd_ImmuneDtJson_NoImmune_Jkzbjz = parseInt(No_Wd_ImmuneDtJson_NoImmune_Jkzbjz)
            + parseInt(No_Wd_ImmuneDtJson[i].NoImmune_Jkzbjz);
        No_Wd_ImmuneDtJson_NoImmune_Lxsjjjz = parseInt(No_Wd_ImmuneDtJson_NoImmune_Lxsjjjz)
           + parseInt(No_Wd_ImmuneDtJson[i].NoImmune_Lxsjjjz);
        No_Wd_ImmuneDtJson_NoImmune_ybhz = parseInt(No_Wd_ImmuneDtJson_NoImmune_ybhz)
          + parseInt(No_Wd_ImmuneDtJson[i].NoImmune_ybhz);

        //未联系上
    //    No_WlxDtJson_NoHandle_Wmy = parseInt(No_WlxDtJson_NoHandle_Wmy)
       //+ parseInt(No_WlxDtJson[i].NoHandle_Wmy);

        //已流出
        No_YLCDtJson_NoHandle_Wmy = parseInt(No_YLCDtJson_NoHandle_Wmy)
        + parseInt(No_YLCDtJson[i].NoHandle_Wmy)

        //未处理
        No_HandleDtJson_NoHandle_Wmy = parseInt(No_HandleDtJson_NoHandle_Wmy)
          + parseInt(No_HandleDtJson[i].NoHandle_Wmy);

  

    }
    //已免疫总人数
    yes_myzrs = parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Zq) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jkybz) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jjz) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_Ybhz) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Lz_lxsjjjz)
         + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jkyyy) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jjz) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz) + parseInt(Yes_Bd_ImmuneDtJson_YesImmune_Dz_lxsjjjz)
         + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Zq)
         + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jkybz) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jjz) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_Ybhz) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Lz_lxsjjjz)
         + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jkyyy) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jjz) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_Ybhz) + parseInt(Yes_Wd_ImmuneDtJson_YesImmune_Dz_lxsjjjz);

    no_myzrs = parseInt(No_Bd_ImmuneDtJson_NoImmune_Jjz) + parseInt(No_Bd_ImmuneDtJson_NoImmune_Jkzbjz) + parseInt(No_Bd_ImmuneDtJson_NoImmune_Lxsjjjz) + parseInt(No_Bd_ImmuneDtJson_NoImmune_ybhz)
    + parseInt(No_Wd_ImmuneDtJson_NoImmune_Jjz) + parseInt(No_Wd_ImmuneDtJson_NoImmune_Jkzbjz) + parseInt(No_Wd_ImmuneDtJson_NoImmune_Lxsjjjz) + parseInt(No_Wd_ImmuneDtJson_NoImmune_ybhz);

    //已经免疫占总人数的百分比
    var yesmybfb = disBFB(yes_myzrs, rs);
    //未免疫占总人数的百分比 
    var nomybfb = disBFB(no_myzrs, rs);
    //已经流出占总人数的百分比
    var ylcbfb = disBFB(No_YLCDtJson_NoHandle_Wmy, rs);
    //未处理占总人数的百分比
    var wclbfb = disBFB(No_HandleDtJson_NoHandle_Wmy, rs);
    //alert(rs);
    var tempHtml = "";
    if (Yes_Bd_ImmuneDtJson.length % 2 == 0)
        tempHtml = tempHtml + '<tr>';
    else
        tempHtml = tempHtml + '<tr class="trBack">';
    tempHtml = tempHtml + '<td class="imm_ass_th imm_ass_user" style="color:red;">合计</td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_zq" style="color:red;">' + Yes_Bd_ImmuneDtJson_YesImmune_Zq + '</td></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jkybz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Lz_Jjz + '</div></td>';
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Lz_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Lz_Ybhz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Lz_lxsjjjz + '</div></td>';

    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jkyyy + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Dz_Jjz + '</div></td>';
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Dz_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Dz_Ybhz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Bd_ImmuneDtJson_YesImmune_Dz_lxsjjjz + '</div></td>';

    //  =======外地已免疫=====
    tempHtml = tempHtml + '<td class="imm_ass_width_zq" style="color:red;">' + Yes_Wd_ImmuneDtJson_YesImmune_Zq + '</td></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jkybz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + +Yes_Wd_ImmuneDtJson_YesImmune_Lz_Jjz + '</div></td>';
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Lz_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Lz_Ybhz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Lz_lxsjjjz + '</div></td>';

    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jkyyy + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Dz_Jjz + '</div></td>';
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Dz_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Dz_Ybhz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + Yes_Wd_ImmuneDtJson_YesImmune_Dz_lxsjjjz + '</div></td>';

    //  =======本地未免疫=====
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson_NoImmune_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson_NoImmune_Jjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson_NoImmune_Jkzbjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson_NoImmune_Lxsjjjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Bd_ImmuneDtJson_NoImmune_ybhz + '</div></td>';

    //  =======外地未免疫=====
    //tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson_NoImmune_Wyywl + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson_NoImmune_Jjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson_NoImmune_Jkzbjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson_NoImmune_Lxsjjjz + '</div></td>';
    tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont">' + No_Wd_ImmuneDtJson_NoImmune_ybhz + '</div></td>';

    //  =======未联系上=====
 //   tempHtml = tempHtml + '<td class="imm_ass_width_w" style="color:red;"><div class="imm_ass_width_cont" style="width: 100%; text-align: center;">' + No_WlxDtJson_NoHandle_Wmy + '</div></td>';
    //  =======已流出=====
    tempHtml = tempHtml + '<td class="imm_ass_width_zq" style="color:red;"><div class="imm_ass_width_cont" style="width: 100%; text-align: center;">' + No_YLCDtJson_NoHandle_Wmy + '</div></td>';

    //=====未处理=====
    tempHtml = tempHtml + '<td class="imm_ass_width_w imm_b" style="color:red;"><div class="imm_ass_width_cont">' + No_HandleDtJson_NoHandle_Wmy + '</div></td></tr>';

    //总人数
    tempHtml = tempHtml + '<tr class="trBack">';
    tempHtml = tempHtml + '<td class="imm_ass_th imm_ass_user" style="color:red;">汇总</td>';
    tempHtml = tempHtml + '<td  colspan="18"  style="color:red;">' +yes_myzrs + '</td>';
    tempHtml = tempHtml + '<td  colspan="8"  style="color:red;">' + no_myzrs + '</td>';
    tempHtml = tempHtml + '<td  class="imm_ass_width_zq"  style="color:red;"> ' + No_YLCDtJson_NoHandle_Wmy + '</td>';
    tempHtml = tempHtml + '<td  class="imm_ass_width_w imm_b"  style="color:red;"><div class="imm_ass_width_cont" >' + No_HandleDtJson_NoHandle_Wmy + '</div></td></tr>';

    //百分比
    tempHtml = tempHtml + '<tr class="trBack">';
    tempHtml = tempHtml + '<td class="imm_ass_th imm_ass_user" style="color:red;">百分比(%)</td>';
    tempHtml = tempHtml + '<td  colspan="18"  style="color:red;">' + yesmybfb + '</td>';
    tempHtml = tempHtml + '<td  colspan="8"  style="color:red;">' + nomybfb +'</td>';
    tempHtml = tempHtml + '<td  class="imm_ass_width_zq"  style="color:red;"> ' + ylcbfb + '</td>';
    tempHtml = tempHtml + '<td  class="imm_ass_width_w imm_b"  style="color:red;"><div class="imm_ass_width_cont" style="width: 100%; text-align: center;">' + wclbfb + '</div></td></tr>';

    return tempHtml;
}




//添加头部
function RespontTitle() {

    var TempHtml = "";
    TempHtml = TempHtml + '<tr name="title">';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_user" rowspan="4"><div class="imm_ass_width_cont">免疫点名称</DIV></td>';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_content" colspan="27">已处理</td>';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_b imm_ass_foot" rowspan="4">未处理</td>';
    TempHtml = TempHtml + '</tr>';

    TempHtml = TempHtml + '<tr name="title">';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_width_w1" colspan="9">本地已免疫</td>';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_width_w1" colspan="9">外地已免疫</td>';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_width_w2" rowspan="2" colspan="4">本地未免疫</td>';
    TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_width_w2" rowspan="2" colspan="4">外地未免疫</td>';
   // TempHtml = TempHtml + '<td class="imm_ass_th imm_ass_width_w3" rowspan="3">未联系上（错号或未接）</td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_zq" rowspan="3" >已流出</td>';
    TempHtml = TempHtml + '</tr>';

    TempHtml = TempHtml + '<tr name="title">';
    TempHtml = TempHtml + ' <td class="imm_ass_width_zq" rowspan="2">种全</td>';
    TempHtml = TempHtml + '<td colspan="4">漏种</td>';
    TempHtml = TempHtml + '<td colspan="4">待种</td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_zq" rowspan="2">种全</td>';
    TempHtml = TempHtml + '<td colspan="4">漏种</td>';
    TempHtml = TempHtml + '<td colspan="4">待种</td>';
    TempHtml = TempHtml + '</tr>';

    TempHtml = TempHtml + '<tr name="title">';
    //本地已免疫
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">已预约补种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';

    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">已预约接种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';

    //外地已免疫
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">已预约补种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';

    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">已预约接种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';

    //本地未免疫
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">建卡证并接种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';

    //外地未免疫
    //TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">无原因未来</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">禁忌症</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">建卡证并接种</div></td>';
    TempHtml = TempHtml + '<td class="imm_ass_width_w"><div class="imm_ass_width_cont">联系上拒绝接种</div></td>';
    TempHtml = TempHtml + ' <td class="imm_ass_width_w"><div class="imm_ass_width_cont">因病缓种</div></td>';
    TempHtml = TempHtml + '</tr>';
    $("#TableHead").html(TempHtml);
}


//计算百分之百
function disBFB(ps, rs) {
    if (rs != 0) {
        var B;
        B = (parseInt(ps) / parseInt(rs)) * 100;
        B = B.toFixed(2);
        return B;
    }
    else {
        return "0";
    }
}
