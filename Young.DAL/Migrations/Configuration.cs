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
                    Name = "术语管理集合",
                    ParentId = null,
                    Description = "管理术语集合根节点",
                    IsSystem = true
                };
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Terms.AddOrUpdate(f => f.Name, root);
                    db.SaveChanges();
                    TermEntity sysNode = new TermEntity
                    {
                        Name = "系统术语集合",
                        ParentId = root.ID,
                        IsSystem = true,
                        Description = "管理系统术语根节点"
                    };
                    db.Terms.AddOrUpdate(f => f.Name, sysNode);
                    TermEntity custNode = new TermEntity
                    {
                        Name = "自定义术语集合",
                        ParentId = root.ID,
                        IsSystem = true,
                        Description = "自定义术语根节点"
                    };
                    db.Terms.AddOrUpdate(f => f.Name, custNode);
                   
                    db.SaveChanges();

                }
        }
    }
}
