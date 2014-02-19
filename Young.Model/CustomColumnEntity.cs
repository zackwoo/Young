using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    public class CustomColumnEntity : BaseGeneralEntity
    {
        public CustomColumnType ColumnType { get; set; }

        /// <summary>
        /// 内部列名，创建后不可以修改
        /// </summary>
        public string InnerName { get; set; }

        /// <summary>
        /// 不同类型，条件字段内容与意义不同
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        public bool EnableDefaultValue { get; set; }

        public virtual CustomListEntity CustomListEntity { get; set; }
    }

    public enum CustomColumnType
    {
        [Display(Name = "数字型",Order=1)]
        Number=0,
        [Display(Name = "布尔型", Order = 2)]
        Boolean=1,
        [Display(Name = "单行文本框", Order = 3)]
        TextLine=2,
        [Display(Name = "日期类型", Order = 5)]
        Date = 3,
        [Display(Name = "时间类型", Order = 6)]
        DateTime = 4,
        [Display(Name = "用户选择", Order = 9)]
        Users=4,
        [Display(Name = "当前登录用户", Order = 8)]
        CurrentUser=5,
        [Display(Name = "术语型", Order = 7)]
        Term=6,
        [Display(Name = "多行文本框", Order = 4)]
        TextArea=7
    }
}
