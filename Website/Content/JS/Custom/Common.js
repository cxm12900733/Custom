/*
==================================================================
程序：通用js
创建：陈少明
说明：
最近更新：2017-9-21
==================================================================
*/

var show = {};
show.formatDate = function (value, row, index) {
    if (!jQuery.isEmptyObject(value)) {
        return utils.formatDate(value, 'yyyy-MM-dd hh:mm:ss');
    } else {
        return "";
    }
}

show.StateName = function (value, row, index) {
    var result = "";
    switch (value) {
        case 1:
            result = "正常"
            break;
        case 2:
            result = "停用"
            break;
    }
    return result;
}

