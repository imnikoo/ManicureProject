namespace Dataa.Migrations
{
    using ManicureDomain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.EntityFramework.ManicureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.EntityFramework.ManicureContext context)
        {
            List<PurchasePlace> PurchasePlaces = new List<PurchasePlace>
            {
                new PurchasePlace { Title="Aliexpress" },
                new PurchasePlace {  Title="Ebay" },
                new PurchasePlace {  Title="OLX" },
            };

            context.PurchasePlaces.AddRange(PurchasePlaces);
            context.SaveChanges();

            IList<Category> Categories = new List<Category>
            {
                new Category { Title = "Кисточка" },
                new Category { Title = "Лак" },
                new Category { Title = "Стемпинг" },
                new Category { Title = "Маникюрные штуки" },
                new Category { Title = "Другое" },
            };
            context.Categories.AddRange(Categories);
            context.SaveChanges();

            IList<Item> Items = new List<Item>
            {
                new Item { Name="Кисточки для рисовашек",   Amount=5, CategoryId=1, OriginalPrice=50, MarginalPrice=100, Purchases = new List<Purchase> { }},
                new Item { Name="Кусачки",                  Amount=7, CategoryId=2, OriginalPrice=10, MarginalPrice=15, Purchases = new List<Purchase> { } },
                new Item { Name="Тату звездочка",           Amount=10,CategoryId=3, OriginalPrice=50, MarginalPrice=70, Purchases = new List<Purchase> { }},
                new Item { Name="Машинка для выбрации",     Amount=3, CategoryId=4, OriginalPrice=100, MarginalPrice=200, Purchases = new List<Purchase> { } },
                new Item { Name="Красный лак горькость",    Amount=1, CategoryId=5, OriginalPrice=10, MarginalPrice=25, Purchases = new List<Purchase> { } },
                new Item { Name="Наклейка осень",           Amount=4, CategoryId=1, OriginalPrice=12, MarginalPrice=30, Purchases = new List<Purchase> { } },
                new Item { Name="Наклейка весна",           Amount=1, CategoryId=2, OriginalPrice=13, MarginalPrice=27, Purchases = new List<Purchase> { } },
                new Item { Name="Чехол айфон",              Amount=0, CategoryId=3, OriginalPrice=5, MarginalPrice=15, Purchases = new List<Purchase> { } },
                new Item { Name="Чехол для всех",           Amount=3, CategoryId=4, OriginalPrice=7, MarginalPrice=15, Purchases = new List<Purchase> { } },
                new Item { Name="Что-то",                   Amount=1, CategoryId=5, OriginalPrice=99.9, MarginalPrice=199.99, Purchases = new List<Purchase> { } },
            };

            context.Items.AddRange(Items);
            context.SaveChanges();

            IList<Purchase> Purchases = new List<Purchase>
            {
                new Purchase { Amount=10, OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=42, ItemId = 1 },
                new Purchase { Amount=20, OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=39, ItemId = 1 },
            };
            context.Purchases.AddRange(Purchases);
            context.SaveChanges();


            IList<City> Cities = new List<City>
            {
                new City { Title = "Днепропетровск" },
                new City { Title = "Киев" },
                new City { Title = "Харьков" }
            };
            context.Cities.AddRange(Cities);

            IList<Client> Clients = new List<Client>
            {
                new Client { Name="Анечка", MailNumber=7, PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[0], },
                new Client { Name="Валечка", MailNumber=3, PhoneNumber="+380930986252", Source="Инстаграмм",  City = Cities[1],  },
                new Client { Name="Галечка", MailNumber=14, PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[2],  }
            };

            context.Clients.AddRange(Clients);
            context.SaveChanges();
        }
    }
}
