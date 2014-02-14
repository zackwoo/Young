using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.EasyUIView
{
    public class PropertyGridRowModel
    {
        public string name { get; set; }
        public object value { get; set; }
        public string group { get; set; }
        public object editor { get; set; }
    }
}