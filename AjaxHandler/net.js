/*-----------------------------------------------------------------
功能说明：   $H_DES$
创建时间：   $H_DATE$
其他信息：   此文件依赖 json2.js <http://www.json.org/json2.js> 和 jquery.js 
------------------------------------------------------------------*/
(function ($) {
    if (!$.net) {
        var defaultOptions = {
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
            //, complete: function(r, status,sss) { debugger; } // 此代码加上用于全局调试 
        };

        $.extend({ net: {} });

        $.extend($.net, {
            CallWebMethod: function (options, method, args, obj) {
                var parameters = $.extend({}, defaultOptions);
                var url0 = options.url + "/$CLS$" + "/" + method;
                if (args != null) {
                    var jsonStr = JSON.stringify(args);
                    $.extend(parameters, options, { url: url0, data: jsonStr }, obj);
                } else {
                    $.extend(parameters, options, { url: url0 }, obj);
                }
                $.ajax(parameters);
            }
        });
    }
    var services = new $CLS$();
    $.extend($.net, { $CLS$: services });
})(jQuery);

function $CLS$() {
    this.Options = { url: "$URL$" };
}