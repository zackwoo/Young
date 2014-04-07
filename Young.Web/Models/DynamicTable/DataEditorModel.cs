using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.DynamicTable
{
    public class DataEditorModel
    {
        public DataEditorModel()
        {
            Valus = new Dictionary<string, string>();
        }

        public string TableName { get; set; }
        public string TableCode { get; set; }
        public int DataID { get; set; }


        public IEnumerable<Young.CustomTable.ColumnType.ColumnTypeBase> ColumnTypes { get; set; }

        public Dictionary<string,string> Valus { get;private set; }
        /// <summary>
        /// 获取列值
        /// </summary>
        /// <param name="columnCode"></param>
        /// <returns></returns>
        public string GetValue(string columnCode)
        {
            if (!Valus.ContainsKey(columnCode))
            {
                return string.Empty;
            }
            return Valus[columnCode];
        }
    }
}