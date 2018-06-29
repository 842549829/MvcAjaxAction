(function ($) {
    function getUrlRelativePath() {
        var url = document.location.toString();
        var arrUrl = url.split("//");
        var start = arrUrl[1].indexOf("/");
        var relUrl = arrUrl[1].substring(start);//stop省略，截取从start开始到结尾的所有字符
        if (relUrl.indexOf("?") != -1) {
            relUrl = relUrl.split("?")[0];
        }
        return relUrl;
    }
    var className = "active";
    $("#navMenu li").each(function () {
        if ($(this).hasClass(className)) {
            $(this).removeClass(className);
        }
    });
    var url = getUrlRelativePath();
    if (url && url.length > 1) {
        var lastIndex = url.lastIndexOf('/');
        if (lastIndex > 0) {
            var id = url.substring(lastIndex + 1, url.length);
            if (id && id.length) {
                $("#" + id).addClass(className);
            } else {
                $("#Index").addClass(className);
            }
        } else {
            $("#Index").addClass(className);
        }
    } else {
        $("#Index").addClass(className);
    }


})(jQuery);