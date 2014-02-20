using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Young.Model;

namespace Young.Web.Models
{
    public class CustomColumnModel
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public CustomColumnType Type { get; set; }

        public string Condition { get; set; }

        public string Description { get; set; }
    }
}