//打开作业流程单基本信息填写页面
function AttachmentView(val, atttype) {
    layer.open({
        type: 2,
        title: ['供应商附件上传', 'font-size:14px;'],
        area: ['800px', '450px'],
        offset: '10px',
        fixed: false, //不固定
        maxmin: true,
        content: '../NQ_SupplierAttachment/SupplierAttachEdit?supplierid=' + val + '&atttype=' + atttype,
        end: function () {           //关闭弹出层触发
            location.reload();       //刷新父界面，可改为其他
        }
    });
}




//
function upload(val) {

    var newwin = window.open()

    newwin.location.href = val;
}

function rdbSelected() {
    
    var val = $('input:radio[name="FSupplierType"]:checked').val();

    if (val == 0)
    {
        $('#agentul').hide();
    }
    if (val == 1)
    {
        $('#agentul').show();
    }
    
}