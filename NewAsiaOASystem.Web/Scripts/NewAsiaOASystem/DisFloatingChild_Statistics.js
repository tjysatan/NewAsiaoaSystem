
//免疫点数据
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
    var zTree = $.fn.zTree.getZTreeObj(treeId);
    zTree.checkNode(treeNode, !treeNode.checked, null, true);
    return false;
}

function onCheck(e, treeId, treeNode) {
    BindNode(treeId);
}

/*
treeId 被点击的控件ID
ContentId 显示内容的DIV ID
*/
function showMenu(treeId, ContentId) {
    var cityObj = $("#" + treeId);
    var cityOffset = $("#" + treeId).offset();
    $("#" + ContentId).css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");
    //免疫点
    if (treeId == "Search_MianYiDianName") {
        //绑定免疫点点击事件
        $("body").bind("mousedown", onBodyDown); 
    }

    else if (treeId == "CommCodeName") {
        //绑定社区点击事件
        $("body").bind("mousedown", onBodyDown_SheQu);
    }
}

//隐藏免疫点
function hideMianYiDianMenu() {
    $("#MianYiDian_NameContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}

//隐藏社区
function hideSheQuMenu() {
    $("#SheQu_NameContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown_SheQu);
}

//免疫点点击事件
function onBodyDown(event) {
    //隐藏免疫点
    if (!(event.target.id == "menuBtn" || event.target.id == "Search_MianYiDianName" || event.target.id == "MianYiDian_NameContent" || $(event.target).parents("#MianYiDian_NameContent").length > 0)) {
        hideMianYiDianMenu();
    }
}

//社区点击事件
function onBodyDown_SheQu(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "CommCodeName" || event.target.id == "SheQu_NameContent" || $(event.target).parents("#SheQu_NameContent").length > 0)) {
        hideSheQuMenu();
    }
}


$(document).ready(function () {
    //获取免疫点数据
    AjaxCombo("/DisNoFloatingChild_Statistics/GetMianYiDianCenter")
    $.fn.zTree.init($("#MianYiDian_NameList"), setting, zNodes);

    //获取社区数据
    AjaxCombo("/DisNoFloatingChild_Statistics/GetSheQuCenter")
    $.fn.zTree.init($("#SheQu_NameList"), setting, zNodes);
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
            alert("请求失败");
        }
    })
}

//绑定选中的值
function BindNode(treeId) {
    var zTree; 
    //免疫点
    if (treeId == "MianYiDian_NameList")
        zTree = $.fn.zTree.getZTreeObj("MianYiDian_NameList");

    //社区
    else if (treeId == "SheQu_NameList")
        zTree = $.fn.zTree.getZTreeObj("SheQu_NameList");

    var nodes = zTree.getCheckedNodes(true),
     text = "", id = "";
    for (var i = 0, l = nodes.length; i < l; i++) {
        //免疫点
        if (treeId == "MianYiDian_NameList")
            id =id+"'"+ nodes[i].id+"',";
        else if (treeId == "SheQu_NameList")//社区
            id = id + "'" + nodes[i].name + "',";//获取社区名称
        text = text + nodes[i].name + ",";
    }

    if (nodes.length > 0) {
        id = id.substring(0, id.length - 1);
        text = text.substring(0, text.length - 1);
    }

    //免疫点
    if (treeId == "MianYiDian_NameList")
    {
        $("#Search_MianYiDian").val(id);//设置隐藏值
        $("#Search_MianYiDianName").val(text); 
    }

    //社区
    else if (treeId == "SheQu_NameList")
    {
        $("#CommCode").val(id);//设置隐藏值
        $("#CommCodeName").val(text); 
    }

}