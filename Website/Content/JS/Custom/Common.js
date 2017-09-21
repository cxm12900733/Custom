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

show.Windows = function (params){
    //id, url, width, height, iconCls, title
    if (!$.isEmptyObject(params.id))
    {
        var url = $.isEmptyObject(params.url) ? "" : params.url;
        var content = '<iframe id="' + params.id + '" src="' + url + '" width="100%" height="99%" frameborder="0" scrolling="no"></iframe>';
        $('#' + params.id).window({
            closed: false,
            cache: false,
            content: content,
            iconCls: $.isEmptyObject(params.iconCls) ? null : params.iconCls,
            title: $.isEmptyObject(params.title) ? "系统弹窗" : params.title,
            modal: true,
            width: !$.isNumeric(params.width) ? "auto" : params.width,
            height: !$.isNumeric(params.height) ? "auto" : params.height,
            minimizable: false,
            maximizable: false,
            collapsible: false,
        }).dialog("center");
    }
}

show.CloseWindow = function (iframeId) {
    $(iframeId).window('close');
}

show.Refresh = function (id)
{
    $(id).treegrid('reload');
}