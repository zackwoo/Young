using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    public class CustomListDataEntity : EntityBase
    {
        /// <summary>
        /// 自定义列表数据，采用JSON格式存储
        /// </summary>
        public string Data { get; set; }

        public virtual CustomListEntity CustomListEntity { get; set; }
    }

}
