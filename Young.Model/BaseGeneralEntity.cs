using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    /// <summary>
    /// Young model 基类
    /// </summary>
    public class BaseGeneralEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
