using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable
{
    public static class ColumnTypeFactory
    {
        public static NumberType CreateNumberType()
        {
            return new NumberType();
        }

        public static LineTextType CreateLineTextType()
        {
            return new LineTextType();
        }

        public static RichTextType CreateRichTextType()
        {
            return new RichTextType();
        }

        public static DateType CreateDateType()
        {
            return new DateType();
        }
        
    }
}
