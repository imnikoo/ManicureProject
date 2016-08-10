using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System.Data.Entity;

namespace Dataa.EntityFramework.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {

        }
    }
}
