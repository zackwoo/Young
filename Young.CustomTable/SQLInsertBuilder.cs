using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    class SQLInsertBuilder : SQLBuilder
    {
        public override void Init()
        {
            Sql.AppendFormat("INSERT INTO {0} ", TabelName);
        }

        public override void BuildFields()
        {
            StringBuilder foo = new StringBuilder();
            StringBuilder bar = new StringBuilder();
            for (int i = 0; i < Fields.Length; i++)
            {
                foo.AppendFormat("[{0}]", Fields[i]);
                bar.AppendFormat("@{0}", Fields[i]);
                if (i < Fields.Length - 1)
                {
                    foo.Append(",");
                    bar.Append(",");
                }
            }
            Sql.AppendFormat(" ({0}) VALUES ({1}) ", foo, bar);
        }
    }
}
