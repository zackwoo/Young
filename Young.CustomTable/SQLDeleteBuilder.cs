using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    class SQLDeleteBuilder : SQLBuilder
    {

        public override void Init()
        {
            Sql.AppendFormat("DELETE FROM {0} ", TabelName);
        }

        public override void BuildWhere()
        {
            Sql.Append(" WHERE ");
            if (WhereFields==null ||!WhereFields.Any())
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
