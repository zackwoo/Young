using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    internal class SQLSelectBuilder : SQLBuilder
    {
        private const string SQL_ROW_Field = "young_row_num";
        public const string SQL_MIN_ROW_Field = "young_row_num_min";
        public const string SQL_MAX_ROW_Field = "young_row_num_max";


        public override void BuildFields()
        {
            StringBuilder sqlFields = new StringBuilder();
            if (!Fields.Contains(PrimaryKey))
            {
                sqlFields.AppendFormat("[{0}],", PrimaryKey);
            }
            for (int i = 0; i < Fields.Length; i++)
            {
                sqlFields.AppendFormat("[{0}]", Fields[i]);
                if (i < Fields.Length - 1)
                {
                    sqlFields.Append(",");
                }
            }
            if (!IsPaging)
            {
                Sql.AppendFormat("SELECT {1} FROM {0} ", TabelName, sqlFields);
            }
            else
            {
                Sql.AppendFormat("SELECT {1} FROM (SELECT ROW_NUMBER() OVER(ORDER BY ID) AS {2}, {1} FROM {0}) as Temp ", TabelName, sqlFields, SQL_ROW_Field);
            }
        }      

        public override void BuildWhere()
        {
            if (!IsPaging)
            {
                base.BuildWhere();
                return;
            }
            Sql.AppendFormat(" WHERE {0} > @{1} AND {0} <= @{2} ", SQL_ROW_Field, SQL_MIN_ROW_Field, SQL_MAX_ROW_Field);
            if (WhereFields != null && WhereFields.Any())
            {
                Sql.Append("AND");
                for (int i = 0; i < WhereFields.Length; i++)
                {
                    Sql.AppendFormat(" {0}=@{0} ", WhereFields[i]);
                    if (i < WhereFields.Length - 1)
                    {
                        Sql.Append("AND");
                    }
                }
            }            
        }

        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPaging { get; set; }
    }
}
