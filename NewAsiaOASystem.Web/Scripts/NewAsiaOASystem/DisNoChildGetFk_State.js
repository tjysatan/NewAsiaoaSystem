//获取未免疫儿童条件查询中反馈状态树形下拉列表
var FkState_setting = {
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
        beforeClick: FkState_beforeClick,
        onCheck: FkState_onCheck
    }
};


function FkState_beforeClick(treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj("FkStateList");
    zTree.checkNode(treeNode, !treeNode.checked, null, true);
    return false;
}

function FkState_onCheck(e, treeId, treeNode) {
    FkState_BindNode();
}

function FkState_showMenu() {

    var cityObj = $("#FkStateName");
    var cityOffset = $("#FkStateName").offset();
    $("#FkStateContent").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

    $("body").bind("mousedown", FkState_onBodyDown);
}
function FkState_hideMenu() {
    $("#FkStateContent").fadeOut("fast");
    $("body").unbind("mousedown", FkState_onBodyDown);
}
function FkState_onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "FkStateName" || event.target.id == "FkStateContent" || $(event.target).parents("#FkStateContent").length > 0)) {
        FkState_hideMenu();
    }
}

var FkState_zNodes = [];
$(document).ready(function () {
    FkState_AjaxCombo("/DisNoFloatingChild/GetFk_State")
    $.fn.zTree.init($("#FkStateList"), FkState_setting, FkState_zNodes);
});

//ajax获取下拉框值
function FkState_AjaxCombo(url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: false,
        success: function (context) {
            FkState_zNodes = eval(context.result);
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}

//绑定选中的值
function FkState_BindNode() {
    var zTree = $.fn.zTree.getZTreeObj("FkStateList"),
   nodes = zTree.getCheckedNodes(true);
    zTree.expandAll(true);
    var text = "";//选中项的显示值
    var FkState_DisArea = "";//地区隐藏ID(本地已免，本地未免，外地已免，外地未免)
    var FkState_DisState = "";//反馈状态隐藏ID（种全，漏种，待种）
    for (var i = 0, l = nodes.length; i < l; i++) {
        //（种全，补种，漏种）
        if (nodes[i].pId != null && nodes[i].pId != "") {
            FkState_DisArea = FkState_DisArea + nodes[i].pId + ",";//保存免疫地区值
            FkState_DisState = FkState_DisState + nodes[i].id + ",";//保存反馈状态值
        }

        //（本地外地已免疫/本地外地未免疫）
        if (nodes[i].pId == null || nodes[i].pId == "") {
            FkState_DisArea = FkState_DisArea + nodes[i].id + ",";
        }

        text = text + nodes[i].pName + ",";
    }
    if (text.length > 0) {
        text = text.substring(0, text.length - 1);
    }
    if (FkState_DisArea.length > 0) {
        FkState_DisArea = FkState_DisArea.substring(0, FkState_DisArea.length - 1);
    }
    if (FkState_DisState.length > 0) {
        FkState_DisState = FkState_DisState.substring(0, FkState_DisState.length - 1);
    }
  
    $("input[name='FkStateName']").val(text);
    $("input[name='FkState_DisArea']").val(FkState_DisArea);
    $("input[name='FkState_DisState']").val(FkState_DisState);
}