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
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程
        /// </summary>
        public List<ProcessModel> Processes { get; set; }
        /// <summary>
        /// 启动流程
        /// </summary>
        public ProcessModel StartProcess { get; set; }
    }
}
