using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;

namespace ManicureProject.Controllers
{
    public class CitiesController : AProjectController<City, CityDTO>
    {
        public CitiesController(BaseService<City, CityDTO> service) : base(service)
        {

        }
    }
}
