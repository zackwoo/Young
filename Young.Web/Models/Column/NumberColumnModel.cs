using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Column
{
    public class NumberColumnModel : ColumnModel
    {
        public override int ColumnType
        {
            get { return (int)Column.ColumnType.Number; }
        }
    }
}