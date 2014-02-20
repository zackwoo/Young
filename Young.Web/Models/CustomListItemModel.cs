using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    public class CustomListItemModel
    {
        [Display(Name = "列表名")]
        public string Name { get; set; }

        [Display(Name = "用途描述")]
        public string Description { get; set; }

        public int ID { get; set; }
        [Display(Name = "创建日期")]
        public DateTime CreatData { get; set; }
    }
}