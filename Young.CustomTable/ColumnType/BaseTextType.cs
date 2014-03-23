using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.ColumnType
{
    /// <summary>
    /// 文本型基类
    /// </summary>
    public abstract class BaseTextType : ColumnTypeBase
    {
      
        /// <summary>
        /// 数据库字段长度
        /// </summary>
        public int DatabaseColumnLength { get; set; }

    }
}
