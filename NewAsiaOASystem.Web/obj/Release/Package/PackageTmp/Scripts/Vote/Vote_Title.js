
//保存表单
function SavaForm() {
    var T_nam = $("#S_ListData").val();
    var S_title = $("#T_Name").val();
    if (T_nam == "")
    {
        alert("请选择主题！");
        return false;
    }
    if (S_title == "") {
        alert("标题名称不为空！");
        return false;
    }
 
        var qname="";
        for (var i = 0; i < 9; i++) {
            if ($("#Q_Name" + i).val().replace(/^\s+/, "") != "")
            {
                qname = qname.replace(/^\s+/, "") + $("#Q_Name" + i).val().replace(/^\s+/, "") + "|";
            }
        }
        if (qname != "") {
            $("#Q_Name").html(qname);
        
        }
        else {
            alert("选项名称不能为空！");
            return false;
        }
       

   
    return submitForm('/Vote_Title/Index');
}

/*编辑标题的时候，获取并且给选项名称赋值和保存选项id*/
function showEdit(val)
{
    var id = $("#Id").val();
    var qname = "";
    var qid = "";
    if (id != null)
    {
        var data = AjaxQuestion(id);
        if (data != null)
        {
            for (var i = 0, j = data.length; i < j; i++) {
                qname =data[i].Q_Question;
                $("#Q_Name"+i).val(qname)
                qid = qid +"'"+ data[i].Id + "',";
            }
            qid = qid.substring(0, qid.length - 1);
            $("#Q_id").html(qid);
        }
    }
}

function AjaxQuestion(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/VoteWebApi/GetVoteQuestionby_tid",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json;
}


/*根据到期日期主题的状态*/
function show_QX(val) {
    var arr = val.split("/");
    var starttime = new Date(arr[0], arr[1], arr[2]);
    var starttimes = starttime.getTime();
    //获取当前时间
    var myDate = new Date();
    var data = myDate.toLocaleDateString();
    var arr1 = data.split("/")
    var lktime= new Date(arr1[0], arr1[1], arr1[2]);
    var lktimes = lktime.getTime();
    if (starttimes > lktimes) {
        return "正常";
    }
    else {
        return "<span style='color:red'>到期</span>";
    }
}


/*
T_type转换值
*/
function showT_type(val) {
    if (val == "1") {
        return '多选';
    }
    else {
        return '单选';
    }
}


function showS_name(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        return AjaxS_Name(val)
    }
}


function AjaxS_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/VoteWebApi/GetVoteSnameby_Id",
        data: { "": Id },
        dataType: "json",
        async: false,
        success: function (reslut) {
            json = eval('(' + reslut + ')');
        },
        error: function (e) {
            alert("请求失败");
        }
    })
    return json.S_title;
}


    
 