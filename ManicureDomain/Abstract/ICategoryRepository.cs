using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface ICategoryRepository : IExtendedRepository<Category>
    {
        ICollection<Category> GetByTitle(string title);
    }
}
