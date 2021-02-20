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
    var zTree = $.fn.zTree.getZTreeObj(treeId),
        nodes = zTree.getCheckedNodes(true);
    //限制每次最多同时选择3个免疫点查询
    if (nodes.length > 3) {
        //treeNode.cancelSelectedNode(e);
        $.messager.alert("操作提示", "每次最多同时选择3个免疫点进行查询！", "error");
    }

    BindNode();
    
}

function showMenu(id) {
    var cityObj = $("#sel_Comm");
    var cityOffset = $("#sel_Comm").offset();
    $("#menuContent").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

    $("body").bind("mousedown", onBodyDown);
}
function hideMenu() {
    $("#menuContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "sel_Comm" || event.target.id == "menuContent" || $(event.target).parents("#menuContent").length > 0)) {
        hideMenu();
    }
}


$(document).ready(function () {
    AjaxCombo("/GNLD_Statistics/GetImmuneCenter")
    $.fn.zTree.init($("#CommList"), setting, zNodes);
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
    var zTree = $.fn.zTree.getZTreeObj("CommList"),
   nodes = zTree.getCheckedNodes(true),
   text = "", id = "";
    for (var i = 0, j = nodes.length; i < j; i++) {
        id = id + nodes[i].id + ",";
        text=text+ nodes[i].name + ",";
    }
    if (id.length > 0) {
        id = id.substring(0, id.length - 1);
        text = text.substring(0, text.length - 1);
    }
    var textObj = $("#sel_Comm");
    var SelectComm = $("#SelectComm");
    SelectComm.attr("value", id);
    textObj.attr("value", text);
}