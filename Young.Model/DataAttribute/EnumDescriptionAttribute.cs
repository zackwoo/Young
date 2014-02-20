using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model.DataAttribute
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public object Value { get; set; }
    }
}
