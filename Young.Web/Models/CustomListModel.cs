using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    public class CustomListModel
    {
        [Display(Name = "列表名")]
        public string Name { get; set; }

        [Display(Name = "用途描述")]
        public string Description { get; set; }
    }
}