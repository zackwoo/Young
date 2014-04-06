using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Young.Web.Models.EasyUIView
{
    public class DataGridModel<TRow>
    {
        public DataGridModel()
        {
            rows = new List<TRow>();
        }
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        public List<TRow> rows { get; private set; }
    }

    /// <summary>
    /// 包装datatable
    /// </summary>
    public class DataGridModel : DataGridModel<Dictionary<string,object>>
    {
        public DataGridModel(DataTable table)
            :base()
        {
            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn column in table.Columns)
                {
                    result.Add(column.ColumnName, row[column]);
                }
                rows.Add(result);
            }
        }
    }


   
}