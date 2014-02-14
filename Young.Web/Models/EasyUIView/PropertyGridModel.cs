using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.EasyUIView
{
    public class PropertyGridModel
    {
        public PropertyGridModel()
        {
            rows = new List<PropertyGridRowModel>();
        }
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        public List<PropertyGridRowModel> rows { get;private set; }
    }

    public class PropertyGridRowModel
    {
        public string name { get; set; }
        public string value { get; set; }
        public string group { get; set; }
        public object editor { get; set; }
    }
}