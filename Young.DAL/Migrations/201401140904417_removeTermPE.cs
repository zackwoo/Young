namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTermPE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TermPropertyEntity", "Value_ID", "dbo.TermEntities");
            DropIndex("dbo.TermPropertyEntity", new[] { "Value_ID" });
            DropColumn("dbo.BooleanPropertyEntity", "Value");
            DropColumn("dbo.DatePropertyEntity", "Value");
            DropColumn("dbo.DoublePropertyEntity", "Value");
            DropColumn("dbo.TermPropertyEntity", "Value_ID");
            DropColumn("dbo.StringPropertyEntity", "Value");
            DropColumn("dbo.IntPropertyEntity", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntPropertyEntity", "Value", c => c.Int(nullable: false));
            AddColumn("dbo.StringPropertyEntity", "Value", c => c.String());
            AddColumn("dbo.TermPropertyEntity", "Value_ID", c => c.Int());
            AddColumn("dbo.DoublePropertyEntity", "Value", c => c.Double(nullable: false));
            AddColumn("dbo.DatePropertyEntity", "Value", c => c.DateTime(nullable: false));
            AddColumn("dbo.BooleanPropertyEntity", "Value", c => c.Boolean(nullable: false));
            CreateIndex("dbo.TermPropertyEntity", "Value_ID");
            AddForeignKey("dbo.TermPropertyEntity", "Value_ID", "dbo.TermEntities", "ID");
        }
    }
}
