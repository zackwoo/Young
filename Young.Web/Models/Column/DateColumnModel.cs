using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Column
{
    public class DateColumnModel:ColumnModel
    {
        public override ColumnType ColumnType
        {
            get { return Column.ColumnType.Date; }
            
        }
    }
}