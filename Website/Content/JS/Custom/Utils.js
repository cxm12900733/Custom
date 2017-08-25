/*
==================================================================
程序：js基础函数工具包
创建：键盘男
说明：js基础函数工具包，纯js代码，提供常用的 js 工具方法。
最近更新：16:46 2015-06-10
==================================================================
*/

var utils = {};
utils.DateTime = {};

/****************************************************************************************************
分类：字符串类函数
****************************************************************************************************/
utils.IfShow = function (ShowMsg) {
    if (ShowMsg == "" || ShowMsg == null) {
        return false;
    }
    else {
        return true;
    }

}
/*
==================================================================
功能：格式化字符串
使用：formatString("{0}-{1}","a","b");
返回：string
==================================================================
*/
utils.formatString = function () {
    for (var i = 1; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        arguments[0] = arguments[0].replace(exp, arguments[i]);
    }
    return arguments[0];
};

/*
==================================================================
功能：生成随机字符串，默认32位
使用：randomString(32)
返回：string
==================================================================
*/
utils.randomString = function (len) {
    len = len || 32;
    var $chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';    /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
    var maxPos = $chars.length;
    var pwd = '';
    for (i = 0; i < len; i++) {
        pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return pwd;
}


/*
==================================================================
功能：格式化时间显示方式
使用：formatDate(datevalue,"yyyy-MM-dd hh:mm:ss");
返回：string
==================================================================
*/
utils.formatDate = function (v, format) {
    if (!v) return "";
    var d = v;
    if (typeof v === 'string') {
        if (v.indexOf("/Date(") > -1)
            d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
        else
            d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
    }
    var o = {
        "M+": d.getMonth() + 1,  //month
        "d+": d.getDate(),       //day
        "h+": d.getHours(),      //hour
        "m+": d.getMinutes(),    //minute
        "s+": d.getSeconds(),    //second
        "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
        "S": d.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

/*
==================================================================
功能：格式化时间显示方式
使用：
* formatNumber(12345.999,'#,##0.00');  
* formatNumber(12345.999,'#,##0.##');  
* formatNumber(123,'000000');
返回：string
==================================================================
*/
utils.formatNumber = function (v, pattern) {
    if (v == null)
        return v;
    var strarr = v ? v.toString().split('.') : ['0'];
    var fmtarr = pattern ? pattern.split('.') : [''];
    var retstr = '';
    // 整数部分   
    var str = strarr[0];
    var fmt = fmtarr[0];
    var i = str.length - 1;
    var comma = false;
    for (var f = fmt.length - 1; f >= 0; f--) {
        switch (fmt.substr(f, 1)) {
            case '#':
                if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                break;
            case '0':
                if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                else retstr = '0' + retstr;
                break;
            case ',':
                comma = true;
                retstr = ',' + retstr;
                break;
        }
    }
    if (i >= 0) {
        if (comma) {
            var l = str.length;
            for (; i >= 0; i--) {
                retstr = str.substr(i, 1) + retstr;
                if (i > 0 && ((l - i) % 3) == 0) retstr = ',' + retstr;
            }
        }
        else retstr = str.substr(0, i + 1) + retstr;
    }
    retstr = retstr + '.';
    // 处理小数部分   
    str = strarr.length > 1 ? strarr[1] : '';
    fmt = fmtarr.length > 1 ? fmtarr[1] : '';
    i = 0;
    for (var f = 0; f < fmt.length; f++) {
        switch (fmt.substr(f, 1)) {
            case '#':
                if (i < str.length) retstr += str.substr(i++, 1);
                break;
            case '0':
                if (i < str.length) retstr += str.substr(i++, 1);
                else retstr += '0';
                break;
        }
    }
    return retstr.replace(/^,+/, '').replace(/\.$/, '');
};

/*
==================================================================
功能：替换空格（将多个连续空格替换为一个空格）
使用：ReplaceSpace(obj) 
返回：string
==================================================================
*/
utils.ReplaceSpace = function (str) {
    while (str.indexOf("  ") >= 0) {
        str = str.replace("  ", " ");
    }
    return str;
}

/*
==================================================================
功能：非空检查，不忽略空格
提示信息：输入框为空，请输入！
使用：isNull(obj,string) 
返回：bool
==================================================================
*/
utils.isNull = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);  //非空检查
    if (obj.value == "") {
        if (show) alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    }
    else {
        return true;
    }
}

/*
==================================================================
功能：邮箱地址检查
提示信息：未输入邮件地址或邮件地址无效！
使用：MailCheck(obj,string)
返回：bool
==================================================================
*/
utils.MailCheck = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);

    if (obj.value != "") {
        var ok1 = obj.value.indexOf("@");
        var ok2 = obj.value.indexOf(".");
        if (!((ok1 != -1) && (ok2 != -1))) {
            if (show) alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }
        var allowstrlist = "&#%<>";
        var endvalue = true;
        for (i = 0; i < obj.value.length; i++) {
            if (allowstrlist.indexOf(obj.value.substr(i, 1)) != -1) {
                endvalue = false;
                break;
            }
        }
        if (endvalue == false) {
            if (show) alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }
        //邮件地址正确
        return true;
    }
    else {
        //请输入电子信箱地址
        if (show) alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    }
}

/*
==================================================================
功能：检查输入的是否为数字
提示信息：未输入或输入的不是一个合法的数字！
使用：isNumeric(obj,string)
返回：bool
==================================================================
*/
utils.isNumeric = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);

    var IfTrue = obj.value.search(/^(-|\+)?\d+(\.\d+)?$/) != -1;

    if (show && IfTrue == false) {
        alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    }
    else {
        return true;
    }
}

/*
==================================================================
功能：打印
使用：Print()
返回：
==================================================================
*/
utils.Print = function () {
    document.all.print.style.display = "none";
    window.print();
    window.close();
}

/*
==================================================================
功能：验证身份证号码是否有效
提示信息：未输入或输入身份证号不正确！
使用：isIDno(obj,string)
返回：bool
==================================================================
*/
utils.isIDno = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);

    //aCity={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江 
    //",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北
    //",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏
    //",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外
    //"};
    var aCity = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91"

    var iSum = 0;
    var info = "";
    var idCardLength = obj.value.length;
    if (!/^\d{17}(\d|x)$/i.test(obj.value) && !/^\d{15}$/i.test(obj.value)) {
        if (show) alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    }

    //在后面的运算中x相当于数字10,所以转换成a
    obj.value = obj.value.replace(/x$/i, "a");

    var curCity = obj.value.substr(0, 2); if (!(aCity.indexOf(curCity) > 0)) {
        if (show) alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    } if (idCardLength == 18) {

        sBirthday = obj.value.substr(6, 4) + "-" + Number(obj.value.substr(10, 2)) + "-" + Number(obj.value.substr(12, 2));
        var d = new Date(sBirthday.replace(/-/g, "/"))
        if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) {
            if (show)
                alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }

        for (var i = 17; i >= 0; i--)
            iSum += (Math.pow(2, i) % 11) * parseInt(obj.value.charAt(17 - i), 11);

        if (iSum % 11 != 1) {
            if (show)
                alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }

    }
    else if (idCardLength == 15) {
        sBirthday = "19" + obj.value.substr(6, 2) + "-" + Number(obj.value.substr(8, 2)) + "-" +
      Number(obj.value.substr(10, 2));
        var d = new Date(sBirthday.replace(/-/g, "/"))
        var dd = d.getFullYear().toString() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
        if (sBirthday != dd) {
            if (show)
                alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }
    }
    return true;
}

/*
==================================================================
功能：验证电话号码格式是否正确
提示信息：未输入或输入电话号码格式不正确！
使用：isPhoneNo(obj,string)
返回：bool
==================================================================
*/
utils.isPhoneNo = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);

    var phoneNo = obj.value;
    var Endvalue = true;
    var allowstrlist = "1234567890()-";
    if (phoneNo != "") {
        for (i = 0; i < phoneNo.length; i++) {
            if (allowstrlist.indexOf(phoneNo.substr(i, 1)) == -1) {
                Endvalue = false;
                break;
            }
        }
        if (Endvalue == false) {
            if (show)
                alert(ShowMsg);
            obj.focus();
            obj.select();
            return false;
        }
    }
    else {
        if (show)
            alert(ShowMsg);
        obj.focus();
        obj.select();
        return false;
    }
    return true;
}

/*
==================================================================
功能：去除字符串左边的空格
使用：LTrim(string) 
返回：string
 ==================================================================
 */
utils.LTrim = function (str) {
    var whitespace = new String(" \t\n\r");
    var s = new String(str);

    if (whitespace.indexOf(s.charAt(0)) != -1) {
        var j = 0, i = s.length;
        while (j < i && whitespace.indexOf(s.charAt(j)) != -1) {
            j++;
        }
        s = s.substring(j, i);
    }
    return s;
}

/*
 ==================================================================
功能：去除右边的空格
使用：RTrim(string)
返回：string
 ==================================================================
*/
utils.RTrim = function (str) {
    var whitespace = new String(" \t\n\r");
    var s = new String(str);

    if (whitespace.indexOf(s.charAt(s.length - 1)) != -1) {
        var i = s.length - 1;
        while (i >= 0 && whitespace.indexOf(s.charAt(i)) != -1) {
            i--;
        }
        s = s.substring(0, i + 1);
    }
    return s;
}

/*
==================================================================
功能：去除字符串两边的空格
使用：Trim(string) 
返回：string
 ==================================================================
 */
utils.Trim = function (str) {
    return this.RTrim(this.LTrim(str));
}


/*
==================================================================
功能：无效字符的检测（不允许输入特殊字符）
提示信息：未输入或输入包含非法字符
使用：CheckChar(obj,Lchar,string)
   Lchar：要检查的特殊字符 （Lchar='' 或者 Lchar=null 的时候使用默认非法字符检测）
返回：bool
==================================================================
*/
utils.CheckChar = function (obj, Lchar, ShowMsg) {
    var show = this.IfShow(ShowMsg);
    var strlist = Lchar; //"\"\ >~!@#$%^&*?<>\"\ ";
    if (strlist == null || strlist == "") {
        strlist = "\"\ >~!@#$%^&*?<>\"\ ";
    }
    //无效字符的检测 
    if (obj.value != "") {
        var tmpbool = true;
        for (i = 0; i < obj.value.length; i++) {
            if (strlist.indexOf(obj.value.substr(i, 1)) != -1) {
                tmpbool = false;
                break;
            }
            else { }
        }

        if (tmpbool == false) {
            if (show) alert(ShowMsg + strlist);
            obj.focus();
            obj.select();
            return false;
        }
        else {
            return true;
        }
    }
    else {
        if (show) alert(ShowMsg + strlist);
        return false;
    }

}

/*
==================================================================
功能：判断是否为日期(格式:yyyy-mm-dd)
提示信息：未输入或输入的日期格式错误！
使用：isDate(obj,string)
返回：bool
==================================================================
*/
utils.isDate = function (obj, ShowMsg) {
    var show = this.IfShow(ShowMsg);

    if (obj.value == null) {
        if (show) alert(ShowMsg);
        return false;
    }

    if (obj.value == "") {
        if (show) alert(ShowMsg);
        return false;
    }

    var datePat = /^(\d{2}|\d{4})(\-)(\d{1,2})(\-)(\d{1,2})$/;

    var dateStr = obj.value;
    // is the format ok?
    var matchArray = dateStr.match(datePat);
    if (matchArray == null) {
        if (show) alert(ShowMsg);
        return false;
    }
    year = matchArray[1];
    month = matchArray[3];
    day = matchArray[5]; if (year.length != 4 || month.length != 2 || day.length != 2) {
        if (show) alert(ShowMsg);
        return false;
    }
    if (month < 1 || month > 12) {
        if (show) alert(ShowMsg);
        return false;
    }
    if (day < 1 || day > 31) {
        if (show) alert(ShowMsg);
        return false;
    } if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        if (show) alert(ShowMsg);
        return false;
    } if (month == 2) {
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || ((day == 29) && (!isleap))) {
            if (show) alert(ShowMsg);
            return false;
        }
    }
    return true;
}


/*
==================================================================
功能：获取当天日期(格式:yyyy-mm-dd)
使用：GetDateNow()
返回：string
==================================================================
*/
utils.GetDateNow = function () {
    var d, y, m, dd;
    d = new Date();
    y = d.getYear();
    m = d.getMonth() + 1;
    dd = d.getDate();
    return y + "-" + m + "-" + dd;
}

/*
==================================================================
功能：获取当天时间(格式:(如:2009-06-12 12:00))
使用：GetDateNow()
返回：string
==================================================================
*/
utils.GetDateTimeNow = function () {
    var now = new Date();
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分
    var ss = now.getSeconds();          //秒
    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm + ":";
    if (ss < 10) clock += '0';
    clock += ss;

    return (clock);
}



/*
//设置或获取对象指定的文件名或路径。
alert(window.location.pathname);

//设置或获取整个 URL 为字符串。
alert(window.location.href);

//设置或获取与 URL 关联的端口号码。
alert(window.location.port);


//设置或获取 URL 的协议部分。
alert(window.location.protocol);

//设置或获取 href 属性中在井号“#”后面的分段。
alert(window.location.hash);

//设置或获取 location 或 URL 的 hostname 和 port 号码。
alert(window.location.host);

//设置或获取 href 属性中跟在问号后面的部分。
alert(window.location.search);
*/

/*
==================================================================
功能：js获取url传递参数
使用：
var Request = getRequest();
var 参数1,参数2,参数3,参数N;
参数1 = Request[''参数1''];
参数2 = Request[''参数2''];
参数3 = Request[''参数3''];
参数N = Request[''参数N''];
返回：Object
==================================================================
*/
utils.getRequest = function () {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.split("?")[1];
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
};


/*
==================================================================
功能：js获取url传递参数
使用：var value = GetQueryString();
返回：string
==================================================================
*/
utils.GetQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}

/*编码html*/
utils.html_encode=function(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&gt;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;");
    s = s.replace(/\"/g, "&quot;");
    s = s.replace(/\n/g, "<br>");
    return s;
}

/*解码html*/
utils.html_decode = function (str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&gt;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    s = s.replace(/<br>/g, "\n");
    return s;
}



/** 
* json格式转树状结构 
* @param   {json}      json数据 
* @param   {String}    id的字符串 
* @param   {String}    父id的字符串 
* @param   {String}    children的字符串 
* @return  {Array}     数组 
*/
utils.toTreeData = function (a, idStr, pidStr, childrenStr) {
    var r = [], hash = {}, len = (a || []).length;
    for (var i = 0; i < len; i++) {
        hash[a[i][idStr]] = a[i];
    }
    for (var j = 0; j < len; j++) {
        var aVal = a[j], hashVP = hash[aVal[pidStr]];
        if (hashVP) {
            !hashVP[childrenStr] && (hashVP[childrenStr] = []);
            hashVP[childrenStr].push(aVal);
        } else {
            r.push(aVal);
        }
    }
    return r;
};

utils.eachTreeRow = function (treeData, eachHandler) {
    for (var i in treeData) {
        if (eachHandler(treeData[i]) == false) break;
        if (treeData[i].children)
            utils.eachTreeRow(treeData[i].children, eachHandler);
    }
};

utils.isInChild = function (treeData, pid, id) {
    var isChild = false;
    utils.eachTreeRow(treeData, function (curNode) {
        if (curNode.id == pid) {
            utils.eachTreeRow([curNode], function (row) {
                if (row.id == id) {
                    isChild = true;
                    return false;
                }
            });
            return false;
        }
    });
    return isChild;
};

utils.fnValueToText = function (list, value, text) {
    var map = {};
    for (var i in list) {
        map[list[i][value || 'value']] = list[i][text || 'text'];
    }
    var fnConvert = function (v, r) {
        return map[v];
    };
    return fnConvert;
};

utils.compareObject = function (v1, v2) {
    var countProps = function (obj) {
        var count = 0;
        for (k in obj) if (obj.hasOwnProperty(k)) count++;
        return count;
    };

    if (typeof (v1) !== typeof (v2)) {
        return false;
    }

    if (typeof (v1) === "function") {
        return v1.toString() === v2.toString();
    }

    if (v1 instanceof Object && v2 instanceof Object) {
        if (countProps(v1) !== countProps(v2)) {
            return false;
        }
        var r = true;
        for (k in v1) {
            r = utils.compareObject(v1[k], v2[k]);
            if (!r) {
                return false;
            }
        }
        return true;
    } else {
        return v1 === v2;
    }
};

utils.minusArray = function (arr1, arr2) {
    var arr = [];
    for (var i in arr1) {
        var b = true;
        for (var j in arr2) {
            if (utils.compareObject(arr1[i], arr2[j])) {
                b = false;
                break;
            }
        }
        if (b) {
            arr.push(arr1[i]);
        }
    }
    return arr;
};

utils.diffrence = function (obj1, obj2, reserve, ignore) {
    var obj = {}, reserve = reserve || [], ignore = ignore || [], reserveMap = {}, ignoreMap = {};
    for (var i in reserve)
        reserveMap[reserve[i]] = true;

    for (var i in ignore)
        ignoreMap[ignore[i]] = true;

    for (var k in obj1) {
        if (!ignoreMap[k] && (obj1[k] != obj2[k] || reserveMap[k])) {
            obj[k] = obj1[k];
        }
    }
    return obj;
};

utils.filterProperties = function (obj, props, ignore) {
    var ret;
    if (obj instanceof Array || Object.prototype.toString.call(obj) === "[object Array]") {
        ret = [];
        for (var k in obj) {
            ret.push(utils.filterProperties(obj[k], props, ignore));
        }
    }
    else if (typeof obj === 'object') {
        ret = {};
        if (ignore) {
            var map = {};
            for (var k in props)
                map[props[k]] = true;

            for (var i in obj) {
                if (!map[i]) ret[i] = obj[i];
            }
        }
        else {
            for (var i in props) {
                var arr = props[i].split(" as ");
                ret[arr[1] || arr[0]] = obj[arr[0]];
            }
        }
    }
    else {
        ret = obj;
    }
    return ret;
};


utils.copyProperty = function (obj, sourcePropertyName, newPropertyName, overWrite) {
    if (obj instanceof Array || Object.prototype.toString.call(obj) === "[object Array]") {
        for (var k in obj)
            utils.copyProperty(obj[k], sourcePropertyName, newPropertyName);
    }
    else if (typeof obj === 'object') {
        if (sourcePropertyName instanceof Array || Object.prototype.toString.call(sourcePropertyName) === "[object Array]") {
            for (var i in sourcePropertyName) {
                utils.copyProperty(obj, sourcePropertyName[i], newPropertyName[i]);
            }
        }
        else if (typeof sourcePropertyName === 'string') {
            if ((obj[newPropertyName] && overWrite) || (!obj[newPropertyName]))
                obj[newPropertyName] = obj[sourcePropertyName];
        }
    }
    return obj;
};

utils.clearIframe = function (context) {
    var frame = $('iframe', context).add(parent.$('iframe', context));
    if (frame.length > 0) {
        frame[0].contentWindow.document.write('');
        frame[0].contentWindow.close();
        frame.remove();
        if ($.browser.msie) {
            CollectGarbage();
        }
    }
};

utils.getThisIframe = function () {
    if (!parent) return null;
    var iframes = parent.document.getElementsByTagName('iframe');
    if (iframes.length == 0) return null;
    for (var i = 0; i < iframes.length; ++i) {
        var iframe = iframes[i];
        if (iframe.contentWindow === self) {
            return iframe;
        }
    }
    return null;
}

utils.functionComment = function (fn) {
    return fn.toString().replace(/^.*\r?\n?.*\/\*|\*\/([.\r\n]*).+?$/gm, '');
};

utils.uuid = (function () { var a = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".split(""); return function (b, f) { var h = a, e = [], d = Math.random; f = f || h.length; if (b) { for (var c = 0; c < b; c++) { e[c] = h[0 | d() * f]; } } else { var g; e[8] = e[13] = e[18] = e[23] = "-"; e[14] = "4"; for (var c = 0; c < 36; c++) { if (!e[c]) { g = 0 | d() * 16; e[c] = h[(c == 19) ? (g & 3) | 8 : g & 15]; } } } return e.join("").toLowerCase(); }; })();



/*
功能：json日期格式转换为正常格式
* @param   {jsonDate}      json数据 
* @return  {string}     数组 
*/
utils.DateTime.JsonDateFormat = function (jsonDate)
{
    if (jsonDate == null || jsonDate.length == 0) return "";
    try {
        var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
        return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
    } catch (ex) {
        return "";
    }
}

/*
功能：json日期格式转换为日期
* @param   {jsonDate}      json数据 
* @return  {string}     日期 
*/
utils.DateTime.ConvertJSONDateToJSDateObject = function (jsonDate) {
    var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
    return date;
}

// 对Date的扩展，将 Date 转化为指定格式的String   
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
// 例子：   
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}