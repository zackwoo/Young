using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable.View
{
    public abstract class ViewFactory
    {

        public abstract string CreateNumberTypeUI(ColumnTypeBase columnType);
        public abstract string CreateLineTextTypeUI(ColumnTypeBase columnType);
        public abstract string CreateRichTextTypeUI(ColumnTypeBase columnType);
        public abstract string CreateDateTypeUI(ColumnTypeBase columnType);
    }
}
