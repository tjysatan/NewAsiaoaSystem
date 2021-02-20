var zNodes = [];

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
    var zTree = $.fn.zTree.getZTreeObj("CommList");
    zTree.checkNode(treeNode, !treeNode.checked, null, true);
    return false;
}

function onCheck(e, treeId, treeNode) {
    BindNode();
}

function showMenu(id) {
    var cityObj = $("#Search_ImmuneName");
    var cityOffset = $("#Search_ImmuneName").offset();
    $("#Search_ImmuneContent").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");
    $("body").bind("mousedown", onBodyDown);
}
 

function hideMenu() {
    $("#Search_ImmuneContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "sel_Comm" || event.target.id == "Search_ImmuneContent" || $(event.target).parents("#Search_ImmuneContent").length > 0)) {
        hideMenu();
    }
}


$(document).ready(function () {
    AjaxCombo("/DisNoFloatingChild/GetImmuneCenter")
    $.fn.zTree.init($("#Search_ImmuneList"), setting, zNodes);
 
 });

//ajax获取下拉框值
function AjaxCombo(url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: false,
        success: function (context) {
            zNodes = eval(context.result);
        },
        error: function (e) {
            $.messager.alert("操作提示", "查询失败,请重试！", "error");
        }
    })
}

//绑定选中的值
function BindNode() {
    var zTree = $.fn.zTree.getZTreeObj("Search_ImmuneList"),
   nodes = zTree.getCheckedNodes(true),
   text = "", id = "";
    for (var i = 0, j = nodes.length; i < j; i++) {
        id = id +"'"+ nodes[i].id + "',";
        text = text + nodes[i].name + ",";
    }
    if (id.length > 0) {
        id = id.substring(0, id.length - 1);
        text = text.substring(0, text.length - 1);
    } 
    var textObj = $("#Search_ImmuneName");
    var SelectComm = $("#Search_ImmuneID");
    SelectComm.attr("value", id);
    textObj.attr("value", text);
}

 