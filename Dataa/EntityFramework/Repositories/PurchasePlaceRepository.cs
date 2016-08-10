using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System.Data.Entity;

namespace Dataa.EntityFramework.Repositories
{
    public class PurchasePlaceRepository : Repository<PurchasePlace>, IPurchasePlaceRepository
    {
        public PurchasePlaceRepository(DbContext context) : base(context)
        {

        }
    }
}
