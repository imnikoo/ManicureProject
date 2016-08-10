using ManicureDomain.Entities;
using ManicureDomain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace ManicureDomain.Data
{
    public static class Storage
    {
        public static List<PurchasePlace> PurchasePlaces = new List<PurchasePlace>
        {
            new PurchasePlace { Id = 1, Title="Aliexpress" },
            new PurchasePlace { Id = 2, Title="Ebay" },
        };

        public static IList<Category> Categories = new List<Category> {
            new Category { Id = 1, Title = "Кисточка" },
            new Category { Id = 2, Title = "Лак" },
            new Category { Id = 3, Title = "Стемпинг" },
            new Category { Id = 4, Title = "Маникюрные штуки" },
            new Category { Id = 5, Title = "Другое" },
        };

        public static IList<Purchase> Purchases = new List<Purchase>
        {
            new Purchase { Amount=10, /*Item=Items[2],*/ OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, PurchasePlace=PurchasePlaces[0], PricePerPiece=42 },
            new Purchase { Amount=20, /*Item=Items[2],*/ OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, PurchasePlace=PurchasePlaces[0], PricePerPiece=39 },
        };

        public static IList<City> Cities = new List<City>
        {
            new City { Id = 1, Title = "Днепропетровск" },
            new City { Id = 2, Title = "Киев" },
            new City { Id = 3, Title = "Харьков" }
        };

        public static IList<Client> Clients = new List<Client>
        {
            new Client { Id = 1, FirstName="Анечка", LastName="Анечка", PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[0], },
            new Client { Id = 2, FirstName="Валечка", LastName="Анечка", PhoneNumber="+380930986252", Source="Инстаграмм",  City = Cities[1],  },
            new Client { Id = 3, FirstName="Галечка", LastName="Анечка", PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[2],  }
        };

        public static IList<Item> Items = new List<Item>
        {
            new Item { Id = 1, Title="Кисточки для рисовашек", Stock=5, Category=Categories[0], OriginalPrice=50, MarginalPrice=100, Purchases = new List<Purchase> { Purchases[0], Purchases[1] }},
            new Item { Id = 2, Title="Кусачки", Stock=7, Category=Categories[3], OriginalPrice=10, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 3, Title="Тату звездочка", Stock=10, Category=Categories[3], OriginalPrice=50, MarginalPrice=70, Purchases = new List<Purchase> { }},
            new Item { Id = 4, Title="Машинка для выбрации", Stock=3, Category=Categories[4], OriginalPrice=100, MarginalPrice=200, Purchases = new List<Purchase> { } },
            new Item { Id = 5, Title="Красный лак горькость", Stock=1, Category=Categories[2], OriginalPrice=10, MarginalPrice=25, Purchases = new List<Purchase> { } },
            new Item { Id = 6, Title="Наклейка осень", Stock=4, Category=Categories[4], OriginalPrice=12, MarginalPrice=30, Purchases = new List<Purchase> { } },
            new Item { Id = 7, Title="Наклейка весна", Stock=1, Category=Categories[4], OriginalPrice=13, MarginalPrice=27, Purchases = new List<Purchase> { } },
            new Item { Id = 8, Title="Чехол айфон", Stock=0, Category=Categories[4], OriginalPrice=5, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 9, Title="Чехол для всех", Stock=3, Category=Categories[4], OriginalPrice=7, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 10, Title="Что-то", Stock=1, Category=Categories[4], OriginalPrice=99.9, MarginalPrice=199.99, Purchases = new List<Purchase> { } },

        };

        public static List<List<OrderItem>> OrderItems = new List<List<OrderItem>>
        {
            new List<OrderItem> { new OrderItem { Item = Items[0], Quantity=2 },
                                    new OrderItem { Item = Items[1], Quantity=1}},
            new List<OrderItem> { }
        };

        public static IList<Order> Orders = new List<Order>
        {
            new Order { Id = 1, Client=Clients[0], Items=OrderItems[0], State=OrderState.Opened, AlreadyPaid=0, Discount="0"},
            new Order { Id = 2, Client=Clients[1], Items=OrderItems[0], State=OrderState.Opened, AlreadyPaid=10, Discount="0" },
        };
    }
}
