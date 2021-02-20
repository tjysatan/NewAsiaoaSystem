// JavaScript Document
function DivHeight(obj) {
    var cwin = obj;
    var ff = 0;
    var opera = 0;
    var ie = 0;
    if (document.getElementById) {
        if (cwin && !window.opera) {
            if (cwin.contentDocument && cwin.contentWindow.document.documentElement) {
                ff = cwin.contentWindow.document.documentElement.scrollHeight; //FF　NS
                //cwin.width=cwin.contentDocument.body.offsetWidth+20;           
            }
            if (cwin.contentWindow.document && cwin.contentWindow.document.body && cwin.contentWindow.document.body.scrollHeight)
                opera = cwin.contentWindow.document.body.scrollHeight; //Opera

            if (cwin.Document && cwin.contentWindow.document.documentElement && cwin.contentWindow.document.documentElement.scrollHeight) {
                ie = cwin.contentWindow.document.documentElement.scrollHeight;
                //                    cwin.height = cwin.contentWindow.document.documentElement.scrollHeight; //IE
                //cwin.width=cwin.Document.body.scrollWidth　+　10;
            }
        }
        else {
            if (cwin.contentWindow.document && cwin.contentWindow.document.body.scrollHeight)
                opera = cwin.contentWindow.document.body.scrollHeight; //Opera
        }
        if (ff > opera) {
            if (ff > ie)
                cwin.height = ff;
            else
                cwin.height = ie;
        } else if (opera > ie)
            cwin.height = opera;
        else
            cwin.height = ie;
        if (cwin.height < 450)
            cwin.height = 450;
    }
    //    if (cwin && !window.opera) {
    //        if (cwin.contentDocument && cwin.contentWindow.document.documentElement) {//cwin.contentDocument && cwin.contentDocument.body.offsetHeight
    //        cwin.height = cwin.contentDocument.body.offsetHeight +20; 
    //        }
    //    else if (cwin.Document && cwin.contentWindow.document.documentElement && cwin.contentWindow.document.body.scrollHeight) { //如果用户的浏览器是IE && cwin.Document.body.scrollHeight 
    //        cwin.height = cwin.contentWindow.document.body.scrollHeight;
    //        }
    //    }
}


