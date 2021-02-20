//查询全部的免疫点信息
function GetallIc() {
    var keyu_data = [];
    $.ajax({
        type: "POST",
        url: "/Api/SysWebApi/GetAllMY",
        async: false,
        dataType: 'json',
        success: function (reslut) {
        
            keyu_data = eval('(' + reslut + ')');
     
        }
       
    })
    return keyu_data;
}
 