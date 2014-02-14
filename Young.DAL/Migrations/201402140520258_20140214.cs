namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20140214 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEntities", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserEntities", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserEntities", "LastActivityTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserEntities", "PasswordQuestion", c => c.String());
            AddColumn("dbo.UserEntities", "PasswordAnswer", c => c.String());
            DropColumn("dbo.UserEntities", "LastOperationTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEntities", "LastOperationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserEntities", "PasswordAnswer");
            DropColumn("dbo.UserEntities", "PasswordQuestion");
            DropColumn("dbo.UserEntities", "LastActivityTime");
            DropColumn("dbo.UserEntities", "IsDelete");
            DropColumn("dbo.UserEntities", "IsApproved");
        }
    }
}
