using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;

namespace ManicureProject.Controllers
{
    public class PurchasePlacesController : AProjectController<PurchasePlace, PurchasePlaceDTO>
    {
        public PurchasePlacesController(BaseService<PurchasePlace, PurchasePlaceDTO> service) : base(service)
        {

        }
    }
}