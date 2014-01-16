using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class TermPropertyEntity : BasePropertyEntity
    {
        public override bool IsTerm
        {
            get { return true; }
        }
        public virtual TermEntity Value { get; set; }
    }
}
