using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class DatePropertyEntity : BasePropertyEntity
    {

        public override bool IsDate
        {
            get { return true; }
        }
        public DateTime Value { get; set; }
    }
}
