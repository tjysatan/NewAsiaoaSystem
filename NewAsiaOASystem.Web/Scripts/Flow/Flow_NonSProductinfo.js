
$(function () {
    if ("" == $("#Id").val())
        $("#Ptitle").text("添加");
    else
        $("#Ptitle").text("修改");

})

//保存表单
function SavaForm()
{
    var Pbianma = $("#Pbianma").val();
    var Pname = $("#Pname").val();
    var Pmodel = $("#Pmodel").val();
    if (Pbianma == "")
    {
        alert("物料代码不为空！");
        return false;
    }
    if (Pname == "")
    {
        alert("产品名称不为空！");
        return false;
    }
    if (Pmodel == "")
    {
        alert("产品型号不为空！");
        return false;
    }
    return submitForm('/Flow_NonSProductinfo/List');
}

//试产表单提交
function SCSavaForm() {
    var Pbianma = $("#Pbianma").val();
    var Pname = $("#Pname").val();
    var Pmodel = $("#Pmodel").val();
    if (Pbianma == "") {
        alert("物料代码不为空！");
        return false;
    }
    if (Pname == "") {
        alert("产品名称不为空！");
        return false;
    }
    if (Pmodel == "") {
        alert("产品型号不为空！");
        return false;
    }
    return submitForm('/Flow_NonSProductinfo/BatchProductionView');
}


//产品库存情况数据
function AjxaNosPind(Sort, Category, wldm, Pname, Pmodel) {
    var json;
    $.ajax({
        type: "POST",
        url: "../Flow_NonSProductinfo/AJxaGetNonsdataJson",
        data: { Sort: Sort, Category: Category, wldm: wldm, Pname: Pname, Pmodel: Pmodel },
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

 

 

 