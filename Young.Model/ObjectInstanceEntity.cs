using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class ObjectInstanceEntity : EntityBase
    {
        public virtual ObjectTypeEntity ObjectTypeEntity { get; set; }

        public virtual List<BasePropertyEntity> BasePropertyEntities { get; set; }
    }
}
