using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Young.CustomTable.ViewModel
{
    /// <summary>
    /// 视图模型
    /// </summary>
    [ModelBinder(typeof(YoungTableDataModelBinder))]
    public class YoungTableDataModel
    {
        public Dictionary<string, string> YoungProperty { get; set; }

        public SqlParameter[] GetSqlParameter()
        {
            SqlParameter[] sp = new SqlParameter[YoungProperty.Count];
            var index = 0;
            foreach (var item in YoungProperty)
            {
                sp[index++] = new SqlParameter(item.Key,item.Value);
            }
            return sp;
        }
    }
}
