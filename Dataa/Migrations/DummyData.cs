using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Migrations
{
    public static class DummyData
    {
        static DummyData()
        {
            GetItems();
            GeneratePurchases();
            GetClients();
            GetOrders();
        }

        private static void GetOrders()
        {
            for (int i = 0; i < 200; i++)
            {
                var client = Clients.GetRandom();
                var items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Item = Items.GetRandom(),
                            Quantity = RandomNumber(1,10)
                        },
                         new OrderItem
                        {
                            Item = Items.GetRandom(),
                            Quantity = RandomNumber(1,10)
                        },
                          new OrderItem
                        {
                            Item = Items.GetRandom(),
                            Quantity = RandomNumber(1,10)
                        }
                    };
                var sum = items.Aggregate(0.0, (s, item) =>
                {
                    return s + item.Quantity * item.Item.MarginalPrice;
                });
                var toPay = sum;
                var order = new Order
                {
                    Client = client,
                    City = client.City,
                    PhoneNumber = client.PhoneNumber,
                    Items = items,
                    Sum = sum,
                    ToPay = toPay,
                    AlreadyPaid = 0,
                    Discount = "0",
                    MailNumber = RandomNumber(1, 100),
                    Reciever = client.FirstName + ' ' + client.LastName,
                    AdditionalInformation = LoremIpsum(1, 4, 1, 4, 1)

                };
                Orders.Add(order);
            }
        }

        private static void GeneratePurchases()
        {
            foreach (var item in Items)
            {
                item.Purchases.Add(new Purchase
                {
                    Amount = RandomNumber(1, 10),
                    ApproximateArrivalDate = DateTime.Now.AddDays(30),
                    IsArrived = false,
                    Item = item,
                    OrderDate = DateTime.Now,
                    PurchasePlace = PurchasePlaces.GetRandom(),
                    PricePerPiece = RandomNumber(1, 25)
                });
            }
        }

        private static void GetClients()
        {
            for (int i = 0; i < 200; i++)
            {
                Clients.Add(new Client
                {
                    FirstName = names.GetRandom(),
                    LastName = lastNames.GetRandom(),
                    PhoneNumber = "380" + GetRandomNumbers(9),
                    Source = "Инстаграмм",
                    City = Cities.GetRandom(),
                    AdditionalInformation = LoremIpsum(1, 4, 2, 4, 1)
                });
            }
        }

        public static List<string> sources = new List<string>
        {
            "Инстаграмм",
            "Вконтакте",
            "Потелефонь"
        };

        private static void GetItems()
        {
            for (int i = 0; i < 100; i++)
            {

                Items.Add(new Item
                {
                    Title = itemTitles.GetRandom(),
                    Stock = RandomNumber(0, 25),
                    CategoryId = RandomNumber(1, 5),
                    OriginalPrice = RandomNumber(5, 200),
                    AdditionalInformation = LoremIpsum(1, 4, 2, 4, 1),
                    MarginalPrice = RandomNumber(100, 400),
                    Purchases = new List<Purchase>
                    {

                    }
                });
            }
        }

        public static readonly List<City> Cities = new List<City>
        {
                new City { Title = "Днепропетровск" },
                new City { Title = "Киев" },
                new City { Title = "Харьков" }
        };

        public static readonly List<PurchasePlace> PurchasePlaces = new List<PurchasePlace>
        {
                new PurchasePlace {  Title="Aliexpress" },
                new PurchasePlace {  Title="Ebay" },
                new PurchasePlace {  Title="OLX" },
        };

        public static readonly List<Category> Categories = new List<Category>
        {
                new Category { Title = "Кисточки" },
                new Category { Title = "Лаки" },
                new Category { Title = "Стемпинг" },
                new Category { Title = "Flash tatoo" },
                new Category { Title = "Стразы" },
                new Category { Title = "Фольга" },
                new Category { Title = "Чехлы" },
                new Category { Title = "Другое" },
        };

        public static readonly List<Item> Items = new List<Item>
        {
        };

        public static readonly List<Client> Clients = new List<Client>
        {
        };

        public static readonly List<Order> Orders = new List<Order>
        {
        };

        private static readonly Random rnd = new Random();
        private static readonly object syncLock = new object();

        private static readonly List<string> itemTitles = new List<string>
        {
            "Кисточки для рисовашек",
            "Кусачки",
            "Тату",
            "Машинка",
            "Красный лак",
            "Наклейка осень",
            "Наклейка весна",
            "Чехол айфон",
            "Чехол для всех",
        };

        private static readonly List<string> names = new List<string>
        {
            "Велизар",
            "Велимир",
            "Венедикт",
            "Вениамин",
            "Венцеслав",
            "Веньямин",
            "Викентий",
            "Виктор",
            "Викторий",
            "Викул",
            "Викула",
            "Вилен",
            "Виленин",
            "Вильгельм",
            "Виссарион",
            "Вит",
            "Виталий",
            "Витовт",
            "Витольд",
            "Владилен",
            "Владимир",
            "Владислав",
            "Владлен",
            "Влас",
            "Власий",
            "Вонифат",
            "Вонифатий",
            "Всеволод",
            "Всеслав",
            "Вукол",
            "Вышеслав",
            "Вячеслав",
            "Гавриил",
            "Гаврил",
            "Гаврила",
            "Галактион",
            "Гедеон",
            "Гедимин",
            "Геласий",
            "Гелий",
            "Геннадий",
            "Генрих",
            "Георгий",
            "Герасим",
            "Гервасий",
            "Герман",
            "Гермоген",
            "Геронтий",
            "Гиацинт",
            "Глеб",
            "Гораций",
            "Горгоний",
            "Гордей",
            "Гостомысл",
            "Гремислав",
            "Григорий",
            "Гурий",
            "Гурьян",
            "Давид",
            "Давыд",
            "Далмат",
            "Даниил",
            "Данил",
            "Данила",
            "Дементий",
            "Демид",
            "Демьян",
            "Денис",
            "Денисий",
            "Димитрий",
            "Диомид",
            "Дионисий",
            "Дмитрий",
            "Добромысл",
            "Добрыня",
            "Довмонт",
            "Доминик",
            "Донат",
            "Доримедонт",
            "Дормедонт",
            "Дормидбнт",
            "Дорофей",
            "Досифей",
            "Евгений",
            "Евграф",
            "Евграфий",
            "Евдоким",
            "Евлампий",
            "Евлогий",
            "Евмен",
            "Евмений",
            "Евсей",
            "Евстафий",
            "Евстахий",
            "Евстигней",
            "Евстрат",
            "Евстратий"
        };

        private static readonly List<string> lastNames = new List<string>
        {
            "Иванов",
            "Смирнов",
            "Кузнецов",
            "Попов",
            "Васильев",
            "Петров",
            "Соколов",
            "Михайлов",
            "Новиков",
            "Фёдоров",
            "Морозов",
            "Волков",
            "Алексеев",
            "Лебедев",
            "Семёнов",
            "Егоров",
            "Павлов",
            "Козлов",
            "Степанов",
            "Николаев",
            "Орлов",
            "Андреев",
            "Макаров",
            "Никитин",
            "Захаров"
        };


        public static T GetRandom<T>(this List<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        private static string GetRandomString(int count)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz ";
            var stringChars = new char[count];
            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[RandomNumber(0, chars.Length)];
            }
            return new string(stringChars);
        }

        private static string GetRandomNumbers(int count)
        {
            var nums = "1234567890";
            var stringChars = new char[count];
            for (var i = 0; i < count; i++)
            {
                stringChars[i] = nums[RandomNumber(0, nums.Length)];
            }
            return new string(stringChars);
        }

        static string LoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences, int numParagraphs)
        {
            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
          "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
          "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            int numSentences = RandomNumber(0, maxSentences - minSentences) + minSentences + 1;
            int numWords = RandomNumber(0, maxWords - minWords) + minWords + 1;

            System.Text.StringBuilder result = new System.Text.StringBuilder();
            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[RandomNumber(0, words.Length)]);
                    }
                    result.Append(". ");
                }
            }
            return result.ToString();
        }

        #region randomizer

        //Function to get random number

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                // synchronize
                return rnd.Next(min, max);
            }
        }

        #endregion randomizer
    }
}
