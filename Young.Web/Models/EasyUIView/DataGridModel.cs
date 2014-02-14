using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models.EasyUIView
{
    public class DataGridModel<TRow>
    {
        public DataGridModel()
        {
            rows = new List<TRow>();
        }
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        public List<TRow> rows { get; private set; }
    }

   
}