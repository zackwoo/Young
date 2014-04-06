using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Command.YoungTable
{
    public class DeleteDataCommand : CommandBase
    {
        /// <summary>
        /// 表code
        /// </summary>
        public string TableCode { get; set; }
        /// <summary>
        /// 记录ID
        /// </summary>
        public int DataID { get; set; }
    }
}