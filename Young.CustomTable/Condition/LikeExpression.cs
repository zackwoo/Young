using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.Condition
{
    public class LikeExpression : Expression
    {
        public override string ToSQL()
        {
            return string.Format(" [{0}] LIKE @{1} ", Name, ParameterKeys[0]);
        }
    }
}
