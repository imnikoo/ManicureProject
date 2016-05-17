namespace Dataa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        MailNumber = c.Int(nullable: false),
                        Source = c.String(),
                        AdditionalInformation = c.String(),
                        City_Id = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        AlreadyPaid = c.Double(nullable: false),
                        ToPay = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        State = c.Int(nullable: false),
                        AdditionalInformation = c.String(),
                        ClientId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Int(nullable: false),
                        OriginalPrice = c.Double(nullable: false),
                        MarginalPrice = c.Double(nullable: false),
                        AdditionalInformation = c.String(),
                        CategoryId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PricePerPiece = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ApproximateArrivalDate = c.DateTime(),
                        ArrivalDate = c.DateTime(),
                        IsArrived = c.Boolean(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchasePlace", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.PurchasePlace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ClientId", "dbo.Client");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Purchase", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Purchase", "PlaceId", "dbo.PurchasePlace");
            DropForeignKey("dbo.Item", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Client", "City_Id", "dbo.City");
            DropIndex("dbo.Purchase", new[] { "ItemId" });
            DropIndex("dbo.Purchase", new[] { "PlaceId" });
            DropIndex("dbo.Item", new[] { "CategoryId" });
            DropIndex("dbo.OrderItem", new[] { "ItemId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "ClientId" });
            DropIndex("dbo.Client", new[] { "City_Id" });
            DropTable("dbo.PurchasePlace");
            DropTable("dbo.Purchase");
            DropTable("dbo.Item");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
            DropTable("dbo.Client");
            DropTable("dbo.City");
            DropTable("dbo.Category");
        }
    }
}
