using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.ColumnType
{
    /// <summary>
    /// 列类型基类
    /// </summary>
    public class ColumnTypeBase
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
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// 是否需要自定义验证
        /// </summary>
        public bool IsNeedCustomValidation { get; set; }
        /// <summary>
        /// 自定义验证正则表达式
        /// </summary>
        public string CustomValidationRegularExpression { get; set; }
        /// <summary>
        /// 自定义验证未通过提示
        /// </summary>
        public string CustomValidationErrorMessage { get; set; }
        /// <summary>
        /// 指定对应数据库数据类型
        /// </summary>
        public System.Data.SqlDbType DatabaseType { get; set; }
        /// <summary>
        /// 是否用于搜索字段
        /// </summary>
        public bool IsForSearch { get; set; }
        /// <summary>
        /// 是否用于列表显示
        /// </summary>
        public bool IsForList { get; set; }

        public virtual YoungTable Table { get; set; }
    }
}
