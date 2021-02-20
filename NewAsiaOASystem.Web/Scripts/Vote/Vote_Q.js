
//保存表单
function SavaForm() {
 
    return submitForm('/Vote_Title/Index');
}

 
function showS_name(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        return AjaxS_Name(val)
    }
}

//ajax获取绑定用户的 信息
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

function showT_name(val) {
    if (null != val && "null" != val && undefined != val && "undefined" != val) {
        return AjaxT_Name(val)
    }
}

//ajax获取绑定用户的 信息
function AjaxT_Name(Id) {
    var json;
    $.ajax({
        type: "POST",
        url: "/Api/VoteWebApi/GetVoteTnameby_Id",
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
    return json.T_Name;
}


  function DRP() {
        $("#S_ListData").change(function () {
            var url = "/Vote_Question/Vote_TitleName/" + $("#S_ListData").val();  //规则是 控制器/方法/参数
            $.getJSON(
               url,
               function (data) {
                 
                   $("#aa").html('');
                   $("<option value='-1' selected='selected'>==请选择==</option>").appendTo("#msd5");
                   $.each(data, function (i, item) {
                       $("#aa").append($("<option></option>").val(item.Id).html(item.T_Name));
                   });
               });
        });
  }

  function Editdrp() {
      var url = "/Vote_Question/Vote_TitleName/" + $("#S_ListData").val();  //规则是 控制器/方法/参数
      $.getJSON(
         url,
         function (data) {

             $("#aa").html('');
             $("<option value='-1' selected='selected'>==请选择==</option>").appendTo("#msd5");
             $.each(data, function (i, item) {
                 $("#aa").append($("<option></option>").val(item.Id).html(item.T_Name));
             });
         });
  }
 
    
 