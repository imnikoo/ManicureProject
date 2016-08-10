using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System.Data.Entity;

namespace Dataa.EntityFramework.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context)
        {

        }
    }
}
