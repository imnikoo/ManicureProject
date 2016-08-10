namespace Dataa.Migrations
{
    using Data.Migrations;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.EntityFramework.ManicureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.EntityFramework.ManicureContext context)
        {

            context.Cities.AddRange(DummyData.Cities);
            context.SaveChanges();


            context.PurchasePlaces.AddRange(DummyData.PurchasePlaces);
            context.SaveChanges();


            context.Categories.AddRange(DummyData.Categories);
            context.SaveChanges();

            context.Clients.AddRange(DummyData.Clients);
            context.SaveChanges();

            context.Items.AddRange(DummyData.Items);
            context.SaveChanges();

            context.Orders.AddRange(DummyData.Orders);
            context.SaveChanges();

            //IList<Purchase> Purchases = new List<Purchase>
            //{
            //    new Purchase { Amount=10, OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=42, ItemId = 1 },
            //    new Purchase { Amount=20, OrderDate=DateTime.Now, ApproximateArrivalDate=DateTime.Now.AddDays(30), IsArrived=false, Place=PurchasePlaces[0], PricePerPiece=39, ItemId = 1 },
            //};
            //context.Purchases.AddRange(Purchases);
            //context.SaveChanges();


        }
    }
}
