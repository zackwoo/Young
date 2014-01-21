using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    /// <summary>
    /// 术语模型
    /// </summary>
    public class TermEntity : BaseGeneralEntity
    {
        [ForeignKey("ParentId")]
        public virtual TermEntity Parent { get; set; }

        public int ParentId { get; set; }

    }
}
