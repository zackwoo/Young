using System.Collections.Generic;

namespace Young.Web.Models.Command
{
    /// <summary>
    /// 用于自定义列表API
    /// </summary>
    public class CustomListCommandModel
    {
        public CommandType CommandType { get; set; }

        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public List<CustomColumnModel> CustomColumnModels { get; set; }
    }

    public enum CommandType
    {
        Create,
        Edit,
        Delete
    }
}