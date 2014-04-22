namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Young.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Young.DAL.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Young.DAL.DataBaseContext context)
        {
            #region 术语初始化

            TermEntity root = new TermEntity
            {
                Name = "术语管理集合",
                ParentId = null,
                Description = "管理术语集合根节点",
                IsSystem = true
            };
            using (var db = new DataBaseContext())
            {
                db.Terms.AddOrUpdate(f => f.Name, root);
                db.SaveChanges();
                var sysNode = new TermEntity
                {
                    Name = "系统术语集合",
                    ParentId = root.ID,
                    IsSystem = true,
                    Description = "管理系统术语根节点"
                };
                db.Terms.AddOrUpdate(f => f.Name, sysNode);
                var custNode = new TermEntity
                {
                    Name = "自定义术语集合",
                    ParentId = root.ID,
                    IsSystem = true,
                    Description = "自定义术语根节点"
                };
                db.Terms.AddOrUpdate(f => f.Name, custNode);

                db.SaveChanges();
                var departmentNode = new TermEntity
                {
                    Name = "部门",
                    ParentId = sysNode.ID,
                    IsSystem = true,
                    Description = "员工所在部门"
                };
                var positionNode = new TermEntity
                {
                    Name = "职位",
                    ParentId = sysNode.ID,
                    IsSystem = true,
                    Description = "员工所属职位"
                };
                db.Terms.AddOrUpdate(f => f.Name, departmentNode);
                db.Terms.AddOrUpdate(f => f.Name, positionNode);
                db.SaveChanges();
            }

            #endregion

            #region role初始化

            var adminRole = new RoleEntity { Name = "系统管理员", IsSystem = true };
            var normalRole = new RoleEntity { Name = "普通用户", IsSystem = true };
            if (!context.Roles.Any(f => f.Name == adminRole.Name))
            {
                context.Roles.Add(adminRole);
            }
            if (!context.Roles.Any(f => f.Name == normalRole.Name))
            {
                context.Roles.Add(normalRole);
            }
            context.SaveChanges();

            #endregion

            #region 系统管理员初始化
            //密码111111
            var adminUser = new UserEntity
            {
                UserName = "admin",
                DisplayName = "系统管理员",
                PasswordQuestion = "密码问题",
                PasswordAnswer = "密码答案",
                IsApproved = true,
                IsDelete = false,
                IsLock = false,
                Email = "zachary.woo@163.com",
                LastActivityTime = DateTime.Now,
                LastLockoutTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                LastPasswordChangedTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Password = "FDb+XQYHYoWFnAfFRNwIec8BV1g="
            };
            adminUser.Roles = new System.Collections.Generic.List<RoleEntity>();
            adminUser.Roles.Add(adminRole);
            if (!context.Users.Any(f => f.UserName == adminUser.UserName))
            {
                context.Users.Add(adminUser);
            }
            context.SaveChanges();

            #endregion
        }
    }
}
