﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
                ExecuteCreateTabelSql(db.Database, table.Code);
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
                ExecuteCreateColumnSql(db.Database, table.Code, column);
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

        public static void DeleteColumn( string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var column = db.Columns.Single(f => f.Code == columnCode);                
                ExecuteDropColumnSql(db.Database, column.Table.Code, column.Code);
                db.Columns.Remove(column);
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

        #region auto set database

        private static void ExecuteCreateTabelSql(Database db, string tcode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"if(not Exists(Select * From SysObjects Where xtype='U' And Name='{0}')) 
                                    begin
                                    CREATE TABLE {0}
                                    ( ID int NOT NULL IDENTITY(1,1) PRIMARY KEY) 
                                    end ", tcode);
            db.ExecuteSqlCommand(sql.ToString());
        }

        private static void ExecuteCreateColumnSql(Database db, string tcode, ColumnTypeBase column)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"if not exists(select * from syscolumns where id=object_id('{0}') and name='{1}') 
                                        begin
                                        --添加列
                                        ALTER TABLE {0} ADD [{1}] {2}
                                        END
                                        ELSE BEGIN
                                        --修改列
                                        ALTER TABLE {0} alter column [{1}] {2}
                                        end", tcode, column.Code, GetColumnSQL(column));
            db.ExecuteSqlCommand(sql.ToString());
        }

        private static void ExecuteDropColumnSql(Database db, string tableCode, string columnCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" ALTER TABLE {0}   
                                DROP COLUMN {1} ", tableCode, columnCode);
            db.ExecuteSqlCommand(sql.ToString());
        }

        public static void ResetDatabase(string tcode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                 var table = db.YoungTables.Single(f => f.Code == tcode);
                 ExecuteCreateTabelSql(db.Database, tcode);
                 foreach (var item in table.Columns)
                 {
                     ExecuteCreateColumnSql(db.Database, tcode, item);
                 }
                 
            }
        }

        private static string GetColumnSQL(ColumnTypeBase col)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(col.DatabaseType);
            if (col is LineTextType)
            {
                sb.AppendFormat(" ({0}) ", ((LineTextType)col).DatabaseColumnLength);
            }
            if (col.IsRequired)
            {
                sb.Append("NOT NULL ");
            }
            return sb.ToString();
        }

        #endregion
    }
}
