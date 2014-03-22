using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.ColumnType
{
    /// <summary>
    /// 数字类型
    /// </summary>
    public class NumberType : ColumnTypeBase
    {
        private System.Data.SqlDbType _databaseType = System.Data.SqlDbType.Float;
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
