namespace Rest.Web.Engineer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartProducts",
                c => new
                    {
                        CartProductId = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Cart_CartId = c.Long(),
                        Product_ProductId = c.Long(),
                    })
                .PrimaryKey(t => t.CartProductId)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Cart_CartId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Long(nullable: false, identity: true),
                        CookieToken = c.Guid(nullable: false),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
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
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        PaymentMethod = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Address = c.String(),
                        AddressTwo = c.String(),
                        PostalCode = c.String(),
                        Mail = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Cart_CartId = c.Long(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.Params",
                c => new
                    {
                        ParamId = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ParamId);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_CategoryId = c.Long(nullable: false),
                        Product_ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Product_ProductId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.CategoryProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Files", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.CartProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.CartProducts", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.CategoryProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_CategoryId" });
            DropIndex("dbo.Orders", new[] { "Cart_CartId" });
            DropIndex("dbo.Files", new[] { "Product_ProductId" });
            DropIndex("dbo.CartProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.CartProducts", new[] { "Cart_CartId" });
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.Params");
            DropTable("dbo.Orders");
            DropTable("dbo.Menus");
            DropTable("dbo.Categories");
            DropTable("dbo.Files");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
            DropTable("dbo.CartProducts");
        }
    }
}
