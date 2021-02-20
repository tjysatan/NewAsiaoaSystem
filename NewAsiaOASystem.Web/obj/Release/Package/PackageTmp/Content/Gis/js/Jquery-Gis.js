
function InitializeMap(lat, lng)
{
   
    // BMap.Convertor.translate(gpsPoint, 0, initMap)
    var gpsPoint = new BMap.Point(lat, lng);
    BMap.Convertor.translate(gpsPoint, 0, translateCallback);
    //坐标转换完之后的回调函数
       function translateCallback (point) {
        // 百度地图API功能
        var map = new BMap.Map("allmap");    // 创建Map实例
        map.centerAndZoom(point, 14);  // 初始化地图,设置中心点坐标和地图级别
           //     map.addOverlay(new BMap.Circle(point, 500));//画圆
           //计算距离
        //function GetDistancejl(lat_a, lng_a, lat_b, lng_b) {
        //    var pointA = new BMap.Point(lat_a, lng_a); 
        //    var pointB = new BMap.Point(lat_b, lng_b);
        //    var s = (map.getDistance(pointA, pointB)).toFixed(0);
        //    return s;
        //}

        var Ic_data = new Array();//全部免疫点信息
        var Ic_Distance = new Array();//当前位置距离全部免疫点距离的数组
        var Ic_Distance3 = new Array();//最近的三个距离的数组(500米范围内的)
        var Ic_data2 = new Array();//最近三个免疫点信息（500范围内的免疫点信息）
       
        if (Ic_data.length > 0) {
            Ic_data = new Array();
        }
        Ic_data = GetallIc();
        var xb_f = 0;//500米范围内距离数组的下标
        var xb_y = 0;//500米范围内免疫点的下标
        ///计算全部距离 且取出 500范围内全部免疫点的信息
        //for (var i = 0, j = Ic_data.length; i < j; i++) {
        //    Ic_Distance[i] = GetDistancejl(point.lng,point.lat, Ic_data[i].D_Lat, Ic_data[i].D_lon);
        //    if (Ic_Distance[i] <= 500) {
        //        Ic_data2[xb_y] = Ic_data[i];
        //        xb_y++;
        //    }
        //}
       
        var data3 = new Array();
           //  data3 = Ic_data2;
      
        data3 = Ic_data;
        var opts = {
             width: 300,     // 信息窗口宽度
          //  height: 130,     // 信息窗口高度
         //   title: "免疫点信息", // 信息窗口标题
            enableMessage: true//设置允许信息窗发送短息
        };
        for (var i = 0; i < data3.length; i++) {
            var marker = new BMap.Marker(new BMap.Point(data3[i].D_Lat, data3[i].D_lon));
            //免疫点负责人
            var Person;
            //免疫点联系方式
            var phone;
            //免疫点地址
            var address;
            //免疫点备注信息
            var Remarks;
            if (data3[i].DisPerson != null) {
                Person = data3[i].DisPerson;
            }
            else {
                Person = "无";
            }
            if (data3[i].DisPhone != null) {
                phone = data3[i].DisPhone;
            }
            else {
                phone = "无";
            }
            if (data3[i].DisAddress != null) {
                address = data3[i].DisAddress;
            }
            else {
                address = "无";
            }
            if (data3[i].Description != null) {
                Remarks = data3[i].Description;
            } else {
                Remarks = "无";
            }

          // var content = "名称：" + data3[i].Name + "<br/>电话：" + phone + ",负责人：" + Person + "<br/>地址：" + address + "<br/><hr/><span style='color:red'>" + Remarks + "</span>";
            var content = "<h6 style='margin:0 0 5px 0;padding:0.2em 0'>" + data3[i].Name + "</h4>" +
                                "<table><tr><td style='font-size:12px;font-weight:bold'>电&nbsp;&nbsp;话:</td><td  style='font-size:12px;'>" + phone + "</td></tr>" +
                                "<tr><td style='font-size:12px;font-weight:bold'>负责人:</td><td  style='font-size:12px;'>" + Person + "</td></tr>" +
                                  "<tr><td style='font-size:12px;font-weight:bold'>地&nbsp;&nbsp;址:</td><td  style='font-size:12px;'>" + address + "</td></tr>" +
                                    "<tr><td style='font-size:12px;font-weight:bold'>备&nbsp;&nbsp;注:</td><td  style='font-size:12px;'></td></tr>" +
                                "</table>" +
                                "<p style='margin:0;line-height:1.5;font-size:13px;text-indent:1em'>" + Remarks + "</p>";
            map.addOverlay(marker); // 将标注添加到地图中
            addClickHandler(content, marker);
        }


        function addClickHandler(content, marker) {
            marker.addEventListener("click", function (e) {
                openInfo(content, e)
            }
            );
        }
        function openInfo(content, e) {
            var p = e.target;
            var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
            var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
            map.openInfoWindow(infoWindow, point); //开启信息窗口
        }
    }
   



}


 

//function rad(d) {
//    return d * Math.PI / 180.0;
//}

//function GetDistance(lat1, lon1, lat2, lon2) {
//    var EARTH_RADIUS = 6378.137;
//    var radLat1 = rad(lat1);
//    var radLat2 = rad(lat2);
//    var a = radLat1 - radLat2;
//    var b = rad(lon1) - rad(lon2);
//    var s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(b / 2), 2)));
//    s = s * EARTH_RADIUS;
//    s *= 1000;
//    return s;
//}


