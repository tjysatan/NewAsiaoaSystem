
function formatOper(id) {
    var s = '<a href="#" onclick="editRoleAuth(' + id + ')">编辑权限</a> ';
    var c = '<a href="#" onclick="editRoleMember(' + id + ')">查看成员</a>';
    return s + c;
}

//编辑权限
function editRoleAuth(id) {
    //设置选中行
    if (id!="") {
        $('#windowDia').html("<iframe src=/SysRole/RoleManagePage?id=" + id + " style='border:0px;' width='600px' height='500px'></iframe>");
        $("#windowDia").window({
            title: '权限管理',
            modal: true,
            closed: false,
            width: 600,
            height: 500,
        });
    }
}

//查看成员
function editRoleMember(id) {
    //设置选中行
    if (id != "") {
        $('#windowDia').html("<iframe src=/SysRole/SysRoleMember?id=" + id + " style='border:0px;' width='600px' height='500px'></iframe>");
        $("#windowDia").window({
            title: '成员管理',
            modal: true,
            closed: false,
            width: 600,
            height: 500,
        });
    }
}

//获取菜单与设置字段
function AjaxMenu() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSysMenu",
        data: { '': '' },
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval('(' + result + ')');
            $("#MenuDg").treegrid('loadData', json);
          //  $("#FieldDg").treegrid('loadData', json);
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}

//选择菜单时动态刷新字段列表
function MenuDgChecked() {
    var Field = eval('(' + GetFidldSelected() + ')');
    var menuid = GetGridSeleteId("MenuDg");//获取选中的菜单ID
    if (menuid.length > 0) {
        AjaxMenuField(menuid.join(","));
        for (var i = 0, j = Field.length; i < j; i++) {
            var menuId = Field[i].menuId;
            var Status = Field[i].Status;
            var Field_name = Field[i].Field;
            $("#FieldAuth input[name='FieldItem']").each(function () {
                if ($(this).attr("id") == menuId && $(this).attr("status") == Status) {
                    $(this).val(Field_name);
                }
            })
        }
    }
    else
        $("#FieldDg").treegrid('loadData', []);
}

//获取功能权限
function AjaxFunc() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSysFunc",
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval('(' + result + ')');
            $("#ButtonDg").datagrid({
                data: json,
                idField: "Id",
            });
            //$("#ButtonDg").datagrid('loadData', json);
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}

//获取菜单具有的字段
function AjaxMenuField(menuId) {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSysRoleMenuField",
        data: { '': menuId },
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval('(' + result + ')'); 
            $("#FieldDg").treegrid('loadData', json);
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}

//获取数据权限
function GetSysAuthorize() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSysAuthorize",
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval('(' + result + ')');
            $("#DataDg").treegrid({ data: json });
            //$("#DataDg").treegrid('loadData', json);
        },
        error: function (e) {
            $.messager.alert("操作提示", '请求失败！', 'error');
        }
    })
}



/*
   获取DataGrid选中的值(输入需要获取的DataGrid ID)
   */
function GetGridSeleteId(id) {
    var arr = new Array();
    var checkedItems = $('#' + id).datagrid('getChecked'); //getSelected
    $.each(checkedItems, function (index, item) {
        arr.push("'" + item.id + "'");
    });
    return arr;
}

function GetGridSeleteID(id) {
    var arr = new Array();
    var checkedItems = $('#' + id).datagrid('getChecked'); //getSelected
    $.each(checkedItems, function (index, item) {
        arr.push("'" + item.Id + "'");
    });
    return arr;
}

//获取菜单中已经关联的按钮
function GetMenuButton(value, rowData, rowIndex) {
    if (value == null || value == undefined || value == "undefined" || value == "null") {
        return value;
    }
    var data = eval('(' + value + ')');
    var v = '';
    for (var i = 0, j = data.length; i < j; i++) {
        v += "<input id='" + data[i].Id + "' name='ButtonItem'  value='" + rowData.Id + "' type='checkbox' />" + data[i].Name;
    }


    return v;
}

//给字段权限添加一个输入框(允许)
function GetYesInput(value, rowData, rowIndex) {
    return "<input id='" + rowData.id + "' name='FieldItem' type='text' width:'100px' status='1'/>";
}

//给字段权限添加一个输入框（拒绝）
function GetNoInput(value, rowData, rowIndex) {
    return "<input id='" + rowData.id + "' name='FieldItem' type='text' width:'100px' status='0'/>";
}

//保存权限
function SaveSysRoleAuth() {
    var RoleId = $("#RoleId").val();//角色ID
    var MenuId = GetGridSeleteId("MenuDg").join(",");//选中的菜单ID
    var FunctionId = GetGridSeleteID("ButtonDg").join(",");//获取选中功能
    //var AuthId = GetGridSeleteId("DataDg").join(",");//获取选中的数据
    //var FieldId = GetFidldSelected();//字段权限
    var json = { 'Id': RoleId, "SysMenu": MenuId, "SysFun": FunctionId, "SysAuth": "", "SysField": "" };
    AjaxSave(json);
    //$("#Role").val(json[0].FieldId);
}

function AjaxSave(json) {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/SaveSysRoleAuth",
        ContentType: "application/json;charset=utf-8;",
        data: json,
        success: function (context) {
            if (context == "success") {
                $.messager.alert("操作提示", '保存成功！', 'info', function () {
                    var ParentObj = window.parent;//获取父窗口
                    window.parent.$('#windowDia').window('close');
                    window.parent.location = "/SysRole/Index";
                });
            }
            else {
                $.messager.alert("操作提示", '保存失败！', 'error');
            }
        },
        error: function () {
            $.messager.alert("操作提示", '保存失败！', 'error');
        }
    })
}

//获取选中的按钮
function GetSButtonSelected() {
    var obj = $("#ButtonAuth input[name='ButtonItem']:checked");
    var buttonId = "[";
    $(obj).each(function () {
        buttonId = buttonId + '{';
        buttonId = buttonId + '"menuId":"' + $(this).attr("value") + '",';
        buttonId = buttonId + '"buttonId":"' + $(this).attr("id") + '"';
        buttonId = buttonId + "},";
    })
    if (obj.length > 0)
        buttonId = buttonId.substring(0, buttonId.length - 1);
    buttonId = buttonId + "]";
    return buttonId;
}


//获取选中的字段
function GetFidldSelected() {
    var obj = $("#FieldAuth input[name='FieldItem']");
    var buttonId = "[";
    $(obj).each(function () {
        buttonId = buttonId + '{';
        buttonId = buttonId + '"menuId":"' + $(this).attr("id") + '",';
        buttonId = buttonId + '"Field":"' + $(this).val() + '",';
        buttonId = buttonId + '"Status":"' + $(this).attr("status") + '"';
        buttonId = buttonId + "},";
    })
    if (obj.length > 0)
        buttonId = buttonId.substring(0, buttonId.length - 1);
    buttonId = buttonId + "]";
    return buttonId;
}

//绑定选中的权限
function GetSelectedAuth() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSelectedAuth",
        data: { "": $("#RoleId").val() },
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            var data = eval('(' + context + ')');
            if (data.length > 0) {
                SeletedGrid(data)
            }
        },
        error: function () {
            $.messager.alert("操作提示", '获取权限失败！', 'error');
        }
    })
}


function SeletedGrid(data) {
    var menu = eval('(' + data[0].menu + ')');
    var func = eval('(' + data[0].func + ')');
    var Auth = eval('(' + data[0].Auth + ')');
    var RoleColumn = eval('(' + data[0].RoleColumn + ')');
    for (var i = 0, j = menu.length; i < j; i++) {
        if (menu[i] != null && menu[i] != undefined) {
            $('#MenuDg').treegrid('select', menu[i].Id);
        }

    }

    for (var i = 0, j = func.length; i < j; i++) {
        if (func[i] != null && func[i] != undefined) {
            $('#ButtonDg').datagrid('selectRecord', func[i].Id);
        }
    }

  /*  for (var i = 0, j = Auth.length; i < j; i++) {
        if (Auth[i] != null && Auth[i] != undefined) {
            $('#DataDg').treegrid('select', Auth[i].Id);
        }
    }

    for (var i = 0, j = RoleColumn.length; i < j; i++) {
        if (RoleColumn[i] != null && RoleColumn[i] != undefined) {
            var menuId = RoleColumn[i].MenuId;
            var Status = RoleColumn[i].Type;
            var Field_name = RoleColumn[i].Field_name;
            $("#FieldAuth input[name='FieldItem']").each(function () {
                if ($(this).attr("id") == menuId && $(this).attr("status") == Status) {
                    $(this).val(Field_name);
                }
            })
        }
    }*/
}


//===========================给角色分配成员==================================

//获取所有部门
function GetDeptMember() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetSysDepartment",
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            var json = eval('(' + context + ')');
            $("#DeptDg").treegrid({ data: json });

        },
        error: function () {
            $.messager.alert("操作提示", '获取角色成员失败！', 'error');
        }
    })
}

//获取该角色具有的部门
function GetDeptSelectedMember() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetRoleDeptData",
        data: { "": $("#RoleId").val() },
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            var json = eval('(' + context + ')');
            SeletedDeptGrid(json);
        },
        error: function () {
            $.messager.alert("操作提示", '获取角色成员失败！', 'error');
        }
    })
}

//选中角色有具有的部门
function SeletedDeptGrid(data) {
    for (var i = 0, j = data.length; i < j; i++) {
        if (data[i] != null && data[i] != undefined) {
            $('#DeptDg').treegrid('select', data[i].Id);
        }
    }
}


//保存该角色选中的的部门
function SaveDeptMember() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/SaveRoleDeptData",
        data:SaveDeptMemberActon(),
        dataType: "json",
        ContentType: "application/json;charset=utf-8;",
        async: false,
        success: function (context) {
            if (context == "success") {
                $.messager.alert("操作提示", '保存成功！', 'info', function () {
                    var ParentObj = window.parent;//获取父窗口
                    window.parent.$('#windowDia').window('close');
                    window.parent.location = "/SysRole/Index";
                });
            }
            else {
                $.messager.alert("操作提示", '保存失败！', 'error');
            }
        },
        error: function () {
            $.messager.alert("操作提示", '保存角色成员失败！', 'error');
        }
    })
}

function SaveDeptMemberActon()
{
    var deptArr = GetGridSeleteId("DeptDg");
    var json = { "RoleId": $("#RoleId").val(), "DepartmentId": deptArr.join(",") };
    return json;
}


//获取该角色具有的部门
function GetPersonMember() {
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetPersonData",
        dataType: "json",
        data: { "": $("#RoleId").val() },
        ContentType: "application/json;charset=utf-8;",
        async: true,
        success: function (context) {
            var data = eval('(' + context + ')');
            PersonMemberHTML(data)
        },
        error: function () {
            $.messager.alert("操作提示", '获取角色成员失败！', 'error');
        }
    })
}

function PersonMemberHTML(data) {
    $("#parson").html("");
    for (var i = 0, j = data.length; i < j; i++) {
        $("#parson").append("<span>" + data[i].Name + "</span>");
    }
}