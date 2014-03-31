using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.DynamicTable
{
    public class DataEditorModel
    {
        public string TableName { get; set; }
        public string TableCode { get; set; }

        public IEnumerable<Young.CustomTable.ColumnType.ColumnTypeBase> ColumnTypes { get; set; }
    }
}