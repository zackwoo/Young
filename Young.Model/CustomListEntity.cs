using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    public class CustomListEntity : BaseGeneralEntity
    {
        //自定义列
        public virtual ICollection<CustomColumnEntity> CustomColumnEntities { get; set; }
        
        //自定义列表数据集
        public virtual ICollection<CustomListDataEntity> DataEntities { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatData { get; set; }
    }
}
