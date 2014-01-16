using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class StringPropertyEntity : BasePropertyEntity
    {
        public override bool IsString
        {
            get { return true; }
        }
        public string Value { get; set; }
    }
}
