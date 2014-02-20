using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.Model.DataAttribute;

namespace Young.Web
{
    public class Util
    {
        public static IEnumerable<SelectListItem> GetSelectListItemByEnum(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new NotSupportedException("传入参数类型不对");    
            }
            var displayList = new List<EnumDescriptionAttribute>();
            foreach (var enumName in enumType.GetEnumNames())
            {
                var field = enumType.GetField(enumName);
                var displayAttrs = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (displayAttrs.Any())
                {
                    var attr = displayAttrs[0] as EnumDescriptionAttribute;
                    displayList.Add(attr);
                }
                else
                {
                    
                    displayList.Add(new EnumDescriptionAttribute { Name = enumName, Order = int.MaxValue,Value = Enum.Parse(enumType,field.Name) });
                }
            }
            var foo = from bar in displayList
                      orderby bar.Order
                      select new SelectListItem {Text = bar.Name, Value = bar.Value.ToString()};
            return foo;
        }
    }
}