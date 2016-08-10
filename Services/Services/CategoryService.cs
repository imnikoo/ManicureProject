using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;

namespace Services.Services
{
    public class CategoryService : BaseService<Category, CategoryDTO>
    {
        public CategoryService(IUnitOfWork uow) : base(uow, uow.CategoryRepository)
        {

        }
    }
}
