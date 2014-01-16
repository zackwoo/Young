using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    /// <summary>
    /// 属性基类
    /// </summary>
    public abstract class BasePropertyEntity : EntityBase
    {
       
        public virtual PropertyTypeEntity PropertyTypeEntity { get; set; }

        public virtual ObjectInstanceEntity ObjectInstanceEntity { get; set; }

        

        /// <summary>
        /// 是否是术语
        /// </summary>
        public virtual bool IsTerm { get { return false; } }

        /// <summary>
        /// 是否是Int型
        /// </summary>
        public virtual bool IsInt { get { return false; } }

        /// <summary>
        /// 是否是Date型
        /// </summary>
        public virtual bool IsDate { get { return false; } }

        /// <summary>
        /// 是否是String型
        /// </summary>
        public virtual bool IsString { get { return false; } }

        /// <summary>
        /// 是否是double型
        /// </summary>
        public virtual bool IsDouble { get { return false; } }

        /// <summary>
        /// 是否是boolean型
        /// </summary>
        public virtual bool IsBoolean { get { return false; } }
    }
}
