using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.Condition
{
    /// <summary>
    /// 大于表达式
    /// </summary>
    public class GreaterThanExpression : Expression
    {
        public override string ToSQL()
        {
            return string.Format(" [{0}] > @{1} ", Name, ParameterKeys[0]);
        }
    }
}
