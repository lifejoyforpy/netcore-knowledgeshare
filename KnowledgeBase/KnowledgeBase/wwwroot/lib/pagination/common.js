/**
 * 第三方插件重置选项
 * @param {string} pluginName 插件名称
 * @param {jQuery} ele        一般是jQuery选择器，部门插件也可以传入jQuery对象或Dom对象，根据插件本身需要
 * @param {object} config     用于覆盖插件自身默认配置
 */
function PluginReset(pluginName, ele, config) {
    this[pluginName](ele, config);
}
PluginReset.prototype = {
    constructor: PluginReset,

    simplePagination: function (ele, config) {
        var c = $.extend(true, {
            itemsOnPage: 10,
            listStyle: "pagination",
            prevText: "<i class='fa fa-angle-left'></i>",
            nextText: "<i class='fa fa-angle-right'></i>"
        }, config || {});

        $(ele).pagination(c);
    }
}

Date.prototype.Format = function (fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}