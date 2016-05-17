using ManicureDomain.Entities;
using ManicureDomain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            new Purchase { Amount=10, /*Item=Items[2],*/ OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=42 },
            new Purchase { Amount=20, /*Item=Items[2],*/ OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=39 },
        };

        public static IList<City> Cities = new List<City>
        {
            new City { Id = 1, Title = "Днепропетровск" },
            new City { Id = 2, Title = "Киев" },
            new City { Id = 3, Title = "Харьков" }
        };

        public static IList<Client> Clients = new List<Client>
        {
            new Client { Id = 1, Name="Анечка", MailNumber=7, PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[0], },
            new Client { Id = 2, Name="Валечка", MailNumber=3, PhoneNumber="+380930986252", Source="Инстаграмм",  City = Cities[1],  },
            new Client { Id = 3, Name="Галечка", MailNumber=14, PhoneNumber="+380930986252", Source="Инстаграмм", City = Cities[2],  }
        };

        public static IList<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name="Кисточки для рисовашек", Amount=5, Category=Categories[0], OriginalPrice=50, MarginalPrice=100, Purchases = new List<Purchase> { Purchases[0], Purchases[1] }},
            new Item { Id = 2, Name="Кусачки", Amount=7, Category=Categories[3], OriginalPrice=10, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 3, Name="Тату звездочка", Amount=10, Category=Categories[3], OriginalPrice=50, MarginalPrice=70, Purchases = new List<Purchase> { }},
            new Item { Id = 4, Name="Машинка для выбрации", Amount=3, Category=Categories[4], OriginalPrice=100, MarginalPrice=200, Purchases = new List<Purchase> { } },
            new Item { Id = 5, Name="Красный лак горькость", Amount=1, Category=Categories[2], OriginalPrice=10, MarginalPrice=25, Purchases = new List<Purchase> { } },
            new Item { Id = 6, Name="Наклейка осень", Amount=4, Category=Categories[4], OriginalPrice=12, MarginalPrice=30, Purchases = new List<Purchase> { } },
            new Item { Id = 7, Name="Наклейка весна", Amount=1, Category=Categories[4], OriginalPrice=13, MarginalPrice=27, Purchases = new List<Purchase> { } },
            new Item { Id = 8, Name="Чехол айфон", Amount=0, Category=Categories[4], OriginalPrice=5, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 9, Name="Чехол для всех", Amount=3, Category=Categories[4], OriginalPrice=7, MarginalPrice=15, Purchases = new List<Purchase> { } },
            new Item { Id = 10, Name="Что-то", Amount=1, Category=Categories[4], OriginalPrice=99.9, MarginalPrice=199.99, Purchases = new List<Purchase> { } },

        };

        public static List<List<OrderItem>> OrderItems = new List<List<OrderItem>>
        {
            new List<OrderItem> { new OrderItem { Item = Items[0], Quantity=2, Price=Items[0].OriginalPrice*2 },
                                    new OrderItem { Item = Items[1], Quantity=1, Price=Items[1].OriginalPrice*1 }},
            new List<OrderItem> { }
        };

        public static IList<Order> Orders = new List<Order>
        {
            new Order { Id = 1, Client=Clients[0], Items=OrderItems[0], State=OrderState.Opened, AlreadyPaid=0, Discount=0 },
            new Order { Id = 2, Client=Clients[1], Items=OrderItems[0], State=OrderState.Opened, AlreadyPaid=10, Discount=0 },
        };
    }
}
