using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Young.CustomTable.ColumnType;
using Young.CustomTable.View;
using Young.CustomTable.ViewModel;

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
                ExecuteCreateColumnSql(db.Database, table.Code, column);
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
        /// <summary>
        /// 设置列为搜索列
        /// </summary>
        /// <param name="columnCode"></param>
        public static void SetSearchColumn(string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var column = db.Columns.Single(f => f.Code == columnCode);
                column.IsForSearch = true;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 移除搜索列
        /// </summary>
        /// <param name="columnCode"></param>
        public static void RemoveSearchColumn(string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var column = db.Columns.Single(f => f.Code == columnCode);
                column.IsForSearch = false;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 设置列表显示列
        /// </summary>
        /// <param name="columnCode"></param>
        public static void SetListColumn(string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var column = db.Columns.Single(f => f.Code == columnCode);
                column.IsForList = true;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 移除列表显示列
        /// </summary>
        /// <param name="columnCode"></param>
        public static void RemoveListColumn(string columnCode)
        {
            using (var db = new CustomTableDatabaseContext())
            {
                var column = db.Columns.Single(f => f.Code == columnCode);
                column.IsForList = false;
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
                db.Columns.Remove(column);
                table.Columns.Add(item);
                db.SaveChanges();
            }
        }

        #endregion

        #region raw sql

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
                sb.Append(" NOT NULL ");
            }
            return sb.ToString();
        }

        #endregion

        #region 创建列控件

        public static MvcHtmlString CreateColumnControl(ColumnTypeBase column,string value)
        {
            var uiFactory = GetViewFactory();
            if (column is DateType)
            {
                return MvcHtmlString.Create(uiFactory.CreateDateTypeUI(column, value));
            }
            if (column is NumberType)
            {
                return MvcHtmlString.Create(uiFactory.CreateNumberTypeUI(column, value));
            }
            if (column is RichTextType)
            {
                return MvcHtmlString.Create(uiFactory.CreateRichTextTypeUI(column, value));
            }
            if (column is LineTextType)
            {
                return MvcHtmlString.Create(uiFactory.CreateLineTextTypeUI(column, value));
            }
            return MvcHtmlString.Empty;
        }

        private static ViewFactory GetViewFactory()
        {
            var ui = System.Configuration.ConfigurationManager.AppSettings["UIFactory"];
            if (!string.IsNullOrWhiteSpace(ui) && ui.Trim().ToLower() == "jqueryui")
            {
                return new JqueryUIFactory();
            }
            return new EasyUIFactory();
        }

        #endregion

        #region DynamicData

        public static void SaveData(YoungTableDataModel model,string tableCode)
        {
            var tableInfo = GetTableByCode(tableCode, true);

            var builder = new SQLInsertBuilder();
            builder.TabelName = tableInfo.Code;
            builder.Fields = tableInfo.Columns.Select(f => f.Code).ToArray();
            builder.Init();
            builder.BuildFields();
            var sql = builder.GetResult();
            using (var db = new CustomTableDatabaseContext())
            {
                db.Database.ExecuteSqlCommand(sql, model.GetSqlParameter());
            }

        }

        public static DataTable Query(string tableCode, string[] column)
        {
            var builder = new SQLSelectBuilder();
            builder.TabelName = tableCode;
            builder.Fields = column;
            builder.IsPaging = false;
            builder.Init();
            builder.BuildFields();
            builder.BuildWhere();
            builder.BuildOrder();
            using (var db = new CustomTableDatabaseContext())
            {
                var ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.Text, builder.GetResult());
                return ds.Tables[0];
            }
        }
        public static DataTable Query(string tableCode,int page,int pageSize, string[] column )
        {
            var builder = new SQLSelectBuilder();
            builder.TabelName = tableCode;
            builder.Fields = column;
            builder.IsPaging = true;
            builder.Init();
            builder.BuildFields();
            builder.BuildWhere();
            builder.BuildOrder();
            using (var db = new CustomTableDatabaseContext())
            {
                var ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.Text, builder.GetResult()
                    , new SqlParameter(SQLSelectBuilder.SQL_MIN_ROW_Field, page * pageSize)
                    , new SqlParameter(SQLSelectBuilder.SQL_MAX_ROW_Field, pageSize * (page + 1)));
                return ds.Tables[0];
            }
        }
        public static int QueryCount(string tableCode)
        {
            var builder = new SQLCountBuilder();
            builder.TabelName = tableCode;
            builder.Init();
            builder.BuildWhere();
            using (var db = new CustomTableDatabaseContext())
            {
               object obj = SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, builder.GetResult());
               return Convert.ToInt32(obj);
            }
        }

        public static void DeleteData(string tableCode, int id)
        {
            var builder = new SQLDeleteBuilder();
            builder.TabelName = tableCode;
            builder.Init();
            builder.BuildWhere();
            using (var db = new CustomTableDatabaseContext())
            {
                db.Database.ExecuteSqlCommand(builder.GetResult(), new SqlParameter("ID", id));
            }
        }


        #endregion


       
    }

   

    
}
