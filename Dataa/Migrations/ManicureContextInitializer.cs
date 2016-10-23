using Data.EntityFramework;
using Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.Migrations
{
    public class ManicureContextInitializer : DropCreateDatabaseIfModelChanges<ManicureContext>
    {
        protected override void Seed(ManicureContext context)
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
        }
    }
}
