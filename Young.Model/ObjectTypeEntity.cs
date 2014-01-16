using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    /// <summary>
    /// 对象实体
    /// </summary>
    public class ObjectTypeEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 对象属性集合
        /// </summary>
        public virtual List<PropertyTypeEntity> PropertyEntities { get; set; }

        /// <summary>
        /// 对象实例集合
        /// </summary>
        public virtual List<ObjectInstanceEntity> ObjectInstanceEntities { get; set; }


    }
}
