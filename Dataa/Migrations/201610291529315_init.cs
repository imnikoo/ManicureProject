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
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Source = c.String(),
                        AdditionalInformation = c.String(),
                        CityId = c.Int(),
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        AlreadyPaid = c.Double(nullable: false),
                        ToPay = c.Double(nullable: false),
                        Discount = c.String(),
                        MailNumber = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        AdditionalInformation = c.String(),
                        CityId = c.Int(nullable: false),
                        Reciever = c.String(),
                        PhoneNumber = c.String(),
                        ClientId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        Order_Id = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Stock = c.Int(nullable: false),
                        OriginalPrice = c.Double(nullable: false),
                        MarginalPrice = c.Double(nullable: false),
                        AdditionalInformation = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
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
                        TrackNumber = c.String(),
                        PurchasePlaceId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchasePlace", t => t.PurchasePlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.PurchasePlaceId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.PurchasePlace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ClientId", "dbo.Client");
            DropForeignKey("dbo.OrderItem", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Purchase", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Purchase", "PurchasePlaceId", "dbo.PurchasePlace");
            DropForeignKey("dbo.Item", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Order", "CityId", "dbo.City");
            DropForeignKey("dbo.Client", "CityId", "dbo.City");
            DropIndex("dbo.Purchase", new[] { "ItemId" });
            DropIndex("dbo.Purchase", new[] { "PurchasePlaceId" });
            DropIndex("dbo.Item", new[] { "CategoryId" });
            DropIndex("dbo.OrderItem", new[] { "Order_Id" });
            DropIndex("dbo.OrderItem", new[] { "ItemId" });
            DropIndex("dbo.Order", new[] { "ClientId" });
            DropIndex("dbo.Order", new[] { "CityId" });
            DropIndex("dbo.Client", new[] { "CityId" });
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
