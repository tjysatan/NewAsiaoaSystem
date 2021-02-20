

var setting = {
    check: {
        enable: true,
        chkStyle: "radio",
        radioType: "all"
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
    var cityObj = $("#CommName");
    var cityOffset = $("#CommName").offset();
    $("#CommNameContent").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

    $("body").bind("mousedown", onBodyDown);
}
function hideMenu() {
    $("#CommNameContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "CommName" || event.target.id == "CommNameContent" || $(event.target).parents("#CommNameContent").length > 0)) {
        hideMenu();
    }
}

var zNodes = [];
$(document).ready(function () {
   // AjaxCombo("/DisImmuneCenter/GetSheQuCenter")
    $.fn.zTree.init($("#CommNameList"), setting, zNodes);
    BindNode();
});

//ajax获取下拉框值
//function AjaxCombo(url) {
//    $.ajax({
//        type: "POST",
//        url: url,
//        dataType: "json",
//        async: false,
//        success: function (context) {
//            zNodes = eval(context.result); 
//        },
//        error: function (e) {
//            alert("请求失败11");
//        }
//    })
//}

//绑定选中的值
function BindNode()
{
    var zTree = $.fn.zTree.getZTreeObj("CommNameList"),
   nodes = zTree.getCheckedNodes(true),
   text = "",id="";
    for (var i = 0, l = nodes.length; i < l; i++) {
        id = nodes[i].id;
        text= nodes[i].name;
    }
    var textObj = $("#CommName");
    var SelectComm = $("#CommId");
    SelectComm.attr("value", id);
    textObj.attr("value", text);
}