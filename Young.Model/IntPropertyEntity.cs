using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class IntPropertyEntity : BasePropertyEntity
    {

        public override bool IsInt
        {
            get { return true; }
        }
        public int Value { get; set; }
    }
}
