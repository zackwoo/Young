using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.CustomList
{
    public class TableDetailModel
    {
        public string TableName { get; set; }

        public string TableCode { get; set; }

        public IEnumerable<Column.ColumnModel> Columns { get; set; }

        public IEnumerable<Column.ColumnModel> SearchColumns { get; set; }
    }
}