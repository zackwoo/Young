using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    class SQLCountBuilder : SQLBuilder
    {

        public override void Init()
        {
            Sql.AppendFormat("SELECT COUNT(1) FROM {0} ", TabelName);    
        }
    }
}
