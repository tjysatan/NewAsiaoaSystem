
//保存表单
function SavaForm() {
    var S_title = $("#S_title").val();
    var S_time = $("#S_time").val();
    if (S_title == "") {
        alert("主题名称不为空！");
        return false;
    }

    if (S_time == "") {
        alert("到期时间不为空！");
        return false;
    }

    return submitForm('/Vote_Subject/Index');
}


/*通过选项票数/参数人数 */
function showL(ps, rs) {
    
        var L;
        L = 80 * (ps / rs)
}

/*通过选项票数/参数人数 */

function showB(ps, rs) {
    var B;
    if (rs != "0") {
        B = ps / rs * 100;
        B = B.toFixed(2) + "%";
    } else {
        B = "0%";
    }
        return B;
 
}

//保存表单
function SavaVoteForm() {
    return submitForm('/Vote_Subject/Index');
}

/*根据到期日期主题的状态*/
function show_QX(val) {
    var qx = showQX(val);
    var arr = qx.split("-");
    var starttime = new Date(arr[0], arr[1], arr[2]);
    var starttimes = starttime.getTime();
  
    //获取当前时间
    var myDate = new Date();
    var data = myDate.toLocaleDateString();
    alert(data);
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

function show_Vtype(val)
{
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        if (val == "0") {
            return "公共问卷"
        }
        else {
            return "内部问卷"
        }
    }
}


function showQX(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        var date = val.substring(0, 10);
        return date;
    }
    return "";

}

function AjaxS_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/VoteWebApi/GetdataVoteSnameby_Id",
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

function AjaxS_alllist(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/VoteWebApi/GetVoteSallList",
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

/*根据标题的ID 查询所有选项*/
function AjaxS_Tid(Id) {
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

function Showurl(Id,openid) {
    var url = "VoteView?Id=" + Id + "&&openId=" + openid;
    return url
}

 





    
 