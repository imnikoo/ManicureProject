using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System.Data.Entity;

namespace Dataa.EntityFramework.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context)
        {

        }
    }
}
