using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    internal class SQLUpdateBuilder : SQLBuilder
    {

        public override void Init()
        {
            Sql.AppendFormat("UPDATE {0} SET ", TabelName);
        }

        public override void BuildFields()
        {
            for (int i = 0; i < Fields.Length; i++)
            {
                Sql.AppendFormat(" [{0}]=@{0} ", Fields[i]);
                if (i < Fields.Length - 1)
                {
                    Sql.Append(",");
                }
            }
        }

        public override void BuildWhere()
        {
            Sql.Append(" WHERE ");
            if (WhereFields == null || !WhereFields.Any())
            {
                Sql.AppendFormat(" [{0}]=@{0} ", PrimaryKey);
                return;
            }
            for (int i = 0; i < WhereFields.Length; i++)
            {
                Sql.AppendFormat(" [{0}]=@{0} ", WhereFields[i]);
                if (i < WhereFields.Length - 1)
                {
                    Sql.Append("AND");
                }
            }
        }
    }
}
