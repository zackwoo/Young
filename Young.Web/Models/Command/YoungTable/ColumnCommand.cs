using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Command.YoungTable
{
    /// <summary>
    /// 列命令
    /// </summary>
    public class ColumnCommand : CommandBase
    {
        /// <summary>
        /// 列Code
        /// </summary>
        public string ColumnCode { get; set; }
    }
}