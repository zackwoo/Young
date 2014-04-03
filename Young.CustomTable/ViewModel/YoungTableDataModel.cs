using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Young.CustomTable.ViewModel
{
    /// <summary>
    /// 视图模型
    /// </summary>
    [ModelBinder(typeof(YoungTableDataModelBinder))]
    public class YoungTableDataModel
    {
        public Dictionary<string, string> YoungProperty { get; set; }

        
    }
}
