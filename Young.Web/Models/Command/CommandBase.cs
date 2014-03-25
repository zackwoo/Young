using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Command
{
    public class CommandBase
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }
        /// <summary>
        /// 命令
        /// </summary>
        public string Command { get; set; }
    }
}