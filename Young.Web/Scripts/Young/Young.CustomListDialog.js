/*
对自定义列表对话框操作的JS
*/
if (typeof Young !== 'object') {
    Young = {};
}


(function () {
    //自定义列类型对应的控件类型
    var custom_column_type = {
        0: 'Number',
        1: 'Boolean',
        2: 'TextLine',
        3: 'Date',
        4: 'DateTime',
        5: 'Users',
        6: 'CurrentUser',
        7: 'Term',
        8: 'TextArea'
    };

    /*
      创建自定义列控件
      入参自定义列参数   
    {
        field: item.InnerName,
        title: item.DisplayName,
        type: item.Type,
        id: item.ID,
        condition: item.Condition,
        description: item.Description
    }
    */
    var CreateControlByCustomColumn = function (column) {
        var htmlTemplate = CreateControlTemplate(column.type);
        var html = htmlTemplate.format(column.title, column.field);
        return html;
    };
    var CreateControlTemplate = function (type) {
        var htmlTemplate = "";
        if (custom_column_type[type] == "Number") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><input name="{1}" class="easyui-numberspinner"/></div>';
        } else if (custom_column_type[type] == "Boolean") {
            throw Error('未实现');
        } else if (custom_column_type[type] == "TextLine") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><input name="{1}" class="easyui-validatebox"/></div>';
        } else if (custom_column_type[type] == "Date") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><input name="{1}" class="easyui-datebox"/></div>';
        } else if (custom_column_type[type] == "DateTime") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><input name="{1}" class="easyui-datetimebox"/></div>';
        } else if (custom_column_type[type] == "Users") {
            throw Error('未实现');
        } else if (custom_column_type[type] == "CurrentUser") {
            throw Error('未实现');
        } else if (custom_column_type[type] == "Term") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><select id="{1}" class="easyui-combotree" style="width:149px;" data-options="url:\'{2}\',method:\'get\'"></select></div>';
        } else if (custom_column_type[type] == "TextArea") {
            htmlTemplate = '<div class="fitem"><label>{0}:</label><textarea name="{1}" class="easyui-validatebox"></textarea></div>';
        }
        return htmlTemplate;
    }

    Young.CreateRecordDataDialog = function (columns, title) {
        title = title || "数据记录";
        var dlg = $('<div style="padding: 10px 20px"></div>');
        $.each(columns, function (i, item) {
            var control = CreateControlByCustomColumn(item);
            dlg.append(control);
        })
        $.parser.parse(dlg);
        dlg.dialog({
            title: title,
            width: 400,
            height: 280,
            closed: false,
            cache: false,
            modal: true
        });
        
    };
})();