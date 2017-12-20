namespace Rest.Web.Engineer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Long(nullable: false, identity: true),
                        FileIdExt = c.Long(),
                        Parts = c.Int(),
                        MimeType = c.String(),
                        Content = c.Binary(),
                        Product_ProductId = c.Long(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Href = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_ProductId = c.Long(nullable: false),
                        Category_CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductId, t.Category_CategoryId })
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Files", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "Product_ProductId" });
            DropIndex("dbo.Files", new[] { "Product_ProductId" });
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Menus");
            DropTable("dbo.Files");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
