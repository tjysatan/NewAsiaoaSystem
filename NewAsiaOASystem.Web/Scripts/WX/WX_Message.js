//$(function () {
//    if ("" == $("#Id").val())
//        $("#Ptitle").text("新增关键词图文回复");
//    else
//        $("#Ptitle").text("新增关键词图文回复");

//})

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
    if (nodes.length > 5) {
        //treeNode.cancelSelectedNode(e);
        $.messager.alert("操作提示", "每次最多同时选择5条图文消息！", "error");
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

var zNodes = [];
$(document).ready(function () {
    AjaxCombo("/FirstBack/MnewsAlbumDropdown");
    $.fn.zTree.init($("#CommList"), setting, zNodes);
    BindNode();
});

//ajax获取下拉框值
function AjaxCombo(url) {
    $.ajax({
        type: "POST",
        url: url,
        data: { Mid: $("#A_id").val() },
        dataType: "json",
        async: false,
        success: function (context) {
            zNodes = eval(context.result);
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}



//绑定选中的值
function BindNode() {
    var zTree = $.fn.zTree.getZTreeObj("CommList"),
   nodes = zTree.getCheckedNodes(true),
   text = "", id = "";
    for (var i = 0, l = nodes.length; i < l; i++) {
        id = id + "'" + nodes[i].id + "',";
        text += nodes[i].name + ",";
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


//保存表单
function SavaForm() {
    var A_KeyWord = $("#A_KeyWord").val();
    //    var passwd1 = $("#txt_CheckPassword").val();
    var sel_Comm = $("#sel_Comm").val();
    // var UserName = $("#UserName").val();
    if (A_KeyWord == "") {
        alert("关键字不为空！");
        return false;
    }

    if (sel_Comm == "") {
        alert("请选择图文消息！");
        return false;
    }

    return submitForm('/FirstBack/Index');
}