using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable
{
    public static class CustomTableTools
    {
        #region table
        public static void AddCustomTable(YoungTable table)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                db.YoungTables.Add(table);
                db.SaveChanges();
            }
        }

        public static void DeleteCustomTable(string code)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var item = db.YoungTables.Single(f => f.Code == code);
                db.YoungTables.Remove(item);
                db.SaveChanges();
            }
        }

        public static YoungTable GetTableByName(string name,bool loadColumn = false)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.SingleOrDefault(f => f.Name == name);
                if (loadColumn)
                {
                    table.Columns.ToList();
                }
                return table;
            }
        }

        public static YoungTable GetTableByCode(string code, bool loadColumn = false)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.SingleOrDefault(f => f.Code == code);
                if (loadColumn)
                {
                    table.Columns.ToList();
                }
                return table;
            }
        }

        public static IEnumerable<YoungTable> GetTableByPaging(int pageIndex, int pageSize)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var list = db.YoungTables.OrderByDescending(f => f.CreateTime).Skip(pageIndex * pageSize).Take(pageSize);
                return list.ToList();
            }
        }

        #endregion

        #region columns

        public static void AddColumn(string tableName, ColumnTypeBase column)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.Single(f => f.Name == tableName);
                table.Columns.Add(column);
                db.SaveChanges();
            }
        }

        public static ColumnTypeBase GetColumn(string tableName, string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.Single(f => f.Name == tableName);
                return table.Columns.SingleOrDefault(f => f.Code == columnCode);                
            }
        }

        public static void DeleteColumn(string tableName, string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.Single(f => f.Name == tableName);
                var column = table.Columns.SingleOrDefault(f => f.Code == columnCode);
                if (column == null) return;
                table.Columns.Remove(column);
                db.SaveChanges();
            }
        }

        public static void EditColumn(string tableName, ColumnTypeBase item)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var table = db.YoungTables.Single(f => f.Name == tableName);
                var column = table.Columns.SingleOrDefault(f => f.Code == item.Code);
                if (column == null) return;
                //column.CustomValidationErrorMessage = item.CustomValidationErrorMessage;
                //column.CustomValidationRegularExpression = item.CustomValidationRegularExpression;
                //column.Description = item.Description;
                //column.IsNeedCustomValidation = item.IsNeedCustomValidation;
                //column.IsRequired = item.IsRequired;
                //column.Name = item.Name;
                db.Columns.Remove(column);
                table.Columns.Add(item);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
