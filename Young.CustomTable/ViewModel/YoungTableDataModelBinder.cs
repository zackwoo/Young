using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Young.CustomTable.ViewModel
{
    public class YoungTableDataModelBinder : System.Web.Mvc.IModelBinder
    {
        private const string _prefix = "young_";
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            
            YoungTableDataModel model =  new YoungTableDataModel();
            model.YoungProperty = new Dictionary<string, string>();
            var request = controllerContext.RequestContext.HttpContext.Request;
            var queryStringKeys = request.QueryString.AllKeys.Where(f => f.StartsWith(_prefix, true, null));
            foreach (var item in queryStringKeys)
            {
                
                model.YoungProperty.Add(item.Substring(_prefix.Length), bindingContext.ValueProvider.GetValue(item).AttemptedValue);
            }
            var formStringKeys = request.Form.AllKeys.Where(f => f.StartsWith(_prefix, true, null));
            foreach (var item in formStringKeys)
            {
                
                model.YoungProperty.Add(item.Substring(_prefix.Length), bindingContext.ValueProvider.GetValue(item).AttemptedValue);
            }

            return model;
        }
    }

}
