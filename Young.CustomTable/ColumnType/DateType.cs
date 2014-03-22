using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.ColumnType
{
    /// <summary>
    /// 日期类型
    /// </summary>
    public class DateType:ColumnTypeBase
    {
        private System.Data.SqlDbType _databaseType = System.Data.SqlDbType.DateTime2;
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
