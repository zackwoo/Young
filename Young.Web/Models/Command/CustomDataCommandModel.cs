using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Command
{
    /// <summary>
    /// 自定义表单数据
    /// </summary>
    public class CustomDataCommandModel
    {
        /// <summary>
        /// 自定义列表名称
        /// </summary>
        public string CustomListName { get; set; }

        public CommandType CommandType { get; set; }

        public string JsonData { get; set; }

        /// <summary>
        /// 数据ID
        /// </summary>
        public int ID { get; set; }

    }
}