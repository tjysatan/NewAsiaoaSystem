
function formatOper(value, row, index) {
    var s = '<a href="#" onclick="check_log(' + index + ')">查看</a> ';
    //var c = '<a href="#" onclick="editUser(' + index + ')">删除</a>';
    return s;
}

function check_log(index)
{ 
    //设置选中行
    $('#dg1').datagrid('selectRow', index);
    var row = $('#dg1').datagrid('getSelected');
    if (row) { 
        $("#windowDia").dialog({
            title: '系统日志查看',
            href: "../SysLog_history/Sys_logView?Name=" + row.logsName,
            modal: false,
            closed: false,
            maximizable: true,
            resizable:true,
            width: 800,
            height: 600,
        });
       
    }
}

