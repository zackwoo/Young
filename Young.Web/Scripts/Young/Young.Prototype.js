/*
基础对象扩展方法
*/
if (typeof Young !== 'object') {
	Young = {};
}

String.prototype.format = function () {
	var args = arguments;
	return this.replace(/\{(\d+)\}/g,
        function (m, i) {
        	return args[i];
        });
}