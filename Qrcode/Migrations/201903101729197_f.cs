namespace Qrcode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(),
                        Size = c.String(),
                        UnitsInStock = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.QrCodeInfoes", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.QrCodeInfoes",
                c => new
                    {
                        QrCodeInfoID = c.Int(nullable: false, identity: true),
                        SKT = c.DateTime(nullable: false),
                        TETT = c.DateTime(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QrCodeInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductID", "dbo.QrCodeInfoes");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "ProductID" });
            DropTable("dbo.QrCodeInfoes");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
