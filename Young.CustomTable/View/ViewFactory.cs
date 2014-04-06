using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable.View
{
    /// <summary>
    /// 生成HTML代码，name以young_为前缀
    /// </summary>
    internal  abstract class ViewFactory
    {

        public abstract string CreateNumberTypeUI(ColumnTypeBase columnType,string value="");
        public abstract string CreateLineTextTypeUI(ColumnTypeBase columnType, string value = "");

        public abstract string CreateDateTypeUI(ColumnTypeBase columnType, string value = "");

        public virtual string CreateRichTextTypeUI(ColumnType.ColumnTypeBase columnType, string value = "")
        {
            var tag = new TagBuilder("textarea");
            tag.MergeAttribute("style", "height: 150px; width:560px;");
            tag.GenerateId(columnType.Code);
            tag.MergeAttribute("name", "young_" + columnType.Code, true);
            tag.AddCssClass("young-richbox");
            tag.InnerHtml = value;
            return tag.ToString();
        }

        #region 创建InputTag
        protected TagBuilder GenerateInputTag(InputType inputType, string id, string name)
        {
            var tag = new TagBuilder("input");
            tag.GenerateId(id);
            tag.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
            tag.MergeAttribute("name", "young_" + name, true);
            return tag;
        }
        protected TagBuilder GenerateInputTag(InputType inputType, string id)
        {
            return GenerateInputTag(inputType, id, id);
        }
        #endregion
    }
}
