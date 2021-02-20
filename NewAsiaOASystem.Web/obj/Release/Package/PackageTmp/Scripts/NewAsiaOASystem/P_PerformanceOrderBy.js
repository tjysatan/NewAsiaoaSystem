$(function () {
    P_PerformanceOrderBy();
    GetOrderByMethods();
})

//获取排序方式
function GetOrderByMethods() {
    $.ajax({
        type: 'POST',
        url: "/DisPerformanceAppraisal/GetOrderBy",
        dataType: "json",
        ContentType: 'application/json;charset=utf-8',
        success: function (context) {
            var data = eval(context.result);
            $("#OrderByMethods").append("<option value=''>请选择</option>");
            for (var i = 0, j = data.length; i < j; i++) {
                if (i == 0)
                    $("#OrderByMethods").append("<option value='" + i + "' selected='selected'>" + data[i] + "</optiion>");
                else
                    $("#OrderByMethods").append("<option value='" + i + "'>" + data[i] + "</optiion>");
            }
        },
        error: function (e) {
            layer.alert('查询失败,请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

function P_PerformanceOrderBy(StartDate, EndDate, OrderByMethods, OrderByType) {
    $.ajax({
        type: 'POST',
        url: "/DisPerformanceAppraisal/P_PerformanceOrderBy",
        data: { StartDate: StartDate, EndDate: EndDate, OrderByMethods: OrderByMethods, OrderByType: OrderByType },
        dataType: "json",
        ContentType: 'application/json;charset=utf-8',
        success: function (context) {
            var data = eval(context.result);
            if (data != null && data.length > 0)
                RespontHtml(data);
            altrow("GetContent");
        },
        error: function (e) {
            layer.alert('查询失败,请重试！', { icon: 2, title: '操作提示' }, function (index) {
                layer.close(index);
            });
        }
    })
}

function P_PerformanceOrderByExportExcel(StartDate, EndDate, OrderByMethods, OrderByType) {
    location.href = "/DisPerformanceAppraisal/P_PerformanceOrderByExcel?StartDate=" + StartDate +
        "&EndDate=" + EndDate + "&OrderByMethods=" + OrderByMethods + "&OrderByType=" + OrderByType;
}

function Btn_Search() {
    var StartDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();
    var OrderByMethods = $("#OrderByMethods").val();
    var OrderByType = $("#OrderByType").val();
    P_PerformanceOrderBy(StartDate, EndDate, OrderByMethods, OrderByType)
}

function Btn_ExportExcel()
{
    var StartDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();
    var OrderByMethods = $("#OrderByMethods").val();
    var OrderByType = $("#OrderByType").val();
    P_PerformanceOrderByExportExcel(StartDate, EndDate, OrderByMethods, OrderByType)
}

function RespontHtml(Content) {
    var data = Content[0].ImmuneDtJson;
    var html = "";
    html = html + '<table cellpadding="0" cellspacing="0" style=" float:left; border:none;">';
    html = html + '<tr class="h-table-content-r" name="title">';
    html = html + '<td width="100">免疫点</td>';
    html = html + '<td width="100">已处理数量</td>';
    html = html + '<td width="100">未处理数量</td>';
    html = html + '<td width="100">已处理百分比</td>';
    html = html + '<td width="100">未处理百分比</td>';
    if (Content[0].RoleType != "0") //微信访问
    {
        html = html + '<td width="100">逾期未处理数量</td>'
        html = html + '<td width="100">逾期已处理数量</td>'
        html = html + '<td width="100">逾期未处理百分比</td>'
        html = html + '<td width="100">逾期已处理百分比</td>'
    }
    html = html + '</tr>';
    html = html + '  </table>';

    html = html + ' <div class="pm_ass_pag">';
    html = html + '<table cellpadding="0" cellspacing="0" style=" float:left;">';
    for (var i = 0, j = data.length; i < j; i++) {
        if (i % 2 == 0)
            html = html + ' <tr class="h-table-content-r1">';
        else
            html = html + ' <tr class="h-table-content-r">';
        html = html + '<td width="100">' + data[i].YesImmune_UnitName + '</td>';
        html = html + '<td width="100">' + data[i].YesChildCount + '</td>';
        html = html + '<td width="100">' + data[i].NotChildCount + '</td>';
        html = html + '<td width="100">' + data[i].Yes_Bfb_ChildCount + '%</td>';
        html = html + '<td width="100">' + data[i].Not_Bfb_ChildCount + '%</td>';
        //if (Content[0].RoleType != "0") //微信访问
        //{
        //    html = html + '<td width="100">-</td>';
        //    html = html + '<td width="100">-</td>';
        //    html = html + '<td width="100">-</td>';
        //    html = html + '<td width="100">-</td>';
        //}

        if (Content[0].RoleType == "1") //各免疫点
        {
            if (data[i].YesImmune_UnitId == Content[0].UnitId) {
                html = html + '<td width="100">' + data[i].Yq_NotChildCount + '</td>';
                html = html + '<td width="100">' + data[i].Yq_YestChildCount + '</td>';
                html = html + '<td width="100">' + data[i].Yq_Bfb_NotChildCount + '%</td>';
                html = html + '<td width="100">' + data[i].Yq_Bfb_YesChildCount + '%</td>';
            }
            else {
                html = html + '<td width="100">-</td>';
                html = html + '<td width="100">-</td>';
                html = html + '<td width="100">-</td>';
                html = html + '<td width="100">-</td>';
            }
        }

        else if (Content[0].RoleType == "2")//疾控中心
        {
            html = html + '<td width="100">' + data[i].Yq_NotChildCount + '</td>';
            html = html + '<td width="100">' + data[i].Yq_YestChildCount + '</td>';
            html = html + '<td width="100">' + data[i].Yq_Bfb_NotChildCount + '%</td>';
            html = html + '<td width="100">' + data[i].Yq_Bfb_YesChildCount + '%</td>';
        }

        html = html + '</tr>';
       
    }
    html = html + SumCount(Content);
    html = html + '</table></div>';

    $("#GetContent").html(html);
    layer.msg('数据加载成功', {
        icon: 1,
        time: 700 //700毫秒关闭（如果不配置，默认是3秒）
    });
}

function SumCount(Content) {
    var data = Content[0].ImmuneDtJson;
    //已处理数量
    var Yclcount = 0;
    //未处理数量
    var WclCount = 0;
    //已经处理百分比
    var YclBFB = 0;
    //未处理百分比
    var wclBFB = 0;
   
    //逾期未处理数量
    var YqwclCount = 0;
    //逾期已处理数量
    var YqyclCount = 0;
    //逾期未处理百分比
    var yqwclBFB = 0;
    //逾期已处理百分比
    var yqyclBFB = 0;
 
    for (var i = 0, j = data.length; i < j; i++) {
   
        //已经处理总数量
        Yclcount = parseInt(Yclcount) + parseInt(data[i].YesChildCount);
        //未处理总数量
        WclCount = parseInt(WclCount) + parseInt(data[i].NotChildCount);
        //总人数
        var SumCount = Yclcount + WclCount;
        //已处理百分比
        YclBFB = BFB(Yclcount, SumCount);
        //未处理百分比
        wclBFB = BFB(WclCount, SumCount);
        //逾期未处理总数
        YqwclCount = parseInt(YqwclCount) + parseInt(data[i].Yq_NotChildCount);
        //逾期已处理数量
        YqyclCount = parseInt(YqyclCount) + parseInt(data[i].Yq_YestChildCount);
        //逾期总人数
        var SumYQCount = YqwclCount + YqyclCount;
        //逾期未处理百分比
        yqwclBFB = BFB(YqwclCount, SumYQCount);
        //逾期已处理百分比
        yqyclBFB = BFB(YqyclCount, SumYQCount);


    }
  

    var tempHtml = "";
    tempHtml = tempHtml + ' <tr class="h-table-content-r">';
    tempHtml = tempHtml + '<td width="100" style="color:red;">合计</td>';
    tempHtml = tempHtml + '<td width="100" style="color:red;">' + Yclcount + '</td>';
    tempHtml = tempHtml + '<td width="100" style="color:red;">' + WclCount + '</td>';
    tempHtml = tempHtml + '<td width="100" style="color:red;">' + YclBFB + '%</td>';
    tempHtml = tempHtml + '<td width="100" style="color:red;">' + wclBFB + '%</td>';
    if (Content[0].RoleType == "1") //各免疫点
    {
        tempHtml = tempHtml + '<td width="100">-</td>';
        tempHtml = tempHtml + '<td width="100">-</td>';
        tempHtml = tempHtml + '<td width="100">-</td>';
        tempHtml = tempHtml + '<td width="100">-</td>';
    }
    else if (Content[0].RoleType == "2")//疾控中心
    {
        tempHtml = tempHtml + '<td width="100" style="color:red;">' + YqwclCount + '</td>';
        tempHtml = tempHtml + '<td width="100" style="color:red;">' + YqyclCount + '</td>';
        tempHtml = tempHtml + '<td width="100" style="color:red;">' + yqwclBFB + '%</td>';
        tempHtml = tempHtml + '<td width="100" style="color:red;">' + yqyclBFB + '%</td>';
    }
    return tempHtml;
    //for (var i = 0, j = data.length; i < j; i++) {
    //    tempHtml = tempHtml + '<tr>';
    //}
}

//计算百分之百
function BFB(ps, rs) {
    var B;
    B =(parseInt(ps) / parseInt(rs)) * 100;
    B = B.toFixed(2);
    return B;
}