/**
 * @author {CaoGuangHui}
 */
$.extend($.fn.tabs.methods, {
    /**
     * tabs组件每个tab panel对应的小工具条绑定的事件没有传递事件参数
     * 本函数修正这个问题
     * @param {[type]} jq [description]
     */
    addEventParam: function (jq) {
        return jq.each(function () {
            var that = this;
            var headers = $(this).find('>div.tabs-header>div.tabs-wrap>ul.tabs>li');
            headers.each(function (i) {
                var tools = $(that).tabs('getTab', i).panel('options').tools;
                if (typeof tools != "string") {
                    $(this).find('>span.tabs-p-tool a').each(function (j) {
                        $(this).unbind('click').bind("click", {
                            handler: tools[j].handler
                        }, function (e) {
                            if ($(this).parents("li").hasClass("tabs-disabled")) {
                                return;
                            }
                            e.data.handler.call(this, e);
                        });
                    });
                }
            })
        });
    },
    /**
     * 加载iframe内容
     * @param  {jq Object} jq     [description]
     * @param  {Object} params    params.which:tab的标题或者index;params.iframe:iframe的相关参数
     * @return {jq Object}        [description]
     */
    loadTabIframe: function (jq, params) {
        return jq.each(function () {
            var $tab = $(this).tabs('getTab', params.which);
            if ($tab == null) return;
            var $parentBody = top.$('#homebody');//.attr("hbody");
            var $tabBody = $tab.panel('body');
            //销毁已有的iframe
            var $frame = $('iframe', $tabBody);
            if ($frame.length > 0) {
                try {//跨域会拒绝访问，这里处理掉该异常
                    $frame[0].contentWindow.document.write('');
                    $frame[0].contentWindow.close();
                } catch (e) {
                    //Do nothing
                }
                $frame.remove();
                if ($.browser && $.browser.msie) {
                    CollectGarbage();
                }
            }
            $tabBody.html('');
            $tabBody.css({ 'overflow': 'hidden', 'position': 'relative' });
            var $mask = $('<div style="position:absolute;width:100%;height:100%;background:#545669;z-index:1000;opacity:0.3;filter:alpha(opacity=30);"><div>').appendTo($parentBody);
            var $maskMessage = $('<div class="mask-message" style="z-index:1001;width:auto;height:16px;line-height:16px;position:absolute;top:50%;left:50%;margin-top:-20px;margin-left:-92px;border:2px dashed #75186D;padding: 12px 5px 10px 30px;background: #ffffff url(\'/Scripts/03jeasyui/jeasyui_extend/images/ajax-loader.gif\') no-repeat scroll 5px center;">' + (params.iframe.message || '拼命加载中,请稍后 ...') + '</div>').appendTo($parentBody);
            var $containterMask = $('<div style="position:absolute;width:100%;height:100%;z-index:1;background:#fff;"></div>').appendTo($tabBody);
            var $containter = $('<div style="position:absolute;width:100%;height:100%;z-index:0;"></div>').appendTo($tabBody);
            var p = '';
            if (params.opts) {
                if (params.opts.menucode) {
                    if (params.opts.menucode != '') {
                        if (params.iframe.src.indexOf("/?") > 0) {
                            p = '&menucode=' + params.opts.menucode;
                        } else {
                            p = '/?menucode=' + params.opts.menucode;
                        }
                    }
                }
            }
            var iframe = document.createElement("iframe");
            iframe.src = params.iframe.src + p;
            //设置iframe的样式  
            iframe.style.width = '100%';
            iframe.style.height = '100%';
            iframe.style.margin = '0';
            iframe.style.padding = '0';
            iframe.style.overflow = 'hidden';
            iframe.style.border = 'none';
            //iframe.frameBorder = params.iframe.frameBorder || 0;
            //iframe.height = params.iframe.height || '100%';
            //iframe.width = params.iframe.width || '100%';
            if (iframe.attachEvent) {
                iframe.attachEvent("onload", function () {
                    $([$mask[0], $maskMessage[0]]).fadeOut(params.iframe.delay || 'fast', function () {
                        $(this).remove();
                        if ($(this).hasClass('mask-message')) {
                            $containterMask.fadeOut(params.iframe.delay || 'fast', function () {
                                $(this).remove();
                            });
                        }
                    });
                });
            } else {
                iframe.onload = function () {
                    $($mask).remove();
                    $($maskMessage).remove();
                    $($containterMask).remove();
                };
            }
            $containter[0].appendChild(iframe);
        });//end return jq.each
    },
    /**
     * 增加iframe模式的标签页
     * @param {[type]} jq     [description]
     * @param {[type]} params [description]
     */
    addIframeTab: function (jq, params) {
        var options = params.opts_ext;
        return jq.each(function () {
            if (params.opts_ext.href) {
                delete params.opts_ext.href;
            }
            $(this).tabs('add', params.opts_ext);
            $(this).tabs('loadTabIframe', { 'opts': params.opts_ext, 'which': params.opts_ext.title, 'iframe': params.iframe });
        });
    },
    /**
     * 更新tab的iframe内容
     * @param  {jq Object} jq     [description]
     * @param  {Object} params [description]
     * @return {jq Object}        [description]
     */
    updateIframeTab: function (jq, params) {
        return jq.each(function () {
            params.iframe = params.iframe || {};
            if (!params.iframe.src) {
                var $tab = $(this).tabs('getTab', params.which);
                if ($tab == null) return;
                var $tabBody = $tab.panel('body');
                var $iframe = $tabBody.find('iframe');
                if ($iframe.length === 0) return;
                $.extend(params.iframe, { 'src': $iframe.attr('src') });
            }
            $(this).tabs('loadTabIframe', params);
        });
    },
    /**
     * 获取tab的iframe url
     * @param  {jq Object} jq     [description]
     * @param  {Object} params [description]
     * @return {jq Object}        [description]
     */
    getIframeTabUrl: function (jq, params) {
        params.iframe = params.iframe || {};
        var $tab = jq.tabs('getTab', params.which);
        if ($tab == null) return null;
        var $tabBody = $tab.panel('body');
        var $iframe = $tabBody.find('iframe');
        if ($iframe.length === 0) return null;
        return $iframe.attr('src');
    },
    /**
      * 绑定双击事件
      * @param {Object} jq
      * @param {Object} caller 绑定的事件处理程序
      */
    bindDblclick: function (jq, caller) {
        return jq.each(function () {
            var that = this;
            $(this).children("div.tabs-header").find("ul.tabs").undelegate('li', 'dblclick.tabs').delegate('li', 'dblclick.tabs', function (e) {
                if (caller && typeof (caller) == 'function') {
                    var title = $(this).text();
                    var index = $(that).tabs('getTabIndex', $(that).tabs('getTab', title));
                    caller(index, title);
                }
            });
        });
    },
    /**
     * 解除绑定双击事件
     * @param {Object} jq
     */
    unbindDblclick: function (jq) {
        return jq.each(function () {
            $(this).children("div.tabs-header").find("ul.tabs").undelegate('li', 'dblclick.tabs');
        });
    },
    allTabs: function (jq) {//返回所有tab集合
        var tabs = $(jq).tabs('tabs');
        var all = [];
        all = $.map(tabs, function (n, i) {
            return $(n).panel('options')
        });
        return all;
    },
    closeCurrent: function (jq) { // 关闭当前
        var currentTab = $(jq).tabs('getSelected'), currentTabIndex = $(jq).tabs('getTabIndex', currentTab);
        $(jq).tabs('close', currentTabIndex);
    },
    closeAll: function (jq) { //关闭全部
        var tabs = $(jq).tabs('allTabs');
        $.each(tabs, function (i, n) {
            $(jq).tabs('close', n.title);
        })
    },
    closeOther: function (jq) { //关闭除当前标签页外的tab页
        var tabs = $(jq).tabs('allTabs');
        var currentTab = $(jq).tabs('getSelected'),
            currentTabIndex = $(jq).tabs('getTabIndex', currentTab);

        $.each(tabs, function (i, n) {
            if (currentTabIndex != i) {
                $(jq).tabs('close', n.title);
            }
        })
    },
    closeLeft: function (jq) { // 关闭当前页左侧tab页
        var tabs = $(jq).tabs('allTabs');
        var currentTab = $(jq).tabs('getSelected'),
            currentTabIndex = $(jq).tabs('getTabIndex', currentTab);
        var i = currentTabIndex - 1;
        while (i > -1) {
            $(jq).tabs('close', tabs[i].title);
            i--;
        }
    },
    closeRight: function (jq) { //// 关闭当前页右侧tab页
        var tabs = $(jq).tabs('allTabs');
        var currentTab = $(jq).tabs('getSelected'),
            currentTabIndex = $(jq).tabs('getTabIndex', currentTab);
        var i = currentTabIndex + 1, len = tabs.length;
        while (i < len) {
            $(jq).tabs('close', tabs[i].title);
            i++;
        }
    }
});

/*
扩展tabs-增加设置Tab标题【setTabTitle】方法
最近用到Tab的标题刷新，虽然Tabs自身已经提供了Update方法，但是满足不了我的现在的需求。由于调用他的update方式时如果tab的页面是用href方式引入的情况下你调用Update刷新tab，就是你跟新href属性，他也会自动刷新一次页面。而我的需求是，值需要修改标题，而不要刷新界面。为此扩展了一个setTabTitle方法。
使用方式：
var tab = $('#tt').tabs('getSelected');
$('#tt').tabs('setTabTitle',{tab:tab,title:"New Title"});

*/
$.extend($.fn.tabs.methods, {
    setTabTitle: function (jq, opts) {
        return jq.each(function () {
            var tab = opts.tab;
            var options = tab.panel("options");
            var tab = options.tab;
            options.title = opts.title;
            var title = tab.find("span.tabs-title");
            title.html(opts.title);
        });
    }
});