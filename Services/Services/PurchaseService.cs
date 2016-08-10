using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;

namespace Services.Services
{
    public class PurchaseService : BaseService<Purchase, PurchaseDTO>
    {
        public PurchaseService(IUnitOfWork uow) : base(uow, uow.PurchaseRepository)
        {

        }
    }
}
