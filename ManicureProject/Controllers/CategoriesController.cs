using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;

namespace ManicureProject.Controllers
{
    public class CategoriesController : AProjectController<Category, CategoryDTO>
    {
        public CategoriesController(BaseService<Category, CategoryDTO> service) : base(service)
        {

        }
    }
}
