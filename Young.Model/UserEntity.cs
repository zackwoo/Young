﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model.Base;

namespace Young.Model
{
    public class UserEntity : EntityBase
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsLock { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDelete { get; set; }
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        //注册时间
        public DateTime RegisterTime { get; set; }
        //最后一次操作时间
        public DateTime LastActivityTime { get; set; }
        //最后一次登陆时间
        public DateTime LastLoginTime { get; set; }
        //最近一次被锁定时间
        public DateTime LastLockoutTime { get; set; }
        //最后一次改变密码时间
        public DateTime LastPasswordChangedTime { get; set; }
        /// <summary>
        /// 用户的密码提示问题
        /// </summary>
        public string PasswordQuestion { get; set; }
        /// <summary>
        /// 用户的密码提示答案
        /// </summary>
        public string PasswordAnswer { get; set; }

        public virtual List<RoleEntity>Roles { get; set; }
        /// <summary>
        /// 员工所属部门
        /// </summary>
        public virtual TermEntity Department { get; set; }

        /// <summary>
        /// 员工所属职位
        /// </summary>
        public virtual TermEntity Position { get; set; }
    }
}
