using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [ForeignKey("ID")]
        public virtual TermEntity Parent { get; set; }
        [ForeignKey("ParentId")]
        public virtual ICollection<TermEntity> Chirdren { get; set; }

        public Nullable<int> ParentId { get; set; }

        //是否系统术语
        public bool IsSystem { get; set; }

    }
}
