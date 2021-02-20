$(function () {
    dataGrid("", "");
})

function dataGrid(Name, Phone) {
    $('#community').treegrid('unselectAll');//更新前先清理所有原来选中的社区

    $.ajax({
        type: "POST",
        url: "/Administrative_divisions/GetcommunityTreeData",
        data: { Name: Name, Phone: Phone },
        dataType: "json",
        async: false,
        success: function (content) {
            if (content.result == null || content.result == "")
                content.result = "[]";
            var data = eval('(' + content.result + ')');
            $("#community").treegrid('loadData', data);
        },
        error: function () {
            $.messager.alert("操作提示", '查询失败,请重试！', 'error');
        }
    })
}

function BtnSearch() {
    var Name = $("#txtSearch_Name").val();
    var Phone = $("#txtSearch_Phone").val();
    dataGrid(Name, Phone);
}


function CommDel(url) {
    var arr = GetTreeGridSelete();
    if (arr.length <= 0) {
        $.messager.alert("操作提示", "请先选择一条记录后再删除！", "warning");
    }
    else {
        $.messager.confirm('提示框', '你确定要删除吗?', function (data) {
            if (data) {
                var id = '';
                for (var i = 0, j = arr.length; i < j; i++) {
                    id = id + "'" + arr[i] + "',"
                }

                id = id.substring(0, id.length - 1);
                location.href = url + "?id=" + id;
            }
        })
    }

    return false;
}

/*
修改记录
*/
function CommUpdate(OpenUrl) {
    //获取到选中值
    var arr = GetTreeGridSelete();
    if (arr.length > 1) {
        $.messager.alert("操作提示", "只能选择一个！", "warning");
    }
    else if (arr.length <= 0) {
        $.messager.alert("操作提示", "请先选择一条记录后再编辑！", "warning");
    }
    else {
        location.href = OpenUrl + "?id=" + arr[0];
    }

    return false;
}

/*
获取DataGrid选中的值
*/
function GetTreeGridSelete() {
    var arr = new Array(); 
    var checkedItems = $('#community').treegrid('getSelections');
    if (checkedItems != null)
        $.each(checkedItems, function (index, item) {
            arr.push(item.Id); 
        });
    return arr;
}