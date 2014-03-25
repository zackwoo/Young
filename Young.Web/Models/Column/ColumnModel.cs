using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Young.Util;

namespace Young.Web.Models.Column
{
    public class ColumnModel
    {
        public ColumnModel()
        {
            IsNew = true;//是否新建
        }

        public string TableName { get; set; }
        public string TableCode { get; set; }

        /// <summary>
        /// 内部编号
        /// </summary>
        [Display(Name = "栏编号")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "栏名称")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "栏描述")]
        public string Description { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        [Display(Name = "是否必填")]
        public bool IsRequired { get; set; }
        /// <summary>
        /// 是否需要自定义验证
        /// </summary>
        [Display(Name = "是否自定义验证")]
        public bool IsNeedCustomValidation { get; set; }
        /// <summary>
        /// 自定义验证正则表达式
        /// </summary>
        [Display(Name = "自定义验证正则表达式")]
        public string CustomValidationRegularExpression { get; set; }
        /// <summary>
        /// 自定义验证未通过提示
        /// </summary>
        [Display(Name = "自定义验证错误提示")]
        public string CustomValidationErrorMessage { get; set; }
        /// <summary>
        /// 指定对应数据库数据类型
        /// </summary>
        [Display(Name = "指定数据库字段类型")]
        public SqlDbType DatabaseType { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual ColumnType ColumnType { get; protected set; }

        public IEnumerable<SelectListItem> ColumnTypeList
        {
            get
            {
                return EnumHelper.GetEnumKV<ColumnType>().Select(keyValues => new SelectListItem
                    {
                        Text = keyValues.Value,
                        Value = keyValues.Key.ToString()
                    }).ToList();
            }
        }

        public IEnumerable<SelectListItem> DatabaseTypeList
        {
            get
            {
                return EnumHelper.GetEnumKV<SqlDbType>().Select(keyValues => new SelectListItem
                {
                    Text = keyValues.Value,
                    Value = keyValues.Key.ToString()
                }).ToList();
            }
        }

        /// <summary>
        /// 是否用于查询字段
        /// </summary>
        public bool IsForSearch { get; set; }
        /// <summary>
        /// 是否新建
        /// </summary>
        public bool IsNew { get; set; }
    }

    public enum ColumnType
    {
        [Display(Name = "单行文本类型")]
        LineText,
        [Display(Name = "富文本类型")]
        RichText,
        [Display(Name = "数字类型")]
        Number,
        [Display(Name = "日期类型")]
        Date
    }
}