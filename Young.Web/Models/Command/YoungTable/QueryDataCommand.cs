using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Command.YoungTable
{
    public class QueryDataCommand : CommandBase
    {
        public QueryDataCommand()
        {
            this.CommandType = Models.Command.CommandType.Query;
        }
        /// <summary>
        /// 表code
        /// </summary>
        public string TableCode { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPaging { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页size
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 查询列
        /// </summary>
        public string[] Columns { get; set; }
    }
}