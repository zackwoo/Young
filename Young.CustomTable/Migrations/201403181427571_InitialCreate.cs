namespace Young.CustomTable.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColumnTypeBases",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        IsRequired = c.Boolean(nullable: false),
                        IsNeedCustomValidation = c.Boolean(nullable: false),
                        CustomValidationRegularExpression = c.String(),
                        CustomValidationErrorMessage = c.String(),
                        DatabaseColumnLength = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        YoungTable_Code = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.YoungTables", t => t.YoungTable_Code)
                .Index(t => t.YoungTable_Code);
            
            CreateTable(
                "dbo.YoungTables",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ColumnTypeBases", "YoungTable_Code", "dbo.YoungTables");
            DropIndex("dbo.ColumnTypeBases", new[] { "YoungTable_Code" });
            DropTable("dbo.YoungTables");
            DropTable("dbo.ColumnTypeBases");
        }
    }
}
