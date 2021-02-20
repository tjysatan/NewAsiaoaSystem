
//填出附件上传页面
function attachView(atttype,seq) {
    var val = $('#Id').val();
    var hiddenftype = $('#hiddenFSupplierType').val();
    var selectedftype = $('input:radio[name="FSupplierType"]:checked').val();

    if (hiddenftype != selectedftype) {
        $.messager.alert("操作提示", "更改供应商类型，请先保存基础数据", "warning");
        return false;
    }

    if (val <= 0) {
        $.messager.alert("操作提示", "请先保存供应商基础数据", "warning");
        return false;
    }

    layer.open({
        type: 2,
        title: ['供应商附件上传', 'font-size:14px;'],
        area: ['800px', '450px'],
        offset: '10px',
        fixed: false, //不固定
        maxmin: true,
        content: '../NQ_SupplierAttachment/SupplierAttachEdit?supplierid=' + val + '&atttype=' + atttype + '&seq=' + seq,
        end: function () {           //关闭弹出层触发
            location.reload();       //刷新父界面，可改为其他
        }
    });
}

//查看图片
function upload(val) {

    var newwin = window.open()

    newwin.location.href = val;
}

function rdbSelected() {

    var val = $('input:radio[name="FSupplierType"]:checked').val();

    if (val == 0) {
        $('#agentul').hide();
    }
    if (val == 1) {
        $('#agentul').show();
    }
}


//保存按钮
function SaveForm() {
    var rtn = validateOASuSave();
    if (rtn == false)
    { return false; }
    return submitSupplierForm('/NQ_OASupplier/List', 'list');
}


//保存基础信息按钮
function SaveBaseForm() {
    //alert(2);
    var rtn = validateEmpty();
    //alert(3);
    if (rtn == false) {
        //alert('false');
        return false;
    }
    //alert(4);
    return submitSupplierForm('/NQ_OASupplier/EditPage?id=', 'edit');
}

function submitSupplierForm(RetUrl, submitType) {
    //表单验证
    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    if (submitType == 'edit') {
                        //alert('edit');
                        location.href = (RetUrl + context.id);
                    }
                    if (submitType == 'list') {
                        //alert('list');
                        location.href = RetUrl;
                    }
                });
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
            if ("error3" == context.result) {
                $.messager.alert("操作提示", '提交成功！', 'info', function () {
                    location.href = RetUrl;
                });
            }
            if ("repeat" == context.result) {
                $.messager.alert("操作提示", '供应商代码不能重复！', 'error');
            }
            //if ("FNumber" == context.result) {
            //    $.messager.alert("操作提示", '供应商代码必须输入！', 'error');
            //}
            if ("FAddress" == context.result) {
                $.messager.alert("操作提示", '地址必须输入！', 'error');
            }
            if ("FContact" == context.result) {
                $.messager.alert("操作提示", '联系人必须输入！', 'error');
            }
            if ("FName" == context.result) {
                $.messager.alert("操作提示", '供应商名称必须输入！', 'error');
            }
            if ("FPhone" == context.result) {
                $.messager.alert("操作提示", '电话必须输入！', 'error');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }


    var status = $('#fstatus').val();

    //alert(status);

    if (status != 0) {
        $.messager.confirm("操作提示", '保存后需要重新审核', function (data) {
            if (data) {
                $('#form1').ajaxSubmit(options);
            }
        });
    }
    else {
        $('#form1').ajaxSubmit(options);
    }

    return false; //为了不刷新页面,返回false
}

//验证基础信息必录项
function validateEmpty() {
    //var FNumber = $('#FNumber').val();
    var FName = $('#FName').val();
    var FPhone = $('#FPhone').val();
    var FAddress = $('#FAddress').val();
    var FContact = $('#FContact').val();


    if (FName == "") {
        //alert(FName);
        $.messager.alert("操作提示", "供应商名称必须输入", "warning");
        return false;
    }
    if (FAddress == "") {
        //alert(FAddress);
        $.messager.alert("操作提示", "地址必须输入", "warning");
        return false;
    }
    if (FContact == "") {
        //alert(FContact);
        $.messager.alert("操作提示", "联系人必须输入", "warning");
        return false;
    }
    if (FPhone == "") {
        //alert(FPhone);
        $.messager.alert("操作提示", "电话必须输入", "warning");
        return false;
    }
}

//验证保存必录项，包括附件上传项
function validateOASuSave() {
    var rtn = validateEmpty();
    if (rtn == false)
    { return false; }
    var Id = $('#Id').val();

    if (Id > 0) {

        var FLicenceDeadLine = $('#FLicenceDeadLine').val();

        var FQuestionnaireDeadLine = $('#FQuestionnaireDeadLine').val(); // 供应商调查表
        var FAgentDeadLine = $('#FAgentDeadLine').val(); //代理证

        var FQualityAgreementDeadLine = $('#FQualityAgreementDeadLine').val(); // 质量协议

        //alert(FQuestionnaireDeadline.length);

        if (FLicenceDeadLine.length == 0) {
            $.messager.alert("操作提示", "营业执照必须上传", "warning");

            return false;
        }

        if (FQuestionnaireDeadLine.length == 0) {
            $.messager.alert("操作提示", "供应商调查表必须上传", "warning");

            return false;
        }

        if (FQualityAgreementDeadLine.length == 0) {
            $.messager.alert("操作提示", "质量协议", "warning");

            return false;
        }



        var val = $('input:radio[name="FSupplierType"]:checked').val();

        if (val == 1) {
            if (FAgentDeadLine.length == 0) {
                $.messager.alert("操作提示", "代理证必须上传", "warning");
                return false;
            }
        }
    }
}

//点击提交审核未通过按钮
function UnCheckForm() {
    //设置选择审核状态 
    $("#checkList").attr("disabled", false);

    $("#checkList").val(2);

    var checkresult = $("#checkList").val();

    var RetUrl = "/NQ_OASupplier/CheckList";

    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    //location.href = (RetUrl + context.id);
                    location.href = RetUrl;
                });
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("context");
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }
    $('#form1').ajaxSubmit(options);
    $("#checkList").attr("disabled", true);
    return false; //为了不刷新页面,返回false

}

//点击提交审核通过按钮
function CheckForm() {
    var RetUrl = "/NQ_OASupplier/CheckList";
    var FLicenceStatus = $("#FLicenceS").val();
    //var FTaxNumStatus = $("#FTaxNumS").val();
    //var FISO9001Status = $("#FISO9001S").val();
    var FAgentStatus = $("#FAgentS").val();
    var FSupplierType = $("#hiddenFSupplierType").val();

    if (FLicenceStatus == 0) {
        $.messager.alert("操作提示", "营业执照未上传", "warning");
        $("#checkList").attr("disabled", true);
        return false;
    }
    //if (FTaxNumStatus == 0) {
    //    $.messager.alert("操作提示", "税务登记证未上传", "warning");
    //    $("#checkList").attr("disabled", true);
    //    return false;
    //}
    //if (FISO9001Status == 0) {
    //    $.messager.alert("操作提示", "ISO9001未上传", "warning");
    //    return false;
    //}

    if (FSupplierType == 1) {
        if (FAgentStatus == 0) {
            $.messager.alert("操作提示", "代理证未上传", "warning");
            $("#checkList").attr("disabled", true);
            return false;
        }
    }

    if (FLicenceStatus == 5) {
        $.messager.alert("操作提示", "营业执照过期", "warning");
        $("#checkList").attr("disabled", true);
        return false;
    }
    //if (FTaxNumStatus == 5) {
    //    $.messager.alert("操作提示", "税务登记证过期", "warning");
    //    $("#checkList").attr("disabled", true);
    //    return false;
    //}
    //if (FISO9001Status == 5) {
    //    $.messager.alert("操作提示", "ISO9001过期", "warning");
    //    $("#checkList").attr("disabled", true);
    //    return false;
    //}

    if (FSupplierType == 1) {
        if (FAgentStatus == 5) {
            $.messager.alert("操作提示", "代理证过期", "warning");
            $("#checkList").attr("disabled", true);
            return false;
        }
    }


    //设置选择审核状态 
    $("#checkList").attr("disabled", false);

    $("#checkList").val(1);

    var checkresult = $("#checkList").val();

    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    //location.href = (RetUrl + context.id);
                    location.href = RetUrl;

                });
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("context");
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }
    $('#form1').ajaxSubmit(options);
    $("#checkList").attr("disabled", true);
    return false; //为了不刷新页面,返回false
}