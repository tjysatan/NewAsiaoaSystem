$(function () {
    GetmenJson();
    //$("body").attr("class", "home");
})


//function test() {
//    var windowHeight = $(window).height();
//    $(document.body).css("height", windowHeight);

//    $(".h-main").css("height", windowHeight);
//    $(".h-down").css("height", windowHeight * 0.88);

//    $(".d_div_l").css("height", windowHeight * 0.88);
//    $(".d_div_r").css("height", windowHeight * 0.88);
//    $(".r-frame").css("height", windowHeight * 0.88);
//}

//菜单被选中后控制隐藏与显示
function SwitchMenu(e, menuId) {
    if ($(e).attr("val") == 1) {
        $(".submenu").css("display", "none");
        $(e).attr("val", "0");
    }
    else {
        $(".submenu").css("display", "none");
        $("#" + menuId).css("display", "block");
        $(e).attr("val", "1");
    }
}

//ajax请求获取菜单数据
function GetmenJson() {
    $.ajax({
        type: "POST",
        url: "/Admin/GetleftnavMenu",
        dataType: "json",
        async: false,
        success: function (context) {
            var data = context.result;
            //InitLeftMenu(data);
            NewInitleftMenu(data)
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}

//初始化左侧菜单
function InitLeftMenu(menu) {
    $(".menuContent").html("");
    if (menu != null && menu != "") {
        var childen = eval('(' + menu + ')');
        $.each(childen, function (i, n) {
            var html = '<div class="menutitle" onclick=\'SwitchMenu(this,"menu' + i + '")\' val="0">';
            if (n.Ico != null && n.Ico != "") {
                html = html + '<span class="h-left-icon ' + n.Ico + '">';
            }
            else
                html = html + '<span class="h-left-icon h-left-icon2">';
            html = html + '</span><span>' + n.Name + '</span></div>';
            $(".menuContent").append(html);
            if (n.children != undefined && n.children != null) {
                var testHtml = "";
                testHtml = '<span class="submenu" id="menu' + i + '" style="display:none"><ul>';
                $.each(n.children, function (j, o) {
                    //testHtml += '<li><a ref="' + o.Id + '" href="' + o.Url + '" target="right"><span class="sbu-dot"></span>' + o.Name + '</a></li>';
                    testHtml += '<li><a ref="' + o.Id + '" onclick="addTab(\'' + o.Name + '\',\'' + o.Url + '\')" style="cursor:pointer"><span class="sbu-dot"></span>' + o.Name + '</a></li>';
                })
                testHtml += '</ul></span>';
                $(".menuContent").append(testHtml);
            }

        });
    }
}


//New初始化左侧菜单
function NewInitleftMenu(menu) {
    $(".sidebar-menu").html("");
    var html = '<li class="sub-menu"><a class="" href="index.html" onclick="" target="MainIframe"><i class="icon-dashboard"></i><span>菜单</span> </a></li>';
    if (menu != null && menu != "") {
        var childen = eval('(' + menu + ')');
        $.each(childen, function (i, n) {
           
            html += '<li class="sub-menu">';
            html += '<a href="javascript:;" class="">';
            html += '  <i class="icon-book"></i>';
            html += '<span>' + n.Name + '</span><span class="arrow"></span>';
            html += ' </a>';
            //$(".sidebar-menu").append(html);
            if (n.children != undefined && n.children != null) {
                var testHtml = "";
                testHtml += '<ul class="sub">';
                $.each(n.children, function (j, o) {
                    testHtml += '<li><a class="" href="' + o.Url + '" target="MainIframe">' + o.Name + '</a></li>';
                });
                testHtml += '</ul>';
                //$(".sidebar-menu").append(testHtml);
                //alert($(".sidebar-menu").append());
                html += testHtml;
            }
            html += '</li>';
         
        });
        $(".sidebar-menu").append(html);
    }
 
}