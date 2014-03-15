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
        public override System.Data.SqlDbType DatabaseType
        {
            get { return System.Data.SqlDbType.DateTime; }
        }
    }
}
