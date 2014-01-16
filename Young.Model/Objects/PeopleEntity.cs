using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Model.Objects
{
    public class PeopleEntity : BaseGeneralEntity
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
}
