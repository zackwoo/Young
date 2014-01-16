using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class DoublePropertyEntity : BasePropertyEntity
    {

        public override bool IsDouble
        {
            get { return true; }
        }
        public double Value { get; set; }
    }
}
