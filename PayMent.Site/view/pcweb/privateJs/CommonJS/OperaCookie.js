function setCookie(name, value) { //写入cookie
    document.cookie = name + '=' + encodeURIComponent(value);
}
function getCookie(name) { //保存cookie
    var arr = document.cookie.split('; ');
    var i = 0;
    for (i = 0; i < arr.length; i++) {
        //arr2->['username', 'abc']
        var arr2 = arr[i].split('=');

        if (arr2[0] == name) {
            var getC = decodeURIComponent(arr2[1]);
            return getC;
        }
    }
    return '';
}
function removeCookie(name) {  //删除cookie
    setCookie(name, '1', -1);
}


function NewremoveCookie(name) {  //删除cookie
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = NewgetCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}


function NewgetCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return ""
}
function NewsetCookie(c_name, value, expiredays) {
    var time = expiredays;
    alert(expiredays);
    var exdate = new Date();
    alert(exdate.getTime());
    exdate.setTime(exdate.getTime() + time * 24 * 3600 * 1000);
    alert(exdate.getTime() + time * 24 * 3600 * 1000);
    alert(exdate.getTime());
    alert(c_name + "==" + value + "----" + exdate.toGMTString());
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == undefined) ? ";" : ";expires=" + exdate.toGMTString()) + ";Path=/";
}
