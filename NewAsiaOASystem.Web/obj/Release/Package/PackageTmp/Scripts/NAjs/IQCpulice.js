
var loadi;
function ddchen() {
    loadi = layer.load(1, { shade: [0.8, '#393D49'] })
}
//关闭等待效果
function disLoading() {
    layer.close(loadi);
}


//打开父级iframe
function iframeopen() {
 layer.open({
        type: 2,
        title: ['元器件选择', 'font-size:14px;'],
        area: ['700px', '450px'],
        offset: '10px',
        fixed: false, //不固定
        maxmin: true,
        content: '../IQC_Sop/SelyqjView',
        btn: ['关闭'],
        yes: function () {
            layer.closeAll();
        },
        end: function () {
        location.reload();
    }
        
    });
    return false;
}

//元器件数据拼接html
function yqjjsonhtml(strval) {
    var jsonStr = strval;
    $("#yqjcon").html("");
    var html = "";
    if (jsonStr != null) {
        if (typeof (jsonStr) != "undefined") {
            html = '<table class="layui-table">';
            html += '<colgroup>';
            html += '<col width="50">';
            html += '<col width="150">';
            html += '<col width="150">';
            html += '<col>';
            html += '</colgroup>';
            html += '<thead>';
            html += '<tr>';
            html += '<th><input type="checkbox" name="TopChecked" value="0" onclick="TopChecked()" /></th>';
            html += '<th>名称</th>';
            html += '<th>型号</th>';
            html += '<th>物料代码</th>';
            html += '</tr>';
            html += '</thead>';
            html += '<tbody>';
            for (var i = 0, j = jsonStr.length; i < j; i++) {
                html += '<tr>';
                html += '<td><input type="checkbox" name="ContentChecked" value="' + jsonStr[i].Id + '"/></td>';
                html += '<td>' + jsonStr[i].Y_Name + '</td>';
                html += '<td>' + jsonStr[i].Y_Ggxh + '</td>';
                html += '<td>' + jsonStr[i].Y_Dm + '</td>';
                html += '</tr>';
            }
            html += '</tbody>';
            html += '</table>';
        }
    }
    else {
       
        html = '<table class="layui-table">';
        html += '<tr>';
        html += '<td>没有查询出元器件,请填入条件查询~</td>';
        html += '</tr>';
        html += '</table>';
    }
    $("#yqjcon").html(html);
}

//查询元件数据ajax
function AJAXyqjjson()
{
    var paladat = $("#paladat").val();
    if (paladat == "")
    {
        layer.alert("条件不为空！", { icon: 2 });
        return false;
    }
    $.ajax({
        url: "../IQC_Sop/AJaxgetyjson",
        type: "POST",
        data: { paladat: paladat },
        dataType: "json",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            disLoading();
            var jsonstr = eval(response);
            yqjjsonhtml(jsonstr);
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
}

//
//保存选择的元器件 返回检验作业流程单的Id
function BtSaveyqj()
{
    var arr = GetGridSelete();
    if (arr.length > 1) {
        layer.alert("只能选择一个元器件！", { icon: 2 });
        return false;
    }
    if (arr.length <= 0) {
        layer.alert("请选择一个元器件！", { icon: 2 });
        return false;
    }
    var yqjId = arr[0];
    var json;
    $.ajax({
        url: "../IQC_Sop/GetsopId",
        type: "POST",
        data: { yqjId: yqjId },
        dataType: "html",
        async: true,
        beforeSend: function () {
            ddchen();
        },
        success: function (response) {
            disLoading();
            if (response == "0") {
                layer.alert("该元器件的来料检验作业标准已经存在！", { icon: 2 });
            }
            else if (response == "1") {
                layer.alert("系统不存在该元器件！", { icon: 2 });
            } else {
                layer.alert("选择成功，请在列表中进一步编辑！", { icon: 1 }, function () { Closeiform();});
            }
        },
        error: function (e) {
            disLoading();
            layer.alert("操作失败！", { icon: 2 });
        }
    })
 
}

//弹出提示框
function ts(val)
{
    layer.open({
         type: 1,
         content: '<div style="padding: 20px 100px;">保存成功</div>',
         btn: ['下一步'],
         btnAlign: 'c', //按钮居中
         shade: 0 ,//不显示遮罩
         yes: function () {
             layer.closeAll();
         },
         end: function () {
            SopjbinfoView();
        },
    });
}

//直接关闭所有弹出框
function Closeiform() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}

//打开作业流程单基本信息填写页面
function SopjbinfoView(val)
{
    layer.open({
        type: 2,
        title: ['作业流程标准单（SOP）', 'font-size:14px;'],
        area: ['800px', '450px'],
        offset: '10px',
        fixed: false, //不固定
        maxmin: true,
        content: '../IQC_Sop/SavesopjbInfoView?Id='+val,
    });
}

//列表中操作按钮
function listbtnshow(val)
{
    var id = "'" + val + "'";
    var s = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="SopjbinfoView('+id+')">编辑</a>';
    var t = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="SopcheakView(' + id + ')">查看</button>';
    var b = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="tjshsopEide(' + id + ')">提交</button>';
    var z = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="IQCZFajax(' + id + ')">作废</button>';
    var a = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="ckczji(' + id + ')">操作记录</button>';
    return s+b+t+z+a;
}

//审核列表中操作的按钮
function listshbtnshow(val)
{
    var id = "'" + val + "'";
    var t = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="shiframeopen('+id+')">审核</button>';
    var s = '<button class="layui-btn layui-btn-primary layui-btn-small" onclick="SopcheakView(' + id + ')">查看</button>';
    return t+s;
}

//检验内容列表中按钮
function listjyconbtnshow(val)
{
    var id = "'" + val + "'";
    var s = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="jyconupdateIframeopen(' + id + ')"><i class="layui-icon">&#xe642;</i></a>';
    var t = '<a class="layui-btn layui-btn-primary layui-btn-small" onclick="Deljycon(' + id + ')"><i class="layui-icon">&#xe640;</i></a>';
    return s+t;
}

//标准作废
function IQCZFajax(val)
{
    $.messager.confirm("操作提示", '确定要作废该检验标准单吗？', function (data) {
        if (data) {
            var str = ajaxzfbyId(val);
            if (str == "0") {
                layer.alert("提交成功！", { icon: 1 }, function () { location.reload(); });
                return false;
            }
            else {
                layer.alert("提交失败！", { icon: 2 }, function () { location.reload(); });
                return false;
            }
        }
    })
}
//作废提交
function ajaxzfbyId(val)
{
    var json;
    $.ajax({
        type: "POST",
        url: "/IQC_Sop/IQC_SopzfEdie",
        data: { Id: val },
        dataType: "html",
        async: false,
        success: function (reslut) {
            json = reslut;
        },
        error: function (e) {
            alert("网路异常，请重试！");
        }
    })
    return json;
}

//打开检验内容添加的方法
function jyconiframeopen(type,sopId) {
    layer.open({
        type: 2,
        title: ['检验内容添加', 'font-size:14px;'],
        area: ['800px', '350px'],
        fixed: false, //不固定
        maxmin: true,
        content: '../IQC_Sop/DQXNjyconAddView?type='+type+'&&sopId='+sopId,
 
        end: function () {
            location.reload();
        }
    })
}

//打开检验内容编辑
function jyconupdateIframeopen(Id) {
    layer.open({
        type: 2,
        title: ['检验内容编辑', 'font-size:14px;'],
        area: ['700px', '350px'],
        fixed: false, //不固定
        maxmin: true,
        content: '../IQC_Sop/JCconUpdateView?Id=' + Id,

        end: function () {
            location.reload();
        }
    })
}

//查看操作记录
function ckczji(val) {
    layer.open({
        type: 2,
        title: ['操作记录', 'font-size:14px;'],
        area: ['900px', '450px'],
        offset: '10px',
        fixed: false, //不固定
        maxmin: true,
        content: '../DKX_DDtypeinfo/LCjlckView?oId=' + val,
        end: function () {
            location.reload();
        }
    })
}

 function guanbi() {
        //layer.alert("保存成功！", { icon: 2 }, function () { layer.closeAll(); });
        //layer.closeAll();
        var index = parent.layer.getFrameIndex(window.name);
        //关闭弹出层
        parent.layer.close(index);
    }

//检验内容和检验仪器提交
 function AjaxjyconEide(val) {
        var sopId = $("#sopId").val();//作业单Id
        var type = $("#type").val();//检验项目
        var jycon = $("#jycon").val();//检验内容
        var jysb = $("#jysb").val();//检验设备
        var QDDJ = $("#qddj").val();//确定等级
        if (jycon == "")
        {
            layer.alert("检验内容不为空！", { icon: 2 });
            return false;
        }
        if (jysb == "")
        {
            layer.alert("检验设备仪器不为空！", { icon: 2 });
            return false;
        }
        if (QDDJ == "")
        {
            layer.alert("请选择缺点等级!");
            return false;
        }

        $.ajax({
            url: "../IQC_Sop/jyconaddEide",
            type: "POST",
            data: { type: type, sopId: sopId, jyconname: jycon, jyyq: jysb, QDDJ: QDDJ },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                if (val == "0")//继续添加
                {
                    if (response == "0") {
                        layer.alert("保存成功！", { icon: 1 }, function () { location.reload(); });
                    }
                    if (response == "1") {
                        layer.alert("保存失败！", { icon: 2 }, function () { location.reload(); });
                    }
                }
                else {
                    if (response == "0") {
                        layer.alert("保存成功！", { icon: 1 }, function () { guanbi(); });
                    }
                    if (response == "1") {
                        layer.alert("保存失败！", { icon: 2 }, function () { location.reload(); });
                    }
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

//检验内容编辑提交
    function AjaxjyconupdateEide()
    {
        var Id = $("#Id").val();
        var jycon = $("#jycon").val();//检验内容
        var jysb = $("#jysb").val();//检验设备
        var QDDJ = $("#qddj").val();//确定等级
        if (jycon == "") {
            layer.alert("检验内容不为空！", { icon: 2 });
            return false;
        }
        if (jysb == "") {
            layer.alert("检验设备仪器不为空！", { icon: 2 });
            return false;
        }
        if (QDDJ == "") {
            layer.alert("请选择缺点等级!");
            return false;
        }
        $.ajax({
            url: "../IQC_Sop/JCconupdateEide",
            type: "POST",
            data: {Id:Id, jyconname: jycon, jyyq: jysb, QDDJ: QDDJ },
            dataType: "html",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                if (response == "0") {
                    layer.alert("保存成功！", { icon: 1 }, function () { guanbi(); });
                }
                if (response == "1") {
                    layer.alert("保存失败！", { icon: 2 }, function () { location.reload(); });
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })

    }

// 检验内容HTML拼接
    function dqxnhtml(strval,type) {
        var jsonStr = strval;
        var html = "";
        if (jsonStr != null) {
            if (typeof (jsonStr) != "undefined") {
                html = '<table class="layui-table">';
                html += '<colgroup>';
                html += '<col width="10%">';
                html += '<col width="25%">';
                html += '<col width="20%">';
                html += '<col width="15%">';
                html += '<col width="10%">';
                html += '<col width="25%">';
                html += '<col>';
                html += '</colgroup>';
                html += '<thead>';
                html += '<tr>';
                html += '<th>序号</th>';
                html += '<th>检验内容</th>';
                html += '<th>设备仪器</th>';
                html += '<th>缺点等级</th>';
                html += '<th>免检</th>';
                html += '<th>操作</th>';
                html += '</tr>';
                html += '</thead>';
                for (var i = 0, j = jsonStr.length; i < j; i++) {
                    var xh = i + 1;
                    html += '<tr>';
                    html += '<td>'+xh+'</td>';
                    html += '<td>' + jsonStr[i].Jyname + '</td>';
                    html += '<td>' + jsonStr[i].Jyyqname + '</td>';
                    html += '<td>' + qqdjshow(jsonStr[i].QDDJ) + '</td>';
                    html += '<td style="color:red; font-weight:bolder">' + Ismjshow(jsonStr[i].Ismj) + '</td>';
                    html += '<td>' + listjyconbtnshow(jsonStr[i].Id) + '</td>';
                    html += '</tr>';
                }
                html += '<tbody>';
                html += '</tbody>';
                html += '</table>';
            }
        }
        else {

            html = '<table class="layui-table">';
            html += '<tr>';
            html += '<td>没有检验内容</td>';
            html += '</tr>';
            html += '</table>';
        }
        if (type == "0") {
            $("#dqxncon").html(html);
        }
        if (type == "1") {
            $("#cccon").html(html);
        }
        if (type == "2") {
            $("#wgcon").html(html);
        }
        if (type == "3") {
            $("#kkxcon").html(html);
        }
        if (type == "4") {
            $("#qtcon").html(html);
        }
    }

//检验内容查看HTML拼接
    function cheakdqxnhtml(strval, type) {
        var jsonStr = strval;
        var html = "";
        if (jsonStr != null) {
            if (typeof (jsonStr) != "undefined") {
                html = '<table class="layui-table">';
                html += '<colgroup>';
                html += '<col width="100">';
                html += '<col width="250">';
                html += '<col width="100">';
               // html += '<col width="200">';
                html += '<col>';
                html += '</colgroup>';
                html += '<thead>';
                html += '<tr>';
                html += '<th>序号</th>';
                html += '<th>检验内容</th>';
                html += '<th>设备仪器</th>';
                html += '<th>缺点等级</th>';
                html += '<th>是否免检</th>';
                //html += '<th>操作</th>';
                html += '</tr>';
                html += '</thead>';
                for (var i = 0, j = jsonStr.length; i < j; i++) {
                    var xh = i + 1;
                    html += '<tr>';
                    html += '<td>' + xh + '</td>';
                    html += '<td>' + jsonStr[i].Jyname + '</td>';
                    html += '<td>' + jsonStr[i].Jyyqname + '</td>';
                    html += '<td>' + qqdjshow(jsonStr[i].QDDJ) + '</td>';
                    html += '<td style="color:red; font-weight:bolder">' + Ismjshow(jsonStr[i].Ismj) + '</td>';
                    //html += '<td>' + listjyconbtnshow(jsonStr[i].Id) + '</td>';
                    html += '</tr>';
                }
                html += '<tbody>';
                html += '</tbody>';
                html += '</table>';
            }
        }
        else {

            html = '<table class="layui-table">';
            html += '<tr>';
            html += '<td>没有检验内容</td>';
            html += '</tr>';
            html += '</table>';
        }
        if (type == "0") {
            $("#dqxncon").html(html);
        }
        if (type == "1") {
            $("#cccon").html(html);
        }
        if (type == "2") {
            $("#wgcon").html(html);
        }
        if (type == "3") {
            $("#kkxcon").html(html);
        }
        if (type == "4") {
            $("#qtcon").html(html);
        }
    }
//检验内容的数据
    function ajaxjyconjson(type, sopId,ymtype)
    {
        $.ajax({
            url: "../IQC_Sop/ajaxjyconjson",
            type: "POST",
            data: { sopId: sopId, type: type },
            dataType: "json",
            async: true,
            beforeSend: function () {
                ddchen();
            },
            success: function (response) {
                disLoading();
                var jsonstr = eval(response);
                if (ymtype == "1")
                {
                    cheakdqxnhtml(jsonstr, type);
                }
                else
                {
                    dqxnhtml(jsonstr, type);
                }
            },
            error: function (e) {
                disLoading();
                layer.alert("操作失败！", { icon: 2 });
            }
        })
    }

//打开检验方法添加的方法
    function jyffiframeopen(type, sopId) {
        layer.open({
            type: 2,
            title: ['检验方法添加', 'font-size:14px;'],
            area: ['700px', '350px'],
            fixed: false, //不固定
            maxmin: true,
            content: '../IQC_Sop/JyffAddView?type=' + type + '&&sopId=' + sopId,

            end: function () {
                location.reload();
            }
        })
    }

//检验方法数据
    function Ajaxjyffjson(type, sopId)
    {
        var json;
        $.ajax({
            url: "../IQC_Sop/jyffjson",
            type: "POST",
            data: { type: type, sopId: sopId},
            dataType: "json",
            async: false,
            success: function (response) {
                json = response;
            },
            error: function (e) {
                layer.alert("操作失败！" + e, { icon: 2 });
            }
        })
        return json;
    }

//删除检验内容
    function Deljycon(val)
    {
        //询问框
        layer.confirm('确定要删除吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            //layer.msg('的确很重要', { icon: 1 });
            $.ajax({
                url: "../IQC_Sop/jyconDel",
                type: "POST",
                data: {Id:val},
                dataType: "html",
                async: true,
                beforeSend: function () {
                    ddchen();
                },
                success: function (response) {
                    disLoading();
                    layer.alert('删除陈功', { icon: 1 }, function () { location.reload(); });
                },
                error: function (e) {
                    disLoading();
                    layer.msg("操作失败！", { icon: 2 });
                }
            })
        });
    }

//技术规格书查看
   function jsggimg(val) {
  
        //layer.open({
        //    type: 2,
        //    title: false,
        //    area: ['530px', '460px'],
        //    shade: 0.8,
        //    closeBtn: 0,
        //    shadeClose: true,
        //    content: val,
        //});
       //layer.msg('点击任意处关闭');
       var newwin = window.open("", val)
       //newwin.document.write(val);
       newwin.location.href = val;

      // window.open("pdf", "PDF 的标题", "height=100, width=400, toolbar= no, menubar=no, scrollbars=no, resizable=no, location=no, status=no,top=100,left=300")
   }

//作业流程单状态显示
   function zydztshow(val)
   {
       if (val == "-1")
           return "未提交";
       if (val == "0")
           return "待审核";
       if (val == "1")
           return "已通过";
       if (val == "2")
           return "未通过";
   }

//审核页面跳转
   function shiframeopen(val) {
       layer.open({
           type: 2,
           title: ['审核页面', 'font-size:14px;'],
           area: ['700px', '450px'],
           offset: '10px',
           fixed: false, //不固定
           maxmin: true,
           content: '../IQC_Sop/SopshView?Id='+val,
           
           end: function () {
               location.reload();
           }
       })
   }

//审核提交
   function shsopEide()
   {
       var sopId = $("#sopId").val();
       var issh = $("#istg").val();
       $.ajax({
           url: "../IQC_Sop/SopshEide",
           type: "POST",
           data: { Id: sopId, Issh: issh },
           dataType: "html",
           async: true,
           beforeSend: function () {
               ddchen();
           },
           success: function (response) {
               disLoading();
               if (response == "0") {
                   layer.alert("提交成功！", { icon: 1 }, function () { guanbi(); });
               }
               if (response == "1") {
                   layer.alert("操作失败！", { icon: 2 }, function () { Closeiform(); });
               }
           },
           error: function (e) {
               disLoading();
               layer.alert("网络异常！", { icon: 2 }, function () { Closeiform(); });
           }
       })
   }

//sop页面查看
   function SopcheakView(val)
   {
       layer.open({
           type: 2,
           title: ['作业流程标准单（SOP）查看', 'font-size:14px;'],
           area: ['700px', '450px'],
           offset: '10px',
           fixed: false, //不固定
           maxmin: true,
           content: '../IQC_Sop/CheaksopView?Id=' + val,
       })
   }

//缺点等级显示
   function qqdjshow(val)
   {
       if (val == "0")
       {
           return "CR(0)";
       }
       else if (val == "0.65")
       {
           return "MA(0.65)";
       }
       else if (val == "1.5") {
           return "MI(1.5)";
       } else {
           return "未编辑";
       }
   }

//是否免检
   function Ismjshow(val)
   {
       if (val == "1") {
           return '<span color="red">免检</span>'
       }
       else {
           return "-";
       }
   }

//提交审核
   function tjshsopEide(val)
   {
       $.ajax({
           url: "../IQC_Sop/SoptjshEide",
           type: "POST",
           data: { Id: val},
           dataType: "html",
           async: true,
           beforeSend: function () {
               ddchen();
           },
           success: function (response) {
               disLoading();
               if (response == "3") {
                   layer.alert("提交成功！", { icon: 1 }, function () { location.reload(); });
               }
               if (response == "2") {
                   layer.alert("操作失败！", { icon: 2 });
               }
               if (response == "1") {
                   layer.alert("该状态下无需提交审核！", { icon: 2 });
               }
               if (response == "0") {
                   layer.alert("提交异常！", { icon: 2 });
               }
           },
           error: function (e) {
               disLoading();
               layer.alert("网络异常！", { icon: 2 });
           }
       })
   }