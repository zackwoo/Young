using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Young.Web.Models.Column
{
    public class LineTextColumnModel : ColumnModel
    {
        public override int ColumnType
        {
            get { return (int)Column.ColumnType.LineText; }
        }
        
        [Display(Name="数据长度")]
        public int Length { get; set; }
    }
}