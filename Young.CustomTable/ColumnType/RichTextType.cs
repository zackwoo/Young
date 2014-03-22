using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.ColumnType
{
    /// <summary>
    /// 富文本框类型
    /// </summary>
    public class RichTextType : ColumnTypeBase
    {
        private System.Data.SqlDbType _databaseType = System.Data.SqlDbType.Text;
        public override System.Data.SqlDbType DatabaseType
        {
            get { return _databaseType; }
            set
            {
                _databaseType = value;
            }
        }
    }
}
