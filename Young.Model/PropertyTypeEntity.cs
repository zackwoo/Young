using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    public class PropertyTypeEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 是否系统自带属性（内置属性）
        /// </summary>
        public bool IsSystemProperty { get; set; }

        public PropertyType Type { get; set; }

        public virtual List<ObjectTypeEntity> ObjectEntities { get; set; }
    }

    public enum PropertyType
    {
        Boolean,
        Int,
        Double,
        Date,
        String,
        Term
    }
}
