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

        public static MvcHtmlString CreateColumnControl(ColumnTypeBase column)
        {
            var uiFactory = GetViewFactory();
            if (column is DateType)
            {
                return MvcHtmlString.Create(uiFactory.CreateDateTypeUI(column));
            }
            if (column is NumberType)
            {
                return MvcHtmlString.Create(uiFactory.CreateNumberTypeUI(column));
            }
            if (column is RichTextType)
            {
                return MvcHtmlString.Create(uiFactory.CreateRichTextTypeUI(column));
            }
            if (column is LineTextType)
            {
                return MvcHtmlString.Create(uiFactory.CreateLineTextTypeUI(column));
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

            var builder = new SQLBuilder();
            builder.TabelName = tableInfo.Code;
            builder.Columns = tableInfo.Columns.Select(f => f.Code).ToArray();
            builder.Type = SQLBuilder.SqlType.INSERT;
            var sql = builder.ToString();

            using (var db = new CustomTableDatabaseContext())
            {
                db.Database.ExecuteSqlCommand(sql, model.GetSqlParameter());
            }

        }

        public static DataTable Query(string tableCode, string[] column = null)
        {
            SQLBuilder builder = new SQLBuilder();
            builder.Type = SQLBuilder.SqlType.SELECT;
            builder.TabelName = tableCode;
            builder.Columns = column;
            using (var db = new CustomTableDatabaseContext())
            {
                var ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.Text, builder.ToString());
                return ds.Tables[0];
            }
        }
        public static DataTable Query(string tableCode,int page,int pageSize, string[] column = null)
        {
            SQLBuilder builder = new SQLBuilder();
            builder.Type = SQLBuilder.SqlType.SELECT;
            builder.TabelName = tableCode;
            builder.Columns = column;
            builder.PageSize = pageSize;
            builder.Page = page;
            using (var db = new CustomTableDatabaseContext())
            {
                var ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.Text, builder.ToString());
                return ds.Tables[0];
            }
        }
        public static int QueryCount(string tableCode)
        {
            SQLBuilder builder = new SQLBuilder();
            builder.Type = SQLBuilder.SqlType.COUNT;
            builder.TabelName = tableCode;
            using (var db = new CustomTableDatabaseContext())
            {
               object obj = SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, builder.ToString());
               return Convert.ToInt32(obj);
            }
        }

        public static void DeleteData(string tableCode, int id)
        {
            SQLBuilder builder = new SQLBuilder();
            builder.TabelName = tableCode;
            builder.Type = SQLBuilder.SqlType.DELETE;
            using (var db = new CustomTableDatabaseContext())
            {
                db.Database.ExecuteSqlCommand(builder.ToString(), new SqlParameter("ID", id));
            }
        }


        #endregion


       
    }

    class SQLBuilder
    {
        public SQLBuilder()
        {
            Type = SqlType.NONE;
            PrimaryKey = "ID";
        }

        public override string ToString()
        {
            if (this.Type == SqlType.NONE)
            {
                return string.Empty;
            }
            StringBuilder sql = new StringBuilder();
            switch (Type)
            {
                case SqlType.COUNT:
                    sql.AppendFormat("SELECT COUNT(1) FROM {0} ", TabelName);
                    break;
                case SqlType.DELETE:
                    sql.AppendFormat("DELETE FROM {0} WHERE {1}=@{1} ", TabelName, PrimaryKey);
                    break;
                case SqlType.INSERT:
                    StringBuilder keys = new StringBuilder();
                    StringBuilder values = new StringBuilder();
                    foreach (var column in Columns)
                    {
                        keys.Append(column + ",");
                        values.Append("@" + column + ",");
                    }
                    keys.Remove(keys.Length - 1, 1);
                    values.Remove(values.Length - 1, 1);
                    sql.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2}) ", TabelName, keys, values);
                    break;
                case SqlType.SELECT:
                    if (Columns == null)
                    {
                        if (IsPaging)
                        {
                            //SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY keyField DESC) AS rowNum, * FROM tableName) AS t WHERE rowNum > start AND rowNum <= end
                            sql.AppendFormat(
                                "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {1}) AS rowNum, * FROM {0}) AS t WHERE rowNum > {2} AND rowNum <= {3} ",
                                TabelName, PrimaryKey, Page * PageSize, Page * PageSize + PageSize);
                        }
                        else
                        {
                            sql.AppendFormat("SELECT * FROM {0} ", TabelName);
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in Columns)
                        {
                            sb.AppendFormat("[{0}],", item);
                        }
                        if (Columns.Contains(PrimaryKey))
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        else
                        {
                            sb.AppendFormat("[{0}]", PrimaryKey);
                        }
                        if (IsPaging)
                        {
                            sql.AppendFormat(
                               "SELECT {4} FROM (SELECT ROW_NUMBER() OVER(ORDER BY {1}) AS rowNum, * FROM {0}) AS t WHERE rowNum > {2} AND rowNum <= {3} ",
                               TabelName, PrimaryKey, Page * PageSize, Page * PageSize + PageSize, sb);
                        }
                        else
                        {
                            sql.AppendFormat("SELECT {1} FROM {0} ", TabelName, sb);
                        }
                        
                    }
                    
                    
                    break;
                case SqlType.UPDATE:
                    StringBuilder col = new StringBuilder();
                    foreach (var column in Columns)
                    {
                        col.AppendFormat(" {0}=@{0},", column);
                    }
                    col.Remove(col.Length - 1, 1);
                    sql.AppendFormat("UPDATE {0} set {2} where {1}=@{1}", TabelName, PrimaryKey, col);
                    break;
            }
            return sql.ToString();
        }

        

        public string[] Columns { get; set; }
        /// <summary>
        /// 创建SQL语句类型
        /// </summary>
        public SqlType Type { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TabelName { get; set; }
        /// <summary>
        /// 标识主键
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 分页使用-页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 分页使用-记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPaging
        {
            get
            {
                return !(Page == PageSize && Page == 0);
            }
        }

        
        #region sqlType
        public enum SqlType
        {
            SELECT,
            UPDATE,
            INSERT,
            DELETE,
            COUNT,
            NONE
        }
        #endregion

        
        
    }

    
}
