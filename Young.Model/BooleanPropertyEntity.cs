using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class BooleanPropertyEntity : BasePropertyEntity
    {
        public override bool IsBoolean
        {
            get { return true; }
        }

        public bool Value { get; set; }
    }
}
