﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Young.CustomTable.View
{
    internal class EasyUIFactory : ViewFactory
    {
        public override string CreateNumberTypeUI(ColumnType.ColumnTypeBase columnType, string value = "")
        {
            var tag = base.GenerateInputTag(InputType.Text, columnType.Code);
            tag.AddCssClass("easyui-numberbox");
            if (columnType.IsRequired)
            {
                tag.MergeAttribute("required", "required");
            }
            tag.MergeAttribute("value", value);
            return tag.ToString(TagRenderMode.SelfClosing);
        }

        public override string CreateLineTextTypeUI(ColumnType.ColumnTypeBase columnType, string value = "")
        {
            var tag = base.GenerateInputTag(InputType.Text, columnType.Code);
            if (columnType.IsRequired)
            {
                tag.MergeAttribute("required", "required");
            }
            tag.MergeAttribute("value", value);
            return tag.ToString(TagRenderMode.SelfClosing);
        }

        public override string CreateDateTypeUI(ColumnType.ColumnTypeBase columnType, string value = "")
        {
            var tag = base.GenerateInputTag(InputType.Text, columnType.Code);
            tag.AddCssClass("easyui-datebox");
            if (columnType.IsRequired)
            {
                tag.MergeAttribute("required", "required");
            }
            tag.MergeAttribute("data-options", "editable:false");
            tag.MergeAttribute("value", value);
            return tag.ToString(TagRenderMode.SelfClosing);
        }
    }
}
