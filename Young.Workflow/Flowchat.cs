using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Workflow
{
    /// <summary>
    /// 流程图
    /// </summary>
    public class Flowchat
    {
        /// <summary>
        /// 流程名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动节点集合
        /// </summary>
        public ICollection<Activity> Activities { get; set; }

        public string Guid { get; set; }
    }
}
