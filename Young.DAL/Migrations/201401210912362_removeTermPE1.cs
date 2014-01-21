namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTermPE1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ObjectInstanceEntities", "ObjectTypeEntity_ID", "dbo.ObjectTypeEntities");
            DropForeignKey("dbo.ObjectTypeEntityPropertyTypeEntities", "ObjectTypeEntity_ID", "dbo.ObjectTypeEntities");
            DropForeignKey("dbo.ObjectTypeEntityPropertyTypeEntities", "PropertyTypeEntity_ID", "dbo.PropertyTypeEntities");
            DropForeignKey("dbo.BasePropertyEntities", "PropertyTypeEntity_ID", "dbo.PropertyTypeEntities");
            DropForeignKey("dbo.BasePropertyEntities", "ObjectInstanceEntity_ID", "dbo.ObjectInstanceEntities");
            DropForeignKey("dbo.BooleanPropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropForeignKey("dbo.DatePropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropForeignKey("dbo.DoublePropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropForeignKey("dbo.TermPropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropForeignKey("dbo.StringPropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropForeignKey("dbo.IntPropertyEntity", "ID", "dbo.BasePropertyEntities");
            DropIndex("dbo.ObjectInstanceEntities", new[] { "ObjectTypeEntity_ID" });
            DropIndex("dbo.ObjectTypeEntityPropertyTypeEntities", new[] { "ObjectTypeEntity_ID" });
            DropIndex("dbo.ObjectTypeEntityPropertyTypeEntities", new[] { "PropertyTypeEntity_ID" });
            DropIndex("dbo.BasePropertyEntities", new[] { "PropertyTypeEntity_ID" });
            DropIndex("dbo.BasePropertyEntities", new[] { "ObjectInstanceEntity_ID" });
            DropIndex("dbo.BooleanPropertyEntity", new[] { "ID" });
            DropIndex("dbo.DatePropertyEntity", new[] { "ID" });
            DropIndex("dbo.DoublePropertyEntity", new[] { "ID" });
            DropIndex("dbo.TermPropertyEntity", new[] { "ID" });
            DropIndex("dbo.StringPropertyEntity", new[] { "ID" });
            DropIndex("dbo.IntPropertyEntity", new[] { "ID" });
            CreateIndex("dbo.TermEntities", "ParentId");
            AddForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities", "ID");
            DropColumn("dbo.TermEntities", "IsLeaf");
            DropTable("dbo.ObjectInstanceEntities");
            DropTable("dbo.PropertyTypeEntities");
            DropTable("dbo.ObjectTypeEntities");
            DropTable("dbo.BasePropertyEntities");
            DropTable("dbo.ObjectTypeEntityPropertyTypeEntities");
            DropTable("dbo.BooleanPropertyEntity");
            DropTable("dbo.DatePropertyEntity");
            DropTable("dbo.DoublePropertyEntity");
            DropTable("dbo.TermPropertyEntity");
            DropTable("dbo.StringPropertyEntity");
            DropTable("dbo.IntPropertyEntity");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IntPropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StringPropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TermPropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DoublePropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DatePropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BooleanPropertyEntity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ObjectTypeEntityPropertyTypeEntities",
                c => new
                    {
                        ObjectTypeEntity_ID = c.Int(nullable: false),
                        PropertyTypeEntity_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ObjectTypeEntity_ID, t.PropertyTypeEntity_ID });
            
            CreateTable(
                "dbo.BasePropertyEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PropertyTypeEntity_ID = c.Int(),
                        ObjectInstanceEntity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ObjectTypeEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropertyTypeEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsSystemProperty = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ObjectInstanceEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ObjectTypeEntity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.TermEntities", "IsLeaf", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.TermEntities", "ParentId", "dbo.TermEntities");
            DropIndex("dbo.TermEntities", new[] { "ParentId" });
            CreateIndex("dbo.IntPropertyEntity", "ID");
            CreateIndex("dbo.StringPropertyEntity", "ID");
            CreateIndex("dbo.TermPropertyEntity", "ID");
            CreateIndex("dbo.DoublePropertyEntity", "ID");
            CreateIndex("dbo.DatePropertyEntity", "ID");
            CreateIndex("dbo.BooleanPropertyEntity", "ID");
            CreateIndex("dbo.BasePropertyEntities", "ObjectInstanceEntity_ID");
            CreateIndex("dbo.BasePropertyEntities", "PropertyTypeEntity_ID");
            CreateIndex("dbo.ObjectTypeEntityPropertyTypeEntities", "PropertyTypeEntity_ID");
            CreateIndex("dbo.ObjectTypeEntityPropertyTypeEntities", "ObjectTypeEntity_ID");
            CreateIndex("dbo.ObjectInstanceEntities", "ObjectTypeEntity_ID");
            AddForeignKey("dbo.IntPropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.StringPropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.TermPropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.DoublePropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.DatePropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.BooleanPropertyEntity", "ID", "dbo.BasePropertyEntities", "ID");
            AddForeignKey("dbo.BasePropertyEntities", "ObjectInstanceEntity_ID", "dbo.ObjectInstanceEntities", "ID");
            AddForeignKey("dbo.BasePropertyEntities", "PropertyTypeEntity_ID", "dbo.PropertyTypeEntities", "ID");
            AddForeignKey("dbo.ObjectTypeEntityPropertyTypeEntities", "PropertyTypeEntity_ID", "dbo.PropertyTypeEntities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ObjectTypeEntityPropertyTypeEntities", "ObjectTypeEntity_ID", "dbo.ObjectTypeEntities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ObjectInstanceEntities", "ObjectTypeEntity_ID", "dbo.ObjectTypeEntities", "ID");
        }
    }
}
