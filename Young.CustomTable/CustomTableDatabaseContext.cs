using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable
{
    class CustomTableDatabaseContext : DbContext
    {
        public CustomTableDatabaseContext()
            : base("YoungDB")
        {

        }
         
        public DbSet<YoungTable> YoungTables { get; set; }
        public DbSet<ColumnTypeBase> Columns { get; set; }
        public DbSet<DateType> DateTypes { get; set; }
        public DbSet<LineTextType> LineTextTypes { get; set; }
        public DbSet<NumberType> NumberTypes { get; set; }
        public DbSet<RichTextType> RichTextTypes { get; set; }
    }
}
