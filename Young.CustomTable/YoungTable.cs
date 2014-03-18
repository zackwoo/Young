using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }


        public virtual ICollection<ColumnTypeBase> Columns { get; set; }
    }
}
