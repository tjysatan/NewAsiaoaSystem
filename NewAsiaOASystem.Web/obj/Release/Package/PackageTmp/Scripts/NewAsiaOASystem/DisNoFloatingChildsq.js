var zNodessq = [];

var sq_setting = {
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
        beforeClick: sqbeforeClick,
        onCheck: sqonCheck
    }
};


function sqbeforeClick(treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj("sqCommList");
    zTree.checkNode(treeNode, !treeNode.checked, null, true);
    return false;
}

function sqonCheck(e, treeId, treeNode) {
    BindNodesq();
}

function showMenuSQ(id) {
    var cityObj = $("#sel_Commsq");
    var cityOffset = $("#sel_Commsq").offset();
    $("#menuContentsq").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");

    $("body").bind("mousedown", sqonBodyDown);
}
function sqhideMenu() {
    $("#menuContentsq").fadeOut("fast");
    $("body").unbind("mousedown", sqonBodyDown);
}
function sqonBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "sel_Commsq" || event.target.id == "menuContentsq" || $(event.target).parents("#menuContentsq").length > 0)) {
        sqhideMenu();
    }
}


$(document).ready(function () {
    AjaxCombosq("/DisFloatingChild/AlbumDropdown")
    $.fn.zTree.init($("#sqCommList"), sq_setting, zNodessq);
 
});

//ajax获取下拉框值
function AjaxCombosq(url) {
    $.ajax({
        type: "POST",
        url: url,
        data: { commId: $("#Id").val() },
        dataType: "json",
        async: false,
        success: function (context) {
            zNodessq = eval(context.result);
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}

//绑定选中的值
function BindNodesq() {
    var zTree = $.fn.zTree.getZTreeObj("sqCommList"),
   nodes = zTree.getCheckedNodes(true),
   text = "", id = "";
    for (var i = 0, l = nodes.length; i < l; i++) {
        id = nodes[i].name;//通过社区名称过滤数据
        text = nodes[i].name;
    }
    var textObj = $("#sel_Commsq");
    var SelectComm = $("#SelectComm");
    SelectComm.attr("value", id);
    textObj.attr("value", text);
}