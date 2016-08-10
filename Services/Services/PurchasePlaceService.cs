using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;

namespace Services.Services
{
    public class PurchasePlaceService : BaseService<PurchasePlace, PurchasePlaceDTO>
    {
        public PurchasePlaceService(IUnitOfWork uow) : base(uow, uow.PurchasePlaceRepository)
        {

        }
    }
}
