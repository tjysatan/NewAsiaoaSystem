/**
 * Created by Administrator on 2016/1/11.
 */
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
            //zk();
        },
        error: function (e) {
            alert("请求失败");
        }
    })
}

//New初始化左侧菜单
function NewInitleftMenu(menu) {
    $(".sidebar-menu").html("");
    var zcname = "主菜单";
    var html = '<li class="sub-menu"><a class="" href="/Admin/Index" onclick="" target="_parent">';
      html+='<i class="icon-dashboard"></i><span>'+zcname+'</span> </a></li>';
    if (menu != null && menu != "") {
        var childen = eval('(' + menu + ')');
        $.each(childen, function (i, n) {
            html += '<li class="sub-menu">';
            html += '<a href="javascript:;" class="">';
            //html += '  <i class="icon-book"></i>';
            if (n.Ico != "" && n.Ico!=null)
                html += '<i class="' + n.Ico + '"></i>';
            else
                html += '  <i class="icon-book"></i>';
            html += '<span>' + n.Name + '</span><span class="arrow"></span>';
            html += ' </a>';
            if (n.children != undefined && n.children != null) {
                var testHtml = "";
                testHtml += '<ul class="sub">';
                $.each(n.children, function (j, o) {
                    testHtml += '<li><a class="" onclick="addTab(\'' + o.Name + '\',\'' + o.Url + '\')" style="cursor:pointer">' + o.Name + '</a></li>';
                });
                testHtml += '</ul>';
                html += testHtml;
            }
            html += '</li>';

        });
        $(".sidebar-menu").append(html);
    }
}


//加载左侧菜单伸缩功能
var Script = function () {
    GetmenJson();//菜单数据
    //    sidebar dropdown menu
    jQuery('#sidebar .sub-menu > a').click(function () {
        var last = jQuery('.sub-menu.open', $('#sidebar'));
        last.removeClass("open");
        jQuery('.arrow', last).removeClass("open");
        jQuery('.sub', last).slideUp(200);
        var sub = jQuery(this).next();
        if (sub.is(":visible")) {
            jQuery('.arrow', jQuery(this)).removeClass("open");
            jQuery(this).parent().removeClass("open");
            sub.slideUp(200);
        } else {
            jQuery('.arrow', jQuery(this)).addClass("open");
            jQuery(this).parent().addClass("open");
            sub.slideDown(200);
        }
        var o = ($(this).offset());
        diff = 200 - o.top;
        //alert(diff)
        return;
        if (diff > 0)
            $(".sidebar-scroll").scrollTo("-=" + Math.abs(diff), 500);
        else
            $(".sidebar-scroll").scrollTo("+=" + Math.abs(diff), 500);
    });

    //    sidebar toggle

    $('.icon-reorder').click(function () {
        if ($('#sidebar > ul').is(":visible") === true) {
            $('#main-content').css({
                'margin-left': '0px'
            });
            $('#sidebar').css({
                'margin-left': '-180px'
            });
            $('#sidebar > ul').hide();
            $("#container").addClass("sidebar-closed");
        } else {
            $('#main-content').css({
                'margin-left': '180px'
            });
            $('#sidebar > ul').show();
            $('#sidebar').css({
                'margin-left': '0'
            });
            $("#container").removeClass("sidebar-closed");
        }
    });

    // custom scrollbar
    $(".sidebar-scroll").niceScroll({ styler: "fb", cursorcolor: "#4A8BC2", cursorwidth: '5', cursorborderradius: '0px', background: '#404040', cursorborder: '' });

    $(".portlet-scroll-1").niceScroll({ styler: "fb", cursorcolor: "#4A8BC2", cursorwidth: '5', cursorborderradius: '0px', background: '#404040', cursorborder: '' });

    $(".portlet-scroll-2").niceScroll({ styler: "fb", cursorcolor: "#4A8BC2", cursorwidth: '5', cursorborderradius: '0px', autohidemode: false, cursorborder: '' });

    $(".portlet-scroll-3").niceScroll({ styler: "fb", cursorcolor: "#4A8BC2", cursorwidth: '5', cursorborderradius: '0px', background: '#404040', autohidemode: false, cursorborder: '' });

    $("html").niceScroll({ styler: "fb", cursorcolor: "#4A8BC2", cursorwidth: '8', cursorborderradius: '0px', background: '#404040', cursorborder: '', zindex: '1000' });


    // theme switcher

    var scrollHeight = '60px';
    jQuery('#theme-change').click(function () {
        if ($(this).attr("opened") && !$(this).attr("opening") && !$(this).attr("closing")) {
            $(this).removeAttr("opened");
            $(this).attr("closing", "1");

            $("#theme-change").css("overflow", "hidden").animate({
                width: '20px',
                height: '22px',
                'padding-top': '3px'
            }, {
                complete: function () {
                    $(this).removeAttr("closing");
                    $("#theme-change .settings").hide();
                }
            });
        } else if (!$(this).attr("closing") && !$(this).attr("opening")) {
            $(this).attr("opening", "1");
            $("#theme-change").css("overflow", "visible").animate({
                width: '226px',
                height: scrollHeight,
                'padding-top': '3px'
            }, {
                complete: function () {
                    $(this).removeAttr("opening");
                    $(this).attr("opened", 1);
                }
            });
            $("#theme-change .settings").show();
        }
    });

    jQuery('#theme-change .colors span').click(function () {
        var color = $(this).attr("data-style");
        setColor(color);
    });

    jQuery('#theme-change .layout input').change(function () {
        setLayout();
    });

    var setColor = function (color) {
        $('#style_color').attr("href", "css/style-" + color + ".css");
    }

    // widget tools

    jQuery('.widget .tools .icon-chevron-down').click(function () {
        var el = jQuery(this).parents(".widget").children(".widget-body");
        if (jQuery(this).hasClass("icon-chevron-down")) {
            jQuery(this).removeClass("icon-chevron-down").addClass("icon-chevron-up");
            el.slideUp(200);
        } else {
            jQuery(this).removeClass("icon-chevron-up").addClass("icon-chevron-down");
            el.slideDown(200);
        }
    });

    jQuery('.widget .tools .icon-remove').click(function () {
        jQuery(this).parents(".widget").parent().remove();
    });

    //    tool tips

    $('.element').tooltip();

    $('.tooltips').tooltip();

    //    popovers

    $('.popovers').popover();

    // scroller

    /*  $('.scroller').slimscroll({
          height: 'auto'
      });*/



}();