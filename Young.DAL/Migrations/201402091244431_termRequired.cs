namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class termRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities");
            DropIndex("dbo.TermEntities", new[] { "ParentId" });
            AlterColumn("dbo.TermEntities", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.TermEntities", "ParentId");
            AddForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities");
            DropIndex("dbo.TermEntities", new[] { "ParentId" });
            AlterColumn("dbo.TermEntities", "ParentId", c => c.Int());
            CreateIndex("dbo.TermEntities", "ParentId");
            AddForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities", "ID");
        }
    }
}
