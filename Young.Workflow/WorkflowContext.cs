using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Workflow.Model;

namespace Young.Workflow
{
    /// <summary>
    /// 工作流
    /// </summary>
    public class WorkflowContext
    {
        public WorkflowModel WorkflowModel { get; private set; }
        public string UserName { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="wfId">工作流ID</param>
        /// <param name="userName">登陆用户</param>
        public WorkflowContext(string wfId, string userName)
        {
            //TODO SET WorkflowModel
            WorkflowModel = GetWorkflowModelByID(wfId);
            UserName = userName;
        }
        private WorkflowModel GetWorkflowModelByID(string id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 用户是否有权限查看列表
        /// </summary>
        /// <param name="wfId">工作流ID</param>
        /// <param name="userName">用户登录名</param>
        /// <returns></returns>
        public bool CanViewList
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 用户是否有发起流程的权限
        /// </summary>
        /// <param name="wfId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CanStartWork
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 获取用户对于该工作流有权处理的流程
        /// </summary>
        /// <param name="wfId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<ProcessModel> GetAllProcess()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取该处理节点下的数据
        /// </summary>
        /// <param name="wfId">工作流ID</param>
        /// <param name="psId">处理流程ID</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页size</param>
        /// <returns></returns>
        public List<object> GetProcessData(string psId, int page = 0, int rows = 0)
        {
            throw new NotImplementedException();
        }
    }
}
