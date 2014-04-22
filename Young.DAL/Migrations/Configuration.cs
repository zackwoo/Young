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
            #region �����ʼ��

            TermEntity root = new TermEntity
            {
                Name = "���������",
                ParentId = null,
                Description = "�������Ｏ�ϸ��ڵ�",
                IsSystem = true
            };
            using (var db = new DataBaseContext())
            {
                db.Terms.AddOrUpdate(f => f.Name, root);
                db.SaveChanges();
                var sysNode = new TermEntity
                {
                    Name = "ϵͳ���Ｏ��",
                    ParentId = root.ID,
                    IsSystem = true,
                    Description = "����ϵͳ������ڵ�"
                };
                db.Terms.AddOrUpdate(f => f.Name, sysNode);
                var custNode = new TermEntity
                {
                    Name = "�Զ������Ｏ��",
                    ParentId = root.ID,
                    IsSystem = true,
                    Description = "�Զ���������ڵ�"
                };
                db.Terms.AddOrUpdate(f => f.Name, custNode);

                db.SaveChanges();
                var departmentNode = new TermEntity
                {
                    Name = "����",
                    ParentId = sysNode.ID,
                    IsSystem = true,
                    Description = "Ա�����ڲ���"
                };
                var positionNode = new TermEntity
                {
                    Name = "ְλ",
                    ParentId = sysNode.ID,
                    IsSystem = true,
                    Description = "Ա������ְλ"
                };
                db.Terms.AddOrUpdate(f => f.Name, departmentNode);
                db.Terms.AddOrUpdate(f => f.Name, positionNode);
                db.SaveChanges();
            }

            #endregion

            #region role��ʼ��

            var adminRole = new RoleEntity { Name = "ϵͳ����Ա", IsSystem = true };
            var normalRole = new RoleEntity { Name = "��ͨ�û�", IsSystem = true };
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

            #region ϵͳ����Ա��ʼ��
            //����111111
            var adminUser = new UserEntity
            {
                UserName = "admin",
                DisplayName = "ϵͳ����Ա",
                PasswordQuestion = "��������",
                PasswordAnswer = "�����",
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
