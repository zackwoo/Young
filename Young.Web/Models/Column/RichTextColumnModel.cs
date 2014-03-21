using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Column
{
    public class RichTextColumnModel:ColumnModel
    {
        public override int ColumnType
        {
            get { return (int) Column.ColumnType.RichText; }
        }
    }
}