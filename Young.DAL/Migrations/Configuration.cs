using Young.Model;
using Young.Model.Base;

namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Young.DAL.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Young.DAL.DataBaseContext context)
        {
                TermEntity root = new TermEntity
                {
                    Name = "���������",
                    ParentId = null,
                    Description = "�������Ｏ�ϸ��ڵ�",
                    IsSystem = true
                };
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Terms.AddOrUpdate(f => f.Name, root);
                    db.SaveChanges();
                    TermEntity sysNode = new TermEntity
                    {
                        Name = "ϵͳ���Ｏ��",
                        ParentId = root.ID,
                        IsSystem = true,
                        Description = "����ϵͳ������ڵ�"
                    };
                    db.Terms.AddOrUpdate(f => f.Name, sysNode);
                    TermEntity custNode = new TermEntity
                    {
                        Name = "�Զ������Ｏ��",
                        ParentId = root.ID,
                        IsSystem = true,
                        Description = "�Զ���������ڵ�"
                    };
                    db.Terms.AddOrUpdate(f => f.Name, custNode);
                   
                    db.SaveChanges();

                }
        }
    }
}
