



/*
弹出添加框
action路径
*/
function addClick(OpenUrl) {
    location.href = OpenUrl;
    return false;
}

/*
修改记录
*/
function update(OpenUrl) {
    //获取到选中值
    var arr = GetGridSelete();
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


function del(url) {
    var arr = GetGridSelete();
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


function S_Mass(url) {
    var yhtype = $("#YHtype").val();

    if (yhtype == "0") {
        $.messager.alert("操作提示", "请选择群发对象！", "warning");
    }
    var arr = GetGridSelete();
    if (arr.length <= 0) {
        $.messager.alert("操作提示", "请选择群发的消息！", "warning");
    }
    if (arr.length > 1) {
        $.messager.alert("操作提示", "只能选择一个！", "warning");
    }
    else {
        $.messager.confirm('提示框', '你确定要发送吗?', function () {
            location.href = url + "?id=" + arr[0] + "&&type=" + yhtype;
        }
        )
    }

    return false;
}

function S_jxMass(url) {
    $.messager.confirm('提示框', '你确定要发送吗?', function () {
        location.href = url;
    }
    )
    return false;
}


function wxdel(url) {
    //获取到选中值
    var arr = GetGridSelete();
    if (arr.length > 1) {
        $.messager.alert("操作提示", "只能选择一个！", "warning");
    }
    if (arr.length <= 0) {
        $.messager.alert("操作提示", "请先选择一条记录后再删除！", "warning");
    }
    else {
        $.messager.confirm('提示框', '你确定要删除吗?', function (data) {
            location.href = url + "?id=" + arr[0];
            //if (data) {
            //    var id = '';
            //    for (var i = 0, j = arr.length; i < j; i++) {
            //        id = id + "'" + arr[i] + "',"
            //    }
            //    id = id.substring(0, id.length - 1);
            //    location.href = url + "?id=" + id;
            //}
        })
    }

    return false;
}

//返回页面
function RedirectUrl(url) {
    location.href = url;
    return false;
}


/*
ajax表单提交
RetUrl 返回路径
*/
function submitForm(RetUrl) {
    //表单验证
    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    location.href = RetUrl;
                });
            }
            if ("Repeat" == context.result) {
                $.messager.alert("操作提示", '用户名已经存在！', 'Repeat');
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
            if ("Nameerror" == context.result) {
                $.messager.alert("操作提示", '免疫点名称和所属区域不能为空！', 'error');
            }
            if ("MenuNameerror" == context.result) {
                $.messager.alert("操作提示", '菜单名称和菜单路径不能为空！', 'error');
            }
            if ("Pointerror" == context.result) {
                $.messager.alert("操作提示", '免疫点坐标不能为空！', 'error');
            }
            if ("error1" == context.result) {
                $.messager.alert("操作提示", '提交失败！', 'error');
            }
            if ("error2" == context.result) {
                $.messager.alert("操作提示", '您已经参与过了！', 'error');
            }
            if ("error3" == context.result) {
                $.messager.alert("操作提示", '提交成功！', 'info', function () {
                    location.href = RetUrl;
                });
            }
            if ("error4" == context.result) {
                $.messager.alert("操作提示", '提交失败！', 'error');
            }

            if ("error5" == context.result) {
                $.messager.alert("操作提示", '调查问卷已经过期！', 'error');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }
    $('#form1').ajaxSubmit(options);
    return false; //为了不刷新页面,返回false 
}

function submittest() {
    //表单验证
    var options = {

        beforeSubmit: function () {
            return true;
        },
        //type: 'POST',
        //async:false,
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    //   location.href = RetUrl;
                    wx.closeWindow();
                });
            }
            if ("Repeat" == context.result) {
                $.messager.alert("操作提示", '用户名已经存在！', 'Repeat');
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
            if ("Nameerror" == context.result) {
                $.messager.alert("操作提示", '免疫点名称和所属区域不能为空！', 'error');
            }
            if ("MenuNameerror" == context.result) {
                $.messager.alert("操作提示", '菜单名称和菜单路径不能为空！', 'error');
            }
            if ("Pointerror" == context.result) {
                $.messager.alert("操作提示", '免疫点坐标不能为空！', 'error');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }
    $('#form1').ajaxSubmit(options);
    return false; //为了不刷新页面,返回false 
}


function submitDel(RetUrl) {
    var options = {
        beforeSubmit: function () {
            return true;
        },
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    window.parent.location = RetUrl;
                });
            }
            else {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }
        },
        error: function (XMLResponse) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    };

    $('#form1').ajaxSubmit(options);
    return false; //为了不刷新页面,返回false 
}


function clearForm() {
    var ID = $("#ID").val();
    $('#form1').form('clear');
    $("#ID").val(ID);
}

/*
Jquery Easyui弹出层
winObj 出口容器ID
titleVal需要显示的标题
OpenUrl打开页面地址
*/
function AlertPageEasy(winObj, titleVal, OpenUrl) {
    var width = $("#" + winObj).css("width");
    var height = $("#" + winObj).css("height");
    $("#" + winObj).html("<iframe src=" + OpenUrl + " style='border:0px;' width='" + width + "' height='" + height + "'></iframe>");
    $("#" + winObj).window({
        title: titleVal,
        //href: OpenUrl,
        modal: true,
        closed: false
    });
}
/*
弹出一个页面层(使用layer插件)
titleVal 页面title内容
Width 显示页面宽带
Height 页面高度
OpenUrl 弹出页面路径
CloseUrl 关闭后跳转地址
*/
function ArertPage(titleVal, Width, Height, OpenUrl, CloseUrl) {
    Width = Width == "" ? 1000 : Width;
    Height = Height == "" ? 500 : Height;
    $.layer({
        type: 1,
        title: titleVal,
        shadeClose: false,
        maxmin: true,
        fix: false,
        area: [Width, Height],
        iframe: {
            src: OpenUrl
        },
        end: function () {
            //$.messager.alert("提示", '保存成功！');
            location.href = CloseUrl;
            //alert("关闭了");
        }
    });
}

/*
弹出信息框
messge 需要提示的消息
ico 小图标值(0-16)
*/
function AretMessage(messge, ico) {
    ico = ico == "" ? -1 : ico;
    $.layer({
        shade: [0.1, '#000'],
        area: ['auto', 'auto'],
        dialog: {
            msg: messge,
            type: ico,
            btn: ['确定']
        }
    });
}

/*
  关闭Layer弹出层
*/
function CloseLayer() {
    var index = parent.layer.getFrameIndex(window.name); //获取当前窗体索引
    parent.layer.close(index); //执行关闭
}

/*
获取DataGrid选中的值
*/
function GetGridSelete() {
    var arr = new Array();
    $("input[name='ContentChecked']").each(function () {
        if ($(this).prop("checked"))
            arr.push($(this).val());
    })
    return arr;
}

/*
获取DataGrid选中的值
*/
function GetGridSelete2() {
    var arr = new Array();
    $("input[name='ContentChecked2']").each(function () {
        if ($(this).prop("checked"))
            arr.push($(this).val());
    })
    return arr;
}

/*
给datagrid设置图片
*/
function showImg(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val && val != "") {
        return '<img src="' + val + '" style="width:40px;hegiht:25px;"/>';
    }
    return "";
}

/*
datagrid转换值
*/
function showVal(val) {
    if (val == "1") {
        return '启用';
    }
    else {
        return '禁用';
    }
}

/*
datagrid日期时间截取
*/
function showDate(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var len = val.indexOf("T"); 
        if (len >= 0) {
            val = val.replace("T", " ");
        }
        var date = new Date(val).Format('yyyy-MM-dd');
        //var date = val.substring(0, 10);
        return date;
    }
    return "";

}

/*
替换文字
*/
function showtype(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var data;
        if (val == "text") {
            data = "文本"
        }
        else if (val == "news") {
            data = "图文"
        }
        return data;
    }
}

/*
替换 是否默认
*/

function showmr(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var data;
        if (val == "False") {
            data = "否"
        }
        else if (val == "True") {
            data = "是"
        }
        return data;
    }
}


/*
datagrid日期时间截取
*/
function showDes(val) {

    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        //var len = val.indexOf("T"); 
        //if (len >= 0) {
        //    val = val.replace("T", " ");
        //}
        //var date = new Date(val).Format('yyyy-MM-dd');
        var date = val.substring(0, 15);
        return date;
    }
    return "";

}
/*
使用ajax请求WebApi绑定Combotree数据
cmt 绑定的控件ID
url请求路径路径
单选
*/
function AjaxCombotree(cmt, returnID, url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: true,
        success: function (result) {
            var json = eval('(' + result + ')');
            $('#' + cmt).combotree({
                data: json,
                onClick: function (node) {
                    if (returnID != null) {
                        $("#" + returnID).val(node.id);

                    }
                }
            })
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}


/*
 使用ajax请求WebApi绑定Combobox数据
    cmt 绑定的控件ID
    url请求路径路径
    多选
*/
function AjaxCombobox(cmt, returnID, url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: true,
        success: function (result) {
            var json = eval('(' + result + ')');
            $('#' + cmt).combotree({
                data: json,
                onClick: function (none) {
                    $("#" + returnID).val($('#' + cmt).combotree('getValues'));
                }
            });

        },
        error: function (e) {
            alert("请求失败");
        }
    })

}
/*
  使用ajax请求WebApi绑定Combotree数据
  cmt 绑定的控件ID
  url请求路径路径
  多选
  */

function AjaxCombotreeMultiple(cmt, returnID, url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: true,
        success: function (result) {
            var json = eval('(' + result + ')');
            $('#' + cmt).combotree({
                data: json,
                onClick: function (node) {
                    var t = $('#' + cmt).combotree('tree');
                    var n = t.tree('getChecked');
                    //存放选择的ID
                    var D_ID = new Array();
                    for (var i = 0, j = n.length; i < j; i++) {
                        D_ID.push(n[i].ID);
                    }
                    var D_IDstring = D_ID.join(",");
                    $("#" + returnID).val(D_IDstring);
                },

            });
        }
    })
}
/*
  使用ajax请求WebApi绑定Combotree数据
  cmt 绑定的控件ID
  url请求路径路径
  多选
  */

function AjaxCombotreeMultiple(cmt, returnID, url) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        async: false,
        success: function (result) {
            var json = eval('(' + result + ')');
            $('#' + cmt).combotree({
                data: json,
                onCheck: function (node) {
                    var t = $('#' + cmt).combotree('tree');
                    var n = t.tree('getChecked');
                    //存放选择的ID
                    var D_ID = new Array();
                    for (var i = 0, j = n.length; i < j; i++) {
                        D_ID.push(n[i].id);
                    }
                    var D_IDstring = D_ID.join(",");
                    $("#" + returnID).val(D_IDstring);
                }
            });

        },
        error: function (e) {
            alert("请求失败");
        }
    })
}

/*
将json数据解析   
*/
function ShowiList(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {

        var Name = new Array();
        var obj = eval('(' + val + ')');
        for (var i = 0, j = obj.length; i < j; i++) {
            Name.push(obj[i].Name);
        }
        return Name.join(",");
    }
    else {
        return "";
    }
}

function ShowPname(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {

        var Name = new Array();
        var obj = eval("[" + val + "]");
        for (var i = 0, j = obj.length; i < j; i++) {
            Name.push(obj[i].Name);
        }
        return Name.join(",");
    }
    return "";

}

//获取Url值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

/*
编辑的时候绑定已经选中下拉框
*/
function SelectedDeptTree(cmt, jsonID) {
    $('#' + cmt).combotree({
        onLoadSuccess: function () {
            var arr = eval("(" + $('#' + jsonID).val() + ")");
            for (var i = 0; i < arr.length ; i++) {
                var node = $('#' + cmt).combotree('tree').tree('find', arr[i].Id);
                if (node != null) {
                    $('#' + cmt).combotree('tree').tree('check', node.target);
                    $('#' + cmt).combotree('tree').tree('expandAll', node.target);
                }
            }
        }
    });
}

//点击数据列表框选中列表与反选列表
function TopChecked() {
    var val = $("input[name='TopChecked']").val();

    if (val == "" || val == "0") {
        $("input[name='ContentChecked']").prop("checked", true);
        $("input[name='TopChecked']").val("1");
    }
    else {
        $("input[name='ContentChecked']").prop("checked", false);
        $("input[name='TopChecked']").val("0");
    }
}


//点击数据列表框选中列表与反选列表
function TopChecked2() {
    var val = $("input[name='TopChecked2']").val();

    if (val == "" || val == "0") {
        $("input[name='ContentChecked2']").prop("checked", true);
        $("input[name='TopChecked2']").val("1");
    }
    else {
        $("input[name='ContentChecked2']").prop("checked", false);
        $("input[name='TopChecked2']").val("0");
    }
}


function Wx_MenusubmitForm(RetUrl, fromname) {
    //表单验证
    var options = {
        beforeSubmit: function () {
            return true;
        },
        //type: 'POST',
        //async:false,
        dataType: "json",//这里是指控制器处理后返回的类型，这里返回json格式。  
        success: function (context) {
            if ("success" == context.result) {
                $.messager.alert("操作提示", '操作成功！', 'info', function () {
                    location.href = RetUrl;
                });
            }
            if ("Repeat" == context.result) {
                $.messager.alert("操作提示", '用户名已经存在！', 'Repeat');
            }
            if ("error" == context.result) {
                $.messager.alert("操作提示", '操作失败！', 'error');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert("操作提示", '操作失败！', 'error');
        }
    }
    $('#' + fromname).ajaxSubmit(options);
    return false; //为了不刷新页面,返回false 
}


// 对Date的扩展，将 Date 转化为指定格式的String 
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}



/* 
    鼠标移动到表格行样式
 *@param tabID 表格名称 
 *@param oveClass 鼠标进入时的样式 
 *@param outClass 鼠标离开时的样式(为样式名称) 
 */
function altrow(tabID) {
    var rowObj = $("#" + tabID).find("tr");
    $.each(rowObj, function (i, obj) {
        var tr = $(obj);
        if ("title" != tr.attr("name")) {
            tr.mousemove(function () {
                tr.addClass("mouseover_Color");
            });
            tr.mouseout(function () {
                tr.removeClass("mouseover_Color");
                //tr.addClass(outClass);
            });
        }
    });
}

//换算年纪
function ages(strBirthday, nowdate) {

    strBirthdayArr = showDate(strBirthdayArr);

    var returnAge;
    var strBirthdayArr = strBirthday.split("-");

    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];

    var myDate = new Date();
    var date = showDate(nowdate);
    var d = date.split("/");
    var nowYear = d[0];
    var nowMonth = d[1];
    var nowDay = d[2];

    if (nowYear == birthYear) {
        returnAge = 0;//同年 则为0岁
    }
    else {
        var ageDiff = nowYear - birthYear; //年之差
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;//日之差
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
            else {
                var monthDiff = nowMonth - birthMonth;//月之差
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
        }
        else {
            returnAge = -1;//返回-1 表示出生日期输入错误 晚于今天
        }
    }
    return returnAge;//返回周岁年龄
}


function ajaxsetTimeout() {
    $.ajax({
        type: "POST",
        url: "/Admin/loginsession",
        dataType: "json",
        async: false,
        success: function (result) {
        }
    })
}

//返回数组
function fhdata(val,type) {
    var t = provice;
    alert(t)
}


function changeNumMoneyToChinese(money) {
    var cnNums = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");
    //汉字的数字 　　
    var cnIntRadice = new Array("", "拾", "佰", "仟"); //基本单位 　　
    var cnIntUnits = new Array("", "万", "亿", "兆"); //对应整数部分扩展单位 　　
    var cnDecUnits = new Array("角", "分", "毫", "厘"); //对应小数部分单位 　　
    var cnInteger = "整"; //整数金额时后面跟的字符 　　
    var cnIntLast = "元"; //整型完以后的单位 　　
    var maxNum = 999999999999999.9999; //最大处理的数字 　　
    var IntegerNum; //金额整数部分 　　
    var DecimalNum; //金额小数部分 　　
    var ChineseStr = ""; //输出的中文金额字符串 　　
    var parts; //分离金额后用的数组，预定义 　　
    if (money === "") { return ""; }
    money = parseFloat(money);
    if (money >= maxNum) { alert('超出最大处理数字'); return ""; }
    if (money === 0) { ChineseStr = cnNums[0] + cnIntLast + cnInteger; return ChineseStr; }
    money = money.toString(); //转换为字符串 　　
    if (money.indexOf(".") === -1) { IntegerNum = money; DecimalNum = ''; }
    else { parts = money.split("."); IntegerNum = parts[0]; DecimalNum = parts[1].substr(0, 4); }
    if (parseInt(IntegerNum, 10) >= 0) { //获取整型部分转换 　
        if (IntegerNum === "0") {
            ChineseStr = "零";
        }
        else {
            var zeroCount = 0; var IntLen = IntegerNum.length; for (var i = 0; i < IntLen; i++) {
                var n = IntegerNum.substr(i, 1); var p = IntLen - i - 1; var q = p / 4; var m = p % 4; if (n === "0") { zeroCount++; } else {
                    if (zeroCount > 0) { ChineseStr += cnNums[0]; } zeroCount = 0; //归零 　　
                    ChineseStr += cnNums[parseInt(n, 10)] + cnIntRadice[m];
                } if (m === 0 && zeroCount < 4) { ChineseStr += cnIntUnits[q]; }
            }
        } ChineseStr += cnIntLast; 　　//整型部分处理完毕 　　}
        if (DecimalNum !== '') { //小数部分 　　
            var decLen = DecimalNum.length;
            for (var i = 0; i < decLen; i++) { var n = DecimalNum.substr(i, 1); if (n !== '0') { ChineseStr += cnNums[Number(n)] + cnDecUnits[i]; } }
        } if (ChineseStr === '') { ChineseStr += cnNums[0] + cnIntLast + cnInteger; } else if (DecimalNum === '') { ChineseStr += cnInteger; } return ChineseStr;
    }
};



 



