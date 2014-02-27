using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Workflow.Model
{
    public class WorkflowModel
    {
        /// <summary>
        /// 工作流ID
        /// 不能采用int自增方式
        /// </summary>
        public string ID { get; set; }
    }
}
