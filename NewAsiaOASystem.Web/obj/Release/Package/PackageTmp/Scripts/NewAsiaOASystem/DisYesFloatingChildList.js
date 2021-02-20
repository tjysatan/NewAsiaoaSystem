
$(document).ready(function () {
    AjaxCombo("/DisYesFloatingChild/GetImmuneCenter", 0)//免疫点
    AjaxCombo("/DisYesFloatingChild/GetVaccinationQK", 1)//疫苗数据
});

var setting = {
    check: {
        enable: true,
        chkboxType: { "Y": "", "N": "" }
    },
    view: {
        dblClickExpand: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeClick: beforeClick,
        onCheck: onCheck
    }
};


function beforeClick(treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj(treeId);
    zTree.checkNode(treeNode, !treeNode.checked, null, true);
    return false;
}

function onCheck(e, treeId, treeNode) {
    BindNode(e, treeId, treeNode);
}

/*
treeId 被点击的文本框ID
menuContentId 显示内容的Div
*/
function showMenu(treeId, menuContentId) {
    var cityObj = $("#" + treeId);
    var cityOffset = $("#" + treeId).offset();
    $("#" + menuContentId).css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

    //$("body").bind("mousedown", onBodyDown);

    //免疫点
    if (treeId == "Search_ImmuneName") {
        //绑定免疫点点击事件
        $("body").bind("mousedown", onBodyDown);
    }
    
    //接种类型
    else if (treeId == "search_JzTypeName") {
        //绑定接种类型点击事件
        $("body").bind("mousedown", onBodyDown_JzType);
    }
}

//隐藏免疫点弹出框
function hideImmuneMenu() {
    $("#Search_ImmuneContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

//隐藏接种类型弹出框
function hideJzTypeMenu() {
    $("#search_JzTypeNameContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown_JzType);
}

function onBodyDown(event) {
    //alert(event.target.id);
    //隐藏免疫点
    if (!(event.target.id == "menuBtn" || event.target.id == "Search_ImmuneName" || event.target.id == "Search_ImmuneContent" || $(event.target).parents("#Search_ImmuneContent").length > 0)) {
        hideImmuneMenu(); //alert("进入免疫点");
    }
    
}

function onBodyDown_JzType(event)
{
    //隐藏疫苗列表(即免疫类型列表)
    if (!(event.target.id == "menuBtn" || event.target.id == "search_JzTypeName" || event.target.id == "search_JzTypeNameContent" || $(event.target).parents("#search_JzTypeNameContent").length > 0)) {
        hideJzTypeMenu();
    }
}



/*
ajax获取下拉框值
url 请求路径
type 0表示免疫点  1表示疫苗类型
*/
function AjaxCombo(url,type) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        success: function (context) {
            var zNodes = eval(context.result);
            if(type==0)//加载免疫点数据
                $.fn.zTree.init($("#Search_ImmuneList"), setting, zNodes);
            else if(type==1)//加载疫苗名称数据
                $.fn.zTree.init($("#search_JzTypeNameList"), setting, zNodes);
        },
        error: function (e) {
            //alert("请求失败");
        }
    })
}

//绑定选中的值
function BindNode(e, treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj(treeId),
   nodes = zTree.getCheckedNodes(true),
   text = "", id = "",tempText="";
    for (var i = 0, l = nodes.length; i < l; i++) {
        id = id + "'" + nodes[i].id + "',";
        text = text+ nodes[i].name + ",";
        tempText = tempText + "'" + nodes[i].name + "',";
    }

    if (id.length > 0)
    {
        id = id.substring(0, id.length - 1);
        text = text.substring(0, text.length - 1)
        tempText = tempText.substring(0, tempText.length - 1)
    }

    //点击免疫点
    if (treeId == "Search_ImmuneList")
    {
        $("#Search_ImmuneID").val(id);
        $("#Search_ImmuneName").val(text);
    }

    //接种类型
    else if (treeId == "search_JzTypeNameList")
    {
        //直接保存姓名，后台使用疫苗名称查询
        $("#search_JzType").val(tempText);
        $("#search_JzTypeName").val(text); 
    }


}