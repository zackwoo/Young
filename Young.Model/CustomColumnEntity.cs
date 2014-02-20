using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;
using Young.Model.DataAttribute;

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
        [EnumDescription(Name = "数字型", Order = 1,Value=0)]
        Number=0,
        [EnumDescription(Name = "布尔型", Order = 2, Value = 1)]
        Boolean=1,
        [EnumDescription(Name = "单行文本框", Order = 3, Value = 2)]
        TextLine=2,
        [EnumDescription(Name = "日期类型", Order = 5, Value = 3)]
        Date = 3,
        [EnumDescription(Name = "时间类型", Order = 6, Value = 4)]
        DateTime = 4,
        [EnumDescription(Name = "用户选择", Order = 9, Value = 5)]
        Users=5,
        [EnumDescription(Name = "当前登录用户", Order = 8, Value = 6)]
        CurrentUser=6,
        [EnumDescription(Name = "术语型", Order = 7, Value = 7)]
        Term=7,
        [EnumDescription(Name = "多行文本框", Order = 4, Value = 8)]
        TextArea=8
    }

    
}
