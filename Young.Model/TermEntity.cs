using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model
{
    /// <summary>
    /// 术语模型
    /// </summary>
    public class TermEntity : BaseGeneralEntity
    {

        public int ParentId { get; set; }

        public bool IsLeaf { get; set; }

        
    }
}
