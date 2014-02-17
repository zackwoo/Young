using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    public class RoleEntity : EntityBase
    {
        public string Name { get; set; }
        /// <summary>
        /// 是否系统角色
        /// 系统角色不能删除
        /// </summary>
        public bool IsSystem { get; set; }

        public virtual List<UserEntity> Users { get; set; }
    }
}
