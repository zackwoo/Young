/*
基础对象扩展方法
*/
if (typeof Young !== 'object') {
	Young = {};
}

//将richtextbox内容从html转码避免安全隐患
Young.encodeRichText = function() {
    $('.young-richbox').each(function (i, item) {
        var raw = $(item).val();
        $(item).val(raw.encodeHTML());
    });
};


/**************** String.prototype扩展 **********************/
String.prototype.format = function() {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function(m, i) {
            return args[i];
        });
};

//编码html,将&,>,<,',"等字符替换为html特殊字符，当不指定isMutiLine为真时，还会替换换行符
String.prototype.encodeHTML = function () {
    var s = this.valueOf();
    s = s.replace(/\&/g, "&amp;");
    s = s.replace(/\>/g, "&gt;");
    s = s.replace(/\</g, "&lt;");
    s = s.replace(/\"/g, "&quot;");
    s = s.replace(/\'/g, "&#39;");
    return s;
};
