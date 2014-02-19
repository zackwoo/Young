using System;
using System.Collections.Generic;
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
        Number,
        Boolean,
        String,
        Date,
        Term
    }
}
