using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;

namespace Services.Services
{
    public class CityService : BaseService<City, CityDTO>
    {
        public CityService(IUnitOfWork uow) : base(uow, uow.CityRepository)
        {

        }
    }
}
