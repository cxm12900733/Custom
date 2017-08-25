/*
==================================================================
程序：通用js
创建：陈少明
说明：
最近更新：2017-8-25
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

show.windows = function (params)
{
    //id, url, width, height, iconCls, title,buttons
    if (!$.isEmptyObject(params.id))
    {
        $('#' + params.id).dialog({
            closed: false,
            cache: false,
            href: $.isEmptyObject(params.url) ? "" : params.url,
            iconCls: $.isEmptyObject(params.iconCls) ? null : params.iconCls,
            title: $.isEmptyObject(params.title) ? "系统弹窗" : params.title,
            modal: true,
            resizable: true,
            width: !$.isNumeric(params.width) ? "auto" : params.width,
            height: !$.isNumeric(params.height) ? "auto" : params.height,
            inline: true,
            buttons: $.isEmptyObject(params.buttons) ? null : params.buttons,
        }).dialog("center");
        //$('#' + params.id).window({
        //    href: $.isEmptyObject(params.url) ? "" : params.url,
        //    iconCls: $.isEmptyObject(params.iconCls) ? null : params.iconCls,
        //    title: $.isEmptyObject(params.title) ? "系统弹窗" : params.title,
        //    closed: false,
        //    cache: false,
        //    modal: true,
        //    resizable: true,
        //    minimizable: false,
        //    maximizable: false,
        //    collapsible: false,
        //    shadow: false,
        //    inline: true,
        //    width: !$.isNumeric(params.width) ? "auto" : params.width,
        //    height: !$.isNumeric(params.height) ? "auto" : params.height,
        //}).window("center");
    }
}