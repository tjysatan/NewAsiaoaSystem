
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加");
    else
        $("#Ptitle").text("修改");

})

//保存表单
function SavaForm(url)
{
    var P_Bianhao = $("#P_Bianhao").val();
    var P_Name = $("#P_Name").val();
    if (P_Bianhao == "")
    {
        alert("物料代码不为空！");
        return false;
    }
    if (P_Name == "")
    {
        alert("产品名称不为空！");
        return false;
    }

    return submitForm(url);
}

//用量保存表单
function SavaFromYL()
{
    var M_Consumption = $("#M_Consumption").val();
    var W_Consumption = $("#W_Consumption").val();
    if (M_Consumption == "") {
        alert("月用量不为空！");
        return false;
    }
    if (W_Consumption == "") {
        alert("周用量不为空！");
        return false;
    }
    return submitForm('/Flow_RoutineStockinfo/List');
}


//产品库存情况数据
function AjxaCgPKCJSON(Sort, Category, cpname,wlsort) {
    var json;
    $.ajax({
        type: "POST",
        url: "../Flow_RoutineStockinfo/GetCgkcJson",
        data: { Sort: Sort, Category: Category,cpname:cpname, wlsort: wlsort },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json =reslut;
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}


//操作
function czshow(val,val2,val3) {
    var Id = "'" + val + "'";
    var scing = "'" + val2 + "'";
    var s = '<a href="#" onclick="UpdateConsumption(' + Id + ')">用量</a>&nbsp;&nbsp;';
    var t = '<a href="#" onclick="MakePlan(' + Id + "," + scing + "," + val3 + ')">计划</a>&nbsp;&nbsp;';
    var z = '<a href="#" onclick="DetcgsccpEide(' + Id + ","+val3+')">删除</a>&nbsp;&nbsp;';
    return s + t+z;
}

//用量（月，周）编辑页面
function UpdateConsumption(val) {
    var id = val;
    if (id != "") {
        $('#windowDia').html("<iframe src=../Flow_RoutineStockinfo/UpdateConsumption?Id=" + id + " style='border:0px;' width='450px' height='230px'></iframe>");
        $("#windowDia").window({
            title: '用量修改',
            modal: true,
            closed: false,
            width: 500,
            height: 310,
        });
    }
}


//制定生产计划
function MakePlan(val, val2,val3) {
    if (val2 == "0") {
        var id = val;
        if (id != "") {
            $('#windowDia').html("<iframe src=../Flow_RoutineStockinfo/PlanproductionView?Id=" + id + "&&type="+val3+"&&  style='border:0px;' width='500px' height='400px'></iframe>");
            $("#windowDia").window({
                title: '生产计划',
                modal: true,
                closed: false,
                width:560,
                height: 310,
            });
        }
    } else {
        $.messager.alert("操作提示", '已经存在生产计划单，无需重复下单！', 'error');
    }
}

//删除页面（禁用页面）
function DetcgsccpEide(Id,type) {
    $.messager.confirm("操作提示", '确定要删除该产品吗？', function (data) {
        if (data) {
            var shreslut = deleteEide(Id);
            if (shreslut == "0") {
                $.messager.alert("提示", '该产品已经删除！', 'info', function () {
                    if(type=="0")
                        location.href = "../Flow_RoutineStockinfo/list";
                    else
                        location.href = "../Flow_RoutineStockinfo/gclist";
                });
            }
            else {
                $.messager.alert("提示", '删除失败！', 'info', function () {
                    if (type == "0")
                        location.href = "../Flow_RoutineStockinfo/list";
                    else
                        location.href = "../Flow_RoutineStockinfo/gclist";
                });
            }
        }
    })
}


//删除
function deleteEide(val) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Flow_RoutineStockinfo/DelcgscEide",
        data: { Id: val },
        dataType: "html",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("网路异常，请重试！");
        }
    })
    return json;
}
 

 