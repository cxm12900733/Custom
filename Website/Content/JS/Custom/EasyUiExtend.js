/*
==================================================================
程序：EasyUi扩展
创建：陈少明
说明：
最近更新：2017-12-8
==================================================================
*/

var UI = {};
//iframe弹出窗
UI.Windows = function (params) {
    //id, url, width, height, iconCls, title
    if (!$.isEmptyObject(params.id))
    {
        var url = $.isEmptyObject(params.url) ? "" : params.url;
        var content = '<iframe id="' + params.id + '" src="' + url + '" width="100%" height="98%" frameborder="0" scrolling="no"></iframe>';
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
//关闭iframe弹出窗
UI.CloseWindow = function (iframeId) {
    $(iframeId).window('close');
}
//DataGrid重新加载
UI.RefreshDataGrid = function (id)
{
    $(id).datagrid('reload');
}
//TreeGrid重新加载
UI.RefreshTreeGrid = function (id) {
    $(id).treegrid('reload');
}
//TreeGrid处理加载的数据
UI.LoadTreegrId = function (data) {
    $.each(data.rows, function (i, n) {
        if (parseInt(n.ParentID) != 0) {
            data.rows[i]._parentId = n.ParentID;
        }

        if (n.Icon != '') {
            data.rows[i].iconCls = n.Icon;
        }
    });
    return data;
}
//处理预先打勾的代码，还未测试通用性,powers:,分开的选中数据.
//function LoadTreegrId(data, powers) {
//    var newdata = data;
//    var powersArr;
//    if (powers != null && powers != "") {
//        powersArr = powers.split(",");
//    }
//    $.each(newdata.rows, function (i, n) {
//        if (parseInt(n.ParentID) != 0) {
//            newdata.rows[i]._parentId = n.ParentID;
//        }

//        if (n.Icon != '') {
//            newdata.rows[i].iconCls = n.Icon;
//        }
//        $.each(powersArr, function (k, v) {
//            if (n.ID == parseInt(v)) {
//                newdata.rows[i].checked = true;
//            }
//        });

//    });

//    return newdata;
//}

UI.RowButton = function (rowButtons,data) {
    if (rowButtons.length > 0) {
        $.each(data.rows, function (k, v) {
            $.each(rowButtons, function (i, n) {
                data.rows[k].Operate = '<a onclick="' + n.Method + '(\'' + n.Url + '\',\'' + n.Name + '\',\'' + n.Icon + '\',\''+v.ID+'\')" class="' + n.Method + '">' + n.Name + '</a>';
            });
        });
    }
    return data;
}

UI.ShowRowButton = function (rowButtons, data) {
    if (rowButtons.length > 0) {
        $.each(rowButtons, function (i, n) {
            $("." + n.Method).linkbutton({ text: n.Name, plain: true, iconCls: n.Icon });
        });
    }
}



$.extend($.fn.validatebox.defaults.rules, {
    minLength: {
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '请输入最少{0}个字'
    },
    maxLength: {
        validator: function (value, param) {
            return value.length <= param[0];
        },
        message: '请输入最多{0}个字'
    }
});