using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.CustomTable.Attributes;
using Young.CustomTable.ColumnType;

namespace Young.CustomTable
{
    /// <summary>
    /// Young 自定义列表
    /// </summary>
    public class YoungTable
    {
        /// <summary>
        /// 内部编号
        /// </summary>
        [Key]
        public string Code { get;protected set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<ColumnTypeBase> Columns { get; set; }
    }
}
