using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    /// <summary>
    /// WEB API返回的通用结果模型
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 附带消息
        /// </summary>
        public string Message { get; set; }
    }
}