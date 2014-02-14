using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using Young.Web.Models;
using Young.Web.Models.EasyUIView;

namespace Young.Web.Controllers
{
    public class APISysConfigController : ApiController
    {
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns></returns>
        public PropertyGridModel GetSysConfigPropertyGrid()
        {
            var model = new SysConfigModel();
            PropertyGridModel pgModel = new PropertyGridModel();
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                var objs = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (objs.Any())
                {
                    var prop = new PropertyGridRowModel
                    {
                        group = ((DisplayAttribute)objs[0]).GroupName,
                        name = ((DisplayAttribute)objs[0]).Name,
                        value = propertyInfo.GetValue(model)
                    };
                    pgModel.rows.Add(prop);
                }
               
            }
            pgModel.total = pgModel.rows.Count;
            return pgModel;
        }

        private string GetModelDisplay(Expression<Func<SysConfigModel, object>> expression)
        {
            var model = new SysConfigModel();
            var sysCongifModel = expression.Parameters[0].Name;

            var propName =
                new Regex("(?<=" + sysCongifModel + @"\.)([a-zA-z_]+)",
                          RegexOptions.Singleline | RegexOptions.ExplicitCapture).Match(expression.Body.ToString())
                                                                                 .Value;
            var prop = model.GetType().GetProperty(propName);
            var objs = prop.GetCustomAttributes(typeof (DisplayAttribute), false);
            if (objs.Any())
            {
                return ((DisplayAttribute) objs[0]).Name;
            }
            return string.Empty;

        }
    }
}
